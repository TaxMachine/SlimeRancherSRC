// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PediaV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PediaV02 : PersistedDataSet
  {
    public List<string> unlockedIds = new List<string>();
    public List<string> completedTuts = new List<string>();
    public int progressGivenForPediaCount;

    public override string Identifier => "SRPED";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      progressGivenForPediaCount = reader.ReadInt32();
      unlockedIds = LoadList(reader, (Func<string, string>) (val => val));
      completedTuts = LoadList(reader, (Func<string, string>) (val => val));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(progressGivenForPediaCount);
      WriteList(writer, unlockedIds, val => val);
      WriteList(writer, completedTuts, val => val);
    }

    public static PediaV02 Load(BinaryReader reader)
    {
      PediaV02 pediaV02 = new PediaV02();
      pediaV02.Load(reader.BaseStream);
      return pediaV02;
    }

    public static void AssertAreEqual(PediaV02 expected, PediaV02 actual)
    {
      TestUtil.AssertAreEqual(expected.unlockedIds, actual.unlockedIds, "unlockedIds");
      TestUtil.AssertAreEqual(expected.completedTuts, actual.completedTuts, "completedTuts");
    }
  }
}
