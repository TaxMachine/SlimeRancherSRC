// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.Vector3V02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using UnityEngine;

namespace MonomiPark.SlimeRancher.Persist
{
  public class Vector3V02 : PersistedDataSet
  {
    public Vector3 value;
    public const float ROT_APPROX_TOLERANCE = 0.1f;

    public override string Identifier => "SRV3";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader) => value = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(value.x);
      writer.Write(value.y);
      writer.Write(value.z);
    }

    public static Vector3V02 Load(BinaryReader reader)
    {
      Vector3V02 vector3V02 = new Vector3V02();
      vector3V02.Load(reader.BaseStream);
      return vector3V02;
    }

    public override bool Equals(object obj) => obj != null && obj is Vector3V02 && Equals(obj as Vector3V02);

    public bool Equals(Vector3V02 v) => value.x == (double) v.value.x && value.y == (double) v.value.y && value.z == (double) v.value.z;

    public override int GetHashCode() => ((17 * 23 + value.x.GetHashCode()) * 23 + value.y.GetHashCode()) * 23 + value.z.GetHashCode();

    public override string ToString() => value.ToString();

    public static void AssertAreEqual(Vector3V02 expected, Vector3V02 actual)
    {
    }

    public static void AssertAreApproximatelyEqual(
      Vector3V02 expected,
      Vector3V02 actual,
      float tolerance)
    {
    }

    internal static void AssertAreApproximatelyEqual(
      Vector3V02 rot1,
      Vector3V02 rot2,
      object rOT_APPROX_TOLERANCE)
    {
      throw new NotImplementedException();
    }
  }
}
