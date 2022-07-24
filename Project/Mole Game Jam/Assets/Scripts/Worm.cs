using UnityEngine;

public class Worm : MonoBehaviour, IConsummable
{
    private void OnEnable()
    {
        //GameEvents.OnEat += GetEaten;
    }

    private void OnDisable()
    {
        //GameEvents.OnEat -= GetEaten;
    }

    public void GetEaten()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }
}
    