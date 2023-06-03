// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ActorDataV09
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ActorDataV09 : VersionedPersistedDataSet<ActorDataV08>
  {
    public long actorId;
    public Vector3V02 pos;
    public Vector3V02 rot;
    public int typeId;
    public SlimeEmotionDataV02 emotions;
    public double transformTime;
    public double reproduceTime;
    public double destroyTime;
    public ResourceCycleDataV03 cycleData;
    public double? disabledAtTime;
    public bool isFeral;
    public List<Identifiable.Id> fashions = new List<Identifiable.Id>();
    public bool isGlitch;
    public RegionRegistry.RegionSetId regionSetId;
    private const float ACTOR_MOVE_TOLERANCE = 0.01f;

    public override string Identifier => "SRAD";

    public override uint Version => 9;

    public ActorDataV09()
    {
    }

    public ActorDataV09(ActorDataV08 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      pos = new Vector3V02();
      pos.Load(reader.BaseStream);
      rot = new Vector3V02();
      rot.Load(reader.BaseStream);
      actorId = reader.ReadInt64();
      typeId = reader.ReadInt32();
      emotions = new SlimeEmotionDataV02();
      emotions.Load(reader.BaseStream);
      transformTime = reader.ReadDouble();
      reproduceTime = reader.ReadDouble();
      destroyTime = reader.ReadDouble();
      cycleData = new ResourceCycleDataV03();
      cycleData.Load(reader.BaseStream);
      LoadNullable(reader, out disabledAtTime);
      isFeral = reader.ReadBoolean();
      fashions = LoadList(reader, (Func<int, Identifiable.Id>) (v => (Identifiable.Id) v));
      isGlitch = reader.ReadBoolean();
      regionSetId = (RegionRegistry.RegionSetId) reader.ReadInt32();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      pos.Write(writer.BaseStream);
      rot.Write(writer.BaseStream);
      writer.Write(actorId);
      writer.Write(typeId);
      emotions.Write(writer.BaseStream);
      writer.Write(transformTime);
      writer.Write(reproduceTime);
      writer.Write(destroyTime);
      cycleData.Write(writer.BaseStream);
      WriteNullable(writer, disabledAtTime);
      writer.Write(isFeral);
      WriteList(writer, fashions, v => (int) v);
      writer.Write(isGlitch);
      writer.Write((int) regionSetId);
    }

    public static void AssertAreEqual(
      ActorDataV09 expected,
      ActorDataV09 actual,
      bool allowActorMovement = false)
    {
      if (allowActorMovement)
        Vector3V02.AssertAreApproximatelyEqual(expected.pos, actual.pos, 0.01f);
      else
        Vector3V02.AssertAreEqual(expected.pos, actual.pos);
      Vector3V02.AssertAreApproximatelyEqual(expected.rot, actual.rot, 0.1f);
      ResourceCycleDataV03.AssertAreEqual(expected.cycleData, actual.cycleData);
      SlimeEmotionDataV02.AssertAreEqual(expected.emotions, actual.emotions);
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
    }

    public static void AssertAreEqual(ActorDataV08 expected, ActorDataV09 actual)
    {
      Vector3V02.AssertAreEqual(expected.pos, actual.pos);
      Vector3V02.AssertAreEqual(expected.rot, actual.rot);
      ResourceCycleDataV03.AssertAreEqual(expected.cycleData, actual.cycleData);
      SlimeEmotionDataV02.AssertAreEqual(expected.emotions, actual.emotions);
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
    }

    protected override void UpgradeFrom(ActorDataV08 legacyData)
    {
      pos = legacyData.pos;
      rot = legacyData.rot;
      typeId = legacyData.typeId;
      emotions = legacyData.emotions;
      transformTime = legacyData.transformTime;
      reproduceTime = legacyData.reproduceTime;
      cycleData = legacyData.cycleData;
      disabledAtTime = legacyData.disabledAtTime;
      isFeral = legacyData.isFeral;
      fashions = legacyData.fashions;
      actorId = legacyData.actorId;
      destroyTime = legacyData.destroyTime;
      isGlitch = legacyData.isGlitch;
      regionSetId = GetRegionSetForPos(pos);
    }

    private static RegionRegistry.RegionSetId GetRegionSetForPos(Vector3V02 pos)
    {
      if (pos.value.y > 900.0)
        return RegionRegistry.RegionSetId.DESERT;
      return pos.value.z < -550.0 ? RegionRegistry.RegionSetId.VALLEY : RegionRegistry.RegionSetId.HOME;
    }
  }
}
