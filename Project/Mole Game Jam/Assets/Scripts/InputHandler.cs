using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float _xInput;
    private float _yInput;

    private InputHandler _inputHandler;
    private MovementComponent _movementComponent;
    private DigComponent _digComponent;

    void Update()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");

        // if dig button is being held
        //_digComponent.Dig(new Entity());
    }

    void FixedUpdate()
    {
       // _movementComponent.Move(_xInput, _yInput, Speed);
    }


    public void HandleInput(PlayerController pc)
    {

    }
}
