// Decompiled with JetBrains decompiler
// Type: GotoWater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class GotoWater : FindConsumable
{
  private DestroyOnTouching nonWater;
  private Vector3? tgtLoc;
  private const float GAME_MINS_THRESHOLD = 20f;
  private const float SEARCH_RAD = 30f;
  private const float SEARCH_RAD_SQR = 900f;

  public override void Start()
  {
    base.Start();
    nonWater = GetComponent<DestroyOnTouching>();
  }

  public override float Relevancy(bool isGrounded)
  {
    if (nonWater == null)
      return 0.0f;
    FindNearestWaterLoc();
    return !tgtLoc.HasValue ? 0.0f : 1f - nonWater.PctTimeToDestruct();
  }

  public override void Selected()
  {
  }

  private void FindNearestWaterLoc()
  {
    float num = 900f;
    LiquidSourceModel liquidSourceModel = null;
    foreach (LiquidSourceModel instance in SRSingleton<SceneContext>.Instance.GameModel.LiquidSources.Instances)
    {
      float sqrMagnitude = (instance.pos - transform.position).sqrMagnitude;
      if (sqrMagnitude < (double) num)
      {
        liquidSourceModel = instance;
        num = sqrMagnitude;
      }
    }
    if (liquidSourceModel != null)
      tgtLoc = new Vector3?(liquidSourceModel.pos);
    else
      tgtLoc = new Vector3?();
  }

  public override void Deselected() => base.Deselected();

  public override void Action()
  {
    if (!tgtLoc.HasValue || !IsGrounded())
      return;
    float nextJumpAvail = float.PositiveInfinity;
    MoveTowards(tgtLoc.Value, false, ref nextJumpAvail, 0.0f);
  }
}
