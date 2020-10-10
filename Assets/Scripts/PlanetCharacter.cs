using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCharacter : MonoBehaviour
{
    public float Rotation => transform.localEulerAngles.y % 360;


    // Call this every update
    public void Move(float velocity)
    {
        var rotation = Time.deltaTime*velocity;
        transform.RotateAround(transform.parent.position, Vector3.back, rotation);
    }

    public void SetPosition(float angle)
    {
        var tempRotation = transform.eulerAngles;
        tempRotation.y = 0;
        transform.eulerAngles = tempRotation;
        transform.RotateAround(transform.parent.position, Vector3.back, angle);
    }
}
