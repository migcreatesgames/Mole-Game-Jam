using UnityEngine;
using UnityEngine.Events;

public class Entity: MonoBehaviour
{
    private float _health;
    [SerializeField] protected float _defaultHealth = 100f;
    private float _stamina;
    private float _speed = 5.0f;
    private bool _isAlive = true; 
    

    protected float MAX_health = 100f;
    protected float MAX_stamina = 100f;

    public float Health { get => _health; protected set => _health = value; }
    public float Stamina { get => _stamina; protected set => _stamina = value; }
    public float Speed { get => _speed; protected set => _speed = value; }
    public bool IsAlive { get => _isAlive; }

    //public OnDamageTaken OnDamageTakenEvent;
    public UnityEvent<float> OnDamageTakenEvent;
    public UnityEvent<float> OnRegainHealthEvent;
    public UnityEvent OnDeathEvent;


    protected virtual void Start() {
        _health = _defaultHealth;
        OnDamageTakenEvent = new UnityEvent<float>();
        OnRegainHealthEvent = new UnityEvent<float>();
        OnDeathEvent = new UnityEvent();
    }

    public virtual void DamageTaken(float damageValue)
    {
        _health -= damageValue;
        if (_health <= 0 && _isAlive)
        {
            Death();
            return;
        }
        OnDamageTakenEvent.Invoke(Health);
    }

    public virtual void RegainHealth(float healthValue)
    {
        if (MAX_health - _health < healthValue)
        {
            _health = MAX_health;
        } else {
            _health += healthValue;
        }
        OnRegainHealthEvent.Invoke(Health);
    }

    public virtual void RegainStamina(float staminaValue)
    {

    }

    public void StaminaUsed()
    {

    }

    public virtual void Death()
    {
        _isAlive = false;
        //GameState = fail
        //Play death animation
        //Play scene loading animation
        // Load Main Menu scene
        OnDeathEvent.Invoke();
    }

    
}

