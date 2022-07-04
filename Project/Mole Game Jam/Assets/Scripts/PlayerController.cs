using UnityEngine;

public class PlayerController : Entity
{
    private float _xInput;
    private float _yInput;

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
            if (Input.GetButtonDown("Dig"))
            {
                Debug.Log("holding Dig Button");
                _digComponent.Dig(this);    
            }
            // if dig button is being held
            
        }
    }

    void FixedUpdate()
    {
        _movementComponent.Move(_xInput, _yInput, Speed);
    }

    
}

