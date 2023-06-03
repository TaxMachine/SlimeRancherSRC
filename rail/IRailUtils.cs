// Decompiled with JetBrains decompiler
// Type: rail.IRailUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailUtils
  {
    uint GetTimeCountSinceGameLaunch();

    uint GetTimeCountSinceComputerLaunch();

    uint GetTimeFromServer();

    RailGameID GetGameID();

    RailResult AsyncGetImageData(
      string image_path,
      uint scale_to_width,
      uint scale_to_height,
      string user_data);

    void GetErrorString(RailResult result, out string error_string);

    RailResult DirtyWordsFilter(
      string words,
      bool replace_sensitive,
      RailDirtyWordsCheckResult check_result);

    EnumRailPlatformType GetRailPlatformType();

    RailResult GetLaunchAppParameters(EnumRailLaunchAppType app_type, out string parameter);

    RailResult GetPlatformLanguageCode(out string language_code);

    RailResult SetWarningMessageCallback(RailWarningMessageCallbackFunction callback);
  }
}
