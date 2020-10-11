using UnityEngine;
using static PlanetaryUtils;

public class RangedProjectile : PlanetCharacter, IChildCollidable
{
    public float Speed = 20;
    public float YDropoff = 0.001f;
    public float AttackDamage = 20f;
    public float LifeTime = 5f;

    private float direction = 0; 
    private float timeLeft = 0;
    private float yVelocity = 0;
    private Team Team;
    public void Setup(float direction, Team team)
    {
        this.direction = direction;
        timeLeft = LifeTime; 
        yVelocity = 0.02f;
        Team = team;
    }

    public void Update()
    {
        if(timeLeft <= 0)
            Destroy(gameObject);
        else
            timeLeft -= Time.deltaTime;
        
        //yVelocity -= YDropoff*Time.deltaTime;
        //transform.GetChild(0).Translate(Vector3.back*yVelocity*Time.deltaTime);

        Move(Speed*direction);
    }

    public void Collision(Collider2D col)
    {
        var damageable = col.attachedRigidbody.transform.GetComponent<IDamageable>(); 
        if(damageable != null)
        {
            if(damageable.GetTeam() != Team)
            {
                Debug.Log(damageable.GetTransform().name);
                damageable.TakeDamage(AttackDamage, Rotation);
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log(col.transform.name);
            Destroy(gameObject);
        }
    }
}