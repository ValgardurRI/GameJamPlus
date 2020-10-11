using UnityEngine;
using static PlanetaryUtils;

public class ForestationUnit : PlanetCharacter, IDamageable
{
    public Team Team;
    public float MaxHealth = 300f;
    private float health;
    void Start()
    {
        health = MaxHealth;
        Planet.Instance.ForestationUnits.Add(this);
    }

    void OnDestroy()
    {
        Planet.Instance.ForestationUnits.Remove(this);
    }

    public float GetPlanetaryPosition()
    {
        return Rotation;
    }

    public PlanetaryUtils.Team GetTeam()
    {
        return Team;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void TakeDamage(float value, float sourceRotation)
    {
        health -= value;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}