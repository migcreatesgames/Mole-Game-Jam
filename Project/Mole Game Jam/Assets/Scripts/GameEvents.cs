using System;
/// <summary>
/// class that stores generic static action Game Events. 
/// </summary>

public class GameEvents
{
    public static Action OnKilledEvent;
    public static Action OnCarry;
    public static Action OnDrop;
    public static Action OnFoundWorm;
    public static Action OnEat;
    public static Action OnGameBegin;
    public static Action OnTimerFinished;

    public static Action<int> OnFoodSaved;
    public static Action<int >OnFoodRemoved;

    public static Action<float> OnFeedBabies;
    public static Action<float> OnDamageEvent;
    public static Action<float> OnHelathUpdateEvent;
    public static Action<float> OnStaminaUpdateEvent;
    public static Action<float> OnMoleBabiesHungerUpdateEvent;
}   