// Decompiled with JetBrains decompiler
// Type: PauseTweenOnDisable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using UnityEngine;

public class PauseTweenOnDisable : MonoBehaviour
{
  public Tween tween;

  public void OnEnable()
  {
    if (tween == null || !tween.IsActive() || tween.IsComplete())
      return;
    tween.Play();
  }

  public void OnDisable()
  {
    if (tween == null || !tween.IsActive() || tween.IsComplete())
      return;
    tween.Pause();
  }
}
