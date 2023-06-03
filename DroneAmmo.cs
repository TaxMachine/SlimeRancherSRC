// Decompiled with JetBrains decompiler
// Type: DroneAmmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

public class DroneAmmo : Ammo
{
  public const int MAX_COUNT = 50;

  public DroneAmmo()
    : base(SRSingleton<SceneContext>.Instance.PlayerState.GetPotentialAmmo(), 1, 1, new Predicate<Identifiable.Id>[1], (id, index) => 50)
  {
  }

  public Identifiable.Id Pop()
  {
    Identifiable.Id slotName = GetSlotName();
    Decrement(slotName);
    return slotName;
  }

  public Identifiable.Id GetSlotName() => GetSlotName(0);

  public bool MaybeAddToSlot(Identifiable.Id id) => MaybeAddToSpecificSlot(id, null, 0);

  public new bool IsEmpty() => GetSlotCount() <= 0;

  public bool IsFull() => GetSlotCount() >= GetSlotMaxCount();

  public int GetSlotCount() => GetSlotCount(0);

  public int GetSlotMaxCount() => GetSlotMaxCount(0);

  public new bool CouldAddToSlot(Identifiable.Id id) => CouldAddToSlot(id, 0, false);

  public bool Any() => GetSlotCount() > 0;

  public bool None() => GetSlotCount() <= 0;
}
