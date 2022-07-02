using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    int numOfWormsCarried;
    [SerializeField] float[] runSpeedsWithWorms;
    CharacterMovement characterMovement;

    public float RunSpeed 
    { 
        get 
        {
            return runSpeedsWithWorms[numOfWormsCarried];
        }
    }
    // [SerializeField] float[] runSpeeds = new float[5f, 4.5f, 4f, 3.5f];

    void Awake() 
    {
        characterMovement = GetComponent<CharacterMovement>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        numOfWormsCarried = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        var worm = other.GetComponent<Worm>();
        if (worm) {
            numOfWormsCarried++;
            GameObject.Destroy(worm.gameObject);
        }
    }
}
