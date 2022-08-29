using UnityEngine;
using UnityEngine.AI;

public class NinaDeLaTierra : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]private Transform Player;
    public float targetRange = 4.5f;
    private Transform T;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        T = this.transform;
    }

    private void Update()
    {
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
