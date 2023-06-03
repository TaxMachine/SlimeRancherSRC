// Decompiled with JetBrains decompiler
// Type: SlimePreviewUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlimePreviewUI : MonoBehaviour
{
  public SlimeDefinitions slimeDefinitions;
  public SlimeAppearanceApplicator slimeAppearanceApplicator;
  public SlimePreviewCamera slimeCam;
  public Toggle groundedToggle;
  public Dropdown typeDropdown;
  public Dropdown appearanceDropdown;
  public Button refreshButton;
  public Button lookAtButton;
  private SlimeDefinition currentSlimeDefinition;
  private SlimeAppearance currentAppearance;
  private readonly List<SlimeAppearance> currentSlimeAppearances = new List<SlimeAppearance>();
  private DLCDirector DLCDirector;

  public void Awake()
  {
    DLCDirector = SRSingleton<GameContext>.Instance.DLCDirector;
    DLCDirector.onPackageInstalled += OnDLCPackageInstalled;
  }

  public void OnDestroy()
  {
    if (DLCDirector == null)
      return;
    DLCDirector.onPackageInstalled -= OnDLCPackageInstalled;
    DLCDirector = null;
  }

  private void Start()
  {
    typeDropdown.ClearOptions();
    appearanceDropdown.ClearOptions();
    typeDropdown.AddOptions(slimeDefinitions.Slimes.Select(slime => slime.Name).ToList());
    typeDropdown.onValueChanged.AddListener(OnTypeSelected);
    appearanceDropdown.onValueChanged.AddListener(OnAppearanceSelected);
    refreshButton.onClick.AddListener(RefreshAppearance);
    lookAtButton.onClick.AddListener(() => slimeCam.ResetCamToTarget(slimeAppearanceApplicator.transform));
    groundedToggle.onValueChanged.AddListener(value => RefreshAppearance());
    OnTypeSelected(0);
  }

  private void OnTypeSelected(int index)
  {
    currentSlimeDefinition = slimeDefinitions.Slimes[index];
    currentSlimeAppearances.Clear();
    currentSlimeAppearances.AddRange(currentSlimeDefinition.Appearances.SelectMany(appearance => new SlimeAppearance[3]
    {
      appearance,
      appearance.QubitAppearance,
      appearance.ShockedAppearance
    }).Where(appearance => appearance != null));
    appearanceDropdown.ClearOptions();
    appearanceDropdown.AddOptions(currentSlimeAppearances.Select(appearance => appearance.name).ToList());
    OnAppearanceSelected(0);
  }

  private void OnAppearanceSelected(int index)
  {
    currentAppearance = currentSlimeAppearances[index];
    RefreshAppearance();
  }

  private void RefreshAppearance()
  {
    slimeAppearanceApplicator.SlimeDefinition = currentSlimeDefinition;
    slimeAppearanceApplicator.Appearance = currentAppearance;
    slimeAppearanceApplicator.ApplyAppearance();
    slimeAppearanceApplicator.transform.localScale = new Vector3(currentSlimeDefinition.PrefabScale, currentSlimeDefinition.PrefabScale, currentSlimeDefinition.PrefabScale);
    EnableBasedOnGrounded[] componentsInChildren = slimeAppearanceApplicator.GetComponentsInChildren<EnableBasedOnGrounded>();
    groundedToggle.gameObject.SetActive(componentsInChildren.Length != 0);
    foreach (EnableBasedOnGrounded enableBasedOnGrounded in componentsInChildren)
      enableBasedOnGrounded.gameObject.SetActive(enableBasedOnGrounded.enableOnGrounded ^ groundedToggle.isOn);
    foreach (Behaviour componentsInChild in slimeAppearanceApplicator.GetComponentsInChildren<DeactivateOnHeld>())
      componentsInChild.enabled = false;
  }

  private void OnDLCPackageInstalled(DLCPackage.Id package)
  {
    if (package != DLCPackage.Id.SECRET_STYLE)
      return;
    OnTypeSelected(0);
  }
}
