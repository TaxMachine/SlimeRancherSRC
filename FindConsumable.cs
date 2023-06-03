// Decompiled with JetBrains decompiler
// Type: FindConsumable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class FindConsumable : SlimeSubbehaviour
{
  public float minSearchRad = 5f;
  public float maxSearchRad = 30f;
  public float facingStability = 1f;
  public float facingSpeed = 5f;
  public float pursuitSpeedFactor = 1f;
  public float minDist;
  protected Dictionary<Identifiable.Id, DriveCalculator> searchIds;
  protected SlimeAudio slimeAudio;
  protected RegionMember member;
  protected LookupDirector lookupDir;
  protected float startTime;
  private const float SCOOT_CYCLE_TIME = 1f;
  private const float SCOOT_CYCLE_FACTOR = 6.28318548f;
  private List<GameObject> nearbyGameObjects = new List<GameObject>();
  private static List<GameObjectActorModelIdentifiableIndex.Entry> localStatic_entries = new List<GameObjectActorModelIdentifiableIndex.Entry>();

  public override void Awake()
  {
    base.Awake();
    slimeAudio = GetComponent<SlimeAudio>();
    member = GetComponent<RegionMember>();
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    startTime = Time.time;
  }

  public override void Start()
  {
    base.Start();
    UpdateSearchIds();
  }

  public void UpdateSearchIds() => searchIds = GetSearchIds();

  protected virtual Dictionary<Identifiable.Id, DriveCalculator> GetSearchIds() => GetComponent<SlimeEat>().GetAllEats();

  protected void RotateTowards(Vector3 dirToTarget) => RotateTowards(dirToTarget, facingSpeed, facingStability);

  protected GameObject FindNearestConsumable(
    IList<GameObjectActorModelIdentifiableIndex.Entry> gameObjects,
    out float drive)
  {
    GameObject nearestConsumable = null;
    float num1 = (float) (1.0 / (maxSearchRad * (double) maxSearchRad));
    drive = 0.0f;
    float num2 = minDist * minDist;
    Vector3 position = transform.position;
    for (int index = 0; index < gameObjects.Count; ++index)
    {
      GameObjectActorModelIdentifiableIndex.Entry gameObject1 = gameObjects[index];
      GameObject gameObject2 = gameObject1.GameObject;
      if (gameObject2 != gameObject && searchIds.TryGetValue(gameObject1.Id, out DriveCalculator _) && Identifiable.IsEdible(gameObject2))
      {
        float sqrMagnitude = (GetGotoPos(gameObject2) - position).sqrMagnitude;
        if (sqrMagnitude >= (double) num2)
        {
          float num3 = searchIds[gameObject1.Id].Drive(emotions, gameObject1.Id);
          float num4 = num3 / sqrMagnitude;
          if (num4 > (double) num1)
          {
            nearestConsumable = gameObject2;
            num1 = num4;
            drive = Mathf.Clamp(num3, 0.0f, 1f);
          }
        }
      }
    }
    return nearestConsumable;
  }

  protected GameObject FindNearestConsumable(out float drive)
  {
    localStatic_entries.Clear();
    CellDirector.Get(searchIds.Keys.AsEnumerable(), member, localStatic_entries);
    return FindNearestConsumable(localStatic_entries, out drive);
  }

  protected GameObject FindNearestConsumableOld(out float drive)
  {
    GameObject nearestConsumableOld = null;
    float num1 = (float) (1.0 / (maxSearchRad * (double) maxSearchRad));
    drive = 0.0f;
    float num2 = minDist * minDist;
    foreach (KeyValuePair<Identifiable.Id, DriveCalculator> searchId in searchIds)
    {
      nearbyGameObjects.Clear();
      CellDirector.Get(searchId.Key, member, nearbyGameObjects);
      Vector3 position = transform.position;
      for (int index = 0; index < nearbyGameObjects.Count; ++index)
      {
        GameObject nearbyGameObject = nearbyGameObjects[index];
        if (nearbyGameObject != gameObject && Identifiable.IsEdible(nearbyGameObject))
        {
          float sqrMagnitude = (GetGotoPos(nearbyGameObject) - position).sqrMagnitude;
          if (sqrMagnitude >= (double) num2)
          {
            float num3 = searchId.Value.Drive(emotions, searchId.Key);
            float num4 = num3 / sqrMagnitude;
            if (num4 > (double) num1)
            {
              nearestConsumableOld = nearbyGameObject;
              num1 = num4;
              drive = Mathf.Clamp(num3, 0.0f, 1f);
            }
          }
        }
      }
    }
    nearbyGameObjects.Clear();
    return nearestConsumableOld;
  }

  protected void MoveTowards(
    Vector3 targetPos,
    bool shouldJump,
    ref float nextJumpAvail,
    float jumpStrength)
  {
    if (!IsGrounded())
      return;
    Vector3 vector3 = targetPos - transform.position;
    float sqrMagnitude = vector3.sqrMagnitude;
    Vector3 normalized = vector3.normalized;
    RotateTowards(normalized);
    if (shouldJump)
    {
      if (Time.fixedTime < (double) nextJumpAvail)
        return;
      float num = Mathf.Min(1f, Mathf.Sqrt(sqrMagnitude) / 30f);
      slimeBody.AddForce((normalized * num + Vector3.up).normalized * jumpStrength * slimeBody.mass, ForceMode.Impulse);
      slimeAudio.Play(slimeAudio.slimeSounds.jumpCue);
      slimeAudio.Play(slimeAudio.slimeSounds.voiceJumpCue);
      nextJumpAvail = Time.time + 1f;
    }
    else if (sqrMagnitude <= 9.0)
    {
      slimeBody.AddForce(normalized * (480f * pursuitSpeedFactor * slimeBody.mass * Time.fixedDeltaTime));
    }
    else
    {
      float num = ScootCycleSpeed();
      slimeBody.AddForce(normalized * (150f * slimeBody.mass * pursuitSpeedFactor * Time.fixedDeltaTime * num));
      Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
      slimeBody.AddForceAtPosition(normalized * (270f * slimeBody.mass * Time.fixedDeltaTime * num), position);
    }
  }

  protected float ScootCycleSpeed() => Mathf.Sin((float) ((Time.time - (double) startTime) * 6.2831854820251465)) + 1f;
}
