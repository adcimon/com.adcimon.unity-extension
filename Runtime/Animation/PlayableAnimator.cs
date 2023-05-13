using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlayableAnimator : MonoBehaviour
{
	public List<AnimationClip> clips = new List<AnimationClip>();

	private PlayableGraph playableGraph;
	private AnimationMixerPlayable playableMixer;
	private List<AnimationClipPlayable> playableClips = new List<AnimationClipPlayable>();

	private void Awake()
	{
		playableGraph = PlayableGraph.Create(gameObject.name);
		playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

		PlayableOutput playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());

		playableMixer = AnimationMixerPlayable.Create(playableGraph);
		playableOutput.SetSourcePlayable(playableMixer);

		playableGraph.Play();

		for( int i = 0; i < clips.Count; i++ )
		{
			AnimationClip clip = clips[i];
			AddClip(clip);
		}
	}

	private void OnDestroy()
	{
		playableGraph.Destroy();
	}

	public bool AddClip( AnimationClip clip )
	{
		AnimationClipPlayable playableClip = AnimationClipPlayable.Create(playableGraph, clip);

		// Add an input to the playable mixer.
		int inputCount = playableMixer.GetInputCount();
		playableMixer.SetInputCount(inputCount + 1);

		if( playableGraph.Connect(playableClip, 0, playableMixer, inputCount) )
		{
			playableClips.Add(playableClip);

			return true;
		}

		// Remove the input from the playable mixer.
		playableMixer.SetInputCount(inputCount - 1);

		return false;
	}

	public bool IsComplete( int index )
	{
		AnimationClipPlayable clip = playableClips[index];
		return clip.GetTime() > clip.GetAnimationClip().length;
	}

	public PlayState GetState( int index )
	{
		AnimationClipPlayable clip = playableClips[index];
		return clip.GetPlayState();
	}

	public float GetNormalizedTime( int index )
	{
		AnimationClipPlayable clip = playableClips[index];
		return (float)clip.GetTime() / clip.GetAnimationClip().length;
	}

	public void SetTime( int index, float time )
	{
		AnimationClipPlayable clip = playableClips[index];
		clip.SetTime(time);
	}

	public void SetNormalizedTime( int index, float time )
	{
		AnimationClipPlayable clip = playableClips[index];
		clip.SetTime(Mathf.Clamp01(time) * clip.GetAnimationClip().length);
	}

	public void SetWeight( int index, float weight )
	{
		playableMixer.SetInputWeight(index, weight);
	}

	public void SetSpeed( int index, float speed )
	{
		AnimationClipPlayable clip = playableClips[index];
		clip.SetSpeed(speed);
	}

	public void SetDuration( int index, float duration )
	{
		AnimationClipPlayable clip = playableClips[index];
		SetSpeed(index, clip.GetAnimationClip().length / duration);
	}

	public void PlayClip( int index )
	{
		AnimationClipPlayable clip = playableClips[index];
		clip.Play();
	}

	public bool PlayOne( string name )
	{
		int index = clips.FindIndex(clip => clip.name.Equals(name));
		if( index == -1 )
		{
			return false;
		}

		for( int i = 0; i < clips.Count; i++ )
		{
			if( index != i )
			{
				StopClip(i);
			}
		}

		SetTime(index, 0);
		SetWeight(index, 1);
		PlayClip(index);

		return true;
	}

	public void PauseClip( int index )
	{
		AnimationClipPlayable clip = playableClips[index];
		clip.Pause();
	}

	public void StopClip( int index )
	{
		AnimationClipPlayable clip = playableClips[index];
		clip.Pause();
		clip.SetTime(0);
		playableMixer.SetInputWeight(index, 0);
	}
}