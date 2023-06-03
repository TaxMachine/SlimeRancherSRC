// Decompiled with JetBrains decompiler
// Type: GotoAsh
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GotoAsh : FindConsumable
{
  private DestroyOnTouching nonAsh;
  private Vector3? tgtLoc;
  private const float GAME_MINS_THRESHOLD = 20f;
  private const float SEARCH_RAD = 30f;
  private const float SEARCH_RAD_SQR = 900f;

  public override void Start()
  {
    base.Start();
    nonAsh = GetComponent<DestroyOnTouching>();
  }

  public override float Relevancy(bool isGrounded)
  {
    if (nonAsh == null)
      return 0.0f;
    FindNearestAshLoc();
    return !tgtLoc.HasValue ? 0.0f : 1f - nonAsh.PctTimeToDestruct();
  }

  public override void Selected()
  {
  }

  private void FindNearestAshLoc()
  {
    float num = 900f;
    AshSource ashSource = null;
    foreach (AshSource allAsh in AshSource.allAshes)
    {
      float sqrMagnitude = (allAsh.transform.position - transform.position).sqrMagnitude;
      if (sqrMagnitude < (double) num)
      {
        ashSource = allAsh;
        num = sqrMagnitude;
      }
    }
    if (ashSource != null)
      tgtLoc = new Vector3?(ashSource.transform.position);
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
