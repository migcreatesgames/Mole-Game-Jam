using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigComponent : MonoBehaviour
{
    public float MAXHitDistance = 10;
    
    public Vector3 BoxCastSize1, BoxCastSize2;
    public Vector3 _origin;
    public Vector3 _dir;
    private Vector3 _targetPos1, _targetPos2;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    public void Dig(Entity digger)
    {
        _origin = digger.gameObject.transform.position;
        _dir = _origin + (Vector3.forward  * MAXHitDistance);
    }

    private void OnDrawGizmos()
    {
        
        Debug.DrawRay(_origin, _dir.normalized, Color.red);
        Gizmos.DrawWireCube(_origin + Vector3.forward, BoxCastSize1);

        //Debug.DrawRay(_origin , Vector3.down * MAXHitDistance, Color.blue);
        //Gizmos.DrawWireCube(_origin + (_dir.normalized * MAXHitDistance), BoxCastSize2);
    }
}
