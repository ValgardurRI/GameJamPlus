using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCharacter : PlanetCharacter, IDamageable
{
    public float StartPosition = 0;
    public float MaxSpeed;
    public float Acceleration = 10;
    public float Friction = 10;
    float velocity = 0;
    private float movementDirection = 0;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
=======
        SetPosition(StartPosition);
>>>>>>> ba3f8c259c594f4d6b38a813ba322f53d21d2493
    }


    // Update is called once per frame
    void Update()
    {
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

    public void OnMove(InputValue value)
    {
        // Read value from control. The type depends on what type of controls.
        // the action is bound to.
        movementDirection = value.Get<Vector2>().x;

    }

    public void TakeDamage(float value)
    {
        
    }

    public float GetPlanetaryPosition()
    {
        return Rotation;
    }

    void SetSpriteDirection()
    {
        if (velocity > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (velocity < 0)
        {
            spriteRenderer.flipX = true;

        }
    }
}
