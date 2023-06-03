// Decompiled with JetBrains decompiler
// Type: TentacleHook
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHook : SRBehaviour, Attachment
{
  [Tooltip("The joint connecting us to our hooked victim.")]
  public FixedJoint hookJoint;
  [Tooltip("The joint connecting us back to the tentacle-user.")]
  public SpringJoint parentJoint;
  [Tooltip("The part of ourselves that is connected to our parent.")]
  public GameObject parentEnd;
  [Tooltip("In meters per second, how quickly we reduce the length of the tentacle.")]
  public float retractSpeed = 1f;
  public SECTR_AudioCue shootCue;
  public SECTR_AudioCue grabCue;
  private const float CONVERT_TO_FIXED_DIST = 0.3f;
  private const float CONVERT_TO_FIXED_DIST_SQR = 0.09f;
  public GameObject tentacleObject;
  private Material tentacleMaterial;
  private float? fadeInTime;
  private FleeThreats targetFleeer;
  private GameObject target;
  private bool snapping;
  private float snapProgress;
  private GameObject hookEndObj;
  private bool pauseRetract;
  private static List<GameObject> allHooked = new List<GameObject>();
  private const float FADE_IN_TIME = 0.5f;
  private const float SNAP_TIME = 0.2f;
  private SafeJointReference parentSafeJoint;
  private SafeJointReference hookSafeJoint;

  public void Init(
    GameObject source,
    GameObject target,
    Vector3 attachPoint,
    bool causeFear,
    float intermediateHeight)
  {
    allHooked.Add(target);
    float magnitude = (attachPoint - source.transform.position).magnitude;
    parentSafeJoint = SafeJointReference.AttachSafely(source, parentJoint);
    parentJoint.minDistance = 0.0f;
    parentJoint.maxDistance = magnitude;
    hookSafeJoint = SafeJointReference.AttachSafely(target, hookJoint);
    hookJoint.transform.position = attachPoint;
    hookJoint.connectedAnchor = Vector3.zero;
    hookEndObj = hookJoint.gameObject;
    if (causeFear)
    {
      SlimeEmotions component = target.GetComponent<SlimeEmotions>();
      if (component != null)
        component.Adjust(SlimeEmotions.Emotion.FEAR, 1f);
    }
    else
    {
      SlimeFaceAnimator component = target.GetComponent<SlimeFaceAnimator>();
      if (component != null)
        component.SetTrigger("triggerLongAwe");
    }
    targetFleeer = target.GetComponent<FleeThreats>();
    if (targetFleeer != null)
      targetFleeer.AddGrappler(this);
    this.target = target;
    AdjustConnector();
  }

  public void Awake()
  {
    tentacleMaterial = tentacleObject.GetComponent<Renderer>().material;
    tentacleMaterial.SetFloat("_Alpha", 0.0f);
    fadeInTime = new float?(Time.time + 0.5f);
    SECTR_AudioSystem.Play(shootCue, parentJoint.transform.position, false);
  }

  public void OnDestroy()
  {
    Destroyer.Destroy(tentacleMaterial, "TentacleHook.OnDestroy");
    if (targetFleeer != null)
      targetFleeer.RemoveGrappler(this);
    if (target != null)
      allHooked.Remove(target);
    allHooked.RemoveAll(x => x == null);
  }

  public void FixedUpdate()
  {
    if (snapping || !(hookJoint == null) && !(parentJoint == null) && !(hookJoint.connectedBody == null) && !(parentJoint.connectedBody == null))
      return;
    Snap();
  }

  private void OnJointBreak(float breakForce)
  {
    if (hookJoint != null && hookJoint.connectedBody == null)
    {
      Destroyer.Destroy(hookJoint, "TentacleHook.OnJointBreak#1");
      hookJoint = null;
      Destroyer.Destroy(hookSafeJoint, "TentacleHook.OnJointBreak#2");
    }
    if (!(parentJoint != null) || !(parentJoint.connectedBody == null))
      return;
    Destroyer.Destroy(parentJoint, "TentacleHook.OnJointBreak#3");
    parentJoint = null;
    Destroyer.Destroy(parentSafeJoint, "TentacleHook.OnJointBreak#4");
  }

  public void Update()
  {
    if (snapping)
    {
      snapProgress = Mathf.Min(1f, snapProgress + Time.deltaTime / 0.2f);
      tentacleMaterial.SetFloat("_Alpha", 1f - snapProgress);
      if (snapProgress < 1.0)
        return;
      Destroyer.Destroy(gameObject, "TentacleHook.Update");
    }
    else
    {
      float time = Time.time;
      if (this.fadeInTime.HasValue)
      {
        double num = time;
        float? fadeInTime = this.fadeInTime;
        double valueOrDefault = fadeInTime.GetValueOrDefault();
        if (num <= valueOrDefault & fadeInTime.HasValue)
        {
          tentacleMaterial.SetFloat("_Alpha", (float) (1.0 - (this.fadeInTime.Value - (double) time) / 0.5));
        }
        else
        {
          tentacleMaterial.SetFloat("_Alpha", 1f);
          if (target != null)
            SECTR_AudioSystem.Play(grabCue, target.transform.position, false);
          this.fadeInTime = new float?();
        }
      }
      if (parentJoint != null && !pauseRetract)
        parentJoint.maxDistance = Mathf.Max(0.0f, parentJoint.maxDistance - Time.deltaTime * retractSpeed);
      AdjustConnector();
    }
  }

  public void SetPauseRetract(bool pauseRetract) => this.pauseRetract = pauseRetract;

  public static bool IsAlreadyHooked(GameObject obj) => allHooked.Contains(obj);

  private void AdjustConnector()
  {
    if (!(hookJoint != null))
      return;
    Vector3 vector3 = hookJoint.transform.position - parentEnd.transform.position;
    if (vector3.sqrMagnitude <= 0.0)
      return;
    parentEnd.transform.forward = vector3;
    hookJoint.transform.forward = vector3;
  }

  private void Snap()
  {
    snapping = true;
    Destroyer.Destroy(hookJoint, "TentacleHook.Snap#1");
    Destroyer.Destroy(parentJoint, "TentacleHook.Snap#2");
    if (!(hookEndObj != null))
      return;
    Rigidbody component = hookEndObj.GetComponent<Rigidbody>();
    if (!(component != null))
      return;
    component.velocity = Vector3.zero;
  }
}
