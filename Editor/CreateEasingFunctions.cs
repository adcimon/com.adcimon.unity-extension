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
        AddCurve(scriptableObject, EasingFunctions.Quadratic.In, 15, "Quadratic In");
        AddCurve(scriptableObject, EasingFunctions.Cubic.In, 15, "Cubic In");
        AddCurve(scriptableObject, EasingFunctions.Quartic.In, 15, "Quartic In");
        AddCurve(scriptableObject, EasingFunctions.Quintic.In, 15, "Quintic In");
        AddCurve(scriptableObject, EasingFunctions.Sinusoidal.In, 15, "Sinusoidal In");
        AddCurve(scriptableObject, EasingFunctions.Circular.In, 15, "Circular In");
        AddCurve(scriptableObject, EasingFunctions.Back.In, 30, "Back In");
        AddCurve(scriptableObject, EasingFunctions.Exponential.In, 15, "Exponential In");
        AddCurve(scriptableObject, EasingFunctions.Elastic.In, 30, "Elastic In");
        AddCurve(scriptableObject, EasingFunctions.Bounce.In, 30, "Bounce In");

        // Out.
        AddCurve(scriptableObject, EasingFunctions.Quadratic.Out, 15, "Quadratic Out");
        AddCurve(scriptableObject, EasingFunctions.Cubic.Out, 15, "CubicOut");
        AddCurve(scriptableObject, EasingFunctions.Quartic.Out, 15, "Quartic Out");
        AddCurve(scriptableObject, EasingFunctions.Quintic.Out, 15, "Quintic Out");
        AddCurve(scriptableObject, EasingFunctions.Sinusoidal.Out, 15, "Sinusoidal Out");
        AddCurve(scriptableObject, EasingFunctions.Circular.Out, 15, "Circular Out");
        AddCurve(scriptableObject, EasingFunctions.Back.Out, 30, "Back Out");
        AddCurve(scriptableObject, EasingFunctions.Exponential.Out, 15, "Exponential Out");
        AddCurve(scriptableObject, EasingFunctions.Elastic.Out, 30, "Elastic Out");
        AddCurve(scriptableObject, EasingFunctions.Bounce.Out, 30, "Bounce Out");

        // InOut.
        AddCurve(scriptableObject, EasingFunctions.Quadratic.InOut, 15, "Quadratic InOut");
        AddCurve(scriptableObject, EasingFunctions.Cubic.InOut, 15, "Cubic InOut");
        AddCurve(scriptableObject, EasingFunctions.Quartic.InOut, 15, "Quartic InOut");
        AddCurve(scriptableObject, EasingFunctions.Quintic.InOut, 15, "Quintic InOut");
        AddCurve(scriptableObject, EasingFunctions.Sinusoidal.InOut, 15, "Sinusoidal InOut");
        AddCurve(scriptableObject, EasingFunctions.Circular.InOut, 15, "Circular InOut");
        AddCurve(scriptableObject, EasingFunctions.Back.InOut, 30, "Back InOut");
        AddCurve(scriptableObject, EasingFunctions.Exponential.InOut, 15, "Exponential InOut");
        AddCurve(scriptableObject, EasingFunctions.Elastic.InOut, 30, "Elastic InOut");
        AddCurve(scriptableObject, EasingFunctions.Bounce.InOut, 30, "Bounce InOut");

        // OutIn.
        AddCurve(scriptableObject, EasingFunctions.Quadratic.OutIn, 15, "Quadratic OutIn");
        AddCurve(scriptableObject, EasingFunctions.Cubic.OutIn, 15, "Cubic OutIn");
        AddCurve(scriptableObject, EasingFunctions.Quartic.OutIn, 15, "Quartic OutIn");
        AddCurve(scriptableObject, EasingFunctions.Quintic.OutIn, 15, "Quintic OutIn");
        AddCurve(scriptableObject, EasingFunctions.Sinusoidal.OutIn, 15, "Sinusoidal OutIn");
        AddCurve(scriptableObject, EasingFunctions.Circular.OutIn, 15, "Circular OutIn");
        AddCurve(scriptableObject, EasingFunctions.Back.OutIn, 30, "Back OutIn");
        AddCurve(scriptableObject, EasingFunctions.Exponential.OutIn, 15, "Exponential OutIn");
        AddCurve(scriptableObject, EasingFunctions.Elastic.OutIn, 30, "Elastic OutIn");
        AddCurve(scriptableObject, EasingFunctions.Bounce.OutIn, 30, "Bounce OutIn");

        if( !AssetDatabase.IsValidFolder("Assets/Editor") )
        {
            AssetDatabase.CreateFolder("Assets", "Editor");
        }

        AssetDatabase.CreateAsset(scriptableObject, "Assets/Editor/EasingFunctions.curves");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static AnimationCurve GenerateCurve( EasingFunction easingFunction, int resolution )
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