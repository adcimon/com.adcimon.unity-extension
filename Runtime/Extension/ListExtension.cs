using System.Collections.Generic;

public static class ListExtension
{
	/// <summary>
	/// Returns the index of the minimum element of the list.
	/// </summary>
	public static int ArgMin(this IList<int> list)
	{
		int index = -1;
		int value = int.MaxValue;

		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] < value)
			{
				index = i;
				value = list[i];
			}
		}

		return index;
	}

	/// <summary>
	/// Returns the index of the maximum element of the list.
	/// </summary>
	public static int ArgMax(this IList<int> list)
	{
		int index = -1;
		int value = int.MinValue;

		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] > value)
			{
				index = i;
				value = list[i];
			}
		}

		return index;
	}

	/// <summary>
	/// Returns the index of the minimum element of the list.
	/// </summary>
	public static int ArgMin(this IList<float> list)
	{
		int index = -1;
		float value = float.MaxValue;

		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] < value)
			{
				index = i;
				value = list[i];
			}
		}

		return index;
	}

	/// <summary>
	/// Returns the index of the maximum element of the list.
	/// </summary>
	public static int ArgMax(this IList<float> list)
	{
		int index = -1;
		float value = int.MinValue;

		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] > value)
			{
				index = i;
				value = list[i];
			}
		}

		return index;
	}

	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	public static string ToString<T>(this IList<T> list)
	{
		string str = "";
		for (int i = 0; i < list.Count; i++)
		{
			if (i != 0)
			{
				str += " ";
			}

			str += list[i].ToString();
		}

		return str;
	}
}