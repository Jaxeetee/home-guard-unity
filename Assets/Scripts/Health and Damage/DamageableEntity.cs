using UnityEngine;

public class DamageableEntity : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float entityHealth = 10;

    [SerializeField]
    private float _maxHealth = 999;
    private float _health;
    public float health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, _maxHealth);
    }

    protected virtual void Awake()
    {
        _health = entityHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{this.name} health: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("death");
    }
}