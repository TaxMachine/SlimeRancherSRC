// Decompiled with JetBrains decompiler
// Type: PediaData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

[Serializable]
public class PediaData : DataModule<PediaData>
{
  public const int CURR_FORMAT_ID = 1;
  public List<string> unlockedIds = new List<string>();
  public List<string> completedTuts = new List<string>();
  public int progressGivenForPediaCount;

  public static void AssertEquals(PediaData dataA, PediaData dataB)
  {
  }
}
