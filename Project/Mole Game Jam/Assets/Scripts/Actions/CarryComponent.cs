using UnityEngine;

public class CarryComponent : MonoBehaviour
{
    private bool _isCarrying = false;
    [SerializeField] float[] _runSpeedsCarryingWorms;
    [SerializeField] private int MAX_num_of_worms_carried = 3;

    [SerializeField] private GameObject[] _worms;
    private Animator _animator;
    public float RunSpeedCarryingWorms {
        get {
            return _runSpeedsCarryingWorms[_numOfWormsCarried];
        }
    }
    int _numOfWormsCarried = 0;
    public int NumOfWormsCarried { get => _numOfWormsCarried; }
    public bool IsCarrying { get => _isCarrying; set => _isCarrying = value; }

    void Awake() {
        Debug.AssertFormat(_runSpeedsCarryingWorms.Length - 1 >= MAX_num_of_worms_carried,
                            "Run speed list shorter than max number of worms carried");
        _animator = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter(Collider other) {
        // picking up worm
        var worm = other.GetComponent<Worm>();
        if (worm && _numOfWormsCarried < MAX_num_of_worms_carried)
            PickUpWorm(worm);


        // feeding the nest
        var nest = other.GetComponent<Nest>();
        if (nest && _numOfWormsCarried > 0) {
            nest.RegainHealth(_numOfWormsCarried * GameManager.Instance.WORM_HP);
            _numOfWormsCarried = 0;
        }
    }

    private void PickUpWorm(Worm worm)
    {
        _isCarrying = true;
        _animator.SetBool("Encumbered", true);
        _numOfWormsCarried++;
        DisplayWorm(_numOfWormsCarried);
        GameObject.Destroy(worm.gameObject);
    }

    private void RemoveWorm()
    {
        _numOfWormsCarried--;
        DisplayWorm(_numOfWormsCarried);
        if (_numOfWormsCarried == 0)
        {
            _isCarrying = false;
            _animator.SetBool("Encumbered", false);
        }
    }
    private void DisplayWorm(int nums)
    {
        if (nums > 0)
            HideWorm();
        for (int i = 0; i < nums; i++)
            _worms[i].SetActive(true);
    }
    private void HideWorm()
    {
        for (int i = 0; i < _worms.Length; i++)
            _worms[i].SetActive(false);
    }
}
