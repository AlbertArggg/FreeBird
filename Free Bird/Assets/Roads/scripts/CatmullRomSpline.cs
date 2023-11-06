using UnityEngine;

public static class CatmullRomSpline
{
    public static Vector3 GetPosition(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        return 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
    }
    
    public static Vector3 GetTangent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = -0.5f * p0 + 1.5f * p1 - 1.5f * p2 + 0.5f * p3;
        Vector3 b = p0 - 2.5f * p1 + 2f * p2 - 0.5f * p3;
        Vector3 c = -0.5f * p0 + 0.5f * p2;
        return 2f * a * t + b + c;
    }
}