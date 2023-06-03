// Decompiled with JetBrains decompiler
// Type: ReflectionProbeRefreshTimer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ReflectionProbeRefreshTimer : MonoBehaviour
{
  public float delayBetweenUpdateSecs = 10f;
  private ReflectionProbe rProbe;
  private float nextUpdateTime;

  public void Start() => rProbe = GetComponent<ReflectionProbe>();

  public void Update()
  {
    if (Time.time < (double) nextUpdateTime)
      return;
    nextUpdateTime = Time.time + delayBetweenUpdateSecs;
    rProbe.RenderProbe();
  }
}
