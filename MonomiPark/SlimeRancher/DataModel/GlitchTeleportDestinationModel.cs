// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GlitchTeleportDestinationModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class GlitchTeleportDestinationModel
  {
    public double? activationTime;
    private readonly Participant participant;

    public GlitchTeleportDestinationModel(
      Participant participant)
    {
      this.participant = participant;
    }

    public void Init() => participant.InitModel(this);

    public void NotifyParticipants() => participant.SetModel(this);

    public void Push(GlitchTeleportDestinationV01 persistence) => activationTime = persistence.activationTime;

    public GlitchTeleportDestinationV01 Pull() => new GlitchTeleportDestinationV01()
    {
      activationTime = activationTime
    };

    public interface Participant
    {
      void InitModel(GlitchTeleportDestinationModel model);

      void SetModel(GlitchTeleportDestinationModel model);

      string id { get; }
    }
  }
}
