#ifndef EASING_FUNCTIONS_HLSL
#define EASING_FUNCTIONS_HLSL

#include "Constants.hlsl"

// Easing equation function.
// t: Current time.
// b: Starting value.
// c: Final value.
// d: Duration.
// Returns: Animated value.

// Simple linear with no easing.
float EaseLinear( float t, float b, float c, float d )
{
    return c * t / d + b;
}

// Quadratic t^2 easing.
// Accelerating from zero velocity.
float EaseQuadraticIn( float t, float b, float c, float d )
{
    return c * (t /= d) * t + b;
}

// Quadratic t^2 easing.
// Decelerating from zero velocity.
float EaseQuadraticOut( float t, float b, float c, float d )
{
    return -c * (t /= d) * (t - 2) + b;
}

// Quadratic t^2 easing.
// Acceleration until halfway, then deceleration.
float EaseQuadraticInOut( float t, float b, float c, float d )
{
    if( (t /= d / 2) < 1 )
    {
        return c / 2 * t * t + b;
    }

    return -c / 2 * ((--t) * (t - 2) - 1) + b;
}

// Quadratic t^2 easing.
// Deceleration until halfway, then acceleration.
float EaseQuadraticOutIn( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseQuadraticOut(t * 2, b, c / 2, d);
    }

    return EaseQuadraticIn((t * 2) - d, b + c / 2, c / 2, d);
}

// Cubic t^3 easing.
// Accelerating from zero velocity.
float EaseCubicIn( float t, float b, float c, float d )
{
    return c * (t /= d) * t * t + b;
}

// Cubic t^3 easing.
// Decelerating from zero velocity.
float EaseCubicOut( float t, float b, float c, float d )
{
    return c * ((t = t / d - 1) * t * t + 1) + b;
}

// Cubic t^3 easing.
// Acceleration until halfway, then deceleration.
float EaseCubicInOut( float t, float b, float c, float d )
{
    if( (t /= d / 2) < 1 )
    {
        return c / 2 * t * t * t + b;
    }

    return c / 2 * ((t -= 2) * t * t + 2) + b;
}

// Cubic t^3 easing.
// Deceleration until halfway, then acceleration.
float OutIn( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseCubicOut(t * 2, b, c / 2, d);
    }

    return EaseCubicIn((t * 2) - d, b + c / 2, c / 2, d);
}

// Quartic t^4 easing.
// Accelerating from zero velocity.
float EaseQuarticIn( float t, float b, float c, float d )
{
    return c * (t /= d) * t * t * t + b;
}

// Quartic t^4 easing.
// Decelerating from zero velocity.
float EaseQuarticOut( float t, float b, float c, float d )
{
    return -c * ((t = t / d - 1) * t * t * t - 1) + b;
}

// Quartic t^4 easing.
// Acceleration until halfway, then deceleration.
float EaseQuarticInOut( float t, float b, float c, float d )
{
    if( (t /= d / 2) < 1 )
    {
        return c / 2 * t * t * t * t + b;
    }

    return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
}

// Quartic t^4 easing.
// Deceleration until halfway, then acceleration.
float EaseQuarticOutIn( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseQuarticOut(t * 2, b, c / 2, d);
    }

    return EaseQuarticIn((t * 2) - d, b + c / 2, c / 2, d);
}

// Quintic t^5 easing.
// Accelerating from zero velocity.
float EaseQuinticIn( float t, float b, float c, float d )
{
    return c * (t /= d) * t * t * t * t + b;
}

// Quintic t^5 easing.
// Decelerating from zero velocity.
float EaseQuinticOut( float t, float b, float c, float d )
{
    return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
}

// Quintic t^5 easing.
// Acceleration until halfway, then deceleration.
float EaseQuinticInOut( float t, float b, float c, float d )
{
    if( (t /= d / 2) < 1 )
    {
        return c / 2 * t * t * t * t * t + b;
    }

    return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
}

// Quintic t^5 easing.
// Deceleration until halfway, then acceleration.
float EaseQuinticOutIn( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseQuinticOut(t * 2, b, c / 2, d);
    }

    return EaseQuinticIn((t * 2) - d, b + c / 2, c / 2, d);
}

// Sinusoidal sin(t) easing.
// Accelerating from zero velocity.
float EaseSinusoidalIn( float t, float b, float c, float d )
{
    return -c * cos(t / d * (PI / 2)) + c + b;
}

// Sinusoidal sin(t) easing.
// Decelerating from zero velocity.
float EaseSinusoidalOut( float t, float b, float c, float d )
{
    return c * sin(t / d * (PI / 2)) + b;
}

// Sinusoidal sin(t) easing.
// Acceleration until halfway, then deceleration.
float EaseSinusoidalInOut( float t, float b, float c, float d )
{
    if( (t /= d / 2) < 1 )
    {
        return c / 2 * (sin(PI * t / 2)) + b;
    }

    return -c / 2 * (cos(PI * --t / 2) - 2) + b;
}

// Sinusoidal sin(t) easing.
// Deceleration until halfway, then acceleration.
float EaseSinusoidalOutIn( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseSinusoidalOut(t * 2, b, c / 2, d);
    }

    return EaseSinusoidalIn((t * 2) - d, b + c / 2, c / 2, d);
}

// Circular sqrt(1 - t^2) easing.
// Accelerating from zero velocity.
float EaseCircularIn( float t, float b, float c, float d )
{
    return -c * (sqrt(1 - (t /= d) * t) - 1) + b;
}

// Circular sqrt(1 - t^2) easing.
// Decelerating from zero velocity.
float EaseCircularOut( float t, float b, float c, float d )
{
    return c * sqrt(1 - (t = t / d - 1) * t) + b;
}

// Circular sqrt(1 - t^2) easing.
// Acceleration until halfway, then deceleration.
float EaseCircularInOut( float t, float b, float c, float d )
{
    if( (t /= d / 2) < 1 )
    {
        return -c / 2 * (sqrt(1 - t * t) - 1) + b;
    }

    return c / 2 * (sqrt(1 - (t -= 2) * t) + 1) + b;
}

// Circular sqrt(1 - t^2) easing.
// Deceleration until halfway, then acceleration.
float EaseCircularOutIn( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseCircularOut(t * 2, b, c / 2, d);
    }

    return EaseCircularIn((t * 2) - d, b + c / 2, c / 2, d);
}

// Back (overshooting cubic easing (s + 1) * t^3 - s * t^2) easing.
// Accelerating from zero velocity.
float EaseBackIn( float t, float b, float c, float d )
{
    return c * (t /= d) * t * ((1.70158 + 1) * t - 1.70158) + b;
}

// Back (overshooting cubic easing (s + 1) * t^3 - s * t^2) easing.
// Decelerating from zero velocity.
float EaseBackOut( float t, float b, float c, float d )
{
    return c * ((t = t / d - 1) * t * ((1.70158 + 1) * t + 1.70158) + 1) + b;
}

// Back (overshooting cubic easing (s + 1) * t^3 - s * t^2) easing.
// Acceleration until halfway, then deceleration.
float EaseBackInOut( float t, float b, float c, float d )
{
    float s = 1.70158;
    if( (t /= d / 2) < 1 )
    {
        return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
    }

    return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
}

// Back (overshooting cubic easing (s + 1) * t^3 - s * t^2) easing.
// Deceleration until halfway, then acceleration.
float EaseBackOutIn( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseBackOut(t * 2, b, c / 2, d);
    }

    return EaseBackIn((t * 2) - d, b + c / 2, c / 2, d);
}

// Exponential 2^t easing.
// Accelerating from zero velocity.
float EaseExponentialIn( float t, float b, float c, float d )
{
    return (t == 0) ? b : c * pow(2, 10 * (t / d - 1)) + b;
}

// Exponential 2^t easing.
// Decelerating from zero velocity.
float EaseExponentialOut( float t, float b, float c, float d )
{
    return (t == d) ? b + c : c * (-pow(2, -10 * t / d) + 1) + b;
}

// Exponential 2^t easing.
// Acceleration until halfway, then deceleration.
float EaseExponentialInOut( float t, float b, float c, float d )
{
    if( t == 0 )
    {
        return b;
    }

    if( t == d )
    {
        return b + c;
    }

    if( (t /= d / 2) < 1 )
    {
        return c / 2 * pow(2, 10 * (t - 1)) + b;
    }

    return c / 2 * (-pow(2, -10 * --t) + 2) + b;
}

// Exponential 2^t easing.
// Deceleration until halfway, then acceleration.
float EaseExponentialOutIn( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseExponentialOut(t * 2, b, c / 2, d);
    }

    return EaseExponentialIn((t * 2) - d, b + c / 2, c / 2, d);
}

// Elastic (exponentially decaying sin wave) easing.
// Accelerating from zero velocity.
float EaseElasticIn( float t, float b, float c, float d )
{
    if( (t /= d) == 1 )
    {
        return b + c;
    }

    float p = d * 0.3;
    float s = p / 4;

    return -(c * pow(2, 10 * (t -= 1)) * sin((t * d - s) * (2 * PI) / p)) + b;
}

// Elastic (exponentially decaying sin wave) easing.
// Decelerating from zero velocity.
float EaseElasticOut( float t, float b, float c, float d )
{
    if( (t /= d) == 1 )
    {
        return b + c;
    }

    float p = d * 0.3;
    float s = p / 4;

    return c * pow(2, -10 * t) * sin((t * d - s) * (2 * PI) / p) + c + b;
}

// Elastic (exponentially decaying sin wave) easing.
// Acceleration until halfway, then deceleration.
float EaseElasticInOut( float t, float b, float c, float d )
{
    if( (t /= d / 2) == 2 )
    {
        return b + c;
    }

    float p = d * (0.3 * 1.5);
    float s = p / 4;

    if( t < 1 )
    {
        return -0.5 * (c * pow(2, 10 * (t -= 1)) * sin((t * d - s) * (2 * PI) / p)) + b;
    }

    return c * pow(2, -10 * (t -= 1)) * sin((t * d - s) * (2 * PI) / p) * 0.5 + c + b;
}

// Elastic (exponentially decaying sin wave) easing.
// Deceleration until halfway, then acceleration.
float EaseElasticOutIn( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseElasticOut(t * 2, b, c / 2, d);
    }

    return EaseElasticIn((t * 2) - d, b + c / 2, c / 2, d);
}

// Bounce (exponentially decaying parabolic) easing.
// Decelerating from zero velocity.
float EaseBounceOut( float t, float b, float c, float d )
{
    if( (t /= d) < (1 / 2.75) )
    {
        return c * (7.5625 * t * t) + b;
    }
    else if( t < (2 / 2.75) )
    {
        return c * (7.5625 * (t -= (1.5 / 2.75)) * t + 0.75) + b;
    }
    else if( t < (2.5 / 2.75) )
    {
        return c * (7.5625 * (t -= (2.25 / 2.75)) * t + 0.9375) + b;
    }
    else
    {
        return c * (7.5625 * (t -= (2.625 / 2.75)) * t + 0.984375) + b;
    }
}

// Bounce (exponentially decaying parabolic) easing.
// Accelerating from zero velocity.
float EaseBounceIn( float t, float b, float c, float d )
{
    return c - EaseBounceOut(d - t, 0, c, d) + b;
}

// Bounce (exponentially decaying parabolic) easing.
// Acceleration until halfway, then deceleration.
float EaseBounceInOut( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseBounceIn(t * 2, 0, c, d) * 0.5 + b;
    }
    else
    {
        return EaseBounceOut(t * 2 - d, 0, c, d) * 0.5 + c * 0.5 + b;
    }
}

// Bounce (exponentially decaying parabolic) easing.
// Deceleration until halfway, then acceleration.
float EaseBounceOutIn( float t, float b, float c, float d )
{
    if( t < d / 2 )
    {
        return EaseBounceOut(t * 2, b, c / 2, d);
    }

    return EaseBounceIn((t * 2) - d, b + c / 2, c / 2, d);
}

#endif