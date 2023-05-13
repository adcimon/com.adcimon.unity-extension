using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WebcamInput))]
public class WebcamInputInspector : Editor
{
	private WebcamInput script;

	private void Awake()
	{
		this.script = (WebcamInput)target;
	}

	public override void OnInspectorGUI()
	{
		Rect rect = EditorGUILayout.GetControlRect(false);
		if( EditorGUI.DropdownButton(rect, new GUIContent("Select Webcam"), FocusType.Keyboard) )
		{
			DrawWebcamMenu(rect);
		}

		DrawDefaultInspector();
	}

	private void DrawWebcamMenu( Rect rect )
	{
		GenericMenu menu = new GenericMenu();

		foreach( WebCamDevice device in WebCamTexture.devices )
		{
			menu.AddItem(new GUIContent(device.name), false, () => ChangeWebcam(device.name));
		}

		menu.DropDown(rect);
	}

	private void ChangeWebcam( string name )
	{
		serializedObject.Update();
		script.device = name;
		serializedObject.ApplyModifiedProperties();
	}
}