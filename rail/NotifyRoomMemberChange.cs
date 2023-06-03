// Decompiled with JetBrains decompiler
// Type: rail.NotifyRoomMemberChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public class NotifyRoomMemberChange : EventBase
  {
    public ulong changer_id;
    public ulong id_for_making_change;
    public EnumRoomMemberActionStatus state_change;
    public ulong room_id;
  }
}
