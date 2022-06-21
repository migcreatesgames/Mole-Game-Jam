using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("RidgidBody")]
    public Rigidbody rb;
    private Vector3 PlayerVelocity;
    public float speed;

    public static bool gameover;

    void Start()
    {
        gameover = false;
        Cursor.visible = false;
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        Vector3 moveinput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f,Input.GetAxisRaw("Vertical"));
        PlayerVelocity = moveinput.normalized * speed;
        if (moveinput.x >= 0)
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (moveinput.x < 0)
        {
            //transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void FixedUpdate()
    {
        if (!gameover)
        {
            rb.MovePosition(rb.position + PlayerVelocity * Time.fixedDeltaTime);
        }
    }

}
