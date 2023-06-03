// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.RanchModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class RanchModel
  {
    public Dictionary<RanchDirector.PaletteType, RanchDirector.Palette> selectedPalettes = new Dictionary<RanchDirector.PaletteType, RanchDirector.Palette>();
    private Participant participant;
    private Dictionary<string, RanchCellModel> ranchCells = new Dictionary<string, RanchCellModel>();

    public void SetParticipant(Participant participant) => this.participant = participant;

    public void Init()
    {
      if (participant != null)
        participant.InitModel(this);
      foreach (RanchCellModel ranchCellModel in ranchCells.Values)
        ranchCellModel.Init();
    }

    public void NotifyParticipants()
    {
      if (participant != null)
        participant.SetModel(this);
      foreach (RanchCellModel ranchCellModel in ranchCells.Values)
        ranchCellModel.NotifyParticipants();
    }

    public void RegisterRanchCell(
      string cellId,
      RanchCellModel.Participant participant,
      bool expectingPush)
    {
      RanchCellModel ranchCellModel = new RanchCellModel();
      ranchCellModel.SetParticipant(participant);
      if (!expectingPush)
      {
        ranchCellModel.Init();
        ranchCellModel.NotifyParticipants();
      }
      ranchCells[cellId] = ranchCellModel;
    }

    public void UnregisterRanchCell(string cellId) => ranchCells.Remove(cellId);

    public void SelectPalette(RanchDirector.PaletteType type, RanchDirector.Palette pal)
    {
      selectedPalettes[type] = pal;
      if (participant == null)
        return;
      participant.NoteSelected(type, pal);
    }

    public void Push(
      Dictionary<RanchDirector.PaletteType, RanchDirector.Palette> selectedPalettes,
      Dictionary<string, double> cellHibernationTimes)
    {
      foreach (KeyValuePair<RanchDirector.PaletteType, RanchDirector.Palette> selectedPalette in selectedPalettes)
        SelectPalette(selectedPalette.Key, selectedPalette.Value);
      foreach (KeyValuePair<string, RanchCellModel> ranchCell in ranchCells)
      {
        if (cellHibernationTimes.ContainsKey(ranchCell.Key))
          ranchCell.Value.Push(new double?(cellHibernationTimes[ranchCell.Key]));
        else
          ranchCell.Value.Push(new double?());
      }
    }

    public void Pull(
      out Dictionary<RanchDirector.PaletteType, RanchDirector.Palette> selectedPalettes,
      out Dictionary<string, double> cellHibernationTimes)
    {
      selectedPalettes = new Dictionary<RanchDirector.PaletteType, RanchDirector.Palette>();
      foreach (KeyValuePair<RanchDirector.PaletteType, RanchDirector.Palette> selectedPalette in this.selectedPalettes)
        selectedPalettes[selectedPalette.Key] = selectedPalette.Value;
      cellHibernationTimes = new Dictionary<string, double>();
      foreach (KeyValuePair<string, RanchCellModel> ranchCell in ranchCells)
      {
        double? hibernationTime;
        ranchCell.Value.Pull(out hibernationTime);
        if (hibernationTime.HasValue)
          cellHibernationTimes[ranchCell.Key] = hibernationTime.Value;
      }
    }

    public interface Participant
    {
      void InitModel(RanchModel model);

      void SetModel(RanchModel model);

      void NoteSelected(RanchDirector.PaletteType type, RanchDirector.Palette pal);
    }
  }
}
