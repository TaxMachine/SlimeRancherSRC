// Decompiled with JetBrains decompiler
// Type: LoadGameUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadGameUI : BaseUI
{
  public GameObject loadGameButtonPrefab;
  public GameObject loadButtonPanel;
  public GameObject mainMenuUIPrefab;
  public GameObject deleteUIPrefab;
  public ScrollRect scroller;
  public TMP_Text status;
  public Button playButton;
  public Button deleteButton;
  public Button backButton;
  public GameObject loadingPanel;
  public GameSummaryPanel summaryPanel;
  public GameObject noSavesPanel;
  private List<Toggle> gameToggles = new List<Toggle>();
  private int selectedIdx;
  private bool settingToggleStates;
  private List<GameData.Summary> availGames = new List<GameData.Summary>();
  private AutoSaveDirector autoSaveDirector;

  public override void Awake()
  {
    base.Awake();
    autoSaveDirector = SRSingleton<GameContext>.Instance.AutoSaveDirector;
    UpdateAvailGames();
  }

  private void UpdateAvailGames()
  {
    foreach (Component gameToggle in gameToggles)
      Destroyer.Destroy(gameToggle.gameObject, "LoadGameUI.UpdateAvailGames");
    gameToggles.Clear();
    noSavesPanel.gameObject.SetActive(true);
    loadingPanel.SetActive(true);
    StartCoroutine(FinishUpdateAvailGames());
  }

  private IEnumerator FinishUpdateAvailGames()
  {
    LoadGameUI loadGameUi = this;
    yield return new WaitForSeconds(0.0f);
    loadGameUi.availGames.Clear();
    foreach (KeyValuePair<string, List<GameData.Summary>> keyValuePair in loadGameUi.autoSaveDirector.AvailableGamesByDisplayName())
      loadGameUi.availGames.Add(keyValuePair.Value[0]);
    loadGameUi.loadingPanel.SetActive(false);
    loadGameUi.summaryPanel.gameObject.SetActive(loadGameUi.availGames.Count > 0);
    loadGameUi.noSavesPanel.gameObject.SetActive(loadGameUi.availGames.Count <= 0);
    foreach (GameData.Summary availGame in loadGameUi.availGames)
    {
      GameObject loadGameButton = loadGameUi.CreateLoadGameButton(availGame);
      loadGameButton.transform.SetParent(loadGameUi.loadButtonPanel.transform, false);
      loadGameUi.gameToggles.Add(loadGameButton.GetComponent<Toggle>());
    }
    if (loadGameUi.gameToggles.Count > 0)
      loadGameUi.gameToggles[0].gameObject.AddComponent<InitSelected>();
    for (int index = 0; index < loadGameUi.gameToggles.Count; ++index)
    {
      Navigation navigation = new Navigation();
      navigation.mode = Navigation.Mode.Explicit;
      if (index > 0)
        navigation.selectOnUp = loadGameUi.gameToggles[index - 1];
      if (index < loadGameUi.gameToggles.Count - 1)
        navigation.selectOnDown = loadGameUi.gameToggles[index + 1];
      loadGameUi.gameToggles[index].navigation = navigation;
      loadGameUi.AddToggleListener(index);
    }
    if (loadGameUi.availGames.Count > 0)
      loadGameUi.SetSelectedIdx(0);
    loadGameUi.StartCoroutine(loadGameUi.ScrollToTop());
  }

  private void AddToggleListener(int idx) => gameToggles[idx].onValueChanged.AddListener(isOn =>
  {
    if (!isOn || settingToggleStates)
      return;
    SetSelectedIdx(idx);
  });

  private IEnumerator ScrollToTop()
  {
    yield return new WaitForEndOfFrame();
    scroller.verticalNormalizedPosition = 1f;
  }

  public void LoadSelectedGame()
  {
    GameSummaryEntry gameSummaryEntry = SelectedGame();
    if (!(gameSummaryEntry != null))
      return;
    LoadGame(gameSummaryEntry.gameName, gameSummaryEntry.saveName);
  }

  public void DeleteSelectedGame()
  {
    GameSummaryEntry gameSummaryEntry = SelectedGame();
    if (!(gameSummaryEntry != null))
      return;
    DeleteGame(gameSummaryEntry.saveName);
  }

  public override void Close() => base.Close();

  private void SetSelectedIdx(int idx)
  {
    selectedIdx = idx;
    try
    {
      settingToggleStates = true;
      gameToggles[idx].Select();
      summaryPanel.Init(availGames[idx]);
      playButton.interactable = !availGames[idx].isInvalid && !availGames[idx].gameOver && !autoSaveDirector.IsLoadingGame();
    }
    finally
    {
      settingToggleStates = false;
    }
  }

  public void SelectNextGame() => SetSelectedIdx(Math.Min(gameToggles.Count - 1, selectedIdx + 1));

  public void SelectPrevGame() => SetSelectedIdx(Math.Max(0, selectedIdx - 1));

  private void DeleteGame(string saveName)
  {
    GameData.Summary gameToDelete = autoSaveDirector.LoadSummary(saveName);
    gameObject.SetActive(false);
    CreateDeleteGameDialog(gameToDelete, () =>
    {
      gameObject.SetActive(true);
      autoSaveDirector.DeleteGame(gameToDelete.name);
    }, () =>
    {
      gameObject.SetActive(true);
      UpdateAvailGames();
    });
  }

  private GameSummaryEntry SelectedGame()
  {
    ToggleGroup component = loadButtonPanel.GetComponent<ToggleGroup>();
    return component.GetActive() != null ? component.GetActive().GetComponent<GameSummaryEntry>() : null;
  }

  private void LoadGame(string gameName, string saveName)
  {
    gameObject.SetActive(false);
    autoSaveDirector.BeginLoad(gameName, saveName, () => gameObject.SetActive(true));
  }

  private GameObject CreateLoadGameButton(GameData.Summary gameSummary)
  {
    GameObject loadGameButton = Instantiate(loadGameButtonPrefab);
    Toggle toggle = loadGameButton.GetComponent<Toggle>();
    toggle.group = loadButtonPanel.GetComponent<ToggleGroup>();
    loadGameButton.GetComponent<GameSummaryEntry>().Init(gameSummary);
    OnSelectDelegator.Create(loadGameButton, () => toggle.isOn = true);
    return loadGameButton;
  }

  private GameObject CreateDeleteGameDialog(
    GameData.Summary gameSummary,
    ConfirmUI.OnConfirm onConfirm,
    OnDestroyDelegate onDestroy)
  {
    GameObject deleteGameDialog = Instantiate(deleteUIPrefab);
    deleteGameDialog.GetComponent<ConfirmUI>().onConfirm = onConfirm;
    deleteGameDialog.GetComponent<ConfirmUI>().onDestroy = onDestroy;
    deleteGameDialog.transform.Find("MainPanel/GameSummaryPanel").GetComponent<GameSummaryPanel>().Init(gameSummary);
    return deleteGameDialog;
  }
}
