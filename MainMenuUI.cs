// Decompiled with JetBrains decompiler
// Type: MainMenuUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuUI : SRBehaviour
{
  public GameObject loadGameUI;
  public GameObject expoSelectGameUI;
  public GameObject newGameUI;
  public GameObject optionsUI;
  public GameObject creditsUI;
  [Tooltip("DLCManageUI prefab.")]
  public GameObject manageDLCUI;
  public GameObject expoModePanel;
  public GameObject standardModePanel;
  public Button continueBtn;
  public Button quitBtn;
  public Button loadBtn;
  public TMP_Text statusText;
  public Button expoQuitBtn;
  [Tooltip("DLC button.")]
  public Button DLCButton;
  public GameObject gdkUserPanel;
  public TMP_Dropdown languageDropdown;
  private MessageBundle uiBundle;
  private const string FORUMS_URL = "http://forums.monomipark.com";
  private const string SUPPORT_URL = "https://support.slimerancher.com/hc";

  public void Awake() => SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(OnBundlesAvailable);

  public void Start()
  {
    Log.Debug("MainMenuUI.Start");
    DLCButton.gameObject.SetActive(DLCManageUI.IsEnabled());
    if (quitBtn != null)
    {
      quitBtn.gameObject.SetActive(true);
      expoQuitBtn.gameObject.SetActive(true);
      gdkUserPanel.gameObject.SetActive(false);
    }
    else
      Log.Debug("quit button was null");
    standardModePanel.SetActive(true);
    expoModePanel.SetActive(false);
    MaybeShowContinue();
  }

  public void OnEnable() => MaybeShowContinue();

  private void MaybeShowContinue()
  {
    if (SRSingleton<GameContext>.Instance.AutoSaveDirector.HasContinue())
    {
      continueBtn.gameObject.SetActive(true);
      InitSelected component = loadBtn.gameObject.GetComponent<InitSelected>();
      if (component != null)
        Destroy(component);
      continueBtn.gameObject.AddComponent<InitSelected>();
    }
    else
    {
      continueBtn.gameObject.SetActive(false);
      InitSelected component = continueBtn.gameObject.GetComponent<InitSelected>();
      if (component != null)
        Destroy(component);
      loadBtn.gameObject.AddComponent<InitSelected>();
    }
  }

  public virtual void OnDestroy()
  {
    if (!(SRSingleton<GameContext>.Instance != null))
      return;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(OnBundlesAvailable);
  }

  public virtual void OnBundlesAvailable(MessageDirector msgDir)
  {
    uiBundle = msgDir.GetBundle("ui");
    SetupLanguages();
  }

  public void OnButtonDLC() => InstantiateAndWaitForDestroy(manageDLCUI);

  public void ContinueGame()
  {
    SetInteractable(false);
    GameData.Summary saveToContinue = SRSingleton<GameContext>.Instance.AutoSaveDirector.GetSaveToContinue();
    SRSingleton<GameContext>.Instance.AutoSaveDirector.BeginLoad(saveToContinue.name, saveToContinue.saveName, () =>
    {
      SetInteractable(true);
      gameObject.SetActive(false);
      gameObject.SetActive(true);
    });
  }

  public void LoadGame() => InstantiateAndWaitForDestroy(loadGameUI);

  public void SelectGame()
  {
    Instantiate(expoSelectGameUI);
    Destroyer.Destroy(gameObject, "MainMenuUI.SelectGame");
  }

  public void NewGame() => InstantiateAndWaitForDestroy(newGameUI);

  public void Quit() => Application.Quit();

  public void Options() => InstantiateAndWaitForDestroy(optionsUI);

  public void Credits() => InstantiateAndWaitForDestroy(creditsUI);

  public void Forums() => Application.OpenURL("http://forums.monomipark.com");

  public void SupportEmail() => Application.OpenURL("https://support.slimerancher.com/hc");

  public GameObject InstantiateAndWaitForDestroy(GameObject prefab)
  {
    GameObject gameObject = Instantiate(prefab);
    BaseUI component = gameObject.GetComponent<BaseUI>();
    this.gameObject.SetActive(false);
    component.onDestroy += () =>
    {
      if (!(this != null) || !(this.gameObject != null))
        return;
      this.gameObject.SetActive(true);
    };
    return gameObject;
  }

  private void SetInteractable(bool interactable)
  {
    foreach (Selectable componentsInChild in GetComponentsInChildren<Selectable>())
      componentsInChild.interactable = interactable;
  }

  private void SetupLanguages() => SetupDropdown(languageDropdown, "l.lang_", lang =>
  {
    CultureInfo culture = SRSingleton<GameContext>.Instance.MessageDirector.GetCulture();
    return Enum.GetName(typeof (MessageDirector.Lang), lang).ToLowerInvariant() == culture.TwoLetterISOLanguageName;
  }, (UnityAction<MessageDirector.Lang>) (lang =>
  {
    SRSingleton<GameContext>.Instance.MessageDirector.SetCulture(lang);
    SRSingleton<GameContext>.Instance.AutoSaveDirector.SaveProfile();
  }));

  private void SetupDropdown<T>(
    TMP_Dropdown dropdown,
    string msgPrefix,
    Predicate<T> isLevel,
    UnityAction<T> assignLevel)
  {
    int num = 0;
    dropdown.options.Clear();
    dropdown.onValueChanged.RemoveAllListeners();
    foreach (T obj in Enum.GetValues(typeof (T)))
    {
      string name = Enum.GetName(typeof (T), obj);
      string text = uiBundle.Xlate(msgPrefix + name.ToLowerInvariant());
      dropdown.options.Add(new TMP_Dropdown.OptionData(text));
      T fLevel = obj;
      int fIdx = num;
      if (isLevel(fLevel))
      {
        dropdown.value = fIdx;
        dropdown.captionText.text = text;
      }
      dropdown.onValueChanged.AddListener(val =>
      {
        if (val != fIdx)
          return;
        assignLevel(fLevel);
      });
      ++num;
    }
    UnityAction<int> call = value =>
    {
      Transform transform = dropdown.transform.Find("Dropdown List");
      if (!(transform != null))
        return;
      Destroyer.Destroy(transform.gameObject, "MainMenuUI.SetupDropdown");
    };
    dropdown.onValueChanged.AddListener(call);
  }
}
