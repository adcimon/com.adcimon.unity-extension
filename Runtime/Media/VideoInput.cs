using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class VideoInput : MonoBehaviour
{
	public bool playOnStart = true;
	public RenderTexture outputTexture { get; private set; }
	public UnityEvent<RenderTexture> onOutputTexture;

	public VideoPlayer videoPlayer { get; private set; }
	public AudioSource audioSource { get; private set; }

	private void Awake()
	{
		videoPlayer = gameObject.GetComponent<VideoPlayer>();
		videoPlayer.playOnAwake = false;

		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.playOnAwake = false;
		audioSource.Pause();
	}

	private void Start()
	{
		if( playOnStart )
		{
			Play();
		}
	}

	private void OnDestroy()
	{
		Stop();
		Release();
	}

	private IEnumerator PrepareCoroutine()
	{
		videoPlayer.source = VideoSource.VideoClip;
		videoPlayer.renderMode = VideoRenderMode.RenderTexture;
		videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		videoPlayer.EnableAudioTrack(0, true);
		videoPlayer.SetTargetAudioSource(0, audioSource);
		videoPlayer.Prepare();

		while( !videoPlayer.isPrepared )
		{
			yield return null;
		}

		StartCoroutine(PlayCoroutine());
	}

	private IEnumerator PlayCoroutine()
	{
		videoPlayer.Play();
		audioSource.Play();

		Debug.Log("Playing video " + videoPlayer.clip.originalPath + " of " + videoPlayer.clip.width + "x" + videoPlayer.clip.height);

		if( outputTexture )
		{
			outputTexture.Release();
			outputTexture = null;
		}

		outputTexture = new RenderTexture((int)videoPlayer.clip.width, (int)videoPlayer.clip.height, 0);
		videoPlayer.targetTexture = outputTexture;
		onOutputTexture?.Invoke(outputTexture);

		while( videoPlayer.isPlaying )
		{
			yield return null;
		}
	}

	/// <summary>
	/// Play the video playback.
	/// </summary>
	public void Play()
	{
		if( !videoPlayer.clip )
		{
			Debug.LogError("Video clip not found");
			return;
		}

		if( !videoPlayer.isPrepared )
		{
			StartCoroutine(PrepareCoroutine());
		}
		else
		{
			StartCoroutine(PlayCoroutine());
		}
	}

	/// <summary>
	/// Pause the video playback.
	/// </summary>
	public void Pause()
	{
		videoPlayer.Pause();
		audioSource.Pause();
	}

	/// <summary>
	/// Stop the video playback.
	/// </summary>
	public void Stop()
	{
		videoPlayer.Stop();
		audioSource.Stop();
	}

	/// <summary>
	/// Release the resources.
	/// </summary>
	private void Release()
	{
		if( outputTexture )
		{
			outputTexture.Release();
			outputTexture = null;
		}
	}
}