using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
	[Serializable]
	public class OnTriggerEvent : UnityEvent<GameObject, Collider> { }

	public LayerMask layerMask;
	public OnTriggerEvent onTriggerEnter;
	public OnTriggerEvent onTriggerExit;

	private new Collider collider;

	private void Awake()
	{
		collider = this.GetComponent<Collider>();
	}

	private void OnTriggerEnter( Collider other )
	{
		if( !((layerMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) )
		{
			return;
		}

		onTriggerEnter.Invoke(other.gameObject, collider);
	}

	private void OnTriggerExit( Collider other )
	{
		if( !((layerMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) )
		{
			return;
		}

		onTriggerExit.Invoke(other.gameObject, collider);
	}
}