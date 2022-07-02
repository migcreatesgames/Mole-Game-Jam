using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;
    Character character;

    float horizontal;
    float vertical;
    

    void Awake() {
        rb = GetComponent<Rigidbody>(); 
        character = GetComponent<Character>();
    }

    // Start is called before the first frame update
    void Start ()
    {
        
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
        rb.velocity = movementDirection * character.RunSpeed;
        if (movementDirection != Vector3.zero) {
            rb.MoveRotation(Quaternion.LookRotation(movementDirection, Vector3.up));
        }
    }  
}
