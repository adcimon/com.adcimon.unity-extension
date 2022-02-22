using UnityEngine;
using UnityEngine.Events;

public class BlitTexture : MonoBehaviour
{
    public Material material;
    public int pass = 0;

    private Texture inputTexture;
    public RenderTexture outputTexture { get; private set; }
    public UnityEvent<RenderTexture> onOutputTexture;

    private void Update()
    {
        if( !inputTexture )
        {
            return;
        }

        if( !outputTexture || inputTexture.width != outputTexture.width || inputTexture.height != outputTexture.height )
        {
            outputTexture = new RenderTexture(inputTexture.width, inputTexture.height, 0);
            onOutputTexture?.Invoke(outputTexture);
        }

        RenderTexture.active = outputTexture;
        if( material )
        {
            Graphics.Blit(inputTexture, outputTexture, material, pass);
        }
        else
        {
            Graphics.Blit(inputTexture, outputTexture);
        }
    }

    private void OnDestroy()
    {
        Release();
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