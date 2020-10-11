using UnityEngine;
using static PlanetaryUtils;

public interface IDamageable
{
    void TakeDamage(float value, float sourceRotation);

    // Returns the position of the damageable as a degree
    float GetPlanetaryPosition();

    Team GetTeam();

    Transform GetTransform();
}