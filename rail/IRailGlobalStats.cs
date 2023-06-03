// Decompiled with JetBrains decompiler
// Type: rail.IRailGlobalStats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailGlobalStats : IRailComponent
  {
    RailResult AsyncRequestGlobalStats(string user_data);

    RailResult GetGlobalStatValue(string name, out long data);

    RailResult GetGlobalStatValue(string name, out double data);

    RailResult GetGlobalStatValueHistory(
      string name,
      long[] global_stats_data,
      uint data_size,
      out int num_global_stats);

    RailResult GetGlobalStatValueHistory(
      string name,
      double[] global_stats_data,
      uint data_size,
      out int num_global_stats);
  }
}
