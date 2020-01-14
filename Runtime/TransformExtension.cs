using UnityEngine;

public static class TransformExtension
{
    /// <summary>
    /// Resets the transform world position, rotation and scale.
    /// </summary>
    public static void Reset( this Transform transform )
    {
        Transform parent = transform.parent;
        transform.parent = null;
        transform.ResetLocal();
        transform.parent = parent;
    }

    /// <summary>
    /// Resets the transform local position, rotation and scale.
    /// </summary>
    public static void ResetLocal( this Transform transform )
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Enables all the children of the transform recursively.
    /// </summary>
    public static void EnableChildren( this Transform transform )
    {
        for( int i = 0; i < transform.childCount; i++ )
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Disables all the children of the transform recursively.
    /// </summary>
    public static void DisableChildren( this Transform transform )
    {
        for( int i = 0; i < transform.childCount; i++ )
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Destroys all the children of the transform recursively.
    /// </summary>
    public static void DestroyChildren( this Transform transform )
    {
        for( int i = 0; i < transform.childCount; i++ )
        {
            MonoBehaviour.Destroy(transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Destroys all the children of the transform recursively after the specified seconds.
    /// </summary>
    public static void DestroyChildren( this Transform transform, float time )
    {
        for( int i = 0; i < transform.childCount; i++ )
        {
            MonoBehaviour.Destroy(transform.GetChild(i).gameObject, time);
        }
    }
}