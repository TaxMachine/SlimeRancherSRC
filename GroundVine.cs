// Decompiled with JetBrains decompiler
// Type: GroundVine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class GroundVine : FindConsumable
{
  [Tooltip("Time between vines, in seconds.")]
  public float cooldown = 2f;
  [Tooltip("The audio cue for the vine coming up out of the ground.")]
  public SECTR_AudioCue vineUpCue;
  [Tooltip("The audio cue for the vine going down back into the ground.")]
  public SECTR_AudioCue vineDownCue;
  private SlimeAppearanceApplicator slimeAppearanceApplicator;
  private GameObject vinePrefab;
  private GameObject vineEnterFX;
  private GameObject vineExitFX;
  private GameObject target;
  private GameObject activeVine;
  private float nextVineTime;
  private GameObject playerObj;
  private int groundMask;
  private Phase phase;
  private float phaseEndTime = float.PositiveInfinity;
  private static HashSet<GameObject> allGrabbed = new HashSet<GameObject>();
  private const float TARGET_SCALE_TIME = 0.25f;
  private const float FULL_VINE_SCALE_TIME = 0.75f;
  private const float FULL_VINE_HEIGHT = 4f;
  private const float MIN_EAT_HEIGHT = 2f;
  private const float MAX_EAT_HEIGHT = 2.5f;
  private const float RELEASE_SCALE_TIME = 0.5f;
  private const float MAX_HEIGHT = 0.5f;
  private const float MIN_EXTRA_HEIGHT = 3f;
  private const float MAX_EXTRA_HEIGHT = 4f;

  public override void Awake()
  {
    base.Awake();
    slimeAppearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    slimeAppearanceApplicator.OnAppearanceChanged += UpdateVineAppearance;
    if (slimeAppearanceApplicator.Appearance != null)
      UpdateVineAppearance(slimeAppearanceApplicator.Appearance);
    nextVineTime = Time.time + cooldown;
    groundMask = 268439553;
  }

  public override void Start()
  {
    base.Start();
    playerObj = SRSingleton<SceneContext>.Instance.Player;
  }

  public override float Relevancy(bool isGrounded)
  {
    if (Time.time < (double) nextVineTime || IsCaptive() || !isGrounded)
      return 0.0f;
    float drive;
    target = FindNearestConsumable(out drive);
    return target == null || target == playerObj || allGrabbed.Contains(target) ? 0.0f : (float) (drive * (double) drive * 0.949999988079071);
  }

  public override void Action()
  {
    if (phase == Phase.IDLE && activeVine == null && target != null)
    {
      if (!MaybeGrapple(target))
        return;
    }
    else if (target == null || activeVine == null)
    {
      Release();
      return;
    }
    if (Time.time < (double) phaseEndTime)
      return;
    if (phase == Phase.GRAB_GROW)
    {
      float time = 0.75f * 1f;
      SECTR_AudioSystem.Play(vineDownCue, activeVine.transform.position, false);
      TweenUtil.ScaleOut(activeVine, time, Ease.InQuint);
      phase = Phase.GRAB_SHRINK;
      phaseEndTime = Time.time + time;
    }
    else if (phase == Phase.GRAB_SHRINK)
    {
      RaycastHit hitInfo;
      Physics.Raycast(transform.position + transform.forward * (PhysicsUtil.RadiusOfObject(gameObject) + 0.5f), Vector3.down, out hitInfo, 2f, groundMask);
      if (hitInfo.collider == null)
      {
        Release();
      }
      else
      {
        SpawnAndPlayFX(vineExitFX, activeVine.transform.position, Quaternion.identity);
        activeVine.transform.position = hitInfo.point;
        target.transform.position = hitInfo.point;
        SpawnAndPlayFX(vineEnterFX, activeVine.transform.position, Quaternion.identity);
        SECTR_AudioSystem.Play(vineUpCue, activeVine.transform.position, false);
        float z = Randoms.SHARED.GetInRange(2f, 2.5f) / 4f;
        float time = 0.75f * z;
        activeVine.transform.localScale = new Vector3(1f, 1f, z);
        TweenUtil.ScaleIn(activeVine, time, Ease.InOutCubic);
        TweenUtil.ScaleIn(target, 0.25f, Ease.Linear);
        phase = Phase.EAT_GROW;
        phaseEndTime = Time.time + time;
        EnableTargetCollider(true);
      }
    }
    else if (phase == Phase.EAT_GROW)
    {
      float time = 0.25f;
      TweenUtil.ScaleOut(activeVine, time, Ease.InQuint);
      phase = Phase.EAT_SHRINK;
      phaseEndTime = Time.time + time;
      SECTR_AudioSystem.Play(vineDownCue, activeVine.transform.position, false);
      Destroyer.Destroy(activeVine.GetComponentInChildren<Joint>(), "GroundVine.Action#1");
    }
    else
    {
      if (phase != Phase.EAT_SHRINK)
        return;
      SpawnAndPlayFX(vineExitFX, activeVine.transform.position, Quaternion.identity);
      Release();
    }
  }

  public override void Selected()
  {
    if (!(target != null))
      return;
    MaybeGrapple(target);
  }

  public override void Deselected()
  {
    base.Deselected();
    nextVineTime = Time.time + cooldown;
    Release();
  }

  public override bool CanRethink() => phase == Phase.IDLE;

  private void UpdateVineAppearance(SlimeAppearance appearance)
  {
    vinePrefab = appearance.VineAppearance.vinePrefab;
    vineEnterFX = appearance.VineAppearance.vineEnterFx;
    vineExitFX = appearance.VineAppearance.vineExitFx;
  }

  private bool MaybeGrapple(GameObject target)
  {
    RaycastHit hitInfo;
    if (!Physics.Raycast(target.transform.position, Vector3.down, out hitInfo, 0.5f, groundMask) || !allGrabbed.Add(target))
      return false;
    float z = (float) ((target.transform.position.y - (double) hitInfo.point.y + Randoms.SHARED.GetInRange(3f, 4f)) / 4.0);
    float time = 0.75f * z;
    activeVine = InstantiateDynamic(vinePrefab, hitInfo.point, Quaternion.Euler(new Vector3(-90f, 0.0f, 0.0f)));
    activeVine.transform.localScale = new Vector3(1f, 1f, z);
    TweenUtil.ScaleIn(activeVine, time, Ease.InOutCubic);
    SpawnAndPlayFX(vineEnterFX, activeVine.transform.position, Quaternion.identity);
    SECTR_AudioSystem.Play(vineUpCue, activeVine.transform.position, false);
    phase = Phase.GRAB_GROW;
    phaseEndTime = Time.time + time;
    Joint componentInChildren = activeVine.GetComponentInChildren<Joint>();
    target.transform.position = componentInChildren.transform.position;
    SafeJointReference.AttachSafely(target, componentInChildren);
    componentInChildren.connectedAnchor = Vector3.zero;
    EnableTargetCollider(false);
    return true;
  }

  public void Release()
  {
    Destroyer.Destroy(activeVine, "GroundVine.Release");
    if (phase >= Phase.GRAB_GROW)
      allGrabbed.Remove(target);
    EnableTargetCollider(true);
    target = null;
    activeVine = null;
    phase = Phase.IDLE;
    phaseEndTime = float.PositiveInfinity;
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    Release();
  }

  public void EnableTargetCollider(bool toEnable)
  {
    if (!(target != null))
      return;
    foreach (Collider component in target.GetComponents<Collider>())
    {
      if (!component.isTrigger)
        component.enabled = toEnable;
    }
  }

  private enum Phase
  {
    IDLE,
    GRAB_GROW,
    GRAB_SHRINK,
    EAT_GROW,
    EAT_SHRINK,
  }
}
