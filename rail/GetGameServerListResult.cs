// Decompiled with JetBrains decompiler
// Type: rail.GetGameServerListResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public class GetGameServerListResult : EventBase
  {
    public List<GameServerInfo> server_info = new List<GameServerInfo>();
    public uint total_num;
    public uint start_index;
    public uint end_index;
  }
}
