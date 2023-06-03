// Decompiled with JetBrains decompiler
// Type: rail.PlayerPersonalInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public class PlayerPersonalInfo
  {
    public EnumRailPlayerOnLineState state;
    public string avatar_url;
    public uint rail_level;
    public RailID rail_id = new RailID();
    public string rail_name;
    public RailResult err_code;
  }
}
