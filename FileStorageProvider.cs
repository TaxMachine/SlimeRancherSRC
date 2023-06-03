// Decompiled with JetBrains decompiler
// Type: FileStorageProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileStorageProvider : StorageProvider
{
  private const string EXTENSION = ".sav";
  private const string TEMP_EXTENSION = ".tmp";
  private const string PROFILE_FILENAME = "slimerancher.prf";
  private const string SETTINGS_FILENAME = "slimerancher.cfg";
  private bool isInitialized;

  public void Initialize()
  {
    Directory.CreateDirectory(SavePath());
    try
    {
      MaybeMoveOldData();
    }
    catch (Exception ex)
    {
      Log.Debug("Attempted to move old data, failed.", "Exception", ex);
    }
    isInitialized = true;
  }

  public bool IsInitialized() => isInitialized;

  private void MaybeMoveOldData()
  {
  }

  public void GetGameData(string name, MemoryStream dataStream) => Load(GetFullFilePath(name), name, dataStream);

  private void Load(string path, string name, MemoryStream loadInto)
  {
    if (!File.Exists(path))
      throw new FileNotFoundException("No save file found", path);
    using (FileStream from = File.Open(path, FileMode.Open))
      CopyStream(from, loadInto);
  }

  private void CopyStream(Stream from, Stream to)
  {
    byte[] buffer = new byte[1024];
    int count;
    do
    {
      count = from.Read(buffer, 0, buffer.Length);
      to.Write(buffer, 0, count);
    }
    while (count >= buffer.Length);
  }

  public List<string> GetAvailableGames()
  {
    string path = SavePath();
    return !Directory.Exists(path) ? new List<string>() : Directory.GetFiles(path, "*.sav").Select(f => Path.GetFileNameWithoutExtension(f)).ToList();
  }

  public string GetGameId(string name) => string.Empty;

  public void StoreGameData(string gameId, string gameName, string name, MemoryStream stream)
  {
    string fullFilePath = GetFullFilePath(name);
    string str = string.Format("{0}{1}", fullFilePath, ".tmp");
    using (FileStream to = File.Create(str))
      CopyStream(stream, to);
    File.Copy(str, fullFilePath, true);
    try
    {
      File.Delete(str);
    }
    catch (Exception ex)
    {
      Log.Warning("Failed to delete temporary save file.", "temp file", str, "Exception", ex.Message);
    }
  }

  public void DeleteGameData(string name)
  {
    string fullFilePath = GetFullFilePath(name);
    if (!File.Exists(fullFilePath))
      throw new FileNotFoundException("No file found to delete", fullFilePath);
    File.Delete(fullFilePath);
  }

  public void DeleteGamesData(List<string> names)
  {
    foreach (string name in names)
      DeleteGameData(name);
  }

  public bool HasGameData(string name) => File.Exists(GetFullFilePath(name));

  private string GetFullFilePath(string name) => Path.Combine(SavePath(), string.Format("{0}{1}", name, ".sav"));

  private string SavePath()
  {
    string str = Application.persistentDataPath;
    string oldValue = "unity.Monomi Park.Slime Rancher";
    if (str.EndsWith(oldValue))
      str = str.Replace(oldValue, Path.Combine("Monomi Park", "Slime Rancher"));
    return str;
  }

  public bool HasProfile() => FileExists("slimerancher.prf");

  public void GetProfileData(MemoryStream dataStream) => LoadDataStream("slimerancher.prf", dataStream);

  public void StoreProfileData(MemoryStream profileDataStream) => StoreDataStream("slimerancher.prf", profileDataStream);

  public bool HasSettings() => FileExists("slimerancher.cfg");

  public void GetSettingsData(MemoryStream dataStream) => LoadDataStream("slimerancher.cfg", dataStream);

  public void StoreSettingsData(MemoryStream dataStream) => StoreDataStream("slimerancher.cfg", dataStream);

  public void Flush()
  {
  }

  private void LoadDataStream(string fileName, MemoryStream stream)
  {
    string path = ToPath(fileName);
    Log.Debug("Loading data from file.", path);
    if (File.Exists(path))
    {
      using (FileStream from = File.Open(path, FileMode.Open))
        CopyStream(from, stream);
    }
    else
      Log.Warning("File not found", "Path", path);
  }

  private void StoreDataStream(string fileName, MemoryStream stream)
  {
    string path = ToPath(fileName);
    Log.Debug("Saving file.", fileName);
    using (FileStream to = File.Create(path))
    {
      try
      {
        CopyStream(stream, to);
      }
      catch (Exception ex)
      {
        Log.Warning("Failed to save file.", "Path", path, "Exception", ex.Message, "Stack Trace", ex.StackTrace);
      }
    }
  }

  private bool FileExists(string fileName) => File.Exists(ToPath(fileName));

  private string ToPath(string fileName) => Path.Combine(SavePath(), fileName);
}
