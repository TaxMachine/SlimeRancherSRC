// Decompiled with JetBrains decompiler
// Type: AutoSaveDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.Script.Util.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AutoSaveDirector : SRBehaviour
{
  public GameObject saveErrorPrefab;
  public LoadErrorUI loadFileErrorPrefab;
  public DLCPurgedExceptionUI prefabDLCPurgedExceptionUI;
  private LoadingUI loadingUI;
  private float nextSaveTime;
  private bool loadedGame;
  private bool loadingGame;
  private LoadNewGameMetadata newGameMetadata;
  private float lastSaveTime;
  private const float SAVE_PERIOD = 1440f;
  private const int MAX_AUTOSAVES = 5;
  private string currentGameId;
  private static bool firstLoad = true;

  public SavedGame SavedGame { get; private set; }

  public SavedProfile ProfileManager { get; private set; } = new SavedProfile();

  public StorageProvider StorageProvider { get; private set; }

  public float GetLastSaveTime() => lastSaveTime;

  public void Awake()
  {
    SavedGame = new SavedGame(new ScenePrefabInstantiator(SRSingleton<GameContext>.Instance.LookupDirector), new SceneSavedGameInfoProvider());
    nextSaveTime = Time.time + 1440f;
    enabled = true;
    Initialize();
  }

  public void Start()
  {
  }

  private void Initialize()
  {
    StorageProvider = new FileStorageProvider();
    StorageProvider.Initialize();
  }

  public void OnSceneLoaded()
  {
    SetupDynamicObjects();
    if (!(gameObject == SRSingleton<GameContext>.Instance.gameObject))
      return;
    LoadProfile();
  }

  private void SetupDynamicObjects()
  {
    if (IsNewGame())
      SRSingleton<DynamicObjectContainer>.Instance.RegisterDynamicObjectActors();
    else
      SRSingleton<DynamicObjectContainer>.Instance.DestroyDynamicObjectActors();
  }

  public void Update()
  {
    if (Levels.isSpecialNonAlloc() || Time.time < (double) nextSaveTime)
      return;
    SaveAllNow();
  }

  public bool SaveAllNow()
  {
    Log.Warning("Saving game and profile...");
    nextSaveTime = Time.time + 1440f;
    try
    {
      SaveGame();
      SaveProfile(false);
      StorageProvider.Flush();
    }
    catch (Exception ex)
    {
      ErrorSaveFailure(ex);
      return false;
    }
    lastSaveTime = Time.time;
    return true;
  }

  public GameData.Summary LoadSummary(string saveName)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      StorageProvider.GetGameData(saveName, memoryStream);
      if (memoryStream.Length == 0L)
      {
        Log.Warning("Datastream was empty when loading save.", nameof (saveName), saveName);
        return new GameData.Summary(saveName);
      }
      memoryStream.Seek(0L, SeekOrigin.Begin);
      return SavedGame.LoadSummary(saveName, memoryStream);
    }
  }

  private void ErrorSaveFailure(Exception e)
  {
    Log.Error("Error while saving.", "Exception", e.Message, "Stack Trace", e.StackTrace);
    Instantiate(saveErrorPrefab).GetComponent<SaveErrorUI>().SetException(e, SavedGame.GetName());
  }

  public void OnApplicationQuit()
  {
    if (Levels.isSpecial())
      return;
    SaveGame();
    SaveProfile(false);
    StorageProvider.Flush();
  }

  public void LoadNewGame(
    string displayName,
    Identifiable.Id gameIconId,
    PlayerState.GameMode gameMode,
    Action onError)
  {
    StartCoroutine(LoadNewGame_Coroutine(new LoadNewGameMetadata()
    {
      displayName = displayName,
      gameMode = gameMode,
      gameIconId = gameIconId
    }, onError));
  }

  private IEnumerator LoadNewGame_Coroutine(
    LoadNewGameMetadata metadata,
    Action onError)
  {
    AutoSaveDirector autoSaveDirector = this;
    autoSaveDirector.loadingGame = true;
    autoSaveDirector.loadedGame = false;
    autoSaveDirector.newGameMetadata = metadata;
    yield return autoSaveDirector.OpenLoadingUI();
    yield return SRSingleton<GameContext>.Instance.DLCDirector.RefreshPackagesAsync();
    try
    {
      autoSaveDirector.currentGameId = null;
      string gameSaveFileName = autoSaveDirector.GetGameSaveFileName(metadata.displayName);
      string str = "";
      int num = 1;
      while (autoSaveDirector.StorageProvider.HasGameData(string.Format("{0}{1}", gameSaveFileName, str)))
      {
        str = string.Format("_{0}", num);
        ++num;
      }
      autoSaveDirector.SavedGame.CreateNew(string.Format("{0}{1}", gameSaveFileName, str), metadata.displayName);
      SceneContext.onNextSceneAwake += autoSaveDirector.OnNextSceneAwake_NewGame;
      SceneContext.onSceneLoaded += autoSaveDirector.OnNewGameLoaded;
      autoSaveDirector.BeginSceneSwitch(onError);
    }
    catch (Exception ex)
    {
      Log.Error("Error while creating a new save file.", "Exception", ex.Message, "Stack Trace", ex.StackTrace);
      LoadErrorUI.OpenLoadErrorUI(autoSaveDirector.loadFileErrorPrefab, MessageUtil.Tcompose("e.pushfile_error", new string[1]
      {
        autoSaveDirector.SavedGame.GetName()
      }), true, "e.ok_button", () => RevertToMainMenu(onError));
      autoSaveDirector.loadingUI.OnLoadingError();
    }
  }

  private string GetGameSaveFileName(string displayName)
  {
    string str = Regex.Replace(displayName, "[^A-Za-z0-9]", "");
    return string.Format("{0:yyyyMMddHHmmss}_{1}", DateTime.Now, str.Substring(0, Mathf.Min(25, str.Length)));
  }

  public Dictionary<string, List<GameData.Summary>> AvailableGamesByDisplayName() => AvailableGames(summary => summary.displayName);

  public Dictionary<string, List<GameData.Summary>> AvailableGamesByGameName() => AvailableGames(summary => summary.name);

  private Dictionary<string, List<GameData.Summary>> AvailableGames(
    Func<GameData.Summary, string> keyFunc)
  {
    List<string> availableGames = StorageProvider.GetAvailableGames();
    Dictionary<string, List<GameData.Summary>> dictionary = new Dictionary<string, List<GameData.Summary>>();
    foreach (string saveName in availableGames)
    {
      try
      {
        GameData.Summary summary = LoadSummary(saveName);
        string key = keyFunc(summary);
        List<GameData.Summary> summaryList;
        if (!dictionary.TryGetValue(key, out summaryList))
        {
          summaryList = new List<GameData.Summary>();
          dictionary.Add(key, summaryList);
        }
        summaryList.Add(summary);
      }
      catch (Exception ex)
      {
        Log.Error("Failed to load summary for saved game.", "name", saveName, "Exception", ex.ToString(), "Exception Stack Trace", ex.StackTrace);
      }
    }
    foreach (KeyValuePair<string, List<GameData.Summary>> keyValuePair in dictionary)
      keyValuePair.Value.Sort(CompareSummaryBySaveOrder);
    return dictionary;
  }

  private int CompareSummaryBySaveOrder(GameData.Summary s1, GameData.Summary s2)
  {
    int num = s2.saveNumber.CompareTo(s1.saveNumber);
    if (num == 0)
      num = s2.saveTimestamp.CompareTo(s1.saveTimestamp);
    return num;
  }

  public bool DisplayNameAvailable(string displayName)
  {
    foreach (string availableGame in StorageProvider.GetAvailableGames())
    {
      Log.Debug(availableGame);
      GameData.Summary summary = LoadSummary(availableGame);
      if (string.Compare(displayName, summary.displayName, false) == 0)
        return false;
    }
    return true;
  }

  public bool GameExists(string gameName) => StorageProvider.HasGameData(gameName);

  public void DeleteGame(string gameName)
  {
    List<GameData.Summary> savesByGameName = GetSavesByGameName(gameName);
    for (int index = 0; index < savesByGameName.Count; ++index)
    {
      string saveName = savesByGameName[index].saveName;
      if (StorageProvider.HasGameData(saveName))
        DeleteSave(saveName);
    }
    StorageProvider.Flush();
  }

  private List<GameData.Summary> GetSavesByGameName(string gameName)
  {
    List<GameData.Summary> savesByGameName;
    if (!AvailableGamesByGameName().TryGetValue(gameName, out savesByGameName))
      savesByGameName = new List<GameData.Summary>();
    return savesByGameName;
  }

  public void CleanupAutosaves(string gameName)
  {
    List<GameData.Summary> savesByGameName = GetSavesByGameName(gameName);
    List<string> name = new List<string>();
    for (int index = 5; index < savesByGameName.Count; ++index)
    {
      string saveName = savesByGameName[index].saveName;
      if (StorageProvider.HasGameData(saveName))
      {
        Log.Warning("Cleaning up autosave file.", "name", saveName);
        name.Add(saveName);
      }
    }
    StorageProvider.DeleteGamesData(name);
  }

  public void DeleteSave(string saveName) => StorageProvider.DeleteGameData(saveName);

  public bool IsNewGame() => !loadedGame || Levels.isSpecial();

  public bool HasContinue()
  {
    if (string.IsNullOrEmpty(ProfileManager.ContinueGameName))
      return false;
    GameData.Summary saveToContinue = GetSaveToContinue();
    return saveToContinue != null && StorageProvider.HasGameData(saveToContinue.saveName);
  }

  public GameData.Summary GetSaveToContinue()
  {
    string continueGameName = ProfileManager.ContinueGameName;
    if (string.IsNullOrEmpty(continueGameName))
      return null;
    List<GameData.Summary> source;
    return AvailableGamesByGameName().TryGetValue(continueGameName, out source) ? source.FirstOrDefault() : null;
  }

  public void SaveGameAndFlush()
  {
    SaveGame();
    StorageProvider.Flush();
  }

  private void SaveGame()
  {
    if (loadingGame)
      Log.Warning("Attempted to save game while loading, skipping.");
    else if (!string.IsNullOrEmpty(SavedGame.GetName()))
    {
      SavedGame.Pull(SRSingleton<SceneContext>.Instance.GameModel);
      string name = SavedGame.GetName();
      string displayName = SavedGame.GetDisplayName();
      using (MemoryStream dataStream = new MemoryStream())
      {
        SavedGame.Save(dataStream);
        dataStream.Seek(0L, SeekOrigin.Begin);
        string nextFileName = GetNextFileName(name, 0, "{0}_{1}");
        if (currentGameId == null)
        {
          currentGameId = StorageProvider.GetGameId(nextFileName);
          Log.Warning("setting initial gameid", "currentGameId", currentGameId, "nextFileName", nextFileName);
        }
        StorageProvider.StoreGameData(currentGameId, displayName, nextFileName, dataStream);
      }
      ProfileManager.ContinueGameName = SavedGame.GameState.summary.isGameOver ? string.Empty : name;
      CleanupAutosaves(name);
    }
    else
      Log.Warning("Save game name was null or empty. Skipping save.");
  }

  private string GetNextFileName(string filename, int startingNumber, string format)
  {
    string name;
    do
    {
      name = string.Format(format, filename, startingNumber++);
    }
    while (StorageProvider.HasGameData(name));
    return name;
  }

  private void LoadProfile()
  {
    Log.Debug("Storage provider initialized. Loading profile.", (StorageProvider == null).ToString());
    LoadFromStream(StorageProvider.GetProfileData, ProfileManager.LoadProfile, () =>
    {
      Log.Debug("No profile was found.");
      ProfileManager = new SavedProfile();
    });
    LoadSettings();
    ProfileManager.Push();
    if (!firstLoad)
      return;
    firstLoad = false;
    string[] commandLineArgs = Environment.GetCommandLineArgs();
    if (commandLineArgs != null)
    {
      foreach (string str in commandLineArgs)
      {
        if (str.Contains("lowGraphics"))
        {
          Log.Debug("Forcing Lowest Quality Graphics");
          SRQualitySettings.ForceLowQuality();
          SaveProfile(false);
        }
      }
    }
    ProfileManager.Profile.RunUpgradeActions(this);
  }

  private void LoadSettings()
  {
    if (StorageProvider.HasSettings())
    {
      LoadFromStream(StorageProvider.GetSettingsData, ProfileManager.LoadSettings, () => Log.Debug("No settings were found."));
    }
    else
    {
      if (!StorageProvider.HasProfile())
        return;
      LoadFromStream(StorageProvider.GetProfileData, ProfileManager.LoadLegacySettings, () => Log.Debug("No profile data was found to load legacy settings."));
    }
  }

  public bool SaveProfile() => SaveProfile(true);

  private bool SaveProfile(bool forceFlush)
  {
    ProfileManager.Pull();
    if (StorageProvider.IsInitialized())
    {
      SaveStream(ProfileManager.SaveProfile, StorageProvider.StoreProfileData);
      SaveStream(ProfileManager.SaveSettings, StorageProvider.StoreSettingsData);
      if (forceFlush)
        StorageProvider.Flush();
      return true;
    }
    Log.Warning("Storage provider not initialized. Skipping profile and settings save.");
    return false;
  }

  private void LoadFromStream(
    Action<MemoryStream> openStream,
    Action<MemoryStream> load,
    Action onErr)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      openStream(memoryStream);
      memoryStream.Seek(0L, SeekOrigin.Begin);
      if (memoryStream.Length > 0L)
        load(memoryStream);
      else
        onErr();
    }
  }

  private void SaveStream(Action<Stream> saveToStream, Action<MemoryStream> storeStream)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      saveToStream(memoryStream);
      memoryStream.Seek(0L, SeekOrigin.Begin);
      storeStream(memoryStream);
    }
  }

  public void ResetProfile()
  {
    Log.Info("Resetting profile.");
    SRQualitySettings.ResetProfile();
    SRSingleton<GameContext>.Instance.OptionsDirector.ResetProfile();
    SRSingleton<GameContext>.Instance.InputDirector.ResetProfile();
    SRSingleton<SceneContext>.Instance.AchievementsDirector.ResetProfile();
    SRSingleton<GameContext>.Instance.MessageDirector.SetCulture(SRSingleton<GameContext>.Instance.MessageDirector.defaultLang);
    ProfileManager.ContinueGameName = "";
    SaveProfile(false);
  }

  public bool IsLoadingGame() => loadingGame;

  public void BeginLoad(string gameName, string saveName, Action onError)
  {
    if (loadingGame)
      return;
    LoadSave(gameName, saveName, true, onError);
  }

  private void OnNextSceneAwake_NewGame(SceneContext sceneContext)
  {
    sceneContext.GameModel.expectingPush = false;
    sceneContext.GameModeConfig.initGameMode = newGameMetadata.gameMode;
    sceneContext.GameModel.gameIconId = newGameMetadata.gameIconId;
    newGameMetadata = null;
  }

  private void OnNextSceneAwake_ExistingGame(SceneContext sceneContext) => sceneContext.GameModel.expectingPush = true;

  private void LoadSave(
    string gameName,
    string saveName,
    bool promptDLCPurgedException,
    Action onError)
  {
    StartCoroutine(LoadSave_Coroutine(gameName, saveName, promptDLCPurgedException, onError));
  }

  private IEnumerator LoadSave_Coroutine(
    string gameName,
    string saveName,
    bool promptDLCPurgedException,
    Action onError)
  {
    AutoSaveDirector autoSaveDirector = this;
    autoSaveDirector.loadingGame = true;
    autoSaveDirector.loadedGame = true;
    yield return autoSaveDirector.OpenLoadingUI();
    yield return SRSingleton<GameContext>.Instance.DLCDirector.RefreshPackagesAsync();
    try
    {
      using (MemoryStream dataStream = new MemoryStream())
      {
        autoSaveDirector.StorageProvider.GetGameData(saveName, dataStream);
        dataStream.Seek(0L, SeekOrigin.Begin);
        autoSaveDirector.SavedGame.Load(dataStream);
        autoSaveDirector.currentGameId = autoSaveDirector.StorageProvider.GetGameId(saveName);
      }
      try
      {
        SRSingleton<GameContext>.Instance.DLCDirector.Purge(autoSaveDirector.SavedGame.GameState);
      }
      catch (DLCPurgedException ex)
      {
        if (promptDLCPurgedException)
        {
          DLCPurgedExceptionUI.OnExceptionCaught(autoSaveDirector.prefabDLCPurgedExceptionUI, ex, () => LoadSave(gameName, saveName, false, onError), () => RevertToMainMenu(onError));
          autoSaveDirector.loadingUI.OnLoadingError();
          yield break;
        }
      }
      SceneContext.onNextSceneAwake += autoSaveDirector.OnNextSceneAwake_ExistingGame;
      autoSaveDirector.BeginSceneSwitch(onError);
    }
    catch (Exception ex)
    {
      Log.Error("Error while loading a save file.", "save", saveName, "Exception", ex.Message, "Stack Trace", ex.StackTrace);
      LoadErrorUI.OpenLoadErrorUI(autoSaveDirector.loadFileErrorPrefab, "e.file_load_failed", false, "e.yes_button", () => LoadFallbackSave(gameName, saveName, true, onError), "e.no_button", () => RevertToMainMenu(onError));
      autoSaveDirector.loadingUI.OnLoadingError();
    }
  }

  private void LoadFallbackSave(
    string gameName,
    string saveName,
    bool promptDLCPurgedException,
    Action onError)
  {
    StartCoroutine(LoadFallbackSave_Coroutine(gameName, saveName, promptDLCPurgedException, onError));
  }

  private IEnumerator LoadFallbackSave_Coroutine(
    string gameName,
    string saveName,
    bool promptDLCPurgedException,
    Action onError)
  {
    AutoSaveDirector autoSaveDirector = this;
    IEnumerable<GameData.Summary> summaries = autoSaveDirector.GetSavesByGameName(gameName).SkipWhile(s => saveName.CompareTo(s.saveName) != 0).Skip(1);
    yield return autoSaveDirector.OpenLoadingUI();
    int count = 0;
    IEnumerator<GameData.Summary> enumerator = summaries.GetEnumerator();
    bool flag;
    try
    {
      while (true)
      {
        if (enumerator.MoveNext())
        {
          GameData.Summary summary = enumerator.Current;
          ++count;
          yield return new WaitForSeconds(0.1f);
          try
          {
            using (MemoryStream dataStream = new MemoryStream())
            {
              autoSaveDirector.StorageProvider.GetGameData(summary.saveName, dataStream);
              dataStream.Seek(0L, SeekOrigin.Begin);
              autoSaveDirector.SavedGame.Load(dataStream);
              autoSaveDirector.currentGameId = autoSaveDirector.StorageProvider.GetGameId(summary.saveName);
            }
            try
            {
              SRSingleton<GameContext>.Instance.DLCDirector.Purge(autoSaveDirector.SavedGame.GameState);
            }
            catch (DLCPurgedException ex)
            {
              if (promptDLCPurgedException)
              {
                DLCPurgedExceptionUI.OnExceptionCaught(autoSaveDirector.prefabDLCPurgedExceptionUI, ex, () => LoadFallbackSave(gameName, saveName, false, onError), () => RevertToMainMenu(onError));
                autoSaveDirector.loadingUI.OnLoadingError();
                flag = false;
                break;
              }
            }
            SceneContext.onNextSceneAwake += autoSaveDirector.OnNextSceneAwake_ExistingGame;
            autoSaveDirector.BeginSceneSwitch(onError);
            flag = false;
            break;
          }
          catch (Exception ex)
          {
            Log.Error("Failed to fallback to prior save.", "save", summary.saveName, "Exception", ex.Message, "Stack Trace", ex.StackTrace);
          }
        }
        else
          goto label_10;
      }
      goto label_17;
    }
    finally
    {
      enumerator?.Dispose();
    }
    label_10:
      enumerator = null;
      Log.Error(string.Format("Failed all fallback attempts. Attempted to load {0} files.", count));
      LoadErrorUI.OpenLoadErrorUI(autoSaveDirector.loadFileErrorPrefab, "e.fallback_failed", true, "e.ok_button", () => RevertToMainMenu(onError));
      autoSaveDirector.loadingUI.OnLoadingError();
      yield break;
    label_17:
      yield return flag;
  }

  private void BeginSceneSwitch(Action onErr)
  {
    SceneContext.onSceneLoaded += OnGameLoaded;
    SceneManager.LoadSceneAsync("worldGenerated", LoadSceneMode.Single);
  }

  private void OnNewGameLoaded(SceneContext sceneContext) => sceneContext.GameModel.OnNewGameLoaded();

  private void OnGameLoaded(SceneContext ctx)
  {
    SceneContext.onSceneLoaded -= OnGameLoaded;
    StartCoroutine(OnGameLoadedCoroutine(ctx));
  }

  private IEnumerator OnGameLoadedCoroutine(SceneContext ctx)
  {
    if (ctx.GameModel.expectingPush)
    {
      Exception exception = null;
      try
      {
        SavedGame.Push(ctx.GameModel);
      }
      catch (Exception ex)
      {
        Log.Error("Error while populating scene from save game.", "save", SavedGame.GetName(), "Exception", ex.Message, "Stack Trace", ex.StackTrace);
        exception = ex;
        LoadErrorUI.OpenLoadErrorUI(loadFileErrorPrefab, MessageUtil.Tcompose("e.pushfile_error", new string[1]
        {
          SavedGame.GetName()
        }), true, "e.ok_button", () => RevertToMainMenu(() => Log.Debug("Falling back to main menu from worldGenerated.")));
        loadingUI.OnLoadingError();
      }
      finally
      {
        ctx.GameModel.expectingPush = false;
      }
      if (exception != null)
        yield break;
    }
    ctx.TutorialDirector.SuppressTutorials();
    yield return new WaitForEndOfFrame();
    yield return new WaitForEndOfFrame();
    CloseLoadingUI();
    if (IsNewGame() && ctx.GameModeConfig.GetModeSettings().newGamePrefab != null)
      Destroyer.Monitor(Instantiate(ctx.GameModeConfig.GetModeSettings().newGamePrefab), metadata => ctx.TutorialDirector.UnsuppressTutorials());
    else
      ctx.TutorialDirector.UnsuppressTutorials();
    ctx.NoteGameFullyLoaded();
    loadingGame = false;
  }

  private void RevertToMainMenu(Action onError)
  {
    SceneContext.onNextSceneAwake -= OnNextSceneAwake_NewGame;
    SceneContext.onNextSceneAwake -= OnNextSceneAwake_ExistingGame;
    SceneContext.onSceneLoaded -= OnNewGameLoaded;
    SceneContext.onSceneLoaded -= OnGameLoaded;
    loadingUI.isReturningToMenu = true;
    if (Levels.isMainMenu())
    {
      RevertToMainMenu_OnRevertComplete();
      onError();
    }
    else
    {
      SceneManager.sceneLoaded += RevertToMainMenu_OnSceneLoaded;
      SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
  }

  private void RevertToMainMenu_OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    SceneManager.sceneLoaded -= RevertToMainMenu_OnSceneLoaded;
    RevertToMainMenu_OnRevertComplete();
  }

  private void RevertToMainMenu_OnRevertComplete()
  {
    loadingGame = false;
    CloseLoadingUI();
  }

  private IEnumerator OpenLoadingUI()
  {
    if (loadingUI == null)
    {
      GameObject gameObject = Instantiate(SRSingleton<GameContext>.Instance.UITemplates.loadingUI);
      loadingUI = gameObject.GetRequiredComponent<LoadingUI>();
      DontDestroyOnLoad(gameObject);
    }
    loadingUI.OnLoadingStart();
    yield return new WaitForEndOfFrame();
  }

  private void CloseLoadingUI()
  {
    if (!(loadingUI != null))
      return;
    Destroyer.Destroy(loadingUI.gameObject, "AutoSaveDirector.CloseLoadingUI");
    loadingUI = null;
  }

  private class LoadNewGameMetadata
  {
    public string displayName;
    public PlayerState.GameMode gameMode;
    public Identifiable.Id gameIconId;
  }
}
