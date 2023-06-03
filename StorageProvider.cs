// Decompiled with JetBrains decompiler
// Type: StorageProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

public interface StorageProvider
{
  void Initialize();

  bool IsInitialized();

  void StoreGameData(string gameId, string gameName, string name, MemoryStream dataStream);

  string GetGameId(string name);

  void GetGameData(string name, MemoryStream dataStream);

  List<string> GetAvailableGames();

  bool HasGameData(string name);

  void DeleteGameData(string name);

  void DeleteGamesData(List<string> name);

  void Flush();

  bool HasProfile();

  void GetProfileData(MemoryStream dataStream);

  void StoreProfileData(MemoryStream dataStream);

  bool HasSettings();

  void GetSettingsData(MemoryStream dataStream);

  void StoreSettingsData(MemoryStream dataStream);
}
