using System.IO;
using UnityEngine;
using UnityEditor;

public class GradientToTexture : EditorWindow
{
	public enum GradientType { Horizontal, Vertical, Radial }

	private GradientType type = GradientType.Horizontal;
	private Gradient gradient = new Gradient();
	private int width = 512;
	private int height = 32;

	[MenuItem("Window/Gradient to Texture")]
	private static void Init()
	{
		GradientToTexture window = (GradientToTexture)EditorWindow.GetWindow(typeof(GradientToTexture));
		window.Show();
	}

	private void OnGUI()
	{
		type = (GradientType)EditorGUILayout.EnumPopup("Type", type);
		gradient = EditorGUILayout.GradientField("Gradient", gradient);
		width = EditorGUILayout.IntSlider("Width", width, 2, 2048);
		height = EditorGUILayout.IntSlider("Height", height, 2, 2048);

		if (GUILayout.Button("Generate"))
		{
			Texture2D texture = Generate();
			Save(texture);
		}
	}

	private Texture2D Generate()
	{
		Texture2D texture = new Texture2D(width, height);
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				Color color = Color.white;

				switch (type)
				{
					case GradientType.Horizontal:
						{
							color = gradient.Evaluate(((float)i) / width);
							break;
						}
					case GradientType.Vertical:
						{
							color = gradient.Evaluate(((float)j) / height);
							break;
						}
					case GradientType.Radial:
						{
							float ox = i / (float)width - 0.5f;
							float oy = j / (float)height - 0.5f;
							float d = Mathf.Sqrt(ox * ox + oy * oy);
							color = gradient.Evaluate(d * 2);
							break;
						}
				}

				texture.SetPixel(i, j, color);
			}
		}

		texture.Apply();

		return texture;
	}

	private void Save(Texture2D texture)
	{
		string path = EditorUtility.SaveFilePanel("", "", "gradient.png", "png");
		if (path == "")
		{
			return;
		}

		byte[] bytes = texture.EncodeToPNG();
		if (bytes != null)
		{
			File.WriteAllBytes(path, bytes);
		}
	}
}