// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DLCV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DLCV01 : PersistedDataSet
  {
    public List<Enum> activated = new List<Enum>();

    public override string Identifier => "SRDLC";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader) => activated = LoadList(reader, (Func<int, Enum>) (id => (Enum) id));

    protected override void WriteData(BinaryWriter writer) => WriteList(writer, activated, id => (int) id);

    public static DLCV01 Load(BinaryReader reader)
    {
      DLCV01 dlcV01 = new DLCV01();
      dlcV01.Load(reader.BaseStream);
      return dlcV01;
    }

    public static void AssertAreEqual(DLCV01 expected, DLCV01 actual) => TestUtil.AssertAreEqual(expected.activated, actual.activated, "activated");

    public enum Enum
    {
    }
  }
}
