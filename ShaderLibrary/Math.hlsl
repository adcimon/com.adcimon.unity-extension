#ifndef MATH_HLSL
#define MATH_HLSL

#include "Constants.hlsl"

// Selects the lesser of x, y and z.
float Min3( float x, float y, float z )
{
	return min(min(x, y), z);
}

// Selects the greater of x, y and z.
float Max3( float x, float y, float z )
{
	return max(max(x, y), z);
}

// Composes a floating point value with the magnitude of x and the sign of s.
float CopySign( float x, float s )
{
	return (s >= 0) ? abs(x) : -abs(x);
}

// Solves the quadratic equation of the form: a*t^2 + b*t + c = 0.
// Returns: False if there are no real roots, true otherwise.
// Reference: Numerical Recipes in C++ (3rd Edition).
bool SolveQuadraticEquation( float a, float b, float c, out float2 roots )
{
	float d = b * b - 4 * a * c;
	float q = -0.5 * (b + CopySign(sqrt(d), b));
	roots = float2(q / a, c / q);

	return (d >= 0);
}

// Calculates the squared distance between a and b.
float SqrDistance( float3 a, float3 b )
{
	float x = b.x - a.x;
	float y = b.y - a.y;
	float z = b.z - a.z;
	return x * x + y * y + z * z;
}

// Calculates the normal vector of the plane defined by the points p0, p1 and p2.
float3 Normal( float3 p0, float3 p1, float3 p2 )
{
	return normalize(cross((p1 - p0), (p2 - p0)));
}

// Calculates the area of the triangle defined by the points p0, p1 and p2.
float TriangleArea( float3 p0, float3 p1, float3 p2 )
{
	float a = SqrDistance(p0, p1);
	float b = SqrDistance(p1, p2);
	float c = SqrDistance(p2, p0);
	return sqrt((2*a*b + 2*b*c + 2*c*a - a*a - b*b - c*c) / 16);
}

// Calculates the distance from the plane to the point along the normal.
// Returns: Distance, positive is front, negative is back.
float DistanceFromPlane( float3 p, float3 planePosition, float3 planeNormal )
{
	return dot(p - planePosition, planeNormal);
}

// Projects a point on a plane.
float3 ProjectPointOnPlane( float3 p, float3 planePosition, float3 planeNormal )
{
	return p - (dot(p - planePosition, planeNormal) * planeNormal);
}

// Calculates the point in the circumference defined by the origin, radius and angle in degrees.
float2 PointInCircumference( float2 origin, float radius, float angle )
{
	float x = radius * cos(angle * DEG_TO_RAD) + origin.x;
	float y = radius * sin(angle * DEG_TO_RAD) + origin.y;
	return float2(x, y);
}

// Calculates the point in the sphere defined by the origin, radius and 2 angles for the latitude and longitude in degrees.
float3 PointInSphere( float3 origin, float radius, float latitudeAngle, float longitudeAngle )
{
	float x = radius * cos(latitudeAngle * DEG_TO_RAD) * sin(longitudeAngle * DEG_TO_RAD) + origin.x;
	float y = radius * sin(latitudeAngle * DEG_TO_RAD) * sin(longitudeAngle * DEG_TO_RAD) + origin.y;
	float z = radius * cos(longitudeAngle * DEG_TO_RAD) + origin.z;
	return float3(x, y, z);
}

// Calculates the ray to plane intersection.
// Returns: Whether the ray intersected the plane.
bool IntersectRayPlane( float3 rayOrigin, float3 rayDirection, float3 planeOrigin, float3 planeNormal, out float intersectionDistance, out float3 intersectionPoint )
{
	static const float epsilon = 0.0000001;

	float denominator = dot(rayDirection, planeNormal);

	// Check if the ray is parallel to the plane or pointing away from the plane.
	if( denominator < epsilon )
	{
		return false;
	}

	intersectionDistance = dot(planeNormal, (planeOrigin - rayOrigin)) / denominator;
	intersectionPoint = rayOrigin + rayDirection * intersectionDistance;

	return true;
}

// Calculates the ray to triangle intersection.
// Returns: Whether the ray intersected the triangle.
// Reference: Möller–Trumbore intersection algorithm (https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm).
bool IntersectRayTriangle( float3 rayOrigin, float3 rayDirection, float3 p0, float3 p1, float3 p2, out float intersectionDistance, out float3 intersectionPoint )
{
	static const float epsilon = 0.0000001;

	float3 v01 = p1 - p0;
	float3 v02 = p2 - p0;

	float3 h = cross(rayDirection, v02);
	float a = dot(v01, h);
	if( a > -epsilon && a < epsilon )
	{
		return false;
	}

	float f = 1.0 / a;
	float3 s = rayOrigin - p0;
	float u = f * dot(s, h);
	if( u < 0 || u > 1 )
	{
		return false;
	}

	float3 q = cross(s, v01);
	float v = f * dot(rayDirection, q);
	if( v < 0 || (u + v) > 1 )
	{
		return false;
	}

	// At this stage we can compute t to find out where the intersection point is on the line.
	float t = f * dot(v02, q);
	if( t > epsilon )
	{
		intersectionDistance = t;
		intersectionPoint = rayOrigin + rayDirection * intersectionDistance;
		return true;
	}

	// This means that there is a line intersection but not a ray intersection.
	return false;
}

// Calculates the ray to AABB intersections.
// Returns: Whether the ray intersected the AABB.
bool IntersectRayAABB( float3 rayOrigin, float3 rayDirection, float3 boxMin, float3 boxMax, out float tEntry, out float3 pointEntry, out float tExit, out float3 pointExit )
{
	float3 rayDirectionInverse = rcp(rayDirection);

	// Perform ray-slab intersection (component-wise).
	float3 t0 = boxMin * rayDirectionInverse - (rayOrigin * rayDirectionInverse);
	float3 t1 = boxMax * rayDirectionInverse - (rayOrigin * rayDirectionInverse);

	// Find the closest/farthest distance (component-wise).
	float3 tSlabEntry = min(t0, t1);
	float3 tSlabExit = max(t0, t1);

	// Find the farthest entry and the nearest exit.
	tEntry = Max3(tSlabEntry.x, tSlabEntry.y, tSlabEntry.z);
	tExit = Min3(tSlabExit.x, tSlabExit.y, tSlabExit.z);

	// Calculate the points.
	pointEntry = rayOrigin + rayDirection * tEntry;
	pointExit = rayOrigin + rayDirection * tExit;

	return (tEntry < tExit);
}

#endif