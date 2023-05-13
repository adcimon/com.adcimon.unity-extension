using UnityEngine;

public class CopyTransform : MonoBehaviour
{
	public Transform target;
	public bool position = true;
	public bool rotation = true;
	public bool scale = true;

	private void LateUpdate()
	{
		if( !target )
		{
			return;
		}

		if( position )
		{
			gameObject.transform.position = target.position;
		}

		if( rotation )
		{
			gameObject.transform.rotation = target.rotation;
		}

		if( scale )
		{
			gameObject.transform.localScale = target.lossyScale;
		}
	}
}