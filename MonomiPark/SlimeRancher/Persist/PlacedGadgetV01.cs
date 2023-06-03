// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PlacedGadgetV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PlacedGadgetV01 : PersistedDataSet
  {
    public Gadget.Id gadgetId;
    public float yRotation;
    public bool isPrimaryInLink;
    public List<AmmoDataV02> ammo;
    public int extractorCyclesRemaining;
    public int extractorQueuedToProduce;
    public double extractorCycleEndTime;
    public double extractorNextProduceTime;

    public override string Identifier => "SRPG";

    public override uint Version => 1;

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
    }

    public static PlacedGadgetV01 Load(BinaryReader reader)
    {
      PlacedGadgetV01 placedGadgetV01 = new PlacedGadgetV01();
      placedGadgetV01.Load(reader.BaseStream);
      return placedGadgetV01;
    }

    public static void AssertAreEqual(PlacedGadgetV01 expected, PlacedGadgetV01 actual)
    {
      for (int index = 0; index < expected.ammo.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected.ammo[index], actual.ammo[index]);
    }
  }
}
