// Decompiled with JetBrains decompiler
// Type: rail.NotifyRoomMemberKicked
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public class NotifyRoomMemberKicked : EventBase
  {
    public ulong id_for_making_kick;
    public uint due_to_kicker_lost_connect;
    public ulong room_id;
    public ulong kicked_id;
  }
}
