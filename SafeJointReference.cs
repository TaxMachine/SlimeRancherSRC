// Decompiled with JetBrains decompiler
// Type: SafeJointReference
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SafeJointReference : MonoBehaviour
{
  public Joint joint;
  private Rigidbody localRigidbody;
  private bool canDestroyJoint;
  private bool initialEnableCompleted;

  public void Awake() => localRigidbody = GetComponent<Rigidbody>();

  public void OnEnable()
  {
    if (initialEnableCompleted)
    {
      if (joint != null && joint.connectedBody == localRigidbody)
      {
        joint.connectedBody = null;
        joint.connectedBody = localRigidbody;
      }
      else
        Destroyer.Destroy(this, "SafeJointReference.OnEnable");
    }
    else
      initialEnableCompleted = true;
  }

  public void Update()
  {
    if (!(joint == null) && !(joint.connectedBody != localRigidbody))
      return;
    Destroyer.Destroy(this, "SafeJointReference.Update");
  }

  public void OnDisable()
  {
    if (!initialEnableCompleted || !(joint == null))
      return;
    Destroyer.Destroy(this, "SafeJointReference.OnDisable");
  }

  public void DestroyJoint()
  {
    if (canDestroyJoint)
    {
      Destroyer.Destroy(joint, "SafeJointReference.DestroyJoint#1");
      joint = null;
    }
    Destroyer.Destroy(this, "SafeJointReference.DestroyJoint#2");
  }

  public static SafeJointReference AttachSafely(
    GameObject toAttach,
    Joint joint,
    bool canDestroyJoint = true)
  {
    SafeJointReference safeJointReference = toAttach.AddComponent<SafeJointReference>();
    safeJointReference.canDestroyJoint = canDestroyJoint;
    safeJointReference.joint = joint;
    joint.connectedBody = toAttach.GetComponent<Rigidbody>();
    return safeJointReference;
  }

  public void OnDrawGizmosSelected()
  {
    if (!(joint != null) || !(joint.connectedBody != null))
      return;
    Vector3 vector3_1 = joint.transform.TransformPoint(joint.anchor);
    Vector3 vector3_2 = joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(vector3_1, 0.25f);
    Gizmos.DrawWireSphere(vector3_2, 0.25f);
    Gizmos.DrawLine(vector3_1, vector3_2);
  }
}
