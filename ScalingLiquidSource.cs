// Decompiled with JetBrains decompiler
// Type: ScalingLiquidSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class ScalingLiquidSource : LiquidSource, LiquidConsumer
{
  [Tooltip("The object to scale based on our fullness")]
  public Transform toScale;
  [Tooltip("The maximum number of consumable units this can hold.")]
  public int maxUnits = 20;
  [Tooltip("The amount of liquid to refill each game hour.")]
  public float refillUnitsPerGameHour = 0.25f;
  private TimeDirector timeDir;
  private AmbianceDirector ambianceDir;
  private Vector3 initScale;

  public override void Awake()
  {
    base.Awake();
    if (!Application.isPlaying)
      return;
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    ambianceDir = SRSingleton<SceneContext>.Instance.AmbianceDirector;
    initScale = toScale.localScale;
  }

  protected override void InitModel(LiquidSourceModel model)
  {
    base.InitModel(model);
    model.isScaling = true;
    model.unitsFilled = maxUnits;
  }

  public override bool Available() => model.unitsFilled >= 1.0;

  public override void ConsumeLiquid() => --model.unitsFilled;

  public void AddLiquid(Identifiable.Id liquidId, float amount)
  {
    if (liquidId != this.liquidId)
      return;
    ++model.unitsFilled;
  }

  public void Update()
  {
    if (!Application.isPlaying)
      return;
    model.unitsFilled = Mathf.Min(maxUnits, model.unitsFilled + (float) ((refillUnitsPerGameHour + (double) ambianceDir.PrecipitationRate()) * timeDir.DeltaWorldTime() * 0.00027777778450399637));
    toScale.localScale = new Vector3(initScale.x, initScale.y * (model.unitsFilled / maxUnits), initScale.z);
  }
}
