using UnityEngine;

public class Nest : Entity
{
    private bool _enableHealthLoss = false;
    private float _healthLossRate;

    protected override void Start()
    {
        base.Start();
        _healthLossRate = GameManager.Instance.GameData.MoleBabyHungerScale;
        GameEvents.OnFeedBabies += RegainHealth;
        GameEvents.OnGameBegin += EnableHealthLoss;
    }

    void Update()
    {
        if (IsDamagedEnabled())
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

    private void EnableHealthLoss() => _enableHealthLoss = true;
    private void DisableHealthLoss() => _enableHealthLoss = false;


    private bool IsDamagedEnabled()
    {
        return _enableHealthLoss && IsAlive && GameManager.Instance.GameStarted;
    }

    public override void DamageTaken(float damageValue)
    {
        base.DamageTaken(damageValue);
        GameEvents.OnMoleBabiesHungerUpdateEvent?.Invoke(Health);
    }
}
