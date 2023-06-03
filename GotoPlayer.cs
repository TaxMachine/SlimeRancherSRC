// Decompiled with JetBrains decompiler
// Type: GotoPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class GotoPlayer : FindConsumable
{
  public float maxJump = 12f;
  public float attemptTime = 10f;
  public float giveUpTime = 10f;
  public bool shouldGotoPlayer;
  public SlimeEmotions.Emotion driver;
  public float extraDrive;
  public float minDrive;
  private GameObject target;
  private float currDrive;
  private float nextLeapAvail;
  private Chomper chomper;
  private const float AGITATION_PER_GIVE_UP = 0.1f;
  private Mode mode;
  private float modeEndTime;
  private const float MAX_VEL_TO_BOUNCE = 0.1f;
  private const float SQR_MAX_VEL_TO_BOUNCE = 0.0100000007f;

  public override void Awake()
  {
    base.Awake();
    chomper = GetComponent<Chomper>();
  }

  protected override Dictionary<Identifiable.Id, DriveCalculator> GetSearchIds() => new Dictionary<Identifiable.Id, DriveCalculator>()
  {
    [Identifiable.Id.PLAYER] = new DriveCalculator(driver, extraDrive, minDrive)
  };

  public override float Relevancy(bool isGrounded)
  {
    if (!shouldGotoPlayer)
      return 0.0f;
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
  }

  public override void Action()
  {
    if (!(target != null) || SRSingleton<SceneContext>.Instance.TimeDirector.IsFastForwarding() || chomper.IsChomping())
      return;
    MoveTowards(GetGotoPos(target), IsBlocked(target), ref nextLeapAvail, DriveToJumpiness(currDrive) * maxJump);
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
