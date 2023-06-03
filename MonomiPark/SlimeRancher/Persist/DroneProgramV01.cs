// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DroneProgramV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DroneProgramV01 : PersistedDataSet
  {
    public string target;
    public string source;
    public string destination;

    public override string Identifier => "SRDRONEPROG";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader)
    {
      target = reader.ReadString();
      source = reader.ReadString();
      destination = reader.ReadString();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(target);
      writer.Write(source);
      writer.Write(destination);
    }

    public static void AssertAreEqual(DroneProgramV01 expected, DroneProgramV01 actual)
    {
    }

    public override string ToString() => string.Format("{0} [target={1}, source={2}, destination={3}]", GetType(), target, source, destination);
  }
}
