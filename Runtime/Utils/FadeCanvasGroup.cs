using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class FadeCanvasGroup : MonoBehaviour
{
    [Serializable]
    public class OnFadeEvent : UnityEvent<CanvasGroup> { }

    public float time;
    public AnimationCurve curve;

    [Header("Fade In")]
    public OnFadeEvent onFadeInStart;
    public OnFadeEvent onFadeInEnd;

    [Header("Fade Out")]
    public OnFadeEvent onFadeOutStart;
    public OnFadeEvent onFadeOutEnd;

    private CanvasGroup canvasGroup;
    private bool fade;
    private float elapsedTime;
    private bool running = false;

    private void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    private void LateUpdate()
    {
        if( !running )
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        if( elapsedTime >= time )
        {
            running = false;

            if( fade )
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;

                onFadeOutEnd.Invoke(canvasGroup);
            }
            else
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;

                onFadeInEnd.Invoke(canvasGroup);
            }

            return;
        }

        float value = curve.Evaluate(elapsedTime / time);
        canvasGroup.alpha = (fade) ? (1 - value) : value;
    }

    public void FadeIn()
    {
        if( running || canvasGroup.alpha == 1 )
        {
            return;
        }

        fade = false;
        elapsedTime = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        running = true;

        onFadeInStart.Invoke(canvasGroup);
    }

    public void FadeOut()
    {
        if( running || canvasGroup.alpha == 0 )
        {
            return;
        }

        fade = true;
        elapsedTime = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        running = true;

        onFadeOutStart.Invoke(canvasGroup);
    }

    public void Fade()
    {
        if( running )
        {
            return;
        }

        if( canvasGroup.alpha == 0 )
        {
            FadeIn();
            return;
        }

        if( canvasGroup.alpha == 1 )
        {
            FadeOut();
            return;
        }
    }
}