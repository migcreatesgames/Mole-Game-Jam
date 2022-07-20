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
        if (Distance() < targetRange)
        {
            navMeshAgent.destination = Player.position;
        }
        else
        {
            Patrol();
        }

    }

    public void Patrol()
    {
        if (transform.position == nextPos.position)
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
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
        }
    }


    private float Distance()
    {
        return Vector3.Distance(T.position, Player.position);
    }

}
