using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlanetCharacter : MonoBehaviour
{
    public float Speed = 100;
    public float Rotation => transform.rotation.z;
    private float velocity = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(Vector3.back*Speed*Time.deltaTime*velocity);
    }

    public void OnMove(InputValue value)
    {
        // Read value from control. The type depends on what type of controls.
        // the action is bound to.
        velocity = value.Get<Vector2>().x;

        // IMPORTANT: The given InputValue is only valid for the duration of the callback.
        //            Storing the InputValue references somewhere and calling Get<T>()
    }
       
}
