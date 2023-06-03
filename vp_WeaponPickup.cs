// Decompiled with JetBrains decompiler
// Type: vp_WeaponPickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class vp_WeaponPickup : vp_Pickup
{
  public int AmmoIncluded;

  protected override bool TryGive(vp_FPPlayerEventHandler player)
  {
    if (player.Dead.Active || !base.TryGive(player))
      return false;
    int num1 = player.SetWeaponByName.Try(InventoryName) ? 1 : 0;
    if (AmmoIncluded > 0)
    {
      int num2 = player.AddAmmo.Try(new object[2]
      {
        InventoryName,
        AmmoIncluded
      }) ? 1 : 0;
    }
    return true;
  }
}
