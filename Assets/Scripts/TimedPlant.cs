using System.Collections.Generic;
using UnityEngine;
using static PlanetaryUtils;

public class TimedPlant : PlanetCharacter
{
    public float SproutTime;
    public PlanetCharacter SpawnPrefab;
    public List<Sprite> SproutStages;
    public Team team;
    
    private float timeRemaining;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        timeRemaining = SproutTime;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0)
        {
            //Instantiate(SpawnPrefab, );
            Destroy(gameObject);
        }
    }


}