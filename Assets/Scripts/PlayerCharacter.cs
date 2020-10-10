using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCharacter : PlanetCharacter
{
    float velocity = 0;
    private float movementDirection = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        Move(movementDirection*MaxSpeed);
    }

    public void OnMove(InputValue value)
    {
        // Read value from control. The type depends on what type of controls.
        // the action is bound to.
        movementDirection = value.Get<Vector2>().x;
    }
       
}