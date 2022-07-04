using UnityEngine;

public class DigDetection : MonoBehaviour
{
    private float _curHitDistance;
    [SerializeField]
    private float _minHitDistance = 100f;
    private bool _detectionEnabled = true;
    private bool hitDetect1, hitDetect2;
    private bool _checkFloor = false;

    public float MAXHitDistance = 10;

    public Vector3 BoxCastSize1, BoxCastSize2;
    public Vector3 _origin;
    public Vector3 _dir1, _dir2;

    [SerializeField]
    private Transform _originTransform;
    [SerializeField]
    private Transform _targetPos1;
    [SerializeField]
    private Transform _targetPos2;
    private Transform _digTargetPos; 

    private Vector3 _hitPoint1, _hitPoint2;
    private RaycastHit m_Hit;

    private DetectionResults _results;
    public DetectionResults Results { get => _results; set => _results = value; }
    public Transform DigTargetPos { get => _digTargetPos; set => _digTargetPos = value; }


    private void Update()
    {
        if (_detectionEnabled)
            Detect();
    }

    private void Detect()
    {
        // check for diggable wall in front of player  
        // if no wall is detected, check if floor is diggable
        _origin = _originTransform.position;//digger.gameObject.transform.localPosition;
        _dir1 = -(_origin - _targetPos1.position);
        _dir2 = (_targetPos1.position - _targetPos2.position);
        _hitPoint1 = _targetPos1.position;
        _hitPoint2 = _targetPos2.position;

        RaycastHit[] hits = Physics.RaycastAll(_origin, _dir1, 1f);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != null)
            {
                Debug.Log($"_hitPoint1 is hitting: {hit.transform.gameObject.name} | {hit.transform.gameObject.tag}");
                if (hit.transform.gameObject.tag == "DiggableWall")
                {

                    _results = DetectionResults.dig_wall;
                    _checkFloor = false;
                }
                else
                {
                    hitDetect1 = true;
                    _hitPoint1 = hit.point;
                    _hitPoint2 = hit.point - new Vector3(0, .5f, 0);
                    _dir1 = -(_origin - _hitPoint1);

                    // check distance between player and wall
                    if (CheckDistance(_hitPoint1, _hitPoint2))
                        _checkFloor = true;
                    else
                        _results = DetectionResults.not_enough_space;
                }
            }
        }
        

        if (_checkFloor)
        {
            RaycastHit[] floorDetect = Physics.SphereCastAll(_hitPoint2, .35f, _dir2);

            foreach (RaycastHit hit in floorDetect)
            {
                if (hit.transform != null)
                {
                    if (hit.transform.gameObject.tag == "Player")
                        continue;

                    Debug.Log($"_hitPoint2 is hitting: {hit.transform.gameObject.name} | {hit.transform.gameObject.tag}");
                    if (hit.transform.gameObject.tag == "DiggableFloor")
                    {
                        _results = DetectionResults.dig_floor;
                    }   
                    //hitDetect2 = true;
                    //_hitPoint1 = hit.point;
                    //_hitPoint2 = hit.point - new Vector3(0, .5f, 0);
                    //_dir1 = -(_origin - _hitPoint1);
                }
            }
            _checkFloor = false;
        }
    }

    private bool CheckDistance(Vector3 pos1, Vector3 pos2)
    {
        float distance = Vector3.Distance(pos1, pos2);
        Debug.Log($"distance: {distance}");
        if (distance < _minHitDistance)
            return false;
        return true;
    }
    
    private void OnDrawGizmos()
    {
        Debug.DrawRay(_origin, _dir1, Color.red);
        Gizmos.DrawWireCube(_hitPoint1, BoxCastSize1);

        Debug.DrawRay(_hitPoint1, -_dir2, Color.blue);
        Gizmos.DrawWireSphere(_hitPoint2, .35f);
        //Gizmos.DrawWireCube(_hitPoint2, BoxCastSize2);
    }
}

public enum DetectionResults
{
    dig_wall, dig_floor, not_enough_space, wall, hole 
}
