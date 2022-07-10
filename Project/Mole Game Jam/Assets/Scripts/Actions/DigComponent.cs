using UnityEngine;

public class DigComponent : MonoBehaviour
{
    private bool _canDig = false;
    [SerializeField]
    private float MAX_digHoldTime;
    [SerializeField]
    private GameObject _holePrefab;
    private DigDetection _detect;
    public enum DigStates
    {
        init_dig, digging, digging_complete
    }

    private DigStates _digState = DigStates.digging_complete;

    public DigStates DigState { get => _digState; set => _digState = value; }

    private void Start() => Init();

    private void Init()
    {
        _detect = GetComponentInChildren<DigDetection>();
    }

    public void Dig(Entity digger)
    {
        if (_digState == DigStates.digging_complete)
        {
            // check if section in front of player can be dug
            // or far enough from obstables/wall to dig

            DetectionResults results = _detect.Results;
            Debug.Log($"results: {_detect.Results}");
            // init dig action
            _canDig = IsValid(results);
            if (_canDig)
            {
                EnterState(DigStates.init_dig);
            }
        }
    }
    public void StopDig()
    {
        if (_digState == DigStates.digging || _digState == DigStates.init_dig)
            EnterState(DigComponent.DigStates.digging_complete);
    }

    public void HoleCompleted()
    {
        if (_digState != DigStates.digging)
            return;
        SpawnDigPrefab();
        EnterState(DigComponent.DigStates.digging_complete);
    }


    private void StateManager(DigStates state)
    {
        switch (state)
        {
            case DigStates.init_dig:
                // get dig target location
                // stop player from moving
                //curDigHoldTime += .01f;   
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                PlayerController.Instance.EnableMovement = true;
                // start "setup dig" animation
                // start and increment counter that tracks time that
                // button is held down

                if (_digState != DigStates.digging)
                {
                    // get dig target location
                    // stop player from moving
                    Transform targetPos = _detect.DigTargetPos;

                    //PlayerController.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    PlayerController.Instance.EnableMovement = false;
                    //SpawnDigPrefab(targetPos);
                    // start "setup dig" animation
                    // Play didding particles
                    // play digging sound
                    _digState = DigStates.digging;

                    // start and increment counter that tracks time that
                    // button is held down
                }
                break;
            case DigStates.digging:

                break;
            case DigStates.digging_complete:
                SpawnDigPrefab();
                _canDig = false;
                PlayerController.Instance.EnableMovement = true;

                break;
            default:
                break;
        }
    }

    private void EnterState(DigStates state)
    {
        switch (state)
        {
            case DigStates.init_dig:
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                _digState = DigComponent.DigStates.init_dig;
                break;

            case DigStates.digging:
                _digState = DigComponent.DigStates.digging;
                break;

            case DigStates.digging_complete:
                _canDig = false;
                _digState = DigStates.digging_complete;
                PlayerController.Instance.EnableMovement = true;
                break;
            default:
                break;
        }
    }

    private void ExitState(DigStates state)
    {
        switch (state)
        {
            case DigStates.init_dig:
                break;
            case DigStates.digging:
                break;
            case DigStates.digging_complete:
                break;
            default:
                break;
        }
    }

    private bool IsValid(DetectionResults results)
    {
        bool valid = false;
        switch (results)
        {
            case DetectionResults.dig_wall:
                valid = true;
                break;
            case DetectionResults.dig_floor:
                valid = true;
                break;
            case DetectionResults.not_enough_space:
                Debug.Log("warning: not enough space, too close to wall ");
                break;
            case DetectionResults.wall:
                Debug.Log("warning: can't brake through a regular wall");
                break;
            case DetectionResults.hole:
                Debug.Log("warning: can't dig next on hole");
                break;
            default:
                break;
        }

        return valid;
    }

    public void SpawnDigPrefab()
    {
        var tmp = Instantiate(_holePrefab, _detect._targetPos2);
        tmp.transform.parent = null;
        tmp.transform.position = _detect._hitPoint2 - new Vector3(0, .1f, 0);
        // give random rotation for visual variation
    }
}



