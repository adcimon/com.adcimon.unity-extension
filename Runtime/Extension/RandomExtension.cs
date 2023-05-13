using UnityEngine;
using System.Collections.Generic;

public static class RandomExtension
{
	/// <summary>
	/// Returns a random bool.
	/// </summary>
	public static bool RandomBool()
	{
		return Random.value >= 0.5f;
	}

	/// <summary>
	/// Returns a random sign integer, 1 or -1.
	/// </summary>
	public static int RandomSign()
	{
		return Random.value <= 0.5f ? 1 : -1;
	}

	/// <summary>
	/// Returns the integer with a random sign.
	/// </summary>
	public static int RandomSign( this int i )
	{
		return i * (Random.value <= 0.5f ? 1 : -1);
	}

	/// <summary>
	/// Returns the float with a random sign.
	/// </summary>
	public static float RandomSign( this float f )
	{
		return f * (Random.value <= 0.5f ? 1f : -1f);
	}

	/// <summary>
	/// Picks a random object contained in the array.
	/// </summary>
	public static T PickRandom<T>( params T[] array )
	{
		if( array.Length == 0 )
		{
			return default(T);
		}

		return array[Random.Range(0, array.Length)];
	}

	/// <summary>
	/// Picks a random object contained in the list.
	/// </summary>
	public static T PickRandom<T>( this IList<T> list )
	{
		if( list.Count == 0 )
		{
			return default(T);
		}

		return list[Random.Range(0, list.Count)];
	}

	/// <summary>
	/// Pops a random object contained in the list.
	/// </summary>
	public static T PopRandom<T>( this IList<T> list )
	{
		if( list.Count == 0 )
		{
			return default(T);
		}

		int index = Random.Range(0, list.Count);
		T value = list[index];
		list.RemoveAt(index);

		return value;
	}

	/// <summary>
	/// Shuffles the array.
	/// </summary>
	public static void Shuffle<T>( this T[] array )
	{
		for( int i = array.Length - 1; i > 0; i-- )
		{
			int j = Random.Range(0, i);
			T temp = array[i];
			array[i] = array[j];
			array[j] = temp;
		}
	}

	/// <summary>
	/// Shuffles the list.
	/// </summary>
	public static void Shuffle<T>( this IList<T> list )
	{
		for( int i = list.Count - 1; i > 0; i-- )
		{
			int j = Random.Range(0, i);
			T temp = list[i];
			list[i] = list[j];
			list[j] = temp;
		}
	}
}