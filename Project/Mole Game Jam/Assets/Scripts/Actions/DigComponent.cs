using UnityEngine;

public class DigComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _holePrefab;
    private DigDetection _detect;
    public enum DigStates
    {
        init_dig, digging, digging_complete
    }

     private DigStates _digState = DigStates.digging_complete;

     public DigStates DigState { get => _digState; set => _digState = value; }

    private void Start () => Init();

    private void Init()
    {
        _detect = GetComponentInChildren<DigDetection>();
    }

    public void Dig(Entity digger)
    {
        // check if section in front of player can be dug
        // or far enough from obstables/wall to dig
        DetectionResults results = _detect.Results;
        Debug.Log($"results: {_detect.Results}");
        
        // init dig action

        SetupDig(results);

        //switch (_digStates)
        //{
        //    case DigStates.init_dig:
        //        break;
        //    case DigStates.digging:
        //        break;
        //    case DigStates.digging_complete:
        //        
        //        break;
        //    default:
        //        break;
        //}
    }

    private void SetupDig(DetectionResults results)
    {
        switch (results)        
        {
            case DetectionResults.dig_wall:
                // get dig target location
                // stop player from moving
                PlayerController.Instance.EnableMovement = true; 
                // start "setup dig" animation

                // start and increment counter that tracks time that
                // button is held down
                break;
            case DetectionResults.dig_floor:
                if (_digState == DigComponent.DigStates.digging_complete)
                {
                    SpawnDigPrefab();
                    PlayerController.Instance.EnableMovement = true;
                    return;
                }
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
            case DetectionResults.not_enough_space:
                PlayerController.Instance.EnableMovement = true;
                Debug.Log("warning: not enough space, too close to wall ");
                break;
            case DetectionResults.wall:

                PlayerController.Instance.EnableMovement = true;
                Debug.Log("warning: can't brake through a regular wall");
                break;
            case DetectionResults.hole:
                PlayerController.Instance.EnableMovement = true;
                Debug.Log("warning: can't dig next on hole");
                break;
            default:
                break;
        }
    }

    
    public void SpawnDigPrefab()
    {
        var tmp = Instantiate(_holePrefab, _detect._targetPos2) ;
        tmp.transform.parent = null;
        tmp.transform.position = _detect._hitPoint2 -new Vector3(0,.1f,0); 
        // give random rotation for variation look
    }
}



