// Decompiled with JetBrains decompiler
// Type: NewGameUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewGameUI : BaseUI
{
  public GameObject mainMenuUIPrefab;
  public InputField gameNameField;
  public Button playButton;
  public Toggle classicToggle;
  public Toggle casualToggle;
  public Toggle timeLimitToggle;
  public ToggleGroup iconGroup;
  public TMP_Text gameModeText;
  public GameObject gameIconPrefab;
  public Button leftIconButton;
  public Button rightIconButton;
  public Identifiable.Id[] availIconIds;
  [Tooltip("TabByMenuKeys attached to the icon selection scrollview.")]
  public TabByMenuKeys iconTabByMenuKeys;
  private PlayerState.GameMode selGameMode;
  private int selIconIdIdx;
  private Toggle[] iconToggles;
  private bool settingToggleStates;
  private const string ERR_EXISTS = "e.game_name_exists";
  private const string ERR_LETTERS_NUMS_ONLY = "e.letters_nums_only";
  private const string ERR_MAX_LENGTH = "e.max_length";
  private const int GAME_NAME_MAX_LENGTH = 24;
  private AutoSaveDirector autoSaveDirector;
  private GameObject waitForErrorDialog;

  public override void Awake()
  {
    base.Awake();
    autoSaveDirector = SRSingleton<GameContext>.Instance.AutoSaveDirector;
  }

  public void Start()
  {
    MessageBundle bundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui");
    HashSet<string> gameDisplayNames = GetAvailableGameDisplayNames();
    for (int index = 1; index < 1000; ++index)
    {
      string str = bundle.Get("m.default_game_name", index);
      if (!gameDisplayNames.Contains(str))
      {
        gameNameField.text = str;
        break;
      }
    }
    classicToggle.onValueChanged.AddListener(isOn =>
    {
      if (!isOn)
        return;
      SetGameMode(PlayerState.GameMode.CLASSIC);
    });
    casualToggle.onValueChanged.AddListener(isOn =>
    {
      if (!isOn)
        return;
      SetGameMode(PlayerState.GameMode.CASUAL);
    });
    timeLimitToggle.onValueChanged.AddListener(isOn =>
    {
      if (!isOn)
        return;
      SetGameMode(PlayerState.GameMode.TIME_LIMIT_V2);
    });
    SetGameMode(PlayerState.GameMode.CLASSIC);
    iconToggles = new Toggle[availIconIds.Length];
    bool flag = true;
    LookupDirector lookupDirector = SRSingleton<GameContext>.Instance.LookupDirector;
    for (int index = 0; index < availIconIds.Length; ++index)
    {
      Identifiable.Id availIconId = availIconIds[index];
      GameObject gameObject = Instantiate(gameIconPrefab);
      gameObject.transform.SetParent(iconGroup.transform, false);
      Toggle toggle = gameObject.GetComponent<Toggle>();
      gameObject.transform.Find("GameIcon").GetComponent<Image>().sprite = lookupDirector.GetIcon(availIconId);
      toggle.group = iconGroup;
      iconToggles[index] = toggle;
      int idxToSet = index;
      toggle.onValueChanged.AddListener(isOn =>
      {
        if (!isOn || settingToggleStates)
          return;
        SetIconIdIdx(idxToSet);
      });
      OnSelectDelegator.Create(toggle.gameObject, () => toggle.isOn = true);
      if (flag)
      {
        flag = false;
        toggle.isOn = true;
      }
    }
  }

  private HashSet<string> GetAvailableGameDisplayNames() => new HashSet<string>(autoSaveDirector.AvailableGamesByDisplayName().Keys);

  public void PlayNewGame()
  {
    string text = gameNameField.text;
    if (!autoSaveDirector.DisplayNameAvailable(text))
      waitForErrorDialog = SRSingleton<GameContext>.Instance.UITemplates.CreateErrorDialog("e.game_name_exists");
    else if (text.Length > 24 || text.Length < 1)
    {
      waitForErrorDialog = SRSingleton<GameContext>.Instance.UITemplates.CreateErrorDialogWithArgs("e.max_length", 24);
    }
    else
    {
      playButton.interactable = false;
      gameObject.SetActive(false);
      SRSingleton<GameContext>.Instance.AutoSaveDirector.LoadNewGame(text, GetGameIconId(), GetGameMode(), () =>
      {
        playButton.interactable = true;
        gameObject.SetActive(true);
      });
    }
  }

  protected override bool Closeable() => waitForErrorDialog == null;

  private PlayerState.GameMode GetGameMode() => selGameMode;

  private void SetGameMode(PlayerState.GameMode mode)
  {
    selGameMode = mode;
    MessageBundle bundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui");
    if (mode == PlayerState.GameMode.TIME_LIMIT_V2)
    {
      int? stat = SRSingleton<SceneContext>.Instance.AchievementsDirector.GetStat(AchievementsDirector.IntStat.TIME_LIMIT_V2_CURRENCY);
      string key = string.Format("m.desc.gamemode_{0}{1}", mode.ToString().ToLowerInvariant(), !stat.HasValue ? string.Empty : (object) "_high_score");
      gameModeText.text = bundle.Get(key, stat);
    }
    else
    {
      string key = string.Format("m.desc.gamemode_{0}", mode.ToString().ToLowerInvariant());
      gameModeText.text = bundle.Get(key);
    }
  }

  private void SetIconIdIdx(int idx)
  {
    selIconIdIdx = idx;
    try
    {
      settingToggleStates = true;
      iconToggles[idx].isOn = true;
      leftIconButton.interactable = idx > 0;
      rightIconButton.interactable = idx < iconToggles.Length - 1;
      iconTabByMenuKeys.RecalcSelected();
    }
    finally
    {
      settingToggleStates = false;
    }
  }

  private Identifiable.Id GetGameIconId() => availIconIds[selIconIdIdx];

  public override void Close() => base.Close();

  public void SelectNextIcon() => SetIconIdIdx(Math.Min(availIconIds.Length - 1, selIconIdIdx + 1));

  public void SelectPrevIcon() => SetIconIdIdx(Math.Max(0, selIconIdIdx - 1));

  public override void Update()
  {
    base.Update();
    iconTabByMenuKeys.enabled = !gameNameField.isFocused || InputDirector.UsingGamepad();
  }
}
