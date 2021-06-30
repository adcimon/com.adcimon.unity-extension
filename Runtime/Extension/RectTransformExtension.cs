using UnityEngine;

public static class RectTransformExtension
{
    /// <summary>
    /// Gets the width of the rect transform.
    /// </summary>
    public static float GetWidth( this RectTransform rectTransform )
    {
        return rectTransform.rect.width;
    }

    /// <summary>
    /// Gets the height of the rect transform.
    /// </summary>
    public static float GetHeight( this RectTransform rectTransform )
    {
        return rectTransform.rect.height;
    }

    /// <summary>
    /// Gets the center of the rect transform.
    /// </summary>
    public static Vector2 GetCenter( this RectTransform rectTransform )
    {
        return rectTransform.rect.center;
    }

    /// <summary>
    /// Gets the size of the rect transform.
    /// </summary>
    public static Vector2 GetSize( this RectTransform rectTransform )
    {
        return rectTransform.rect.size;
    }

    /// <summary>
    /// Sets the width of the rect transform regardless of its anchors, pivot and offsets.
    /// </summary>
    public static void SetWidth( this RectTransform rectTransform, float width )
    {
        SetSize(rectTransform, new Vector2(width, rectTransform.rect.size.y));
    }

    /// <summary>
    /// Sets the height of the rect transform regardless of its anchors, pivot and offsets.
    /// </summary>
    public static void SetHeight( this RectTransform rectTransform, float height )
    {
        SetSize(rectTransform, new Vector2(rectTransform.rect.size.x, height));
    }

    /// <summary>
    /// Sets the size of the rect transform regardless of its anchors, pivot and offsets.
    /// </summary>
    public static void SetSize( this RectTransform rectTransform, float width, float height )
    {
        SetSize(rectTransform, new Vector2(width, height));
    }

    /// <summary>
    /// Sets the size of the rect transform regardless of its anchors, pivot and offsets.
    /// </summary>
    public static void SetSize( this RectTransform rectTransform, Vector2 size )
    {
        Vector2 oldSize = rectTransform.rect.size;
        Vector2 deltaSize = size - oldSize;
        rectTransform.offsetMin = rectTransform.offsetMin - new Vector2(deltaSize.x * rectTransform.pivot.x, deltaSize.y * rectTransform.pivot.y);
        rectTransform.offsetMax = rectTransform.offsetMax + new Vector2(deltaSize.x * (1 - rectTransform.pivot.x), deltaSize.y * (1 - rectTransform.pivot.y));
    }

    /// <summary>
    /// Sets the default scale of the rect transform.
    /// </summary>
    public static void SetDefaultScale( this RectTransform rectTransform )
    {
        rectTransform.localScale = Vector3.one;
    }

    /// <summary>
    /// Sets the point in which both anchors and the pivot should be placed. This makes it very easy to set positions and scales, but it destroys autoscaling.
    /// </summary>
    public static void SetPivotAndAnchors( this RectTransform rectTransform, Vector2 point )
    {
        rectTransform.pivot = point;
        rectTransform.anchorMin = point;
        rectTransform.anchorMax = point;
    }

    /// <summary>
    /// Sets the position of the rect transform within it's parent's coordinates. Depending on the position of the pivot, the rect transform actual position will differ.
    /// </summary>
    public static void SetPivotPosition( this RectTransform rectTransform, Vector2 position )
    {
        rectTransform.localPosition = new Vector3(position.x, position.y, rectTransform.localPosition.z);
    }

    /// <summary>
    /// Sets the position of the bottom left corner of the rect transform within it's parent's coordinates.
    /// </summary>
    public static void SetBottomLeftPosition( this RectTransform rectTransform, Vector2 position )
    {
        rectTransform.localPosition = new Vector3(position.x + (rectTransform.pivot.x * rectTransform.rect.width), position.y + (rectTransform.pivot.y * rectTransform.rect.height), rectTransform.localPosition.z);
    }

    /// <summary>
    /// Sets the position of the top left corner of the rect transform within it's parent's coordinates.
    /// </summary>
    public static void SetTopLeftPosition( this RectTransform rectTransform, Vector2 position )
    {
        rectTransform.localPosition = new Vector3(position.x + (rectTransform.pivot.x * rectTransform.rect.width), position.y - ((1 - rectTransform.pivot.y) * rectTransform.rect.height), rectTransform.localPosition.z);
    }

    /// <summary>
    /// Sets the position of the bottom right corner of the rect transform within it's parent's coordinates.
    /// </summary>
    public static void SetBottomRightPosition( this RectTransform rectTransform, Vector2 position )
    {
        rectTransform.localPosition = new Vector3(position.x - ((1 - rectTransform.pivot.x) * rectTransform.rect.width), position.y + (rectTransform.pivot.y * rectTransform.rect.height), rectTransform.localPosition.z);
    }

    /// <summary>
    /// Sets the position of the top right corner of the rect transform within it's parent's coordinates.
    /// </summary>
    public static void SetTopRightPosition( this RectTransform rectTransform, Vector2 position )
    {
        rectTransform.localPosition = new Vector3(position.x - ((1 - rectTransform.pivot.x) * rectTransform.rect.width), position.y - ((1 - rectTransform.pivot.y) * rectTransform.rect.height), rectTransform.localPosition.z);
    }
}