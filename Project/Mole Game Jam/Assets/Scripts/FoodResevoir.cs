using UnityEngine;

public class FoodResevoir : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        var worm = other.GetComponent<Worm>();
        if (worm)
            GameEvents.OnFoodSaved?.Invoke(1);
    }
    private void OnTriggerExit(Collider other)
    {
        var worm = other.GetComponent<Worm>();
        if (worm)
            GameEvents.OnFoodRemoved?.Invoke(1);
    }
}
