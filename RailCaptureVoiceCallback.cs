// Decompiled with JetBrains decompiler
// Type: RailCaptureVoiceCallback
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using rail;

public delegate void RailCaptureVoiceCallback(
  EnumRailVoiceCaptureFormat fmt,
  bool is_last_package,
  byte[] encoded_buffer,
  uint encoded_length);
