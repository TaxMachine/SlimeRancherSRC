// Decompiled with JetBrains decompiler
// Type: rail.RoomInfoListFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public class RoomInfoListFilter
  {
    public string room_name_contained;
    public List<RoomInfoListFilterKey> key_filters = new List<RoomInfoListFilterKey>();
    public EnumRailOptionalValue filter_password;
    public EnumRailOptionalValue filter_friends_owned;
    public uint available_slot_at_least;
  }
}
