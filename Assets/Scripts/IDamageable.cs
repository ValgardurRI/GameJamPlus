using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float value);

    // Returns the position of the damageable as a degree
    float GetPlanetaryPosition();
}