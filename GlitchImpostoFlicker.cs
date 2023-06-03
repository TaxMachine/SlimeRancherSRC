// Decompiled with JetBrains decompiler
// Type: GlitchImpostoFlicker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using System.Linq;
using UnityEngine;

public class GlitchImpostoFlicker : SRBehaviour
{
  private TimeDirector timeDirector;
  private GlitchMetadata metadata;
  private bool hasStarted;
  private Tween tween;
  private Vector3[] path_cache;

  public void Awake()
  {
    if (!Application.isPlaying)
      return;
    metadata = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
  }

  public void OnEnable()
  {
    if (!Application.isPlaying || !hasStarted)
      return;
    ResetNextFlickerTime();
  }

  public void Start()
  {
    if (!Application.isPlaying)
      return;
    hasStarted = true;
    OnEnable();
  }

  public void OnDisable()
  {
    if (!Application.isPlaying)
      return;
    timeDirector.RemovePassedTimeDelegate(OnTimeReached);
  }

  private void ResetNextFlickerTime() => timeDirector.AddPassedTimeDelegate(timeDirector.HoursFromNow(metadata.impostoFlickerCooldownTime.GetRandom() * 0.0166666675f), OnTimeReached);

  private void OnTimeReached()
  {
    if (metadata.impostoFlickerFX != null)
      SpawnAndPlayFX(metadata.impostoFlickerFX, gameObject);
    if (metadata.impostoFlickerCue != null)
      SECTR_AudioSystem.Play(metadata.impostoFlickerCue, transform.position, false);
    Tween tween = this.tween;
    if (tween != null)
      tween.Kill(true);
    this.tween = transform.DOPath(FlickerPath, metadata.impostoFlickerSpeed.GetRandom()).SetEase(Ease.Linear).SetSpeedBased(true);
    ResetNextFlickerTime();
  }

  private Vector3[] FlickerPath
  {
    get
    {
      if (path_cache == null)
        path_cache = Enumerable.Range(0, metadata.impostoFlickerPoints).Select(ii => Quaternion.Euler(0.0f, Randoms.SHARED.GetInRange(0, 360), 0.0f) * Vector3.forward * metadata.impostoFlickerRadius.GetRandom() + gameObject.transform.position).Concat(gameObject.transform.position.ToEnumerable()).ToArray();
      return path_cache;
    }
  }
}
