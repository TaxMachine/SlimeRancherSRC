// Decompiled with JetBrains decompiler
// Type: vp_Layer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public sealed class vp_Layer
{
  public static readonly vp_Layer instance = new vp_Layer();
  public const int Default = 0;
  public const int TransparentFX = 1;
  public const int IgnoreRaycast = 2;
  public const int Water = 4;
  public const int MovableObject = 21;
  public const int Ragdoll = 22;
  public const int IgnoreBullets = 24;
  public const int Enemy = 25;
  public const int Pickup = 26;
  public const int Trigger = 27;
  public const int MovingPlatform = 28;
  public const int Weapon = 31;
  public const int Player = 8;
  public const int Launched = 9;
  public const int ActorIgnorePlayer = 11;
  public const int Mountains = 12;
  public const int Held = 13;
  public const int RaycastOnly = 14;
  public const int Actor = 15;
  public const int ActorEchoes = 16;
  public const int Beatrix = 17;
  public const int VacCone = 18;
  public const int Interactable = 19;
  public const int Drone = 20;
  public const int ActorStatic = 21;
  public const int ActorTrigger = 22;
  public const int PenWalls = 29;

  static vp_Layer()
  {
    Physics.IgnoreLayerCollision(8, 29);
    Physics.IgnoreLayerCollision(8, 11);
    Physics.IgnoreLayerCollision(8, 16);
    Physics.IgnoreLayerCollision(8, 14);
    Physics.IgnoreLayerCollision(29, 29);
  }

  private vp_Layer()
  {
  }

  public static void Set(GameObject obj, int layer, bool recursive = false)
  {
    if (layer < 0 || layer > 31)
    {
      Debug.LogError("vp_Layer: Attempted to set layer id out of range [0-31].");
    }
    else
    {
      obj.layer = layer;
      if (!recursive)
        return;
      foreach (Component component in obj.transform)
        Set(component.gameObject, layer, true);
    }
  }

  public static bool IsInMask(int layer, int layerMask) => (layerMask & 1 << layer) == 0;

  public static class Mask
  {
    public const int BulletBlockers = -754974997;
    public const int ExternalBlockers = -675375893;
    public const int PhysicsBlockers = 270532864;
    public const int IgnoreWalkThru = -755066901;
    public const int AnyActor = -2206209;
  }
}
