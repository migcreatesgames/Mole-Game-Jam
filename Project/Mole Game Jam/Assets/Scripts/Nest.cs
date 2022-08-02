using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nest : Entity
{
    
    [SerializeField] private bool _enableHealthLoss;
    [SerializeField] private float _healthLossRate = 1;
    
    [SerializeField] TempUI _ui;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _ui.SetBabiesHealthText(Health);
        OnDamageTakenEvent.AddListener(_ui.SetBabiesHealthText);
        OnRegainHealthEvent.AddListener(_ui.SetBabiesHealthText);
        
    }

    // Update is called once per frame
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

    

    
}
