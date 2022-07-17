using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerController : Entity
{
    private float _xInput;
    private float _yInput;
    private float curDigHoldTime = 0f;
    private float MAXDigHoldTime = 3f;

    private bool _enableMovement = true;
    private bool _isRecharging = false;
    private static PlayerController _instance;
    private InputHandler _inputHandler;
    private MovementComponent _movementComponent;
    private DigComponent _digComponent;
    private CarryComponent _carryComponent;
    private HideComponent _hideComponent;
    private State _state = State.idle;


    private int _rechargeDelay = 2;
    public static PlayerController Instance { get => _instance; set => _instance = value; }
    public bool EnableMovement { get => _enableMovement; set => _enableMovement = value; }
    public State State { get => _state; set => _state = value; }
    public int RechargeDelay { get => _rechargeDelay; set => _rechargeDelay = value; }
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

    void Update()
    {
        if (_inputHandler)
        {
            //_inputHandler.HandleInput(_instance);

            // movement
            if (_enableMovement)
            {
                _xInput = Input.GetAxis("Horizontal");
                _yInput = Input.GetAxis("Vertical");
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
                //Debug.Log($"dig state: {_digComponent.DigState}");
                //Debug.Log($"curDigHoldTime : {curDigHoldTime}");
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
                    Stamina -= .5f;
                    GameEvents.OnStaminaUpdateEvent?.Invoke(Stamina);
                }
            }

            if (Input.GetButtonUp("Hide"))
            {
                ExitState(State.hiding);
                GameEvents.OnStaminaUpdateEvent?.Invoke(Stamina);
            }
        }
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
                if (_state != State.idle)
                    _state = State.idle;
                _animator.SetTrigger("Idle");
                break;

            case State.walking:
                _state = State.walking;
                _animator.SetTrigger("Walk");
                break;

            case State.digging:
                if(_state != State.digging)
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
                        Stamina -= .1f;
                        GameEvents.OnStaminaUpdateEvent?.Invoke(Stamina);
                    }
                }


                // continue digging
                if (curDigHoldTime < MAXDigHoldTime && _digComponent)
                {
                    curDigHoldTime += .01f;
                    Stamina -= .1f;
                    GameEvents.OnStaminaUpdateEvent?.Invoke(Stamina);
                }                
                
                // stop digging
                if (curDigHoldTime >= MAXDigHoldTime && _digComponent.CanDig)
                {
                    _digComponent.HoleCompleted();
                    //ExitState(State.digging);
                }
                break;

            case State.hiding:
                _state = State.hiding;

                // stop stamina recharge
                if (_isRecharging)
                    CancelRechargeStamina();


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
                //StartCoroutine(RechargeStamina());
                curDigHoldTime = 0f;
                break;
            case State.hiding:
                _hideComponent.UnHide();
                StartCoroutine(RechargeStamina());
                if (GetComponent<DustTrail>().EnableDustTrails)
                    GetComponent<DustTrail>().EnableDustTrails = false;
                break;
            default:
                break;
        }
    }

    private IEnumerator RechargeStamina()
    {
        _isRecharging = true;
        Debug.Log("RechargeStamina called");
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
        Debug.Log("CancelRechargeStamina called");
        _isRecharging = false;
        StopCoroutine(RechargeStamina());
    }
}

public enum State { idle, walking, digging, hiding}
