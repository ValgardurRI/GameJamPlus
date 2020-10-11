using UnityEngine;

public static class PlanetaryUtils
{

    public static float SignedPlanetaryDistance(float angle1, float angle2)
    {
        var diff = angle1 - angle2;
        
        if(Mathf.Abs(diff) < Mathf.Abs(diff + 360) && Mathf.Abs(diff) < Mathf.Abs(diff - 360))
            return diff;
        else if(Mathf.Abs(diff + 360) < Mathf.Abs(diff - 360))
            return diff + 360;
        else
            return diff - 360;
    }

    public static float PlanetaryDistance(float angle1, float angle2)
    {
        return Mathf.Abs(SignedPlanetaryDistance(angle1, angle2));    
    }

    public static float PlanetaryDirection(float angle1, float angle2)
    {
        return Mathf.Sign(SignedPlanetaryDistance(angle1, angle2));
    }

    public enum Team
    {
        // Set enum values specifically to ensure conversion to bool
        Nature = 0,
        Robots = 1
    }
}