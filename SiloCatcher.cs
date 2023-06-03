// Decompiled with JetBrains decompiler
// Type: SiloCatcher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using Assets.Script.Util.Extensions;
using UnityEngine;

public class SiloCatcher : SRBehaviour, VacShootAccelerator
{
  [Tooltip("Input/output type.")]
  public Type type;
  [Tooltip("FX played on successful input.")]
  public GameObject storeFX;
  [Tooltip("Index in SiloStorage to input/output the object. (SILO_DEFAULT, SILO_OUTPUT_ONLY")]
  public int slotIdx;
  private bool hasStarted;
  private HashSet<GameObject> collectedThisFrame = new HashSet<GameObject>();
  private SiloStorage storageSilo;
  private DroneNetwork droneNetwork;
  private SECTR_AudioSource audioSource;
  private DecorizerStorage storageDecorizer;
  private GlitchStorage storageGlitch;
  private VacAccelerationHelper accelerationInput = VacAccelerationHelper.CreateInput();
  private const float EJECT_RATE = 0.25f;
  private const float EJECT_DIST = 1.2f;
  private const float MAX_ANGLE_DEGS = 45f;
  private float nextEject;
  private WeaponVacuum vac;
  private Region region;
  private VacAccelerationHelper accelerationOutput = VacAccelerationHelper.CreateOutput();

  public void Awake()
  {
    if (type.HasInput())
      audioSource = GetRequiredComponent<SECTR_AudioSource>();
    if (type.HasOutput())
      region = GetRequiredComponentInParent<Region>();
    switch (type)
    {
      case Type.SILO_DEFAULT:
      case Type.SILO_OUTPUT_ONLY:
        storageSilo = GetRequiredComponentInParent<SiloStorage>();
        break;
      case Type.REFINERY:
        droneNetwork = GetComponentInParent<DroneNetwork>();
        if (!(droneNetwork != null))
          break;
        droneNetwork.Register(this);
        break;
      case Type.DECORIZER:
        storageDecorizer = GetRequiredComponentInParent<DecorizerStorage>();
        break;
      case Type.VIKTOR_STORAGE:
        storageGlitch = GetRequiredComponentInParent<GlitchStorage>();
        break;
    }
  }

  public void Start()
  {
    if (type.HasOutput())
      vac = SRSingleton<SceneContext>.Instance.Player.GetRequiredComponentInChildren<WeaponVacuum>();
    hasStarted = true;
  }

  public void OnDestroy()
  {
    if (!(droneNetwork != null))
      return;
    droneNetwork.Deregister(this);
    droneNetwork = null;
  }

  public void OnTriggerEnter(Collider collider)
  {
    if (!hasStarted || !isActiveAndEnabled || collider.isTrigger || !type.HasInput())
      return;
    Identifiable.Id id = Identifiable.GetId(collider.gameObject);
    if (id == Identifiable.Id.NONE)
      return;
    Vacuumable component1 = collider.gameObject.GetComponent<Vacuumable>();
    if (component1 == null || component1.isCaptive() || !collectedThisFrame.Add(collider.gameObject) || !Insert(id))
      return;
    DestroyOnTouching component2 = collider.gameObject.GetComponent<DestroyOnTouching>();
    if (component2 != null)
      component2.NoteDestroying();
    Destroyer.DestroyActor(collider.gameObject, "BaseCatcher.OnTriggerEnter");
    SpawnAndPlayFX(storeFX, collider.gameObject.transform.position, collider.gameObject.transform.rotation);
    audioSource.Play();
    accelerationInput.OnTriggered();
  }

  public void LateUpdate() => collectedThisFrame.Clear();

  private bool Insert(Identifiable.Id id)
  {
    switch (type)
    {
      case Type.SILO_DEFAULT:
        return storageSilo.MaybeAddIdentifiable(id, slotIdx);
      case Type.REFINERY:
        return SRSingleton<SceneContext>.Instance.GadgetDirector.AddToRefinery(id);
      case Type.DECORIZER:
        return storageDecorizer.Add(id);
      case Type.VIKTOR_STORAGE:
        return storageGlitch.Add(id);
      default:
        throw new ArgumentException(type.ToString());
    }
  }

  public float GetVacShootSpeedFactor() => accelerationInput.Factor;

  public void OnTriggerStay(Collider collider)
  {
    if (!hasStarted || !isActiveAndEnabled || !type.HasOutput() || Time.time < (double) nextEject)
      return;
    SiloActivator componentInParent = collider.gameObject.GetComponentInParent<SiloActivator>();
    if (componentInParent == null || !componentInParent.enabled)
      return;
    Vector3 normalized = (collider.gameObject.transform.position - transform.position).normalized;
    Identifiable.Id id;
    if (Mathf.Abs(Vector3.Angle(transform.forward, normalized)) > 45.0 || !Remove(out id))
      return;
    vac.ForceJoint(InstantiateActor(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(id), region.setId, transform.position + normalized * 1.2f, transform.rotation).GetComponent<Vacuumable>());
    nextEject = Time.time + 0.25f / accelerationOutput.Factor;
    accelerationOutput.OnTriggered();
  }

  private bool Remove(out Identifiable.Id id)
  {
    switch (type)
    {
      case Type.SILO_DEFAULT:
      case Type.SILO_OUTPUT_ONLY:
        Ammo relevantAmmo = storageSilo.GetRelevantAmmo();
        relevantAmmo.SetAmmoSlot(slotIdx);
        id = relevantAmmo.GetSelectedId();
        if (id == Identifiable.Id.NONE)
          return false;
        relevantAmmo.DecrementSelectedAmmo();
        return true;
      case Type.DECORIZER:
        return storageDecorizer.Remove(out id);
      case Type.VIKTOR_STORAGE:
        return storageGlitch.Remove(out id);
      default:
        throw new ArgumentException(type.ToString());
    }
  }

  public enum Type
  {
    SILO_DEFAULT,
    SILO_OUTPUT_ONLY,
    REFINERY,
    DECORIZER,
    VIKTOR_STORAGE,
  }
}
