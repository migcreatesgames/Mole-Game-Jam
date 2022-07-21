using UnityEngine;

public class DigDetection : MonoBehaviour
{
    [SerializeField]
    private float _minHitDistance = 1f;
    private bool _detectionEnabled = true;
    private bool _checkFloor = false;
    private bool _foundWorm = false;

    public float MAXHitDistance = 10;
    public float SphereRadius = 1f;

    public Vector3 BoxCastSize;
    public Vector3 _origin;
    public Vector3 _dir1, _dir2;

    [SerializeField]
    private Transform _originTransform;
    [SerializeField]
    private Transform _targetPos1;
    [SerializeField]
    public Transform _targetPos2;
    private Transform _digTargetPos;
    private GameObject _targetWall;

    public Vector3 _hitPoint1, _hitPoint2;

    [SerializeField] bool _debugging = false;

    private DetectionResults _results;
    public DetectionResults Results { get => _results; set => _results = value; }
    public Transform DigTargetPos { get => _digTargetPos; set => _digTargetPos = value; }
    public GameObject TargetWall { get => _targetWall; set => _targetWall = value; }
    public bool FoundWorm { get => _foundWorm; set => _foundWorm = value; }

    private void FixedUpdate()
    {
        Debug.Log($"_foundWorm: {_foundWorm}");
        if (_debugging)
        {
            Debug.Log($"_checkFloor: {_checkFloor}");
        }
        if (_detectionEnabled)
            Detect();
    }

    private void Detect()
    {
        // check for diggable wall in front of player  
        // if no wall is detected, check if floor is diggable
        _origin = _originTransform.position;
        _dir1 = -(_origin - _targetPos1.position);
        _dir2 = (_targetPos1.position - _targetPos2.position);
        _hitPoint1 = _targetPos1.position;
        _hitPoint2 = _targetPos2.position;
        _foundWorm = false;
        RaycastHit[] wallHits = Physics.RaycastAll(_origin, _dir1, 1f);
        if (wallHits.Length == 0)
            _checkFloor = true;

        foreach (RaycastHit hit in wallHits)
        {
            if (hit.transform != null)
            {
                //Debug.Log($"_hitPoint1 is hitting: {hit.transform.gameObject.name} | {hit.transform.gameObject.tag}");

                if (hit.transform.gameObject.tag == "DiggableWall")
                {
                    _results = DetectionResults.dig_wall;
                    _targetWall = hit.transform.gameObject;
                    _checkFloor = false;
                }

                if (hit.transform.gameObject.tag == "Wall")
                {
                    _hitPoint1 = hit.point;
                    _hitPoint2 = hit.point - new Vector3(0, .5f, 0);
                    _dir1 = -(_origin - _hitPoint1);

                    // check distance between player and wall
                    if (CheckDistance(_origin, _hitPoint1))
                        _checkFloor = true;
                    else
                    {
                        _checkFloor = false ;
                        _results = DetectionResults.not_enough_space;
                    }
                }
            }
        }

        if (!_checkFloor)
            return;
        RaycastHit[] floorHits = Physics.SphereCastAll(_hitPoint2, SphereRadius, transform.forward);
        //RaycastHit[] floorHits = Physics.SphereCastAll(_hitPoint2, SphereRadius, -transform.up);

        foreach (RaycastHit hit in floorHits)
        {
            if (hit.transform.gameObject.tag == "Player")
                continue;

            if (hit.transform.gameObject.tag == "Worm")
            {
                _foundWorm = true;
                // do not like this but have to
                CarryComponent.TargetWorm = hit.transform.parent.gameObject;
                Debug.Log($"target: {CarryComponent.TargetWorm}");
            }

            if (hit.transform.gameObject.tag == "HoleMound")
            {
                // do not like this but have to
                DigComponent.MoundTarget = hit.transform.gameObject;
                Debug.Log($"target: {CarryComponent.TargetWorm}");
            }



            //Debug.Log($"_hitPoint2 is hitting: {hit.transform.gameObject.name} | {hit.transform.gameObject.tag}");
            if (hit.transform.gameObject.tag == "DiggableFloor")
                _results = DetectionResults.dig_floor;
            if (hit.transform.gameObject.tag == "Hole")
                _results = DetectionResults.hole;
        }
    }

    private bool CheckDistance(Vector3 pos1, Vector3 pos2)
    {
        float distance = Vector3.Distance(pos1, pos2);
        if (distance < _minHitDistance)
            return false;
        return true;
    }
    
    private void OnDrawGizmos()
    {
        Debug.DrawRay(_origin, _dir1, Color.red);
        Gizmos.DrawWireCube(_hitPoint1, BoxCastSize);

        Debug.DrawRay(_hitPoint1, -_dir2, Color.blue);
        Gizmos.DrawWireSphere(_hitPoint2, SphereRadius);
    }
}

public enum DetectionResults
{
    dig_wall, dig_floor, not_enough_space, wall, hole 
}
