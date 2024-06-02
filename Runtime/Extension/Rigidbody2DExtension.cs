using UnityEngine;

public static class Rigidbody2DExtension
{
	/// <summary>
	/// Freezes the rigidbody2D movement.
	/// </summary>
	public static void Freeze(this Rigidbody2D rigidbody2D)
	{
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0;
		rigidbody2D.isKinematic = true;
	}
}