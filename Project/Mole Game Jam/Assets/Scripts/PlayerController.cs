using UnityEngine;

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

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        _inputHandler = GetComponent<InputHandler>();
        _movementComponent = GetComponent<MovementComponent>();
        _digComponent = GetComponent<DigComponent>();
        _carryComponent = GetComponent<CarryComponent>();
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
            }
            if (Input.GetButton("Dig"))
            {
        
                Debug.Log("holding Dig Button");
                Debug.Log($"dig state: {_digComponent.DigState}");
                Debug.Log($"curDigHoldTime : {curDigHoldTime}");
                
                if (curDigHoldTime > MAXDigHoldTime && _digComponent.DigState == DigComponent.DigStates.digging)
                {
                    // stop digging
                    curDigHoldTime = 0f;
                    _digComponent.DigState = DigComponent.DigStates.digging_complete;
                    _digComponent.Dig(this);
                }
                if (curDigHoldTime == 0 && _digComponent.DigState == DigComponent.DigStates.digging_complete)
                {
                    // start digging
                    curDigHoldTime += .01f;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    _digComponent.DigState = DigComponent.DigStates.init_dig;
                    _digComponent.Dig(this);
                }
                if (curDigHoldTime < MAXDigHoldTime && _digComponent.DigState == DigComponent.DigStates.digging)
                {
                    // continue digging
                    Debug.Log("continure digging");
                    curDigHoldTime += .01f;
                    _digComponent.Dig(this);
                }     
            }
        }

        if (Input.GetButtonUp("Dig"))
        {
            curDigHoldTime = 0f;
            _digComponent.DigState = DigComponent.DigStates.digging_complete;
            PlayerController.Instance.EnableMovement = true;
        }
    }

    void FixedUpdate()
    {
        _movementComponent.Move(_xInput, _yInput, Speed);
    }

    
}

