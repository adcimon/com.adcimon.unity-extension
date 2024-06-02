using UnityEngine;

/// <summary>
/// Easing equation function.
/// </summary>
/// <param name="t">Current time.</param>
/// <param name="b">Starting value.</param>
/// <param name="c">Final value.</param>
/// <param name="d">Duration.</param>
/// <returns>Animated value.</returns>
public delegate float EasingFunction(float t, float b, float c, float d);

public static class EasingFunctions
{
	/// <summary>
	/// Simple linear with no easing.
	/// </summary>
	public static float Linear(float t, float b, float c, float d)
	{
		return c * t / d + b;
	}

	/// <summary>
	/// Quadratic t^2 easing.
	/// </summary>
	public static class Quadratic
	{
		/// <summary>
		/// Accelerating from zero velocity.
		/// </summary>
		public static float In(float t, float b, float c, float d)
		{
			return c * (t /= d) * t + b;
		}

		/// <summary>
		/// Decelerating from zero velocity.
		/// </summary>
		public static float Out(float t, float b, float c, float d)
		{
			return -c * (t /= d) * (t - 2) + b;
		}

		/// <summary>
		/// Acceleration until halfway, then deceleration.
		/// </summary>
		public static float InOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2) < 1)
			{
				return c / 2 * t * t + b;
			}

			return -c / 2 * ((--t) * (t - 2) - 1) + b;
		}

		/// <summary>
		/// Deceleration until halfway, then acceleration.
		/// </summary>
		public static float OutIn(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return Out(t * 2, b, c / 2, d);
			}

			return In((t * 2) - d, b + c / 2, c / 2, d);
		}
	}

	/// <summary>
	/// Cubic t^3 easing.
	/// </summary>
	public static class Cubic
	{
		/// <summary>
		/// Accelerating from zero velocity.
		/// </summary>
		public static float In(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * t + b;
		}

		/// <summary>
		/// Decelerating from zero velocity.
		/// </summary>
		public static float Out(float t, float b, float c, float d)
		{
			return c * ((t = t / d - 1) * t * t + 1) + b;
		}

		/// <summary>
		/// Acceleration until halfway, then deceleration.
		/// </summary>
		public static float InOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2) < 1)
			{
				return c / 2 * t * t * t + b;
			}

			return c / 2 * ((t -= 2) * t * t + 2) + b;
		}

		/// <summary>
		/// Deceleration until halfway, then acceleration.
		/// </summary>
		public static float OutIn(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return Out(t * 2, b, c / 2, d);
			}

			return In((t * 2) - d, b + c / 2, c / 2, d);
		}
	}

	/// <summary>
	/// Quartic t^4 easing.
	/// </summary>
	public static class Quartic
	{
		/// <summary>
		/// Accelerating from zero velocity.
		/// </summary>
		public static float In(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * t * t + b;
		}

		/// <summary>
		/// Decelerating from zero velocity.
		/// </summary>
		public static float Out(float t, float b, float c, float d)
		{
			return -c * ((t = t / d - 1) * t * t * t - 1) + b;
		}

		/// <summary>
		/// Acceleration until halfway, then deceleration.
		/// </summary>
		public static float InOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2) < 1)
			{
				return c / 2 * t * t * t * t + b;
			}

			return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
		}

		/// <summary>
		/// Deceleration until halfway, then acceleration.
		/// </summary>
		public static float OutIn(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return Out(t * 2, b, c / 2, d);
			}

			return In((t * 2) - d, b + c / 2, c / 2, d);
		}
	}

	/// <summary>
	/// Quintic t^5 easing.
	/// </summary>
	public static class Quintic
	{
		/// <summary>
		/// Accelerating from zero velocity.
		/// </summary>
		public static float In(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * t * t * t + b;
		}

		/// <summary>
		/// Decelerating from zero velocity.
		/// </summary>
		public static float Out(float t, float b, float c, float d)
		{
			return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
		}

		/// <summary>
		/// Acceleration until halfway, then deceleration.
		/// </summary>
		public static float InOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2) < 1)
			{
				return c / 2 * t * t * t * t * t + b;
			}

			return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
		}

		/// <summary>
		/// Deceleration until halfway, then acceleration.
		/// </summary>
		public static float OutIn(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return Out(t * 2, b, c / 2, d);
			}

			return In((t * 2) - d, b + c / 2, c / 2, d);
		}
	}

	/// <summary>
	/// Sinusoidal sin(t) easing.
	/// </summary>
	public static class Sinusoidal
	{
		/// <summary>
		/// Accelerating from zero velocity.
		/// </summary>
		public static float In(float t, float b, float c, float d)
		{
			return -c * Mathf.Cos(t / d * (Mathf.PI / 2)) + c + b;
		}

		/// <summary>
		/// Decelerating from zero velocity.
		/// </summary>
		public static float Out(float t, float b, float c, float d)
		{
			return c * Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
		}

		/// <summary>
		/// Acceleration until halfway, then deceleration.
		/// </summary>
		public static float InOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2) < 1)
			{
				return c / 2 * (Mathf.Sin(Mathf.PI * t / 2)) + b;
			}

			return -c / 2 * (Mathf.Cos(Mathf.PI * --t / 2) - 2) + b;
		}

		/// <summary>
		/// Deceleration until halfway, then acceleration.
		/// </summary>
		public static float OutIn(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return Out(t * 2, b, c / 2, d);
			}

			return In((t * 2) - d, b + c / 2, c / 2, d);
		}
	}

	/// <summary>
	/// Circular sqrt(1 - t^2) easing.
	/// </summary>
	public static class Circular
	{
		/// <summary>
		/// Accelerating from zero velocity.
		/// </summary>
		public static float In(float t, float b, float c, float d)
		{
			return -c * (Mathf.Sqrt(1 - (t /= d) * t) - 1) + b;
		}

		/// <summary>
		/// Decelerating from zero velocity.
		/// </summary>
		public static float Out(float t, float b, float c, float d)
		{
			return c * Mathf.Sqrt(1 - (t = t / d - 1) * t) + b;
		}

		/// <summary>
		/// Acceleration until halfway, then deceleration.
		/// </summary>
		public static float InOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2) < 1)
			{
				return -c / 2 * (Mathf.Sqrt(1 - t * t) - 1) + b;
			}

			return c / 2 * (Mathf.Sqrt(1 - (t -= 2) * t) + 1) + b;
		}

		/// <summary>
		/// Deceleration until halfway, then acceleration.
		/// </summary>
		public static float OutIn(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return Out(t * 2, b, c / 2, d);
			}

			return In((t * 2) - d, b + c / 2, c / 2, d);
		}
	}

	/// <summary>
	/// Back (overshooting cubic easing (s + 1) * t^3 - s * t^2) easing.
	/// </summary>
	public static class Back
	{
		/// <summary>
		/// Accelerating from zero velocity.
		/// </summary>
		public static float In(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * ((1.70158f + 1) * t - 1.70158f) + b;
		}

		/// <summary>
		/// Decelerating from zero velocity.
		/// </summary>
		public static float Out(float t, float b, float c, float d)
		{
			return c * ((t = t / d - 1) * t * ((1.70158f + 1) * t + 1.70158f) + 1) + b;
		}

		/// <summary>
		/// Acceleration until halfway, then deceleration.
		/// </summary>
		public static float InOut(float t, float b, float c, float d)
		{
			float s = 1.70158f;
			if ((t /= d / 2) < 1)
			{
				return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
			}

			return c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
		}

		/// <summary>
		/// Deceleration until halfway, then acceleration.
		/// </summary>
		public static float OutIn(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return Out(t * 2, b, c / 2, d);
			}

			return In((t * 2) - d, b + c / 2, c / 2, d);
		}
	}

	/// <summary>
	/// Exponential 2^t easing.
	/// </summary>
	public static class Exponential
	{
		/// <summary>
		/// Accelerating from zero velocity.
		/// </summary>
		public static float In(float t, float b, float c, float d)
		{
			return (t == 0) ? b : c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
		}

		/// <summary>
		/// Decelerating from zero velocity.
		/// </summary>
		public static float Out(float t, float b, float c, float d)
		{
			return (t == d) ? b + c : c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
		}

		/// <summary>
		/// Acceleration until halfway, then deceleration.
		/// </summary>
		public static float InOut(float t, float b, float c, float d)
		{
			if (t == 0)
			{
				return b;
			}

			if (t == d)
			{
				return b + c;
			}

			if ((t /= d / 2) < 1)
			{
				return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;
			}

			return c / 2 * (-Mathf.Pow(2, -10 * --t) + 2) + b;
		}

		/// <summary>
		/// Deceleration until halfway, then acceleration.
		/// </summary>
		public static float OutIn(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return Out(t * 2, b, c / 2, d);
			}

			return In((t * 2) - d, b + c / 2, c / 2, d);
		}
	}

	/// <summary>
	/// Elastic (exponentially decaying sin wave) easing.
	/// </summary>
	public static class Elastic
	{
		/// <summary>
		/// Accelerating from zero velocity.
		/// </summary>
		public static float In(float t, float b, float c, float d)
		{
			if ((t /= d) == 1)
			{
				return b + c;
			}

			float p = d * 0.3f;
			float s = p / 4;

			return -(c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
		}

		/// <summary>
		/// Decelerating from zero velocity.
		/// </summary>
		public static float Out(float t, float b, float c, float d)
		{
			if ((t /= d) == 1)
			{
				return b + c;
			}

			float p = d * 0.3f;
			float s = p / 4;

			return c * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) + c + b;
		}

		/// <summary>
		/// Acceleration until halfway, then deceleration.
		/// </summary>
		public static float InOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2) == 2)
			{
				return b + c;
			}

			float p = d * (0.3f * 1.5f);
			float s = p / 4;

			if (t < 1)
			{
				return -0.5f * (c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
			}

			return c * Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * 0.5f + c + b;
		}

		/// <summary>
		/// Deceleration until halfway, then acceleration.
		/// </summary>
		public static float OutIn(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return Out(t * 2, b, c / 2, d);
			}

			return In((t * 2) - d, b + c / 2, c / 2, d);
		}
	}

	/// <summary>
	/// Bounce (exponentially decaying parabolic) easing.
	/// </summary>
	public static class Bounce
	{
		/// <summary>
		/// Accelerating from zero velocity.
		/// </summary>
		public static float In(float t, float b, float c, float d)
		{
			return c - Out(d - t, 0, c, d) + b;
		}

		/// <summary>
		/// Decelerating from zero velocity.
		/// </summary>
		public static float Out(float t, float b, float c, float d)
		{
			if ((t /= d) < (1 / 2.75f))
			{
				return c * (7.5625f * t * t) + b;
			}
			else if (t < (2 / 2.75f))
			{
				return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f) + b;
			}
			else if (t < (2.5 / 2.75f))
			{
				return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f) + b;
			}
			else
			{
				return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f) + b;
			}
		}

		/// <summary>
		/// Acceleration until halfway, then deceleration.
		/// </summary>
		public static float InOut(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return In(t * 2, 0, c, d) * 0.5f + b;
			}
			else
			{
				return Out(t * 2 - d, 0, c, d) * 0.5f + c * 0.5f + b;
			}
		}

		/// <summary>
		/// Deceleration until halfway, then acceleration.
		/// </summary>
		public static float OutIn(float t, float b, float c, float d)
		{
			if (t < d / 2)
			{
				return Out(t * 2, b, c / 2, d);
			}

			return In((t * 2) - d, b + c / 2, c / 2, d);
		}
	}
}