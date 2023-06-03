// Decompiled with JetBrains decompiler
// Type: rail.IRailFloatingWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailFloatingWindow
  {
    RailResult AsyncShowRailFloatingWindow(EnumRailWindowType window_type, string user_data);

    RailResult SetNotifyWindowPosition(
      EnumRailNotifyWindowType window,
      EnumRailNotifyWindowPosition position);

    RailResult AsyncShowStoreWindow(ulong id, RailStoreOptions options, string user_data);

    bool IsFloatingWindowAvailable();

    RailResult AsyncShowDefaultGameStoreWindow(string user_data);

    RailResult AsyncShowChatWindowWithFriend(RailID rail_id);
  }
}
