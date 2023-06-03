// Decompiled with JetBrains decompiler
// Type: ProfileData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ProfileData : Persistable
{
  public const int CURR_FORMAT_ID = 3;
  public const int MIN_HANDLED_FORMAT_ID = 1;
  public int optionsFormatID = 2;
  public OptionsData options = new OptionsData();
  public AchieveData achieve = new AchieveData();
  public string continueGameName;
  private const string NAME = "slimerancher.prf";

  private static BinaryFormatter CreateFormatter()
  {
    BinaryFormatter formatter = new BinaryFormatter();
    SurrogateSelector surrogateSelector = new SurrogateSelector();
    surrogateSelector.AddSurrogate(typeof (Vector3), new StreamingContext(StreamingContextStates.All), new Vector3Surrogate());
    formatter.SurrogateSelector = surrogateSelector;
    return formatter;
  }

  public void Load(Stream stream)
  {
    BinaryFormatter formatter = CreateFormatter();
    int num = (int) formatter.Deserialize(stream);
    if (num > 3)
    {
      Debug.Log("File format newer than current version type=ProfileData fileVer=" + num + " currVer=" + 3);
      throw new VersionMismatchException("File format newer than current version.");
    }
    if (num < 1)
    {
      Debug.Log("Unhandled version type=ProfileData fileVer=" + num + " currVer=" + 3);
      throw new VersionMismatchException("File format unhandled.");
    }
    options = DataModule<OptionsData>.Deserialize(formatter, stream, 2);
    achieve = DataModule<AchieveData>.Deserialize(formatter, stream, 2);
    try
    {
      continueGameName = (string) formatter.Deserialize(stream);
    }
    catch (EndOfStreamException ex)
    {
      continueGameName = null;
    }
  }

  public long Write(Stream stream) => throw new NotImplementedException("Write is not supported for legacy data.");
}
