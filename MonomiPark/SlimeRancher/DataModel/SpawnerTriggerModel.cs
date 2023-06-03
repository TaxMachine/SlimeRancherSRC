// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.SpawnerTriggerModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace MonomiPark.SlimeRancher.DataModel
{
  public class SpawnerTriggerModel : PositionalModel
  {
    public double nextTriggerTime;
    private Participant part;

    public void SetParticipant(Participant part) => this.part = part;

    public void Init() => part.InitModel(this);

    public void NotifyParticipants() => part.SetModel(this);

    public void Push(double nextTriggerTime) => this.nextTriggerTime = nextTriggerTime;

    public void Pull(out double nextTriggerTime) => nextTriggerTime = this.nextTriggerTime;

    public interface Participant
    {
      void InitModel(SpawnerTriggerModel model);

      void SetModel(SpawnerTriggerModel model);
    }
  }
}
