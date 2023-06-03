// Decompiled with JetBrains decompiler
// Type: rail.IRailVoiceHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailVoiceHelperImpl : RailObject, IRailVoiceHelper
  {
    internal IRailVoiceHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailVoiceHelperImpl()
    {
    }

    public virtual IRailVoiceChannel AsyncCreateVoiceChannel(
      CreateVoiceChannelOption options,
      string channel_name,
      string user_data,
      out int result)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateVoiceChannelOption__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr voiceChannelSwig0 = RAIL_API_PINVOKE.IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_0(swigCPtr_, num, channel_name, user_data, out result);
        return voiceChannelSwig0 == IntPtr.Zero ? null : (IRailVoiceChannel) new IRailVoiceChannelImpl(voiceChannelSwig0);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateVoiceChannelOption(num);
      }
    }

    public virtual IRailVoiceChannel AsyncCreateVoiceChannel(
      CreateVoiceChannelOption options,
      string channel_name,
      string user_data)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateVoiceChannelOption__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr voiceChannelSwig1 = RAIL_API_PINVOKE.IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_1(swigCPtr_, num, channel_name, user_data);
        return voiceChannelSwig1 == IntPtr.Zero ? null : (IRailVoiceChannel) new IRailVoiceChannelImpl(voiceChannelSwig1);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateVoiceChannelOption(num);
      }
    }

    public virtual IRailVoiceChannel AsyncCreateVoiceChannel(
      CreateVoiceChannelOption options,
      string channel_name)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateVoiceChannelOption__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr voiceChannelSwig2 = RAIL_API_PINVOKE.IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_2(swigCPtr_, num, channel_name);
        return voiceChannelSwig2 == IntPtr.Zero ? null : (IRailVoiceChannel) new IRailVoiceChannelImpl(voiceChannelSwig2);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateVoiceChannelOption(num);
      }
    }

    public virtual IRailVoiceChannel AsyncCreateVoiceChannel(CreateVoiceChannelOption options)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateVoiceChannelOption__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr voiceChannelSwig3 = RAIL_API_PINVOKE.IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_3(swigCPtr_, num);
        return voiceChannelSwig3 == IntPtr.Zero ? null : (IRailVoiceChannel) new IRailVoiceChannelImpl(voiceChannelSwig3);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateVoiceChannelOption(num);
      }
    }

    public virtual IRailVoiceChannel OpenVoiceChannel(
      RailVoiceChannelID voice_channel_id,
      out int result)
    {
      IntPtr num = voice_channel_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceChannelID__SWIG_0();
      if (voice_channel_id != null)
        RailConverter.Csharp2Cpp(voice_channel_id, num);
      try
      {
        IntPtr cPtr = RAIL_API_PINVOKE.IRailVoiceHelper_OpenVoiceChannel(swigCPtr_, num, out result);
        return cPtr == IntPtr.Zero ? null : (IRailVoiceChannel) new IRailVoiceChannelImpl(cPtr);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailVoiceChannelID(num);
      }
    }

    public virtual RailResult SetupVoiceCapture(
      RailVoiceCaptureOption options,
      RailCaptureVoiceCallback callback)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceCaptureOption__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailVoiceHelper_SetupVoiceCapture__SWIG_0(swigCPtr_, num, callback);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailVoiceCaptureOption(num);
      }
    }

    public virtual RailResult SetupVoiceCapture(RailVoiceCaptureOption options)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceCaptureOption__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailVoiceHelper_SetupVoiceCapture__SWIG_1(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailVoiceCaptureOption(num);
      }
    }

    public virtual RailResult StartVoiceCapturing(uint duration_milliseconds, bool repeat) => (RailResult) RAIL_API_PINVOKE.IRailVoiceHelper_StartVoiceCapturing__SWIG_0(swigCPtr_, duration_milliseconds, repeat);

    public virtual RailResult StartVoiceCapturing(uint duration_milliseconds) => (RailResult) RAIL_API_PINVOKE.IRailVoiceHelper_StartVoiceCapturing__SWIG_1(swigCPtr_, duration_milliseconds);

    public virtual RailResult StartVoiceCapturing() => (RailResult) RAIL_API_PINVOKE.IRailVoiceHelper_StartVoiceCapturing__SWIG_2(swigCPtr_);

    public virtual RailResult StopVoiceCapturing() => (RailResult) RAIL_API_PINVOKE.IRailVoiceHelper_StopVoiceCapturing(swigCPtr_);

    public virtual RailResult GetCapturedVoiceData(
      byte[] buffer,
      uint buffer_length,
      out uint encoded_bytes_written)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailVoiceHelper_GetCapturedVoiceData(swigCPtr_, buffer, buffer_length, out encoded_bytes_written);
    }

    public virtual RailResult DecodeVoice(
      byte[] encoded_buffer,
      uint encoded_length,
      byte[] pcm_buffer,
      uint pcm_buffer_length,
      out uint pcm_buffer_written)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailVoiceHelper_DecodeVoice(swigCPtr_, encoded_buffer, encoded_length, pcm_buffer, pcm_buffer_length, out pcm_buffer_written);
    }
  }
}
