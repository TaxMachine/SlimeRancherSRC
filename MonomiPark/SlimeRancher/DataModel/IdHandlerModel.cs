// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.IdHandlerModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace MonomiPark.SlimeRancher.DataModel
{
  public abstract class IdHandlerModel
  {
    private Participant participant;

    public void SetParticipant(Participant participant) => this.participant = participant;

    public void Init() => participant.InitModel(this);

    public void NotifyParticipants() => participant.SetModel(this);

    public interface Participant
    {
      void InitModel(IdHandlerModel model);

      void SetModel(IdHandlerModel model);

      string GetId();
    }
  }
}
