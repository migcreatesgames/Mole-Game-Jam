using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryComponent : MonoBehaviour
{
    [SerializeField] float[] _runSpeedsCarryingWorms;
    public float RunSpeedCarryingWorms {
        get {
            return _runSpeedsCarryingWorms[_numOfWormsCarried];
        }
    }
    int _numOfWormsCarried;
    public int NumOfWormsCarried {get => _numOfWormsCarried; }
    // Start is called before the first frame update
    void Start()
    {
        _numOfWormsCarried = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        var worm = other.GetComponent<Worm>();
        if (worm) {
            _numOfWormsCarried++;
            GameObject.Destroy(worm.gameObject);
        }
    }
}
