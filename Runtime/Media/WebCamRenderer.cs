using System;
using UnityEngine;

public class WebCamRenderer : MonoBehaviour
{
    private enum State { Stopped, Initializing, Playing }
    private State state = State.Stopped;

    public string deviceName = "";
    public int width = 640;
    public int height = 480;
    public int fps = 30;
    public Material material;
    public string textureName = "_MainTex";
    public bool playOnStart = true;
    public bool useRenderTexture = true;

    public WebCamTexture webcamTexture { get; private set; }
    public RenderTexture renderTexture { get; private set; }

    private void Start()
    {
        ListDevices();

        if( playOnStart )
        {
            Play(deviceName);
        }
    }

    private void Update()
    {
        if( webcamTexture && webcamTexture.isPlaying )
        {
            if( state == State.Initializing )
            {
                width = webcamTexture.width;
                height = webcamTexture.height;
                fps = (int)webcamTexture.requestedFPS;
                Debug.Log("Playing " + webcamTexture.deviceName + " " + webcamTexture.width + "x" + webcamTexture.height + " at " + webcamTexture.requestedFPS);

                if( useRenderTexture )
                {
                    renderTexture = new RenderTexture(webcamTexture.width, webcamTexture.height, 16);
                }

                state = State.Playing;
            }

            if( useRenderTexture )
            {
                Graphics.Blit(webcamTexture, renderTexture);
            }
        }
    }

    private void Play( string deviceName )
    {
        if( state != State.Stopped )
        {
            return;
        }

        try
        {
            webcamTexture = new WebCamTexture(deviceName, width, height, fps);
            webcamTexture.Play();

            if( material )
            {
                material.SetTexture(textureName, webcamTexture);
            }

            state = State.Initializing;
        }
        catch( Exception exception )
        {
            Debug.Log(exception.Message);
            return;
        }
    }

    private void Stop()
    {
        if( state == State.Stopped )
        {
            return;
        }

        if( renderTexture )
        {
            renderTexture.Release();
            renderTexture = null;
        }

        state = State.Stopped;
    }

    private void ListDevices()
    {
        foreach( WebCamDevice device in WebCamTexture.devices )
        {
            Debug.Log(device.name);
        }
    }
}