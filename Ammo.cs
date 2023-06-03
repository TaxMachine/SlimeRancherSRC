// Decompiled with JetBrains decompiler
// Type: Ammo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ammo
{
  private AmmoModel ammoModel;
  private int selectedAmmoIdx;
  private readonly HashSet<Identifiable.Id> potentialAmmo;
  private int numSlots;
  private int initUsableSlots;
  private Func<Identifiable.Id, int, int> initSlotMaxCountFunction;
  private Predicate<Identifiable.Id>[] slotPreds;
  private TimeDirector timeDir;
  private LookupDirector lookupDir;
  private double waterIsMagicUntil = double.NegativeInfinity;
  private const float MAGIC_WATER_LIFETIME = 0.5f;

  private Slot[] Slots => ammoModel.slots;

  public Ammo(
    HashSet<Identifiable.Id> potentialAmmo,
    int numSlots,
    int usableSlots,
    Predicate<Identifiable.Id>[] slotPreds,
    Func<Identifiable.Id, int, int> slotMaxCountFunction)
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    this.potentialAmmo = potentialAmmo;
    this.numSlots = numSlots;
    this.slotPreds = slotPreds;
    initUsableSlots = usableSlots;
    initSlotMaxCountFunction = slotMaxCountFunction;
  }

  public void InitModel(AmmoModel model) => model.Reset(numSlots, initUsableSlots, initSlotMaxCountFunction);

  public void SetModel(AmmoModel model)
  {
    ValidateAndAdjustSlots(ref model.slots);
    ammoModel = model;
  }

  public Predicate<Identifiable.Id> GetSlotPredicate(int index) => slotPreds[index];

  public bool SetAmmoSlot(int idx)
  {
    if (selectedAmmoIdx == idx || idx >= ammoModel.usableSlots)
      return false;
    selectedAmmoIdx = idx;
    return true;
  }

  public void NextAmmoSlot() => selectedAmmoIdx = (selectedAmmoIdx + 1) % ammoModel.usableSlots;

  public void PrevAmmoSlot() => selectedAmmoIdx = (selectedAmmoIdx + ammoModel.usableSlots - 1) % ammoModel.usableSlots;

  public Identifiable.Id GetSlotName(int slotIdx) => Slots[slotIdx] == null ? Identifiable.Id.NONE : AdjustId(Slots[slotIdx].id);

  public SlimeEmotionData GetSlimeEmotionData(int slotIdx) => Slots[slotIdx] == null ? new SlimeEmotionData() : Slots[slotIdx].emotions;

  public int GetCount(Identifiable.Id id)
  {
    for (int index = 0; index < GetUsableSlotCount(); ++index)
    {
      if (Slots[index] != null && Slots[index].id == id)
        return Slots[index].count;
    }
    return 0;
  }

  public int GetSlotCount(int slotIdx) => Slots[slotIdx] == null ? 0 : Slots[slotIdx].count;

  public int GetSelectedAmmoIdx() => selectedAmmoIdx;

  public int? GetAmmoIdx(Identifiable.Id id)
  {
    for (int index = 0; index < GetUsableSlotCount(); ++index)
    {
      if (Slots[index] != null && Slots[index].id == id)
        return new int?(index);
    }
    return new int?();
  }

  public Identifiable.Id GetSelectedId()
  {
    Slot slot = Slots[selectedAmmoIdx];
    return slot != null ? AdjustId(slot.id) : Identifiable.Id.NONE;
  }

  public void RegisterPotentialAmmo(GameObject prefab) => potentialAmmo.Add(Identifiable.GetId(prefab));

  public int GetSlotMaxCount(int index) => GetSlotMaxCount(GetSlotName(index), index);

  public int GetSlotMaxCount(Identifiable.Id id, int index) => ammoModel.GetSlotMaxCount(id, index);

  private Identifiable.Id AdjustId(Identifiable.Id id) => Identifiable.IsWater(id) && !timeDir.HasReached(waterIsMagicUntil) ? Identifiable.Id.MAGIC_WATER_LIQUID : id;

  public GameObject GetSelectedStored()
  {
    Slot slot = Slots[selectedAmmoIdx];
    return slot == null ? null : lookupDir.GetPrefab(AdjustId(slot.id));
  }

  public Dictionary<SlimeEmotions.Emotion, float> GetSelectedEmotions() => Slots[selectedAmmoIdx].emotions;

  public bool HasSelectedAmmo() => Slots[selectedAmmoIdx] != null && Slots[selectedAmmoIdx].count > 0;

  public void DecrementSelectedAmmo(int amount = 1)
  {
    if (Slots[selectedAmmoIdx] == null || Identifiable.IsWater(Slots[selectedAmmoIdx].id) && !timeDir.HasReached(waterIsMagicUntil))
      return;
    Slots[selectedAmmoIdx].count = Math.Max(Slots[selectedAmmoIdx].count - amount, 0);
    if (Slots[selectedAmmoIdx].count != 0)
      return;
    Slots[selectedAmmoIdx] = null;
  }

  public void Clear() => Clear(i => true);

  public void Clear(Predicate<int> predicate)
  {
    for (int index = 0; index < Slots.Length; ++index)
    {
      if (predicate(index))
        Clear(index);
    }
  }

  public void ClearSelected() => Clear(selectedAmmoIdx);

  public void Clear(int index)
  {
    if (Identifiable.IsWater(GetSlotName(index)))
      waterIsMagicUntil = double.NegativeInfinity;
    Slots[index] = null;
  }

  public bool ReplaceWithQuicksilverAmmo(Identifiable.Id id, int count)
  {
    if (!potentialAmmo.Contains(id))
      return false;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    for (int index = 0; index < Slots.Length; ++index)
    {
      if (Slots[index] != null && Slots[index].id == id)
      {
        if (Slots[index].count >= GetSlotMaxCount(id, index))
          return false;
        Slots[index] = new Slot(id, Mathf.Min(GetSlotMaxCount(id, index), Slots[index].count + count));
        return true;
      }
      if ((slotPreds[index] == null || slotPreds[index](id)) && (!nullable2.HasValue || GetSlotCount(index) < nullable2.Value))
      {
        nullable2 = new int?(GetSlotCount(index));
        nullable1 = new int?(index);
      }
    }
    if (!nullable1.HasValue)
      return false;
    Slots[nullable1.Value] = new Slot(id, Mathf.Min(GetSlotMaxCount(id, nullable1.Value), count));
    return true;
  }

  public bool Contains(Identifiable.Id id)
  {
    foreach (Slot slot in Slots)
    {
      if (slot != null && slot.id == id)
        return true;
    }
    return false;
  }

  public void Decrement(Identifiable.Id id, int count = 1)
  {
    for (int index = 0; index < Slots.Length; ++index)
    {
      Slot slot = Slots[index];
      if (slot != null && slot.id == id)
      {
        Decrement(index, count);
        return;
      }
    }
    throw new InvalidOperationException("Cannot decrement ammo we don't have: " + id);
  }

  public void Decrement(int index, int count = 1)
  {
    Slots[index].count -= count;
    if (Slots[index].count > 0)
      return;
    Slots[index] = null;
  }

  public bool IsEmpty()
  {
    for (int index = 0; index < Slots.Length; ++index)
    {
      Slot slot = Slots[index];
      if (slot != null && slot.count > 0)
        return false;
    }
    return true;
  }

  public bool Any(Predicate<Identifiable.Id> predicate)
  {
    for (int index = 0; index < GetUsableSlotCount(); ++index)
    {
      if (Slots[index] != null && Slots[index].count > 0 && predicate(Slots[index].id))
        return true;
    }
    return false;
  }

  public double GetRemainingWaterIsMagicMins()
  {
    double num = timeDir.HoursUntil(waterIsMagicUntil);
    return num >= 0.0 ? num * 60.0 : 0.0;
  }

  public float GetFullness(int index) => Mathf.Min(1f, GetSlotCount(index) / (float) GetSlotMaxCount(index));

  public float GetSelectedFullness() => GetFullness(selectedAmmoIdx);

  public bool MaybeAddToSpecificSlot(Identifiable identifiable, int slotIdx) => MaybeAddToSpecificSlot(identifiable.id, identifiable, slotIdx);

  public bool MaybeAddToSpecificSlot(Identifiable.Id id, Identifiable identifiable, int slotIdx) => MaybeAddToSpecificSlot(id, identifiable, slotIdx, GetAmountFilledPerVac(id, slotIdx));

  public bool MaybeAddToSpecificSlot(
    Identifiable.Id id,
    Identifiable identifiable,
    int slotIdx,
    int count)
  {
    return MaybeAddToSpecificSlot(id, identifiable, slotIdx, count, false);
  }

  public bool MaybeAddToSpecificSlot(
    Identifiable.Id id,
    Identifiable identifiable,
    int slotIdx,
    int count,
    bool overflow)
  {
    if (Slots[slotIdx] == null)
    {
      if (slotPreds[slotIdx] != null && !slotPreds[slotIdx](id) || !potentialAmmo.Contains(id))
        return false;
      Slots[slotIdx] = new Slot(id, 0);
    }
    if (Slots[slotIdx].id != id || !overflow && Slots[slotIdx].count + count > GetSlotMaxCount(id, slotIdx))
      return false;
    Slots[slotIdx].count += count;
    if (identifiable != null)
    {
      SlimeEmotions component = identifiable.GetComponent<SlimeEmotions>();
      if (component != null)
        Slots[slotIdx].AverageIn(component);
    }
    return true;
  }

  public bool CouldAddToSlot(Identifiable.Id id) => CouldAddToSlot(id, 0, GetUsableSlotCount() - 1, false);

  public bool CouldAddToSlot(Identifiable.Id id, int slotIdx, bool overflow) => CouldAddToSlot(id, slotIdx, slotIdx, overflow);

  private bool CouldAddToSlot(Identifiable.Id id, int indexMin, int indexMax, bool overflow)
  {
    if (id == Identifiable.Id.MAGIC_WATER_LIQUID)
      id = Identifiable.Id.WATER_LIQUID;
    if (!potentialAmmo.Contains(id))
      return false;
    for (int index = indexMin; index <= indexMax; ++index)
    {
      if (Slots[index] == null)
      {
        if (slotPreds[index] == null || slotPreds[index](id))
          return true;
      }
      else if (Slots[index].id == id && (overflow || Slots[index].count < GetSlotMaxCount(id, index)))
        return true;
    }
    return false;
  }

  public bool MaybeAddToSlot(Identifiable.Id id, Identifiable identifiable)
  {
    bool flag1 = id == Identifiable.Id.MAGIC_WATER_LIQUID;
    if (flag1)
      id = Identifiable.Id.WATER_LIQUID;
    bool flag2 = false;
    bool flag3 = false;
    for (int index = 0; index < ammoModel.usableSlots; ++index)
    {
      if (Slots[index] != null && Slots[index].id == id)
      {
        int slotMaxCount = GetSlotMaxCount(id, index);
        if (flag1)
        {
          Slots[index].count = slotMaxCount;
          waterIsMagicUntil = timeDir.HoursFromNow(0.5f);
        }
        else if (Slots[index].count >= slotMaxCount)
        {
          flag3 = true;
        }
        else
        {
          Slots[index].count = Mathf.Min(slotMaxCount, Slots[index].count + GetAmountFilledPerVac(id, index));
          SlimeEmotions component = identifiable == null ? null : identifiable.GetComponent<SlimeEmotions>();
          if (component != null)
            Slots[index].AverageIn(component);
        }
        flag2 = true;
        break;
      }
    }
    if (!flag2)
    {
      for (int index = 0; index < ammoModel.usableSlots && !flag2; ++index)
      {
        if ((slotPreds[index] == null || slotPreds[index](id)) && Slots[index] == null && potentialAmmo.Contains(id))
        {
          SlimeEmotions component = identifiable == null ? null : identifiable.GetComponent<SlimeEmotions>();
          if (flag1)
          {
            Slots[index] = new Slot(id, GetSlotMaxCount(id, index));
            waterIsMagicUntil = timeDir.HoursFromNow(0.5f);
          }
          else
            Slots[index] = new Slot(id, GetAmountFilledPerVac(id, index));
          if (component != null)
            Slots[index].AverageIn(component);
          flag2 = true;
        }
      }
    }
    return flag2 && !flag3;
  }

  private int GetAmountFilledPerVac(Identifiable.Id id, int index)
  {
    if (id == Identifiable.Id.WATER_LIQUID)
      return 5;
    return id == Identifiable.Id.GLITCH_DEBUG_SPRAY_LIQUID ? SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.debugSprayAmmoPerStation : 1;
  }

  public bool Replace(int index, Identifiable.Id id)
  {
    Slot slot = Slots[index];
    double waterIsMagicUntil = this.waterIsMagicUntil;
    Clear(index);
    if (MaybeAddToSpecificSlot(id, null, index))
      return true;
    Slots[index] = slot;
    this.waterIsMagicUntil = waterIsMagicUntil;
    return false;
  }

  public bool Replace(Identifiable.Id previous, Identifiable.Id next)
  {
    for (int index = 0; index < Slots.Length; ++index)
    {
      if (GetSlotName(index) == previous)
      {
        Slots[index] = new Slot(next, Mathf.Min(GetSlotCount(index), GetSlotMaxCount(next, index)));
        if (Identifiable.IsSlime(Slots[index].id))
        {
          Slots[index].emotions = new SlimeEmotionData();
          Slots[index].emotions[SlimeEmotions.Emotion.AGITATION] = 0.0f;
          Slots[index].emotions[SlimeEmotions.Emotion.HUNGER] = 0.5f;
          Slots[index].emotions[SlimeEmotions.Emotion.FEAR] = 0.0f;
        }
        return true;
      }
    }
    return false;
  }

  public bool HasFullSlots(int numSlots, int fullToAmount)
  {
    if (ammoModel.usableSlots < numSlots)
      return false;
    for (int slotIdx = 0; slotIdx < ammoModel.usableSlots; ++slotIdx)
    {
      if (GetSlotCount(slotIdx) < fullToAmount)
        return false;
    }
    return true;
  }

  public List<AmmoDataV02> ToSerializable() => Enumerable.Range(0, Slots.Length).Select(ii => new AmmoDataV02()
  {
    id = GetSlotName(ii),
    emotionData = new SlimeEmotionDataV02()
    {
      emotionData = GetSlimeEmotionData(ii)
    },
    count = GetSlotCount(ii)
  }).ToList();

  public void FromSerializable(List<AmmoDataV02> data)
  {
    if (data.Count > 0 && data.Count != Slots.Length)
      Log.Warning("Unserializing ammo slot length differs, ignoring extra.");
    for (int index = 0; index < Slots.Length && index < data.Count; ++index)
    {
      if (data[index] != null && data[index].id != Identifiable.Id.NONE)
      {
        if (slotPreds[index] != null && !slotPreds[index](data[index].id))
          Debug.Log("Unserializing ammo slot contained no-longer-legal ID, ignoring: " + data[index].id);
        else if (!potentialAmmo.Contains(data[index].id))
          Log.Warning("Unserializing ammo slot contained invalid ammo id: " + data[index].id);
        else if (lookupDir.GetPrefab(data[index].id) == null)
        {
          Log.Warning("Found unknown ammo ID: " + data[index].id);
        }
        else
        {
          Slots[index] = new Slot(data[index].id, data[index].count);
          Slots[index].emotions = new SlimeEmotionData();
          foreach (KeyValuePair<SlimeEmotions.Emotion, float> keyValuePair in data[index].emotionData.emotionData)
            Slots[index].emotions.Add(keyValuePair.Key, keyValuePair.Value);
        }
      }
    }
  }

  private void ValidateAndAdjustSlots(ref Slot[] slots)
  {
    if (slots.Length != slotPreds.Length)
    {
      Log.Warning("Unserializing ammo slot length different than expected, ignoring extra and/or padding.", nameof (slots), slots.Length, "preds", slotPreds.Length);
      Slot[] slotArray = slots;
      slots = new Slot[slotPreds.Length];
      for (int index = 0; index < slotPreds.Length; ++index)
        slots[index] = index >= slotArray.Length ? null : slotArray[index];
    }
    for (int index = 0; index < slotPreds.Length; ++index)
    {
      if (slots[index] == null || slots[index].id == Identifiable.Id.NONE)
        slots[index] = null;
      else if (slotPreds[index] != null && !slotPreds[index](slots[index].id))
      {
        Debug.Log("Unserialized ammo slot contained no-longer-legal ID, ignoring: " + slots[index].id);
        slots[index] = null;
      }
      else if (!potentialAmmo.Contains(slots[index].id))
      {
        Log.Warning("Unserializing ammo slot contained invalid ammo id: " + slots[index].id);
        slots[index] = null;
      }
    }
  }

  public void IncreaseUsableSlots(int usableSlots)
  {
    ammoModel.IncreaseUsableSlots(usableSlots);
    Debug.Log("MST Increased slots: " + ammoModel.usableSlots + " set: " + usableSlots);
  }

  public int GetUsableSlotCount() => ammoModel.usableSlots;

  public class Slot
  {
    public readonly Identifiable.Id id;
    public int count;
    public SlimeEmotionData emotions;

    public Slot(Identifiable.Id id, int count)
    {
      this.id = id;
      this.count = count;
    }

    public void AverageIn(SlimeEmotions emotions)
    {
      if (this.emotions == null)
        this.emotions = new SlimeEmotionData(emotions);
      else
        this.emotions.AverageIn(emotions, 1f / count);
    }
  }

  [Serializable]
  public class AmmoData
  {
    public Identifiable.Id id;
    public int count;
    public SlimeEmotionData emotionData;

    public AmmoData(Identifiable.Id id, int count, SlimeEmotionData emotionData)
    {
      this.id = id;
      this.count = count;
      this.emotionData = emotionData;
    }

    public override bool Equals(object o)
    {
      if (!(o is AmmoData))
        return false;
      AmmoData ammoData = (AmmoData) o;
      if (id != ammoData.id || count != ammoData.count)
        return false;
      return emotionData != null ? emotionData.Equals(ammoData.emotionData) : ammoData.emotionData == null;
    }

    public override int GetHashCode() => id.GetHashCode() ^ count.GetHashCode() ^ (emotionData == null ? 0 : emotionData.GetHashCode());
  }
}
