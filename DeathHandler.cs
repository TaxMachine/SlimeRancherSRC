// Decompiled with JetBrains decompiler
// Type: DeathHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public static class DeathHandler
{
  public static void Kill(
    GameObject gameObject,
    Source source,
    GameObject sourceGameObject,
    string stackTrace)
  {
    Interface interfaceComponent = gameObject.GetInterfaceComponent<Interface>();
    if (interfaceComponent != null)
      interfaceComponent.OnDeath(source, sourceGameObject, stackTrace);
    else
      Destroyer.DestroyActor(gameObject, stackTrace, true);
  }

  public enum Source
  {
    UNDEFINED,
    SLIME_ATTACK,
    SLIME_ATTACK_PLAYER,
    SLIME_CRYSTAL_SPIKES,
    SLIME_DAMAGE_PLAYER_ON_TOUCH,
    SLIME_EXPLODE,
    SLIME_IGNITE,
    SLIME_RAD,
    CHICKEN_VAMPIRISM,
    KILL_ON_TRIGGER,
    EMERGENCY_RETURN,
    FALL_DAMAGE,
  }

  public interface Interface
  {
    void OnDeath(Source source, GameObject sourceGameObject, string stackTrace);
  }
}
