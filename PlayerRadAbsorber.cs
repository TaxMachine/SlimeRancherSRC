// Decompiled with JetBrains decompiler
// Type: PlayerRadAbsorber
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlayerRadAbsorber : SRBehaviour
{
  public SECTR_PointSource radAudio;
  private PlayerState playerState;
  private PlayerDamageable damageable;
  private bool absorbingThisFrame;

  public void Awake()
  {
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    damageable = GetComponent<PlayerDamageable>();
  }

  public void FixedUpdate()
  {
    SRSingleton<Overlay>.Instance.SetEnableRad(absorbingThisFrame);
    if (absorbingThisFrame && !radAudio.IsPlaying)
      radAudio.Play();
    else if (!absorbingThisFrame && radAudio.IsPlaying)
      radAudio.Stop(false);
    absorbingThisFrame = false;
  }

  public void Absorb(GameObject source, float rads)
  {
    int healthLoss = playerState.AddRads(rads);
    if (healthLoss > 0 && damageable.Damage(healthLoss, null))
      DeathHandler.Kill(gameObject, DeathHandler.Source.SLIME_RAD, source, "PlayerRadAbsorber.Absorb");
    absorbingThisFrame = true;
  }
}
