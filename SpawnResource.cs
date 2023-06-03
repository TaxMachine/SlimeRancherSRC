// Decompiled with JetBrains decompiler
// Type: SpawnResource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SpawnResource : 
  SRBehaviour,
  LiquidConsumer,
  DestroyAfterTimeListener,
  DestroyAfterTimeCondition,
  SpawnResourceModel.Participant
{
  public static IdComparer idComparer = new IdComparer();
  public Id id;
  public GameObject[] ObjectsToSpawn;
  public GameObject[] BonusObjectsToSpawn;
  public float MaxObjectsSpawned = 1f;
  public float MinObjectsSpawned = 1f;
  public float MinNutrientObjectsSpawned = 1f;
  public float MinSpawnIntervalGameHours = 18f;
  public float MaxSpawnIntervalGameHours = 24f;
  public Joint[] SpawnJoints;
  public float BonusChance = 0.01f;
  public int minBonusSelections;
  public GameObject SpawnFX;
  public int MaxActiveSpawns;
  public int MaxTotalSpawns;
  public float wateringDurationHours = 23f;
  public bool forceDestroyLeftoversOnSpawn;
  [Tooltip("GameObject that is shown/hidden based off the watered state. (optional)")]
  public GameObject onWateredFX;
  public UnityAction onReachedSpawnTime;
  private bool allowSpawningInFastForwarding;
  private Queue<SpawnRequest> spawnQueue = new Queue<SpawnRequest>();
  private List<GameObject> spawned = new List<GameObject>();
  private int totalSpawnsRemaining;
  private Randoms rand;
  private TimeDirector timeDir;
  private AmbianceDirector ambianceDir;
  private Region region;
  private LandPlot landPlot;
  private int spawnBlockers;
  private SpawnResourceModel model;
  private const float MAX_WATER_STORED = 3f;
  private const float WATER_USED_PER_HOUR = 0.130434781f;
  private const float WATERED_TIME_FACTOR = 0.5f;
  private const float MIN_BONUS_RIPENESS = 3f;
  private const float MAX_BONUS_RIPENESS = 9f;
  private const float SCALE_IN_RESOURCE_TIME = 4f;
  private const float FAST_FORWARD_MIN_HOURS = 0.25f;
  private const float MIN_SPAWN_TIME_STEP = 1f;

  public double GetNextSpawnTime() => model.nextSpawnTime;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    ambianceDir = SRSingleton<SceneContext>.Instance.AmbianceDirector;
    rand = Randoms.SHARED;
    totalSpawnsRemaining = MaxTotalSpawns;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterResourceSpawner(transform.position, this);
  }

  public void Start()
  {
    landPlot = GetComponentInParent<LandPlot>();
    region = GetComponentInParent<Region>();
    if (landPlot != null)
      allowSpawningInFastForwarding = true;
    else
      allowSpawningInFastForwarding = false;
  }

  public void InitModel(SpawnResourceModel model)
  {
    model.pos = transform.position;
    model.nextSpawnRipens = false;
  }

  public void SetModel(SpawnResourceModel model)
  {
    this.model = model;
    if (model.nextSpawnTime != 0.0)
      return;
    if (GetComponent<SpawnResourceForceFirstRipeness>() != null)
      model.nextSpawnRipens = true;
    else if (timeDir.IsAtStart())
    {
      double num = timeDir.HoursFromNowOrStart(-rand.GetInRange(3f, 9f));
      model.nextSpawnTime = num;
    }
    else
      model.nextSpawnTime = timeDir.WorldTime();
  }

  public void InitAsReplacement(SpawnResource old)
  {
    totalSpawnsRemaining = old.totalSpawnsRemaining;
    if (old.model != null)
    {
      old.model.SetParticipant(this);
      SetModel(old.model);
    }
    foreach (Joint spawnJoint in old.SpawnJoints)
    {
      if (spawnJoint.connectedBody != null)
      {
        Joint joint = NearestJoint(spawnJoint.transform.position, 0.1f);
        if (joint != null)
        {
          spawnJoint.connectedBody.position = joint.transform.position;
          spawnJoint.connectedBody.GetComponent<ResourceCycle>().Reattach(joint);
        }
      }
    }
  }

  public void Update()
  {
    if (region.Hibernated)
      return;
    UpdateToTime(timeDir.WorldTime(), timeDir.DeltaWorldTime());
    if (spawnQueue.Count <= 0)
      return;
    Spawn(spawnQueue.Dequeue());
  }

  public void UpdateToTime(double worldTime, double deltaWorldTime)
  {
    model.storedWater += (float) ((ambianceDir.PrecipitationRate() - 0.1304347813129425) * deltaWorldTime * 0.00027777778450399637);
    model.storedWater = Mathf.Clamp(model.storedWater, 0.0f, 3f);
    if (onWateredFX != null)
      onWateredFX.SetActive(IsWatered());
    if (spawnBlockers > 0)
      return;
    model.nextSpawnTime -= AdditionalRipenessPerSecond() * deltaWorldTime;
    if (!TimeUtil.HasReached(worldTime, model.nextSpawnTime))
      return;
    float num1 = (float) ((worldTime - model.nextSpawnTime) * 0.00027777778450399637);
    if (allowSpawningInFastForwarding && num1 >= 0.25)
    {
      ResourceCycle resourceToSpawn = GetResourceToSpawn();
      float num2 = resourceToSpawn.unripeGameHours + resourceToSpawn.ripeGameHours;
      float num3 = resourceToSpawn.edibleGameHours + resourceToSpawn.rottenGameHours;
      if (landPlot != null && landPlot.HasUpgrade(LandPlot.Upgrade.MIRACLE_MIX))
        num3 *= 2f;
      float stepHours;
      for (; num1 >= 0.0; num1 -= stepHours)
      {
        if (num1 < num2 + (double) num3)
          spawnQueue.Enqueue(new SpawnRequest()
          {
            spawnAtTime = new double?(model.nextSpawnTime)
          });
        stepHours = rand.GetInRange(MinSpawnIntervalGameHours, MaxSpawnIntervalGameHours) * (IsWatered() ? 0.5f : 1f);
        if (stepHours < 1.0)
          stepHours = 1f;
        StepNextSpawnTime(0.0f, stepHours);
      }
    }
    else if (model.nextSpawnRipens)
    {
      spawnQueue.Enqueue(new SpawnRequest()
      {
        spawnRipe = true
      });
      UpdateNextSpawnTime(0.0f);
      model.nextSpawnRipens = false;
    }
    else
    {
      double nextSpawnTime = model.nextSpawnTime;
      float num4 = (float) (rand.GetInRange(MinSpawnIntervalGameHours, MaxSpawnIntervalGameHours) * (IsWatered() ? 0.5 : 1.0) * 3600.0);
      while (nextSpawnTime + num4 < worldTime)
        nextSpawnTime += num4;
      spawnQueue.Enqueue(new SpawnRequest()
      {
        spawnAtTime = new double?(nextSpawnTime)
      });
      UpdateNextSpawnTime(0.0f);
    }
    if (onReachedSpawnTime == null)
      return;
    onReachedSpawnTime();
  }

  private ResourceCycle GetResourceToSpawn() => ObjectsToSpawn[0].GetComponent<ResourceCycle>();

  public void OnDestroy()
  {
    DropFromJoints();
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterResourceSpawner(transform.position);
  }

  public void WillDestroyAfterTime()
  {
    DropFromJoints();
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterResourceSpawner(transform.position);
  }

  private void DropFromJoints()
  {
    foreach (Joint spawnJoint in SpawnJoints)
    {
      if (spawnJoint.connectedBody != null)
      {
        ResourceCycle component = spawnJoint.connectedBody.GetComponent<ResourceCycle>();
        spawnJoint.connectedBody.WakeUp();
        if (component != null)
          component.Detach(AdditionalRipenessPerSecond);
        spawnJoint.connectedBody = null;
      }
    }
  }

  public Identifiable.Id GetPrimarySpawnId()
  {
    if (BonusObjectsToSpawn != null && BonusObjectsToSpawn.Length != 0)
      return BonusObjectsToSpawn[0].GetComponent<Identifiable>().id;
    return ObjectsToSpawn == null || ObjectsToSpawn.Length < 1 ? Identifiable.Id.NONE : ObjectsToSpawn[0].GetComponent<Identifiable>().id;
  }

  public void AddLiquid(Identifiable.Id liquidId, float amount)
  {
    if (!Identifiable.IsWater(liquidId))
      return;
    model.storedWater = Mathf.Min(3f, model.storedWater + amount);
  }

  private bool HasNutrientSoil()
  {
    LandPlot componentInParent = GetComponentInParent<LandPlot>();
    return componentInParent != null && componentInParent.HasUpgrade(LandPlot.Upgrade.SOIL);
  }

  private bool HasSprinkler() => landPlot != null && landPlot.HasUpgrade(LandPlot.Upgrade.SPRINKLER);

  private bool IsWatered() => HasSprinkler() || model.storedWater > 0.0;

  private float AdditionalRipenessPerSecond() => !IsWatered() ? 0.0f : 0.5f;

  private IEnumerable<SpawnMetadata> GetSpawnMetadatas(
    SpawnRequest request,
    double worldTime)
  {
    int num1 = 0;
    if (MaxActiveSpawns != 0)
    {
      spawned.RemoveAll(go => go == null);
      num1 = spawned.Count;
    }
    ReferenceCount<GameObject> source = new ReferenceCount<GameObject>();
    if (MaxActiveSpawns == 0 || num1 < MaxActiveSpawns)
    {
      int num2 = (int) UnityEngine.Random.Range(HasNutrientSoil() ? MinNutrientObjectsSpawned : MinObjectsSpawned, MaxObjectsSpawned);
      for (int index = 0; index < num2; ++index)
      {
        GameObject[] gameObjectArray = BonusObjectsToSpawn == null || BonusObjectsToSpawn.Length < 1 || index >= minBonusSelections && UnityEngine.Random.value >= (double) BonusChance ? ObjectsToSpawn : BonusObjectsToSpawn;
        GameObject key = gameObjectArray[rand.GetInRange(0, gameObjectArray.Length - 1)];
        ResourceCycle component = key.GetComponent<ResourceCycle>();
        if (!request.spawnAtTime.HasValue || !component.WouldProgressToRotten(request.spawnAtTime.Value, worldTime))
          source.Increment(key);
      }
    }
    return source.Select(pair => new SpawnMetadata()
    {
      request = request,
      prefab = pair.Key,
      count = pair.Value
    });
  }

  private void Spawn(SpawnRequest request) => Spawn(request, timeDir.WorldTime());

  private void Spawn(SpawnRequest request, double worldTime) => Spawn(GetSpawnMetadatas(request, worldTime));

  private void Spawn(IEnumerable<SpawnMetadata> metadatas)
  {
    List<Joint> iterable = new List<Joint>();
    foreach (Joint spawnJoint in SpawnJoints)
    {
      if (spawnJoint.connectedBody == null)
        iterable.Add(spawnJoint);
      else if (forceDestroyLeftoversOnSpawn)
      {
        Destroyer.DestroyActor(spawnJoint.connectedBody.gameObject, "SpawnResource.Spawn#1");
        iterable.Add(spawnJoint);
      }
    }
    foreach (SpawnMetadata metadata in metadatas)
    {
      for (int index = 0; index < metadata.count; ++index)
      {
        Joint joint = rand.Pluck(iterable, null);
        if (!(joint == null))
        {
          Vector3 position = joint.transform.position;
          Quaternion rotation = joint.transform.rotation;
          GameObject gameObject = InstantiateActor(metadata.prefab, region.setId, position, rotation);
          ResourceCycle component = gameObject.GetComponent<ResourceCycle>();
          component.Attach(joint, AdditionalRipenessPerSecond);
          if (MaxActiveSpawns != 0)
            spawned.Add(gameObject);
          if (metadata.request.spawnRipe)
            component.ProgressResource(timeDir.WorldTime());
          else if (metadata.request.spawnAtTime.HasValue)
          {
            component.ProgressResource(metadata.request.spawnAtTime.Value + component.unripeGameHours * 3600.0);
          }
          else
          {
            if (SpawnFX != null)
              SpawnAndPlayFX(SpawnFX, position, Quaternion.identity);
            TweenUtil.ScaleIn(gameObject, 4f);
          }
        }
        else
          break;
      }
      if (MaxTotalSpawns != 0)
      {
        totalSpawnsRemaining -= metadata.count;
        if (totalSpawnsRemaining <= 0)
          Destroyer.Destroy(gameObject, "SpawnResource.Spawn#2");
      }
    }
  }

  private void UpdateNextSpawnTime(float preripenedHours) => model.nextSpawnTime = timeDir.HoursFromNow(-preripenedHours + rand.GetInRange(MinSpawnIntervalGameHours, MaxSpawnIntervalGameHours));

  private void StepNextSpawnTime(float ripeness, float stepHours) => model.nextSpawnTime = TimeDirector.HoursFromTime(-ripeness + stepHours, model.nextSpawnTime);

  public void RefreshSpawnJointObjectPositions()
  {
    foreach (FixedJoint spawnJoint in SpawnJoints)
    {
      if (spawnJoint.connectedBody != null)
      {
        spawnJoint.connectedBody.transform.position = spawnJoint.transform.position;
        spawnJoint.connectedBody.transform.rotation = spawnJoint.transform.rotation;
        RegionMember component = spawnJoint.connectedBody.GetComponent<RegionMember>();
        if (component != null)
          component.UpdateRegionMembership(true);
      }
    }
  }

  public Joint PickRipeResourceJoint()
  {
    foreach (Joint spawnJoint in SpawnJoints)
    {
      if (spawnJoint.connectedBody != null && spawnJoint.connectedBody.GetComponent<ResourceCycle>().GetState() == ResourceCycle.State.RIPE)
        return spawnJoint;
    }
    return null;
  }

  public Joint NearestJoint(Vector3 pos, float maxDist)
  {
    float num = maxDist * maxDist;
    Joint joint = null;
    foreach (Joint spawnJoint in SpawnJoints)
    {
      float sqrMagnitude = (spawnJoint.transform.position - pos).sqrMagnitude;
      if (sqrMagnitude < (double) num)
      {
        num = sqrMagnitude;
        joint = spawnJoint;
      }
    }
    return joint;
  }

  private bool AllJointsDisconnected() => SpawnJoints.All(j => j.connectedBody == null);

  public bool ReadyToDestroy()
  {
    model.nextSpawnTime = double.PositiveInfinity;
    return AllJointsDisconnected();
  }

  public void RegisterSpawnBlocker() => ++spawnBlockers;

  public void DeregisterSpawnBlocker() => --spawnBlockers;

  public void FastForward(double startTime, double endTime) => UpdateToTime(endTime, endTime - startTime);

  public IEnumerable<DroneFastForwarder.GatherGroup> GetFastForwardGroups(double endTime) => spawnQueue.SelectMany(r => GetSpawnMetadatas(r, endTime)).GroupBy(m => Identifiable.GetId(m.prefab)).Select(g => new GatherGroup(this, g.Key, g.Aggregate(0, (agg, m) => agg + m.count))).Cast<DroneFastForwarder.GatherGroup>();

  public enum Id
  {
    NONE,
    CUBERRY_TREE,
    MANGO_TREE,
    CARROT_PATCH,
    OCAOCA_PATCH,
    PEAR_TREE,
    POGO_TREE,
    PARSNIP_PATCH,
    BEET_PATCH,
    ONION_PATCH,
    LEMON_TREE,
    GINGER_PATCH,
    CUBERRY_TREE_DLX,
    MANGO_TREE_DLX,
    CARROT_PATCH_DLX,
    OCAOCA_PATCH_DLX,
    PEAR_TREE_DLX,
    POGO_TREE_DLX,
    PARSNIP_PATCH_DLX,
    BEET_PATCH_DLX,
    ONION_PATCH_DLX,
    LEMON_TREE_DLX,
    GINGER_PATCH_DLX,
  }

  public class IdComparer : IEqualityComparer<Id>
  {
    public bool Equals(Id id1, Id id2) => id1 == id2;

    public int GetHashCode(Id obj) => (int) obj;
  }

  private class SpawnRequest
  {
    public double? spawnAtTime;
    public bool spawnRipe;
  }

  private class SpawnMetadata
  {
    public SpawnRequest request;
    public GameObject prefab;
    public int count;
  }

  private class GatherGroup : DroneFastForwarder.GatherGroup
  {
    private SpawnResource resource;
    private Identifiable.Id rid;
    private int rcount;

    public override Identifiable.Id id => rid;

    public override int count => rcount;

    public override bool overflow => false;

    public GatherGroup(SpawnResource resource, Identifiable.Id id, int count)
    {
      this.resource = resource;
      rid = id;
      rcount = count;
    }

    public override void Decrement(int decrement)
    {
      rcount = Mathf.Max(rcount - decrement, 0);
      if (rcount > 0 || !resource.spawnQueue.Any())
        return;
      resource.spawnQueue.Dequeue();
    }

    public override void Dispose() => rcount = 0;
  }
}
