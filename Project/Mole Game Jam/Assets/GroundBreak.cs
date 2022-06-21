using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBreak : MonoBehaviour
{
    void OnCollisionEnter(Collision colEnter)
    {
        if (colEnter.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
