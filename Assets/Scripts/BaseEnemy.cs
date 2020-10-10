using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseEnemy : PlanetCharacter, IDamageable
{
    public float AttackRange;
    public float Speed;
    public float MaxHealth = 100;
    protected IDamageable target;
    protected float currentHealth;
    protected float velocity = 0;
    
    // When using Unity Events, call base function before
    public virtual void Start()
    {
        currentHealth = MaxHealth;
        target = GetClosestTarget();
    }

    public float GetPlanetaryPosition()
    {
        return Rotation;
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected bool InAttackRange()
    {
        return Mathf.Abs(Rotation - target.GetPlanetaryPosition()) < AttackRange;
    }



    protected abstract void Attack();
    protected abstract IDamageable GetClosestTarget();
}