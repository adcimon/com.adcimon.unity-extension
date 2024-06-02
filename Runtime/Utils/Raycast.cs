using System;
using UnityEngine;
using UnityEngine.Events;

public class Raycast : MonoBehaviour
{
	public enum DirectionSpace { Local, Global }

	[Serializable]
	public class OnRaycastHit : UnityEvent<GameObject, Collider> { }

	public LayerMask layerMask;
	public DirectionSpace directionSpace = DirectionSpace.Local;
	public Vector3 direction = Vector3.forward;
	public float distance = 1;
	public bool castOnUpdate = true;
	public OnRaycastHit onRaycastHit;

	private new Camera camera;

	private void Awake()
	{
		camera = this.GetComponent<Camera>();
	}

	private void Update()
	{
		if (castOnUpdate)
		{
			Cast();
		}
	}

	public void Cast()
	{
		Ray ray;
		if (camera)
		{
			ray = camera.ScreenPointToRay(Input.mousePosition);
		}
		else
		{
			ray = new Ray(transform.position, ((directionSpace == DirectionSpace.Local) ? (transform.TransformDirection(direction)) : (direction)));
		}

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, distance, layerMask))
		{
			Debug.DrawLine(ray.origin, hit.point, Color.green);

			onRaycastHit.Invoke(gameObject, hit.collider);
		}
		else
		{
			Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.white);
		}
	}
}