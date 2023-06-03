// Decompiled with JetBrains decompiler
// Type: SlimeSubbehaviourPlexer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSubbehaviourPlexer : 
  RegisteredActorBehaviour,
  FloatingReactor,
  RegistryFixedUpdateable
{
  private const float RETHINK_PERIOD = 1f;
  private SlimeSubbehaviour[] subbehaviors;
  private SlimeSubbehaviour currBehavior;
  private float nextRethinkTime;
  private Vacuumable vacuumable;
  private bool isFloating;
  private float activationTime;
  private float distToGround;
  private Collider ownCollider;
  private bool lastBlocked;
  private GameObject lastBlockedTarget;
  private float nextBlockCheckTime;
  private float nextGroundCheckTime;
  private bool wasGrounded;
  private RaycastHit groundHit;
  private Rigidbody body;
  private TotemLinker totemLinker;
  private int behaviorBlockers;
  private const float BLOCKED_CHECK_INTERVAL = 1f;
  private const float GROUND_CHECK_INTERVAL = 0.25f;
  private const float DEFAULT_ACTIVATION_DELAY = 3f;
  private const float GROUNDING_FACTOR = 1.3f;
  private const float BLOCKED_FACTOR = 5f;

  public float? activationDelayOverride { get; set; }

  public override void Start()
  {
    base.Start();
    CollectSubbehaviours();
    body = GetComponent<Rigidbody>();
    vacuumable = GetComponent<Vacuumable>();
    foreach (Collider component in GetComponents<Collider>())
    {
      if (!component.isTrigger)
      {
        ownCollider = component;
        break;
      }
    }
    totemLinker = GetComponentInChildren<TotemLinker>();
    activationTime = Time.fixedTime + (activationDelayOverride.HasValue ? activationDelayOverride.Value : 3f);
  }

  public void RegisterBehaviorBlocker() => ++behaviorBlockers;

  public void UnregisterBehaviorBlocker() => --behaviorBlockers;

  public void CollectSubbehaviours()
  {
    SlimeSubbehaviour[] components = GetComponents<SlimeSubbehaviour>();
    List<SlimeSubbehaviour> slimeSubbehaviourList = new List<SlimeSubbehaviour>(components);
    foreach (SlimeSubbehaviour slimeSubbehaviour1 in components)
    {
      foreach (SlimeSubbehaviour slimeSubbehaviour2 in components)
      {
        if (slimeSubbehaviour1.Forbids(slimeSubbehaviour2))
        {
          Destroyer.Destroy(slimeSubbehaviour2, "SlimeSubbehaviourPlexer.CollectSubbehaviours");
          slimeSubbehaviourList.Remove(slimeSubbehaviour2);
        }
      }
    }
    subbehaviors = slimeSubbehaviourList.ToArray();
  }

  public void RegistryFixedUpdate()
  {
    if (ownCollider != null)
      distToGround = ownCollider.bounds.extents.y * 1.3f;
    if (Time.fixedTime >= (double) nextGroundCheckTime)
    {
      nextGroundCheckTime = Time.fixedTime + 0.25f;
      if (distToGround > 0.0)
        SRSingleton<GameContext>.Instance.RaycastBatcher.QueueRaycast(new RaycastCommand(body.position, Vector3.down, distToGround), OnGroundedRaycastResultReceived);
      else
        wasGrounded = false;
    }
    if (Time.fixedTime < (double) activationTime)
      return;
    if (IsCaptive() || behaviorBlockers > 0)
    {
      if (!(currBehavior != null))
        return;
      if (currBehavior.CanRethink())
      {
        currBehavior.Deselected();
        currBehavior = null;
      }
      else
        currBehavior.Action();
    }
    else if (Time.fixedTime >= (double) nextRethinkTime && (currBehavior == null || currBehavior.CanRethink()))
    {
      nextRethinkTime = Time.fixedTime + 1f;
      SlimeSubbehaviour bestBehaviour = GetBestBehaviour();
      if (bestBehaviour != null)
      {
        if (bestBehaviour != currBehavior)
        {
          if (currBehavior != null)
            currBehavior.Deselected();
          currBehavior = bestBehaviour;
          currBehavior.Selected();
        }
        bestBehaviour.Action();
      }
      else
      {
        if (currBehavior != null)
          currBehavior.Deselected();
        currBehavior = null;
      }
    }
    else
    {
      if (!(currBehavior != null))
        return;
      currBehavior.Action();
    }
  }

  private void OnGroundedRaycastResultReceived(RaycastHit result)
  {
    groundHit = result;
    wasGrounded = result.collider != null;
  }

  private SlimeSubbehaviour GetBestBehaviour()
  {
    bool isGrounded = IsGrounded();
    float num1 = 0.0001f;
    SlimeSubbehaviour bestBehaviour = null;
    if (subbehaviors != null)
    {
      foreach (SlimeSubbehaviour subbehavior in subbehaviors)
      {
        if (subbehavior.enabled)
        {
          float num2 = subbehavior.Relevancy(isGrounded);
          if (num2 < 0.0 || num2 > 1.0)
            Log.Error("Behavior relevancy outside of correct range.", "relevancy", num2, "behavior", subbehavior.name);
          if (num2 > (double) num1)
          {
            num1 = num2;
            bestBehaviour = subbehavior;
          }
        }
      }
    }
    return bestBehaviour;
  }

  public bool IsCaptive()
  {
    if (!(vacuumable != null))
      return false;
    return vacuumable.isCaptive() || vacuumable.isHeld();
  }

  public void ForceRethink()
  {
    nextRethinkTime = float.NegativeInfinity;
    if (!(currBehavior != null))
      return;
    currBehavior.Deselected();
    currBehavior = null;
  }

  public bool IsFloating() => isFloating;

  public void SetIsFloating(bool isFloating) => this.isFloating = isFloating;

  public bool IsGrounded()
  {
    if (!IsFloating())
      return wasGrounded;
    groundHit.normal = Vector3.up;
    return true;
  }

  public RaycastHit GroundHit() => groundHit;

  public bool IsTotemed() => totemLinker != null && totemLinker.IsLinkedFrom();

  public bool IsNearGrounded(float dist) => body != null && Physics.Raycast(body.position, Vector3.down, distToGround + dist);

  public bool IsBlocked(GameObject obj, int layersToIgnore, bool forceCheckFullDist)
  {
    Vector3 direction = obj == null ? transform.forward : SlimeSubbehaviour.GetGotoPos(obj) - transform.position;
    return IsBlocked(obj, direction, layersToIgnore, forceCheckFullDist);
  }

  public bool IsBlocked(
    GameObject obj,
    Vector3 direction,
    int layersToIgnore,
    bool forceCheckFullDist)
  {
    if (forceCheckFullDist || (Time.time > (double) nextBlockCheckTime || lastBlockedTarget != obj) && distToGround > 0.0)
    {
      direction.y = 0.0f;
      float radius = distToGround * 0.05f;
      float num;
      if (obj != null)
      {
        num = Vector3.Distance(transform.position, obj.transform.position);
        if (!forceCheckFullDist)
          num = Mathf.Min(distToGround * 5f, num);
      }
      else
        num = distToGround * 5f;
      RaycastHit hitInfo;
      Physics.SphereCast(body.position, radius, direction, out hitInfo, num, ~layersToIgnore);
      lastBlocked = hitInfo.collider != null && (obj == null || hitInfo.collider.gameObject != obj);
      lastBlockedTarget = obj;
      nextBlockCheckTime = Time.time + 1f;
    }
    return lastBlocked;
  }
}
