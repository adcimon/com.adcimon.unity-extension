using UnityEngine;

public static class MathExtension
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
    /// Calculates the normal vector of the plane defined by the points p0, p1 and p2.
    /// </summary>
    public static Vector3 Normal( Vector3 p0, Vector3 p1, Vector3 p2 )
    {
        return Vector3.Cross((p1 - p0), (p2 - p0)).normalized;
    }

    /// <summary>
    /// Calculates the area of the triangle defined by the points p0, p1 and p2.
    /// </summary>
    public static float TriangleArea( Vector3 p0, Vector3 p1, Vector3 p2 )
    {
        float a = SqrDistance(p0, p1);
        float b = SqrDistance(p1, p2);
        float c = SqrDistance(p2, p0);
        return Mathf.Sqrt((2 * a * b + 2 * b * c + 2 * c * a - a * a - b * b - c * c) / 16);
    }

    /// <summary>
    /// Calculates the point in the circumference defined by the origin, radius and angle in degrees.
    /// </summary>
    public static Vector2 PointInCircumference( Vector2 origin, float radius, float angle )
    {
        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad) + origin.x;
        float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad) + origin.y;
        return new Vector2(x, y);
    }

    /// <summary>
    /// Calculates the point in the sphere defined by the origin, radius and 2 angles for the latitude and longitude.
    /// </summary>
    public static Vector3 PointInSphere( Vector3 origin, float radius, float latitudeAngle, float longitudeAngle )
    {
        float x = radius * Mathf.Cos(latitudeAngle * Mathf.Deg2Rad) * Mathf.Sin(longitudeAngle * Mathf.Deg2Rad) + origin.x;
        float y = radius * Mathf.Sin(latitudeAngle * Mathf.Deg2Rad) * Mathf.Sin(longitudeAngle * Mathf.Deg2Rad) + origin.y;
        float z = radius * Mathf.Cos(longitudeAngle * Mathf.Deg2Rad) + origin.z;
        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Checks if the ray intersects the triangle defined by the points p0, p1 and p2.
    /// Implementation of the Möller–Trumbore intersection algorithm.
    /// Reference: https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
    /// </summary>
    public static bool RayIntersectsTriangle( Ray ray, Vector3 p0, Vector3 p1, Vector3 p2, out float distance, out Vector3 point )
    {
        distance = 0;
        point = Vector3.zero;

        Vector3 v01 = p1 - p0;
        Vector3 v02 = p2 - p0;

        Vector3 h = Vector3.Cross(ray.direction, v02);
        float a = Vector3.Dot(v01, h);
        if( a > -Mathf.Epsilon && a < Mathf.Epsilon )
        {
            return false;
        }

        float f = 1f / a;
        Vector3 s = ray.origin - p0;
        float u = f * Vector3.Dot(s, h);
        if( u < 0 || u > 1 )
        {
            return false;
        }

        Vector3 q = Vector3.Cross(s, v01);
        float v = f * Vector3.Dot(ray.direction, q);
        if( v < 0 || u + v > 1 )
        {
            return false;
        }

        // At this stage we can compute t to find out where the intersection point is on the line.
        float t = f * Vector3.Dot(v02, q);
        if( t > Mathf.Epsilon )
        {
            distance = t;
            point.x = u * p1.x + v * p2.x + (1 - (u + v)) * p0.x;
            point.y = u * p1.y + v * p2.y + (1 - (u + v)) * p0.y;
            point.z = u * p1.z + v * p2.z + (1 - (u + v)) * p0.z;
            return true;
        }

        // This means that there is a line intersection but not a ray intersection.
        return false;
    }
}