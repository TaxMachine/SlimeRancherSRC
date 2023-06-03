// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.FirestormV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class FirestormV01 : PersistedDataSet
  {
    public double endStormTime;
    public bool stormPreparing;
    public double nextStormTime;

    public override string Identifier => "SRF";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader)
    {
      endStormTime = reader.ReadDouble();
      stormPreparing = reader.ReadBoolean();
      nextStormTime = reader.ReadDouble();
    }

    public static FirestormV01 Load(BinaryReader reader)
    {
      FirestormV01 firestormV01 = new FirestormV01();
      firestormV01.Load(reader.BaseStream);
      return firestormV01;
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(endStormTime);
      writer.Write(stormPreparing);
      writer.Write(nextStormTime);
    }

    public static void AssertAreEqual(FirestormV01 expected, FirestormV01 actual)
    {
    }
  }
}
