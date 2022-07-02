using UnityEngine;

public class Entity: MonoBehaviour
{
    private int _health;
    private float _stamina;

    public int Health { get => _health; set => _health = value; }
    public float Stamina { get => _stamina; set => _stamina = value; }
}