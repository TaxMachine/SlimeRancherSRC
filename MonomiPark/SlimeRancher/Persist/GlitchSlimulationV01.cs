// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GlitchSlimulationV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GlitchSlimulationV01 : PersistedDataSet
  {
    public Dictionary<string, GlitchTeleportDestinationV01> teleporters;
    public Dictionary<string, GlitchTarrNodeV01> nodes;
    public Dictionary<string, GlitchImpostoDirectorV01> impostoDirectors;
    public Dictionary<string, GlitchImpostoV01> impostos;
    public Dictionary<long, GlitchSlimeDataV01> slimes;

    public override string Identifier => "SRGLITCH";

    public override uint Version => 1;

    public GlitchSlimulationV01()
    {
      teleporters = new Dictionary<string, GlitchTeleportDestinationV01>();
      nodes = new Dictionary<string, GlitchTarrNodeV01>();
      slimes = new Dictionary<long, GlitchSlimeDataV01>();
      impostoDirectors = new Dictionary<string, GlitchImpostoDirectorV01>();
      impostos = new Dictionary<string, GlitchImpostoV01>();
    }

    protected override void LoadData(BinaryReader reader)
    {
      teleporters = LoadDictionary(reader, r => r.ReadString(), r => LoadPersistable<GlitchTeleportDestinationV01>(r));
      nodes = LoadDictionary(reader, r => r.ReadString(), r => LoadPersistable<GlitchTarrNodeV01>(r));
      slimes = LoadDictionary(reader, r => r.ReadInt64(), r => LoadPersistable<GlitchSlimeDataV01>(r));
      impostoDirectors = LoadDictionary(reader, r => r.ReadString(), r => LoadPersistable<GlitchImpostoDirectorV01>(r));
      impostos = LoadDictionary(reader, r => r.ReadString(), r => LoadPersistable<GlitchImpostoV01>(r));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteDictionary(writer, teleporters, (w, key) => w.Write(key), (w, val) => WritePersistable(w, val));
      WriteDictionary(writer, nodes, (w, key) => w.Write(key), (w, val) => WritePersistable(w, val));
      WriteDictionary(writer, slimes, (w, key) => w.Write(key), (w, val) => WritePersistable(w, val));
      WriteDictionary(writer, impostoDirectors, (w, key) => w.Write(key), (w, val) => WritePersistable(w, val));
      WriteDictionary(writer, impostos, (w, key) => w.Write(key), (w, val) => WritePersistable(w, val));
    }

    public static void AssertAreEqual(GlitchSlimulationV01 expected, GlitchSlimulationV01 actual)
    {
      if (!TestUtil.AssertNullness(expected, actual))
        return;
      TestUtil.AssertAreEqual(expected.teleporters, actual.teleporters, (a, b) => GlitchTeleportDestinationV01.AssertAreEqual(a, b), "teleporters");
      TestUtil.AssertAreEqual(expected.nodes, actual.nodes, (a, b) => GlitchTarrNodeV01.AssertAreEqual(a, b), "nodes");
      TestUtil.AssertAreEqual(expected.slimes, actual.slimes, (a, b) => GlitchSlimeDataV01.AssertAreEqual(a, b), "slimes");
      TestUtil.AssertAreEqual(expected.impostoDirectors, actual.impostoDirectors, (a, b) => GlitchImpostoDirectorV01.AssertAreEqual(a, b), "impostoDirectors");
      TestUtil.AssertAreEqual(expected.impostos, actual.impostos, (a, b) => GlitchImpostoV01.AssertAreEqual(a, b), "impostos");
    }
  }
}
