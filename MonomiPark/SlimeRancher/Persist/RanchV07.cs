// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.RanchV07
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class RanchV07 : VersionedPersistedDataSet<RanchV06>
  {
    public List<LandPlotV08> plots;
    public Dictionary<string, AccessDoor.State> accessDoorStates;
    public Dictionary<RanchDirector.PaletteType, RanchDirector.Palette> palettes;
    public Dictionary<string, double> ranchFastForward;

    public override string Identifier => "SRRANCH";

    public override uint Version => 7;

    public RanchV07()
    {
    }

    public RanchV07(RanchV06 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      plots = LoadList<LandPlotV08>(reader);
      accessDoorStates = LoadDictionary(reader, r => r.ReadString(), r => (AccessDoor.State) r.ReadInt32());
      palettes = LoadDictionary(reader, r => (RanchDirector.PaletteType) r.ReadInt32(), r => (RanchDirector.Palette) r.ReadInt32());
      ranchFastForward = LoadDictionary(reader, r => r.ReadString(), r => r.ReadDouble());
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteList(writer, plots);
      WriteDictionary(writer, accessDoorStates, (w, k) => w.Write(k), (w, v) => w.Write((int) v));
      WriteDictionary(writer, palettes, (w, k) => w.Write((int) k), (w, v) => w.Write((int) v));
      WriteDictionary(writer, ranchFastForward, (w, k) => w.Write(k), (w, v) => w.Write(v));
    }

    public static RanchV07 Load(BinaryReader reader)
    {
      RanchV07 ranchV07 = new RanchV07();
      ranchV07.Load(reader.BaseStream);
      return ranchV07;
    }

    protected override void UpgradeFrom(RanchV06 legacyData)
    {
      plots = UpgradeFrom(legacyData.plots);
      accessDoorStates = legacyData.accessDoorStates;
      palettes = legacyData.palettes;
      ranchFastForward = new Dictionary<string, double>();
    }

    private List<LandPlotV08> UpgradeFrom(List<LandPlotV07> legacyData)
    {
      if (legacyData == null)
        return null;
      List<LandPlotV08> landPlotV08List = new List<LandPlotV08>();
      foreach (LandPlotV07 legacyData1 in legacyData)
        landPlotV08List.Add(new LandPlotV08(legacyData1));
      return landPlotV08List;
    }

    public static void AssertAreEqual(RanchV07 expected, RanchV07 actual)
    {
      TestUtil.AssertAreEqual(expected.plots, actual.plots, (e, a, m) => LandPlotV08.AssertAreEqual(e, a), "plots");
      TestUtil.AssertAreEqual(expected.accessDoorStates, actual.accessDoorStates, (e, a) => { }, "accessDoorStates");
      TestUtil.AssertAreEqual(expected.palettes, actual.palettes, (e, a) => { }, "palettes");
      TestUtil.AssertAreEqual(expected.ranchFastForward, actual.ranchFastForward, (e, a) => { }, "ranchFastForward");
    }

    public static void AssertAreEqual(RanchV06 expected, RanchV07 actual)
    {
      TestUtil.AssertAreEqual(expected.plots, actual.plots, (e, a, m) => LandPlotV08.AssertAreEqual(e, a), "plots");
      TestUtil.AssertAreEqual(expected.accessDoorStates, actual.accessDoorStates, (e, a) => { }, "accessDoorStates");
      TestUtil.AssertAreEqual(expected.palettes, actual.palettes, (e, a) => { }, "palettes");
    }
  }
}
