using System.Collections.Generic;
using UnityEngine;
using static PlanetaryUtils;

public class TimedPlant : PlanetCharacter, IDamageable
{
    public float SproutTime;
    public PlanetCharacter SpawnPrefab;
    public Team team;
    public float MaxHealth;

    private float timeRemaining;
    private float health; 

    public float GetPlanetaryPosition()
    {
        return Rotation;
    }

    public Team GetTeam()
    {
        return team;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void TakeDamage(float value, float sourceRotation)
    {
        health -= value;
        if(health <= 0 && this != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        health = MaxHealth;
        timeRemaining = SproutTime;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0)
        {
            var parent = team == Team.Nature ? Planet.Instance.NatureUnits : Planet.Instance.RobotUnits;
            var newPrefab = Instantiate(SpawnPrefab, parent);
            newPrefab.SetPosition(Rotation);
            Destroy(gameObject);
        }
    }


}