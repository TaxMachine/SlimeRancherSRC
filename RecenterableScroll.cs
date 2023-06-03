// Decompiled with JetBrains decompiler
// Type: RecenterableScroll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class RecenterableScroll : MonoBehaviour
{
  public float border = 8f;
  private ScrollRect scroll;
  private RectTransform scrollTransform;

  private void Awake()
  {
    scroll = GetComponent<ScrollRect>();
    scrollTransform = GetComponent<RectTransform>();
  }

  public void ScrollToItem(RectTransform itemTransform)
  {
    Vector3[] fourCornersArray1 = new Vector3[4];
    scrollTransform.GetWorldCorners(fourCornersArray1);
    Vector3[] fourCornersArray2 = new Vector3[4];
    scroll.content.GetWorldCorners(fourCornersArray2);
    Vector3[] fourCornersArray3 = new Vector3[4];
    itemTransform.GetWorldCorners(fourCornersArray3);
    float num1 = fourCornersArray2[1].y - fourCornersArray2[0].y;
    float num2 = fourCornersArray1[1].y - fourCornersArray1[0].y;
    float num3 = fourCornersArray3[0].y - fourCornersArray2[0].y - border;
    double num4 = fourCornersArray3[1].y - (double) fourCornersArray2[0].y + border;
    float num5 = Mathf.Clamp01(num3 / (num1 - num2));
    double num6 = num2;
    float num7 = Mathf.Clamp01((float) ((num4 - num6) / (num1 - (double) num2)));
    if (num7 > (double) scroll.verticalNormalizedPosition)
      scroll.verticalNormalizedPosition = num7;
    else if (num5 < (double) scroll.verticalNormalizedPosition)
      scroll.verticalNormalizedPosition = num5;
    float num8 = fourCornersArray2[2].x - fourCornersArray2[0].x;
    float num9 = fourCornersArray1[2].x - fourCornersArray1[0].x;
    float num10 = fourCornersArray3[0].x - fourCornersArray2[0].x - border;
    double num11 = fourCornersArray3[2].x - (double) fourCornersArray2[0].x + border;
    float num12 = Mathf.Clamp01(num10 / (num8 - num9));
    double num13 = num9;
    float num14 = Mathf.Clamp01((float) ((num11 - num13) / (num8 - (double) num9)));
    if (num14 > (double) scroll.horizontalNormalizedPosition)
    {
      scroll.horizontalNormalizedPosition = num14;
    }
    else
    {
      if (num12 >= (double) scroll.horizontalNormalizedPosition)
        return;
      scroll.horizontalNormalizedPosition = num12;
    }
  }
}
