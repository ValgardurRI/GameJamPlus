using UnityEngine;
using System.Linq;
using static PlanetaryUtils;
using System;

public class RangedUnit : BaseUnit
{
    public RangedProjectile projectilePrefab;
    public override void Update()
    {
        base.Update();

        if(target != null)
        {
            velocity = TargetDirection()*Speed;
            if(InAttackRange())
                Attack();
            else
                Move(velocity);
        }
    }

    protected override void Attack()
    {
        if(attackCooldown == 0)
        {
            var projectile = Instantiate(projectilePrefab, Planet.Instance.transform);
            projectile.SetPosition(Rotation);
            projectile.Setup(TargetDirection(), Team);
            attackCooldown = SecondsPerAttack;
        }
    }
}