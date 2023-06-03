// Decompiled with JetBrains decompiler
// Type: DisableEffectsOnLowQuality
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class DisableEffectsOnLowQuality : MonoBehaviour
{
  private DepthTextureMode lastDepthMode;

  public void Awake() => CheckQuality();

  public void Update() => CheckQuality();

  private void CheckQuality()
  {
    SSAOPro component1 = GetComponent<SSAOPro>();
    if (component1.enabled != SRQualitySettings.AmbientOcclusion)
      component1.enabled = SRQualitySettings.AmbientOcclusion;
    Bloom component2 = GetComponent<Bloom>();
    if (component2.enabled != SRQualitySettings.Bloom)
      component2.enabled = SRQualitySettings.Bloom;
    DepthTextureMode depthTextureMode = SRQualitySettings.GetDepthTextureMode();
    if (depthTextureMode == lastDepthMode)
      return;
    GetComponent<Camera>().depthTextureMode = depthTextureMode;
    lastDepthMode = depthTextureMode;
  }
}
