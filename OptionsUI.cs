// Decompiled with JetBrains decompiler
// Type: OptionsUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionsUI : BaseUI
{
  public GameObject videoPanel;
  public GameObject audioPanel;
  public GameObject inputPanel;
  public GameObject gamepadPanel;
  public GameObject modsPanel;
  public GameObject otherPanel;
  public GameObject tabsPanel;
  public GameObject bindingLinePrefab;
  public GameObject bindingsPanel;
  public GameObject title;
  public Toggle disableCameraBobToggle;
  public TMP_Dropdown tutorialsDropdown;
  public Toggle bufferForGifToggle;
  public Toggle vacLockToggle;
  public Toggle sprintHoldToggle;
  public SECTR_AudioBus masterBus;
  public SECTR_AudioBus musicBus;
  public SECTR_AudioBus sfxBus;
  public Slider masterSlider;
  public Slider musicSlider;
  public Slider sfxSlider;
  public Slider sensitivitySlider;
  public GameObject modTogglePrefab;
  public RectTransform modListPanel;
  public TMP_Dropdown overallDropdown;
  public TMP_Dropdown lightingDropdown;
  public TMP_Dropdown shadowsDropdown;
  public TMP_Dropdown texturesDropdown;
  public TMP_Dropdown particlesDropdown;
  public TMP_Dropdown modelDetailDropdown;
  public TMP_Dropdown waterDetailDropdown;
  public TMP_Dropdown antialiasingDropdown;
  public Toggle ambientOcclusionToggle;
  public Toggle bloomToggle;
  public Slider fovSlider;
  public Slider otherTabFovSlider;
  public TMP_Text fovValText;
  public TMP_Text otherTabFovValText;
  public GameObject otherTabFovRow;
  public Slider overscanSlider;
  public TMP_Text overscanValText;
  public GameObject overscanFovRow;
  public Button resetProfileButton;
  public Toggle enableVsyncToggle;
  public Toggle enableVsyncOtherRow;
  public Toggle enableVsyncVideoToggle;
  public TMP_Dropdown resolutionDropdown;
  public Toggle fullscreenToggle;
  public Button resolutionApplyButton;
  public Toggle disableGamepadToggle;
  public Toggle swapSticksToggle;
  public Toggle invertGamepadLookYToggle;
  public Toggle invertMouseLookYToggle;
  public Toggle disableMouseLookSmooth;
  public Toggle showMinimalHUDToggle;
  public Button defaultKeyBtn;
  public Button defaultGamepadBtn;
  public GameObject confirmResolutionDialogPrefab;
  public GameObject videoTab;
  public GameObject audioTab;
  public GameObject inputTab;
  public GameObject gamepadTab;
  public XlateText gamepadTabText;
  public GameObject modsTab;
  public GameObject otherTab;
  public TMP_Dropdown languageDropdown;
  private bool initializing;
  private List<Resolution> dropdownResolutions = new List<Resolution>();
  private Dictionary<BindingLineUI, string> labelKeyDict = new Dictionary<BindingLineUI, string>();
  private bool preventClosing;
  private OptionsDirector optionsDirector;
  public const int MIN_WIDTH = 800;
  public const int MIN_HEIGHT = 600;
  private OnQualityChanged onQualityChanged;
  private bool notifyingQualityChanged;

  public bool IsInitialzing => initializing;

  public override void Awake()
  {
    optionsDirector = SRSingleton<GameContext>.Instance.OptionsDirector;
    base.Awake();
    modsTab.SetActive(false);
    title.GetComponent<XlateText>().key = "t.options";
    gamepadPanel.SetActive(false);
    tabsPanel.SetActive(true);
    videoPanel.SetActive(true);
    videoTab.SetActive(true);
    inputTab.SetActive(true);
    SelectVideoTab();
    bufferForGifToggle.gameObject.SetActive(true);
    otherTabFovRow.SetActive(false);
    otherTabFovSlider.gameObject.SetActive(false);
    enableVsyncOtherRow.gameObject.SetActive(false);
    enableVsyncToggle.gameObject.SetActive(false);
    SetupVertNav(languageDropdown, tutorialsDropdown, disableCameraBobToggle, showMinimalHUDToggle, bufferForGifToggle, vacLockToggle, sprintHoldToggle, enableVsyncToggle, otherTabFovSlider, overscanSlider, resetProfileButton);
    SRInput.Actions.ListenOptions.OnBindingAdded += OnBindingAdded;
    SRInput.Actions.ListenOptions.OnBindingRejected += OnBindingRejected;
  }

  private void OnBindingAdded(PlayerAction action, BindingSource binding) => RefreshBindings();

  private void OnBindingRejected(
    PlayerAction action,
    BindingSource binding,
    BindingSourceRejectionType rejection)
  {
    RefreshBindings();
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    SRInput.Actions.ListenOptions.OnBindingAdded -= OnBindingAdded;
    SRInput.Actions.ListenOptions.OnBindingRejected -= OnBindingRejected;
  }

  private void SetupOptionsUI()
  {
    initializing = true;
    SetupOtherOptions();
    SetupMods();
    SetupVideoSettings();
    SetupAudio();
    SetupInput();
    SetupLanguages();
    initializing = false;
  }

  private void SetupAudio()
  {
    masterSlider.value = masterBus.UserVolume;
    musicSlider.value = musicBus.UserVolume;
    sfxSlider.value = sfxBus.UserVolume;
    sensitivitySlider.value = SRSingleton<GameContext>.Instance.InputDirector.MouseLookSensitivity;
  }

  public override void OnBundlesAvailable(MessageDirector msgDir)
  {
    base.OnBundlesAvailable(msgDir);
    SetupOptionsUI();
  }

  private void SetupVideoSettings()
  {
    SetupDropdown(overallDropdown, "l.quality_", level => level == SRQualitySettings.CurrentLevel, (UnityAction<SRQualitySettings.Level>) (level => SRQualitySettings.CurrentLevel = level));
    SetupDropdown(lightingDropdown, "l.lighting_", level => level == SRQualitySettings.Lighting, (UnityAction<SRQualitySettings.LightingLevel>) (level => SRQualitySettings.Lighting = level));
    SetupDropdown(shadowsDropdown, "l.shadows_", level => level == SRQualitySettings.Shadows, (UnityAction<SRQualitySettings.ShadowsLevel>) (level => SRQualitySettings.Shadows = level));
    SetupDropdown(texturesDropdown, "l.textures_", level => level == SRQualitySettings.Textures, (UnityAction<SRQualitySettings.TextureLevel>) (level => SRQualitySettings.Textures = level));
    SetupDropdown(particlesDropdown, "l.particles_", level => level == SRQualitySettings.Particles, (UnityAction<SRQualitySettings.ParticlesLevel>) (level => SRQualitySettings.Particles = level));
    SetupDropdown(modelDetailDropdown, "l.model_detail_", level => level == SRQualitySettings.ModelDetail, (UnityAction<SRQualitySettings.ModelDetailLevel>) (level => SRQualitySettings.ModelDetail = level));
    SetupDropdown(waterDetailDropdown, "l.water_detail_", level => level == SRQualitySettings.WaterDetail, (UnityAction<SRQualitySettings.WaterDetailLevel>) (level => SRQualitySettings.WaterDetail = level));
    SetupDropdown(antialiasingDropdown, "l.antialiasing_", level => level == SRQualitySettings.Antialiasing, (UnityAction<SRQualitySettings.AntialiasingMode>) (level => SRQualitySettings.Antialiasing = level));
    ambientOcclusionToggle.isOn = SRQualitySettings.AmbientOcclusion;
    bloomToggle.isOn = SRQualitySettings.Bloom;
    onQualityChanged += () =>
    {
      ambientOcclusionToggle.isOn = SRQualitySettings.AmbientOcclusion;
      bloomToggle.isOn = SRQualitySettings.Bloom;
    };
    fullscreenToggle.isOn = Screen.fullScreen;
    fovSlider.value = optionsDirector.GetFOV();
    fovValText.text = Mathf.RoundToInt(fovSlider.value).ToString();
    otherTabFovSlider.value = optionsDirector.GetFOV();
    otherTabFovValText.text = Mathf.RoundToInt(otherTabFovSlider.value).ToString();
    overscanSlider.value = optionsDirector.GetOverscanAdjustment() * 100f;
    overscanValText.text = Mathf.RoundToInt(overscanSlider.value).ToString();
    enableVsyncToggle.isOn = optionsDirector.enableVsync;
    enableVsyncVideoToggle.isOn = optionsDirector.enableVsync;
    int num1 = 0;
    Resolution currentResolution = Screen.currentResolution with
    {
      width = Screen.width,
      height = Screen.height
    };
    Log.Debug("Current resolution", "height", currentResolution.height, "width", currentResolution.width, "refreshRate", currentResolution.refreshRate, "fullScreenMode", Screen.fullScreenMode);
    int num2 = 0;
    int num3 = 0;
    foreach (Resolution resolution in Screen.resolutions)
    {
      if (num2 != resolution.height || num3 != resolution.width)
      {
        bool flag = currentResolution.width == resolution.width && currentResolution.height == resolution.height;
        num2 = resolution.height;
        num3 = resolution.width;
        if (flag || resolution.width >= 800 && resolution.height >= 600)
        {
          string text = uiBundle.Get("m.resolution", resolution.width, resolution.height);
          resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(text));
          if (flag)
          {
            resolutionDropdown.value = num1;
            resolutionDropdown.captionText.text = text;
          }
          dropdownResolutions.Add(resolution);
          ++num1;
        }
      }
    }
    if (!Application.isEditor)
      return;
    resolutionDropdown.interactable = false;
    resolutionDropdown.captionText.text = "Disabled in Editor";
    fullscreenToggle.interactable = false;
    resolutionApplyButton.interactable = false;
    overallDropdown.navigation = overallDropdown.navigation with
    {
      selectOnUp = null
    };
  }

  private void SetupLanguages() => SetupDropdown(languageDropdown, "l.lang_", lang =>
  {
    CultureInfo culture = SRSingleton<GameContext>.Instance.MessageDirector.GetCulture();
    return Enum.GetName(typeof (MessageDirector.Lang), lang).ToLowerInvariant() == culture.TwoLetterISOLanguageName;
  }, (UnityAction<MessageDirector.Lang>) (lang => SRSingleton<GameContext>.Instance.MessageDirector.SetCulture(lang)));

  private void SetupOtherOptions()
  {
    disableCameraBobToggle.isOn = optionsDirector.disableCameraBob;
    bufferForGifToggle.isOn = optionsDirector.bufferForGif;
    vacLockToggle.isOn = optionsDirector.vacLockOnHold;
    sprintHoldToggle.isOn = optionsDirector.sprintHold;
    showMinimalHUDToggle.isOn = optionsDirector.GetShowMinimalHUD();
    SetupDropdown(tutorialsDropdown, "l.tutorials.", value => optionsDirector.enabledTutorials == value, (UnityAction<OptionsDirector.EnabledTutorials>) (value => optionsDirector.enabledTutorials = value));
  }

  private void SetupInput()
  {
    for (int index = 0; index < bindingsPanel.transform.childCount; ++index)
      Destroyer.Destroy(bindingsPanel.transform.GetChild(index).gameObject, "OptionsUI.SetupInput");
    CreateKeyBindingLine("key.forward", SRInput.Actions.verticalPos);
    CreateKeyBindingLine("key.left", SRInput.Actions.horizontalNeg);
    CreateKeyBindingLine("key.back", SRInput.Actions.verticalNeg);
    CreateKeyBindingLine("key.right", SRInput.Actions.horizontalPos);
    CreateKeyBindingLine("key.shoot", SRInput.Actions.attack);
    CreateKeyBindingLine("key.vac", SRInput.Actions.vac);
    CreateKeyBindingLine("key.burst", SRInput.Actions.burst);
    CreateKeyBindingLine("key.jump", SRInput.Actions.jump);
    CreateKeyBindingLine("key.run", SRInput.Actions.run);
    CreateKeyBindingLine("key.interact", SRInput.Actions.interact);
    CreateKeyBindingLine("key.gadgetMode", SRInput.Actions.toggleGadgetMode);
    CreateKeyBindingLine("key.flashlight", SRInput.Actions.light);
    CreateKeyBindingLine("key.radar", SRInput.Actions.radarToggle);
    CreateKeyBindingLine("key.map", SRInput.Actions.openMap);
    CreateKeyBindingLine("key.slot_1", SRInput.Actions.slot1);
    CreateKeyBindingLine("key.slot_2", SRInput.Actions.slot2);
    CreateKeyBindingLine("key.slot_3", SRInput.Actions.slot3);
    CreateKeyBindingLine("key.slot_4", SRInput.Actions.slot4);
    CreateKeyBindingLine("key.slot_5", SRInput.Actions.slot5);
    CreateKeyBindingLine("key.prev_slot", SRInput.Actions.prevSlot);
    CreateKeyBindingLine("key.next_slot", SRInput.Actions.nextSlot);
    CreateKeyBindingLine("key.reportissue", SRInput.Actions.reportIssue);
    CreateKeyBindingLine("key.screenshot", SRInput.Actions.screenshot);
    CreateKeyBindingLine("key.recordgif", SRInput.Actions.recordGif);
    CreateKeyBindingLine("key.pedia", SRInput.Actions.pedia);
    Button[] componentsInChildren = bindingsPanel.GetComponentsInChildren<Button>(true);
    for (int index = 0; index < componentsInChildren.Length; index += 2)
    {
      Navigation navigation = new Navigation();
      navigation.mode = Navigation.Mode.Explicit;
      navigation.selectOnRight = componentsInChildren[index + 1];
      if (index < componentsInChildren.Length - 2)
      {
        navigation.selectOnDown = componentsInChildren[index + 2];
      }
      else
      {
        navigation.selectOnDown = sensitivitySlider;
        sensitivitySlider.navigation = sensitivitySlider.navigation with
        {
          mode = Navigation.Mode.Explicit,
          selectOnUp = componentsInChildren[index]
        };
      }
      if (index > 0)
      {
        navigation.selectOnUp = componentsInChildren[index - 2];
      }
      else
      {
        navigation.selectOnUp = defaultKeyBtn;
        defaultKeyBtn.navigation = defaultKeyBtn.navigation with
        {
          mode = Navigation.Mode.Explicit,
          selectOnDown = componentsInChildren[index]
        };
      }
      componentsInChildren[index].navigation = navigation;
      componentsInChildren[index + 1].navigation = new Navigation()
      {
        mode = Navigation.Mode.Explicit,
        selectOnLeft = componentsInChildren[index],
        selectOnDown = index >= componentsInChildren.Length - 2 ? sensitivitySlider : componentsInChildren[index + 3],
        selectOnUp = index <= 0 ? defaultKeyBtn : (Selectable) componentsInChildren[index - 1]
      };
    }
  }

  private void SetupMods()
  {
    foreach (Component componentsInChild in modListPanel.GetComponentsInChildren<Toggle>(true))
      Destroyer.Destroy(componentsInChild.gameObject, "OptionsUI.SetupMods");
    foreach (ModDirector.Mod mod in Enum.GetValues(typeof (ModDirector.Mod)))
      CreateModToggle(mod);
    Toggle[] componentsInChildren = modListPanel.GetComponentsInChildren<Toggle>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      Navigation navigation = new Navigation();
      navigation.mode = Navigation.Mode.Explicit;
      if (index < componentsInChildren.Length - 1)
        navigation.selectOnDown = componentsInChildren[index + 1];
      if (index > 0)
        navigation.selectOnUp = componentsInChildren[index - 1];
      componentsInChildren[index].navigation = navigation;
    }
    invertMouseLookYToggle.isOn = SRSingleton<GameContext>.Instance.InputDirector.GetInvertMouseLookY();
    disableMouseLookSmooth.isOn = SRSingleton<GameContext>.Instance.InputDirector.GetDisableMouseLookSmooth();
    if (componentsInChildren.Length == 0)
      return;
    componentsInChildren[0].gameObject.AddComponent<InitSelected>();
  }

  public void RefreshBindings()
  {
    if (initializing)
      return;
    foreach (BindingLineUI componentsInChild in GetComponentsInChildren<BindingLineUI>())
      componentsInChild.Refresh();
    if (!(gamepadPanel != null))
      return;
    gamepadPanel.GetComponent<GamepadPanel>().RefreshBindings();
  }

  private GameObject CreateKeyBindingLine(string label, PlayerAction action)
  {
    GameObject bindingLineObj = Instantiate(bindingLinePrefab);
    bindingLineObj.transform.SetParent(bindingsPanel.transform, false);
    BindingPanel.CreateBindingLine(label, action, bindingLineObj, uiBundle, labelKeyDict, null);
    return bindingLineObj;
  }

  public void ToggleDisableCameraBob() => optionsDirector.disableCameraBob = disableCameraBobToggle.isOn;

  public void ToggleEnableVsync()
  {
    optionsDirector.enableVsync = enableVsyncToggle.isOn;
    optionsDirector.UpdateVsync();
  }

  public void ToggleEnableVsyncVideo()
  {
    optionsDirector.enableVsync = enableVsyncVideoToggle.isOn;
    optionsDirector.UpdateVsync();
  }

  public void ToggleBufferForGif() => optionsDirector.bufferForGif = bufferForGifToggle.isOn;

  public void ToggleVacLock() => optionsDirector.vacLockOnHold = vacLockToggle.isOn;

  public void ToggleSprintHold() => optionsDirector.sprintHold = sprintHoldToggle.isOn;

  public override void Close()
  {
    base.Close();
    SRInput.AddOrReplaceBinding(SRInput.PauseActions.closeMap, SRInput.Actions.openMap);
    SRSingleton<SceneContext>.Instance.TutorialDirector.MaybeShowStatusTutorials();
    SRSingleton<GameContext>.Instance.AutoSaveDirector.SaveProfile();
  }

  private void DeselectAll()
  {
    videoPanel.SetActive(false);
    audioPanel.SetActive(false);
    inputPanel.SetActive(false);
    gamepadPanel.SetActive(false);
    modsPanel.SetActive(false);
    otherPanel.SetActive(false);
  }

  public void SelectVideoTab()
  {
    DeselectAll();
    videoPanel.SetActive(true);
  }

  public void SelectAudioTab()
  {
    DeselectAll();
    audioPanel.SetActive(true);
  }

  public void SelectInputTab()
  {
    DeselectAll();
    inputPanel.SetActive(true);
  }

  public void SelectGamepadTab()
  {
    DeselectAll();
    gamepadPanel.SetActive(true);
  }

  public void SelectModsTab()
  {
    DeselectAll();
    modsPanel.SetActive(true);
  }

  public void SelectOtherTab()
  {
    DeselectAll();
    otherPanel.SetActive(true);
  }

  public void OnAudioLevelsChanged()
  {
    if (initializing)
      return;
    masterBus.UserVolume = masterSlider.value;
    musicBus.UserVolume = musicSlider.value;
    sfxBus.UserVolume = sfxSlider.value;
  }

  public void OnSensitivityChanged() => SRSingleton<GameContext>.Instance.InputDirector.MouseLookSensitivity = sensitivitySlider.value;

  public void OnAmbientOcclusionChanged()
  {
    SRQualitySettings.AmbientOcclusion = ambientOcclusionToggle.isOn;
    onQualityChanged();
  }

  public void OnBloomChanged()
  {
    SRQualitySettings.Bloom = bloomToggle.isOn;
    onQualityChanged();
  }

  public void OnFOVChanged()
  {
    optionsDirector.SetFOV(fovSlider.value);
    fovValText.text = Mathf.RoundToInt(fovSlider.value).ToString();
  }

  public void OnOtherTabFOVChanged()
  {
    optionsDirector.SetFOV(otherTabFovSlider.value);
    otherTabFovValText.text = Mathf.RoundToInt(otherTabFovSlider.value).ToString();
  }

  public void OnOverscanAdjustmentChanged()
  {
    optionsDirector.SetOverscanAdjustment(Mathf.Clamp(overscanSlider.value, 0.0f, 15f) * 0.01f);
    overscanValText.text = Mathf.RoundToInt(overscanSlider.value).ToString();
  }

  public void OnApplyResolution()
  {
    CreateConfirmResolutionDialog();
    Resolution dropdownResolution = dropdownResolutions[resolutionDropdown.value];
    optionsDirector.SetScreenResolution(dropdownResolution.width, dropdownResolution.height, fullscreenToggle.isOn);
  }

  public void ToggleInvertMouseLookY() => SRSingleton<GameContext>.Instance.InputDirector.SetInvertMouseLookY(invertMouseLookYToggle.isOn);

  public void ToggleDisableMouseLookSmooth() => SRSingleton<GameContext>.Instance.InputDirector.SetDisableMouseLookSmooth(disableMouseLookSmooth.isOn);

  public void ToggleShowMinimalHUD() => SRSingleton<GameContext>.Instance.OptionsDirector.SetShowMinimalHUD(showMinimalHUDToggle.isOn);

  public void ResetKeyMouseDefaults()
  {
    SRSingleton<GameContext>.Instance.InputDirector.ResetKeyMouseDefaults();
    RefreshBindings();
  }

  public void ResetProfile() => SRSingleton<GameContext>.Instance.UITemplates.CreateConfirmDialog("m.confirm_reset_profile", () =>
  {
    Close();
    SRSingleton<GameContext>.Instance.AutoSaveDirector.ResetProfile();
  });

  private void CreateModToggle(ModDirector.Mod mod)
  {
    GameObject gameObject = Instantiate(modTogglePrefab);
    gameObject.transform.SetParent(modListPanel, false);
    Toggle component = gameObject.GetComponent<Toggle>();
    component.isOn = SRSingleton<SceneContext>.Instance.ModDirector.IsModActive(mod);
    gameObject.transform.Find("Label").GetComponent<TMP_Text>().text = uiBundle.Get("l.mod_" + Enum.GetName(typeof (ModDirector.Mod), mod).ToLowerInvariant());
    component.onValueChanged.AddListener(selected =>
    {
      if (selected)
        SRSingleton<SceneContext>.Instance.ModDirector.ActivateMod(mod);
      else
        SRSingleton<SceneContext>.Instance.ModDirector.DeactivateMod(mod);
    });
  }

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
      string levelMsg = uiBundle.Xlate(msgPrefix + name.ToLowerInvariant());
      dropdown.options.Add(new TMP_Dropdown.OptionData(levelMsg));
      T fLevel = obj;
      int fIdx = num;
      OnQualityChanged onQualityChanged = () =>
      {
        if (!isLevel(fLevel))
          return;
        dropdown.value = fIdx;
        dropdown.captionText.text = levelMsg;
      };
      this.onQualityChanged += onQualityChanged;
      onQualityChanged();
      dropdown.onValueChanged.AddListener(val =>
      {
        if (val != fIdx)
          return;
        assignLevel(fLevel);
        NotifyQualityChanged();
      });
      ++num;
    }
  }

  private void NotifyQualityChanged()
  {
    if (notifyingQualityChanged)
      return;
    try
    {
      notifyingQualityChanged = true;
      onQualityChanged();
    }
    finally
    {
      notifyingQualityChanged = false;
    }
  }

  private GameObject CreateConfirmResolutionDialog()
  {
    Resolution oldRes = new Resolution();
    oldRes.width = Screen.width;
    oldRes.height = Screen.height;
    bool oldFullscreen = Screen.fullScreen;
    oldRes.refreshRate = Screen.currentResolution.refreshRate;
    PreventClosing(true);
    ConfirmResolutionUI.OnCancel onCancel = () =>
    {
      optionsDirector.SetScreenResolution(oldRes.width, oldRes.height, oldFullscreen);
      fullscreenToggle.isOn = Screen.fullScreen;
      for (int index = 0; index < dropdownResolutions.Count; ++index)
      {
        Resolution dropdownResolution = dropdownResolutions[index];
        if ((oldRes.width != dropdownResolution.width ? 0 : (oldRes.height == dropdownResolution.height ? 1 : 0)) != 0)
        {
          resolutionDropdown.value = index;
          resolutionDropdown.captionText.text = uiBundle.Get("m.resolution", dropdownResolution.width, dropdownResolution.height);
        }
      }
      resolutionApplyButton.Select();
      PreventClosing(false);
    };
    ConfirmResolutionUI.OnConfirm onConfirm = () =>
    {
      resolutionApplyButton.Select();
      PreventClosing(false);
    };
    GameObject resolutionDialog = Instantiate(confirmResolutionDialogPrefab);
    resolutionDialog.GetComponent<ConfirmResolutionUI>().onCancel = onCancel;
    resolutionDialog.GetComponent<ConfirmResolutionUI>().onConfirm = onConfirm;
    return resolutionDialog;
  }

  protected override bool Closeable() => base.Closeable() && !preventClosing;

  public void PreventClosing(bool prevent) => preventClosing = prevent;

  private void SetupVertNav(params Selectable[] selectables)
  {
    List<Selectable> selectableList = new List<Selectable>();
    foreach (Selectable selectable in selectables)
    {
      if (selectable.gameObject.activeSelf)
        selectableList.Add(selectable);
    }
    for (int index = 0; index < selectableList.Count; ++index)
    {
      Navigation navigation = selectableList[index].navigation with
      {
        mode = Navigation.Mode.Explicit,
        selectOnUp = index != 0 ? selectableList[index - 1] : null,
        selectOnDown = index != selectableList.Count - 1 ? selectableList[index + 1] : null
      };
      selectableList[index].navigation = navigation;
    }
    if (selectableList.Count <= 0)
      return;
    selectableList[0].gameObject.AddComponent<InitSelected>();
  }

  private delegate void OnQualityChanged();
}
