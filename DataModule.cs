// Decompiled with JetBrains decompiler
// Type: DataModule`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public abstract class DataModule<T> where T : DataModule<T>
{
  public void Serialize(BinaryFormatter formatter, FileStream file, int currFormatID)
  {
    formatter.Serialize(file, currFormatID);
    formatter.Serialize(file, this);
  }

  public static T Deserialize(BinaryFormatter formatter, Stream file, int currFormatID)
  {
    int num = (int) formatter.Deserialize(file);
    if (num > currFormatID)
    {
      Debug.Log("File format newer than current version type=" + typeof (T) + " fileVer=" + num + " currVer=" + currFormatID);
      throw new VersionMismatchException("File format newer than current version.");
    }
    if (num < currFormatID)
    {
      Debug.Log("Unhandled version type=" + typeof (T) + " fileVer=" + num + " currVer=" + currFormatID);
      throw new VersionMismatchException("File format unhandled.");
    }
    return formatter.Deserialize(file) as T;
  }
}
