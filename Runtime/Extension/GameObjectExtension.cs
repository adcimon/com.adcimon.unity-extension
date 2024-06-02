using UnityEngine;

public static class GameObjectExtension
{
	/// <summary>
	/// Enables all the children of the gameObject recursively.
	/// </summary>
	public static void EnableChildren(this GameObject gameObject)
	{
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			gameObject.transform.GetChild(i).gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// Disables all the children of the gameObject recursively.
	/// </summary>
	public static void DisableChildren(this GameObject gameObject)
	{
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			gameObject.transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// Destroys all the children of the gameObject recursively.
	/// </summary>
	public static void DestroyChildren(this GameObject gameObject)
	{
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			MonoBehaviour.Destroy(gameObject.transform.GetChild(i).gameObject);
		}
	}

	/// <summary>
	/// Destroys all the children of the gameObject recursively after the specified seconds.
	/// </summary>
	public static void DestroyChildren(this GameObject gameObject, float time)
	{
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			MonoBehaviour.Destroy(gameObject.transform.GetChild(i).gameObject, time);
		}
	}

	/// <summary>
	/// Returns the component of type T if the gameobject has one attached, adds one if it doesn't.
	/// </summary>
	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
	}

	/// <summary>
	/// Checks whether the gameobject has a component of type T attached.
	/// </summary>
	public static bool HasComponent<T>(this GameObject gameObject) where T : Component
	{
		return gameObject.GetComponent<T>() != null;
	}

	/// <summary>
	/// Sets the layer of the gameobject and its children.
	/// </summary>
	public static void SetLayerRecursively(this GameObject gameObject, int layer)
	{
		gameObject.layer = layer;

		int childCount = gameObject.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			gameObject.transform.GetChild(i).gameObject.SetLayerRecursively(layer);
		}
	}
}