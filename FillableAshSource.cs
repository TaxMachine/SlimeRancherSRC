// Decompiled with JetBrains decompiler
// Type: FillableAshSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class FillableAshSource : AshSource, LandPlotModel.Participant
{
  [Tooltip("The maximum number of consumable units this can hold.")]
  public int maxUnits = 20;
  public float minYPos = 0.05f;
  private float ashFillSpeed = 0.25f;
  private Vector3 initPos;
  private LandPlotModel plotModel;
  private Tween ashMoveTween;
  private Rigidbody body;

  public override void Awake()
  {
    allAshes.Add(this);
    body = GetComponent<Rigidbody>();
    initPos = transform.localPosition;
    UpdateAshPosition();
  }

  public void InitModel(LandPlotModel model) => model.ashUnits = maxUnits;

  public void SetModel(LandPlotModel model)
  {
    plotModel = model;
    UpdateAshPosition();
  }

  public override bool Available() => plotModel.ashUnits >= 1.0;

  public override void ConsumeAsh()
  {
    plotModel.ashUnits = Mathf.Max(plotModel.ashUnits - 1f, 0.0f);
    UpdateAshPosition();
  }

  public void AddAsh(float amount)
  {
    plotModel.ashUnits = Mathf.Min(plotModel.ashUnits + amount, maxUnits);
    UpdateAshPosition();
  }

  private void UpdateAshPosition()
  {
    if (plotModel == null || !gameObject.activeInHierarchy)
      return;
    Tween ashMoveTween = this.ashMoveTween;
    if (ashMoveTween != null)
      ashMoveTween.Kill();
    this.ashMoveTween = body.DOMoveY(GetAshYPosition(), ashFillSpeed).SetSpeedBased(true);
  }

  private float GetAshYPosition() => transform.parent.TransformPoint(0.0f, Mathf.Max(initPos.y * (plotModel.ashUnits / maxUnits), minYPos), 0.0f).y;

  public float GetAshSpace() => maxUnits - plotModel.ashUnits;
}
