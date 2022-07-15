using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerController : Entity
{
    private bool _inCooldown = false;
    private float _cooldownScale = 1;
    private float _xInput;
    private float _yInput;
    private float curDigHoldTime = 0f;
    private float MAXDigHoldTime = 3f;

    private bool _enableMovement = true; 

    private static PlayerController _instance;
    private InputHandler _inputHandler;
    private MovementComponent _movementComponent;
    private DigComponent _digComponent;
    private CarryComponent _carryComponent;
    private HideComponent _hideComponent;
    private State _state = State.idle;

    public static PlayerController Instance { get => _instance; set => _instance = value; }
    public bool EnableMovement { get => _enableMovement; set => _enableMovement = value; }
    public State State { get => _state; set => _state = value; }
    public int CooldownDelay { get; private set; }
    public float _testStaminaCost { get; private set; }

    private Animator _animator;

    [SerializeField] TempUI _ui;

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
    }

    protected override void Start()
    {
        base.Start();
        _ui.SetMoleHealthText(Health);
        //OnDamageTakenEvent.AddListener(_ui.SetMoleHealthText);
        //OnRegainHealthEvent.AddListener(_ui.SetMoleHealthText);
    }

    void Update()
    {
        #region testing 

        if (_inCooldown)
        {
            if (UIManager._instance.StaminaBar.fillAmount >= 0)
            {
                UIManager._instance.StaminaBar.fillAmount = 
                    Mathf.Lerp(UIManager._instance.StaminaBar.fillAmount, 200, Time.deltaTime * _cooldownScale);
                if (Stamina < 200)
                    Stamina = UIManager._instance.StaminaBar.fillAmount * 200;
            }
            if (UIManager._instance.StaminaBar.fillAmount == 1 && Stamina == 200)
            {
                _inCooldown = false;
                StopAllCoroutines();
            }
        }

        if (!_inCooldown && Stamina < _testStaminaCost)
        {
     
            _inCooldown = true;
            StartCoroutine(CoolDown());
        }
        #endregion


        Debug.Log($"state: {_state}");

        if (_inputHandler)
        {
            //_inputHandler.HandleInput(_instance);
            Debug.Log($"state: {_state}");
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
                //Debug.Log("holding Dig Button");
                //Debug.Log($"dig state: {_digComponent.DigState}");
                //Debug.Log($"curDigHoldTime : {curDigHoldTime}");
                if (_state != State.hiding)
                    EnterState(State.digging);
            }

            if (Input.GetButtonUp("Dig"))
            {
                // stop digging
                //if (_state == State.digging)
                ExitState(State.digging);
            }


            // hiding 
            if (Input.GetButton("Hide"))
            { 
                if (_state != State.hiding && _state != State.digging)
                {
                    if (Stamina > 1)
                        EnterState(State.hiding);
                }

                //Debug.Log($"stamina: {Stamina}");
                // deduct stamina
                // check stamina
                if (Stamina <= 0)
                    ExitState(State.hiding);
                Stamina *= .25f;
            }

            if (Input.GetButtonUp("Hide"))
            {
                ExitState(State.hiding);
                Stamina = 1000000000; // for testing will remove later
                //if (_state == State.hiding)
                    
            }
        }
        Debug.Log($"state: {_state}");
    }
    
    void FixedUpdate()
    {
        Speed = _carryComponent.RunSpeedCarryingWorms;
        if (_enableMovement)
            _movementComponent.Move(_xInput, _yInput, Speed);
    }

    private void EnterState(State state)
    {
        switch (state)
        {
            case State.idle:
                state = State.idle;
                _animator.SetTrigger("Idle");

                break;
            case State.walking:
                state = State.walking;
                _animator.SetTrigger("Walk");
                break;

            case State.digging:
                if(state != State.digging)
                    state = State.digging;

                // start digging
                if (curDigHoldTime == 0)
                {
                    // start digging
                    _digComponent.Dig(this);
                    if (_digComponent.CanDig)
                        curDigHoldTime += .01f;
                }
                // continue digging
                if (curDigHoldTime < MAXDigHoldTime && _digComponent.CanDig)
                {
                    //Debug.Log("continure digging");
                    curDigHoldTime += .01f;
                }
                // stop digging
                if (curDigHoldTime >= MAXDigHoldTime && _digComponent.CanDig)
                {
                    _digComponent.HoleCompleted();
                    curDigHoldTime = 0f;
                }
                break;

            case State.hiding:
                state = State.hiding;
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
                curDigHoldTime = 0f;
                break;
            case State.hiding:
                _hideComponent.UnHide();
                if (GetComponent<DustTrail>().EnableDustTrails)
                    GetComponent<DustTrail>().EnableDustTrails = false;
                break;
            default:
                break;
        }
    }

    private IEnumerator CoolDown()
    {
        print("cooldown Called");
        StopCoroutine(AutoCoolDown());
        yield return new WaitForSeconds(CooldownDelay * 2);
        _inCooldown = false;
        StopCoroutine(CoolDown());
    }


    private IEnumerator AutoCoolDown()
    {
        yield return new WaitForSeconds(CooldownDelay * 2f);

        if (!_inCooldown && Stamina < 200 && 
            (_state != State.digging || _state != State.hiding))
        {
            print("auto rechage stamina - recharge = true");
            _inCooldown = true;
            StartCoroutine(CoolDown());
        }
        StopCoroutine(AutoCoolDown());
    }
}

public enum State { idle, walking, digging, hiding}
