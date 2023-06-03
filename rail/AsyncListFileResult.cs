// Decompiled with JetBrains decompiler
// Type: rail.AsyncListFileResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public class AsyncListFileResult : EventBase
  {
    public List<RailStreamFileInfo> file_list = new List<RailStreamFileInfo>();
    public uint try_list_file_num;
    public uint all_file_num;
    public uint start_index;
  }
}
