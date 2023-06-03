// Decompiled with JetBrains decompiler
// Type: rail.IRailPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailPlayer
  {
    bool AlreadyLoggedIn();

    RailID GetRailID();

    RailResult GetPlayerDataPath(out string path);

    RailResult AsyncAcquireSessionTicket(string user_data);

    RailResult AsyncStartSessionWithPlayer(
      RailSessionTicket player_ticket,
      RailID player_rail_id,
      string user_data);

    void TerminateSessionOfPlayer(RailID player_rail_id);

    void AbandonSessionTicket(RailSessionTicket session_ticket);

    RailResult GetPlayerName(out string name);

    EnumRailPlayerOwnershipType GetPlayerOwnershipType();

    RailResult AsyncGetGamePurchaseKey(string user_data);
  }
}
