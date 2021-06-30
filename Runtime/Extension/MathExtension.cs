using UnityEngine;

public static class MathExtension
{
    /// <summary>
    /// Composes a floating point value with the magnitude of x and the sign of s.
    /// </summary>
    public static float CopySign( float x, float s )
    {
        return (s >= 0) ? Mathf.Abs(x) : -Mathf.Abs(x);
    }

    /// <summary>
    /// Solves the quadratic equation of the form: a*t^2 + b*t + c = 0.
    /// </summary>
    public static bool SolveQuadraticEquation( float a, float b, float c, out Vector2 roots )
    {
        float d = b * b - 4 * a * c;
        float q = -0.5f * (b + CopySign(Mathf.Sqrt(d), b));
        roots = new Vector2(q / a, c / q);

        return (d >= 0);
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
        float a = Vector3Extension.SqrDistance(p0, p1);
        float b = Vector3Extension.SqrDistance(p1, p2);
        float c = Vector3Extension.SqrDistance(p2, p0);
        return Mathf.Sqrt((2*a*b + 2*b*c + 2*c*a - a*a - b*b - c*c) / 16);
    }

    /// <summary>
    /// Calculates the distance from the plane to the point along the normal.
    /// </summary>
    public static float DistanceFromPlane( Vector3 p, Vector3 planePosition, Vector3 planeNormal )
    {
        return Vector3.Dot(p - planePosition, planeNormal);
    }

    /// <summary>
    /// Projects a point on a plane.
    /// </summary>
    public static Vector3 ProjectPointOnPlane( Vector3 p, Vector3 planePosition, Vector3 planeNormal )
    {
        return p - (Vector3.Dot(p - planePosition, planeNormal) * planeNormal);
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
    /// Calculates the point in the sphere defined by the origin, radius and 2 angles for the latitude and longitude in degrees.
    /// </summary>
    public static Vector3 PointInSphere( Vector3 origin, float radius, float latitudeAngle, float longitudeAngle )
    {
        float x = radius * Mathf.Cos(latitudeAngle * Mathf.Deg2Rad) * Mathf.Sin(longitudeAngle * Mathf.Deg2Rad) + origin.x;
        float y = radius * Mathf.Sin(latitudeAngle * Mathf.Deg2Rad) * Mathf.Sin(longitudeAngle * Mathf.Deg2Rad) + origin.y;
        float z = radius * Mathf.Cos(longitudeAngle * Mathf.Deg2Rad) + origin.z;
        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Calculates the ray to plane intersection.
    /// </summary>
    public static bool IntersectRayPlane( Ray ray, Vector3 planeOrigin, Vector3 planeNormal, out float intersectionDistance, out Vector3 intersectionPoint )
    {
        const float epsilon = 0.0000001f;

        intersectionDistance = 0;
        intersectionPoint = Vector3.zero;

        float denominator = Vector3.Dot(ray.direction, planeNormal);

        // Check if the ray is parallel to the plane or pointing away from the plane.
        if( denominator < epsilon )
        {
            return false;
        }

        intersectionDistance = Vector3.Dot(planeNormal, (planeOrigin - ray.origin)) / denominator;
        intersectionPoint = ray.origin + ray.direction * intersectionDistance;

        return true;
    }

    /// <summary>
    /// Calculates the ray to triangle intersection.
    /// Reference: Möller–Trumbore intersection algorithm (https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm).
    /// </summary>
    public static bool IntersectRayTriangle( Ray ray, Vector3 p0, Vector3 p1, Vector3 p2, out float intersectionDistance, out Vector3 intersectionPoint )
    {
        const float epsilon = 0.0000001f;

        intersectionDistance = 0;
        intersectionPoint = Vector3.zero;

        Vector3 v01 = p1 - p0;
        Vector3 v02 = p2 - p0;

        Vector3 h = Vector3.Cross(ray.direction, v02);
        float a = Vector3.Dot(v01, h);
        if( a > -epsilon && a < epsilon )
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
        if( t > epsilon )
        {
            intersectionDistance = t;
            intersectionPoint = ray.origin + ray.direction * t;
            return true;
        }

        // This means that there is a line intersection but not a ray intersection.
        return false;
    }

    /// <summary>
    /// Calculates the ray to AABB intersections.
    /// </summary>
    public static bool IntersectRayAABB( Ray ray, Bounds bounds, out float tEntry, out Vector3 pointEntry, out float tExit, out Vector3 pointExit )
    {
        Vector3 rayDirectionInverse = Vector3Extension.Reciprocal(ray.direction);

        // Perform ray-slab intersection (component-wise).
        Vector3 t0 = Vector3.Scale(bounds.min, rayDirectionInverse) - Vector3.Scale(ray.origin, rayDirectionInverse);
        Vector3 t1 = Vector3.Scale(bounds.max, rayDirectionInverse) - Vector3.Scale(ray.origin, rayDirectionInverse);

        // Find the closest/farthest distance (component-wise).
        Vector3 tSlabEntry = Vector3.Min(t0, t1);
        Vector3 tSlabExit = Vector3.Max(t0, t1);

        // Find the farthest entry and the nearest exit.
        tEntry = Mathf.Max(tSlabEntry.x, tSlabEntry.y, tSlabEntry.z);
        tExit = Mathf.Min(tSlabExit.x, tSlabExit.y, tSlabExit.z);

        // Calculate the points.
        pointEntry = ray.origin + ray.direction * tEntry;
        pointExit = ray.origin + ray.direction * tExit;

        return (tEntry < tExit);
    }
}