// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GameV12
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GameV12 : GamePersistedDataSet<GameV11>
  {
    public string gameName;
    public string displayName;
    public GameSummaryV04 summary = new GameSummaryV04();
    public WorldV22 world = new WorldV22();
    public PlayerV14 player = new PlayerV14();
    public RanchV07 ranch = new RanchV07();
    public List<ActorDataV09> actors = new List<ActorDataV09>();
    public PediaV03 pedia = new PediaV03();
    public GameAchieveV03 achieve = new GameAchieveV03();
    public HolidayDirectorV02 holiday = new HolidayDirectorV02();
    public AppearancesV01 appearances = new AppearancesV01();
    public InstrumentV01 instrument = new InstrumentV01();

    public override string Identifier => "SRGAME";

    public override uint Version => 12;

    protected override void LoadSummaryData(BinaryReader reader)
    {
      gameName = reader.ReadString();
      displayName = reader.ReadString();
      summary = GameSummaryV04.Load(reader);
    }

    protected override void WriteSummaryData(BinaryWriter writer)
    {
      writer.Write(gameName);
      writer.Write(displayName);
      summary.Write(writer.BaseStream);
    }

    protected override void LoadGameData(BinaryReader reader)
    {
      world = WorldV22.Load(reader);
      player = PlayerV14.Load(reader);
      ranch = RanchV07.Load(reader);
      ReadSectionSeparator(reader);
      actors = LoadList<ActorDataV09>(reader);
      ReadSectionSeparator(reader);
      pedia = PediaV03.Load(reader);
      achieve = GameAchieveV03.Load(reader);
      holiday = HolidayDirectorV02.Load(reader);
      appearances = AppearancesV01.Load(reader);
      instrument = InstrumentV01.Load(reader);
    }

    protected override void WriteGameData(BinaryWriter writer)
    {
      world.Write(writer.BaseStream);
      player.Write(writer.BaseStream);
      ranch.Write(writer.BaseStream);
      WriteSectionSeparator(writer);
      WriteList(writer, actors);
      WriteSectionSeparator(writer);
      pedia.Write(writer.BaseStream);
      achieve.Write(writer.BaseStream);
      holiday.Write(writer.BaseStream);
      appearances.Write(writer.BaseStream);
      instrument.Write(writer.BaseStream);
    }

    protected override void UpgradeFrom(GameV11 legacyData)
    {
      gameName = legacyData.gameName;
      displayName = legacyData.displayName;
      summary = legacyData.summary;
      world = legacyData.world;
      achieve = legacyData.achieve;
      ranch = legacyData.ranch;
      pedia = legacyData.pedia;
      holiday = legacyData.holiday;
      actors = legacyData.actors;
      player = legacyData.player;
      appearances = legacyData.appearances;
      instrument = new InstrumentV01();
    }

    public static void AssertAreEqual(GameV12 expected, GameV12 actual, bool allowActorMovement = false)
    {
      GameSummaryV04.AssertAreEqual(expected.summary, actual.summary);
      WorldV22.AssertAreEqual(expected.world, actual.world);
      GameAchieveV03.AssertAreEqual(expected.achieve, actual.achieve);
      PediaV03.AssertAreEqual(expected.pedia, actual.pedia);
      PlayerV14.AssertAreEqual(expected.player, actual.player);
      RanchV07.AssertAreEqual(expected.ranch, actual.ranch);
      for (int index = 0; index < expected.actors.Count; ++index)
        ActorDataV09.AssertAreEqual(expected.actors[index], actual.actors[index], allowActorMovement);
      HolidayDirectorV02.AssertAreEqual(expected.holiday, actual.holiday);
      AppearancesV01.AssertAreEqual(expected.appearances, actual.appearances);
      InstrumentV01.AssertAreEqual(expected.instrument, actual.instrument);
    }

    public static void AssertAreEqual(GameV11 expected, GameV12 actual)
    {
      GameSummaryV04.AssertAreEqual(expected.summary, actual.summary);
      WorldV22.AssertAreEqual(expected.world, actual.world);
      GameAchieveV03.AssertAreEqual(expected.achieve, actual.achieve);
      PediaV03.AssertAreEqual(expected.pedia, actual.pedia);
      PlayerV14.AssertAreEqual(expected.player, actual.player);
      RanchV07.AssertAreEqual(expected.ranch, actual.ranch);
      TestUtil.AssertAreEqual(expected.actors, actual.actors, (e, a, r) => ActorDataV09.AssertAreEqual(e, a), "actors");
      HolidayDirectorV02.AssertAreEqual(expected.holiday, actual.holiday);
      AppearancesV01.AssertAreEqual(expected.appearances, actual.appearances);
      InstrumentV01.AssertAreEqual(new InstrumentV01(), actual.instrument);
    }
  }
}
