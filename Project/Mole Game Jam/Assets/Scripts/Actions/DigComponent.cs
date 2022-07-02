using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigComponent : MonoBehaviour
{
    private float _curHitDistance;

    public float MAXHitDistance = 10;
    
    public Vector3 BoxCastSize1, BoxCastSize2;
    public Transform originTransform;   
    public Transform targetTransform1;
    public Transform targetTransform2;
    public Vector3 _origin1;
    public Vector3 _dir1, _dir2;
    private Vector3 _targetPos1, _targetPos2;

    public void Dig(Entity digger)
    {
        // stop player from moving
        // check if section in front of player can be dug or far enough from obstables/wall to dig
        // if so start dig animation
        // else tell player area not diggable or too close to wall

        _origin1 = originTransform.position;//digger.gameObject.transform.localPosition;
        _dir1 = (_origin1 - targetTransform1.position);
        _dir2 = (targetTransform1.position - targetTransform2.position);



    }

    private void OnDrawGizmos()
    {
        // check for diggable wall in front of player  
       
        Debug.DrawRay(_origin1, -_dir1, Color.red);
        Gizmos.DrawWireCube(targetTransform1.position, BoxCastSize1);

        Debug.DrawRay(targetTransform1.position, -_dir2, Color.blue);
        Gizmos.DrawWireCube(targetTransform2.position, BoxCastSize2);
        // if no wall is detected, check if floor is diggable
     
    }
}
