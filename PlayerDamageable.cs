// Decompiled with JetBrains decompiler
// Type: PlayerDamageable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlayerDamageable : SRBehaviour, Damageable
{
  private PlayerState playerState;
  private SECTR_AudioSource playerAudio;
  private ScreenShaker screenShaker;
  public SECTR_AudioCue damagedCue;
  private const float PER_DAMAGE_SCREEN_SHAKE = 0.2f;

  private void Start()
  {
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    playerAudio = GetComponent<SECTR_AudioSource>();
    screenShaker = GetComponent<ScreenShaker>();
  }

  public bool Damage(int healthLoss, GameObject source)
  {
    healthLoss = Mathf.RoundToInt(healthLoss * SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().playerDamageMultiplier);
    if (!playerState.CanBeDamaged())
      return false;
    SRSingleton<Overlay>.Instance.PlayDamage();
    playerAudio.Cue = GetDamageCue(source);
    playerAudio.Play();
    screenShaker.ShakeDamage(0.2f * healthLoss);
    return playerState.Damage(healthLoss, source);
  }

  private SECTR_AudioCue GetDamageCue(GameObject source) => source != null && Identifiable.GetId(source) == Identifiable.Id.GLITCH_TARR_SLIME ? SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.damageLossExposure.onExposedSFX : damagedCue;
}
