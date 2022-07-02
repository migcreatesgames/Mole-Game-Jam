using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    Rigidbody rb;
  

    void Start ()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    public void Move(float x, float y, float speed)
    {
        var movementDirection = new Vector3(x, 0, y);
        movementDirection.Normalize();
        rb.velocity = movementDirection * speed;
        if (movementDirection != Vector3.zero)
        {
            //rb.MoveRotation(Quaternion.LookRotation(movementDirection, Vector3.up));
            transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        }
    }
}
