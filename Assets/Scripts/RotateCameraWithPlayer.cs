using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraWithPlayer : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        var tempAngles = transform.eulerAngles;
        tempAngles.z = player.transform.localEulerAngles.y;
        transform.eulerAngles = tempAngles;
    }
}
