// Decompiled with JetBrains decompiler
// Type: TactusStickiness
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TactusStickiness : MonoBehaviour
{
  public float jointBreakForce = 10f;
  public float jointBreakTorque = float.PositiveInfinity;
  public float jointTTLMins = 10f;
  private Dictionary<int, double> ineligibleGameObjIds = new Dictionary<int, double>();
  private TimeDirector timeDir;
  private WaitForChargeup waiter;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    waiter = GetComponentInParent<WaitForChargeup>();
  }

  public void OnCollisionEnter(Collision col)
  {
    if (waiter != null && waiter.IsWaiting() || ineligibleGameObjIds.ContainsKey(col.gameObject.GetInstanceID()))
      return;
    ineligibleGameObjIds[col.gameObject.GetInstanceID()] = double.PositiveInfinity;
    CreateJointObject(col.gameObject);
  }

  public void Update()
  {
    List<int> intList = null;
    foreach (KeyValuePair<int, double> ineligibleGameObjId in ineligibleGameObjIds)
    {
      if (timeDir.HasReached(ineligibleGameObjId.Value))
      {
        if (intList == null)
          intList = new List<int>();
        intList.Add(ineligibleGameObjId.Key);
      }
    }
    if (intList == null)
      return;
    foreach (int key in intList)
      ineligibleGameObjIds.Remove(key);
  }

  private void ReportBrokenJoint(int objID) => ineligibleGameObjIds[objID] = timeDir.HoursFromNowOrStart(0.0166666675f);

  private void CreateJointObject(GameObject stuckObj)
  {
    GameObject gameObject = new GameObject("Joint");
    gameObject.transform.SetParent(transform, false);
    gameObject.transform.position = stuckObj.transform.position;
    gameObject.transform.rotation = stuckObj.transform.rotation;
    gameObject.AddComponent<Rigidbody>().isKinematic = true;
    FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
    SafeJointReference.AttachSafely(stuckObj, fixedJoint);
    gameObject.AddComponent<JointHelper>().SetTactusStickiness(this, stuckObj, timeDir.HoursFromNowOrStart(jointTTLMins * 0.0166666675f));
  }

  public class JointHelper : MonoBehaviour
  {
    private TactusStickiness stickiness;
    private GameObject stuckObj;
    private Joint joint;
    private double expiration;
    private SlimeSubbehaviourPlexer plexer;
    private Collider[] stuckColliders;

    public void SetTactusStickiness(
      TactusStickiness stickiness,
      GameObject stuckObj,
      double expiration)
    {
      this.stickiness = stickiness;
      this.stuckObj = stuckObj;
      joint = GetComponent<Joint>();
      this.expiration = expiration;
      plexer = stuckObj.GetComponent<SlimeSubbehaviourPlexer>();
      int num = plexer != null ? 1 : 0;
      stuckColliders = stuckObj.GetComponents<Collider>();
      foreach (Collider stuckCollider in stuckColliders)
        Physics.IgnoreCollision(stuckCollider, stickiness.GetComponent<Collider>(), true);
      StartCoroutine(DelayedSetJointBreakForce());
    }

    public void OnDestroy()
    {
      int num = plexer != null ? 1 : 0;
      foreach (Collider stuckCollider in stuckColliders)
      {
        if (stuckCollider != null)
          Physics.IgnoreCollision(stuckCollider, stickiness.GetComponent<Collider>(), false);
      }
    }

    public void OnJointBreak(float force)
    {
      stickiness.ReportBrokenJoint(stuckObj.GetInstanceID());
      Destroyer.Destroy(gameObject, "TactusStickiness.OnJointBreak");
    }

    public void Update()
    {
      if (!stickiness.timeDir.HasReached(expiration))
        return;
      stickiness.ReportBrokenJoint(stuckObj.GetInstanceID());
      Destroyer.Destroy(gameObject, "TactusStickiness.Update");
    }

    public IEnumerator DelayedSetJointBreakForce()
    {
      yield return new WaitForSeconds(1f);
      if (joint != null)
      {
        joint.breakForce = stickiness.jointBreakForce;
        joint.breakTorque = stickiness.jointBreakTorque;
      }
    }
  }
}
