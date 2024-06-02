using UnityEngine;

public class BackgroundPlayer : MonoBehaviour
{
	public bool runInBackground = true;

	private void Awake()
	{
		Application.runInBackground = runInBackground;
	}

	private void Update()
	{
		if (Application.runInBackground != runInBackground)
		{
			Application.runInBackground = runInBackground;
		}
	}
}