using UnityEngine;

public class Nest : Entity
{
    [SerializeField] private bool _enableHealthLoss;
    [SerializeField] private float _healthLossRate = 1;

    void Update()
    {
        if (_enableHealthLoss && IsAlive)
            DamageTaken(_healthLossRate * Time.deltaTime);
    }

    public override void Death() {
        base.Death();
        GameManager.Instance.GameOver();
    }

    public override void DamageTaken(float damageValue)
    {
        base.DamageTaken(damageValue);
        GameEvents.OnMoleBabiesHungerUpdateEvent?.Invoke(Health);
    }
}
