// Decompiled with JetBrains decompiler
// Type: UIAudioSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIAudioSource : SRBehaviour
{
  public SECTR_AudioCue onEnable;
  public bool skipOnEnableIfPaused;

  public void OnEnable()
  {
    if (!(onEnable != null) || skipOnEnableIfPaused && Time.timeScale <= 0.0)
      return;
    SECTR_AudioSystem.Play(onEnable, SECTR_AudioSystem.Listener, Vector3.zero, false);
  }

  public void PlayClick() => SECTR_AudioSystem.Play(SRSingleton<GameContext>.Instance.UITemplates.clickCue, SECTR_AudioSystem.Listener, Vector3.zero, false);

  public void PlayPurchase() => SECTR_AudioSystem.Play(SRSingleton<GameContext>.Instance.UITemplates.purchaseCue, SECTR_AudioSystem.Listener, Vector3.zero, false);

  public void PlayPurchaseExpansion() => SECTR_AudioSystem.Play(SRSingleton<GameContext>.Instance.UITemplates.purchaseExpansionCue, SECTR_AudioSystem.Listener, Vector3.zero, false);

  public void PlayPurchasePlot() => SECTR_AudioSystem.Play(SRSingleton<GameContext>.Instance.UITemplates.purchasePlotCue, SECTR_AudioSystem.Listener, Vector3.zero, false);

  public void PlayPurchaseUpgrade() => SECTR_AudioSystem.Play(SRSingleton<GameContext>.Instance.UITemplates.purchaseUpgradeCue, SECTR_AudioSystem.Listener, Vector3.zero, false);

  public void PlayPurchasePersonalUpgrade() => SECTR_AudioSystem.Play(SRSingleton<GameContext>.Instance.UITemplates.purchasePersonalUpgradeCue, SECTR_AudioSystem.Listener, Vector3.zero, false);
}
