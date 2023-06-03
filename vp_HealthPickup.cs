// Decompiled with JetBrains decompiler
// Type: vp_HealthPickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_HealthPickup : vp_Pickup
{
  public float Health = 1f;

  protected override bool TryGive(vp_FPPlayerEventHandler player)
  {
    if (player.Health.Get() < 0.0 || player.Health.Get() >= (double) player.MaxHealth.Get())
      return false;
    player.Health.Set(Mathf.Min(player.MaxHealth.Get(), player.Health.Get() + Health));
    return true;
  }
}
