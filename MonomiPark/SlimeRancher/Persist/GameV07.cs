// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GameV07
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GameV07 : GamePersistedDataSet<GameV05>
  {
    public string gameName;
    public string displayName;
    public GameSummaryV04 summary = new GameSummaryV04();
    public WorldV19 world = new WorldV19();
    public PlayerV13 player = new PlayerV13();
    public RanchV06 ranch = new RanchV06();
    public List<ActorDataV05> actors = new List<ActorDataV05>();
    public PediaV03 pedia = new PediaV03();
    public GameAchieveV03 achieve = new GameAchieveV03();
    public HolidayDirectorV01 holiday = new HolidayDirectorV01();

    public override string Identifier => "SRGAME";

    public override uint Version => 7;

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
      world = WorldV19.Load(reader);
      player = PlayerV13.Load(reader);
      ranch = RanchV06.Load(reader);
      ReadSectionSeparator(reader);
      actors = LoadList<ActorDataV05>(reader);
      ReadSectionSeparator(reader);
      pedia = PediaV03.Load(reader);
      achieve = GameAchieveV03.Load(reader);
      holiday = HolidayDirectorV01.Load(reader);
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
    }

    protected override void UpgradeFrom(GameV05 legacyData)
    {
      gameName = legacyData.gameName;
      displayName = legacyData.displayName;
      summary = legacyData.summary;
      world = legacyData.world;
      achieve = legacyData.achieve;
      player = legacyData.player;
      ranch = legacyData.ranch;
      actors = legacyData.actors;
      pedia = legacyData.pedia;
      holiday = new HolidayDirectorV01();
    }

    public static void AssertAreEqual(GameV07 expected, GameV07 actual)
    {
      GameSummaryV04.AssertAreEqual(expected.summary, actual.summary);
      WorldV19.AssertAreEqual(expected.world, actual.world);
      GameAchieveV03.AssertAreEqual(expected.achieve, actual.achieve);
      PediaV03.AssertAreEqual(expected.pedia, actual.pedia);
      PlayerV13.AssertAreEqual(expected.player, actual.player);
      RanchV06.AssertAreEqual(expected.ranch, actual.ranch);
      for (int index = 0; index < expected.actors.Count; ++index)
        ActorDataV05.AssertAreEqual(expected.actors[index], actual.actors[index]);
      HolidayDirectorV01.AssertAreEqual(expected.holiday, actual.holiday);
    }

    public static void AssertAreEqual(GameV05 expected, GameV07 actual)
    {
      GameSummaryV04.AssertAreEqual(expected.summary, actual.summary);
      WorldV19.AssertAreEqual(expected.world, actual.world);
      GameAchieveV03.AssertAreEqual(expected.achieve, actual.achieve);
      PediaV03.AssertAreEqual(expected.pedia, actual.pedia);
      PlayerV13.AssertAreEqual(expected.player, actual.player);
      RanchV06.AssertAreEqual(expected.ranch, actual.ranch);
      for (int index = 0; index < expected.actors.Count; ++index)
        ActorDataV05.AssertAreEqual(expected.actors[index], actual.actors[index]);
      HolidayDirectorV01.AssertAreEqual(new HolidayDirectorV01(), actual.holiday);
    }
  }
}
