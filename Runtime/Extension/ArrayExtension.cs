using System;

public static class ArrayExtension
{
	/// <summary>
	/// Gets the sub array of the specified array.
	/// </summary>
	public static T[] SubArray<T>(this T[] array, int from, int to, bool inclusive = true)
	{
		if (array == null || from < 0 || to > array.Length || from > to)
		{
			return null;
		}

		T[] subArray = null;
		int length;
		try
		{
			if (inclusive)
			{
				length = to - from + 1;
				subArray = new T[length];
				Array.Copy(array, from, subArray, 0, length);
			}
			else
			{
				length = to - from - 1;
				subArray = new T[length];
				Array.Copy(array, from + 1, subArray, 0, length);
			}
		}
		catch
		{
			return null;
		}

		return subArray;
	}
}