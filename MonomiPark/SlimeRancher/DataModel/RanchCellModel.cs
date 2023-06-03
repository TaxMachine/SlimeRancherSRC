// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.RanchCellModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace MonomiPark.SlimeRancher.DataModel
{
  public class RanchCellModel
  {
    public double? hibernationTime;
    public double? sleepingTime;
    private Participant participant;

    public void SetParticipant(Participant participant) => this.participant = participant;

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

    public void Push(double? hibernationTime) => this.hibernationTime = hibernationTime;

    public void Pull(out double? hibernationTime) => hibernationTime = this.hibernationTime;

    public interface Participant
    {
      void InitModel(RanchCellModel model);

      void SetModel(RanchCellModel model);
    }
  }
}
