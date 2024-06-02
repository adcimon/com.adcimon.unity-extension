using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
	public bool playOnStart = true;
	public string device = "";

	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	private void Start()
	{
		if (playOnStart)
		{
			Play();
		}
	}

	/// <summary>
	/// Play the microphone device.
	/// </summary>
	public void Play()
	{
		if (Microphone.IsRecording(device))
		{
			Microphone.End(device);
		}

		if (!Contains(Microphone.devices, device))
		{
			device = Microphone.devices[0];
		}

		int minFreq, maxFreq;
		Microphone.GetDeviceCaps(device, out minFreq, out maxFreq);

		audioSource.clip = Microphone.Start(device, true, 1, maxFreq);
		StartCoroutine(PlayWhenReady());
	}

	/// <summary>
	/// Stop the microphone device.
	/// </summary>
	public void Stop()
	{
		if (Microphone.IsRecording(device))
		{
			Microphone.End(device);
		}
	}

	private IEnumerator PlayWhenReady()
	{
		yield return new WaitUntil(() => Microphone.GetPosition(device) > 1);
		audioSource.loop = true;
		audioSource.Play();
	}

	private bool Contains(string[] array, string str)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Equals(str))
			{
				return true;
			}
		}

		return false;
	}
}