// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ResourceWaterV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ResourceWaterV02 : PersistedDataSet
  {
    public float spawn;
    public float water;

    public override string Identifier => "SRRW";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      spawn = reader.ReadSingle();
      water = reader.ReadSingle();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(spawn);
      writer.Write(water);
    }

    public static ResourceWaterV02 Load(BinaryReader reader)
    {
      ResourceWaterV02 resourceWaterV02 = new ResourceWaterV02();
      resourceWaterV02.Load(reader.BaseStream);
      return resourceWaterV02;
    }

    public static void AssertAreEqual(ResourceWaterV02 expected, ResourceWaterV02 actual)
    {
    }
  }
}
