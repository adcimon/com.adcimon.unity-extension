#ifndef RANDOM_HLSL
#define RANDOM_HLSL

// Hash function.
// Reference: H. Schechter & R. Bridson (goo.gl/RXiKaH).
uint Hash( uint s )
{
	s ^= 2747636419u;
	s *= 2654435769u;
	s ^= s >> 16;
	s *= 2654435769u;
	s ^= s >> 16;
	s *= 2654435769u;
	return s;
}

// Returns a random number.
float Random( uint seed )
{
	return float(Hash(seed)) / 4294967295.0; // 2^32-1
}

// Uniformaly distributed points on a unit sphere
// Reference: http://mathworld.wolfram.com/SpherePointPicking.html
float3 RandomUnitVector( uint seed )
{
	float PI2 = 6.28318530718;
	float z = 1 - 2 * Random(seed);
	float xy = sqrt(1.0 - z * z);
	float sn, cs;
	sincos(PI2 * Random(seed + 1), sn, cs);
	return float3(sn * xy, cs * xy, z);
}

// Uniformaly distributed points inside a unit sphere.
float3 RandomVector( uint seed )
{
	return RandomUnitVector(seed) * sqrt(Random(seed + 2));
}

// Uniformaly distributed points inside a unit cube.
float3 RandomVector01( uint seed )
{
	return float3(Random(seed), Random(seed + 1), Random(seed + 2));
}

#endif