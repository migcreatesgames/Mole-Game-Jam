using UnityEngine;
using TMPro;

public class PlayerController : Entity
{
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
    public static PlayerController Instance { get => _instance; set => _instance = value; }
    public bool EnableMovement { get => _enableMovement; set => _enableMovement = value; }

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
    }

    protected override void Start()
    {
        base.Start();
        _ui.SetMoleHealthText(Health);
        OnDamageTakenEvent.AddListener(_ui.SetMoleHealthText);
        OnRegainHealthEvent.AddListener(_ui.SetMoleHealthText);
    }

    void Update()
    {
        if (_inputHandler)
        {
            //_inputHandler.HandleInput(_instance);
            if (_enableMovement)
            {

                _xInput = Input.GetAxisRaw("Horizontal");
                _yInput = Input.GetAxisRaw("Vertical");
                if (_xInput == 0 && _yInput == 0)
                    _animator.SetTrigger("Idle");
                else
                    _animator.SetTrigger("Walk");

            }
            if (Input.GetButton("Dig"))
            {
                //Debug.Log("holding Dig Button");
                //Debug.Log($"dig state: {_digComponent.DigState}");
                //Debug.Log($"curDigHoldTime : {curDigHoldTime}");

                // start digging
                if (curDigHoldTime == 0 && !_digComponent.CanDig)
                {
                    // start digging
                    _digComponent.Dig(this);
                    if (_digComponent.CanDig)
                        curDigHoldTime += .01f;
                }
                // continue digging
                if (curDigHoldTime < MAXDigHoldTime && _digComponent.CanDig)
                {
                    //    Debug.Log("continure digging");
                    curDigHoldTime += .01f;
                }
                // stop digging
                if (curDigHoldTime >= MAXDigHoldTime && _digComponent.CanDig)
                {
                    _digComponent.HoleCompleted();
                    curDigHoldTime = 0f;
                }
            }
        }

        if (Input.GetButtonUp("Dig"))
        {
            // stop digging
            _digComponent.StopDig();
            curDigHoldTime = 0f;
        }
    }

    void FixedUpdate()
    {
        Speed = _carryComponent.RunSpeedCarryingWorms;
        if (_enableMovement)
            _movementComponent.Move(_xInput, _yInput, Speed);
    }
}

