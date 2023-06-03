// Decompiled with JetBrains decompiler
// Type: AppearancePreviewLineup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AppearancePreviewLineup : MonoBehaviour
{
  [Header("Prefabs")]
  [Tooltip("SlimeAppearanceApplicator prefab for slime appearances.")]
  public SlimeAppearanceApplicator appearancePreviewPrefab;
  [Tooltip("SlimeAppearanceApplicator prefab for qubit appearances.")]
  public SlimeAppearanceApplicator qubitPreviewPrefab;
  [Header("Camera")]
  public SlimePreviewCamera slimePreviewCamera;
  [Header("Slime Definitions")]
  public SlimeDefinitions slimeDefinitions;
  [Header("UI")]
  public Text slimeNameText;
  public Text controlStateText;
  public Dropdown selectedSlimeDefinition;
  public Dropdown animationParamDropdown;
  public Text animationValueText;
  [Header("Spacing")]
  public float xSpacing = 2f;
  public float qubitYSpacing = 3f;
  [Header("Extra Appearances")]
  [Tooltip("Extra appearances to show in the preview.")]
  public List<SlimeAppearance> extraAppearances = new List<SlimeAppearance>();
  private int currentFocusedIndex;
  private List<SlimeAppearanceApplicator> currentAppearancePreviews = new List<SlimeAppearanceApplicator>();
  private List<SlimeDefinition> currentDefinitions = new List<SlimeDefinition>();
  private List<SlimeAppearanceApplicator> qubitPreviews = new List<SlimeAppearanceApplicator>();
  private List<MaterialStealthController> stealthControllers = new List<MaterialStealthController>();
  private bool qubitModeEnabled;
  private bool menuControlsEnabled;
  private int extraOffset;
  private int baseSlimeCount;
  private List<SlimeDefinition> baseSlimes;
  private float currentCloakOpacity = 1f;
  private float targetCloakOpacity = 1f;
  private const float OPACITY_CHANGE_PER_SEC = 2f;

  private void Start()
  {
    foreach (SlimeAppearanceApplicator appearancePreview in currentAppearancePreviews)
    {
      if (appearancePreview != null)
        appearancePreview.ApplyAppearance();
    }
    selectedSlimeDefinition.options.Clear();
    baseSlimes = slimeDefinitions.Slimes.Where(slime => !slime.IsLargo).ToList();
    baseSlimeCount = baseSlimes.Count;
    if (extraAppearances.Count > 0)
    {
      extraOffset = 1;
      selectedSlimeDefinition.options.Add(new Dropdown.OptionData("Extra Appearances"));
    }
    foreach (UnityEngine.Object baseSlime in baseSlimes)
      selectedSlimeDefinition.options.Add(new Dropdown.OptionData("All " + baseSlime.name));
    foreach (UnityEngine.Object slime in slimeDefinitions.Slimes)
      selectedSlimeDefinition.options.Add(new Dropdown.OptionData(slime.name));
    selectedSlimeDefinition.value = 0;
    selectedSlimeDefinition.RefreshShownValue();
    OnDropdownValueChanged(0);
    ToggleControls();
  }

  public void Update()
  {
    if (Input.GetKeyDown(KeyCode.Tab))
      ToggleControls();
    if (menuControlsEnabled)
    {
      bool wasPressed1 = SRInput.PauseActions.menuDown.WasPressed;
      int num = SRInput.PauseActions.menuUp.WasPressed ? 1 : 0;
      bool wasPressed2 = SRInput.PauseActions.menuLeft.WasPressed;
      bool wasPressed3 = SRInput.PauseActions.menuRight.WasPressed;
      if (num != 0 && selectedSlimeDefinition.value > 0)
        --selectedSlimeDefinition.value;
      else if (wasPressed1 && selectedSlimeDefinition.value < selectedSlimeDefinition.options.Count)
        ++selectedSlimeDefinition.value;
      if (wasPressed2)
        MoveCamera(-1);
      else if (wasPressed3)
        MoveCamera(1);
    }
    if (targetCloakOpacity > (double) currentCloakOpacity)
      currentCloakOpacity = Mathf.Min(targetCloakOpacity, currentCloakOpacity + 2f * Time.deltaTime);
    else if (targetCloakOpacity < (double) currentCloakOpacity)
      currentCloakOpacity = Mathf.Max(targetCloakOpacity, currentCloakOpacity - 2f * Time.deltaTime);
    ApplyCloak();
  }

  private void ToggleControls()
  {
    slimePreviewCamera.zoomControlsEnabled = !slimePreviewCamera.zoomControlsEnabled;
    menuControlsEnabled = !slimePreviewCamera.zoomControlsEnabled;
    controlStateText.text = (menuControlsEnabled ? "Menu Controls Enabled" : "Camera Controls Enabled") + " (Tab to change)";
  }

  public void OnDropdownValueChanged(int index)
  {
    if (index < extraOffset)
      ShowAppearances(extraAppearances.Select(appearance =>
      {
        SlimeDefinition instance = ScriptableObject.CreateInstance<SlimeDefinition>();
        instance.PrefabScale = appearance.DependentAppearances.Length != 0 ? 2f : 1f;
        instance.AppearancesDefault = new SlimeAppearance[1]
        {
          appearance
        };
        return instance;
      }).ToList());
    else if (index < baseSlimeCount)
    {
      SlimeDefinition baseType = baseSlimes[index - extraOffset];
      List<SlimeDefinition> slimesToShow = new List<SlimeDefinition>()
      {
        baseType
      };
      slimesToShow.AddRange(slimeDefinitions.Slimes.Where(slime => slime.BaseSlimes.Contains(baseType)).ToList());
      ShowAppearances(slimesToShow);
    }
    else
      ShowAppearances(new List<SlimeDefinition>()
      {
        slimeDefinitions.Slimes[index - baseSlimeCount - extraOffset]
      });
  }

  public void ShowAppearances(List<SlimeDefinition> slimesToShow)
  {
    currentDefinitions.Clear();
    currentDefinitions.AddRange(slimesToShow);
    currentFocusedIndex = 0;
    Refresh();
  }

  public void Refresh()
  {
    for (int index1 = 0; index1 < transform.childCount; ++index1)
    {
      if (Application.isPlaying)
        Destroy(gameObject.transform.GetChild(index1).gameObject);
      else
        DestroyImmediate(gameObject.transform.GetChild(index1).gameObject);
    }
    qubitPreviews.Clear();
    currentAppearancePreviews.Clear();
    int index = 0;
    foreach (SlimeDefinition currentDefinition in currentDefinitions)
    {
      SlimeDefinition definition = currentDefinition;
      currentAppearancePreviews.AddRange(definition.Appearances.Select(appearance =>
      {
        SlimeAppearanceApplicator andShowAppearance = CreateAndShowAppearance(appearancePreviewPrefab, appearance, definition, index);
        if (index == 0)
          PopulateAnimationDropdown(andShowAppearance.GetComponentInChildren<Animator>());
        ++index;
        return andShowAppearance;
      }));
    }
    stealthControllers.Clear();
    foreach (Component appearancePreview in currentAppearancePreviews)
      stealthControllers.Add(new MaterialStealthController(appearancePreview.gameObject));
    if (currentFocusedIndex < currentAppearancePreviews.Count)
      LookAtIndex(currentFocusedIndex);
    else
      LookAtIndex(0);
    ApplyCloak();
  }

  private SlimeAppearanceApplicator CreateAndShowAppearance(
    SlimeAppearanceApplicator prefab,
    SlimeAppearance appearance,
    SlimeDefinition definition,
    int index,
    float yOffset = -0.5f)
  {
    SlimeAppearanceApplicator appearancePreview = LineupUtils.GenerateAppearancePreview(appearancePreviewPrefab, definition, appearance);
    appearancePreview.transform.parent = transform;
    appearancePreview.transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
    appearancePreview.transform.localPosition = new Vector3(index * xSpacing, yOffset + definition.PrefabScale / 2f, 0.0f);
    if (!qubitModeEnabled || !(appearance.QubitAppearance != null))
      return appearancePreview;
    qubitPreviews.Add(CreateAndShowAppearance(qubitPreviewPrefab, appearance.QubitAppearance, definition, index, qubitYSpacing));
    return appearancePreview;
  }

  private void PopulateAnimationDropdown(Animator animator)
  {
    animationParamDropdown.ClearOptions();
    List<string> options = new List<string>();
    foreach (AnimatorControllerParameter parameter in animator.parameters)
    {
      if (parameter.type == AnimatorControllerParameterType.Bool)
        options.Add(parameter.name);
    }
    animationParamDropdown.AddOptions(options);
    UpdateCurrentAnimationValueText();
  }

  public void MoveCamera(int direction) => LookAtIndex(currentFocusedIndex + direction);

  public void SetQubitMode(bool showQubits)
  {
    qubitModeEnabled = showQubits;
    Refresh();
  }

  public void SetCloakedMode(bool cloak)
  {
    targetCloakOpacity = cloak ? 0.0f : 1f;
    ApplyCloak();
  }

  private void ApplyCloak()
  {
    foreach (MaterialStealthController stealthController in stealthControllers)
      stealthController.SetOpacity(currentCloakOpacity);
  }

  public void ToggleAnimationBool()
  {
    string text = animationParamDropdown.options[animationParamDropdown.value].text;
    if (string.IsNullOrEmpty(text))
      return;
    foreach (Component appearancePreview in currentAppearancePreviews)
    {
      Animator componentInChildren = appearancePreview.GetComponentInChildren<Animator>();
      componentInChildren.SetBool(text, !componentInChildren.GetBool(text));
    }
    UpdateCurrentAnimationValueText();
  }

  public void UpdateCurrentAnimationValueText()
  {
    string text = animationParamDropdown.options[animationParamDropdown.value].text;
    if (currentAppearancePreviews.Count > 0)
    {
      Animator componentInChildren = currentAppearancePreviews[0].GetComponentInChildren<Animator>();
      animationValueText.text = text + ": " + componentInChildren.GetBool(text).ToString();
    }
    else
      animationValueText.text = "";
  }

  private void LookAtIndex(int previewIndex)
  {
    if (previewIndex < 0 || previewIndex >= currentAppearancePreviews.Count)
      return;
    currentFocusedIndex = previewIndex;
    slimePreviewCamera.ResetCamToTarget(currentAppearancePreviews[currentFocusedIndex].transform);
    slimeNameText.text = currentAppearancePreviews[currentFocusedIndex].Appearance.name;
  }
}
