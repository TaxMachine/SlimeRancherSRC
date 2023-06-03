// Decompiled with JetBrains decompiler
// Type: rail.IRailDlcHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailDlcHelper
  {
    RailResult AsyncQueryIsOwnedDlcsOnServer(List<RailDlcID> dlc_ids, string user_data);

    RailResult AsyncCheckAllDlcsStateReady(string user_data);

    bool IsDlcInstalled(RailDlcID dlc_id, out string installed_path);

    bool IsDlcInstalled(RailDlcID dlc_id);

    bool IsOwnedDlc(RailDlcID dlc_id);

    uint GetDlcCount();

    bool GetDlcInfo(uint index, RailDlcInfo dlc_info);

    bool AsyncInstallDlc(RailDlcID dlc_id, string user_data);

    bool AsyncRemoveDlc(RailDlcID dlc_id, string user_data);
  }
}
