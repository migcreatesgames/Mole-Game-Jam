using UnityEngine;

public class Nest : Entity
{
    [SerializeField] private bool _enableHealthLoss;
    [SerializeField] private float _healthLossRate = 1;

    protected override void Start()
    {
        base.Start();
        GameEvents.OnFeedBabies += RegainHealth; 
    }

    void Update()
    {
        if (_enableHealthLoss && IsAlive)
            DamageTaken(_healthLossRate * Time.deltaTime);
    }

    public override void Death() {
        base.Death();
        GameManager.Instance.GameOver(FailStates.babiesDied);
    }
    public override void RegainHealth(float healthValue)
    {
        base.RegainHealth(healthValue);
        GameEvents.OnMoleBabiesHungerUpdateEvent?.Invoke(Health);
    }


    public override void DamageTaken(float damageValue)
    {
        Debug.Log($"Health: {Health}");
        base.DamageTaken(damageValue);
        GameEvents.OnMoleBabiesHungerUpdateEvent?.Invoke(Health);
    }
}
