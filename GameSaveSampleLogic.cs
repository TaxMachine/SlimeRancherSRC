// Decompiled with JetBrains decompiler
// Type: GameSaveSampleLogic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Microsoft.Xbox;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameSaveSampleLogic : MonoBehaviour
{
  public Text output;
  private PlayerSaveData playerSaveData;

  private void Start()
  {
    playerSaveData = new PlayerSaveData();
    playerSaveData.name = "Jane Doe";
    playerSaveData.level = 2;
  }

  public void Save()
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize(serializationStream, playerSaveData);
      Gdk.Helpers.Save(serializationStream.ToArray());
      output.text = "\n Saved game data:\n Name: " + playerSaveData.name + "\n Level: " + playerSaveData.level;
    }
  }

  public void Load()
  {
    Gdk.Helpers.OnGameSaveLoaded += OnGameSaveLoaded;
    Gdk.Helpers.LoadSaveData();
  }

  private void OnGameSaveLoaded(object sender, GameSaveLoadedArgs saveData)
  {
    using (MemoryStream serializationStream = new MemoryStream(saveData.Data))
    {
      playerSaveData = new BinaryFormatter().Deserialize(serializationStream) as PlayerSaveData;
      output.text = "\n Loaded save game:\n Name: " + playerSaveData.name + "\n Level: " + playerSaveData.level;
    }
  }

  [Serializable]
  private class PlayerSaveData
  {
    public string name;
    public int level;
  }
}
