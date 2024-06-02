using UnityEngine;

public class Logger : MonoBehaviour
{
	public void Log(string message)
	{
		Debug.Log(message);
	}

	public void Warning(string message)
	{
		Debug.LogWarning(message);
	}

	public void Error(string message)
	{
		Debug.LogError(message);
	}
}