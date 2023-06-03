// Decompiled with JetBrains decompiler
// Type: DeactivateOnDLCDisabled
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DeactivateOnDLCDisabled : MonoBehaviour
{
  public DLCPackage.Id requiredDlc;
  private DLCDirector director;

  public void Start()
  {
    director = SRSingleton<GameContext>.Instance.DLCDirector;
    director.onPackageInstalled += CheckDLCState;
    CheckDLCState(requiredDlc);
  }

  public void OnDestroy()
  {
    if (director == null)
      return;
    director.onPackageInstalled -= CheckDLCState;
    director = null;
  }

  private void CheckDLCState(DLCPackage.Id package)
  {
    if (package != requiredDlc)
      return;
    gameObject.SetActive(director.IsPackageInstalledAndEnabled(requiredDlc));
  }
}
