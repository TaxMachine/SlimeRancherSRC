// Decompiled with JetBrains decompiler
// Type: rail.IRailVoiceChannelImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailVoiceChannelImpl : RailObject, IRailVoiceChannel, IRailComponent
  {
    internal IRailVoiceChannelImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailVoiceChannelImpl()
    {
    }

    public virtual RailVoiceChannelID GetVoiceChannelID()
    {
      IntPtr voiceChannelId1 = RAIL_API_PINVOKE.IRailVoiceChannel_GetVoiceChannelID(swigCPtr_);
      RailVoiceChannelID voiceChannelId2 = new RailVoiceChannelID();
      RailVoiceChannelID ret = voiceChannelId2;
      RailConverter.Cpp2Csharp(voiceChannelId1, ret);
      return voiceChannelId2;
    }

    public virtual RailResult GetVoiceChannelName(out string name)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailVoiceChannel_GetVoiceChannelName(swigCPtr_, num);
      }
      finally
      {
        name = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult JoinVoiceChannel() => (RailResult) RAIL_API_PINVOKE.IRailVoiceChannel_JoinVoiceChannel(swigCPtr_);

    public virtual RailResult LeaveVoiceChannel() => (RailResult) RAIL_API_PINVOKE.IRailVoiceChannel_LeaveVoiceChannel(swigCPtr_);

    public virtual EnumRailVoiceChannelSpeakerState GetSpeakerState() => (EnumRailVoiceChannelSpeakerState) RAIL_API_PINVOKE.IRailVoiceChannel_GetSpeakerState(swigCPtr_);

    public virtual RailResult MuteSpeaker() => (RailResult) RAIL_API_PINVOKE.IRailVoiceChannel_MuteSpeaker(swigCPtr_);

    public virtual RailResult ResumeSpeaker() => (RailResult) RAIL_API_PINVOKE.IRailVoiceChannel_ResumeSpeaker(swigCPtr_);

    public virtual RailResult GetUsers(List<RailID> user_list)
    {
      IntPtr num = user_list == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailVoiceChannel_GetUsers(swigCPtr_, num);
      }
      finally
      {
        if (user_list != null)
          RailConverter.Cpp2Csharp(num, user_list);
        RAIL_API_PINVOKE.delete_RailArrayRailID(num);
      }
    }

    public virtual RailResult AddUsers(List<RailID> user_list)
    {
      IntPtr num = user_list == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      if (user_list != null)
        RailConverter.Csharp2Cpp(user_list, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailVoiceChannel_AddUsers(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailID(num);
      }
    }

    public virtual RailResult RemoveUsers(List<RailID> user_list)
    {
      IntPtr num = user_list == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      if (user_list != null)
        RailConverter.Csharp2Cpp(user_list, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailVoiceChannel_RemoveUsers(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailID(num);
      }
    }

    public virtual RailResult CloseChannel() => (RailResult) RAIL_API_PINVOKE.IRailVoiceChannel_CloseChannel(swigCPtr_);

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
