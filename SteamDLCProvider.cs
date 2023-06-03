// Decompiled with JetBrains decompiler
// Type: SteamDLCProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamDLCProvider : DLCProvider
{
  private static readonly Lookup<DLCPackage.Id, AppId_t> LOOKUP = new Lookup<DLCPackage.Id, AppId_t>(DLCPackage.IdComparer.Instance)
  {
    {
      DLCPackage.Id.PLAYSET_SCIFI,
      new AppId_t(982310U)
    },
    {
      DLCPackage.Id.SECRET_STYLE,
      new AppId_t(1079180U)
    }
  };

  public SteamDLCProvider()
    : base(LOOKUP.Keys)
  {
  }

  public override IEnumerator Refresh()
  {
    SteamDLCProvider steamDlcProvider = this;
    if (!SteamManager.Initialized)
    {
      Log.Debug("Ignoring SteamDLCProvider.Refresh; Steamworks is not initialized.");
    }
    else
    {
      for (int iDLC = 0; iDLC < SteamApps.GetDLCCount(); ++iDLC)
      {
        AppId_t pAppID;
        DLCPackage.Id id;
        if (SteamApps.BGetDLCDataByIndex(iDLC, out pAppID, out bool _, out string _, 128) && SteamApps.BIsDlcInstalled(pAppID) && LOOKUP.TryGetValue(pAppID, out id))
          steamDlcProvider.SetState(id, DLCPackage.State.INSTALLED);
      }
      yield return null;
    }
  }

  public override void ShowInStore(DLCPackage.Id package)
  {
    AppId_t appIdT;
    if (!LOOKUP.TryGetValue(package, out appIdT))
      return;
    Application.OpenURL(string.Format("https://store.steampowered.com/app/{0}", appIdT));
  }
}
