// Decompiled with JetBrains decompiler
// Type: GlitchTarrNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.DataModel;
using System;
using UnityEngine;

public class GlitchTarrNode : IdHandler<GlitchTarrNodeModel>
{
  [Tooltip("Tarr node activation group minor index.")]
  [Range(0.0f, 10f)]
  public int activationIndex;
  private TimeDirector timeDirector;
  private GlitchCellDirector cellDirector;
  private GlitchTarrNodeModel model;
  private GlitchMetadata metadata;
  private Tween tween;

  public Group activationGroup => cellDirector.tarrActivationGroup;

  public Vector3 scale { get; private set; }

  public override void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    cellDirector = GetRequiredComponentInParent<GlitchCellDirector>();
    metadata = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    scale = transform.localScale;
    base.Awake();
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!(timeDirector != null))
      return;
    timeDirector.RemovePassedTimeDelegate(OnActivationStateChanged);
    timeDirector = null;
  }

  protected override string IdPrefix() => "glitchTN";

  protected override GameModel.Unregistrant Register(GameModel game) => game.Glitch.nodes.Register(this);

  protected override void InitModel(GlitchTarrNodeModel model) => model.activationTime = 0.0;

  protected override void SetModel(GlitchTarrNodeModel model)
  {
    this.model = model;
    ResetNode(this.model.activationTime);
  }

  public void ResetNode(double activationTime)
  {
    model.activationTime = activationTime;
    OnActivationStateChanged();
    if (timeDirector.HasReached(activationTime))
      return;
    timeDirector.RemovePassedTimeDelegate(OnActivationStateChanged);
    timeDirector.AddPassedTimeDelegate(activationTime, OnActivationStateChanged);
  }

  public State GetState()
  {
    if (!timeDirector.HasReached(model.activationTime))
      return State.INACTIVE;
    return tween != null && tween.IsActive() && tween.IsPlaying() ? State.ACTIVATING : State.ACTIVE;
  }

  private void OnActivationStateChanged()
  {
    Tween tween = this.tween;
    if (tween != null)
      tween.Kill();
    this.tween = null;
    bool flag = timeDirector.HasReached(model.activationTime);
    gameObject.SetActive(flag);
    if (!flag)
      return;
    this.tween = transform.DOScale(scale, metadata.tarrNodeScaleInSpeed).From(scale * 0.2f).SetEase(Ease.Linear).SetSpeedBased(true);
  }

  public enum State
  {
    INACTIVE,
    ACTIVATING,
    ACTIVE,
  }

  public enum Group
  {
    A,
    B,
  }
}
