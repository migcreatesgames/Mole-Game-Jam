using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody rb;

    float horizontal;
    float vertical;
    public float runSpeed = 5.0f;

    // Start is called before the first frame update
    void Start ()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void Update ()
    {
        if (horizontal != 0) {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        else if (vertical != 0) {
            vertical = Input.GetAxisRaw("Vertical");
        } else {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
    }

    private void FixedUpdate()
    {  
        var movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection.Normalize();
        rb.velocity = movementDirection * runSpeed;
        if (movementDirection != Vector3.zero) {
            rb.MoveRotation(Quaternion.LookRotation(movementDirection, Vector3.up));
        }
    }
}
