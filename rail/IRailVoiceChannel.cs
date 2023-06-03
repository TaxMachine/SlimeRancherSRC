// Decompiled with JetBrains decompiler
// Type: rail.IRailVoiceChannel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailVoiceChannel : IRailComponent
  {
    RailVoiceChannelID GetVoiceChannelID();

    RailResult GetVoiceChannelName(out string name);

    RailResult JoinVoiceChannel();

    RailResult LeaveVoiceChannel();

    EnumRailVoiceChannelSpeakerState GetSpeakerState();

    RailResult MuteSpeaker();

    RailResult ResumeSpeaker();

    RailResult GetUsers(List<RailID> user_list);

    RailResult AddUsers(List<RailID> user_list);

    RailResult RemoveUsers(List<RailID> user_list);

    RailResult CloseChannel();
  }
}
