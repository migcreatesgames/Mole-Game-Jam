using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NinaDeLaTierra : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform Player;

    private void Update()
    {
        navMeshAgent.destination = Player.position;
    }

}
