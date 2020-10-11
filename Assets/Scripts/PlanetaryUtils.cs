using UnityEngine;

public static class PlanetaryUtils
{
    public static float SignedPlanetaryDistance(float alpha, float beta) {
        var distance = PlanetaryDistance(alpha, beta);

        float sign = (alpha - beta >= 0 && alpha - beta <= 180) || (alpha - beta <=-180 && alpha - beta >= -360) ? 1 : -1; 
        distance *= sign;
        return distance;
    }

    public static float PlanetaryDistance(float alpha, float beta)
    {
        float phi = Mathf.Abs(beta - alpha) % 360;       // This is either the distance or 360 - distance
        float distance = phi > 180 ? 360 - phi : phi;

        return distance;
    }

    public static float PlanetaryDirection(float alpha, float beta)
    {
        return Mathf.Sign(SignedPlanetaryDistance(alpha, beta));
    }

    public enum Team
    {
        // Set enum values specifically to ensure conversion to bool
        Nature = 0,
        Robots = 1
    }
}