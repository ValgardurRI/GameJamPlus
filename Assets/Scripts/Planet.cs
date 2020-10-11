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

    private int[] octantForestationCounts;
    private Transform octants;
    private string loseString= "Memories begin to fade";
    private string winString = "The cycle of life and death continues";
    private bool ended = false;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            ForestationUnits = new List<ForestationUnit>();
            octantForestationCounts = new int[8];
            NatureUnits = transform.Find("NatureUnits");
            RobotUnits = transform.Find("RobotUnits");
            octants = transform.Find("Octants");
        }
    }

    void EndgameCheck()
    {
        if(!ended)
        {
            int natureOccupiedOctants = 0;
            foreach(var count in octantForestationCounts)
            if(count >= 2)
                natureOccupiedOctants++;

            
            if(natureOccupiedOctants >= 4)
            {
                ended = true;
                SceneLoader.Instance.ReloadScene(winString);
            }
            else if(natureOccupiedOctants == 0)
            {
                ended = true;
                SceneLoader.Instance.ReloadScene(loseString);
            }
        }
    }

    void Update()
    {
        int rotationToOctant(float rotation)
        {
            var val = Mathf.FloorToInt((rotation + 22.5f)/45) % 8;
            return val;
        }

        for(int i = 0; i < octantForestationCounts.Length; i++)
        {
            octantForestationCounts[i] = 0;
        }

        foreach(var natureUnit in ForestationUnits)
        {
            var value = natureUnit.Team == Team.Nature ? 1 : -1 ;
            octantForestationCounts[rotationToOctant(natureUnit.Rotation)] += value;
        }

        for(int i = 0; i < octantForestationCounts.Length; i++)
        {
            var sprite = octantForestationCounts[i] >= 2 ? NatureOctantSprite : RobotOctantSprite;
            octants.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprite;
        }
        EndgameCheck();
    }
}
