// Decompiled with JetBrains decompiler
// Type: CrystalSlimeLaunch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

public class CrystalSlimeLaunch : SlimeSubbehaviour
{
  private static readonly int ANIMATION_ROCK_MODE = Animator.StringToHash("RockMode");
  private const float LAUNCH_MIN_HOURS = 0.0500000045f;
  private const float LAUNCH_MAX_HOURS = 0.25f;
  private const float MIN_LAUNCH_HOURS = 0.00166666682f;
  private const float PREP_HOURS = 0.0166666675f;
  private const float LAUNCH_FORCE = 200f;
  private const float LAUNCH_FORCE_FORWARD = 40f;
  private const float SPIKES_SPREAD = 1.5f;
  private const int LAYER_MASK = 2129921;
  private GameObject launchSpawnLarge;
  private GameObject launchSpawnSmall;
  private TimeDirector timeDirector;
  private Animator animatorBase;
  private SlimeFaceAnimator animatorFace;
  private CalmedByWaterSpray calmed;
  private SlimeAppearanceApplicator slimeAppearanceApplicator;
  private SlimeAudio audio;
  private Vector3 rollDirection;
  private Vector3 rollForward;
  private double nextLaunchTime;
  private State state;
  private double stateEndTime;

  public override void Awake()
  {
    base.Awake();
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    animatorBase = GetComponentInChildren<Animator>();
    animatorFace = GetComponent<SlimeFaceAnimator>();
    audio = GetComponent<SlimeAudio>();
    calmed = GetComponent<CalmedByWaterSpray>();
    slimeAppearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    slimeAppearanceApplicator.OnAppearanceChanged += UpdateSpikeAppearance;
    if (!(slimeAppearanceApplicator.Appearance != null))
      return;
    UpdateSpikeAppearance(slimeAppearanceApplicator.Appearance);
  }

  public override void Start()
  {
    base.Start();
    ResetNextLaunchTime();
  }

  public override float Relevancy(bool isGrounded)
  {
    if (!isGrounded || !timeDirector.HasReached(nextLaunchTime) || calmed.IsCalmed())
      return 0.0f;
    rollDirection = transform.right;
    rollDirection.y = 0.0f;
    rollDirection.Normalize();
    rollForward = Vector3.Cross(rollDirection, Vector3.up);
    return 0.3f;
  }

  public override void Selected()
  {
    stateEndTime = timeDirector.HoursFromNow(0.0166666675f);
    state = State.PREPARING;
    animatorFace.SetTrigger("triggerAwe");
    audio.Play(audio.slimeSounds.rollCue);
  }

  public override void Deselected()
  {
    base.Deselected();
    animatorBase.SetBool(ANIMATION_ROCK_MODE, false);
    state = State.IDLE;
  }

  public override bool CanRethink() => state == State.IDLE;

  public override void Action()
  {
    if (calmed.IsCalmed())
    {
      plexer.ForceRethink();
    }
    else
    {
      if (timeDirector.HasReached(stateEndTime) && IsGrounded())
      {
        if (state == State.PREPARING)
        {
          StartCoroutine(CreateSpikes());
          stateEndTime = timeDirector.HoursFromNow(0.00166666682f);
          state = State.LAUNCHED;
        }
        else if (state == State.LAUNCHED)
        {
          ResetNextLaunchTime();
          state = State.IDLE;
          stateEndTime = 0.0;
        }
      }
      if (state != State.LAUNCHED)
        return;
      slimeBody.AddTorque(rollDirection * (1200f * slimeBody.mass * Time.fixedDeltaTime));
    }
  }

  private void UpdateSpikeAppearance(SlimeAppearance appearance)
  {
    launchSpawnLarge = appearance.CrystalAppearance.largeCrystalPrefab;
    launchSpawnSmall = appearance.CrystalAppearance.smallCrystalPrefab;
  }

  private IEnumerator CreateSpikes()
  {
    CrystalSlimeLaunch crystalSlimeLaunch = this;
    crystalSlimeLaunch.animatorBase.SetBool(ANIMATION_ROCK_MODE, true);
    crystalSlimeLaunch.slimeBody.AddForce(Vector3.up * 200f + crystalSlimeLaunch.rollForward * 40f * crystalSlimeLaunch.slimeBody.mass);
    crystalSlimeLaunch.CreateSpikes(crystalSlimeLaunch.launchSpawnLarge, crystalSlimeLaunch.transform.position);
    yield return new WaitForSeconds(0.2f);
    int inRange = Randoms.SHARED.GetInRange(Mathf.CeilToInt(4f * crystalSlimeLaunch.slimeBody.mass), Mathf.CeilToInt(7f * crystalSlimeLaunch.slimeBody.mass));
    float f = Randoms.SHARED.GetFloat(6.28318548f);
    int num = 0;
    while (num < inRange)
    {
      crystalSlimeLaunch.CreateSpikes(crystalSlimeLaunch.launchSpawnSmall, crystalSlimeLaunch.transform.position + new Vector3(Mathf.Cos(f), 0.0f, Mathf.Sin(f)) * 1.5f);
      ++num;
      f += 6.28318548f / inRange;
    }
  }

  private bool CreateSpikes(GameObject prefab, Vector3 position)
  {
    RaycastHit? nullable = new RaycastHit?();
    RaycastHit raycastHit1;
    foreach (RaycastHit raycastHit2 in Physics.RaycastAll(new Ray(position, Vector3.down), 2f, 2129921, QueryTriggerInteraction.Ignore))
    {
      if (raycastHit2.rigidbody == null)
      {
        if (nullable.HasValue)
        {
          double distance1 = raycastHit2.distance;
          raycastHit1 = nullable.Value;
          double distance2 = raycastHit1.distance;
          if (distance1 >= distance2)
            continue;
        }
        nullable = new RaycastHit?(raycastHit2);
      }
    }
    if (!nullable.HasValue)
      return false;
    GameObject original = prefab;
    raycastHit1 = nullable.Value;
    Vector3 point = raycastHit1.point;
    Quaternion rotation = Quaternion.Euler(0.0f, Randoms.SHARED.GetFloat(360f), 0.0f);
    InstantiateDynamic(original, point, rotation);
    return true;
  }

  private void ResetNextLaunchTime() => nextLaunchTime = timeDirector.HoursFromNow(Mathf.Lerp((float)(71f / (452f * Math.PI)), 0.25f, Mathf.Clamp(Randoms.SHARED.GetInRange(-0.1f, 0.1f) + (1f - emotions.GetCurr(SlimeEmotions.Emotion.AGITATION)), 0.0f, 1f)));

  private enum State
  {
    IDLE,
    PREPARING,
    LAUNCHED,
  }
}
