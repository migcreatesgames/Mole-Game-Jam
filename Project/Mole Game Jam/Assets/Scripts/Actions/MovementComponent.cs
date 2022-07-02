using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    Rigidbody rb;
    public float runSpeed = 5.0f;

    void Start ()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    public void Move(float x, float y)
    {
        var movementDirection = new Vector3(x, 0, y);
        movementDirection.Normalize();
        rb.velocity = movementDirection * runSpeed;
        if (movementDirection != Vector3.zero)
        {
            rb.MoveRotation(Quaternion.LookRotation(movementDirection, Vector3.up));
        }
    }
}
