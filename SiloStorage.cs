// Decompiled with JetBrains decompiler
// Type: SiloStorage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SiloStorage : MonoBehaviour, LandPlotModel.Participant
{
  public StorageType type;
  public int numSlots = 4;
  public int maxAmmo = 100;
  protected LandPlotModel model;
  protected Ammo ammo;
  private const int SILO_FULL_ACHIEVE_SLOTS = 12;
  private const int SILO_FULL_ACHIEVE_AMOUNT = 50;

  public Ammo LocalAmmo => ammo;

  public virtual void Awake() => InitAmmo();

  private void InitAmmo()
  {
    if (ammo != null)
      return;
    ammo = new Ammo(type.GetContents(), numSlots, numSlots, new Predicate<Identifiable.Id>[numSlots], (id, index) => maxAmmo);
  }

  public void InitModel(LandPlotModel model)
  {
    InitAmmo();
    model.siloAmmo[type] = new AmmoModel();
    LocalAmmo.InitModel(model.siloAmmo[type]);
  }

  public void SetModel(LandPlotModel model)
  {
    this.model = model;
    LocalAmmo.SetModel(model.siloAmmo[type]);
  }

  public virtual Ammo GetRelevantAmmo() => ammo;

  public Identifiable.Id GetSlotIdentifiable(int slotIdx) => GetRelevantAmmo().GetSlotName(slotIdx);

  public int GetSlotCount(int slotIdx) => GetRelevantAmmo().GetSlotCount(slotIdx);

  public bool MaybeAddIdentifiable(Identifiable.Id id)
  {
    int num = GetRelevantAmmo().MaybeAddToSlot(id, null) ? 1 : 0;
    OnAdded();
    return num != 0;
  }

  public bool MaybeAddIdentifiable(Identifiable.Id id, int slotIdx, int count = 1, bool overflow = false)
  {
    int num = GetRelevantAmmo().MaybeAddToSpecificSlot(id, null, slotIdx, count, overflow) ? 1 : 0;
    OnAdded();
    return num != 0;
  }

  public bool CanAccept(Identifiable.Id id) => GetRelevantAmmo().CouldAddToSlot(id);

  public bool CanAccept(Identifiable.Id id, int slotIdx, bool overflow) => GetRelevantAmmo().CouldAddToSlot(id, slotIdx, overflow);

  private void OnAdded()
  {
    if (!GetRelevantAmmo().HasFullSlots(12, 50))
      return;
    SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.FILLED_SILO, 1);
  }

  public enum StorageType
  {
    NON_SLIMES,
    PLORT,
    FOOD,
    CRAFTING,
    ELDER,
  }

  public class StorageTypeComparer : IEqualityComparer<StorageType>
  {
    public static StorageTypeComparer Instance = new StorageTypeComparer();

    public bool Equals(StorageType a, StorageType b) => a == b;

    public int GetHashCode(StorageType a) => (int) a;
  }
}
