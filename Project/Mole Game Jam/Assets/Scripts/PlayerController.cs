using UnityEngine;

public class PlayerController : Entity
{
    private float _xInput;
    private float _yInput;

    private static PlayerController _instance;
    private InputHandler _inputHandler;
    private MovementComponent _movementComponent;
    private DigComponent _digComponent;
    private CarryComponent _carryComponent;

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
            _xInput = Input.GetAxisRaw("Horizontal");
            _yInput = Input.GetAxisRaw("Vertical");

            // if dig button is being held
            _digComponent.Dig(this);
        }
    }

    void FixedUpdate()
    {
        _movementComponent.Move(_xInput, _yInput, Speed);
    }
}

