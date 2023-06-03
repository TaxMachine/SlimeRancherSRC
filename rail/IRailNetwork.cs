// Decompiled with JetBrains decompiler
// Type: rail.IRailNetwork
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailNetwork
  {
    RailResult AcceptSessionRequest(RailID local_peer, RailID remote_peer);

    RailResult SendData(
      RailID local_peer,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len,
      uint message_type);

    RailResult SendData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len);

    RailResult SendReliableData(
      RailID local_peer,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len,
      uint message_type);

    RailResult SendReliableData(
      RailID local_peer,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len);

    bool IsDataReady(RailID local_peer, out uint data_len, out uint message_type);

    bool IsDataReady(RailID local_peer, out uint data_len);

    RailResult ReadData(
      RailID local_peer,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len,
      uint message_type);

    RailResult ReadData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len);

    RailResult BlockMessageType(RailID local_peer, uint message_type);

    RailResult UnblockMessageType(RailID local_peer, uint message_type);

    RailResult CloseSession(RailID local_peer, RailID remote_peer);

    RailResult ResolveHostname(string domain, List<string> ip_list);
  }
}
