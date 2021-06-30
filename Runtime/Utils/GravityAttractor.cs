using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
	public float gravity = -9.8f;

	public void Attract( Rigidbody body )
    {
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 localUp = body.transform.up;

        // Simulate gravity and rotation.
		body.AddForce(gravityUp * gravity);

        // Align local up axis with the center of the planet.
        body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
	}  
}