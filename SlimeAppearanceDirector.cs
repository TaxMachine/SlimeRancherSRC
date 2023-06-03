// Decompiled with JetBrains decompiler
// Type: SlimeAppearanceDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Slimes/Slime Appearance Director")]
public class SlimeAppearanceDirector : ScriptableObject, AppearancesModel.Participant
{
  public SlimeDefinitions SlimeDefinitions;
  [Tooltip("The icon to show for slime appearances that don't have an icon defined.")]
  public Sprite missingIcon;
  [Tooltip("SlimeAppearancePopupUI prefab reference.")]
  public GameObject appearancePopupUI;
  [Tooltip("The default animator controller to use for slimes.")]
  public RuntimeAnimatorController defaultAnimatorController;
  private readonly Dictionary<SlimeAppearance, List<AppearanceDefinitionPair>> appearancesByDependentAppearance = new Dictionary<SlimeAppearance, List<AppearanceDefinitionPair>>(SlimeAppearance.DefaultComparer);
  private AppearanceSelections appearanceSelections = new AppearanceSelections();

  public event OnSlimeAppearanceChangedDelegate onSlimeAppearanceChanged = (_param1, _param2) => { };

  public void OnEnable()
  {
    foreach (SlimeDefinition slime in SlimeDefinitions.Slimes)
    {
      foreach (SlimeAppearance appearance in slime.Appearances)
        RegisterDependentAppearances(slime, appearance);
    }
    RefreshDefaultChosenSlimes();
  }

  public void RegisterDependentAppearances(SlimeDefinition definition, SlimeAppearance appearance)
  {
    if (appearance == null)
    {
      Log.Error("Found an unassigned appearance in a slime definition.", "SlimeDefinition", definition.name);
    }
    else
    {
      foreach (SlimeAppearance dependentAppearance in appearance.DependentAppearances)
      {
        if (dependentAppearance == null)
        {
          Log.Error("Found an unassigned dependent appearance in a slime appearance.", "SlimeAppearance", appearance);
        }
        else
        {
          List<AppearanceDefinitionPair> appearanceDefinitionPairList;
          if (!appearancesByDependentAppearance.TryGetValue(dependentAppearance, out appearanceDefinitionPairList))
          {
            appearanceDefinitionPairList = new List<AppearanceDefinitionPair>();
            appearancesByDependentAppearance.Add(dependentAppearance, appearanceDefinitionPairList);
          }
          appearanceDefinitionPairList.Add(new AppearanceDefinitionPair()
          {
            appearance = appearance,
            definition = definition
          });
        }
      }
    }
  }

  public void RefreshDefaultChosenSlimes()
  {
    foreach (SlimeDefinition slime in SlimeDefinitions.Slimes)
    {
      SlimeAppearance slimeAppearance = slime.Appearances.First();
      UnlockAppearance(slime, slimeAppearance);
      UpdateChosenSlimeAppearance(slime, slimeAppearance);
    }
  }

  public SlimeAppearance GetChosenSlimeAppearance(Identifiable.Id id) => GetChosenSlimeAppearance(SlimeDefinitions.GetSlimeByIdentifiableId(id));

  public SlimeAppearance GetChosenSlimeAppearance(SlimeDefinition slimeDefinition) => appearanceSelections.GetSelectedAppearance(slimeDefinition);

  public void UpdateChosenSlimeAppearance(
    SlimeDefinition definition,
    SlimeAppearance newChosenAppearance)
  {
    SetChosenSlimeAppearance(definition, newChosenAppearance);
    List<AppearanceDefinitionPair> appearanceDefinitionPairList;
    if (!appearancesByDependentAppearance.TryGetValue(newChosenAppearance, out appearanceDefinitionPairList))
      return;
    foreach (AppearanceDefinitionPair appearanceDefinitionPair in appearanceDefinitionPairList)
    {
      if (AreDependentAppearancesChosen(appearanceDefinitionPair.appearance))
        SetChosenSlimeAppearance(appearanceDefinitionPair.definition, appearanceDefinitionPair.appearance);
    }
  }

  public Sprite GetCurrentSlimeIcon(Identifiable.Id slimeId)
  {
    Sprite icon = GetChosenSlimeAppearance(slimeId).Icon;
    return !(icon != null) ? missingIcon : icon;
  }

  private bool AreDependentAppearancesChosen(SlimeAppearance appearance) => appearance.DependentAppearances.All(a => appearanceSelections.GetAllSelectedAppearances().Contains(a));

  private void SetChosenSlimeAppearance(
    SlimeDefinition slimeDefinition,
    SlimeAppearance newAppearance)
  {
    appearanceSelections.SelectAppearanceForSlime(slimeDefinition, newAppearance);
    onSlimeAppearanceChanged(slimeDefinition, newAppearance);
  }

  public void UnlockAppearance(SlimeDefinition slimeDefinition, SlimeAppearance appearance) => appearanceSelections.UnlockAppearanceForSlime(slimeDefinition, appearance);

  public void LockAppearance(SlimeDefinition slimeDefinition, SlimeAppearance appearance) => appearanceSelections.LockAppearanceForSlime(slimeDefinition, appearance);

  public bool IsAppearanceUnlocked(SlimeDefinition slimeDefinition, SlimeAppearance appearance) => appearanceSelections.GetUnlockedAppearances(slimeDefinition).Contains(appearance);

  public IEnumerable<SlimeAppearance> GetUnlockedAppearances(SlimeDefinition slimeDefinition) => appearanceSelections.GetUnlockedAppearances(slimeDefinition);

  public void InitForLevel()
  {
    SRSingleton<SceneContext>.Instance.GameModel.RegisterAppearances(this);
    RefreshDefaultChosenSlimes();
  }

  public void InitModel(AppearancesModel model)
  {
    foreach (SlimeDefinition slime in SlimeDefinitions.Slimes.Where(slime => !slime.IsLargo))
    {
      SlimeAppearance appearanceForSet = slime.GetAppearanceForSet(SlimeAppearance.AppearanceSaveSet.CLASSIC);
      if (appearanceForSet == null)
        throw new Exception("No classic appearance available for slime " + slime.Name);
      model.AppearanceSelections.UnlockAppearanceForSlime(slime, appearanceForSet);
      model.AppearanceSelections.SelectAppearanceForSlime(slime, appearanceForSet);
    }
  }

  public void SetModel(AppearancesModel model)
  {
    appearanceSelections = model.AppearanceSelections;
    foreach (Identifiable.Id id in model.unlocks.Keys.ToList())
    {
      SlimeDefinition slime = SlimeDefinitions.GetSlimeByIdentifiableId(id);
      foreach (SlimeAppearance appearance in model.unlocks[id].Select(saveSet => slime.GetAppearanceForSet(saveSet)).ToList())
        UnlockAppearance(slime, appearance);
    }
    foreach (Identifiable.Id id in model.selections.Keys.ToList())
    {
      SlimeDefinition byIdentifiableId = SlimeDefinitions.GetSlimeByIdentifiableId(id);
      UpdateChosenSlimeAppearance(byIdentifiableId, byIdentifiableId.GetAppearanceForSet(model.selections[id]));
    }
  }

  public delegate void OnSlimeAppearanceChangedDelegate(
    SlimeDefinition slime,
    SlimeAppearance appearance);

  private class AppearanceDefinitionPair
  {
    public SlimeAppearance appearance;
    public SlimeDefinition definition;
  }
}
