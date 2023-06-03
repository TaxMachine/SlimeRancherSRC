// Decompiled with JetBrains decompiler
// Type: StalkConsumable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class StalkConsumable : FindConsumable, Collidable, RegistryUpdateable
{
  [Tooltip("Whether this should engage targets with parkour.")]
  public bool doesParkour;
  [Tooltip("The power of the feint jump will be modified by this. A value of 1.0 is a normal power jump.")]
  public float feintPowerMultiplier = 1f;
  [Tooltip("The minimum angle away from the player that the feint should be toward.")]
  public float feintMinAngle = 55f;
  [Tooltip("The maximum angle away from the player that the feint should be toward.")]
  public float feintMaxAngle = 80f;
  private GameObject target;
  private float currDrive;
  private bool pouncing;
  private bool feinting;
  private double pounceResetTime;
  private bool pounceFromPivot;
  private Mode mode;
  private double endModeTime;
  private double nextStalkTime;
  private double initStealthUntil;
  private const float POUNCE_DIST = 8f;
  private const float POUNCE_DIST_SQR = 64f;
  private const float APPROACH_TIME = 3f;
  private const float WAIT_TIME = 3f;
  private const float PREP_TIME = 2f;
  private const float POUNCE_TIME = 1f;
  private const float FEINT_TIME = 1f;
  private const float PIVOT_TIME = 1.5f;
  private const float POUNCE_RESET_TIME = 15f;
  private const float PLAYER_EAT_EXTRA_DRIVE = -0.1f;
  private const float CALMED_PLAYER_EAT_EXTRA_DRIVE = -1f;
  private int animButtWiggleId;
  private SlimeStealth stealth;
  private bool pivotNow;
  private Identifiable identifiable;

  public override void Awake()
  {
    base.Awake();
    stealth = GetComponent<SlimeStealth>();
    animButtWiggleId = Animator.StringToHash("ButtWiggle");
    identifiable = GetComponent<Identifiable>();
  }

  public override void Start()
  {
    base.Start();
    searchIds[Identifiable.Id.PLAYER] = new CalmableDriveCalculator(GetComponent<CalmedByWaterSpray>(), SlimeEmotions.Emotion.NONE, -0.1f, -1f);
  }

  public override bool Forbids(SlimeSubbehaviour toMaybeForbid) => toMaybeForbid is GotoConsumable;

  public override float Relevancy(bool isGrounded)
  {
    if (Time.time < nextStalkTime)
      return 0.0f;
    target = FindNearestConsumable(out currDrive);
    return target == null || target == null ? 0.0f : currDrive * currDrive;
  }

  protected override Dictionary<Identifiable.Id, DriveCalculator> GetSearchIds()
  {
    Dictionary<Identifiable.Id, DriveCalculator> searchIds = base.GetSearchIds();
    searchIds[Identifiable.Id.PLAYER] = new CalmableDriveCalculator(GetComponent<CalmedByWaterSpray>(), SlimeEmotions.Emotion.NONE, -0.1f, -1f);
    return searchIds;
  }

  public override bool CanRethink() => mode == Mode.NONE;

  public override void Selected() => SetStealth(true);

  public override void Deselected()
  {
    base.Deselected();
    SetMode(Mode.NONE);
    SetStealth(false);
  }

  public override void OnDisable()
  {
    base.OnDisable();
    target = null;
  }

  public override void Action()
  {
    if (target == null)
    {
      SetMode(Mode.NONE);
      endModeTime = 0.0;
    }
    else
    {
      if (mode == Mode.NONE)
      {
        bool flag = (GetGotoPos(target) - transform.position).sqrMagnitude < 64.0;
        SetMode(flag ? Mode.PREP : Mode.APPROACH);
        endModeTime = Time.time + (flag ? 2.0 : 3.0);
      }
      if (mode == Mode.APPROACH)
      {
        if (IsGrounded())
        {
          Vector3 vector3 = GetGotoPos(target) - transform.position;
          Vector3 normalized1 = vector3.normalized;
          RotateTowards(normalized1);
          Rigidbody component = GetComponent<Rigidbody>();
          float num = component.velocity.sqrMagnitude < 10.0 ? 650f : 400f;
          Vector3 normalized2 = (normalized1 * 400f + Vector3.up * num).normalized;
          component.AddForce(normalized2 * (250f * component.mass * pursuitSpeedFactor * Time.fixedDeltaTime));
          Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
          component.AddForceAtPosition(normalized2 * (450f * component.mass * Time.fixedDeltaTime), position);
          if (vector3.sqrMagnitude < 64.0)
          {
            SetMode(Mode.PREP);
            endModeTime = Time.fixedTime + 2.0;
          }
        }
        if (mode != Mode.APPROACH || Time.fixedTime <= endModeTime)
          return;
        SetMode(Mode.WAIT);
        endModeTime = Time.fixedTime + 3.0;
      }
      else if (mode == Mode.WAIT)
      {
        if (Time.fixedTime <= endModeTime)
          return;
        SetMode(Mode.NONE);
        endModeTime = 0.0;
      }
      else if (mode == Mode.PREP)
      {
        if (Time.fixedTime <= endModeTime)
          return;
        if (doesParkour)
        {
          feinting = false;
          SetMode(Mode.FEINT);
          endModeTime = Time.time + 1.0;
        }
        else
        {
          feinting = false;
          pounceFromPivot = false;
          SetMode(Mode.POUNCE);
          endModeTime = Time.time + 1.0;
        }
      }
      else if (mode == Mode.POUNCE)
      {
        if (IsGrounded() || pounceFromPivot)
        {
          if (pounceFromPivot)
            GetComponent<Rigidbody>().velocity = Vector3.zero;
          Vector3 vector3 = GetGotoPos(target) - transform.position;
          float sqrMagnitude = vector3.sqrMagnitude;
          Vector3 normalized = vector3.normalized;
          LeapToward(sqrMagnitude, normalized, normalized);
          SetMode(Mode.NONE);
          endModeTime = 0.0;
          target = null;
          pouncing = true;
          pounceResetTime = SRSingleton<SceneContext>.Instance.TimeDirector.WorldTime() + 60.0;
          pounceFromPivot = false;
          nextStalkTime = Time.fixedTime + 15.0;
        }
        if (Time.fixedTime <= endModeTime)
          return;
        SetMode(Mode.NONE);
        endModeTime = 0.0;
        feinting = false;
      }
      else if (mode == Mode.FEINT)
      {
        if (IsGrounded())
        {
          Vector3 vector3 = GetGotoPos(target) - transform.position;
          float sqrMagnitude = vector3.sqrMagnitude;
          Vector3 normalized = vector3.normalized;
          float angle = Randoms.SHARED.GetInRange(feintMinAngle, feintMaxAngle) * (Randoms.SHARED.GetBoolean() ? -1f : 1f);
          LeapToward(sqrMagnitude * Mathf.Pow(feintPowerMultiplier, 2f), Quaternion.AngleAxis(angle, Vector3.up) * normalized, normalized);
          feinting = true;
          SetMode(Mode.PIVOT);
          endModeTime = Time.time + 1.5;
        }
        if (Time.fixedTime <= endModeTime)
          return;
        SetMode(Mode.NONE);
        endModeTime = 0.0;
        feinting = false;
      }
      else
      {
        if (mode != Mode.PIVOT)
          return;
        if (pivotNow)
        {
          pivotNow = false;
          pounceFromPivot = true;
          SetMode(Mode.POUNCE);
          endModeTime = Time.time + 1.0;
        }
        if (Time.fixedTime <= endModeTime)
          return;
        SetMode(Mode.NONE);
        endModeTime = 0.0;
        feinting = false;
      }
    }
  }

  private void LeapToward(float distanceSquared, Vector3 directionToJump, Vector3 directionToFace)
  {
    RotateTowards(directionToFace);
    float num1 = 1.2f;
    float num2 = Mathf.Sqrt(Mathf.Sqrt(distanceSquared) * Physics.gravity.magnitude) * num1;
    GetComponent<Rigidbody>().AddForce((directionToJump + Vector3.up).normalized * num2, ForceMode.VelocityChange);
    slimeAudio.Play(slimeAudio.slimeSounds.jumpCue);
    slimeAudio.Play(slimeAudio.slimeSounds.voiceJumpCue);
  }

  public void RegistryUpdate()
  {
    if (!hasStarted || !IsGrounded() || !SRSingleton<SceneContext>.Instance.TimeDirector.HasReached(pounceResetTime))
      return;
    pouncing = false;
  }

  public void ProcessCollisionEnter(Collision col)
  {
    if (Identifiable.BOOP_CLASS.Contains(identifiable.id) && pouncing && stealth == null && col.gameObject == SRSingleton<SceneContext>.Instance.Player)
    {
      Vector3 vector3 = col.gameObject.transform.InverseTransformPoint(col.contacts[0].point);
      if (vector3.z <= 0.20000000298023224 || vector3.y <= 1.0)
        return;
      SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.TABBY_HEADBUTT, 1);
    }
    else
    {
      if (!feinting)
        return;
      pivotNow = true;
      feinting = false;
    }
  }

  public void ProcessCollisionExit(Collision col)
  {
  }

  private void SetMode(Mode mode)
  {
    if (this.mode == mode)
      return;
    this.mode = mode;
    GetComponentInChildren<Animator>().SetBool(animButtWiggleId, mode == Mode.PREP);
    if (mode != Mode.PREP)
      return;
    slimeAudio.Play(slimeAudio.slimeSounds.wiggleCue);
  }

  private void SetStealth(bool isStealthed)
  {
    if (!(stealth != null))
      return;
    stealth.SetStealth(isStealthed);
  }

  private enum Mode
  {
    NONE,
    APPROACH,
    WAIT,
    PREP,
    POUNCE,
    FEINT,
    PIVOT,
  }

  private class CalmableDriveCalculator : DriveCalculator
  {
    private CalmedByWaterSpray calmed;
    private float calmedExtraDrive;

    public CalmableDriveCalculator(
      CalmedByWaterSpray calmed,
      SlimeEmotions.Emotion emotion,
      float normalBonus,
      float calmedBonus)
      : base(emotion, normalBonus, 0.0f)
    {
      calmedExtraDrive = calmedBonus;
      this.calmed = calmed;
    }

    public override float Drive(SlimeEmotions emotions, Identifiable.Id id) => Mathf.Max(0.0f, emotions.GetCurr(emotion) + (calmed.IsCalmed() ? calmedExtraDrive : extraDrive));
  }
}
