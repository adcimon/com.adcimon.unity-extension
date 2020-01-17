using UnityEngine;

public static class StringExtension
{
    /// <summary>
    /// Indicates whether the specified string is null or an empty string.
    /// </summary>
    public static bool IsNullOrEmpty( this string str )
    {
        return string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// Replaces each format item in the string with the specified text equivalent of a corresponding object's value.
    /// </summary>
    public static string Format( this string str, params object[] args )
    {
        return string.Format(str, args);
    }

    /// <summary>
    /// Adds a color tag to the specified string.
    /// </summary>
    public static string Color( this string str, Color color )
    {
        string hex = ColorUtility.ToHtmlStringRGBA(color);
        return "<color=#" + hex + ">" + str + "</color>";
    }

    /// <summary>
    /// Adds a bold tag to the specified string.
    /// </summary>
    public static string Bold( this string str )
    {
        return "<b>" + str + "</b>";
    }

    /// <summary>
    /// Adds an italic tag to the specified string.
    /// </summary>
    public static string Italic( this string str )
    {
        return "<i>" + str + "</i>";
    }
}