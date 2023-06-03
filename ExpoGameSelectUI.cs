// Decompiled with JetBrains decompiler
// Type: ExpoGameSelectUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class ExpoGameSelectUI : BaseUI
{
  public GameObject mainMenuUIPrefab;

  public void LoadGame(TextAsset asset) => SRSingleton<GameContext>.Instance.AutoSaveDirector.BeginLoad("", asset.name, () => { });

  public override void Close()
  {
    Instantiate(mainMenuUIPrefab);
    base.Close();
  }
}
