// Decompiled with JetBrains decompiler
// Type: DirectedActorSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirectedActorSpawner : SRBehaviour
{
  [Tooltip("An effect to play along with each spawn.")]
  public GameObject spawnFX;
  [Tooltip("The size of the area in which to do the spawning.")]
  public float radius = 5f;
  [Tooltip("Adjusts how much time between the actors being spawned.")]
  public float spawnDelayFactor = 1f;
  [Tooltip("Whether we should immediately enable toteming of spawned actors.")]
  public bool enableToteming;
  public SpawnConstraint[] constraints;
  public GameObject[] spawnLocs;
  public bool allowDirectedSpawns = true;
  public float directedSpawnWeight = 1f;
  protected TimeDirector timeDir;
  private Region region;
  protected const float POP_FORCE = 10f;
  protected const float POP_ROTATE_MAX = 20f;

  public virtual void Awake() => timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;

  public virtual void Start()
  {
    Register(GetComponentInParent<CellDirector>());
    region = GetComponentInParent<Region>();
  }

  protected abstract void Register(CellDirector cellDir);

  protected virtual GameObject MaybeReplacePrefab(GameObject prefab) => prefab;

  public virtual bool CanSpawn(float? forHour = null) => !region.Hibernated && GetEligibleConstraints(forHour).Count > 0;

  protected virtual bool CanContinueSpawning() => true;

  public bool CanSpawnSomething()
  {
    float currHour = timeDir.CurrHour();
    foreach (SpawnConstraint constraint in constraints)
    {
      if (constraint.InWindow(currHour))
        return true;
    }
    return false;
  }

  public virtual IEnumerator Spawn(int count, Randoms rand)
  {
    DirectedActorSpawner directedActorSpawner = this;
    Dictionary<SpawnConstraint, float> weightMap1 = new Dictionary<SpawnConstraint, float>();
    float currHour = directedActorSpawner.timeDir.CurrHour();
    foreach (SpawnConstraint constraint in directedActorSpawner.constraints)
    {
      if (constraint.InWindow(currHour))
        weightMap1[constraint] = constraint.weight;
    }
    if (weightMap1.Count > 0)
    {
      SpawnConstraint spawnConstraint = rand.Pick(weightMap1, null);
      SlimeSet slimeset = spawnConstraint.slimeset;
      bool feral = spawnConstraint.feral;
      bool maxAgitation = spawnConstraint.maxAgitation;
      for (int ii = 0; ii < count && directedActorSpawner.CanContinueSpawning(); ++ii)
      {
        Dictionary<GameObject, float> weightMap2 = new Dictionary<GameObject, float>();
        foreach (SlimeSet.Member member in slimeset.members)
          weightMap2[member.prefab] = member.weight;
        GameObject prefab = rand.Pick(weightMap2, null);
        if (prefab == null)
        {
          Log.Warning("Spawner slimeset select with no choices? Skipping.");
          break;
        }
        GameObject original = directedActorSpawner.MaybeReplacePrefab(prefab);
        GameObject gameObject = directedActorSpawner.spawnLocs == null || directedActorSpawner.spawnLocs.Length == 0 ? null : rand.Pick(directedActorSpawner.spawnLocs);
        Vector3 vector3 = gameObject == null ? directedActorSpawner.transform.position + directedActorSpawner.transform.rotation * directedActorSpawner.GetSpawnOffset(rand) * directedActorSpawner.radius : gameObject.transform.position;
        Quaternion rotation = gameObject == null ? directedActorSpawner.transform.rotation : gameObject.transform.rotation;
        GameObject spawnedObj = InstantiateActor(original, directedActorSpawner.region.setId, vector3, rotation);
        if (feral)
        {
          SlimeFeral component1 = spawnedObj.GetComponent<SlimeFeral>();
          if (component1 != null)
          {
            Vacuumable component2 = spawnedObj.GetComponent<Vacuumable>();
            if (component2 != null && component2.size != Vacuumable.Size.NORMAL)
              component1.SetFeral();
            else
              Log.Warning("Normal sized slimes cannot be made feral, but trying to mark as such.");
          }
          else
            Log.Warning("Slime has no feral behavior, but trying to mark as such.");
        }
        if (maxAgitation)
        {
          SlimeEmotions component = spawnedObj.GetComponent<SlimeEmotions>();
          if (component != null)
            component.Adjust(SlimeEmotions.Emotion.AGITATION, 1f);
        }
        if (directedActorSpawner.enableToteming)
        {
          TotemLinker componentInChildren = spawnedObj.GetComponentInChildren<TotemLinker>();
          if (componentInChildren != null)
            componentInChildren.SetStackReceptive(true);
        }
        foreach (SpawnListener componentsInChild in spawnedObj.GetComponentsInChildren<SpawnListener>(true))
          componentsInChild.DidSpawn();
        directedActorSpawner.SpawnFX(spawnedObj, vector3);
        Vector3 force = rotation * Vector3.up * 10f;
        Vector3 torque = rotation * Vector3.up * rand.GetInRange(-20f, 20f);
        Rigidbody component3 = spawnedObj.GetComponent<Rigidbody>();
        component3.AddForce(force, ForceMode.VelocityChange);
        component3.AddTorque(torque, ForceMode.VelocityChange);
        directedActorSpawner.OnActorSpawned(spawnedObj);
        yield return new WaitForSeconds(rand.GetInRange(0.1f, 0.3f) * directedActorSpawner.spawnDelayFactor);
      }
      slimeset = null;
    }
  }

  protected virtual void OnActorSpawned(GameObject spawnedObj)
  {
  }

  protected virtual void SpawnFX(GameObject spawnedObj, Vector3 pos)
  {
    if (!(spawnFX != null))
      return;
    SpawnAndPlayFX(spawnFX, pos, Quaternion.identity);
  }

  private Vector3 GetSpawnOffset(Randoms rand)
  {
    UnityEngine.Random.InitState(rand.GetInt());
    Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle;
    return new Vector3(insideUnitCircle.x, 0.0f, insideUnitCircle.y);
  }

  public List<SpawnConstraint> GetEligibleConstraints(float? forHour)
  {
    List<SpawnConstraint> eligibleConstraints = new List<SpawnConstraint>();
    foreach (SpawnConstraint constraint in constraints)
    {
      if (forHour.HasValue && constraint.InWindow(forHour.Value))
        eligibleConstraints.Add(constraint);
    }
    return eligibleConstraints;
  }

  public enum TimeMode
  {
    ANY,
    DAY,
    NIGHT,
    CUSTOM,
  }

  [Serializable]
  public class TimeWindow
  {
    public TimeMode timeMode;
    public float startHour;
    public float endHour = 24f;

    public float Start()
    {
      switch (timeMode)
      {
        case TimeMode.ANY:
          return 0.0f;
        case TimeMode.DAY:
          return 6f;
        case TimeMode.NIGHT:
          return 18f;
        default:
          return startHour;
      }
    }

    public float End()
    {
      switch (timeMode)
      {
        case TimeMode.ANY:
          return 24f;
        case TimeMode.DAY:
          return 18f;
        case TimeMode.NIGHT:
          return 6f;
        default:
          return endHour;
      }
    }
  }

  [Serializable]
  public class SpawnConstraint
  {
    public SlimeSet slimeset;
    public float weight = 1f;
    public TimeWindow window;
    public bool feral;
    public bool maxAgitation;

    public bool InWindow(float currHour)
    {
      float num1 = window.Start();
      float num2 = window.End();
      if (num2 >= (double) num1 && num1 <= (double) currHour && currHour <= (double) num2)
        return true;
      if (num2 >= (double) num1)
        return false;
      return currHour <= (double) num2 || currHour >= (double) num1;
    }
  }
}
