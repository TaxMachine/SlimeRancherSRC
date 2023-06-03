// Decompiled with JetBrains decompiler
// Type: PlortCollector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class PlortCollector : SRBehaviour, LandPlotModel.Participant
{
  [Tooltip("The area within which we collect plorts.")]
  public TrackCollisions collectionArea;
  [Tooltip("Time between collections in hours.")]
  public float collectPeriod = 1f;
  [Tooltip("Animator to animate while collecting any plorts.")]
  public Animator collectAnim;
  [Tooltip("Effect to play on collecting an individual plort.")]
  public GameObject collectFX;
  [Tooltip("Where to pull the plorts to")]
  public Transform collectPt;
  private SiloStorage storage;
  private SECTR_AudioSource vacAudio;
  private TimeDirector timeDir;
  private Region region;
  private List<JointReference> joints = new List<JointReference>();
  private double endCollectAt;
  private double forceCollectUntil;
  private const float COLLECT_DIST = 1f;
  private const float COLLECT_DIST_SQR = 1f;
  private const float COLLECT_SPEED = 5f;
  private const float MIN_COLLECT_TIME = 0.0833333358f;
  private const float MAX_COLLECT_TIME = 0.166666672f;
  private int animCycloneActiveId;
  private LandPlotModel model;

  public void Awake()
  {
    region = GetComponentInParent<Region>();
    storage = GetComponentInParent<SiloStorage>();
    vacAudio = GetComponent<SECTR_AudioSource>();
    collectAnim = GetComponentInChildren<Animator>();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    animCycloneActiveId = Animator.StringToHash("CycloneActive");
  }

  public void InitModel(LandPlotModel model)
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    model.collectorNextTime = timeDir.HoursFromNowOrStart(collectPeriod);
  }

  public void SetModel(LandPlotModel model) => this.model = model;

  public void Update()
  {
    if (region.Hibernated)
      return;
    if (joints.Count > 0 && timeDir.HasReached(endCollectAt))
    {
      foreach (JointReference joint in joints)
        joint.Destroy();
      joints.Clear();
    }
    else if (timeDir.HasReached(model.collectorNextTime))
      DoCollection();
    if (joints.Count > 0)
    {
      List<JointReference> jointReferenceList = new List<JointReference>();
      foreach (JointReference joint in joints)
      {
        if (joint.joint == null || joint.joint.connectedBody == null)
          jointReferenceList.Add(joint);
        else if (!storage.CanAccept(joint.id))
          jointReferenceList.Add(joint);
        else if ((joint.joint.connectedBody.transform.position - collectPt.position).sqrMagnitude <= 1.0)
        {
          if (storage.MaybeAddIdentifiable(joint.id))
          {
            if (collectFX != null)
              SpawnAndPlayFX(collectFX, joint.joint.connectedBody.transform.position, joint.joint.connectedBody.transform.rotation);
            Destroyer.DestroyActor(joint.joint.connectedBody.gameObject, "PlortCollector.Update");
          }
          jointReferenceList.Add(joint);
        }
        else
          joint.joint.maxDistance = Mathf.Max(0.0f, joint.joint.maxDistance - Time.deltaTime * 5f);
      }
      foreach (JointReference jointReference in jointReferenceList)
      {
        joints.Remove(jointReference);
        jointReference.Destroy();
      }
    }
    bool flag = joints.Count > 0 || !timeDir.HasReached(forceCollectUntil);
    if (collectAnim != null)
      collectAnim.SetBool(animCycloneActiveId, flag);
    if (flag && !vacAudio.IsPlaying)
    {
      vacAudio.Play();
    }
    else
    {
      if (flag || !vacAudio.IsPlaying)
        return;
      vacAudio.Stop(false);
    }
  }

  public void StartCollection()
  {
    if (joints.Count != 0 || !timeDir.HasReached(forceCollectUntil))
      return;
    DoCollection();
  }

  private void DoCollection()
  {
    model.collectorNextTime += 3600.0 * collectPeriod;
    foreach (GameObject currCollider in collectionArea.CurrColliders())
    {
      Identifiable component1 = currCollider.GetComponent<Identifiable>();
      if (component1 != null && storage.CanAccept(component1.id))
      {
        Vacuumable component2 = currCollider.GetComponent<Vacuumable>();
        if (component2 != null && !component2.isCaptive())
        {
          GameObject gameObject = new GameObject("CollectJoint");
          gameObject.AddComponent<Rigidbody>().isKinematic = true;
          gameObject.transform.SetParent(collectPt, false);
          gameObject.transform.localPosition = Vector3.zero;
          SpringJoint toJoint = gameObject.AddComponent<SpringJoint>();
          toJoint.spring = 1000f;
          toJoint.maxDistance = (currCollider.transform.position - collectPt.position).magnitude;
          toJoint.autoConfigureConnectedAnchor = false;
          toJoint.connectedAnchor = Vector3.zero;
          SafeJointReference.AttachSafely(currCollider, toJoint);
          toJoint.connectedBody.WakeUp();
          component2.capture(toJoint);
          joints.Add(new JointReference()
          {
            vacuumable = component2,
            joint = toJoint,
            id = component1.id
          });
        }
      }
    }
    forceCollectUntil = timeDir.HoursFromNow(0.0833333358f);
    endCollectAt = timeDir.HoursFromNow(0.166666672f);
  }

  public void FastForward(
    List<Identifiable.Id> produceIds,
    List<Identifiable.Id> alreadyCollectedIds)
  {
    for (int index = produceIds.Count - 1; index >= 0; --index)
    {
      if (storage.MaybeAddIdentifiable(produceIds[index]))
        alreadyCollectedIds.Add(produceIds[index]);
    }
  }

  private class JointReference
  {
    public Identifiable.Id id;
    public Vacuumable vacuumable;
    public SpringJoint joint;

    public void Destroy()
    {
      Destroyer.Destroy(joint, "PlortCollector.JointReference.Destroy");
      if (!(vacuumable != null))
        return;
      vacuumable.release();
      vacuumable = null;
    }
  }
}
