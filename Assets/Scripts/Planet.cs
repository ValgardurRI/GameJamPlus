using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlanetaryUtils;

public class Planet : MonoBehaviour
{
    private static Planet _instance;
    public static Planet Instance => _instance;
    public float Radius => transform.localScale.x;
    [HideInInspector]
    public Transform NatureUnits;
    [HideInInspector]
    public Transform RobotUnits;
    [HideInInspector]
    public List<ForestationUnit> ForestationUnits;

    public Sprite NatureOctantSprite;
    public Sprite RobotOctantSprite;

    private int[] octantForestationCount;
    private Transform octants;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            ForestationUnits = new List<ForestationUnit>();
            octantForestationCount = new int[8];
            NatureUnits = transform.Find("NatureUnits");
            RobotUnits = transform.Find("RobotUnits");
            octants = transform.Find("Octants");
        }
    }

    void Update()
    {
        int rotationToOctant(float rotation)
        {
            var val = Mathf.FloorToInt(rotation/90);
            Debug.Log(val);
            return val;
        }

        for(int i = 0; i < octantForestationCount.Length; i++)
        {
            octantForestationCount[i] = 0;
        }

        foreach(var natureUnit in ForestationUnits)
        {
            var value = natureUnit.Team == Team.Nature ? 1 : -1 ;
            octantForestationCount[rotationToOctant(natureUnit.Rotation)] += value;
        }

        for(int i = 0; i < octantForestationCount.Length; i++)
        {
            var sprite = octantForestationCount[i] >= 2 ? NatureOctantSprite : RobotOctantSprite;
            octants.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprite;
        }

    }
}
