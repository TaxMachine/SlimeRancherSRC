// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.HolidayDirectorV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class HolidayDirectorV02 : VersionedPersistedDataSet<HolidayDirectorV01>
  {
    public List<string> eventGordos;
    public List<string> eventEchoNoteGordos;

    public override string Identifier => "SRHD";

    public override uint Version => 2;

    public static HolidayDirectorV02 Load(BinaryReader reader)
    {
      HolidayDirectorV02 holidayDirectorV02 = new HolidayDirectorV02();
      holidayDirectorV02.Load(reader.BaseStream);
      return holidayDirectorV02;
    }

    public HolidayDirectorV02()
    {
      eventGordos = new List<string>();
      eventEchoNoteGordos = new List<string>();
    }

    public HolidayDirectorV02(HolidayDirectorV01 legacy) => UpgradeFrom(legacy);

    protected override void LoadData(BinaryReader reader)
    {
      eventGordos = LoadList(reader, (Func<string, string>) (s => s));
      eventEchoNoteGordos = LoadList(reader, (Func<string, string>) (s => s));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteList(writer, eventGordos, s => s);
      WriteList(writer, eventEchoNoteGordos, s => s);
    }

    protected override void UpgradeFrom(HolidayDirectorV01 legacy)
    {
      eventGordos = legacy.eventGordos;
      eventEchoNoteGordos = new List<string>();
    }

    public static void AssertAreEqual(HolidayDirectorV02 expected, HolidayDirectorV02 actual)
    {
      TestUtil.AssertAreEqual(expected.eventGordos, actual.eventGordos, "eventGordos");
      TestUtil.AssertAreEqual(expected.eventEchoNoteGordos, actual.eventEchoNoteGordos, "eventEchoNoteGordos");
    }

    public static void AssertAreEqual(HolidayDirectorV01 expected, HolidayDirectorV02 actual) => TestUtil.AssertAreEqual(expected.eventGordos, actual.eventGordos, "eventGordos");
  }
}
