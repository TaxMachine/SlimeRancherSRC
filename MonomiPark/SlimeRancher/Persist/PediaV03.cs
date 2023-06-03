// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PediaV03
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PediaV03 : VersionedPersistedDataSet<PediaV02>
  {
    public List<string> unlockedIds = new List<string>();
    public List<string> completedTuts = new List<string>();
    public List<string> popupQueue = new List<string>();
    public int progressGivenForPediaCount;

    public override string Identifier => "SRPED";

    public override uint Version => 3;

    public PediaV03()
    {
    }

    public PediaV03(PediaV02 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      progressGivenForPediaCount = reader.ReadInt32();
      unlockedIds = LoadList(reader, (Func<string, string>) (val => val));
      completedTuts = LoadList(reader, (Func<string, string>) (val => val));
      popupQueue = LoadList(reader, (Func<string, string>) (val => val));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(progressGivenForPediaCount);
      WriteList(writer, unlockedIds, val => val);
      WriteList(writer, completedTuts, val => val);
      WriteList(writer, popupQueue, val => val);
    }

    public static PediaV03 Load(BinaryReader reader)
    {
      PediaV03 pediaV03 = new PediaV03();
      pediaV03.Load(reader.BaseStream);
      return pediaV03;
    }

    public static void AssertAreEqual(PediaV03 expected, PediaV03 actual)
    {
      TestUtil.AssertAreEqual(expected.unlockedIds, actual.unlockedIds, "unlockedIds");
      TestUtil.AssertAreEqual(expected.completedTuts, actual.completedTuts, "completedTuts");
      TestUtil.AssertAreEqual(expected.popupQueue, actual.popupQueue, "popupQueue");
    }

    public static void AssertAreEqual(PediaV02 expected, PediaV03 actual)
    {
      TestUtil.AssertAreEqual(expected.unlockedIds, actual.unlockedIds, "unlockedIds");
      TestUtil.AssertAreEqual(expected.completedTuts, actual.completedTuts, "completedTuts");
    }

    protected override void UpgradeFrom(PediaV02 legacyData)
    {
      unlockedIds = new List<string>();
      foreach (string unlockedId in legacyData.unlockedIds)
        unlockedIds.Add(UpgradePediaId(unlockedId));
      completedTuts = legacyData.completedTuts;
      progressGivenForPediaCount = legacyData.progressGivenForPediaCount;
      popupQueue = new List<string>();
    }

    private string UpgradePediaId(string pediaId)
    {
      switch (pediaId)
      {
        case "VACPACK":
          return "TUTORIALS";
        case "SHOOTING":
          return "CORRALLING";
        default:
          return pediaId;
      }
    }
  }
}
