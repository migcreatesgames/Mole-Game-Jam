using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Entity
{
    private float _xInput;
    private float _yInput;
    private float curDigHoldTime = 0f;
    private float MAXDigHoldTime = 3f;
    private float _pickUpAnimTime = 2f;

    private bool _enableInput = true;
    private bool _enableMovement = false;
    private bool _isRecharging = false; 

    public Transform _targetTransform;
    public GameObject mainLight;
    private NavMeshAgent _navMeshAgent;

    private static PlayerController _instance;
    private InputHandler _inputHandler;
    private MovementComponent _movementComponent;
    private DigComponent _digComponent;
    private CarryComponent _carryComponent;
    private HideComponent _hideComponent;
    private EatComponent _eatComponent;
    private State _state = State.idle;

    private int _rechargeDelay = 2;
    public static PlayerController Instance { get => _instance; set => _instance = value; }
    public bool EnableMovement { get => _enableMovement; set => _enableMovement = value; }
    public State State { get => _state; set => _state = value; }
    public int RechargeDelay { get => _rechargeDelay; set => _rechargeDelay = value; }
    public bool EnableInput { get => _enableInput; set => _enableInput = value; }
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; set => _navMeshAgent = value; }

    private Animator _animator;
    private bool _encumbered;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        _animator = GetComponentInChildren<Animator>();
        _inputHandler = GetComponent<InputHandler>();
        _movementComponent = GetComponent<MovementComponent>();
        _digComponent = GetComponent<DigComponent>();
        _carryComponent = GetComponent<CarryComponent>();
        _hideComponent = GetComponent<HideComponent>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    protected override void Start()
    {
        MAX_health = GameManager.Instance.GameData.PlayerHealth;
        MAX_stamina = GameManager.Instance.GameData.PlayerStamina;
        MAX_speed = GameManager.Instance.GameData.MovementSpeed;
        MAXDigHoldTime = GameManager.Instance.GameData.DigDuration;
        _carryComponent.EncumberedSpeeds[0] = GameManager.Instance.GameData.MovementSpeed;
        for (int i = 1; i < _carryComponent.EncumberedSpeeds.Length; i++)
        {
            _carryComponent.EncumberedSpeeds[i] =
                GameManager.Instance.GameData.EncumberedSpeeds[i - 1];
        }
        

        base.Start();
    }
    public void OnEnable()
    {
        GameEvents.OnDrop += Drop;
        GameEvents.OnCarry += PickUp;
        GameEvents.OnFoundWorm += DigForWorm;
        GameEvents.OnEat += Eat;
    }

    public void OnDisable ()
    {
        GameEvents.OnDrop -= Drop;
        GameEvents.OnCarry -= PickUp;
        GameEvents.OnFoundWorm -= DigForWorm;
        GameEvents.OnEat -= Eat;    
    }

    void Update()
    {
        if (!GameManager.Instance.IntroPlaying)
        {
            //Debug.Log($"state: {_state}");
            if (_inputHandler && _enableInput)
            {
                //_inputHandler.HandleInput(_instance);s
                // movement
                if (_enableMovement)
                {
                    _xInput = Input.GetAxisRaw("Horizontal");
                    _yInput = Input.GetAxisRaw("Vertical");
                    if (_state != State.hiding || _state != State.digging)
                    {
                        if (_xInput == 0 && _yInput == 0)
                            EnterState(State.idle);
                        else
                            EnterState(State.walking);
                    }
                }

                // digging
                if (Input.GetButton("Dig"))
                {
                    if (_state != State.hiding)
                        EnterState(State.digging);
                }

                if (Input.GetButtonUp("Dig"))
                    ExitState(State.digging);

                // hiding 
                if (Input.GetButton("Hide"))
                {
                    if (_state != State.hiding && _state != State.digging)
                    {
                        if (Stamina > 1)
                            EnterState(State.hiding);
                    }
                    if (_state == State.hiding)
                    {
                        if (Stamina == 0)
                            ExitState(State.hiding);
                        Stamina -= GameManager.Instance.GameData.BurrowStaminaCost;
                        GameEvents.OnStaminaUpdateEvent?.Invoke(Stamina);
                    }
                }

                if (Input.GetButtonUp("Hide"))
                {
                    ExitState(State.hiding);
                    GameEvents.OnStaminaUpdateEvent?.Invoke(Stamina);
                }

                if (Input.GetButtonDown("PickUp"))
                    GameEvents.OnCarry?.Invoke();

                if (Input.GetButtonDown("Eat"))
                    GameEvents.OnEat?.Invoke();

                if (Input.GetButtonDown("Drop"))
                    GameEvents.OnDrop?.Invoke();
            }
        }
        else
        {
            // skip intro cutscene
            if (Input.GetButtonDown("Dig"))
                GameEvents.OnGameBegin?.Invoke();
            // handles ai movement for cutscene
            if (_navMeshAgent.enabled)
                _navMeshAgent.destination = _targetTransform.position;
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.IntroPlaying)
        {
            if (_enableMovement)
            {
                if (_state == State.hiding)
                    Speed = GameManager.Instance.GameData.PlayerBurrowSpeed;
                else 
                    Speed = _carryComponent.RunSpeedCarryingWorms;                
                _movementComponent.Move(_xInput, _yInput, Speed);
            }
        }
    }

    private void EnterState(State state)
    {
        switch (state)
        {
            case State.idle:
                _state = State.idle;

                if (!_carryComponent.IsCarrying)
                    _animator.SetTrigger("Idle");
                else
                    _animator.SetTrigger("Idle_Encumbered");
                break;

            case State.walking:
                _state = State.walking;
                if (!_carryComponent.IsCarrying)
                    _animator.SetTrigger("Walk");
                else
                    _animator.SetTrigger("Walk_Encumbered");
                break;

            case State.digging:
                if (_state != State.digging)
                    _state = State.digging;

                // start digging
                if (curDigHoldTime == 0)
                {
                    // start digging
                    _digComponent.Dig(this);
                    if (_digComponent.CanDig)
                    {
                        // stop stamina recharge
                        if (_isRecharging)
                            CancelRechargeStamina();

                        curDigHoldTime += .01f;
                        Stamina -= GameManager.Instance.GameData.DigStaminaCost;
                        GameEvents.OnStaminaUpdateEvent?.Invoke(Stamina);
                    }
                }

                // continue digging
                if (curDigHoldTime < MAXDigHoldTime && _digComponent)
                {
                    if (_isRecharging)
                        CancelRechargeStamina();

                    curDigHoldTime += .01f;
                    Stamina -= GameManager.Instance.GameData.DigStaminaCost;
                    GameEvents.OnStaminaUpdateEvent?.Invoke(Stamina);
                }

                // stop digging
                if (curDigHoldTime >= MAXDigHoldTime && _digComponent.CanDig)
                    _digComponent.HoleCompleted();

                break;

            case State.hiding:
                _state = State.hiding;

                // stop stamina recharge
                if (_isRecharging)
                    CancelRechargeStamina();
                if (_animator.GetBool("Encumbered"))
                    _encumbered = true;
                if (!GetComponent<DustTrail>().EnableDustTrails)
                    GetComponent<DustTrail>().EnableDustTrails = true;
                _hideComponent.Hide();
                break;
            default:
                break;
        }
    }

    private void ExitState(State state)
    {
        switch (state)
        {
            case State.idle:
                break;
            case State.walking:
                break;
            case State.digging:
                _digComponent.StopDig();
                StartCoroutine("RechargeStamina");
                curDigHoldTime = 0f;
                break;
            case State.hiding:
                _hideComponent.UnHide();
                StartCoroutine("RechargeStamina");
                if (GetComponent<DustTrail>().EnableDustTrails)
                    GetComponent<DustTrail>().EnableDustTrails = false;
                if (_encumbered)
                    _animator.SetBool("Encumbered", true);
                EnterState(State.idle);
                break;
            default:
                break;
        }
    }

    public void Eat()
    {
    
    }
    
    public void PickUp()
    {
        if (_carryComponent.CanPickUp)
        {
            _animator.SetTrigger("Grab");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _pickUpAnimTime = 2f;
            StartCoroutine("DisableMovement");
        }
    }
    
    private void DigForWorm()
    {
        _animator.SetTrigger("Grab");
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        _pickUpAnimTime = 3f;
        StartCoroutine("DisableMovement");
    }

    public void Drop()
    {
        Debug.Log("drop");
    }

    private IEnumerator DisableMovement()
    {
        _enableMovement = false;
        yield return new WaitForSeconds(_pickUpAnimTime);
        _enableMovement = true;
        _animator.SetBool("FoundWorm", false);
        _animator.SetTrigger("Idle_Encumbered");
        //_carryComponent.DugWorm();
        StopCoroutine("DisableMovement");
    }
    
    public override void Death()
    {
        base.Death();
        GameManager.Instance.GameOver(FailStates.playerDied);
    }
    
    public override void RegainHealth(float healthValue)
    {
        base.RegainHealth(healthValue);
        GameEvents.OnHelathUpdateEvent.Invoke(Health);
    }

    public override void DamageTaken(float damageValue)
    {
        base.DamageTaken(damageValue);
        GameEvents.OnDamageEvent?.Invoke(Health);
    }

    private IEnumerator RechargeStamina()
    {
        _isRecharging = true;
        yield return new WaitForSeconds(RechargeDelay);
       
        while (Stamina < MAX_stamina)
        {
            RegainStamina(Stamina += MAX_stamina / 100);
            yield return new WaitForSeconds(.1f);
        }
        _isRecharging = false;
        StopCoroutine(RechargeStamina());
    }
    
    private void CancelRechargeStamina()
    {
        _isRecharging = false;
        StopCoroutine(RechargeStamina());
    }

    public void EnableMainLight()
    {
        mainLight.SetActive(true);
    }

}

public enum State { idle, walking, digging, hiding }
