// Decompiled with JetBrains decompiler
// Type: rail.IRailVoiceHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailVoiceHelper
  {
    IRailVoiceChannel AsyncCreateVoiceChannel(
      CreateVoiceChannelOption options,
      string channel_name,
      string user_data,
      out int result);

    IRailVoiceChannel AsyncCreateVoiceChannel(
      CreateVoiceChannelOption options,
      string channel_name,
      string user_data);

    IRailVoiceChannel AsyncCreateVoiceChannel(CreateVoiceChannelOption options, string channel_name);

    IRailVoiceChannel AsyncCreateVoiceChannel(CreateVoiceChannelOption options);

    IRailVoiceChannel OpenVoiceChannel(RailVoiceChannelID voice_channel_id, out int result);

    RailResult SetupVoiceCapture(RailVoiceCaptureOption options, RailCaptureVoiceCallback callback);

    RailResult SetupVoiceCapture(RailVoiceCaptureOption options);

    RailResult StartVoiceCapturing(uint duration_milliseconds, bool repeat);

    RailResult StartVoiceCapturing(uint duration_milliseconds);

    RailResult StartVoiceCapturing();

    RailResult StopVoiceCapturing();

    RailResult GetCapturedVoiceData(
      byte[] buffer,
      uint buffer_length,
      out uint encoded_bytes_written);

    RailResult DecodeVoice(
      byte[] encoded_buffer,
      uint encoded_length,
      byte[] pcm_buffer,
      uint pcm_buffer_length,
      out uint pcm_buffer_written);
  }
}
