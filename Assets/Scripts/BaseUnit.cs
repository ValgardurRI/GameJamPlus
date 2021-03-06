using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlanetaryUtils;

public abstract class BaseUnit : PlanetCharacter, IDamageable
{
    public GameObject ExplosionVFX;

    public float AttackRange;
    public float Speed;
    public float MaxHealth = 100;
    public float AttackDamage = 10;
    public float SecondsPerAttack = 10;
    public float Acceleration = 5;
    public float Friction = 5;
    public float KnockbackWeight = 1;
    public Team Team;
    public float InitialPosition = 0f;

    public Transform targetTransform;
    protected IDamageable target;
    protected float currentHealth;
    protected float velocity = 0;
    protected float attackCooldown = 0f;
    protected float targetCheckTimer = 0f;
    protected Animator animator;
    
    // When using Unity Events, call base function before
    public virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = MaxHealth;
        target = GetClosestTarget();
        if (InitialPosition != 0f)
            SetPosition(InitialPosition);
        if((target = GetClosestTarget()) != null)
            targetTransform = target.GetTransform();
    }

    public virtual void Update()
    {
        attackCooldown -= Time.deltaTime;
        attackCooldown = Mathf.Clamp(attackCooldown, 0, SecondsPerAttack);
        
        targetCheckTimer -= Time.deltaTime;
        if(target == null || targetCheckTimer <= 0)
        {
            if((target = GetClosestTarget()) != null)
                targetTransform = target?.GetTransform();
            targetCheckTimer = 0.8f;
        }

        
        if(this != null)
        {
            var direction = TargetDirection();
            if(InAttackRange())
                Attack();
            else
            {
                if(direction != 0)
                {
                    // If the character registers movement, add acceleration
                    velocity += Acceleration*direction;
                }
                else
                {
                    // Otherwise start slowing the character
                    if(Mathf.Abs(velocity) < Friction)
                    {
                        velocity = 0;
                    }
                    else
                    {
                        velocity += velocity < 0 ? Friction : -Friction;
                    }
                }

                velocity = Mathf.Clamp(velocity, -Speed, Speed);
                Move(velocity);
            }
        }
    }

    public float GetPlanetaryPosition()
    {
        return Rotation;
    }

    public void TakeDamage(float value, float sourceRotation)
    {
        currentHealth -= value;
        velocity -= value*10*PlanetaryUtils.PlanetaryDirection(Rotation, sourceRotation)/KnockbackWeight;
        if (currentHealth <= 0 && this != null)
        {
            Instantiate(ExplosionVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public Team GetTeam()
    {
        return Team;
    }
    public Transform GetTransform()
    {
        return transform;
    }
    protected bool InAttackRange()
    {
        if(target == null)
            return false;

        return PlanetaryDistance(Rotation, target.GetPlanetaryPosition()) < AttackRange;
    }

    protected float TargetDirection()
    {
        if(target == null)
            return 0;

        return PlanetaryDirection(Rotation, target.GetPlanetaryPosition());
    }

    protected virtual void Attack()
    {
        animator.SetTrigger("AttackTrigger");
    }

    protected IDamageable GetClosestTarget(Func<IDamageable, bool> filter = null)
    {
        Transform unitTransform = Team == Team.Nature ? Planet.Instance.RobotUnits : Planet.Instance.NatureUnits;
        float minDistance = float.MaxValue;
        IDamageable minObject = null;
        foreach(var unit in unitTransform.GetComponentsInChildren<IDamageable>())
        {
            if(filter != null && !filter(unit))
                continue;

            float distance = PlanetaryDistance(Rotation, unit.GetPlanetaryPosition()); 
            if(distance < minDistance)
            {
                minDistance = distance;
                minObject = unit;
            }
        }
        return minObject;
    }
}