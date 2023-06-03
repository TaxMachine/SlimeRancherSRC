// Decompiled with JetBrains decompiler
// Type: rail.IRailPlayerStats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailPlayerStats : IRailComponent
  {
    RailID GetRailID();

    RailResult AsyncRequestStats(string user_data);

    RailResult GetStatValue(string name, out int data);

    RailResult GetStatValue(string name, out double data);

    RailResult SetStatValue(string name, int data);

    RailResult SetStatValue(string name, double data);

    RailResult UpdateAverageStatValue(string name, double data);

    RailResult AsyncStoreStats(string user_data);

    RailResult ResetAllStats();
  }
}
