// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GlitchImpostoDirectorModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class GlitchImpostoDirectorModel
  {
    public double? hibernationTime;
    private readonly Participant participant;

    public GlitchImpostoDirectorModel(Participant participant) => this.participant = participant;

    public void Init() => participant.InitModel(this);

    public void NotifyParticipants() => participant.SetModel(this);

    public void Push(GlitchImpostoDirectorV01 persistence) => hibernationTime = persistence.hibernationTime;

    public GlitchImpostoDirectorV01 Pull() => new GlitchImpostoDirectorV01()
    {
      hibernationTime = hibernationTime
    };

    public interface Participant
    {
      void InitModel(GlitchImpostoDirectorModel model);

      void SetModel(GlitchImpostoDirectorModel model);

      string id { get; }
    }
  }
}
