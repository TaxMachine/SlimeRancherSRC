// Decompiled with JetBrains decompiler
// Type: SlimeStage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class SlimeStage : MonoBehaviour
{
  public Rigidbody jointBody;
  public Rigidbody largoJointBody;
  public Attractor attractor;
  public GameObject activationFX;
  public float jointBreakForce = 20f;
  public float jointBreakTorque = float.PositiveInfinity;
  private Joint joint;
  private bool isJointActive;
  private GameObject slime;
  private Animator anim;
  private int animActiveId;

  public void Awake()
  {
    anim = GetComponentInParent<Animator>();
    animActiveId = Animator.StringToHash("active");
  }

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger || !(joint == null))
      return;
    Identifiable.Id id = Identifiable.GetId(col.gameObject);
    if (!Identifiable.IsSlime(id))
      return;
    if (Identifiable.IsTarr(id))
      SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.SLIME_STAGE_TARRS, 1);
    slime = col.gameObject;
    slime.transform.rotation = Quaternion.Euler(new Vector3(0.0f, slime.transform.rotation.eulerAngles.y, 0.0f));
    slime.GetComponent<SlimeSubbehaviourPlexer>().RegisterBehaviorBlocker();
    slime.GetComponent<SlimeFaceAnimator>().SetTrigger("triggerAwe");
    joint = CreateJoint((Identifiable.IsLargo(id) || Identifiable.IsTarr(id) ? largoJointBody : (Component) jointBody).gameObject);
    SafeJointReference.AttachSafely(slime, joint);
    StartCoroutine(DelayedSetJointBreakForce());
    isJointActive = true;
    jointBody.transform.localRotation = Quaternion.Euler(Vector3.zero);
    largoJointBody.transform.localRotation = Quaternion.Euler(Vector3.zero);
    SRBehaviour.InstantiateDynamic(activationFX, transform.position, transform.rotation);
    anim.SetBool(animActiveId, true);
    attractor.SetAweFactor(1f);
  }

  private IEnumerator DelayedSetJointBreakForce()
  {
    yield return new WaitForSeconds(1f);
    if (joint != null)
    {
      joint.breakForce = jointBreakForce;
      joint.breakTorque = jointBreakTorque;
    }
  }

  public void FixedUpdate()
  {
    if (isJointActive && joint == null)
    {
      if (slime != null)
      {
        slime.GetComponent<SlimeSubbehaviourPlexer>().UnregisterBehaviorBlocker();
        slime = null;
      }
      anim.SetBool(animActiveId, false);
      attractor.SetAweFactor(0.0f);
      isJointActive = false;
    }
    if (!(joint != null))
      return;
    jointBody.transform.Rotate(Vector3.up, 90f * Time.fixedDeltaTime);
    largoJointBody.transform.Rotate(Vector3.up, 90f * Time.fixedDeltaTime);
    if (!(joint.connectedBody == null))
      return;
    Destroyer.Destroy(joint, "SlimeStage.FixedUpdate");
  }

  private static Joint CreateJoint(GameObject parent)
  {
    ConfigurableJoint joint = parent.AddComponent<ConfigurableJoint>();
    joint.anchor = Vector3.zero;
    joint.autoConfigureConnectedAnchor = false;
    joint.connectedAnchor = Vector3.zero;
    SoftJointLimitSpring jointLimitSpring = new SoftJointLimitSpring();
    jointLimitSpring.damper = 0.2f;
    jointLimitSpring.spring = 1000f;
    joint.xMotion = ConfigurableJointMotion.Limited;
    joint.yMotion = ConfigurableJointMotion.Limited;
    joint.zMotion = ConfigurableJointMotion.Limited;
    joint.angularXMotion = ConfigurableJointMotion.Limited;
    joint.angularYMotion = ConfigurableJointMotion.Limited;
    joint.angularZMotion = ConfigurableJointMotion.Limited;
    joint.linearLimitSpring = jointLimitSpring;
    joint.angularXLimitSpring = jointLimitSpring;
    joint.angularYZLimitSpring = jointLimitSpring;
    return joint;
  }
}
