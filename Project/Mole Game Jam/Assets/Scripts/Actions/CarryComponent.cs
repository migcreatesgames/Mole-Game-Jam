using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryComponent : MonoBehaviour
{
    [SerializeField] float[] _runSpeedsCarryingWorms;
    [SerializeField] private int MAX_num_of_worms_carried = 3;
    public float RunSpeedCarryingWorms {
        get {
            return _runSpeedsCarryingWorms[_numOfWormsCarried];
        }
    }
    int _numOfWormsCarried;
    public int NumOfWormsCarried {get => _numOfWormsCarried; }
    // Start is called before the first frame update
    
    void Awake() {
        Debug.AssertFormat(_runSpeedsCarryingWorms.Length - 1 >= MAX_num_of_worms_carried, 
                            "Run speed list shorter than max number of worms carried");
    }
    
    void Start()
    {
        _numOfWormsCarried = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        // picking up worm
        var worm = other.GetComponent<Worm>();
        if (worm && _numOfWormsCarried < MAX_num_of_worms_carried) {
            _numOfWormsCarried++;
            GameObject.Destroy(worm.gameObject);
        }

        // feeding the nest
        var nest = other.GetComponent<Nest>();
        if (nest && _numOfWormsCarried > 0) {
            nest.RegainHealth(_numOfWormsCarried * GameManager.Instance.WORM_HP);
            _numOfWormsCarried = 0;
        }
    }
}
