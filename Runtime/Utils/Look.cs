using UnityEngine;

public class Look : MonoBehaviour
{
	public Transform target;
	public bool smooth = true;
	public float speed = 5;

	private void Update()
	{
		if( !target )
		{
			return;
		}

		if( !smooth )
		{
			transform.LookAt(target);
		}
		else
		{
			Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
		}
	}
}