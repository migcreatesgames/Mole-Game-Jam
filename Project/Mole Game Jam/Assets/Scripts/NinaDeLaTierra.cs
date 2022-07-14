using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NinaDeLaTierra : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]private Transform Player;

    private Transform T;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        T = this.transform;
    }

    private void Update()
    {
        float targetRange = 4.5f;
        if (Distance() < targetRange && PlayerController.Instance.GetComponent<HideComponent>().IsVisible)
        {
            navMeshAgent.destination = Player.position;
        }
            
    }

    private float Distance()
    {
        return Vector3.Distance(T.position, Player.position);
    }

}
