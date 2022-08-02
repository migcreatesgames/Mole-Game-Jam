using UnityEngine;

public class FoodZone : MonoBehaviour
{
    public float FoodValue = 25f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Worm>())
        {
            Debug.Log("feed moles");
            Destroy(other.gameObject);
            FeedBabies();
        }
    }

    private void FeedBabies()
    {
        GameEvents.OnFeedBabies?.Invoke(FoodValue);
    }
}
