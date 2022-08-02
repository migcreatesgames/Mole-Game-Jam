using UnityEngine;

public class EatComponent : MonoBehaviour
{
    private void Start()
    {
        GameEvents.OnEat += Eat;    
    }

    private void Eat()
    {
        if (CarryComponent.NumOfWormsCarried > 0)
        {
            GetComponent<CarryComponent>().EatWorm();
            return;
        }
        else
        {
            if (CheckFloorForFood())
                GetComponent<CarryComponent>().EatFoodFromFloor();
        }
    }

    private bool CheckFloorForFood()
    {
        return GetComponent<CarryComponent>().CanPickUp;
    }
}

