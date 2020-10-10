using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetCharacter : MonoBehaviour
{
    public float Rotation => transform.rotation.z;


    // Call this every update
    public void Move(float velocity)
    {
        var rotation = Time.deltaTime*velocity;
        transform.RotateAround(transform.parent.position, Vector3.back, rotation);
    }  
}
