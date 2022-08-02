using UnityEngine;

public class CarryComponent : MonoBehaviour
{
    private bool _isCarrying = false;
    private bool _canPickUp = false;
    private bool _nearBaby = false;
    [SerializeField] float[] _runSpeedsCarryingWorms;
    [SerializeField] private int MAX_num_of_worms_carried = 3;

    private Worm _worm;
    private static GameObject _targetWorm;
    private static GameObject _targetBaby;
    [SerializeField] private GameObject[] _worms;
    [SerializeField] private GameObject _consumableWorm;
    private Animator _animator;
    public float RunSpeedCarryingWorms {
        get {
            return _runSpeedsCarryingWorms[_numOfWormsCarried];
        }
    }
    static int _numOfWormsCarried = 0;
    public static int NumOfWormsCarried { get => _numOfWormsCarried; }
    public bool IsCarrying { get => _isCarrying; set => _isCarrying = value; }
    public bool CanPickUp { get => _canPickUp; set => _canPickUp = value; }
    public static GameObject TargetWorm { get => _targetWorm; set => _targetWorm = value; }

    void Awake() {
        Debug.AssertFormat(_runSpeedsCarryingWorms.Length - 1 >= MAX_num_of_worms_carried,
                            "Run speed list shorter than max number of worms carried");
        _animator = GetComponentInChildren<Animator>();

        GameEvents.OnCarry += PickUpWorm;
        GameEvents.OnDrop += DropWorm;
        GameEvents.OnFoundWorm += DugWorm;
    }

    void OnTriggerEnter(Collider other) {
        
        // picking up worm
        var worm = other.GetComponent<Worm>();
        if (worm && _numOfWormsCarried < MAX_num_of_worms_carried)
        {
            _canPickUp = true;
            _worm = worm;
        }
        if (other.gameObject.tag == "Baby")
        {
            //_nearBaby = true;
            _targetBaby = other.gameObject;
            _canPickUp = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        var worm = other.GetComponent<Worm>();
        if (worm || other.gameObject.tag == "Baby")
            _canPickUp = false;
    }
    private void PickUpWorm()
    {
        if (_canPickUp ==true && _numOfWormsCarried < MAX_num_of_worms_carried)
            PickUpWorm(_worm);
    }
    private void PickUpWorm(Worm worm)
    {
        _isCarrying = true;
        if (FoodResevoir.inFoodResevoir)
            GameEvents.OnFoodRemoved?.Invoke(-1);
        _animator.SetBool("Encumbered", true);
        PlayerController.Instance.PickUp();
        _numOfWormsCarried++;   
        DisplayWorm(_numOfWormsCarried);
        _canPickUp = false;
        if (worm != null)
            Destroy(worm.gameObject);
    }

    private void DugWorm()
    {
        _isCarrying = true;
        _animator.SetBool("Encumbered", true);
        _numOfWormsCarried++;
        DisplayWorm(_numOfWormsCarried);
        Destroy(_targetWorm);
    }

    private void DropWorm()
    {
        if (_numOfWormsCarried == 0)
            return;
        _numOfWormsCarried--;
        DisplayWorm(_numOfWormsCarried);
        if (_numOfWormsCarried == 0)
        {
            _isCarrying = false;
            _animator.SetBool("Encumbered", false);
            HideWorm();
        }
        var tmp = GameObject.Instantiate(_consumableWorm, transform.position, transform.rotation);
        tmp.transform.parent = null;
    }

    public void EatWorm()
    {
        _numOfWormsCarried--;
        DisplayWorm(_numOfWormsCarried);
        PlayerController.Instance.RegainHealth(25);

        PlayerController.Instance.Stamina += 50;
        PlayerController.Instance.RegainStamina(PlayerController.Instance.Stamina);
        if (_numOfWormsCarried == 0)
        {
            _isCarrying = false;
            _animator.SetBool("Encumbered", false);
            HideWorm();
        }
        _canPickUp = false;
    }

    public void EatFoodFromFloor()
    {
        if (_targetBaby != null)
        {   
            Destroy(_targetBaby.transform.parent.gameObject);
            PlayerController.Instance.RegainHealth(100);
            PlayerController.Instance.Stamina = 100;
            PlayerController.Instance.RegainStamina(PlayerController.Instance.Stamina);
            GameManager.Instance.EatBaby();

        }
        else if(_worm != null)
        {

            if (FoodResevoir.inFoodResevoir)
                GameEvents.OnFoodRemoved?.Invoke(-1);

            PlayerController.Instance.RegainHealth(25);

            PlayerController.Instance.Stamina += 50;
            PlayerController.Instance.RegainStamina(PlayerController.Instance.Stamina);
            Destroy(_worm.gameObject);
        }
        _canPickUp = false;
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
