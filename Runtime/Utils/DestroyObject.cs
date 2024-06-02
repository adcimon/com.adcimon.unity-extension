using UnityEngine;

public class DestroyObject : MonoBehaviour
{
	public Object target;
	public float lifetime = 0;
	public bool autoDestroy = false;
	public bool runOnStart = true;

	private void Start()
	{
		if (runOnStart)
		{
			if (autoDestroy)
			{
				AutoDestroy();
			}
			else
			{
				Destroy();
			}
		}
	}

	public void Destroy()
	{
		if (!target)
		{
			return;
		}

		Object.Destroy(target, lifetime);
	}

	public void AutoDestroy()
	{
		target = gameObject;
		Destroy();
	}
}