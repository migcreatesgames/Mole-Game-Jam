using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NinaDeLaTierra : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]private Transform Player;

    private Transform T;

    [Header("Movement")]
    [SerializeField] private Transform[] Positions;
    [SerializeField] private float speed;
    private Transform nextPos;
    private int nextPosIndex;
    public float StartDelay;
    private float Delay;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        T = this.transform;
    }

    void Start()
    {
        nextPos = Positions[0];
        Delay = StartDelay;
    }

    void Update()
    {
        float targetRange = 4.5f;
        if (Distance() < targetRange && PlayerController.Instance.GetComponent<HideComponent>().IsVisible)
        {
            navMeshAgent.destination = Player.position;
        }
        //else
        //{
        //    navMeshAgent.destination = nextPos.position;
        //}
        else
        {
            Patrol();
        }

    }

    public void Patrol()
    {
        //Vector3 Mypos = ;
        if (Vector3.Distance(navMeshAgent.destination, nextPos.position) <= .75f)
        {
            Delay -= Time.deltaTime;
            if (Delay <= 0)
            {
                nextPosIndex++;
                Delay = StartDelay;
            }

            if (nextPosIndex >= Positions.Length)
            {

                //nextPos = Positions[nextPosIndex];
                nextPosIndex = (nextPosIndex + 0) % Positions.Length;
            }
            nextPos = Positions[nextPosIndex];
        }
        else
        {
            navMeshAgent.destination = nextPos.position;
        }
    }


    private float Distance()
    {
        return Vector3.Distance(T.position, Player.position);
    }

}
