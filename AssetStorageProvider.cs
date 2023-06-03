// Decompiled with JetBrains decompiler
// Type: AssetStorageProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AssetStorageProvider : StorageProvider
{
  private string directory;

  public AssetStorageProvider(string directory) => this.directory = directory;

  public void Initialize()
  {
  }

  public bool IsInitialized() => true;

  public List<string> GetAvailableGames() => Resources.LoadAll<TextAsset>(directory).Select(r => r.name).ToList();

  public bool HasGameData(string name) => LoadAsset(name) != null;

  public void GetGameData(string name, MemoryStream stream)
  {
    TextAsset textAsset = LoadAsset(name);
    stream.Write(textAsset.bytes, 0, textAsset.bytes.Length);
  }

  public string GetGameId(string name) => string.Empty;

  public void StoreGameData(string gameId, string gameName, string name, MemoryStream stream)
  {
  }

  public void DeleteGameData(string name)
  {
  }

  public void DeleteGamesData(List<string> name)
  {
  }

  public bool HasProfile() => false;

  public void GetProfileData(MemoryStream stream)
  {
  }

  public void StoreProfileData(MemoryStream stream)
  {
  }

  public bool HasSettings() => false;

  public void GetSettingsData(MemoryStream stream)
  {
  }

  public void StoreSettingsData(MemoryStream stream)
  {
  }

  public void Flush()
  {
  }

  private TextAsset LoadAsset(string name) => Resources.Load<TextAsset>(string.Format("{0}/{1}", directory, name));
}
