// Decompiled with JetBrains decompiler
// Type: rail.RailPublishFileToUserSpaceOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public class RailPublishFileToUserSpaceOption
  {
    public RailKeyValue key_value = new RailKeyValue();
    public string description;
    public List<string> tags = new List<string>();
    public EnumRailSpaceWorkShareLevel level;
    public string version;
    public string preview_path_filename;
    public EnumRailSpaceWorkType type;
    public string space_work_name;
  }
}
