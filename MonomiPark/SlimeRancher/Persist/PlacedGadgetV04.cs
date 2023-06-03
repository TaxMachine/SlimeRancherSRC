// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PlacedGadgetV04
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PlacedGadgetV04 : VersionedPersistedDataSet<PlacedGadgetV03>
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

    public override string Identifier => "SRPG";

    public override uint Version => 4;

    public PlacedGadgetV04()
    {
    }

    public PlacedGadgetV04(PlacedGadgetV03 legacyData) => UpgradeFrom(legacyData);

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
    }

    public static PlacedGadgetV04 Load(BinaryReader reader)
    {
      PlacedGadgetV04 placedGadgetV04 = new PlacedGadgetV04();
      placedGadgetV04.Load(reader.BaseStream);
      return placedGadgetV04;
    }

    protected override void UpgradeFrom(PlacedGadgetV03 legacyData)
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
      fashions = new List<Identifiable.Id>();
    }

    public static void AssertAreEqual(PlacedGadgetV04 expected, PlacedGadgetV04 actual)
    {
      for (int index = 0; index < expected.ammo.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected.ammo[index], actual.ammo[index]);
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
    }

    public static void AssertAreEqual(PlacedGadgetV03 expected, PlacedGadgetV04 actual)
    {
      for (int index = 0; index < expected.ammo.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected.ammo[index], actual.ammo[index]);
    }
  }
}
