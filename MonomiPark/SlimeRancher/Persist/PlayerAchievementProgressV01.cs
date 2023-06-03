// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PlayerAchievementProgressV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PlayerAchievementProgressV01 : PersistedDataSet
  {
    public Dictionary<int, int> counts = new Dictionary<int, int>();
    public Dictionary<int, bool> events = new Dictionary<int, bool>();
    public Dictionary<int, List<Enum>> lists = new Dictionary<int, List<Enum>>();

    public override string Identifier => "SRPAP";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader)
    {
      ReadCounts(reader);
      ReadSectionSeparator(reader);
      ReadEvents(reader);
      ReadSectionSeparator(reader);
      ReadLists(reader);
    }

    private void ReadCounts(BinaryReader reader)
    {
      int num = reader.ReadInt32();
      for (; num > 0; --num)
      {
        ReadElementSeparator(reader);
        counts.Add(reader.ReadInt32(), reader.ReadInt32());
      }
    }

    private void ReadEvents(BinaryReader reader)
    {
      int num = reader.ReadInt32();
      for (; num > 0; --num)
      {
        ReadElementSeparator(reader);
        events.Add(reader.ReadInt32(), reader.ReadBoolean());
      }
    }

    private void ReadLists(BinaryReader reader)
    {
      int num1 = reader.ReadInt32();
      for (; num1 > 0; --num1)
      {
        ReadElementSeparator(reader);
        int key = reader.ReadInt32();
        int num2 = reader.ReadInt32();
        List<Enum> enumList = new List<Enum>();
        for (; num2 > 0; --num2)
          enumList.Add((TempEnum) reader.ReadInt32());
        lists.Add(key, enumList);
      }
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteCounts(writer);
      WriteSectionSeparator(writer);
      WriteEvents(writer);
      WriteSectionSeparator(writer);
      WriteLists(writer);
    }

    private void WriteCounts(BinaryWriter writer)
    {
      writer.Write(counts.Count);
      foreach (KeyValuePair<int, int> count in counts)
      {
        WriteElementSeparator(writer);
        writer.Write(count.Key);
        writer.Write(count.Value);
      }
    }

    private void WriteEvents(BinaryWriter writer)
    {
      writer.Write(events.Count);
      foreach (KeyValuePair<int, bool> keyValuePair in events)
      {
        WriteElementSeparator(writer);
        writer.Write(keyValuePair.Key);
        writer.Write(keyValuePair.Value);
      }
    }

    private void WriteLists(BinaryWriter writer)
    {
      writer.Write(lists.Count);
      foreach (KeyValuePair<int, List<Enum>> list in lists)
      {
        WriteElementSeparator(writer);
        writer.Write(list.Key);
        writer.Write(list.Value.Count);
        foreach (Enum @enum in list.Value)
          writer.Write(Convert.ToInt32(@enum));
      }
    }

    private enum TempEnum
    {
    }
  }
}
