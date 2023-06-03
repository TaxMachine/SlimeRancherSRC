// Decompiled with JetBrains decompiler
// Type: Drone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AttachFashions))]
[RequireComponent(typeof (DroneMovement))]
[RequireComponent(typeof (DroneNoClip))]
[RequireComponent(typeof (DroneSubbehaviourPlexer))]
[RequireComponent(typeof (KeepUpright))]
[RequireComponent(typeof (RegionMember))]
[RequireComponent(typeof (Rigidbody))]
public class Drone : SRBehaviour, GadgetModel.Participant
{
  [Tooltip("Transform guide: resting position.")]
  public Transform guideRest;
  [Tooltip("Transform guide: ammo dump spawn position.")]
  public Transform guideDumpSpawn;
  private DroneModel droneModel;

  public AttachFashions fashion { get; private set; }

  public DroneAmmo ammo { get; private set; }

  public DroneAnimator animator { get; private set; }

  public DroneAudioOnActive onActiveCue { get; private set; }

  public DroneGadget gadget { get; private set; }

  public DroneMetadata metadata => gadget.metadata;

  public DroneMovement movement { get; private set; }

  public DroneNetwork network { get; private set; }

  public DroneNoClip noClip { get; private set; }

  public DroneStation station => gadget.station;

  public DroneSubbehaviourPlexer plexer { get; private set; }

  public KeepUpright upright { get; private set; }

  public Region region => gadget.region;

  public RegionMember regionMember { get; private set; }

  public Rigidbody rigidbody { get; private set; }

  public TrailRenderer trail { get; private set; }

  public event OnDestroyed onDestroyed;

  public void Awake()
  {
    gadget = GetComponentInParent<DroneGadget>();
    animator = GetComponentInChildren<DroneAnimator>();
    trail = GetComponentInChildren<TrailRenderer>();
    plexer = GetComponent<DroneSubbehaviourPlexer>();
    movement = GetComponent<DroneMovement>();
    fashion = GetComponent<AttachFashions>();
    upright = GetComponent<KeepUpright>();
    noClip = GetComponent<DroneNoClip>();
    regionMember = GetComponent<RegionMember>();
    rigidbody = GetComponent<Rigidbody>();
    onActiveCue = SFX(metadata.onActiveCue);
    ammo = new DroneAmmo();
    regionMember.regionsChanged += OnRegionsChanged;
  }

  public void OnDestroy()
  {
    regionMember.regionsChanged -= OnRegionsChanged;
    if (network != null)
    {
      network.Deregister(this);
      network = null;
    }
    if (onDestroyed == null)
      return;
    onDestroyed();
  }

  public void InitModel(GadgetModel model)
  {
    DroneModel droneModel = (DroneModel) model;
    ammo.InitModel(droneModel.ammo);
    droneModel.position = transform.position;
    droneModel.rotation = transform.rotation;
    droneModel.currRegionSetId = region.setId;
  }

  public void SetModel(GadgetModel model)
  {
    droneModel = (DroneModel) model;
    ammo.SetModel(((DroneModel) model).ammo);
    transform.position = droneModel.position;
    transform.rotation = droneModel.rotation;
    network = GetComponentInParent<DroneNetwork>();
    network.Register(this);
  }

  public void LateUpdate()
  {
    droneModel.position = transform.position;
    droneModel.rotation = transform.rotation;
  }

  public void OnCollisionEnter(Collision collision)
  {
    switch (Identifiable.GetId(collision.gameObject))
    {
      case Identifiable.Id.NONE:
        break;
      case Identifiable.Id.PLAYER:
        break;
      default:
        SECTR_AudioSystem.Play(metadata.onBoppedCue, transform.position, false);
        break;
    }
  }

  public DroneUI InstantiateDroneUI() => Instantiate(metadata.droneUI.gameObject).GetComponent<DroneUI>().Init(gadget);

  public DroneAudioOnActive SFX(SECTR_AudioCue cue) => gameObject.AddComponent<DroneAudioOnActive>().Init(cue);

  public DroneProgram.Orientation GetRestingOrientation() => new DroneProgram.Orientation(station.guideRest.position - guideRest.localPosition, station.guideRest.rotation);

  public void TeleportToStation(bool includeFX)
  {
    if (includeFX && metadata.onTeleportFX != null)
      SpawnAndPlayFX(metadata.onTeleportFX, transform.position, transform.rotation);
    rigidbody.velocity = Vector3.zero;
    rigidbody.angularVelocity = Vector3.zero;
    DroneProgram.Orientation restingOrientation = GetRestingOrientation();
    transform.position = restingOrientation.pos;
    transform.rotation = restingOrientation.rot;
    trail.Clear();
  }

  public void ForceResting(bool includeFX)
  {
    ammo.Clear();
    TeleportToStation(includeFX);
    plexer.ForceResting();
  }

  public void OnGadgetDestroyed()
  {
    if (network != null)
    {
      network.Deregister(this);
      network = null;
    }
    if (!ammo.Any())
      return;
    transform.SetParent(SRSingleton<DynamicObjectContainer>.Instance.transform, true);
    plexer.ForceDumpAmmo(true);
  }

  private void OnRegionsChanged(List<Region> exited, List<Region> joined)
  {
    if (exited == null || !exited.Contains(region))
      return;
    ForceResting(true);
    regionMember.UpdateRegionMembership(true);
  }

  public delegate void OnDestroyed();
}
