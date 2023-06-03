// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.AppearancesV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class AppearancesV01 : PersistedDataSet
  {
    public Dictionary<Identifiable.Id, List<SlimeAppearance.AppearanceSaveSet>> unlocks = new Dictionary<Identifiable.Id, List<SlimeAppearance.AppearanceSaveSet>>();
    public Dictionary<Identifiable.Id, SlimeAppearance.AppearanceSaveSet> selections = new Dictionary<Identifiable.Id, SlimeAppearance.AppearanceSaveSet>();

    public override string Identifier => "SRAPP";

    public override uint Version => 1;

    public static AppearancesV01 Load(BinaryReader reader)
    {
      AppearancesV01 appearancesV01 = new AppearancesV01();
      appearancesV01.Load(reader.BaseStream);
      return appearancesV01;
    }

    protected override void LoadData(BinaryReader reader)
    {
      unlocks = LoadDictionary(reader, r => (Identifiable.Id) r.ReadInt32(), r => LoadList(r, (Func<int, SlimeAppearance.AppearanceSaveSet>) (val => (SlimeAppearance.AppearanceSaveSet) val)));
      selections = LoadDictionary(reader, r => (Identifiable.Id) r.ReadInt32(), r => (SlimeAppearance.AppearanceSaveSet) r.ReadInt32());
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteDictionary(writer, unlocks, (w, v) => w.Write((int) v), (w, v) => WriteList(w, v, val => (int) val));
      WriteDictionary(writer, selections, (w, v) => w.Write((int) v), (w, v) => w.Write((int) v));
    }

    public static void AssertAreEqual(AppearancesV01 expected, AppearancesV01 actual)
    {
      TestUtil.AssertAreEqual(expected.unlocks, actual.unlocks, (expectedUnlockList, actualUnlockList) => TestUtil.AssertAreEqual(expectedUnlockList, actualUnlockList, "unlock.Value"), "unlocks");
      TestUtil.AssertAreEqual(expected.selections, actual.selections, (expectedSelection, actualSelection) => { }, "selections");
    }
  }
}
