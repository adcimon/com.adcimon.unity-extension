using UnityEngine;

public static class ComponentExtension
{
	/// <summary>
	/// Adds a component class of type T to the component's gameobject.
	/// </summary>
	public static T AddComponent<T>(this Component component) where T : Component
	{
		return component.gameObject.AddComponent<T>();
	}

	/// <summary>
	/// Returns the component of type T if the component's gameobject has one attached, adds one if it doesn't.
	/// </summary>
	public static T GetOrAddComponent<T>(this Component component) where T : Component
	{
		return component.GetComponent<T>() ?? component.AddComponent<T>();
	}

	/// <summary>
	/// Checks whether the component's gameobject has a component of type T attached.
	/// </summary>
	public static bool HasComponent<T>(this Component component) where T : Component
	{
		return component.GetComponent<T>() != null;
	}
}