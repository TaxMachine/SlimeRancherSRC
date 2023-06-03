// Decompiled with JetBrains decompiler
// Type: DroneSubbehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Drone))]
[RequireComponent(typeof (DroneSubbehaviourPlexer))]
public abstract class DroneSubbehaviour : CollidableActorBehaviour
{
  protected Drone drone;
  protected TimeDirector timeDirector;
  private static readonly DroneProgram.Orientation[] CATCHER_ORIENTATIONS = new DroneProgram.Orientation[6]
  {
    new DroneProgram.Orientation(),
    new DroneProgram.Orientation()
    {
      rot = Quaternion.Euler(0.0f, -45f, 0.0f)
    },
    new DroneProgram.Orientation()
    {
      rot = Quaternion.Euler(0.0f, 45f, 0.0f)
    },
    new DroneProgram.Orientation() { pos = Vector3.up },
    new DroneProgram.Orientation()
    {
      rot = Quaternion.Euler(0.0f, -45f, 0.0f),
      pos = Vector3.up
    },
    new DroneProgram.Orientation()
    {
      rot = Quaternion.Euler(0.0f, 45f, 0.0f),
      pos = Vector3.up
    }
  };
  private const float POSITION_CHECK_SPHERECAST_START = 1000f;
  private const float DRONE_RADIUS = 0.5f;
  private List<MeshCollider> collidersToReset = new List<MeshCollider>();

  public DroneSubbehaviourPlexer plexer { get; private set; }

  public override void Awake()
  {
    base.Awake();
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    plexer = GetComponent<DroneSubbehaviourPlexer>();
    drone = GetComponent<Drone>();
  }

  public abstract bool Relevancy();

  public abstract void Action();

  public virtual void Selected()
  {
  }

  public virtual void Deselected()
  {
  }

  protected bool OnAction_DumpAmmo(ref double time)
  {
    if (!timeDirector.HasReached(time))
      return false;
    GameObject gameObject = SRSingleton<SceneContext>.Instance.GameModel.InstantiateActor(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(drone.ammo.Pop()), drone.region.setId, drone.guideDumpSpawn.position, Quaternion.Euler(Randoms.SHARED.GetInRange(0, 360), Randoms.SHARED.GetInRange(0, 360), Randoms.SHARED.GetInRange(0, 360)));
    PhysicsUtil.RestoreFreezeRotationConstraints(gameObject);
    gameObject.GetComponent<Rigidbody>().velocity = (Quaternion.Euler(0.0f, Randoms.SHARED.GetInRange(0, 360), 0.0f) * new Vector3(0.0f, -0.5f, 0.5f) + Vector3.down).normalized * 5f;
    float fromValue = gameObject.transform.localScale.x * 0.2f;
    gameObject.transform.DOScale(gameObject.transform.localScale, 0.1f).From(fromValue).SetEase(Ease.Linear);
    time = timeDirector.HoursFromNow(0.00500000035f);
    return true;
  }

  protected IEnumerable<DroneProgram.Orientation> GetTargetOrientations_Gather(GameObject source) => GetTargetOrientations_Gather(source, GatherConfig.DEFAULT);

  protected IEnumerable<DroneProgram.Orientation> GetTargetOrientations_Gather(
    GameObject source,
    GatherConfig config)
  {
    for (float distanceMultiplier = 1f; distanceMultiplier <= 3.0; ++distanceMultiplier)
    {
      float angle = Randoms.SHARED.GetFloat(6.28318548f);
      float delta = 0.628318548f;
      int ii = 0;
      while (ii < 10)
      {
        Vector3 vector3_1 = new Vector3(Mathf.Cos(angle) * config.distanceHorizontal, distanceMultiplier * config.distanceVertical, Mathf.Sin(angle) * config.distanceHorizontal);
        Vector3 vector3_2 = source.transform.position + vector3_1;
        if (SpaceIsClearForDrone(vector3_2))
          yield return new DroneProgram.Orientation(vector3_2, Quaternion.LookRotation(-vector3_1));
        ++ii;
        angle += delta;
      }
    }
    yield return new DroneProgram.Orientation(source.transform.position + config.fallbackOffset, Quaternion.LookRotation(config.fallbackOffset));
  }

  protected CatcherOrientation GetTargetOrientation_Catcher(GameObject source)
  {
    for (int index = 0; index < CATCHER_ORIENTATIONS.Length; ++index)
    {
      if (CatcherOrientation.IsAvailable(source, index))
      {
        DroneProgram.Orientation orientation = CATCHER_ORIENTATIONS[index];
        Vector3 vector3 = orientation.rot * (source.transform.forward * 2f + orientation.pos);
        Vector3 position = source.transform.position + vector3;
        if (SpaceIsClearForDrone(position))
          return new CatcherOrientation(source, index, new DroneProgram.Orientation()
          {
            pos = position,
            rot = Quaternion.LookRotation(-vector3)
          });
      }
    }
    return new CatcherOrientation(new DroneProgram.Orientation()
    {
      pos = source.transform.position + Vector3.up,
      rot = Quaternion.LookRotation(-Vector3.up)
    });
  }

  private bool SpaceIsClearForDrone(Vector3 position)
  {
    if (drone.noClip.enabled)
      return true;
    RaycastHit[] raycastHitArray = Physics.SphereCastAll(position + Vector3.up * 1000f, 0.5f, Vector3.down, 1000f, -537968901);
    collidersToReset.Clear();
    bool flag;
    if (raycastHitArray.Length != 0)
    {
      for (int index = 0; index < raycastHitArray.Length; ++index)
      {
        MeshCollider component = raycastHitArray[index].collider.GetComponent<MeshCollider>();
        if (component != null && !component.convex && raycastHitArray[index].collider.GetComponent<Rigidbody>() == null)
        {
          collidersToReset.Add(component);
          try
          {
            component.convex = true;
          }
          catch
          {
            Log.Error("Exception when changing to convex.", "object name", component.name);
            throw;
          }
        }
      }
      flag = !Physics.CheckSphere(position, 0.5f, -537968901);
      foreach (MeshCollider meshCollider in collidersToReset)
        meshCollider.convex = false;
    }
    else
      flag = true;
    return flag;
  }

  protected class GatherConfig
  {
    public static GatherConfig DEFAULT = new GatherConfig();
    public const int GATHER_ATTEMPTS = 10;
    public Vector3 fallbackOffset;
    public float distanceHorizontal;
    public float distanceVertical;

    public GatherConfig()
    {
      fallbackOffset = Vector3.up;
      distanceHorizontal = 2f;
      distanceVertical = 1f;
    }
  }

  protected class CatcherOrientation : IDisposable
  {
    private static Dictionary<GameObject, HashSet<int>> BLACKLIST = new Dictionary<GameObject, HashSet<int>>();
    private GameObject source;
    private int? index;

    public DroneProgram.Orientation orientation { get; private set; }

    public static bool IsAvailable(GameObject source, int index) => !BLACKLIST.ContainsKey(source) || !BLACKLIST[source].Contains(index);

    public CatcherOrientation(GameObject source, int index, DroneProgram.Orientation orientation)
    {
      this.orientation = orientation;
      this.source = source;
      this.index = new int?(index);
      if (!BLACKLIST.ContainsKey(source))
        BLACKLIST[source] = new HashSet<int>();
      BLACKLIST[source].Add(index);
    }

    public CatcherOrientation(DroneProgram.Orientation orientation) => this.orientation = orientation;

    public void Dispose()
    {
      if (!(source != null) || !index.HasValue)
        return;
      if (BLACKLIST.ContainsKey(source) && BLACKLIST[source].Remove(index.Value) && BLACKLIST[source].Count == 0)
        BLACKLIST.Remove(source);
      source = null;
      index = new int?();
    }
  }
}
