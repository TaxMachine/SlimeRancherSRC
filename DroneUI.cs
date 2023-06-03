// Decompiled with JetBrains decompiler
// Type: DroneUI
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

public class DroneUI : BaseUI
{
  public Transform programsParent;
  public TMP_Text warningText;
  public Button activateButton;
  public Button resetButton;
  private List<DroneUIProgram> programUIs = new List<DroneUIProgram>();
  private bool programsChanged;
  private const int GRID_COLUMNS = 6;
  private DroneGadget gadget;
  private DroneMetadata.Program[] programs;
  private DroneUIProgramPicker pickerUI;

  public DroneUI Init(DroneGadget gadget)
  {
    this.gadget = gadget;
    programs = this.gadget.programs.Select(p => p.Clone()).ToArray();
    ResetUI();
    string programWarning = GetProgramWarning();
    warningText.gameObject.SetActive(programWarning != null);
    if (programWarning != null)
      warningText.text = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui").Xlate(programWarning);
    return this;
  }

  public void Start() => SECTR_AudioSystem.Play(metadata.onGuiEnableCue, Vector3.zero, false);

  public void OnEnable()
  {
    if (!(gadget != null))
      return;
    SECTR_AudioSystem.Play(metadata.onGuiEnableCue, Vector3.zero, false);
  }

  public void OnDisable() => SECTR_AudioSystem.Play(metadata.onGuiDisableCue, Vector3.zero, false);

  private string GetProgramWarning() => !gadget.drone.ammo.IsEmpty() ? "w.drone_reprogram_drops_ammo" : null;

  private void ResetUI()
  {
    foreach (Component programUi in programUIs)
      Destroyer.Destroy(programUi.gameObject, "DroneUI.ResetUI");
    programUIs.Clear();
    for (int index1 = 0; index1 < programs.Length; ++index1)
    {
      DroneMetadata.Program program = programs[index1];
      int? index2 = programs.Length >= 2 ? new int?(index1 + 1) : new int?();
      DroneUIProgram droneUiProgram = Instantiate(metadata.droneUIProgram.gameObject, programsParent).GetComponent<DroneUIProgram>().Init(program, index2);
      programUIs.Add(droneUiProgram);
      int idx = index1;
      SetProgramPicker(droneUiProgram.buttonTarget, program, p => new DroneMetadata.Program(metadata.GetDefaultTarget(), metadata.GetDefaultBehaviour(), metadata.GetDefaultBehaviour()), true, wp => GatherTarget(wp, tgt =>
      {
        wp.target = tgt;
        programs[idx] = program = wp;
        programsChanged = true;
      }));
      SetProgramPicker(droneUiProgram.buttonSource, program, p => new DroneMetadata.Program(p.target, metadata.GetDefaultBehaviour(), metadata.GetDefaultBehaviour()), program.target.id != "drone.target.none", wp => GatherSource(wp, src =>
      {
        wp.source = src;
        programs[idx] = program = wp;
        programsChanged = true;
      }));
      SetProgramPicker(droneUiProgram.buttonDestination, program, p => new DroneMetadata.Program(p.target, p.source, metadata.GetDefaultBehaviour()), program.source.id != "drone.behaviour.none", wp => GatherDestination(wp, dest =>
      {
        wp.destination = dest;
        programs[idx] = program = wp;
        programsChanged = true;
      }));
    }
    UpdateButtonState();
    SelectFirstButton();
    for (int index = 1; index < programUIs.Count; ++index)
      programUIs[index - 1].LinkGamepadNav(programUIs[index]);
    programUIs.Last().LinkGamepadNav(activateButton.interactable ? activateButton : (Selectable) resetButton);
  }

  private void UpdateButtonState()
  {
    activateButton.interactable = programsChanged && programs.Any(p => p.IsComplete());
    resetButton.interactable = programs.Any(p => !p.IsReset());
    LinkNavigation(activateButton, resetButton, NavigationDirection.DOWN_UP);
  }

  private void SelectFirstButton()
  {
    for (int index = 0; index < programs.Length; ++index)
    {
      if (programs[index].target.id == "drone.target.none")
      {
        programUIs[index].buttonTarget.button.Select();
        return;
      }
      if (programs[index].source.id == "drone.behaviour.none")
      {
        programUIs[index].buttonSource.button.Select();
        return;
      }
      if (programs[index].destination.id == "drone.behaviour.none")
      {
        programUIs[index].buttonDestination.button.Select();
        return;
      }
    }
    if (activateButton.interactable)
      activateButton.Select();
    else
      programUIs[0].buttonTarget.button.Select();
  }

  protected override bool Closeable() => base.Closeable() && pickerUI == null;

  public void OnClickConfirmation()
  {
    SECTR_AudioSystem.Play(metadata.onGuiButtonActivateCue, Vector3.zero, false);
    gadget.SetPrograms(programs);
    Close();
  }

  public void OnClickReset()
  {
    SECTR_AudioSystem.Play(metadata.onGuiButtonResetCue, Vector3.zero, false);
    foreach (DroneMetadata.Program program in programs)
    {
      program.target = metadata.GetDefaultTarget();
      program.source = metadata.GetDefaultBehaviour();
      program.destination = metadata.GetDefaultBehaviour();
    }
    gadget.SetPrograms(programs);
    programsChanged = false;
    ResetUI();
  }

  private void SetProgramPicker(
    DroneUIProgramButton button,
    DroneMetadata.Program program,
    DeriveProgram deriver,
    bool interactable,
    Action<DroneMetadata.Program> onClicked)
  {
    button.button.interactable = interactable;
    button.button.onClick.AddListener(() => onClicked(deriver(program)));
  }

  private void GatherTarget(
    DroneMetadata.Program workingProgram,
    Action<DroneMetadata.Program.Target> onComplete)
  {
    if (workingProgram.target.id == "drone.target.none")
      CreatePicker("t.drone.pick_target", metadata.pickTargetIcon, metadata.targets, metadata.onGuiButtonTargetCue, onComplete);
    else
      onComplete(workingProgram.target);
  }

  private void GatherSource(
    DroneMetadata.Program workingProgram,
    Action<DroneMetadata.Program.Behaviour> onComplete)
  {
    if (workingProgram.source.id == "drone.behaviour.none")
      CreatePicker("t.drone.pick_source", metadata.pickSourceIcon, FilterSources(workingProgram, metadata.sources), metadata.onGuiButtonSourceCue, onComplete);
    else
      onComplete(workingProgram.source);
  }

  private void GatherDestination(
    DroneMetadata.Program workingProgram,
    Action<DroneMetadata.Program.Behaviour> onComplete)
  {
    if (workingProgram.destination.id == "drone.behaviour.none")
      CreatePicker("t.drone.pick_destination", metadata.pickDestinationIcon, FilterDestinations(workingProgram, metadata.destinations), metadata.onGuiButtonDestinationCue, onComplete);
    else
      onComplete(workingProgram.destination);
  }

  private DroneMetadata.Program.Behaviour[] FilterSources(
    DroneMetadata.Program workingProgram,
    DroneMetadata.Program.Behaviour[] allSrcs)
  {
    return allSrcs.Where(s => s.isCompatible(workingProgram)).ToArray();
  }

  private DroneMetadata.Program.Behaviour[] FilterDestinations(
    DroneMetadata.Program workingProgram,
    DroneMetadata.Program.Behaviour[] allDests)
  {
    return allDests.Where(d => d.isCompatible(workingProgram) && d.id != workingProgram.source.id.Replace("source", "destination")).ToArray();
  }

  private void CreatePicker<T>(
    string title,
    Sprite titleIcon,
    T[] options,
    SECTR_AudioCue buttonCue,
    Action<T> onPicked)
    where T : DroneMetadata.Program.BaseComponent
  {
    if (pickerUI != null)
      Destroyer.Destroy(pickerUI.gameObject, "DroneUI.SetProgramPicker");
    pickerUI = Instantiate(metadata.droneUIProgramPicker.gameObject).GetComponent<DroneUIProgramPicker>();
    pickerUI.title.text = uiBundle.Get(title);
    pickerUI.icon.sprite = titleIcon;
    Button[] buttonArray = new Button[options.Length];
    for (int index = 0; index < options.Length; ++index)
    {
      T option = options[index];
      DroneUIProgramButton droneUiProgramButton = Instantiate(metadata.droneUIProgramButton.gameObject, pickerUI.contentGrid).GetComponent<DroneUIProgramButton>().Init(option);
      droneUiProgramButton.button.onClick.AddListener(() =>
      {
        SECTR_AudioSystem.Play(buttonCue, Vector3.zero, false);
        pickerUI.Close();
        onPicked(option);
      });
      buttonArray[index] = droneUiProgramButton.button;
      if (index == 0)
        droneUiProgramButton.button.gameObject.AddComponent<InitSelected>();
    }
    int num1 = Mathf.CeilToInt(buttonArray.Length / 6f);
    for (int index = 0; index < buttonArray.Length; ++index)
    {
      int num2 = index / 6;
      int num3 = index % 6;
      Navigation navigation = buttonArray[index].navigation with
      {
        mode = Navigation.Mode.Explicit
      };
      if (num2 > 0)
        navigation.selectOnUp = buttonArray[(num2 - 1) * 6 + num3];
      if (num2 < num1 - 1)
        navigation.selectOnDown = buttonArray[Math.Min((num2 + 1) * 6 + num3, buttonArray.Length - 1)];
      if (num3 > 0)
        navigation.selectOnLeft = buttonArray[num2 * 6 + (num3 - 1)];
      if (num3 < 5 && index < buttonArray.Length - 1)
        navigation.selectOnRight = buttonArray[num2 * 6 + (num3 + 1)];
      buttonArray[index].navigation = navigation;
    }
    DroneUIProgramPicker pickerUi = pickerUI;
    pickerUi.onDestroy = pickerUi.onDestroy + (() =>
    {
      if (!(SRSingleton<SceneContext>.Instance != null) || !(this != null) || !(gameObject != null))
        return;
      ResetUI();
    });
  }

  private DroneMetadata metadata => gadget.metadata;

  private delegate DroneMetadata.Program DeriveProgram(DroneMetadata.Program program);
}
