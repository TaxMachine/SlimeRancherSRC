// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GameV09
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GameV09 : GamePersistedDataSet<GameV08>
  {
    public string gameName;
    public string displayName;
    public GameSummaryV04 summary = new GameSummaryV04();
    public WorldV22 world = new WorldV22();
    public PlayerV13 player = new PlayerV13();
    public RanchV07 ranch = new RanchV07();
    public List<ActorDataV08> actors = new List<ActorDataV08>();
    public PediaV03 pedia = new PediaV03();
    public GameAchieveV03 achieve = new GameAchieveV03();
    public HolidayDirectorV02 holiday = new HolidayDirectorV02();
    public DLCV01 dlc = new DLCV01();

    public override string Identifier => "SRGAME";

    public override uint Version => 9;

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
      player = PlayerV13.Load(reader);
      ranch = RanchV07.Load(reader);
      ReadSectionSeparator(reader);
      actors = LoadList<ActorDataV08>(reader);
      ReadSectionSeparator(reader);
      pedia = PediaV03.Load(reader);
      achieve = GameAchieveV03.Load(reader);
      holiday = HolidayDirectorV02.Load(reader);
      dlc = DLCV01.Load(reader);
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
      dlc.Write(writer.BaseStream);
    }

    protected override void UpgradeFrom(GameV08 legacyData)
    {
      gameName = legacyData.gameName;
      displayName = legacyData.displayName;
      summary = legacyData.summary;
      world = new WorldV22(new WorldV21(new WorldV20(legacyData.world)));
      achieve = legacyData.achieve;
      player = legacyData.player;
      ranch = legacyData.ranch;
      actors = UpgradeFrom(legacyData.actors, world.worldTime);
      pedia = legacyData.pedia;
      holiday = new HolidayDirectorV02(legacyData.holiday);
      dlc = legacyData.dlc;
    }

    private static List<ActorDataV08> UpgradeFrom(List<ActorDataV05> legacyData, double worldTime)
    {
      int num = 1;
      List<ActorDataV08> actorDataV08List = new List<ActorDataV08>();
      foreach (ActorDataV05 legacyData1 in legacyData)
      {
        ActorDataV08 actorDataV08 = new ActorDataV08(new ActorDataV07(new ActorDataV06(legacyData1)));
        actorDataV08.actorId = num++;
        if (actorDataV08.typeId == 7)
          actorDataV08.destroyTime = worldTime + 21600.0;
        else if (Identifiable.IsPlort((Identifiable.Id) actorDataV08.typeId))
          actorDataV08.destroyTime = worldTime + 86400.0;
        actorDataV08List.Add(actorDataV08);
      }
      return actorDataV08List;
    }

    public static void AssertAreEqual(GameV09 expected, GameV09 actual, bool allowActorMovement = false)
    {
      GameSummaryV04.AssertAreEqual(expected.summary, actual.summary);
      WorldV22.AssertAreEqual(expected.world, actual.world);
      GameAchieveV03.AssertAreEqual(expected.achieve, actual.achieve);
      PediaV03.AssertAreEqual(expected.pedia, actual.pedia);
      PlayerV13.AssertAreEqual(expected.player, actual.player);
      RanchV07.AssertAreEqual(expected.ranch, actual.ranch);
      for (int index = 0; index < expected.actors.Count; ++index)
        ActorDataV08.AssertAreEqual(expected.actors[index], actual.actors[index], allowActorMovement);
      HolidayDirectorV02.AssertAreEqual(expected.holiday, actual.holiday);
      DLCV01.AssertAreEqual(expected.dlc, actual.dlc);
    }

    public static void AssertAreEqual(GameV08 expected, GameV09 actual)
    {
      GameSummaryV04.AssertAreEqual(expected.summary, actual.summary);
      WorldV22.AssertAreEqual(new WorldV21(new WorldV20(expected.world)), actual.world);
      GameAchieveV03.AssertAreEqual(expected.achieve, actual.achieve);
      PediaV03.AssertAreEqual(expected.pedia, actual.pedia);
      PlayerV13.AssertAreEqual(expected.player, actual.player);
      RanchV07.AssertAreEqual(expected.ranch, actual.ranch);
      TestUtil.AssertAreEqual(UpgradeFrom(expected.actors, expected.world.worldTime), actual.actors, (e, a, r) => ActorDataV08.AssertAreEqual(e, a), "actors");
      HolidayDirectorV02.AssertAreEqual(expected.holiday, actual.holiday);
      DLCV01.AssertAreEqual(expected.dlc, actual.dlc);
    }

    private class Placeholder : PersistedDataSet
    {
      public override string Identifier => "SRDLC";

      public override uint Version => 1;

      protected override void LoadData(BinaryReader reader) => LoadList(reader, (Func<int, Enum>) (i => Enum.NONE));

      protected override void WriteData(BinaryWriter writer) => WriteList(writer, new List<Enum>(), i => 0);

      public static Placeholder Load(BinaryReader reader)
      {
        Placeholder placeholder = new Placeholder();
        placeholder.Load(reader.BaseStream);
        return placeholder;
      }

      public enum Enum
      {
        NONE,
      }
    }
  }
}
