using UnityEngine;
using UnityEngine.Events;

public class Entity: MonoBehaviour
{
    private float _health;
    private float _stamina = 10000;
    private float _speed = 5.0f;
    private bool _isAlive = true; 
    

    protected float MAX_health = 100f;
    protected float MAX_stamina = 100f;

    public float Health { get => _health; protected set => _health = value; }
    public float Stamina { get => _stamina; protected set => _stamina = value; }
    public float Speed { get => _speed; protected set => _speed = value; }
    public bool IsAlive { get => _isAlive; }

    protected virtual void Start() 
    {
        _health = MAX_health;
    }

    public virtual void DamageTaken(float damageValue)
    {
        Debug.Log($"damageValue: {damageValue}");

        _health -= damageValue;
        Debug.Log($"healtg: {_health}");
        if (_health <= 0 && _isAlive)
        {
            Death();
            return;
        }
        GameEvents.OnDamageEvent?.Invoke(_health);
    }

    public virtual void RegainHealth(float healthValue)
    {
        if (MAX_health - _health < healthValue)
        {
            _health = MAX_health;
        } else {
            _health += healthValue;
        }
        GameEvents.OnHelathUpdateEvent.Invoke(_health) ;
    }

    public virtual void RegainStamina(float staminaValue)
    {

    }

    public void StaminaUsed(float staminaValue)
    {

    }

    public virtual void Death()
    {
        _isAlive = false;
        //GameState = fail
        //Play death animation
        //Play scene loading animation
        // Load Main Menu scene
        //OnDeathEvent.Invoke();
    }
}

