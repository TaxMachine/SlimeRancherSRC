// Decompiled with JetBrains decompiler
// Type: GlitchTarrNodeMusic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using UnityEngine;

public class GlitchTarrNodeMusic : SECTR_PointSource
{
  private const int MIN_DISTANCE = 10;
  private const int MAX_DISTANCE = 20;
  private float minDistance;
  private float maxDistance;
  private float maxVolume;

  protected override void Start()
  {
    base.Start();
    if (!Application.isPlaying)
      return;
    maxVolume = Random.Range(Cue.Volume.x, Cue.Volume.y);
    GlitchTarrNode componentInParent = gameObject.GetRequiredComponentInParent<GlitchTarrNode>();
    minDistance = 10f * componentInParent.scale.x;
    maxDistance = 20f * componentInParent.scale.x;
    minDistance *= minDistance;
    maxDistance *= maxDistance;
  }

  protected void Update()
  {
    if (!Application.isPlaying || !IsPlaying)
      return;
    instance.Volume = maxVolume * GetCurrentMultiplier();
  }

  private float GetCurrentMultiplier()
  {
    float sqrMagnitude = (SRSingleton<SceneContext>.Instance.Player.transform.position - transform.position).sqrMagnitude;
    if (sqrMagnitude <= (double) minDistance)
      return 1f;
    return sqrMagnitude >= (double) maxDistance ? 0.0f : (float) (1.0 - (sqrMagnitude - (double) minDistance) / (maxDistance - (double) minDistance));
  }
}
