using UnityEngine;

public class TestEnemy : BaseEnemy
{
    
    void Update()
    {
        velocity = Mathf.Sign(Rotation - target.GetPlanetaryPosition())*Speed;
        Move(velocity);
    }

    protected override void Attack()
    {
        
    }

    protected override IDamageable GetClosestTarget()
    {
        return transform.parent.GetComponentInChildren<PlayerCharacter>();
    }
}