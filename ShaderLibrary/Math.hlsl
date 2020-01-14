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

// Converts degrees to radians.
float DegToRad( float degrees )
{
	return degrees * (PI / 180.0);
}

// Converts radians to degrees.
float RadToDeg( float radians )
{
	return radians * (180.0 / PI);
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

// Creates a translation matrix.
float4x4 TranslationMatrix( float3 translation )
{
	return float4x4(
		1, 0, 0, translation.x,
		0, 1, 0, translation.y,
		0, 0, 1, translation.z,
		0, 0, 0, 1
		);
}

// Creates a rotation matrix.
float4x4 RotationMatrix( float angle, float3 axis )
{
	float c, s;
	sincos(angle, s, c);

	float t = 1 - c;
	float x = axis.x;
	float y = axis.y;
	float z = axis.z;

	return float4x4(
		t*x*x + c,		t*x*y - s*z,	t*x*z + s*y,	0,
		t*x*y + s*z,	t*y*y + c,		t*y*z - s*x,	0,
		t*x*z - s*y,	t*y*z + s*x,	t*z*z + c,		0,
		0,				0,				0,				1
		);
}

// Creates a scaling matrix.
float4x4 ScalingMatrix( float3 scale )
{
	return float4x4(
		scale.x,	0,			0,			0,
		0,			scale.y,	0,			0,
		0,			0,			scale.z,	0,
		0,			0,			0,			1
		);
}

// Calculates the distance from the plane to the point along the normal.
// Returns: Distance, positive is front, negative is back.
float DistanceFromPlane( float3 p, float3 planePosition, float3 planeNormal )
{
	float NdotP = dot(planeNormal, planePosition);
	return dot(float4(p, 1), float4(planeNormal, -NdotP));
}

// Projets a point on a plane.
float3 ProjectPointOnPlane( float3 p, float3 planePosition, float3 planeNormal )
{
	return p - (dot(p - planePosition, planeNormal) * planeNormal);
}

// Calculates the ray to plane intersection.
// Returns: Whether the ray intersected the plane.
bool IntersectRayPlane( float3 rayOrigin, float3 rayDirection, float3 planeOrigin, float3 planeNormal, out float intersectionLength, out float3 intersectionPoint )
{
	static const float epsilon = 0.0000001;

	float denominator = dot(rayDirection, planeNormal);

	// Check if the ray is parallel to the plane or pointing away from the plane.
	if( denominator < epsilon )
	{
		return false;
	}

	intersectionLength = dot(planeNormal, (planeOrigin - rayOrigin)) / denominator;
	intersectionPoint = rayOrigin + rayDirection * intersectionLength;

	return true;
}

// Calculates the ray to triangle intersection.
// Returns: Whether the ray intersected the triangle.
// Reference: Möller–Trumbore intersection algorithm (https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm).
bool IntersectRayTriangle( float3 rayOrigin, float3 rayDirection, float3 p0, float3 p1, float3 p2, out float intersectionLength, out float3 intersectionPoint )
{
	static const float epsilon = 0.0000001;

	intersectionLength = 0;
	intersectionPoint = float3(0, 0, 0);

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
		intersectionLength = t;
		intersectionPoint = rayOrigin + rayDirection * t;
		return true;
	}

	// This means that there is a line intersection but not a ray intersection.
	return false;
}

// Calculates the ray to AABB intersections.
// Returns: Whether the ray intersected the AABB.
bool IntersectRayAABB( float3 rayOrigin, float3 rayDirection, float3 boxMin, float3 boxMax, float tMin, float tMax, out float tEntr, out float3 pointEntr, out float tExit, out float3 pointExit )
{
	float3 rayDirectionInverse = rcp(rayDirection);

	// Perform ray-slab intersection (component-wise).
	float3 t0 = boxMin * rayDirectionInverse - (rayOrigin * rayDirectionInverse);
	float3 t1 = boxMax * rayDirectionInverse - (rayOrigin * rayDirectionInverse);

	// Find the closest/farthest distance (component-wise).
	float3 tSlabEntr = min(t0, t1);
	float3 tSlabExit = max(t0, t1);

	// Find the farthest entry and the nearest exit.
	tEntr = Max3(tSlabEntr.x, tSlabEntr.y, tSlabEntr.z);
	tExit = Min3(tSlabExit.x, tSlabExit.y, tSlabExit.z);

	// Clamp to the range.
	tEntr = max(tEntr, tMin);
	tExit = min(tExit, tMax);

	// Calculate the points.
	pointEntr = rayOrigin + rayDirection * tEntr;
	pointExit = rayOrigin + rayDirection * tExit;

	return (tEntr < tExit);
}

#endif