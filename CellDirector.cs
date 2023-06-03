// Decompiled with JetBrains decompiler
// Type: CellDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellDirector : SRBehaviour
{
  public const string RANCH_HOME = "cellRanch_Home";
  public const string RANCH_LAB = "cellRanch_Lab";
  public int cullSlimesLimit = 250;
  public int cullAnimalsLimit = 50;
  public int targetSlimeCount = 100;
  public int targetAnimalCount = 20;
  public int minPerSpawn = 3;
  public int maxPerSpawn = 5;
  public int minAnimalPerSpawn = 3;
  public int maxAnimalPerSpawn = 5;
  public float despawnFactor = 1.2f;
  public float avgSpawnTimeGameHours = 2f;
  public bool isRanch;
  public bool isHomeRanch;
  public bool isWilds;
  public AmbianceDirector.Zone ambianceZone;
  public bool ignoreCoopCorralAnimals;
  public OnSlimeAdded onSlimeAdded;
  public OnSlimeRemoved onSlimeRemoved;
  public bool notShownOnMap;
  private List<DirectedSlimeSpawner> spawners = new List<DirectedSlimeSpawner>();
  private List<DirectedAnimalSpawner> animalSpawners = new List<DirectedAnimalSpawner>();
  protected Randoms rand;
  public GameObjectActorModelIdentifiableIndex identifiableIndex = new GameObjectActorModelIdentifiableIndex();
  protected int tarrSlimeCount;
  private double nextSpawn = double.PositiveInfinity;
  private TimeDirector timeDir;
  private GameObject player;
  private ZoneDirector zoneDirector;
  private float spawnThrottleTime;
  private const float SPAWN_THROTTLE_DELAY = 1f;
  private const float PCT_OF_TARGET_TO_DESPAWN = 0.1f;
  private const float PROB_DESTROY_ON_SLEEP = 0.5f;
  private static List<CellDirector> allCellDirectors = new List<CellDirector>();
  private static HashSet<GameObject> selectedGameObjects = new HashSet<GameObject>();
  private static HashSet<GameObjectActorModelIdentifiableIndex.Entry> selectedGameObjectEntries = new HashSet<GameObjectActorModelIdentifiableIndex.Entry>();

  public Region region { get; private set; }

  public virtual void Awake()
  {
    allCellDirectors.Add(this);
    rand = new Randoms();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    nextSpawn = timeDir.HoursFromNowOrStart(-avgSpawnTimeGameHours * targetSlimeCount / maxPerSpawn);
    region = GetComponent<Region>();
    zoneDirector = GetComponentInParent<ZoneDirector>();
  }

  public void OnDestroy() => allCellDirectors.Remove(this);

  public ZoneDirector.Zone GetZoneId() => zoneDirector != null ? zoneDirector.zone : ZoneDirector.Zone.NONE;

  public void Start() => player = SRSingleton<SceneContext>.Instance.Player;

  public virtual void ForceCheckSpawn()
  {
    spawnThrottleTime = 0.0f;
    nextSpawn = 0.0;
  }

  public void Update()
  {
    if (Time.time < (double) spawnThrottleTime || region.Hibernated)
      return;
    UpdateToTime(timeDir.WorldTime());
    if (identifiableIndex.GetSlimes().Count() > cullSlimesLimit)
      Despawn(identifiableIndex.GetSlimes(), identifiableIndex.GetSlimes().Count() - cullSlimesLimit);
    if (identifiableIndex.GetAnimals().Count() > cullAnimalsLimit)
      Despawn(identifiableIndex.GetAnimals(), identifiableIndex.GetAnimals().Count() - cullAnimalsLimit);
    spawnThrottleTime = Time.time + 1f;
  }

  protected virtual void UpdateToTime(double worldTime)
  {
    if (!TimeUtil.HasReached(worldTime, nextSpawn))
      return;
    if (spawners.Count > 0 && NeedsMoreSlimes() && CanSpawnSlimes())
    {
      Dictionary<DirectedSlimeSpawner, float> weightMap = new Dictionary<DirectedSlimeSpawner, float>();
      float num1 = 0.0f;
      foreach (DirectedSlimeSpawner spawner in spawners)
      {
        if (spawner.CanSpawn(new float?()))
        {
          weightMap[spawner] = spawner.directedSpawnWeight;
          num1 += spawner.directedSpawnWeight;
        }
      }
      if (weightMap.Count > 0 && num1 > 0.0)
      {
        DirectedSlimeSpawner directedSlimeSpawner = rand.Pick(weightMap, null);
        float num2 = SRSingleton<SceneContext>.Instance.ModDirector.SlimeCountFactor();
        StartCoroutine(directedSlimeSpawner.Spawn(Mathf.RoundToInt(rand.GetInRange(minPerSpawn, maxPerSpawn + 1) * num2), rand));
      }
    }
    else if (HasTooManySlimes())
      Despawn(identifiableIndex.GetSlimes(), Mathf.CeilToInt(0.1f * targetSlimeCount));
    if (animalSpawners.Count > 0 && NeedsMoreAnimals())
    {
      List<DirectedAnimalSpawner> iterable = new List<DirectedAnimalSpawner>();
      foreach (DirectedAnimalSpawner animalSpawner in animalSpawners)
      {
        if (animalSpawner.CanSpawn(new float?()))
          iterable.Add(animalSpawner);
      }
      if (iterable.Count > 0)
        StartCoroutine(rand.Pick(iterable, null).Spawn(rand.GetInRange(minAnimalPerSpawn, maxAnimalPerSpawn + 1), rand));
    }
    else if (HasTooManyAnimals())
      Despawn(identifiableIndex.GetAnimals(), Mathf.CeilToInt(0.1f * targetAnimalCount));
    nextSpawn += avgSpawnTimeGameHours * 3600.0 * rand.GetInRange(0.5f, 1.5f);
  }

  private void Despawn(
    IEnumerable<GameObjectActorModelIdentifiableIndex.Entry> actorList,
    int count)
  {
    if (isRanch && actorList.Any(e => Identifiable.IsSlime(e.Id)))
    {
      Log.Error("CellDirector is despawning slimes on the ranch... " + new
      {
        gameObject = gameObject.name,
        cullSlimesLimit = cullSlimesLimit,
        targetSlimeCount = targetSlimeCount,
        despawnFactor = despawnFactor,
        slimesCount = identifiableIndex.GetSlimeCount(),
        hasTooManySlimes = HasTooManySlimes()
      });
      SentrySdk.CaptureMessage("CellDirector is despawning slimes on the ranch!");
    }
    Dictionary<GameObject, float> weightMap = new Dictionary<GameObject, float>();
    foreach (GameObjectActorModelIdentifiableIndex.Entry actor in actorList)
      weightMap[actor.GameObject] = GetDespawnWeight(actor.GameObject);
    for (int index = 0; index < count; ++index)
    {
      GameObject gameObject = rand.Pick(weightMap, null);
      weightMap.Remove(gameObject);
      Destroyer.DestroyActor(gameObject, "CellDirector.Despawn");
    }
  }

  private float GetDespawnWeight(GameObject actor)
  {
    float sqrMagnitude = (player.transform.position - actor.transform.position).sqrMagnitude;
    Vacuumable component = actor.GetComponent<Vacuumable>();
    if (component != null)
      sqrMagnitude *= GetSizeMultiplier(component.size);
    return sqrMagnitude;
  }

  private float GetSizeMultiplier(Vacuumable.Size size)
  {
    switch (size)
    {
      case Vacuumable.Size.NORMAL:
        return 1f;
      case Vacuumable.Size.LARGE:
        return 0.5f;
      case Vacuumable.Size.GIANT:
        return 0.25f;
      default:
        return 1f;
    }
  }

  public void Register(DirectedSlimeSpawner spawner)
  {
    if (!spawner.allowDirectedSpawns)
      return;
    spawners.Add(spawner);
  }

  public void Register(DirectedAnimalSpawner spawner) => animalSpawners.Add(spawner);

  public void Register(GameObject obj, ActorModel actorModel)
  {
    Identifiable.Id ident = actorModel.ident;
    identifiableIndex.Register(obj, actorModel);
    if (Identifiable.IsTarr(ident))
      ++tarrSlimeCount;
    if (!Identifiable.IsSlime(ident) || onSlimeAdded == null)
      return;
    onSlimeAdded(ident);
  }

  public void Unregister(GameObject obj, ActorModel actorModel)
  {
    Identifiable.Id ident = actorModel.ident;
    identifiableIndex.Deregister(obj, actorModel);
    if (Identifiable.IsTarr(ident))
      --tarrSlimeCount;
    if (!Identifiable.IsSlime(ident) || onSlimeRemoved == null)
      return;
    onSlimeRemoved(ident);
  }

  public void Get(Identifiable.Id id, ref List<GameObject> result) => result.AddRange(identifiableIndex.GetObjectsByIdentifiableId(id).Select(entry => entry.GameObject));

  public void Get(Identifiable.Id id, List<GameObject> result, HashSet<GameObject> toIgnore) => AddUniquesFromList(identifiableIndex.GetObjectsByIdentifiableId(id), result, toIgnore);

  public void Get(
    IEnumerable<Identifiable.Id> ids,
    List<GameObjectActorModelIdentifiableIndex.Entry> result,
    HashSet<GameObjectActorModelIdentifiableIndex.Entry> toIgnore)
  {
    foreach (Identifiable.Id id in ids)
      AddUniquesFromList(identifiableIndex.GetObjectsByIdentifiableId(id), result, toIgnore);
  }

  public void Get(IEnumerable<Identifiable.Id> ids, List<GameObject> results)
  {
    selectedGameObjects.Clear();
    foreach (Identifiable.Id id in ids)
      Get(id, results, selectedGameObjects);
    selectedGameObjects.Clear();
  }

  public void GetToys(
    IList<GameObjectActorModelIdentifiableIndex.Entry> results,
    HashSet<GameObjectActorModelIdentifiableIndex.Entry> toIgnore)
  {
    AddUniquesFromList(identifiableIndex.GetToys(), results, toIgnore);
  }

  public void GetLargos(
    IList<GameObjectActorModelIdentifiableIndex.Entry> results,
    HashSet<GameObjectActorModelIdentifiableIndex.Entry> toIgnore)
  {
    AddUniquesFromList(identifiableIndex.GetLargos(), results, toIgnore);
  }

  public void GetSlimes(List<GameObject> result, HashSet<GameObject> toIgnore) => AddUniquesFromList(identifiableIndex.GetSlimes(), result, toIgnore);

  private void AddUniquesFromList(
    IList<GameObjectActorModelIdentifiableIndex.Entry> sourceList,
    IList<GameObjectActorModelIdentifiableIndex.Entry> targetList,
    HashSet<GameObjectActorModelIdentifiableIndex.Entry> targetListLookup)
  {
    for (int index = 0; index < sourceList.Count; ++index)
    {
      GameObjectActorModelIdentifiableIndex.Entry source = sourceList[index];
      if (!targetListLookup.Contains(source))
      {
        targetList.Add(source);
        targetListLookup.Add(source);
      }
    }
  }

  private void AddUniquesFromList(
    IList<GameObjectActorModelIdentifiableIndex.Entry> sourceList,
    List<GameObject> targetList,
    HashSet<GameObject> targetListLookup)
  {
    for (int index = 0; index < sourceList.Count; ++index)
    {
      GameObject gameObject = sourceList[index].GameObject;
      if (!targetListLookup.Contains(gameObject))
      {
        targetList.Add(gameObject);
        targetListLookup.Add(gameObject);
      }
    }
  }

  public static void Get(Identifiable.Id id, RegionMember nearMember, List<GameObject> results)
  {
    selectedGameObjects.Clear();
    for (int index = 0; index < nearMember.regions.Count; ++index)
      nearMember.regions[index].cellDir.Get(id, results, selectedGameObjects);
    selectedGameObjects.Clear();
  }

  public static void Get(
    IEnumerable<Identifiable.Id> ids,
    RegionMember nearMember,
    List<GameObjectActorModelIdentifiableIndex.Entry> results)
  {
    selectedGameObjectEntries.Clear();
    for (int index = 0; index < nearMember.regions.Count; ++index)
      nearMember.regions[index].cellDir.Get(ids, results, selectedGameObjectEntries);
    selectedGameObjectEntries.Clear();
  }

  public static void GetToysNearMember(
    RegionMember nearMember,
    IList<GameObjectActorModelIdentifiableIndex.Entry> results)
  {
    selectedGameObjectEntries.Clear();
    for (int index = 0; index < nearMember.regions.Count; ++index)
      nearMember.regions[index].cellDir.GetToys(results, selectedGameObjectEntries);
    selectedGameObjectEntries.Clear();
  }

  public static void GetLargosNearMember(
    RegionMember nearMember,
    IList<GameObjectActorModelIdentifiableIndex.Entry> results)
  {
    selectedGameObjectEntries.Clear();
    for (int index = 0; index < nearMember.regions.Count; ++index)
      nearMember.regions[index].cellDir.GetLargos(results, selectedGameObjectEntries);
    selectedGameObjectEntries.Clear();
  }

  public static void GetSlimes(RegionMember nearMember, List<GameObject> results)
  {
    selectedGameObjects.Clear();
    for (int index = 0; index < nearMember.regions.Count; ++index)
      nearMember.regions[index].cellDir.GetSlimes(results, selectedGameObjects);
    selectedGameObjects.Clear();
  }

  public static void UnregisterFromAll(
    RegionMember member,
    GameObject gameObj,
    ActorModel actorModel)
  {
    foreach (Region region in member.regions)
      region.cellDir.Unregister(gameObj, actorModel);
  }

  public static bool IsOnRanch(RegionMember member)
  {
    if (member.regions.Count == 0)
      return false;
    foreach (Region region in member.regions)
    {
      if (!region.cellDir.isRanch)
        return false;
    }
    return true;
  }

  public static bool IsOnHomeRanch(RegionMember member)
  {
    if (member.regions.Count == 0)
      return false;
    foreach (Region region in member.regions)
    {
      if (!region.cellDir.isHomeRanch)
        return false;
    }
    return true;
  }

  public static bool IsInWilds(RegionMember member)
  {
    if (member.regions.Count == 0)
      return false;
    foreach (Region region in member.regions)
    {
      if (!region.cellDir.isWilds)
        return false;
    }
    return true;
  }

  public static IEnumerable<GameObjectActorModelIdentifiableIndex.Entry> GetAllRegistered()
  {
    foreach (CellDirector allCellDirector in allCellDirectors)
    {
      foreach (GameObjectActorModelIdentifiableIndex.Entry entry in allCellDirector.identifiableIndex.GetAllRegistered())
        yield return entry;
    }
  }

  protected virtual bool CanSpawnSlimes() => tarrSlimeCount <= 0;

  private bool NeedsMoreSlimes() => identifiableIndex.GetSlimeCount() < targetSlimeCount;

  private bool NeedsMoreAnimals() => GetNonignoredAnimalCount() < targetAnimalCount;

  private bool HasTooManySlimes() => identifiableIndex.GetSlimeCount() > targetSlimeCount * (double) despawnFactor;

  private bool HasTooManyAnimals() => GetNonignoredAnimalCount() > targetAnimalCount * (double) despawnFactor;

  private int GetNonignoredAnimalCount()
  {
    if (!ignoreCoopCorralAnimals)
      return identifiableIndex.GetAnimalCount();
    int nonignoredAnimalCount = 0;
    foreach (GameObject gameObject in identifiableIndex.GetAnimals().Select(entry => entry.GameObject))
    {
      Vector3 position = gameObject.transform.position;
      if (!CoopRegion.IsWithin(position) && !CorralRegion.IsWithin(position))
        ++nonignoredAnimalCount;
    }
    return nonignoredAnimalCount;
  }

  public delegate void OnSlimeAdded(Identifiable.Id slimeId);

  public delegate void OnSlimeRemoved(Identifiable.Id slimeId);
}
