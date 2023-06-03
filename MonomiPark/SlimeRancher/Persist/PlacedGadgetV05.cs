// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PlacedGadgetV05
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PlacedGadgetV05 : VersionedPersistedDataSet<PlacedGadgetV04>
  {
    public Gadget.Id gadgetId;
    public float yRotation;
    public bool isPrimaryInLink;
    public List<AmmoDataV02> ammo = new List<AmmoDataV02>();
    public int extractorCyclesRemaining;
    public int extractorQueuedToProduce;
    public double extractorCycleEndTime;
    public double extractorNextProduceTime;
    public double waitForChargeupTime;
    public double lastSpawnTime;
    public Identifiable.Id baitTypeId;
    public Identifiable.Id gordoTypeId;
    public int gordoEatenCount;
    public List<Identifiable.Id> fashions = new List<Identifiable.Id>();
    public DroneV04 drone;

    public override string Identifier => "SRPG";

    public override uint Version => 5;

    public PlacedGadgetV05()
    {
    }

    public PlacedGadgetV05(PlacedGadgetV04 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      gadgetId = (Gadget.Id) reader.ReadInt32();
      yRotation = reader.ReadSingle();
      isPrimaryInLink = reader.ReadBoolean();
      ammo = LoadList<AmmoDataV02>(reader);
      extractorCyclesRemaining = reader.ReadInt32();
      extractorQueuedToProduce = reader.ReadInt32();
      extractorCycleEndTime = reader.ReadDouble();
      extractorNextProduceTime = reader.ReadDouble();
      waitForChargeupTime = reader.ReadDouble();
      lastSpawnTime = reader.ReadDouble();
      if (gadgetId == Gadget.Id.HYDRO_SHOWER)
        gadgetId = Gadget.Id.HYDRO_TURRET;
      baitTypeId = (Identifiable.Id) reader.ReadInt32();
      gordoTypeId = (Identifiable.Id) reader.ReadInt32();
      gordoEatenCount = reader.ReadInt32();
      fashions = LoadList(reader, (Func<int, Identifiable.Id>) (v => (Identifiable.Id) v));
      drone = LoadPersistable<DroneV04>(reader);
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) gadgetId);
      writer.Write(yRotation);
      writer.Write(isPrimaryInLink);
      WriteList(writer, ammo);
      writer.Write(extractorCyclesRemaining);
      writer.Write(extractorQueuedToProduce);
      writer.Write(extractorCycleEndTime);
      writer.Write(extractorNextProduceTime);
      writer.Write(waitForChargeupTime);
      writer.Write(lastSpawnTime);
      writer.Write((int) baitTypeId);
      writer.Write((int) gordoTypeId);
      writer.Write(gordoEatenCount);
      WriteList(writer, fashions, v => (int) v);
      WritePersistable(writer, drone);
    }

    public static PlacedGadgetV05 Load(BinaryReader reader)
    {
      PlacedGadgetV05 placedGadgetV05 = new PlacedGadgetV05();
      placedGadgetV05.Load(reader.BaseStream);
      return placedGadgetV05;
    }

    protected override void UpgradeFrom(PlacedGadgetV04 legacyData)
    {
      gadgetId = legacyData.gadgetId;
      yRotation = legacyData.yRotation;
      isPrimaryInLink = legacyData.isPrimaryInLink;
      ammo = legacyData.ammo;
      extractorCycleEndTime = legacyData.extractorCycleEndTime;
      extractorCyclesRemaining = legacyData.extractorCyclesRemaining;
      extractorNextProduceTime = legacyData.extractorNextProduceTime;
      extractorQueuedToProduce = legacyData.extractorQueuedToProduce;
      waitForChargeupTime = legacyData.waitForChargeupTime;
      lastSpawnTime = legacyData.lastSpawnTime;
      gordoTypeId = legacyData.gordoTypeId;
      baitTypeId = legacyData.baitTypeId;
      gordoEatenCount = legacyData.gordoEatenCount;
      fashions = legacyData.fashions;
      drone = null;
    }

    public static void AssertAreEqual(PlacedGadgetV05 expected, PlacedGadgetV05 actual)
    {
      for (int index = 0; index < expected.ammo.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected.ammo[index], actual.ammo[index]);
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
      DroneV04.AssertAreEqual(expected.drone, actual.drone);
    }

    public static void AssertAreEqual(PlacedGadgetV04 expected, PlacedGadgetV05 actual)
    {
      for (int index = 0; index < expected.ammo.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected.ammo[index], actual.ammo[index]);
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
      DroneV04.AssertAreEqual((DroneV04) null, actual.drone);
    }
  }
}
