#ifndef MATRIX_HLSL
#define MATRIX_HLSL

#include "Constants.hlsl"

#define IDENTITY_MATRIX_2X2 float2x2(1, 0, 0, 1)
#define IDENTITY_MATRIX_3X3 float3x3(1, 0, 0, 0, 1, 0, 0, 0, 1)
#define IDENTITY_MATRIX_4X4 float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1)

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

// Creates a rotation matrix from an angle in degrees around the axis.
float4x4 RotationMatrix( float angle, float3 axis )
{
	float c, s;
	sincos(angle * DEG_TO_RAD, s, c);

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

#endif