using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigComponent : MonoBehaviour
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

    private Vector3 hitPoint1, hitPoint2;


    public void Dig(Entity digger)
    {
        // stop player from moving
        // check if section in front of player can be dug or far enough from obstables/wall to dig
        // if so start dig animation
        // else tell player area not diggable or too close to wall

        _origin = _originTransform.position;//digger.gameObject.transform.localPosition;
        _dir1 = -(_origin - _targetPos1.position);
        _dir2 = (_targetPos1.position - _targetPos2.position);
        hitPoint1 = _targetPos1.position;
        hitPoint2 = _targetPos2.position;

        RaycastHit[] hits = Physics.RaycastAll(_origin, _dir1, 1f);
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != null)
            {
                hitDetect1 = true;
                hitPoint1 = hit.point;
                hitPoint2 = hit.point - new Vector3(0, .5f, 0);
                _dir1 = -(_origin - hitPoint1);
                
                Debug.Log($"hitting: {hit.transform.gameObject.name}");
            }              
        }
    }

    private void OnDrawGizmos()
    {
        // check for diggable wall in front of player  
        Debug.DrawRay(_origin, _dir1, Color.red);
        Gizmos.DrawWireCube(hitPoint1, BoxCastSize1);
      
        Debug.DrawRay(hitPoint1, -_dir2, Color.blue);
        Gizmos.DrawWireCube(hitPoint2, BoxCastSize2);
        // if no wall is detected, check if floor is diggable
    }
}
