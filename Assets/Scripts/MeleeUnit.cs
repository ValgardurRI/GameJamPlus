using UnityEngine;

public class MeleeUnit : BaseUnit
{
    public override void Update()
    {
        base.Update();

        if(this != null)
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
            target.TakeDamage(AttackDamage, Rotation);
            attackCooldown = SecondsPerAttack;
        }
    }
}