// Decompiled with JetBrains decompiler
// Type: HydroTurret
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HydroTurret : SRBehaviour
{
  public Identifiable.Id liquidId = Identifiable.Id.WATER_LIQUID;
  public Transform spawnLoc;
  public FilteredTrackCollisions tracker;
  public Transform toRotate;
  public SECTR_AudioCue shootCue;
  public SECTR_AudioCue rotateCue;
  [Tooltip("Delay in game mins between shots")]
  public float shootDelay = 2f;
  [Tooltip("Delay in game mins before we can shoot if we need to retarget.")]
  public float retargetDelay = 0.5f;
  [Tooltip("Delay in game mins before we can shoot if another turret on our gadget shot.")]
  public float localShotDelay = 0.2f;
  private TimeDirector timeDir;
  private Region region;
  private GameObject liquidPrefab;
  private double nextShootTime;
  private GameObject currTarget;
  private WaitForChargeup waiter;
  private List<HydroTurret> otherTurrets = new List<HydroTurret>();
  private const float SHOOT_SCALE_UP_TIME = 0.1f;
  private const float SHOOT_SCALE_FACTOR = 0.2f;
  private const float MAX_ROT_PER_SEC = 180f;
  private const float MAX_WALL_CHECK_DIST = 5f;

  public void Awake()
  {
    region = GetComponentInParent<Region>();
    waiter = GetComponent<WaitForChargeup>();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    tracker.SetFilter(IsTarr);
    liquidPrefab = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(liquidId);
    foreach (HydroTurret component in GetComponents<HydroTurret>())
    {
      if (component != this)
        otherTurrets.Add(component);
    }
  }

  private void DelayForLocalShot() => nextShootTime = Math.Max(nextShootTime, timeDir.HoursFromNow(localShotDelay * 0.0166666675f));

  private bool IsTarr(GameObject gameObj) => Identifiable.IsTarr(Identifiable.GetId(gameObj));

  public void Update()
  {
    if (waiter.IsWaiting() || !timeDir.HasReached(nextShootTime))
      return;
    HashSet<GameObject> iterable = tracker.CurrColliders();
    if (iterable.Count > 0)
    {
      if (currTarget == null || !iterable.Contains(currTarget))
        currTarget = Randoms.SHARED.Pick(iterable, null);
      switch (TryToShootAt(currTarget))
      {
        case ShootResult.SHOT:
          currTarget = null;
          nextShootTime = timeDir.HoursFromNow(shootDelay * 0.0166666675f);
          using (List<HydroTurret>.Enumerator enumerator = otherTurrets.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.DelayForLocalShot();
            break;
          }
        case ShootResult.FAIL:
          nextShootTime = timeDir.HoursFromNow(retargetDelay * 0.0166666675f);
          break;
      }
    }
    else
      nextShootTime = timeDir.HoursFromNow(retargetDelay * 0.0166666675f);
  }

  private ShootResult TryToShootAt(GameObject target)
  {
    Vector3 vector3_1 = target.transform.position - toRotate.position;
    float num1 = 25f;
    float num2 = Mathf.Abs(Physics.gravity.y);
    Vector3 vector3_2 = vector3_1 with { y = 0.0f };
    float magnitude = vector3_2.magnitude;
    float y1 = vector3_1.y;
    float num3 = num1 * num1;
    float f = (float) (num3 * (double) num3 - num2 * (num2 * (double) magnitude * magnitude + 2.0 * y1 * num3));
    if (f < 0.0)
      return ShootResult.FAIL;
    float y2 = num1 * num1 - Mathf.Sqrt(f);
    float num4 = num2 * magnitude;
    vector3_2.Normalize();
    Vector3 dir = new Vector3(vector3_2.x * num4, y2, vector3_2.z * num4).normalized * num1;
    if (WouldHitWall(dir, Mathf.Min(vector3_1.magnitude, 5f)))
      return ShootResult.FAIL;
    if (!RotateTowards(dir))
      return ShootResult.PENDING;
    GameObject gameObject = InstantiateActor(liquidPrefab, region.setId, spawnLoc.position, spawnLoc.rotation);
    gameObject.GetComponent<Rigidbody>().velocity = dir;
    float x = gameObject.transform.localScale.x;
    float fromValue = x * 0.2f;
    gameObject.transform.DOScale(x, 0.1f).From(fromValue).SetEase(Ease.Linear);
    if (shootCue != null)
      SECTR_AudioSystem.Play(shootCue, spawnLoc.position, false);
    return ShootResult.SHOT;
  }

  private bool RotateTowards(Vector3 dir)
  {
    Quaternion quaternion = Quaternion.LookRotation(dir, Vector3.up);
    toRotate.rotation = Quaternion.RotateTowards(toRotate.rotation, quaternion, 180f * Time.deltaTime);
    return Quaternion.Angle(toRotate.rotation, quaternion) < (double) Mathf.Epsilon;
  }

  private bool WouldHitWall(Vector3 dir, float maxDist)
  {
    LayerMask layerMask = 268435457;
    return Physics.Raycast(toRotate.position, dir, maxDist, layerMask, QueryTriggerInteraction.Ignore);
  }

  private enum ShootResult
  {
    PENDING,
    SHOT,
    FAIL,
  }
}
