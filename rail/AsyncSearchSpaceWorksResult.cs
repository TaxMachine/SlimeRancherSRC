// Decompiled with JetBrains decompiler
// Type: rail.AsyncSearchSpaceWorksResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public class AsyncSearchSpaceWorksResult : EventBase
  {
    public uint total_available_works;
    public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
  }
}
