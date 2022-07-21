using UnityEngine;

public class Nest : Entity
{
    [SerializeField] private bool _enableHealthLoss;
    [SerializeField] private float _healthLossRate = 1;
    
    [SerializeField] TempUI _ui;
    
    protected override void Start()
    {
        base.Start();
        _ui.SetBabiesHealthText(Health);
 
        
        //OnDamageTakenEvent.AddListener(_ui.SetBabiesHealthText);
        //OnRegainHealthEvent.AddListener(_ui.SetBabiesHealthText);   
    }

    void Update()
    {
        if (_enableHealthLoss && IsAlive) {
            DamageTaken(_healthLossRate * Time.deltaTime);
         
        }
    }

    public override void Death() {
        base.Death();
        _ui.SetBabiesHealthText(0f);
        GameManager.Instance.GameOver();
    }

    public override void DamageTaken(float damageValue)
    {
        base.DamageTaken(_healthLossRate * Time.deltaTime);
        GameEvents.OnMoleBabiesHungerUpdateEvent?.Invoke(Health);
    }
}
