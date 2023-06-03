// Decompiled with JetBrains decompiler
// Type: UnityEngine.UI.RadialLayoutGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace UnityEngine.UI
{
  public class RadialLayoutGroup : LayoutGroup
  {
    [Tooltip("Width/height of the layout child.")]
    public Vector2 childSize;
    [Tooltip("Radius to spread the layout children from the center.")]
    public float radius;
    [Tooltip("Angular offset, in degrees.")]
    public float offset;

    public Transform GetChild(float radians)
    {
      Transform child = null;
      if (rectChildren.Count > 0)
      {
        Vector2 vector2 = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * radius;
        float num = float.MaxValue;
        foreach (Transform rectChild in rectChildren)
        {
          float sqrMagnitude = ((Vector2) rectChild.localPosition - vector2).sqrMagnitude;
          if (sqrMagnitude < (double) num)
          {
            num = sqrMagnitude;
            child = rectChild;
          }
        }
      }
      return child;
    }

    public override void CalculateLayoutInputHorizontal()
    {
      base.CalculateLayoutInputHorizontal();
      SetLayoutInputForAxis(childSize.x, childSize.x, childSize.x, 0);
    }

    public override void CalculateLayoutInputVertical() => SetLayoutInputForAxis(childSize.y, childSize.y, childSize.y, 1);

    public override void SetLayoutHorizontal() => SetLayoutAlongAxis(0);

    public override void SetLayoutVertical() => SetLayoutAlongAxis(1);

    private void SetLayoutAlongAxis(int axis)
    {
      if (rectChildren.Count <= 0)
        return;
      float num = 6.28318548f / rectChildren.Count;
      float f = offset * ((float) Math.PI / 180f);
      foreach (RectTransform rectChild in rectChildren)
      {
        SetChildAlongAxis(rectChild, axis, (axis == 0 ? Mathf.Cos(f) : Mathf.Sin(f)) * radius, rectTransform.rect.size[axis]);
        f += num;
      }
    }
  }
}
