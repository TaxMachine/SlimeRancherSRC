// Decompiled with JetBrains decompiler
// Type: GotoConsumable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class GotoConsumable : FindConsumable
{
  public float maxJump = 12f;
  public float attemptTime = 10f;
  public float giveUpTime = 10f;
  private GameObject target;
  private float currDrive;
  private float nextLeapAvail;
  private SlimeEat eat;
  private const float AGITATION_PER_GIVE_UP = 0.1f;
  private int fastForwardLayerMask;
  private Mode mode;
  private float modeEndTime;
  private static List<Identifiable.Id> reuseIdList = new List<Identifiable.Id>();
  private static readonly List<Identifiable.Id> alreadyCollectedList = new List<Identifiable.Id>();
  private const float MAX_VEL_TO_BOUNCE = 0.1f;
  private const float SQR_MAX_VEL_TO_BOUNCE = 0.0100000007f;

  public override void Awake()
  {
    base.Awake();
    eat = GetComponent<SlimeEat>();
    fastForwardLayerMask = LayerMask.GetMask("Actor", "ActorEchoes", "ActorIgnorePlayer");
  }

  public override float Relevancy(bool isGrounded)
  {
    if (Time.time >= (double) modeEndTime)
    {
      if (mode == Mode.ATTEMPTING)
      {
        mode = Mode.GIVE_UP;
        modeEndTime = Time.time + giveUpTime;
        emotions.Adjust(SlimeEmotions.Emotion.AGITATION, 0.1f);
      }
      else if (mode == Mode.GIVE_UP)
      {
        mode = Mode.AVAIL;
        modeEndTime = float.PositiveInfinity;
      }
    }
    if (mode == Mode.GIVE_UP)
      return 0.0f;
    target = FindNearestConsumable(out currDrive);
    return !(target == null) ? (float) (currDrive * (double) currDrive * 0.949999988079071) : 0.0f;
  }

  public override void Selected()
  {
    mode = Mode.ATTEMPTING;
    modeEndTime = Time.time + attemptTime;
    GetComponent<SlimeFaceAnimator>().SetSeekingFood(true);
  }

  public override void Deselected()
  {
    base.Deselected();
    GetComponent<SlimeFaceAnimator>().SetSeekingFood(false);
  }

  public override void Action()
  {
    if (!(target != null))
      return;
    if (SRSingleton<SceneContext>.Instance.TimeDirector.IsFastForwarding() && CellDirector.IsOnRanch(member))
    {
      if (IsBlocked(target, fastForwardLayerMask, true))
        return;
      Identifiable.Id id = Identifiable.GetId(target);
      if (id == Identifiable.Id.PLAYER)
        return;
      eat.EatImmediate(target, id, eat.GetProducedIds(id, reuseIdList), alreadyCollectedList, false);
    }
    else
    {
      if (eat.IsChomping())
        return;
      MoveTowards(GetGotoPos(target), IsBlocked(target), ref nextLeapAvail, DriveToJumpiness(currDrive) * maxJump);
    }
  }

  private float DriveToJumpiness(float drive)
  {
    float num = Mathf.Max(0.0f, drive - 0.666f) / 0.334f;
    return Mathf.Lerp(0.4f, 1f, num * num);
  }

  private enum Mode
  {
    AVAIL,
    ATTEMPTING,
    GIVE_UP,
  }
}
