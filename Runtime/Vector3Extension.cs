using UnityEngine;

public static class Vector3Extension
{
    /// <summary>
    /// Calculates the squared distance between a and b.
    /// </summary>
    public static float SqrDistance( Vector3 a, Vector3 b )
    {
        float x = b.x - a.x;
        float y = b.y - a.y;
        float z = b.z - a.z;
        return x * x + y * y + z * z;
    }

    /// <summary>
    /// Calculates the reciprocal of the vector.
    /// </summary>
    public static Vector3 Reciprocal( Vector3 v )
    {
        return new Vector3(1.0f / v.x, 1.0f / v.y, 1.0f / v.z);
    }
}