using UnityEngine;

public class MeleeUnit : BaseUnit
{
    public override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        if(attackCooldown == 0)
        {
            base.Attack();
            target.TakeDamage(AttackDamage, Rotation);
            attackCooldown = SecondsPerAttack;
        }
    }
}