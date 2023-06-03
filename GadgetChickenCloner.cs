// Decompiled with JetBrains decompiler
// Type: GadgetChickenCloner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using Assets.Script.Util.Extensions;
using UnityEngine;

public class GadgetChickenCloner : SRBehaviour
{
  [Tooltip("GameObject to be rotated along the z-axis; animation property.")]
  public GameObject spinner;
  [Tooltip("GameObject representing the blurred spinner; activated/deactivated based off current speed.")]
  public GameObject spinnerBlur;
  [Tooltip("Rotation speed of the 'spinner' GameObject; animation property.")]
  public float spinnerRotationSpeed;
  [Tooltip("FX played when a chicken enters the cloner, and is successfully cloned.")]
  public GameObject onSuccessFX;
  [Tooltip("FX played when a chicken enters the cloner, and is not cloned.")]
  public GameObject onFailureFX;
  [Tooltip("SFX played when then cloning animation begins. (non-looping)")]
  public SECTR_AudioCue onAnimationStartSFX;
  [Tooltip("SFX played while the cloning animation runs. (looping)")]
  public SECTR_AudioCue onAnimationRunSFX;
  [Tooltip("SFX played when then cloning animation ends. (non-looping)")]
  public SECTR_AudioCue onAnimationEndSFX;
  private static readonly int ANIMATION_ACTIVE = Animator.StringToHash("ACTIVE");
  private static readonly float MIN_BLUR_ROTATION_SPEED = 400f;
  private TimeDirector timeDirector;
  private SECTR_PointSource audioSource;
  private Animator animator;
  private HashSet<GameObject> ignored = new HashSet<GameObject>();
  private double animatorDeactivateTime;

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    animator = GetComponentInParent<Animator>();
    animator.SetBool(ANIMATION_ACTIVE, false);
    audioSource = GetComponent<SECTR_PointSource>();
    spinnerBlur.SetActive(false);
  }

  public void Update()
  {
    if (timeDirector.OnPassedTime(animatorDeactivateTime))
    {
      animator.SetBool(ANIMATION_ACTIVE, false);
      audioSource.Cue = onAnimationEndSFX;
      audioSource.Play();
    }
    if (Mathf.Approximately(spinnerRotationSpeed, 0.0f))
      return;
    spinner.transform.Rotate(0.0f, 0.0f, Time.deltaTime * spinnerRotationSpeed);
    spinnerBlur.SetActive(spinnerRotationSpeed >= (double) MIN_BLUR_ROTATION_SPEED);
  }

  public void OnTriggerEnter(Collider collider)
  {
    if (!Identifiable.MEAT_CLASS.Contains(Identifiable.GetId(collider.gameObject)) || !ignored.Add(collider.gameObject))
      return;
    Destroyer.DestroyActor(collider.gameObject, "GadgetChickenCloner.OnTriggerEnter");
    Quaternion rotation = Quaternion.LookRotation(Vector3.Angle(collider.gameObject.GetComponent<Rigidbody>().velocity, transform.forward) > 90.0 ? Vector3.back : Vector3.forward);
    if (!animator.GetBool(ANIMATION_ACTIVE))
    {
      audioSource.Cue = onAnimationStartSFX;
      audioSource.Play();
      audioSource.Cue = onAnimationRunSFX;
      audioSource.Play();
    }
    animator.SetBool(ANIMATION_ACTIVE, true);
    animatorDeactivateTime = timeDirector.HoursFromNow(0.0166666675f);
    if (Randoms.SHARED.GetProbability(0.5f))
    {
      SpawnAndPlayFX(onSuccessFX, this.gameObject, Vector3.zero, rotation);
      RegionRegistry.RegionSetId setId = collider.gameObject.GetComponent<RegionMember>().setId;
      GameObject prefab = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.GetId(collider.gameObject));
      GameObject[] gameObjectArray = new GameObject[2];
      List<Identifiable.Id> allFashions = collider.gameObject.GetRequiredComponent<AttachFashions>().GetAllFashions();
      for (int index = 0; index < gameObjectArray.Length; ++index)
      {
        GameObject gameObject = InstantiateActor(prefab, setId, transform.position, transform.rotation * rotation);
        gameObjectArray[index] = gameObject;
        gameObject.GetRequiredComponent<Vacuumable>().Launch(Vacuumable.LaunchSource.CHICKEN_CLONER);
        gameObject.GetRequiredComponent<AttachFashions>().SetFashions(allFashions);
        gameObject.GetComponent<Rigidbody>().velocity = (Quaternion.Euler(0.0f, index == 0 ? 20f : -20f, 0.0f) * gameObject.transform.forward).normalized * 10f;
        gameObject.transform.DOScale(gameObject.transform.localScale, 0.2f).From(0.2f).SetEase(Ease.Linear);
        ignored.Add(gameObject);
      }
      PhysicsUtil.IgnoreCollision(gameObjectArray[0], gameObjectArray[1], 0.2f);
    }
    else
      SpawnAndPlayFX(onFailureFX, this.gameObject, Vector3.zero, rotation);
  }

  public void OnTriggerExit(Collider collider) => ignored.RemoveWhere(go => go == null || go == collider.gameObject);
}
