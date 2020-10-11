using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlanetaryUtils;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCharacter : PlanetCharacter, IDamageable
{
    public float StartPosition = 0;
    public float MaxSpeed;
    public float Acceleration = 10;
    public float Friction = 10;
    public Team Team;
    float velocity = 0;
    [SerializeField]
    private float movementDirection = 0;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        SetPosition(StartPosition);
    }


    // Update is called once per frame
    void Update()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        if(movementDirection != 0)
        {
            // If the character registers movement, add acceleration
            velocity += Acceleration*movementDirection;
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

        velocity = Mathf.Clamp(velocity, -MaxSpeed, MaxSpeed);
        Move(velocity);
        SetSpriteDirection();

    }

    public void TakeDamage(float value, float sourceRotation)
    {
        //velocity -= value*PlanetaryUtils.PlanetaryDirection(Rotation, sourceRotation);
    }

    public Team GetTeam()
    {
        return Team;
    }
    public float GetPlanetaryPosition()
    {
        return Rotation;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    void SetSpriteDirection()
    {
        if(velocity != 0)
        {
            spriteRenderer.flipX = velocity > 0;
        }
    }

}
