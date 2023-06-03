// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GameSummaryV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GameSummaryV02 : VersionedPersistedDataSet<GameSummaryV01>
  {
    public Identifiable.Id iconId;
    public PlayerState.GameMode gameMode;
    public string version;
    public int currency;
    public int pediaCount;
    public double worldTime;
    public DateTimeOffset saveTimestamp;

    public override string Identifier => "SRGSUMM";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      version = reader.ReadString();
      gameMode = (PlayerState.GameMode) reader.ReadInt32();
      iconId = (Identifiable.Id) reader.ReadInt32();
      currency = reader.ReadInt32();
      pediaCount = reader.ReadInt32();
      worldTime = reader.ReadDouble();
      saveTimestamp = ReadDateTimeOffset(reader);
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
    }

    private DateTimeOffset ReadDateTimeOffset(BinaryReader reader) => new DateTimeOffset(reader.ReadInt64(), TimeSpan.FromMinutes(reader.ReadDouble()));

    private void WriteDateTimeOffset(BinaryWriter writer, DateTimeOffset dateTimeOffset)
    {
      writer.Write(dateTimeOffset.Ticks);
      writer.Write(dateTimeOffset.Offset.TotalMinutes);
    }

    public static GameSummaryV02 Load(BinaryReader reader)
    {
      GameSummaryV02 gameSummaryV02 = new GameSummaryV02();
      gameSummaryV02.Load(reader.BaseStream);
      return gameSummaryV02;
    }

    public static void AssertAreEqual(GameSummaryV02 expected, GameSummaryV02 actual)
    {
    }

    public static void AssertAreEqual(GameSummaryV01 expected, GameSummaryV02 actual)
    {
    }

    protected override void UpgradeFrom(GameSummaryV01 legacyData)
    {
      version = legacyData.version;
      gameMode = legacyData.gameMode;
      iconId = legacyData.iconId;
      currency = legacyData.currency;
      pediaCount = legacyData.pediaCount;
      worldTime = legacyData.worldTime;
      saveTimestamp = DateTimeOffset.MinValue;
    }
  }
}
