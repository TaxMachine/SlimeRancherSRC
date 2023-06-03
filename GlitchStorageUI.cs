// Decompiled with JetBrains decompiler
// Type: GlitchStorageUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class GlitchStorageUI : StorageSlotUI
{
  private GlitchStorage storage;

  public override void Awake()
  {
    base.Awake();
    storage = GetComponentInParent<GlitchStorage>();
  }

  protected override Identifiable.Id GetCurrentId() => storage.selected;

  protected override int GetCurrentCount() => storage.count;
}
