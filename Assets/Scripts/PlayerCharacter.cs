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
    // Start is called before the first frame update
    void Start()
    {
        SetPosition(StartPosition);
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
}