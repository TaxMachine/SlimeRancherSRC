// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.AppearancesModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class AppearancesModel
  {
    public readonly Dictionary<Identifiable.Id, HashSet<SlimeAppearance.AppearanceSaveSet>> unlocks = new Dictionary<Identifiable.Id, HashSet<SlimeAppearance.AppearanceSaveSet>>();
    public readonly Dictionary<Identifiable.Id, SlimeAppearance.AppearanceSaveSet> selections = new Dictionary<Identifiable.Id, SlimeAppearance.AppearanceSaveSet>();
    private Participant participant;

    public AppearanceSelections AppearanceSelections { get; private set; }

    public AppearancesModel()
    {
      AppearanceSelections = new AppearanceSelections();
      AppearanceSelections.onAppearanceUnlocked += OnAppearanceUnlocked;
      AppearanceSelections.onAppearanceSelected += OnAppearanceSelected;
      AppearanceSelections.onAppearanceLocked += OnAppearanceLocked;
    }

    public void SetParticipant(Participant participant) => this.participant = this.participant == null ? participant : throw new Exception("Replacing participant on AppearanceModel");

    public void Init()
    {
      if (participant == null)
        return;
      participant.InitModel(this);
    }

    public void NotifyParticipants()
    {
      if (participant == null)
        return;
      participant.SetModel(this);
    }

    private void OnAppearanceUnlocked(SlimeDefinition slime, SlimeAppearance appearance)
    {
      if (!ShouldPersistSlimeAppearanceInfo(slime.IdentifiableId))
        return;
      HashSet<SlimeAppearance.AppearanceSaveSet> appearanceSaveSetSet;
      if (!unlocks.TryGetValue(slime.IdentifiableId, out appearanceSaveSetSet))
      {
        appearanceSaveSetSet = new HashSet<SlimeAppearance.AppearanceSaveSet>();
        unlocks[slime.IdentifiableId] = appearanceSaveSetSet;
      }
      appearanceSaveSetSet.Add(appearance.SaveSet);
    }

    private void OnAppearanceSelected(SlimeDefinition slime, SlimeAppearance appearance)
    {
      if (!ShouldPersistSlimeAppearanceInfo(slime.IdentifiableId))
        return;
      selections[slime.IdentifiableId] = appearance.SaveSet;
    }

    private void OnAppearanceLocked(SlimeDefinition slime, SlimeAppearance appearance)
    {
      HashSet<SlimeAppearance.AppearanceSaveSet> appearanceSaveSetSet;
      if (!ShouldPersistSlimeAppearanceInfo(slime.IdentifiableId) || !unlocks.TryGetValue(slime.IdentifiableId, out appearanceSaveSetSet))
        return;
      appearanceSaveSetSet.Remove(appearance.SaveSet);
    }

    public void Push(AppearancesV01 persistence)
    {
      foreach (KeyValuePair<Identifiable.Id, List<SlimeAppearance.AppearanceSaveSet>> unlock in persistence.unlocks)
      {
        HashSet<SlimeAppearance.AppearanceSaveSet> appearanceSaveSetSet;
        if (!unlocks.TryGetValue(unlock.Key, out appearanceSaveSetSet))
        {
          appearanceSaveSetSet = new HashSet<SlimeAppearance.AppearanceSaveSet>();
          unlocks[unlock.Key] = appearanceSaveSetSet;
        }
        foreach (SlimeAppearance.AppearanceSaveSet appearanceSaveSet in unlock.Value)
          appearanceSaveSetSet.Add(appearanceSaveSet);
      }
      foreach (KeyValuePair<Identifiable.Id, SlimeAppearance.AppearanceSaveSet> selection in persistence.selections)
        selections[selection.Key] = selection.Value;
    }

    public AppearancesV01 Pull() => new AppearancesV01()
    {
      unlocks = unlocks.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value.ToList()),
      selections = selections
    };

    private static bool ShouldPersistSlimeAppearanceInfo(Identifiable.Id slimeId) => !Identifiable.IsLargo(slimeId) && !Identifiable.IsTarr(slimeId);

    public interface Participant
    {
      void InitModel(AppearancesModel model);

      void SetModel(AppearancesModel model);
    }
  }
}
