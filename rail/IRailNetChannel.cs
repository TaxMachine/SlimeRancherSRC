// Decompiled with JetBrains decompiler
// Type: rail.IRailNetChannel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailNetChannel
  {
    RailResult AsyncCreateChannel(RailID local_peer, string user_data);

    RailResult AsyncJoinChannel(RailID local_peer, ulong channel_id, string user_data);

    RailResult AsyncInviteMemberToChannel(
      RailID local_peer,
      ulong channel_id,
      List<RailID> remote_peers,
      string user_data);

    RailResult GetAllMembers(RailID local_peer, ulong channel_id, List<RailID> remote_peers);

    RailResult SendDataToChannel(
      RailID local_peer,
      ulong channel_id,
      byte[] data_buf,
      uint data_len,
      uint message_type);

    RailResult SendDataToChannel(
      RailID local_peer,
      ulong channel_id,
      byte[] data_buf,
      uint data_len);

    RailResult SendDataToMember(
      RailID local_peer,
      ulong channel_id,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len,
      uint message_type);

    RailResult SendDataToMember(
      RailID local_peer,
      ulong channel_id,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len);

    bool IsDataReady(
      RailID local_peer,
      out ulong channel_id,
      out uint data_len,
      out uint message_type);

    bool IsDataReady(RailID local_peer, out ulong channel_id, out uint data_len);

    RailResult ReadData(
      RailID local_peer,
      ulong channel_id,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len,
      uint message_type);

    RailResult ReadData(
      RailID local_peer,
      ulong channel_id,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len);

    RailResult BlockMessageType(RailID local_peer, ulong channel_id, uint message_type);

    RailResult UnblockMessageType(RailID local_peer, ulong channel_id, uint message_type);

    RailResult ExitChannel(RailID local_peer, ulong channel_id);
  }
}
