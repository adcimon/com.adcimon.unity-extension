using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraSortMode : MonoBehaviour
{
    public TransparencySortMode sortMode = TransparencySortMode.Default;

	private void Awake()
    {
        this.GetComponent<Camera>().transparencySortMode = sortMode;
	}
}