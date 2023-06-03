// Decompiled with JetBrains decompiler
// Type: GadgetDashPad
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections;
using Assets.Script.Util.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class GadgetDashPad : ControllerCollisionListenerBehaviour
{
  [Tooltip("FX played when the dash pad is activated.")]
  public GameObject onActivationFX;
  [Tooltip("Time before energy begins being depleted again. (in-game minutes)")]
  public float activationDuration;
  [Tooltip("2D HUD overlay properties.")]
  public HudFX hudFX;
  private TimeDirector timeDirector;
  private PlayerModel player;
  private const float COOLDOWN_DURATION = 0.75f;
  private float activationTime;

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    player = SRSingleton<SceneContext>.Instance.GameModel.GetPlayerModel();
  }

  protected override void OnControllerCollisionEntered()
  {
    base.OnControllerCollisionEntered();
    player.runEnergyDepletionTime = double.MaxValue;
    FXHelper.OnRunEnergyDepletionTimeChanged(hudFX);
    if (!(onActivationFX != null))
      return;
    SpawnAndPlayFX(onActivationFX, gameObject, Vector3.zero, Quaternion.identity);
  }

  protected override void OnControllerCollisionExited()
  {
    base.OnControllerCollisionExited();
    activationTime = Time.time + 0.75f;
    player.runEnergyDepletionTime = timeDirector.HoursFromNow(activationDuration * 0.0166666675f);
    FXHelper.OnRunEnergyDepletionTimeChanged(hudFX);
  }

  protected override bool Predicate(GameObject collision) => collision == SRSingleton<SceneContext>.Instance.Player && Time.time >= (double) activationTime;

  [Serializable]
  public class HudFX
  {
    [Tooltip("SFX played during the activation of the HUD overlay. (2D, looping")]
    public SECTR_AudioCue onActiveSFX;
    [Tooltip("SFX played at the deactivation of the HUD overlay. (2D, non-looping")]
    public SECTR_AudioCue onDeactivatedSFX;
  }

  private class FXHelper : SRSingleton<FXHelper>
  {
    private const float FX_FADE_TIME_IN = 0.25f;
    private const float FX_FADE_TIME_OUT = 1f;
    private GameObject overlay;
    private GameObject meter;
    private float alpha;
    private SECTR_AudioCueInstance onActiveSFXInstance;
    private SECTR_AudioCue onDeactivatedSFX;
    private Tween tween;

    public static void OnRunEnergyDepletionTimeChanged(HudFX args) => SRSingleton<SceneContext>.Instance.StartCoroutine(OnRunEnergyDepletionTimeChanged_Coroutine(args));

    private static IEnumerator OnRunEnergyDepletionTimeChanged_Coroutine(HudFX args)
    {
      TimeDirector timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
      double time = SRSingleton<SceneContext>.Instance.GameModel.GetPlayerModel().runEnergyDepletionTime;
      if (Instance == null)
      {
        GameObject gameObject = new GameObject("GadgetDashPad.FXHelper");
        gameObject.transform.SetParent(SRSingleton<DynamicObjectContainer>.Instance.transform);
        gameObject.AddComponent<FXHelper>();
        Instance.onDeactivatedSFX = args.onDeactivatedSFX;
        Instance.onActiveSFXInstance = SECTR_AudioSystem.Play(args.onActiveSFX, Vector3.zero, true);
      }
      DestroyAfterTime destroyAfterTime = Instance.gameObject.GetComponent<DestroyAfterTime>() ?? Instance.gameObject.AddComponent<DestroyAfterTime>();
      destroyAfterTime.SetDeathTime(time);
      destroyAfterTime.destroyAsActor = false;
      Tween tween = Instance.tween;
      if (tween != null)
        tween.Kill();
      yield return new WaitForEndOfFrame();
      float interval = (float) ((time - timeDirector.WorldTime()) * 0.01666666753590107) - 1.25f;
      Instance.tween = DOTween.Sequence().Append(DOTween.To(() => Instance.alpha, Instance.SetFXAlpha, 1f, 0.25f)).AppendInterval(interval).Append(DOTween.To(() => Instance.alpha, Instance.SetFXAlpha, 0.0f, 1f).OnStart(() => Instance.OnFadeOutStart()));
    }

    public override void Awake()
    {
      base.Awake();
      overlay = SRSingleton<Overlay>.Instance.Play(SRSingleton<Overlay>.Instance.dashPadFX);
      meter = SRSingleton<HudUI>.Instance.energyMeter.Play(SRSingleton<HudUI>.Instance.energyMeter.dashPadFX);
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      Destroyer.Destroy(overlay, "GadgetDashPad.FXHelper.OnDestroy");
      Destroyer.Destroy(meter, "GadgetDashPad.FXHelper.OnDestroy");
      if (tween != null)
        tween.Kill();
      onActiveSFXInstance.Stop(true);
    }

    private void SetFXAlpha(float alpha)
    {
      this.alpha = alpha;
      Renderer requiredComponent1 = overlay.GetRequiredComponent<Renderer>();
      requiredComponent1.material.color = GetColor(requiredComponent1.material.color, this.alpha);
      Image requiredComponent2 = meter.GetRequiredComponent<Image>();
      requiredComponent2.color = GetColor(requiredComponent2.color, this.alpha);
    }

    private void OnFadeOutStart()
    {
      SECTR_AudioSystem.Play(onDeactivatedSFX, Vector3.zero, false);
      onActiveSFXInstance.Stop(false);
    }

    private static Color GetColor(Color color, float alpha) => new Color(color.r, color.g, color.b, alpha);
  }
}
