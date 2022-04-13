using UnityEngine;
using UnityEngine.UI;

public static class UIExt
{
    public static void SetHeight(this RectTransform value,float height)
    {
        value.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,height);
        value.ForceUpdateRectTransforms();
    }

    public static void SetWidth(this RectTransform value,float width)
    {
        value.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,width);
        value.ForceUpdateRectTransforms();
    }

    public static void SetAlpha(this Image value,float alpha)
    {
        value.color = new Color(value.color.r,value.color.g,value.color.b,Mathf.Clamp01(alpha));
    }

    public static Color GetColorWithAlpha(this Image value,float alpha)
    {
        return new Color(value.color.r,value.color.g,value.color.b,Mathf.Clamp01(alpha));
    }

    public static void SetAlpha(this Text value,float alpha)
    {
        value.color = new Color(value.color.r,value.color.g,value.color.b,Mathf.Clamp01(alpha));
    }

    public static Color GetColorWithAlpha(this Text value,float alpha)
    {
        return new Color(value.color.r,value.color.g,value.color.b,Mathf.Clamp01(alpha));
    }

    public static void SetAlpha(this SpriteRenderer value,float alpha)
    {
        value.color = new Color(value.color.r,value.color.g,value.color.b,Mathf.Clamp01(alpha));
    }

    public static Color GetColorWithAlpha(this SpriteRenderer value,float alpha)
    {
        return new Color(value.color.r,value.color.g,value.color.b,Mathf.Clamp01(alpha));
    }

    public static void SetAlpha(this TMPro.TMP_Text value,float alpha)
    {
        value.color = new Color(value.color.r,value.color.g,value.color.b,Mathf.Clamp01(alpha));
    }

    public static Color GetColorWithAlpha(this TMPro.TMP_Text value,float alpha)
    {
        return new Color(value.color.r,value.color.g,value.color.b,Mathf.Clamp01(alpha));
    }

    public static void SetAlpha(this UnityEngine.U2D.SpriteShapeRenderer value,float alpha)
    {
        value.color = new Color(value.color.r,value.color.g,value.color.b,Mathf.Clamp01(alpha));
    }

    public static Color GetColorWithAlpha(this UnityEngine.U2D.SpriteShapeRenderer value,float alpha)
    {
        return new Color(value.color.r,value.color.g,value.color.b,Mathf.Clamp01(alpha));
    }
}
