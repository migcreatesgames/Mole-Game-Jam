using UnityEngine;

public class DigDetection : MonoBehaviour
{
    private float _curHitDistance;
    public float MAXHitDistance = 10;

    private bool hitDetect1, hitDetect2;

    public Vector3 BoxCastSize1, BoxCastSize2;
    [SerializeField]
    private Transform _originTransform;
    [SerializeField]
    private Transform _targetPos1;
    [SerializeField]
    private Transform _targetPos2;

    public Vector3 _origin;
    public Vector3 _dir1, _dir2;

    private Vector3 _hitPoint1, _hitPoint2;
    private RaycastHit m_Hit;
    public bool Detect(Entity digger)
    {
        // check for diggable wall in front of player  
        // if no wall is detected, check if floor is diggable
        _origin = _originTransform.position;//digger.gameObject.transform.localPosition;
        _dir1 = -(_origin - _targetPos1.position);
        _dir2 = (_targetPos1.position - _targetPos2.position);
        _hitPoint1 = _targetPos1.position;
        _hitPoint2 = _targetPos2.position;

        RaycastHit[] hits = Physics.RaycastAll(_origin, _dir1, 1f);
        //RaycastHit floorDetect =
            //Physics.BoxCastAll(_hitPoint2, BoxCastSize2, _dir2, out m_Hit, transform.rotation);
     
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != null)
            {
                hitDetect1 = true;
                _hitPoint1 = hit.point;
                _hitPoint2 = hit.point - new Vector3(0, .5f, 0);
                _dir1 = -(_origin - _hitPoint1);

                Debug.Log($"_hitPoint1 is hitting: {hit.transform.gameObject.name}");
                return true;
            }
        }
        RaycastHit[] floorDetect =
         Physics.SphereCastAll(_hitPoint2, .5f, _dir2);

        foreach (RaycastHit hit in floorDetect)
        {
            if (hit.transform != null)
            {
                //hitDetect2 = true;
                //_hitPoint1 = hit.point;
                //_hitPoint2 = hit.point - new Vector3(0, .5f, 0);
                //_dir1 = -(_origin - _hitPoint1);

                Debug.Log($"_hitPoint2 is hitting: {hit.transform.gameObject.name}");
            
            }

        }        
        return false;
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
