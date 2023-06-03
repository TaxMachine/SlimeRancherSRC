// Decompiled with JetBrains decompiler
// Type: TweenUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class TweenUtil : MonoBehaviour
{
  private const float MIN_SCALE = 0.001f;

  public static Tweener ScaleIn(GameObject obj, float time, Ease easeType = Ease.OutQuad)
  {
    Vector3 fromValue = new Vector3(1f / 1000f, 1f / 1000f, 1f / 1000f);
    Vector3 localScale = obj.transform.localScale;
    if (obj.GetComponent<ScaleYOnlyMarker>() != null)
      fromValue = new Vector3(localScale.x, 1f / 1000f, localScale.z);
    else if (obj.GetComponent<ScaleZOnlyMarker>() != null)
      fromValue = new Vector3(localScale.x, localScale.y, 1f / 1000f);
    return obj.transform.DOScale(obj.transform.localScale, time).From(fromValue).SetEase(easeType);
  }

  public static Tweener ScaleOut(GameObject obj, float time, Ease easeType = Ease.InQuad)
  {
    Vector3 endValue = new Vector3(1f / 1000f, 1f / 1000f, 1f / 1000f);
    Vector3 localScale = obj.transform.localScale;
    if (obj.GetComponent<ScaleYOnlyMarker>() != null)
      endValue = new Vector3(localScale.x, 1f / 1000f, localScale.z);
    else if (obj.GetComponent<ScaleZOnlyMarker>() != null)
      endValue = new Vector3(localScale.x, localScale.y, 1f / 1000f);
    return obj.transform.DOScale(endValue, time).SetEase(easeType);
  }

  public static Tweener ScaleTo(GameObject obj, Vector3 scaleTo, float time, Ease easeType = Ease.InOutQuad) => obj.transform.DOScale(scaleTo, time).SetEase(easeType);
}
