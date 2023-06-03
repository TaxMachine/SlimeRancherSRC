// Decompiled with JetBrains decompiler
// Type: rail.RailSystemState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public enum RailSystemState
  {
    kSystemStateUnknown = 0,
    kSystemStatePlatformOnline = 1,
    kSystemStatePlatformOffline = 2,
    kSystemStatePlatformExit = 3,
    kSystemStatePlayerOwnershipExpired = 20, // 0x00000014
    kSystemStatePlayerOwnershipActivated = 21, // 0x00000015
  }
}
