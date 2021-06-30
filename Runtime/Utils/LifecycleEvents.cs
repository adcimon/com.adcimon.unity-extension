using System;
using UnityEngine;
using UnityEngine.Events;

public class LifecycleEvents : MonoBehaviour
{
    [Serializable]
    public class OnLifecycleEvent : UnityEvent<GameObject, Component> { }

    public OnLifecycleEvent onAwake;
    public OnLifecycleEvent onEnable;
    public OnLifecycleEvent onStart;
    public OnLifecycleEvent onDisable;

    private void Awake()
    {
        onAwake.Invoke(gameObject, this);
    }

    private void OnEnable()
    {
        onEnable.Invoke(gameObject, this);
    }

    private void Start()
    {
        onStart.Invoke(gameObject, this);
    }

    private void OnDisable()
    {
        onDisable.Invoke(gameObject, this);
    }
}