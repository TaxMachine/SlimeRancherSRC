// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.HolidayDirectorV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class HolidayDirectorV01 : PersistedDataSet
  {
    public List<string> eventGordos = new List<string>();

    public override string Identifier => "SRHD";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader) => eventGordos = LoadList(reader, (Func<string, string>) (s => s));

    protected override void WriteData(BinaryWriter writer) => WriteList(writer, eventGordos, s => s);

    public static HolidayDirectorV01 Load(BinaryReader reader)
    {
      HolidayDirectorV01 holidayDirectorV01 = new HolidayDirectorV01();
      holidayDirectorV01.Load(reader.BaseStream);
      return holidayDirectorV01;
    }

    public static void AssertAreEqual(HolidayDirectorV01 expected, HolidayDirectorV01 actual) => TestUtil.AssertAreEqual(expected.eventGordos, actual.eventGordos, "eventGordos");
  }
}
