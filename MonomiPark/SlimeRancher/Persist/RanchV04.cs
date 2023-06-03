// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.RanchV04
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class RanchV04 : PersistedDataSet
  {
    public List<LandPlotV03> plots;
    public Dictionary<Vector3V02, AccessDoor.State> accessDoorStates;

    public override string Identifier => "SRRANCH";

    public override uint Version => 4;

    protected override void LoadData(BinaryReader reader)
    {
      plots = LoadList<LandPlotV03>(reader);
      accessDoorStates = LoadDictionary(reader, r => Vector3V02.Load(r), r => (AccessDoor.State) r.ReadInt32());
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteList(writer, plots);
      WriteDictionary(writer, accessDoorStates, (w, k) => k.Write(w.BaseStream), (w, v) => w.Write((int) v));
    }

    public static RanchV04 Load(BinaryReader reader)
    {
      RanchV04 ranchV04 = new RanchV04();
      ranchV04.Load(reader.BaseStream);
      return ranchV04;
    }

    public static void AssertAreEqual(RanchV04 expected, RanchV04 actual)
    {
      for (int index = 0; index < expected.plots.Count; ++index)
        LandPlotV03.AssertAreEqual(expected.plots[index], actual.plots[index]);
      TestUtil.AssertAreEqual(expected.accessDoorStates, actual.accessDoorStates, (e, a) => { }, "accessDoorStates");
    }
  }
}
