// Decompiled with JetBrains decompiler
// Type: AmmoModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

public class AmmoModel
{
  public Ammo.Slot[] slots;
  public int usableSlots;
  private Func<Identifiable.Id, int, int> slotMaxCountFunction;

  public void IncreaseUsableSlots(int usableSlots) => this.usableSlots = Math.Max(this.usableSlots, usableSlots);

  public int GetSlotMaxCount(Identifiable.Id id, int index) => slotMaxCountFunction(id, index);

  public void SetSlotMaxCountFunction(
    Func<Identifiable.Id, int, int> slotMaxCountFunction)
  {
    this.slotMaxCountFunction = slotMaxCountFunction;
  }

  public void Push(Ammo.Slot[] slots) => this.slots = slots;

  public void Pull(out Ammo.Slot[] slots) => slots = this.slots;

  public void Reset(int numSlots, int initUsableSlots, int[] deprecatedMaxSlotCounts) => Reset(numSlots, initUsableSlots, (id, index) => deprecatedMaxSlotCounts[index]);

  public void Reset(
    int numSlots,
    int initUsableSlots,
    Func<Identifiable.Id, int, int> initSlotMaxCountFunction)
  {
    slots = new Ammo.Slot[numSlots];
    usableSlots = initUsableSlots;
    slotMaxCountFunction = initSlotMaxCountFunction;
  }
}
