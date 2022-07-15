using System;

/// <summary>
/// static class that store generic Game Events. 
/// </summary>

public static class GameEvents
{
    public static Action<float> OnDamageEvent;
    public static Action OnKilledEvent;

    public static Action<float> OnStaminaUpdateEvent;
    public static Action<float> OnMoleBabiesHungerUpdateEvent;


    //public static Action OnAddCameraTarget;
    //public static Action OnRemoveCameraTarget;
}