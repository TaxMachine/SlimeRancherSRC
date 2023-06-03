// Decompiled with JetBrains decompiler
// Type: SlimeAppearanceUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlimeAppearanceUI : BaseUI
{
  public GameObject contentPanel;
  public GameObject placeholderPanel;
  public GameObject buttonListPanel;
  public GameObject appearancePanel;
  public GameObject tutorialPanel;
  public Button showTutorialButton;
  public GameObject buttonListItemPrefab;
  public SECTR_AudioBus sfxBus;
  public SECTR_AudioCue openCue;
  public SECTR_AudioCue closeCue;
  public Vector2 confirmCueVolumeRange = Vector2.one;
  public SlimeDefinitions slimeDefinitions;
  public SlimeAppearanceDirector slimeAppearanceDirector;
  public XlateText appearanceOneNameText;
  public XlateText appearanceTwoNameText;
  public Button toggleButton;
  public Button selectAreaOne;
  public Button selectAreaTwo;
  public GameObject slimeAppearanceCarouselPrefab;
  public SECTR_AudioCue tabbySelectionCue;
  public SECTR_AudioCue saberSelectionCue;
  public int confirmCueMaxInstances = 5;
  private readonly HashSet<Identifiable.Id> tabbySoundSlimes = new HashSet<Identifiable.Id>()
  {
    Identifiable.Id.TABBY_SLIME,
    Identifiable.Id.LUCKY_SLIME,
    Identifiable.Id.HUNTER_SLIME
  };
  private GameObject slimeAppearanceCarouselCamSetup;
  private SlimeAppearanceCarousel slimeAppearanceCarousel;
  private PediaDirector pediaDirector;
  private TutorialDirector tutorialDirector;
  private SlimeDefinition currentSlime;
  private SECTR_AudioCue confirmCue;
  private bool tutorialOnStack;

  public static bool IsEnabled() => SRSingleton<GameContext>.Instance.DLCDirector.IsPackageInstalledAndEnabled(DLCPackage.Id.SECRET_STYLE);

  public override void Awake()
  {
    base.Awake();
    contentPanel.SetActive(false);
    placeholderPanel.SetActive(true);
    tutorialDirector = SRSingleton<SceneContext>.Instance.TutorialDirector;
    pediaDirector = SRSingleton<SceneContext>.Instance.PediaDirector;
    if (!tutorialDirector.IsCompletedOrDisabled(TutorialDirector.Id.APPEARANCE_UI))
      ShowTutorial(false);
    else
      HideTutorial();
    slimeAppearanceCarouselCamSetup = Instantiate(slimeAppearanceCarouselPrefab);
    slimeAppearanceCarousel = slimeAppearanceCarouselCamSetup.GetComponentInChildren<SlimeAppearanceCarousel>();
    toggleButton.onClick.AddListener(() =>
    {
      if (slimeAppearanceDirector.GetChosenSlimeAppearance(currentSlime) == currentSlime.Appearances.ElementAt(0))
      {
        slimeAppearanceCarousel.ConfirmSlimeAppearance(1);
        SetAppearanceSelectableStates(true, false);
      }
      else
      {
        slimeAppearanceCarousel.ConfirmSlimeAppearance(0);
        SetAppearanceSelectableStates(false, true);
      }
    });
    selectAreaOne.onClick.AddListener(() =>
    {
      slimeAppearanceCarousel.ConfirmSlimeAppearance(0);
      SetAppearanceSelectableStates(false, true);
    });
    selectAreaTwo.onClick.AddListener(() =>
    {
      slimeAppearanceCarousel.ConfirmSlimeAppearance(1);
      SetAppearanceSelectableStates(true, false);
    });
    slimeAppearanceCarousel.onSlimeAppearanceConfirmed += (definition, appearance) =>
    {
      Play(confirmCue);
      slimeAppearanceDirector.UpdateChosenSlimeAppearance(definition, appearance);
    };
    List<SlimeDefinition> list = slimeDefinitions.Slimes.Where(ShouldShowSlimeInList).ToList();
    PediaSortSlimes(list);
    bool flag = false;
    for (int index = 0; index < list.Count; ++index)
    {
      SlimeDefinition slime = list[index];
      GameObject gameObject = AddButton(slime);
      if (!flag && IsSlimeAppearanceMenuUnlocked(slime))
      {
        gameObject.AddComponent<InitSelected>();
        flag = true;
      }
    }
  }

  private void SetAppearanceSelectableStates(bool appearanceOne, bool appearanceTwo)
  {
    selectAreaOne.interactable = appearanceOne;
    selectAreaTwo.interactable = appearanceTwo;
  }

  private void PediaSortSlimes(List<SlimeDefinition> slimes) => slimes.Sort((slimeOne, slimeTwo) => pediaDirector.GetPediaId(slimeOne.IdentifiableId).Value.CompareTo(pediaDirector.GetPediaId(slimeTwo.IdentifiableId).Value));

  private bool ShouldShowSlimeInList(SlimeDefinition slime) => !slime.IsLargo && slime.Appearances.Count() > 1;

  private bool IsSlimeAppearanceMenuUnlocked(SlimeDefinition slime) => slimeAppearanceDirector.GetUnlockedAppearances(slime).Count() > 1;

  public void OnEnable() => Play(openCue);

  public void OnDisable()
  {
    Destroy(slimeAppearanceCarouselCamSetup);
    Play(closeCue);
  }

  public void ShowTutorial(bool onTop)
  {
    appearancePanel.SetActive(false);
    tutorialPanel.SetActive(true);
    showTutorialButton.gameObject.SetActive(false);
    tutorialOnStack = onTop;
  }

  public void HideTutorial()
  {
    tutorialDirector.MarkTutorialCompleted(TutorialDirector.Id.APPEARANCE_UI);
    appearancePanel.SetActive(true);
    tutorialPanel.SetActive(false);
    showTutorialButton.gameObject.SetActive(true);
    tutorialOnStack = false;
  }

  private GameObject AddButton(SlimeDefinition slime)
  {
    GameObject button = CreateButton(slime);
    button.transform.SetParent(buttonListPanel.transform, false);
    button.GetComponent<Toggle>().group = buttonListPanel.GetComponent<ToggleGroup>();
    return button;
  }

  private GameObject CreateButton(SlimeDefinition slime)
  {
    GameObject buttonObj = Instantiate(buttonListItemPrefab);
    Toggle component1 = buttonObj.GetComponent<Toggle>();
    TMP_Text component2 = buttonObj.transform.Find("Name").gameObject.GetComponent<TMP_Text>();
    Image component3 = buttonObj.transform.Find("Icon").gameObject.GetComponent<Image>();
    if (IsSlimeAppearanceMenuUnlocked(slime))
    {
      component2.text = Identifiable.GetName(slime.IdentifiableId);
      component3.sprite = slime.Appearances.First().Icon;
      UnityAction<bool> onButton = isOn =>
      {
        if (!isOn)
          return;
        Select(slime);
      };
      component1.onValueChanged.AddListener(onButton);
      OnSelectDelegator.Create(buttonObj, () =>
      {
        onButton(true);
        buttonObj.GetComponent<Toggle>().isOn = true;
      });
    }
    else
    {
      component2.text = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("pedia").Get("t." + Enum.GetName(typeof (PediaDirector.Id), pediaDirector.lockedEntry.id).ToLowerInvariant());
      component3.sprite = pediaDirector.lockedEntry.icon;
      component1.interactable = false;
    }
    return buttonObj;
  }

  protected override void OnCancelPressed()
  {
    if (tutorialOnStack)
      HideTutorial();
    else
      base.OnCancelPressed();
  }

  private void Select(SlimeDefinition slime)
  {
    currentSlime = slime;
    placeholderPanel.SetActive(false);
    contentPanel.SetActive(true);
    slimeAppearanceCarousel.ShowSlime(slime);
    appearanceOneNameText.SetKey(slime.Appearances.ElementAt(0).NameXlateKey);
    appearanceTwoNameText.SetKey(slime.Appearances.ElementAt(1).NameXlateKey);
    if (slimeAppearanceDirector.GetChosenSlimeAppearance(slime) == slime.Appearances.ElementAt(0))
      SetAppearanceSelectableStates(false, true);
    else
      SetAppearanceSelectableStates(true, false);
    confirmCue = ScriptableObject.CreateInstance<SECTR_AudioCue>();
    List<SECTR_AudioCue.ClipData> clipDataList = new List<SECTR_AudioCue.ClipData>();
    if (tabbySoundSlimes.Contains(slime.IdentifiableId))
      clipDataList.AddRange(tabbySelectionCue.AudioClips);
    else if (slime.IdentifiableId == Identifiable.Id.SABER_SLIME)
      clipDataList.AddRange(saberSelectionCue.AudioClips);
    else
      clipDataList.AddRange(slime.Sounds.voiceFunCue.AudioClips);
    confirmCue.Bus = sfxBus;
    confirmCue.Volume = confirmCueVolumeRange;
    confirmCue.MaxInstances = confirmCueMaxInstances;
    confirmCue.Pitch = new Vector2(0.9f, 1.1f);
    confirmCue.AudioClips = clipDataList;
    confirmCue.Spatialization = SECTR_AudioCue.Spatializations.Simple2D;
  }
}
