using UnityEngine;

public class ToggleObject : MonoBehaviour
{
	public Object target;

	public void Toggle()
	{
		if( !target )
		{
			return;
		}

		System.Type type = target.GetType();
		if( type == typeof(GameObject) )
		{
			GameObject go = (GameObject)target;
			go.SetActive(!go.activeSelf);
		}
		else if( type.IsSubclassOf(typeof(Behaviour)) )
		{
			Behaviour behaviour = (Behaviour)target;
			behaviour.enabled = !behaviour.enabled;
		}
	}
}