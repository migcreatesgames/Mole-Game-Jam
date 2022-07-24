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

        if (CheckFloorForFood())
        {

        }
    }

    private bool CheckFloorForFood()
    {
        return GetComponent<CarryComponent>().CanPickUp;
    }
}

public interface IConsummable 
{
    public void GetEaten();
}
