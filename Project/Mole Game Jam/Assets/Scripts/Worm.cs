using UnityEngine;

public class Worm : MonoBehaviour, IConsummable
{
    private void Start()
    {
        GameEvents.OnEat += GetEaten;
    }
    public void GetEaten()
    {
        Destroy(gameObject);
    }
}
