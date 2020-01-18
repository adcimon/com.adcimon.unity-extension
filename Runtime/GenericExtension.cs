using System;
using System.Linq;

public static class GenericExtension
{
    /// <summary>
    /// Determines whether the object exists as an element in the specified array.
    /// </summary>
    public static bool IsIn<T>( this T target, params T[] array )
    {
        return array.Contains(target);
    }

    /// <summary>
    /// Determines whether the object is between the specified range.
    /// </summary>
    public static bool IsBetween<T>( this T target, T lower, T upper, bool inclusive = true ) where T : IComparable<T>
    {
        if( inclusive )
        {
            return target.CompareTo(lower) >= 0 && target.CompareTo(upper) <= 0;
        }
        else
        {
            return target.CompareTo(lower) > 0 && target.CompareTo(upper) < 0;
        }
    }
}