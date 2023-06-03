// Decompiled with JetBrains decompiler
// Type: EmptyStorageProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

public class EmptyStorageProvider : StorageProvider, IDisposable
{
  public void DeleteGameData(string name)
  {
  }

  public void DeleteGamesData(List<string> names)
  {
  }

  public void Dispose()
  {
  }

  public List<string> GetAvailableGames() => new List<string>();

  public string GetGameId(string name) => string.Empty;

  public void GetGameData(string name, MemoryStream dataStream)
  {
  }

  public void GetProfileData(MemoryStream dataStream)
  {
  }

  public void GetSettingsData(MemoryStream dataStream)
  {
  }

  public bool HasGameData(string name) => false;

  public bool HasProfile() => false;

  public bool HasSettings() => false;

  public void Initialize()
  {
  }

  public bool IsInitialized() => true;

  public void StoreGameData(string gameId, string gameName, string name, MemoryStream dataStream)
  {
  }

  public void StoreProfileData(MemoryStream dataStream)
  {
  }

  public void StoreSettingsData(MemoryStream dataStream)
  {
  }

  public void Flush()
  {
  }
}
