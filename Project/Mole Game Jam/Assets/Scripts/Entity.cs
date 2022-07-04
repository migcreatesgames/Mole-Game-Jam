using UnityEngine;

public class Entity: MonoBehaviour
{
    private int _health;
    private float _stamina;
    private float _speed = 5.0f;

    protected int MAX_health = 3;
    protected float MAX_stamina = 100f;

    public int Health { get => _health; set => _health = value; }
    public float Stamina { get => _stamina; set => _stamina = value; }
    public float Speed { get => _speed; set => _speed = value; }

    public virtual void DamageTaken(int damageValue)
    {
        _health -= damageValue;
        if (_health <= 0)
        {
            Death();
            return;
        }

        //OnDamaged.Invoke();
    }

    public virtual void RegainHealth(int healthValue)
    {
        if (_health < MAX_health)
        {
            _health += healthValue;
            //OnRegainHealth.Invoke();
        }
    }

    public virtual void RegainStamina(float staminaValue)
    {

    }

    public void StaminaUsed()
    {

    }

    public virtual void Death()
    {
        //GameState = fail
        //Play death animation
        //Play scene loading animation
        // Load Main Menu scene
        // OnDeath.Invoked();
    }
}

