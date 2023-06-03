// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.RanchV06
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class RanchV06 : VersionedPersistedDataSet<RanchV05>
  {
    public List<LandPlotV07> plots;
    public Dictionary<string, AccessDoor.State> accessDoorStates;
    public Dictionary<RanchDirector.PaletteType, RanchDirector.Palette> palettes;

    public override string Identifier => "SRRANCH";

    public override uint Version => 6;

    public RanchV06()
    {
    }

    public RanchV06(RanchV05 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      plots = LoadList<LandPlotV07>(reader);
      accessDoorStates = LoadDictionary(reader, r => r.ReadString(), r => (AccessDoor.State) r.ReadInt32());
      palettes = LoadDictionary(reader, r => (RanchDirector.PaletteType) r.ReadInt32(), r => (RanchDirector.Palette) r.ReadInt32());
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteList(writer, plots);
      WriteDictionary(writer, accessDoorStates, (w, k) => w.Write(k), (w, v) => w.Write((int) v));
      WriteDictionary(writer, palettes, (w, k) => w.Write((int) k), (w, v) => w.Write((int) v));
    }

    public static RanchV06 Load(BinaryReader reader)
    {
      RanchV06 ranchV06 = new RanchV06();
      ranchV06.Load(reader.BaseStream);
      return ranchV06;
    }

    protected override void UpgradeFrom(RanchV05 legacyData)
    {
      plots = new List<LandPlotV07>();
      foreach (LandPlotV04 plot in legacyData.plots)
        plots.Add(new LandPlotV07(new LandPlotV06(new LandPlotV05(plot))));
      accessDoorStates = legacyData.accessDoorStates;
      palettes = new Dictionary<RanchDirector.PaletteType, RanchDirector.Palette>();
    }

    public static void AssertAreEqual(RanchV06 expected, RanchV06 actual)
    {
      for (int index = 0; index < expected.plots.Count; ++index)
        LandPlotV07.AssertAreEqual(expected.plots[index], actual.plots[index]);
      TestUtil.AssertAreEqual(expected.accessDoorStates, actual.accessDoorStates, (e, a) => { }, "accessDoorStates");
      TestUtil.AssertAreEqual(expected.palettes, actual.palettes, (e, a) => { }, "palettes");
    }

    public static void AssertAreEqual(RanchV05 expected, RanchV06 actual)
    {
      for (int index = 0; index < expected.plots.Count; ++index)
        LandPlotV07.AssertAreEqual(new LandPlotV06(new LandPlotV05(expected.plots[index])), actual.plots[index]);
      TestUtil.AssertAreEqual(expected.accessDoorStates, actual.accessDoorStates, (e, a) => { }, "accessDoorStates");
    }
  }
}
