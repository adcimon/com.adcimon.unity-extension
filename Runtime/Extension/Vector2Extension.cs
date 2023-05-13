using UnityEngine;

public static class Vector2Extension
{
	/// <summary>
	/// Calculates the squared distance between a and b.
	/// </summary>
	public static float SqrDistance( Vector2 a, Vector2 b )
	{
		float x = b.x - a.x;
		float y = b.y - a.y;
		return x * x + y * y;
	}

	/// <summary>
	/// Calculates the reciprocal of the vector.
	/// </summary>
	public static Vector2 Reciprocal( Vector2 v )
	{
		return new Vector2(1.0f / v.x, 1.0f / v.y);
	}

	/// <summary>
	/// Returns the absolute value of the vector.
	/// </summary>
	public static Vector2 Absolute( Vector2 v )
	{
		return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
	}
}