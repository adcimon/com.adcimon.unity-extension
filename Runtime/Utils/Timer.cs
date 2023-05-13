using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	[Serializable]
	public class OnTimerEvent : UnityEvent<Timer> { }

	public float time;
	public bool unscaled = true;
	public OnTimerEvent onStart;
	public OnTimerEvent onEnd;

	private enum State { Stopped, Playing, Paused }
	private State state = State.Stopped;

	private float elapsedTime;

	private void Update()
	{
		if( state != State.Playing )
		{
			return;
		}

		elapsedTime += (unscaled) ? Time.unscaledDeltaTime : Time.deltaTime;
		if( elapsedTime >= time )
		{
			state = State.Stopped;

			onEnd.Invoke(this);
		}
	}

	public void Play()
	{
		if( state == State.Playing )
		{
			return;
		}

		elapsedTime = 0;
		state = State.Playing;

		onStart.Invoke(this);
	}

	public void Pause()
	{
		state = State.Paused;
	}

	public void Resume()
	{
		if( state == State.Paused )
		{
			state = State.Playing;
		}
	}

	public void Restart()
	{
		state = State.Stopped;
		Play();
	}

	public void Stop()
	{
		state = State.Stopped;
	}
}