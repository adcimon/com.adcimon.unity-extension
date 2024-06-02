using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(AspectRatioFitter))]
public class DrawTexture : MonoBehaviour
{
	private RawImage image;
	private AspectRatioFitter fitter;
	private Texture texture;

	private void Awake()
	{
		image = this.GetComponent<RawImage>();
		fitter = this.GetComponent<AspectRatioFitter>();
	}

	private void Update()
	{
		if (!texture)
		{
			return;
		}

		float aspectRatio = (float)texture.width / (float)texture.height;
		aspectRatio = (float)(Math.Truncate((double)aspectRatio * 100.0) / 100.0);
		if (fitter.aspectRatio != aspectRatio)
		{
			fitter.aspectRatio = aspectRatio;
		}
	}

	public void SetTexture(Texture texture)
	{
		this.texture = texture;
		image.color = Color.white;
		image.texture = this.texture;
		image.SetMaterialDirty();
	}

	public void SetTexture(Texture2D texture)
	{
		this.texture = texture;
		image.color = Color.white;
		image.texture = this.texture;
		image.SetMaterialDirty();
	}

	public void SetTexture(RenderTexture texture)
	{
		this.texture = texture;
		image.color = Color.white;
		image.texture = this.texture;
		image.SetMaterialDirty();
	}
}