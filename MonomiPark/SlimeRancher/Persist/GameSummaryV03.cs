// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GameSummaryV03
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GameSummaryV03 : VersionedPersistedDataSet<GameSummaryV02>
  {
    public Identifiable.Id iconId;
    public PlayerState.GameMode gameMode;
    public string version;
    public int currency;
    public int pediaCount;
    public double worldTime;
    public bool isGameOver;
    public DateTimeOffset saveTimestamp;

    public override string Identifier => "SRGSUMM";

    public override uint Version => 3;

    protected override void LoadData(BinaryReader reader)
    {
      version = reader.ReadString();
      gameMode = (PlayerState.GameMode) reader.ReadInt32();
      iconId = (Identifiable.Id) reader.ReadInt32();
      currency = reader.ReadInt32();
      pediaCount = reader.ReadInt32();
      worldTime = reader.ReadDouble();
      saveTimestamp = ReadDateTimeOffset(reader);
      isGameOver = reader.ReadBoolean();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(version);
      writer.Write((int) gameMode);
      writer.Write((int) iconId);
      writer.Write(currency);
      writer.Write(pediaCount);
      writer.Write(worldTime);
      WriteDateTimeOffset(writer, saveTimestamp);
      writer.Write(isGameOver);
    }

    private DateTimeOffset ReadDateTimeOffset(BinaryReader reader) => new DateTimeOffset(reader.ReadInt64(), TimeSpan.FromMinutes(reader.ReadDouble()));

    private void WriteDateTimeOffset(BinaryWriter writer, DateTimeOffset dateTimeOffset)
    {
      writer.Write(dateTimeOffset.Ticks);
      writer.Write(dateTimeOffset.Offset.TotalMinutes);
    }

    public static GameSummaryV03 Load(BinaryReader reader)
    {
      GameSummaryV03 gameSummaryV03 = new GameSummaryV03();
      gameSummaryV03.Load(reader.BaseStream);
      return gameSummaryV03;
    }

    public static void AssertAreEqual(GameSummaryV03 expected, GameSummaryV03 actual)
    {
    }

    public static void AssertAreEqual(GameSummaryV02 expected, GameSummaryV03 actual)
    {
    }

    public static void AssertAreEqual(GameSummaryV01 expected, GameSummaryV03 actual)
    {
    }

    protected override void UpgradeFrom(GameSummaryV02 legacyData)
    {
      version = legacyData.version;
      gameMode = legacyData.gameMode;
      iconId = legacyData.iconId;
      currency = legacyData.currency;
      pediaCount = legacyData.pediaCount;
      worldTime = legacyData.worldTime;
      saveTimestamp = legacyData.saveTimestamp;
      isGameOver = legacyData.gameMode == PlayerState.GameMode.TIME_LIMIT && legacyData.worldTime > 475200.0;
    }
  }
}
