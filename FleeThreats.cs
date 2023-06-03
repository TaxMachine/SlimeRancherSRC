// Decompiled with JetBrains decompiler
// Type: FleeThreats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class FleeThreats : SlimeSubbehaviour, RegistryFixedUpdateable
{
  public FearProfile fearProfile;
  public SlimeEmotions.Emotion driver = SlimeEmotions.Emotion.FEAR;
  public float maxJump = 2f;
  public float facingStability = 0.2f;
  public float facingSpeed = 1f;
  private Threat threat;
  private RegionMember member;
  private TotemLinker totemLinker;
  private HashSet<TentacleHook> grapplers = new HashSet<TentacleHook>();
  private float nextLeap;
  private const float LEAP_COOLDOWN = 0.5f;
  private List<GameObject> nearbyGameObjects = new List<GameObject>();

  public override void Awake()
  {
    base.Awake();
    member = GetComponent<RegionMember>();
    totemLinker = GetComponentInChildren<TotemLinker>();
  }

  public override void Start() => base.Start();

  public override float Relevancy(bool isGrounded)
  {
    threat = FindNearestThreat();
    return threat != null ? emotions.GetCurr(driver) : 0.0f;
  }

  public void RegistryFixedUpdate()
  {
    if (threat != null && threat.gameObject != null && threat.gameObject.activeSelf)
    {
      emotions.Adjust(driver, fearProfile.DistToFearAdjust(threat.id, (GetGotoPos(threat.gameObject) - transform.position).magnitude));
    }
    else
    {
      if (threat == null || !(threat.gameObject == null))
        return;
      threat = null;
    }
  }

  public override void Selected()
  {
    SlimeFaceAnimator component = GetComponent<SlimeFaceAnimator>();
    if (component != null)
      component.SetTrigger("triggerAlarm");
    if (!(totemLinker != null))
      return;
    totemLinker.DisableToteming();
  }

  public override void Action()
  {
    if (threat == null || !(threat.gameObject != null) || !IsGrounded() || Time.fixedTime < (double) nextLeap)
      return;
    Vector3 dirToTarget = -(GetGotoPos(threat.gameObject) - transform.position).normalized;
    RotateTowards(dirToTarget);
    if (grapplers.Count > 0)
      return;
    float curr = emotions.GetCurr(driver);
    if (IsBlocked(null))
      slimeBody.AddForce((dirToTarget + Vector3.up * 5f).normalized * (3f * curr * maxJump * slimeBody.mass), ForceMode.Impulse);
    else
      slimeBody.AddForce((dirToTarget + Vector3.up).normalized * (curr * maxJump * slimeBody.mass), ForceMode.Impulse);
    nextLeap = Time.fixedTime + 0.5f;
  }

  private void RotateTowards(Vector3 dirToTarget)
  {
    Vector3 angularVelocity = slimeBody.angularVelocity;
    slimeBody.AddTorque(Vector3.Cross(Quaternion.AngleAxis(angularVelocity.magnitude * 57.29578f * facingStability / facingSpeed, angularVelocity) * transform.forward, dirToTarget) * (facingSpeed * facingSpeed));
  }

  private Threat FindNearestThreat()
  {
    Threat nearestThreat = null;
    Vector3 position = transform.position;
    float curr = emotions.GetCurr(driver);
    foreach (Identifiable.Id threateningIdentifiable in fearProfile.GetThreateningIdentifiables())
    {
      double searchRadius = fearProfile.GetSearchRadius(threateningIdentifiable, curr);
      float num = (float) (searchRadius * searchRadius);
      nearbyGameObjects.Clear();
      CellDirector.Get(threateningIdentifiable, member, nearbyGameObjects);
      for (int index = 0; index < nearbyGameObjects.Count; ++index)
      {
        GameObject nearbyGameObject = nearbyGameObjects[index];
        if (nearbyGameObject.activeInHierarchy && (threateningIdentifiable != Identifiable.Id.FIRE_COLUMN || FireColumnIsActiveThreat(nearbyGameObject)))
        {
          float sqrMagnitude = (GetGotoPos(nearbyGameObject) - position).sqrMagnitude;
          if (sqrMagnitude < (double) num)
          {
            if (nearestThreat == null)
              nearestThreat = new Threat();
            nearestThreat.id = threateningIdentifiable;
            nearestThreat.gameObject = nearbyGameObject;
            num = sqrMagnitude;
          }
        }
      }
    }
    nearbyGameObjects.Clear();
    return nearestThreat;
  }

  private bool FireColumnIsActiveThreat(GameObject potentialThreatObject)
  {
    FireColumn componentInParent = potentialThreatObject.GetComponentInParent<FireColumn>();
    return componentInParent != null && componentInParent.IsFireActive();
  }

  public void AddGrappler(TentacleHook hook) => grapplers.Add(hook);

  public void RemoveGrappler(TentacleHook hook) => grapplers.Remove(hook);

  private class Threat
  {
    public Identifiable.Id id;
    public GameObject gameObject;
  }
}
