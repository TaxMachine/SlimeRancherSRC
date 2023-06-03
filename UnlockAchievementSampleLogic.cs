// Decompiled with JetBrains decompiler
// Type: UnlockAchievementSampleLogic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Microsoft.Xbox;
using UnityEngine;
using UnityEngine.UI;

public class UnlockAchievementSampleLogic : MonoBehaviour
{
  public Text output;

  public void UnlockAchievement()
  {
    Gdk.Helpers.UnlockAchievement("1");
    output.text = "Unlocking achievement...";
  }
}
