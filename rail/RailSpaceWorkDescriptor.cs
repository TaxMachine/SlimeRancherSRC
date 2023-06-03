// Decompiled with JetBrains decompiler
// Type: rail.RailSpaceWorkDescriptor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public class RailSpaceWorkDescriptor
  {
    public List<RailSpaceWorkVoteDetail> vote_details = new List<RailSpaceWorkVoteDetail>();
    public string description;
    public string preview_url;
    public SpaceWorkID id = new SpaceWorkID();
    public string detail_url;
    public List<RailID> uploader_ids = new List<RailID>();
    public string name;
  }
}
