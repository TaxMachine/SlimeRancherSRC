// Decompiled with JetBrains decompiler
// Type: PlayerData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : DataModule<PlayerData>
{
  public const int CURR_FORMAT_ID = 3;
  public Vector3 playerPos;
  public Vector3 playerRotEuler;
  public int health;
  public int energy;
  public int rad;
  public int currency;
  public Ammo.AmmoData[] ammo;
  public List<PlayerState.Upgrade> upgrades;
  public Dictionary<PlayerState.Upgrade, float> upgradeLocks;
  public List<MailDirector.Mail> mail;
  public int keys;
  public Dictionary<ProgressDirector.ProgressType, int> progress;
  public Dictionary<ProgressDirector.ProgressType, List<float>> delayedProgress;
  public int currencyEverCollected;
  public PlayerState.GameMode gameMode;
  public Identifiable.Id gameIconId = Identifiable.Id.CARROT_VEGGIE;
  public string version = "0.3.0";

  public static void AssertEquals(PlayerData dataA, PlayerData dataB)
  {
    TestUtil.AreApproximatelyEqual(dataA.playerPos, dataB.playerPos, 0.01f, "Player position: " + dataA.playerPos + " vs " + dataB.playerPos);
    TestUtil.AreApproximatelyEqual(dataA.playerRotEuler, dataB.playerRotEuler, 0.01f, "Player rotation: " + dataA.playerRotEuler + " vs " + dataB.playerRotEuler);
  }

  private static string PrintAmmo(Ammo.AmmoData[] allAmmo)
  {
    string str1 = "Ammo: ";
    foreach (Ammo.AmmoData ammoData in allAmmo)
    {
      string str2 = str1;
      string str3;
      if (ammoData != null)
        str3 = ammoData.id.ToString() + ":" + ammoData.count + ",";
      else
        str3 = "null,";
      str1 = str2 + str3;
    }
    return str1;
  }

  private static string PrintDelayedProgress(
    Dictionary<ProgressDirector.ProgressType, List<float>> delayedProg)
  {
    string str = "DelayedProg: ";
    foreach (KeyValuePair<ProgressDirector.ProgressType, List<float>> keyValuePair in delayedProg)
    {
      str = str + keyValuePair.Key + ":";
      foreach (float num in keyValuePair.Value)
        str = str + num + ",";
      str += ";";
    }
    return str;
  }
}
