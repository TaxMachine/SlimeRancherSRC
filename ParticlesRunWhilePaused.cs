// Decompiled with JetBrains decompiler
// Type: ParticlesRunWhilePaused
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ParticlesRunWhilePaused : MonoBehaviour
{
  private ParticleSystem uiParticleSystem;

  public void Awake() => uiParticleSystem = GetComponent<ParticleSystem>();

  public void Update()
  {
    if (Time.timeScale >= 0.0099999997764825821)
      return;
    uiParticleSystem.Simulate(Time.unscaledDeltaTime, false, false);
    uiParticleSystem.Play();
  }
}
