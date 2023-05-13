using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
	public GravityAttractor attractor;

	private Rigidbody rigidBody;

	private void Awake()
	{
		rigidBody = gameObject.GetComponent<Rigidbody>();

		// Gravity and rotation are simulated in the attractor.
		rigidBody.useGravity = false;
		rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
	}

	private void FixedUpdate()
	{
		if( attractor )
		{
			attractor.Attract(rigidBody);
		}
	}
}