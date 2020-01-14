using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

public class CreateEasingFunctions
{
    [MenuItem("Assets/Create/Easing Functions", false, 404)]
    public static void Create()
    {
        Type curvePresetLibraryType = Type.GetType("UnityEditor.CurvePresetLibrary, UnityEditor");
        ScriptableObject scriptableObject = ScriptableObject.CreateInstance(curvePresetLibraryType);

        AddCurve(scriptableObject, EasingFunctions.Linear, 2, "Linear");

        // In.
        AddCurve(scriptableObject, EasingFunctions.Quadratic.In, 15, "QuadIn");
        AddCurve(scriptableObject, EasingFunctions.Cubic.In, 15, "CubicIn");
        AddCurve(scriptableObject, EasingFunctions.Quartic.In, 15, "QuartIn");
        AddCurve(scriptableObject, EasingFunctions.Quintic.In, 15, "QuintIn");
        AddCurve(scriptableObject, EasingFunctions.Sinusoidal.In, 15, "SineIn");
        AddCurve(scriptableObject, EasingFunctions.Circular.In, 15, "CircIn");
        AddCurve(scriptableObject, EasingFunctions.Back.In, 30, "BackIn");
        AddCurve(scriptableObject, EasingFunctions.Exponential.In, 15, "ExpoIn");
        AddCurve(scriptableObject, EasingFunctions.Elastic.In, 30, "ElasticIn");
        AddCurve(scriptableObject, EasingFunctions.Bounce.In, 30, "BounceIn");

        // Out.
        AddCurve(scriptableObject, EasingFunctions.Quadratic.Out, 15, "QuadOut");
        AddCurve(scriptableObject, EasingFunctions.Cubic.Out, 15, "CubicOut");
        AddCurve(scriptableObject, EasingFunctions.Quartic.Out, 15, "QuartOut");
        AddCurve(scriptableObject, EasingFunctions.Quintic.Out, 15, "QuintOut");
        AddCurve(scriptableObject, EasingFunctions.Sinusoidal.Out, 15, "SineOut");
        AddCurve(scriptableObject, EasingFunctions.Circular.Out, 15, "CircOut");
        AddCurve(scriptableObject, EasingFunctions.Back.Out, 30, "BackOut");
        AddCurve(scriptableObject, EasingFunctions.Exponential.Out, 15, "ExpoOut");
        AddCurve(scriptableObject, EasingFunctions.Elastic.Out, 30, "ElasticOut");
        AddCurve(scriptableObject, EasingFunctions.Bounce.Out, 30, "BounceOut");

        // InOut.
        AddCurve(scriptableObject, EasingFunctions.Quadratic.InOut, 15, "QuadInOut");
        AddCurve(scriptableObject, EasingFunctions.Cubic.InOut, 15, "CubicInOut");
        AddCurve(scriptableObject, EasingFunctions.Quartic.InOut, 15, "QuartInOut");
        AddCurve(scriptableObject, EasingFunctions.Quintic.InOut, 15, "QuintInOut");
        AddCurve(scriptableObject, EasingFunctions.Sinusoidal.InOut, 15, "SineInOut");
        AddCurve(scriptableObject, EasingFunctions.Circular.InOut, 15, "CircInOut");
        AddCurve(scriptableObject, EasingFunctions.Back.InOut, 30, "BackInOut");
        AddCurve(scriptableObject, EasingFunctions.Exponential.InOut, 15, "ExpoInOut");
        AddCurve(scriptableObject, EasingFunctions.Elastic.InOut, 30, "ElasticInOut");
        AddCurve(scriptableObject, EasingFunctions.Bounce.InOut, 30, "BounceInOut");

        // OutIn.
        AddCurve(scriptableObject, EasingFunctions.Quadratic.OutIn, 15, "QuadOutIn");
        AddCurve(scriptableObject, EasingFunctions.Cubic.OutIn, 15, "CubicOutIn");
        AddCurve(scriptableObject, EasingFunctions.Quartic.OutIn, 15, "QuartOutIn");
        AddCurve(scriptableObject, EasingFunctions.Quintic.OutIn, 15, "QuintOutIn");
        AddCurve(scriptableObject, EasingFunctions.Sinusoidal.OutIn, 15, "SineOutIn");
        AddCurve(scriptableObject, EasingFunctions.Circular.OutIn, 15, "CircOutIn");
        AddCurve(scriptableObject, EasingFunctions.Back.OutIn, 30, "BackOutIn");
        AddCurve(scriptableObject, EasingFunctions.Exponential.OutIn, 15, "ExpoOutIn");
        AddCurve(scriptableObject, EasingFunctions.Elastic.OutIn, 30, "ElasticOutIn");
        AddCurve(scriptableObject, EasingFunctions.Bounce.OutIn, 30, "BounceOutIn");

        if( !AssetDatabase.IsValidFolder("Assets/Editor") )
        {
            AssetDatabase.CreateFolder("Assets", "Editor");
        }

        AssetDatabase.CreateAsset(scriptableObject, "Assets/Editor/EasingFunctions.curves");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static AnimationCurve GenerateCurve( EasingFunction easingFunction, int resolution )
    {
        AnimationCurve curve = new AnimationCurve();
        for( int i = 0; i < resolution; i++ )
        {
            float time = i / (resolution - 1f);
            float value = easingFunction(time, 0, 1, 1);
            Keyframe key = new Keyframe(time, value);
            curve.AddKey(key);
        }

        for( int i = 0; i < resolution; i++ )
        {
            curve.SmoothTangents(i, 0);
        }

        return curve;
    }

    private static void AddCurve( ScriptableObject scriptableObject, EasingFunction easingFunction, int resolution, string name )
    {
        Type curvePresetLibraryType = Type.GetType("UnityEditor.CurvePresetLibrary, UnityEditor");
        MethodInfo addMehtod = curvePresetLibraryType.GetMethod("Add");
        addMehtod.Invoke(scriptableObject, new object[]
        {
            GenerateCurve(easingFunction, resolution),
            name
        });
    }
}