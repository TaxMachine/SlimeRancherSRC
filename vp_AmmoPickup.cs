// Decompiled with JetBrains decompiler
// Type: vp_AmmoPickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class vp_AmmoPickup : vp_Pickup
{
  public int GiveAmount = 1;

  protected override bool TryGive(vp_FPPlayerEventHandler player)
  {
    if (player.Dead.Active)
      return false;
    for (int index = 0; index < GiveAmount; ++index)
    {
      if (!base.TryGive(player))
      {
        if (!TryReloadIfEmpty(player))
          return false;
        base.TryGive(player);
        return true;
      }
    }
    TryReloadIfEmpty(player);
    return true;
  }

  private bool TryReloadIfEmpty(vp_FPPlayerEventHandler player) => player.CurrentWeaponAmmoCount.Get() <= 0 && !(player.CurrentWeaponClipType.Get() != InventoryName) && player.Reload.TryStart();
}
