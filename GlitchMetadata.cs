// Decompiled with JetBrains decompiler
// Type: GlitchMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlitchMetadata : ScriptableObject
{
  [Header("Damage Loss Exposure")]
  [Tooltip("Exposure metadata applied when the player receives damage.")]
  public ExposureMetadata damageLossExposure;
  [Tooltip("Time after exposing slimes on player damage before it can occur again. (in-game minutes)")]
  public float damageLossCooldown;
  [Tooltip("FX spawned on the AmmoSlotUI when slimes are removed.")]
  public GameObject damageLossAmmoFX;
  [Header("Glitch Teleporters")]
  [Tooltip("Time before the teleporter out of the SLIMULATIONS is activated. (in-game hours, random range)")]
  public Vector2 teleportActivationDelay;
  [Tooltip("Prefab containing the 'exit teleporter is now active' HUD prefab.")]
  public GameObject teleportHudPrefab;
  [Tooltip("Maximum time to show the 'exit teleporter is now active' HUD image. (seconds)")]
  public float teleportHudLifetime;
  [Header("Debug Spray/Station")]
  [Tooltip("Max ammo override capacity for Identifiable.Id.GLITCH_DEBUG_SPRAY_LIQUID.")]
  public int debugSprayMaxAmmo;
  [Tooltip("Amount of ammo acquired by vaccing a debug spray station.")]
  public int debugSprayAmmoPerStation;
  [Header("Ditto Largo/Slime")]
  [Tooltip("Default ditto metadata used by standard slimes.")]
  public DittoMetadata dittoStandard;
  [Tooltip("Default ditto metadata used by largos.")]
  public DittoMetadata dittoLargo;
  [Tooltip("Dictionary of custom ditto metadata overrides by id.")]
  public List<DittoMetadata_DictEntry> dittoOverrides;
  public Dictionary<Identifiable.Id, DittoMetadata> dittoOverridesDictionary;
  [Header("Glitch Imposto")]
  [Tooltip("Exposure metadata applied to the imposto exposures.")]
  public ExposureMetadata impostoExposure;
  [Tooltip("Time before an imposto is deactivated when the player isn't looking. (in-game minutes)")]
  public float impostoDeactivateTime;
  [Tooltip("Squared distance between the imposto and the player to be considered in detection range.")]
  public float impostoDetectionRange;
  [Tooltip("Time before the imposto is deactivated after coming back into detection range. (seconds)")]
  public float impostoFailedExposedDelayTime;
  [Tooltip("Time that each imposto will cooldown before it is made available again. (in-game hours)")]
  public float impostoCooldownTime;
  [Tooltip("Minimum amount of time that a GlitchImpostoDirector must be hibernated before it resets impostos. (in-game hours)")]
  public float impostoMinHibernationTime;
  [Tooltip("Time between the next flicker animation is played. (in-game minutes, random range)")]
  public Vector2 impostoFlickerCooldownTime;
  [Tooltip("Flicker animation speed. (random range)")]
  public Vector2 impostoFlickerSpeed;
  [Tooltip("Flicker animation radius. (random range)")]
  public Vector2 impostoFlickerRadius;
  [Tooltip("Flicker animation number of points to move to.")]
  public int impostoFlickerPoints;
  [Tooltip("Flicker FX. (optional)")]
  public GameObject impostoFlickerFX;
  [Tooltip("Flicker SFX cue. (optional)")]
  public SECTR_AudioCue impostoFlickerCue;
  [Header("Glitch Slime")]
  [Tooltip("Time the glitch slime is alive. (in-game minutes, random range)")]
  public Vector2 slimeLifetime;
  [Tooltip("Exposure metadata applied to the glitch slime split.")]
  public ExposureMetadata slimeExposure;
  [Tooltip("Base percentage chance that the glitch slime will split.")]
  public float slimeBaseExposureChance;
  [Tooltip("Percentage degradation of the glitch slime split chance each time a split occurs.")]
  public float slimeExposureChanceDegradation;
  [Tooltip("Time before the glitch slime begins fleeing. (seconds)")]
  public float slimeFleeDelay;
  [Tooltip("Max ammo capacity override for Identifiable.Id.GLITCH_SLIME.")]
  public int slimeMaxAmmo;
  [Header("Glitch Tarr Slime")]
  [Tooltip("Lifetime override used by tarr in SLIMULATIONS. (in-game minutes, random range)")]
  public Vector2 tarrLifetime;
  [Tooltip("Base percentage chance that the tarr slime will split into multiple tarr slimes on timed death.")]
  public float tarrBaseMultiplyChance;
  [Tooltip("Percentage degradation that the tarr slime will split into multiple tarr slimes on timed death.")]
  public float tarrMultiplyChanceDegradation;
  [Tooltip("Number of tarr slimes the tarr slime will multiply into on timed death.")]
  public Vector2 tarrMultiplyCount;
  [Header("Glitch Tarr Spawner")]
  [Tooltip("Time between checks of the tarr spawner should spawn more tarr. (in-game minutes)")]
  public float tarrSpawnerThrottleTime;
  [Header("Glitch Tarr Node")]
  [Tooltip("Base delay before more tarr spawners are activated. (in-game hours)")]
  public float tarrNodeActivationDelay;
  [Tooltip("Delay per spawner before the next tarr spawner is activated. (in-game hours)")]
  public float tarrNodeActivationDelayPerNode;
  [Tooltip("Minimum distance the tarr spawner must be from the entrance teleporter to be potentially activated immediately.")]
  public float tarrNodeMinActivationDistanceSquared;
  [Tooltip("Speed that tarr nodes are scaled in on activation.")]
  public float tarrNodeScaleInSpeed;
  [Tooltip("Delay added to tarr nodes that spawn on the player before damage is applied. (in-game minutes)")]
  public float tarrNodeSpawnDamagePreventionTime;
  [Tooltip("Additional multiplier applied to the tarr node music min/max distance.")]
  public float tarrNodeMusicDistanceMultiplier;
  [Header("Audio - Music")]
  [Tooltip("Music associated with Zone.SLIMULATIONS.")]
  public MusicDirector.Music.Zone.Default musicSlimulation;
  [Tooltip("Music associated with Zone.VIKTOR_LAB.")]
  public MusicDirector.Music.Zone.Default musicViktorLab;
  [Header("Ambiance")]
  [Tooltip("Fixed time-of-day in the AmbianceDirector.")]
  [Range(0.0f, 1f)]
  public float ambianceTimeOfDay;
  [Header("Glitch Terminal Animation")]
  [Tooltip("FX prefab used during the player's transition to/from the SLIMULATIONS.")]
  public GameObject animationFX;
  [Tooltip("Material to update the sea renderer with on teleportation in SLIMULATIONS region.")]
  public Material animationSeaMaterial;
  [Tooltip("SFX cue to play when the terminal enters BOOT_UP state. (3D)")]
  public SECTR_AudioCue animationOnTerminalBootupCue;
  [Tooltip("SFX cue to play when the terminal enters IDLE state. (3D, Looping)")]
  public SECTR_AudioCue animationOnTerminalIdleCue;
  [Tooltip("SFX cue to play when the player teleports into the SLIMULATIONS region. (2D)")]
  public SECTR_AudioCue animationOnTeleportInCue;
  [Tooltip("SFX cue to play when the player teleports out of the SLIMULATIONS region. (2D)")]
  public SECTR_AudioCue animationOnTeleportOutCue;
  [Header("Glitch Terminal Activator")]
  [Tooltip("GUI prefab to show when the activator is inactive due to progress.")]
  public GameObject activatorGuiProgress;
  [Tooltip("GUI prefab to show when the activator is inactive due to ammo.")]
  public GameObject activatorGuiAmmo;
  [Tooltip("GUI prefab to show when the activator is activate not but activate-able.")]
  public GameObject activatorGuiPreActive;

  public bool MaybeDamageExposure(GameObject source)
  {
    if (source == null)
      return false;
    switch (Identifiable.GetId(source))
    {
      case Identifiable.Id.GLITCH_TARR_SLIME:
      case Identifiable.Id.GLITCH_TARR_PORTAL:
        PlayerState playerState = SRSingleton<SceneContext>.Instance.PlayerState;
        int? ammoIdx = playerState.Ammo.GetAmmoIdx(Identifiable.Id.GLITCH_SLIME);
        if (!ammoIdx.HasValue)
          return false;
        int count = Mathf.Min(Mathf.FloorToInt(damageLossExposure.spawnCount.GetRandom()), playerState.Ammo.GetSlotCount(ammoIdx.Value));
        SRSingleton<SceneContext>.Instance.Player.GetComponentInChildren<WeaponVacuum>().OnDamageExposure(damageLossExposure, count);
        SRSingleton<AmmoSlotUI>.Instance.SpawnAndPlayFX(damageLossAmmoFX, ammoIdx.Value, count);
        playerState.Ammo.Decrement(ammoIdx.Value, count);
        TimeDirector timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
        playerState.nextAmmoLossDamageTime = timeDirector.HoursFromNow(damageLossCooldown * 0.0166666675f);
        SRSingleton<SceneContext>.Instance.TutorialDirector.MaybeShowPopup(TutorialDirector.Id.SLIMULATIONS_DAMAGE);
        return true;
      default:
        return false;
    }
  }

  public ExposureMetadata GetDittoExposureMetadata(Identifiable.Id id)
  {
    if (dittoOverridesDictionary.ContainsKey(id))
      return dittoOverridesDictionary[id].exposure;
    if (Identifiable.LARGO_CLASS.Contains(id))
      return dittoLargo.exposure;
    return !Identifiable.SLIME_CLASS.Contains(id) ? null : dittoStandard.exposure;
  }

  public float GetDittoProbability(Identifiable.Id id, GlitchSlimeSpawner spawner) => (dittoOverridesDictionary.ContainsKey(id) ? new float?(dittoOverridesDictionary[id].probability) : (Identifiable.LARGO_CLASS.Contains(id) ? new float?(spawner.probablityLargo.GetOrDefault(dittoLargo.probability)) : (Identifiable.SLIME_CLASS.Contains(id) ? new float?(spawner.probablityStandard.GetOrDefault(dittoStandard.probability)) : new float?()))) ?? 0.0f;

  public void OnEnable()
  {
    if (dittoOverrides == null)
      return;
    dittoOverridesDictionary = dittoOverrides.ToDictionary(e => e.id, e => (DittoMetadata) e, Identifiable.idComparer);
  }

  [Serializable]
  public class ExposureMetadata
  {
    [Tooltip("Random range of number of glitch slimes to spawn.")]
    public Vector2 spawnCount;
    [Tooltip("Additional offset applied to the spawn position as a random position in a sphere.")]
    public float spawnRadius;
    [Tooltip("Speed to spawn the glitch slime with.")]
    public float velocity;
    [Tooltip("Rotation applied to the velocity vector around the X axis. (random range)")]
    public Vector2 velocityRotationX;
    [Tooltip("Rotation applied to the velocity vector around the Y axis. (random range)")]
    public Vector2 velocityRotationY;
    [Tooltip("Time after spawn that the glitch slimes will not be capturable. (seconds)")]
    public float capturePreventionTime;
    [Tooltip("FX played on the glitch slime when it is not capturable. (optional)")]
    public GameObject capturePreventionFX;
    [Tooltip("FX played when the exposure occurs. (optional)")]
    public GameObject onExposedFX;
    [Tooltip("SFX played when the exposure occurs. (optional)")]
    public SECTR_AudioCue onExposedSFX;

    public void OnExposed(
      GameObject gameObject = null,
      Vector3? origin = null,
      GetPositionAndVelocity getPositionAndVelocity = null,
      int? count = null,
      GameObject source = null,
      OnInstantiated onInstantiated = null)
    {
      onInstantiated = onInstantiated != null ? onInstantiated : go => { };
      count = new int?(count.HasValue ? count.Value : Mathf.FloorToInt(spawnCount.GetRandom()));
      source = source != null ? source : SRSingleton<SceneContext>.Instance.Player;
      origin = new Vector3?(origin.HasValue ? origin.Value : gameObject.transform.position);
      if (getPositionAndVelocity == null)
      {
        Vector3 sourceForward = source.transform.forward;
        getPositionAndVelocity = (out Vector3 position, out Vector3 velocity) =>
        {
          Vector3 normalized = (Quaternion.Euler(velocityRotationX.GetRandom(), velocityRotationY.GetRandom(), 0.0f) * sourceForward).normalized;
          position = origin.Value + UnityEngine.Random.insideUnitSphere * spawnRadius;
          velocity = normalized * this.velocity;
        };
      }
      SRSingleton<SceneContext>.Instance.StartCoroutine(OnExposed_Coroutine(gameObject, getPositionAndVelocity, count.Value, source.transform.position, onInstantiated));
    }

    private IEnumerator OnExposed_Coroutine(
      GameObject gameObject,
      GetPositionAndVelocity getPositionAndVelocity,
      int count,
      Vector3 sourcePosition,
      OnInstantiated onInstantiated)
    {
      GameObject prefab = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.GLITCH_SLIME);
      for (int ii = 0; ii < count; ++ii)
      {
        Vector3 position;
        Vector3 velocity;
        getPositionAndVelocity(out position, out velocity);
        GameObject gameObject1 = SRBehaviour.InstantiateActor(prefab, RegionRegistry.RegionSetId.SLIMULATIONS, position, Quaternion.identity);
        gameObject1.transform.LookAt(sourcePosition);
        PhysicsUtil.RestoreFreezeRotationConstraints(gameObject1);
        gameObject1.GetComponent<Rigidbody>().velocity = velocity;
        gameObject1.GetComponent<Vacuumable>().PreventCaptureFor(capturePreventionTime);
        if (capturePreventionFX != null)
          Destroyer.Destroy(SRBehaviour.SpawnAndPlayFX(capturePreventionFX, gameObject1), capturePreventionTime, "GlitchMetadata.OnExposed");
        float fromValue = gameObject1.transform.localScale.x * 0.2f;
        gameObject1.transform.DOScale(gameObject1.transform.localScale, 0.2f).From(fromValue).SetEase(Ease.Linear);
        onInstantiated(gameObject1);
        SpawnAndPlayFX(gameObject);
        yield return new WaitForSeconds(0.02f);
      }
    }

    public void OnFailedExposed(GameObject gameObject) => SpawnAndPlayFX(gameObject);

    private void SpawnAndPlayFX(GameObject gameObject)
    {
      if (gameObject == null)
        return;
      if (onExposedFX != null)
        SRBehaviour.SpawnAndPlayFX(onExposedFX, gameObject.transform.position, Quaternion.identity);
      SECTR_AudioSystem.Play(onExposedSFX, gameObject.transform.position, false);
    }

    public delegate void OnInstantiated(GameObject instance);

    public delegate void GetPositionAndVelocity(out Vector3 position, out Vector3 velocity);
  }

  [Serializable]
  public class DittoMetadata
  {
    [Tooltip("Probability that the spawned slime will become a ditto.")]
    public float probability;
    [Tooltip("Exposure metadata when this ditto is exposed.")]
    public ExposureMetadata exposure;
  }

  [Serializable]
  public class DittoMetadata_DictEntry : DittoMetadata
  {
    [Tooltip("Identifiable id to override the ditto metadata for.")]
    public Identifiable.Id id;
  }
}
