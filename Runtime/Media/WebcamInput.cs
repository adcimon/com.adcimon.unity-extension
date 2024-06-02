using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WebcamInput : MonoBehaviour
{
	private enum State { Stopped, Initializing, Playing }
	private State state = State.Stopped;

	public bool playOnStart = true;
	public string device = "";
	public int width = 640;
	public int height = 480;
	public int fps = 30;
	public bool horizontalFlip = false;
	public bool verticalFlip = false;

	private WebCamTexture inputTexture;
	public RenderTexture outputTexture { get; private set; }
	public UnityEvent<RenderTexture> onOutputTexture;

	private void Start()
	{
		if (playOnStart)
		{
			Play();
		}
	}

	private void Update()
	{
		if (inputTexture && inputTexture.isPlaying && inputTexture.didUpdateThisFrame)
		{
			if (state == State.Initializing)
			{
				width = inputTexture.width;
				height = inputTexture.height;
				fps = (int)inputTexture.requestedFPS;
				Debug.Log("Capturing " + inputTexture.deviceName + " " + inputTexture.width + "x" + inputTexture.height + " at " + inputTexture.requestedFPS + "fps");

				state = State.Playing;
			}

			if (!outputTexture)
			{
				outputTexture = new RenderTexture(inputTexture.width, inputTexture.height, 0);
				onOutputTexture?.Invoke(outputTexture);
			}

			Blit(inputTexture, outputTexture, horizontalFlip, verticalFlip);
		}
	}

	private void OnDestroy()
	{
		Stop();
		Release();
	}

	/// <summary>
	/// Play the webcam device.
	/// </summary>
	public bool Play()
	{
		if (state != State.Stopped)
		{
			return false;
		}

		try
		{
			device = IsWebcam(device) ? device : WebCamTexture.devices[0].name;
			inputTexture = new WebCamTexture(device, width, height, fps);
			inputTexture.Play();

			state = State.Initializing;

			return true;
		}
		catch (Exception exception)
		{
			Debug.Log(exception.Message);
			return false;
		}
	}

	/// <summary>
	/// Stop the webcam device.
	/// </summary>
	public bool Stop()
	{
		if (state == State.Stopped)
		{
			return false;
		}

		Release();

		state = State.Stopped;

		return true;
	}

	/// <summary>
	/// Blit a source texture into the destination render texture with aspect ratio compensation.
	/// </summary>
	private void Blit(Texture source, RenderTexture destination, bool horizontalFlip = false, bool verticalFlip = false)
	{
		if (source == null || destination == null)
		{
			return;
		}

		float aspect1 = (float)source.width / source.height;
		float aspect2 = (float)destination.width / destination.height;

		Vector2 scale = new Vector2(aspect2 / aspect1, aspect1 / aspect2);
		scale = Vector2.Min(Vector2.one, scale);
		if (horizontalFlip)
		{
			scale.x *= -1;
		}
		if (verticalFlip)
		{
			scale.y *= -1;
		}

		Vector2 offset = (Vector2.one - scale) / 2;

		RenderTexture.active = destination;
		Graphics.Blit(source, destination, scale, offset);
	}

	/// <summary>
	/// Get the list of webcam devices' names.
	/// </summary>
	public List<string> GetWebcams()
	{
		return WebCamTexture.devices.Select(d => d.name).ToList();
	}

	/// <summary>
	/// Is the device a webcam?
	/// </summary>
	private bool IsWebcam(string deviceName)
	{
		for (int i = 0; i < WebCamTexture.devices.Length; i++)
		{
			WebCamDevice device = WebCamTexture.devices[i];
			if (device.name.Equals(deviceName))
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Release the resources.
	/// </summary>
	private void Release()
	{
		if (inputTexture)
		{
			inputTexture.Stop();
			inputTexture = null;
		}

		if (outputTexture)
		{
			outputTexture.Release();
			outputTexture = null;
		}
	}
}