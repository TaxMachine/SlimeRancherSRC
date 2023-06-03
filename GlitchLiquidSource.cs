// Decompiled with JetBrains decompiler
// Type: GlitchLiquidSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class GlitchLiquidSource : LiquidSource
{
  [Tooltip("FX objects that should be activated/deactivated based off the current state.")]
  public GameObject[] onAvailableFX;
  [Tooltip("SFX played when the station is consumed. (once, non-looping)")]
  public SECTR_AudioCue onConsumeCue;
  [Tooltip("FX spawned when the station is consumed.")]
  public GameObject onConsumeFX;
  private TutorialDirector tutorialDirector;

  public override void Awake()
  {
    base.Awake();
    if (!Application.isPlaying)
      return;
    tutorialDirector = SRSingleton<SceneContext>.Instance.TutorialDirector;
  }

  protected override void InitModel(LiquidSourceModel model)
  {
    base.InitModel(model);
    model.unitsFilled = 1f;
  }

  protected override void SetModel(LiquidSourceModel model)
  {
    base.SetModel(model);
    OnAvailabilityChanged();
  }

  private void OnAvailabilityChanged()
  {
    bool flag = Available();
    foreach (GameObject gameObject in onAvailableFX)
      gameObject.SetActive(flag);
  }

  public override bool Available() => base.Available() && model.unitsFilled > 0.0;

  public override void ConsumeLiquid()
  {
    base.ConsumeLiquid();
    model.unitsFilled = 0.0f;
    tutorialDirector.MaybeShowPopup(TutorialDirector.Id.SLIMULATIONS_DEBUG_SPRAY);
    SpawnAndPlayFX(onConsumeFX, transform.position, Quaternion.identity);
    SECTR_AudioSystem.Play(onConsumeCue, transform.position, false);
    OnAvailabilityChanged();
  }

  public override bool ReplacesExistingLiquidAmmo() => true;

  public void ResetLiquidState()
  {
    model.unitsFilled = 1f;
    OnAvailabilityChanged();
  }
}
