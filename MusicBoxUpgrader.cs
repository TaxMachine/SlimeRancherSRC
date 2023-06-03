// Decompiled with JetBrains decompiler
// Type: MusicBoxUpgrader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MusicBoxUpgrader : PlotUpgrader
{
  public GameObject musicBox;

  public override void Apply(LandPlot.Upgrade upgrade)
  {
    if (upgrade != LandPlot.Upgrade.MUSIC_BOX)
      return;
    musicBox.SetActive(true);
  }
}
