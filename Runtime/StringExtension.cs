using System.Text.RegularExpressions;
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

    /// <summary>
    /// Returns whether the specified string is an email.
    /// </summary>
    public static bool IsEmail( this string str )
    {
        if( string.IsNullOrEmpty(str) )
        {
            return false;
        }

        string regexString = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        Regex regex = new Regex(regexString);
        return regex.IsMatch(str);
    }
}