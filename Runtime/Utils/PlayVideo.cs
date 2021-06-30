using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class PlayVideo : MonoBehaviour
{
    public RawImage rawImage;
    public RenderTexture renderTexture;
    public bool runInBackground = true;
    public bool playOnAwake = true;

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    private void Awake()
    {
        Application.runInBackground = runInBackground;

        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        if( playOnAwake )
        {
            StartCoroutine(PrepareCoroutine());
        }
    }

    private IEnumerator PrepareCoroutine()
    {
        if( renderTexture != null )
        {
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        }

        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.Prepare();

        while( !videoPlayer.isPrepared )
        {
            yield return null;
        }

        rawImage.texture = videoPlayer.texture;
        if( renderTexture != null )
        {
            videoPlayer.targetTexture = renderTexture;
        }

        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        videoPlayer.Play();
        audioSource.Play();

        while( videoPlayer.isPlaying )
        {
            yield return null;
        }
    }

    public void Play()
    {
        if( !videoPlayer.isPrepared )
        {
            StartCoroutine(PrepareCoroutine());
        }
        else
        {
            StartCoroutine(PlayCoroutine());
        }
    }

    public void Pause()
    {
        videoPlayer.Pause();
        audioSource.Pause();
    }

    public void Stop()
    {
        videoPlayer.Stop();
        audioSource.Stop();
    }
}