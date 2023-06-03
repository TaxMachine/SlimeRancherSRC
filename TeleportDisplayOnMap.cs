// Decompiled with JetBrains decompiler
// Type: TeleportDisplayOnMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class TeleportDisplayOnMap : DisplayOnMap
{
  private TeleportSource teleportSrc;

  public override void Awake()
  {
    base.Awake();
    teleportSrc = GetComponentInChildren<TeleportSource>();
  }

  public override bool ShowOnMap() => base.ShowOnMap() && SRSingleton<SceneContext>.Instance.TeleportNetwork.IsLinkFullyActive(teleportSrc);
}
