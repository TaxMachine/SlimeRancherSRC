// Decompiled with JetBrains decompiler
// Type: Chomper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chomper : SRBehaviour
{
  [Tooltip("Time per attack.")]
  public float timePerAttack = 3f;
  private float nextChompTime;
  private SlimeFaceAnimator faceAnim;
  private Animator bodyAnim;
  private int animQuickBiteId;
  private int animBiteId;
  private Metadata metadata;

  public void Awake()
  {
    faceAnim = GetComponent<SlimeFaceAnimator>();
    bodyAnim = GetComponentInChildren<Animator>();
    animBiteId = Animator.StringToHash("Bite");
    animQuickBiteId = Animator.StringToHash("QuickBite");
  }

  public void BiteComplete()
  {
    if (this.metadata != null)
    {
      Metadata metadata = this.metadata;
      if (metadata != null)
        metadata.onComplete(this.metadata.gameObject, this.metadata.id, this.metadata.isHeld, this.metadata.isLaunched);
      DestroyJoints();
      this.metadata = null;
    }
    ResetEatClock();
    bodyAnim.SetBool(animBiteId, false);
    bodyAnim.SetBool(animQuickBiteId, false);
  }

  public bool IsChomping() => metadata != null;

  private void OnJointBreak(float breakForce)
  {
    if (metadata == null || !(metadata.joint != null) || !(metadata.joint.connectedBody == null))
      return;
    ForceCancelChomp();
  }

  public bool CanChomp() => Time.fixedTime >= (double) nextChompTime && !IsChomping();

  public bool CancelChomp(GameObject obj)
  {
    if (metadata == null || metadata.gameObject != obj || metadata.isQuickChomp)
      return false;
    ForceCancelChomp();
    return true;
  }

  private void ForceCancelChomp()
  {
    DestroyJoints();
    metadata = null;
  }

  private void DestroyJoints()
  {
    if (metadata == null || !(metadata.gameObject != null))
      return;
    foreach (SafeJointReference safeJointReference in GetComponents<SafeJointReference>().Concat(metadata.gameObject.GetComponents<SafeJointReference>()))
      safeJointReference.DestroyJoint();
  }

  public void StartChomp(
    GameObject other,
    Identifiable.Id otherId,
    bool whileHeld,
    bool quick,
    OnChompStartDelegate onChompStart,
    OnChompCompleteDelegate onChompComplete)
  {
    if (onChompStart != null)
      onChompStart();
    metadata = new Metadata()
    {
      onComplete = onChompComplete,
      gameObject = other,
      isQuickChomp = quick,
      id = otherId,
      isHeld = whileHeld,
      isLaunched = LayerMask.NameToLayer("Launched") == other.layer
    };
    faceAnim.SetTrigger(otherId == Identifiable.Id.PLAYER ? "triggerAttackTelegraph" : (quick ? "triggerChompOpenQuick" : "triggerChompOpen"));
    bodyAnim.SetBool(quick ? animQuickBiteId : animBiteId, true);
    if (!quick)
      return;
    foreach (SafeJointReference safeJointReference in GetComponents<SafeJointReference>().Concat(other.GetComponents<SafeJointReference>()))
      safeJointReference.DestroyJoint();
    metadata.joint = SlimeUtil.AttachToMouth(gameObject, other);
  }

  public void ResetEatClock() => nextChompTime = Time.fixedTime + timePerAttack;

  public void OnDisable() => ForceCancelChomp();

  public delegate void OnChompStartDelegate();

  public delegate void OnChompCompleteDelegate(
    GameObject chomped,
    Identifiable.Id chompedId,
    bool whileHeld,
    bool wasLaunched);

  private class Metadata
  {
    public OnChompCompleteDelegate onComplete;
    public GameObject gameObject;
    public Identifiable.Id id;
    public bool isHeld;
    public bool isLaunched;
    public bool isQuickChomp;
    public FixedJoint joint;
  }
}
