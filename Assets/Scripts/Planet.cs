using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private static Planet _instance;
    public static Planet Instance => _instance;
    public float Radius => transform.localScale.x;
    [HideInInspector]
    public Transform NatureUnits;
    [HideInInspector]
    public Transform RobotUnits;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            NatureUnits = transform.Find("NatureUnits");
            RobotUnits = transform.Find("RobotUnits");
        }
    }
}
