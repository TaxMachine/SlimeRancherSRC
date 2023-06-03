// Decompiled with JetBrains decompiler
// Type: RescueAmmoOnDeath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RescueAmmoOnDeath : SRBehaviour
{
  public Transform[] spawnNodes;
  private Queue<Identifiable.Id> rescueIds;
  private PlayerDeathHandler deathHandler;
  private LookupDirector lookupDir;
  private ProgressDirector progressDir;
  private Region region;
  private const float PCT_TO_RESCUE = 0.5f;
  private const int MAX_TO_RESCUE_PER_SLOT = 6;
  private const float RESCUE_ITEM_SPAWN_FORCE = 20f;
  private const float RESCUE_ITEM_SPAWN_TORQUE = 20f;

  public void Start()
  {
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    deathHandler = SRSingleton<SceneContext>.Instance.Player.GetComponent<PlayerDeathHandler>();
    deathHandler.onAmmoWillClear += OnAmmoWillClear;
    region = GetComponentInParent<Region>();
    rescueIds = new Queue<Identifiable.Id>();
  }

  public void OnDestroy()
  {
    if (!(deathHandler != null))
      return;
    deathHandler.onAmmoWillClear -= OnAmmoWillClear;
  }

  private void OnAmmoWillClear(
    PlayerState.AmmoMode mode,
    Ammo ammo,
    PlayerDeathHandler.DeathType deathType)
  {
    if (!progressDir.HasProgress(ProgressDirector.ProgressType.UNLOCK_DOCKS) || deathType != PlayerDeathHandler.DeathType.DEFAULT || mode != PlayerState.AmmoMode.DEFAULT)
      return;
    int usableSlotCount = ammo.GetUsableSlotCount();
    Identifiable.Id[] idArray = new Identifiable.Id[usableSlotCount];
    int[] numArray = new int[usableSlotCount];
    for (int slotIdx = 0; slotIdx < usableSlotCount; ++slotIdx)
    {
      Identifiable.Id slotName = ammo.GetSlotName(slotIdx);
      idArray[slotIdx] = !CanRescue(slotName) ? Identifiable.Id.NONE : slotName;
      numArray[slotIdx] = Math.Min(6, RandomlyRound(ammo.GetSlotCount(slotIdx) * 0.5f));
    }
    for (int index1 = 0; index1 < spawnNodes.Length; ++index1)
    {
      int index2 = Randoms.SHARED.GetInt(usableSlotCount);
      if (idArray[index2] != Identifiable.Id.NONE && numArray[index2] > 0)
      {
        --numArray[index2];
        if (rescueIds.Count >= spawnNodes.Length)
        {
          int num = (int) rescueIds.Dequeue();
        }
        rescueIds.Enqueue(idArray[index2]);
      }
    }
    if (!enabled)
      return;
    SpawnRescuedItems();
  }

  private bool CanRescue(Identifiable.Id id) => !Identifiable.IsSlime(id) && !Identifiable.IsFashion(id) && id != Identifiable.Id.PUDDLE_PLORT && id != Identifiable.Id.FIRE_PLORT && id != Identifiable.Id.QUICKSILVER_PLORT && id != Identifiable.Id.WATER_LIQUID && id != Identifiable.Id.MAGIC_WATER_LIQUID;

  private int RandomlyRound(float val)
  {
    int num = Mathf.FloorToInt(val);
    float p = val - num;
    return num + (Randoms.SHARED.GetProbability(p) ? 1 : 0);
  }

  private void SpawnRescuedItems()
  {
    List<Transform> iterable = new List<Transform>(spawnNodes);
    while (iterable.Count > 0 && rescueIds.Count > 0)
    {
      Transform transform = Randoms.SHARED.Pluck(iterable, null);
      Rigidbody component = InstantiateActor(lookupDir.GetPrefab(rescueIds.Dequeue()), region.setId, transform.position, transform.rotation).GetComponent<Rigidbody>();
      if (component != null)
      {
        component.AddForce((Vector3.up + UnityEngine.Random.onUnitSphere) * 20f);
        component.AddTorque(UnityEngine.Random.onUnitSphere * 20f);
      }
    }
  }
}
