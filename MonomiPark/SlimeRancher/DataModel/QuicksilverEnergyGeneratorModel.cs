// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.QuicksilverEnergyGeneratorModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace MonomiPark.SlimeRancher.DataModel
{
  public class QuicksilverEnergyGeneratorModel
  {
    public QuicksilverEnergyGenerator.State state;
    public double? timer;
    private Participant part;

    public void SetParticipant(Participant part) => this.part = part;

    public void Init() => part.InitModel(this);

    public void NotifyParticipants() => part.SetModel(this);

    public void Push(QuicksilverEnergyGenerator.State state, double? timer)
    {
      this.state = state;
      this.timer = timer;
    }

    public void Pull(out QuicksilverEnergyGenerator.State state, out double? timer)
    {
      state = this.state;
      timer = this.timer;
    }

    public interface Participant
    {
      void InitModel(QuicksilverEnergyGeneratorModel model);

      void SetModel(QuicksilverEnergyGeneratorModel model);
    }
  }
}
