// Decompiled with JetBrains decompiler
// Type: SiloSlotUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SiloSlotUI : StorageSlotUI
{
  [Tooltip("Silo slot index.")]
  public int slotIdx;
  private SiloStorage storage;

  public override void Awake()
  {
    base.Awake();
    storage = GetComponentInParent<SiloStorage>();
  }

  protected override Identifiable.Id GetCurrentId() => storage.GetSlotIdentifiable(slotIdx);

  protected override int GetCurrentCount() => storage.GetSlotCount(slotIdx);
}
