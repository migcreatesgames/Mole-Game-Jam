using UnityEngine;

public class FoodResevoir : MonoBehaviour
{
    public static bool inFoodResevoir = false;
    private void OnTriggerEnter(Collider other)
    {
        var worm = other.GetComponent<Worm>();
        if (worm)
            GameEvents.OnFoodSaved?.Invoke(1);
        if (other.gameObject.tag == "Player")
            inFoodResevoir = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inFoodResevoir = false;
    }
}
