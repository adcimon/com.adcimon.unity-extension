#ifndef COLOR_HLSL
#define COLOR_HLSL

// Hue value to RGB.
half3 HueToRGB( half h )
{
	h = frac(h) * 6 - 2;
	half3 rgb = saturate(half3(abs(h - 1) - 1, 2 - abs(h), 2 - abs(h - 2)));
	return rgb;
}

#endif