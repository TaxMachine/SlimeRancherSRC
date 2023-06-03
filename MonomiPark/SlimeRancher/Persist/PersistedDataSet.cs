// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PersistedDataSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MonomiPark.SlimeRancher.Persist
{
  public abstract class PersistedDataSet : Persistable
  {
    protected const short SECTION_SEPARATOR = 4096;
    protected const short ELEMENT_SEPARATOR = 8192;
    protected const short DATA_PAYLOAD_BEGIN = 12288;
    protected const short DATA_PAYLOAD_END = 16384;

    public virtual string Identifier => "";

    public virtual uint Version => 0;

    public void Load(Stream stream) => Load(stream, false);

    public virtual void Load(Stream stream, bool skipPayloadEnd)
    {
      BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
      if (IsValidHeader(reader))
      {
        ReadDataPayloadBegin(reader);
        LoadData(reader);
        if (skipPayloadEnd)
          return;
        ReadDataPayloadEnd(reader);
      }
      else
        throw new InvalidDataException(Log.Format("Failed to load dataset. Invalid header", "Expected Id", Identifier, "Expected Version", Version));
    }

    protected virtual bool IsValidHeader(BinaryReader reader) => Identifier.CompareTo(reader.ReadString()) == 0 && (int) reader.ReadUInt32() == (int) Version;

    protected abstract void LoadData(BinaryReader reader);

    public long Write(Stream stream)
    {
      long position = stream.Position;
      BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
      WriteHeader(writer);
      WriteDataPayloadBegin(writer);
      WriteData(writer);
      WriteDataPayloadEnd(writer);
      return stream.Position - position;
    }

    protected void WriteHeader(BinaryWriter writer)
    {
      writer.Write(Identifier);
      writer.Write(Version);
    }

    protected abstract void WriteData(BinaryWriter writer);

    protected void ReadSectionSeparator(BinaryReader reader)
    {
      if (reader.ReadInt16() != 4096)
        throw new Exception("Unexpected data when reading section separator.");
    }

    protected void ReadElementSeparator(BinaryReader reader)
    {
      if (reader.ReadInt16() != 8192)
        throw new Exception("Unexpected data when reading element separator.");
    }

    protected void ReadDataPayloadBegin(BinaryReader reader)
    {
      if (reader.ReadInt16() != 12288)
        throw new Exception("Unexpected data when reading beginning of data payload.");
    }

    protected void ReadDataPayloadEnd(BinaryReader reader)
    {
      if (reader.ReadInt16() != 16384)
        throw new Exception("Unexpected data when reading end of data payload.");
    }

    protected void WriteDataPayloadBegin(BinaryWriter writer) => writer.Write((short) 12288);

    protected void WriteDataPayloadEnd(BinaryWriter writer) => writer.Write((short) 16384);

    protected void WriteElementSeparator(BinaryWriter writer) => writer.Write((short) 8192);

    protected void WriteSectionSeparator(BinaryWriter writer) => writer.Write((short) 4096);

    protected static List<T> LoadList<T>(BinaryReader reader) where T : Persistable, new() => LoadList(reader, r =>
    {
      T obj = new T();
      obj.Load(r.BaseStream);
      return obj;
    });

    protected static List<T> LoadList<T>(BinaryReader reader, Func<int, T> transform) => LoadList(reader, r => transform(r.ReadInt32()));

    protected static List<T> LoadList<T>(BinaryReader reader, Func<float, T> transform) => LoadList(reader, r => transform(r.ReadSingle()));

    protected static List<T> LoadList<T>(BinaryReader reader, Func<double, T> transform) => LoadList(reader, r => transform(r.ReadDouble()));

    protected static List<T> LoadList<T>(BinaryReader reader, Func<string, T> transform) => LoadList(reader, r => transform(r.ReadString()));

    protected static List<T> LoadList<T>(BinaryReader reader, Func<BinaryReader, T> readFunction)
    {
      List<T> objList = new List<T>();
      for (int index = reader.ReadInt32(); index > 0; --index)
        objList.Add(readFunction(reader));
      return objList;
    }

    protected Dictionary<K, V> LoadDictionary<K, V>(
      BinaryReader reader,
      Func<BinaryReader, K> keyLoad,
      Func<BinaryReader, V> valueLoad)
      where V : new()
    {
      Dictionary<K, V> dictionary = new Dictionary<K, V>();
      for (int index = reader.ReadInt32(); index > 0; --index)
      {
        K key = keyLoad(reader);
        V v = valueLoad(reader);
        ReadElementSeparator(reader);
        dictionary.Add(key, v);
      }
      return dictionary;
    }

    protected static void WriteList<T>(BinaryWriter writer, List<T> items) where T : Persistable => WriteList(writer, items, (Action<T>) (it => it.Write(writer.BaseStream)));

    protected static void WriteList<T>(BinaryWriter writer, List<T> items, Func<T, int> transform) => WriteList(writer, items, it => writer.Write(transform(it)));

    protected static void WriteList<T>(
      BinaryWriter writer,
      List<T> items,
      Func<T, float> transform)
    {
      WriteList(writer, items, it => writer.Write(transform(it)));
    }

    protected static void WriteList<T>(
      BinaryWriter writer,
      List<T> items,
      Func<T, double> transform)
    {
      WriteList(writer, items, it => writer.Write(transform(it)));
    }

    protected static void WriteList<T>(
      BinaryWriter writer,
      List<T> items,
      Func<T, string> transform)
    {
      WriteList(writer, items, it => writer.Write(transform(it)));
    }

    protected static void WriteList<T>(BinaryWriter writer, List<T> items, Action<T> writeFunction)
    {
      writer.Write(items.Count);
      foreach (T obj in items)
        writeFunction(obj);
    }

    protected void WriteDictionary<K, V>(
      BinaryWriter writer,
      Dictionary<K, V> dict,
      Action<BinaryWriter, K> keyAction,
      Action<BinaryWriter, V> valueAction)
    {
      writer.Write(dict.Count);
      foreach (KeyValuePair<K, V> keyValuePair in dict)
      {
        keyAction(writer, keyValuePair.Key);
        valueAction(writer, keyValuePair.Value);
        WriteElementSeparator(writer);
      }
    }

    protected void WriteNullable(BinaryWriter writer, double? nullable)
    {
      writer.Write(nullable.HasValue);
      if (!nullable.HasValue)
        return;
      writer.Write(nullable.Value);
    }

    protected void WriteNullable(BinaryWriter writer, float? nullable)
    {
      writer.Write(nullable.HasValue);
      if (!nullable.HasValue)
        return;
      writer.Write(nullable.Value);
    }

    protected void LoadNullable(BinaryReader reader, out double? nullable)
    {
      if (reader.ReadBoolean())
        nullable = new double?(reader.ReadDouble());
      else
        nullable = new double?();
    }

    protected void LoadNullable(BinaryReader reader, out float? nullable)
    {
      if (reader.ReadBoolean())
        nullable = new float?(reader.ReadSingle());
      else
        nullable = new float?();
    }

    protected static T LoadPersistable<T>(BinaryReader reader) where T : class, Persistable, new()
    {
      if (!reader.ReadBoolean())
        return default (T);
      T obj = new T();
      obj.Load(reader.BaseStream);
      return obj;
    }

    protected static void WritePersistable<T>(BinaryWriter writer, T instance) where T : class, Persistable
    {
      writer.Write(instance != null);
      if (instance == null)
        return;
      instance.Write(writer.BaseStream);
    }

    protected static Dictionary<string, T> UpgradeFrom<T>(
      Dictionary<Vector3V02, T> legacy,
      Dictionary<Vector3, string> mapping)
    {
      if (legacy == null)
        return null;
      Dictionary<string, T> dictionary = new Dictionary<string, T>();
      foreach (KeyValuePair<Vector3V02, T> keyValuePair in legacy)
      {
        KeyValuePair<Vector3V02, T> pair = keyValuePair;
        try
        {
          dictionary[mapping.First(mp => (pair.Key.value - mp.Key).sqrMagnitude < 100.0).Value] = pair.Value;
        }
        catch (InvalidOperationException ex)
        {
          Log.Warning("Failed to get id from position.", "position", pair.Key.value, "type", typeof (T));
        }
      }
      return dictionary;
    }
  }
}
