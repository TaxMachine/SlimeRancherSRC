// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ActorDataV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ActorDataV02 : PersistedDataSet
  {
    public Vector3V02 pos;
    public Vector3V02 rot;
    public int id;
    public SlimeEmotionDataV02 emotions;
    public float transformTime;
    public float reproduceTime;
    public ResourceCycleDataV02 cycleData;
    public float? disabledAtTime;

    public override string Identifier => "SRAD";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      pos = new Vector3V02();
      pos.Load(reader.BaseStream);
      rot = new Vector3V02();
      rot.Load(reader.BaseStream);
      id = reader.ReadInt32();
      emotions = new SlimeEmotionDataV02();
      emotions.Load(reader.BaseStream);
      transformTime = reader.ReadSingle();
      reproduceTime = reader.ReadSingle();
      cycleData = new ResourceCycleDataV02();
      cycleData.Load(reader.BaseStream);
      if (!reader.ReadBoolean())
        return;
      disabledAtTime = new float?(reader.ReadSingle());
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
    }

    public static void AssertAreEqual(ActorDataV02 expected, ActorDataV02 actual)
    {
      Vector3V02.AssertAreEqual(expected.pos, actual.pos);
      Vector3V02.AssertAreEqual(expected.rot, actual.rot);
      ResourceCycleDataV02.AssertAreEqual(expected.cycleData, actual.cycleData);
      SlimeEmotionDataV02.AssertAreEqual(expected.emotions, actual.emotions);
    }
  }
}
