// Decompiled with JetBrains decompiler
// Type: rail.IRailStorageHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailStorageHelper
  {
    IRailFile OpenFile(string filename, out int result);

    IRailFile OpenFile(string filename);

    IRailFile CreateFile(string filename, out int result);

    IRailFile CreateFile(string filename);

    bool IsFileExist(string filename);

    bool ListFiles(List<string> filelist);

    RailResult RemoveFile(string filename);

    bool IsFileSyncedToCloud(string filename);

    RailResult GetFileTimestamp(string filename, out ulong time_stamp);

    uint GetFileCount();

    RailResult GetFileNameAndSize(uint file_index, out string filename, out ulong file_size);

    RailResult AsyncQueryQuota();

    RailResult SetSyncFileOption(string filename, RailSyncFileOption option);

    bool IsCloudStorageEnabledForApp();

    bool IsCloudStorageEnabledForPlayer();

    RailResult AsyncPublishFileToUserSpace(
      RailPublishFileToUserSpaceOption option,
      string user_data);

    IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option, out int result);

    IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option);

    RailResult AsyncListStreamFiles(
      string contents,
      RailListStreamFileOption option,
      string user_data);

    RailResult AsyncRenameStreamFile(string old_filename, string new_filename, string user_data);

    RailResult AsyncDeleteStreamFile(string filename, string user_data);
  }
}
