// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DecorizerV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DecorizerV01 : PersistedDataSet
  {
    public Dictionary<Identifiable.Id, int> contents;
    public Dictionary<string, DecorizerSettingsV01> settings;

    public override string Identifier => "SRDZR";

    public override uint Version => 1;

    public static DecorizerV01 FromLegacy() => new DecorizerV01()
    {
      contents = new Dictionary<Identifiable.Id, int>(),
      settings = new Dictionary<string, DecorizerSettingsV01>()
    };

    protected override void LoadData(BinaryReader reader)
    {
      contents = LoadDictionary(reader, r => (Identifiable.Id) r.ReadInt32(), r => r.ReadInt32());
      settings = LoadDictionary(reader, r => r.ReadString(), r => LoadPersistable<DecorizerSettingsV01>(r));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteDictionary(writer, contents, (w, v) => w.Write((int) v), (w, v) => w.Write(v));
      WriteDictionary(writer, settings, (w, v) => w.Write(v), WritePersistable);
    }

    public static void AssertAreEqual(DecorizerV01 expected, DecorizerV01 actual)
    {
      TestUtil.AssertAreEqual(expected.contents, actual.contents, (a, b) => { }, "contents");
      TestUtil.AssertAreEqual(expected.settings, actual.settings, DecorizerSettingsV01.AssertAreEqual, "settings");
    }
  }
}
