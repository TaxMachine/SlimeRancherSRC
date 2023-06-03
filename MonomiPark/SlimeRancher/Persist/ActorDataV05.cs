// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ActorDataV05
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ActorDataV05 : VersionedPersistedDataSet<ActorDataV04>
  {
    public Vector3V02 pos;
    public Vector3V02 rot;
    public int id;
    public SlimeEmotionDataV02 emotions;
    public double transformTime;
    public double reproduceTime;
    public ResourceCycleDataV03 cycleData;
    public double? disabledAtTime;
    public bool isFeral;
    public List<Identifiable.Id> fashions = new List<Identifiable.Id>();

    public override string Identifier => "SRAD";

    public override uint Version => 5;

    public ActorDataV05()
    {
    }

    public ActorDataV05(ActorDataV04 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      pos = new Vector3V02();
      pos.Load(reader.BaseStream);
      rot = new Vector3V02();
      rot.Load(reader.BaseStream);
      id = reader.ReadInt32();
      emotions = new SlimeEmotionDataV02();
      emotions.Load(reader.BaseStream);
      transformTime = reader.ReadDouble();
      reproduceTime = reader.ReadDouble();
      cycleData = new ResourceCycleDataV03();
      cycleData.Load(reader.BaseStream);
      if (reader.ReadBoolean())
        disabledAtTime = new double?(reader.ReadDouble());
      isFeral = reader.ReadBoolean();
      fashions = LoadList(reader, (Func<int, Identifiable.Id>) (v => (Identifiable.Id) v));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      pos.Write(writer.BaseStream);
      rot.Write(writer.BaseStream);
      writer.Write(id);
      emotions.Write(writer.BaseStream);
      writer.Write(transformTime);
      writer.Write(reproduceTime);
      cycleData.Write(writer.BaseStream);
      if (disabledAtTime.HasValue)
      {
        writer.Write(true);
        writer.Write(disabledAtTime.Value);
      }
      else
        writer.Write(false);
      writer.Write(isFeral);
      WriteList(writer, fashions, v => (int) v);
    }

    public static void AssertAreEqual(ActorDataV05 expected, ActorDataV05 actual)
    {
      Vector3V02.AssertAreEqual(expected.pos, actual.pos);
      Vector3V02.AssertAreEqual(expected.rot, actual.rot);
      ResourceCycleDataV03.AssertAreEqual(expected.cycleData, actual.cycleData);
      SlimeEmotionDataV02.AssertAreEqual(expected.emotions, actual.emotions);
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
    }

    public static void AssertAreEqual(ActorDataV04 expected, ActorDataV05 actual)
    {
      Vector3V02.AssertAreEqual(expected.pos, actual.pos);
      Vector3V02.AssertAreEqual(expected.rot, actual.rot);
      ResourceCycleDataV03.AssertAreEqual(expected.cycleData, actual.cycleData);
      SlimeEmotionDataV02.AssertAreEqual(expected.emotions, actual.emotions);
      TestUtil.AssertAreEqual(new List<Identifiable.Id>(), actual.fashions, "fashions");
    }

    protected override void UpgradeFrom(ActorDataV04 legacyData)
    {
      pos = legacyData.pos;
      rot = legacyData.rot;
      id = legacyData.id;
      emotions = legacyData.emotions;
      transformTime = legacyData.transformTime;
      reproduceTime = legacyData.reproduceTime;
      cycleData = legacyData.cycleData;
      disabledAtTime = legacyData.disabledAtTime;
      isFeral = legacyData.isFeral;
      fashions = new List<Identifiable.Id>();
    }
  }
}
