using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MicrophoneInput))]
public class MicrophoneInputInspector : Editor
{
	private MicrophoneInput script;

	private void Awake()
	{
		this.script = (MicrophoneInput)target;
	}

	public override void OnInspectorGUI()
	{
		Rect rect = EditorGUILayout.GetControlRect(false);
		if (EditorGUI.DropdownButton(rect, new GUIContent("Select Microphone"), FocusType.Keyboard))
		{
			DrawWebcamMenu(rect);
		}

		DrawDefaultInspector();
	}

	private void DrawWebcamMenu(Rect rect)
	{
		GenericMenu menu = new GenericMenu();

		foreach (string device in Microphone.devices)
		{
			menu.AddItem(new GUIContent(device), false, () => ChangeWebcam(device));
		}

		menu.DropDown(rect);
	}

	private void ChangeWebcam(string name)
	{
		serializedObject.Update();
		script.device = name;
		serializedObject.ApplyModifiedProperties();
	}
}