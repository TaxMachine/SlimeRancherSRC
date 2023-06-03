// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.DirectedAnimalSpawnerModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace MonomiPark.SlimeRancher.DataModel
{
  public class DirectedAnimalSpawnerModel : PositionalModel
  {
    public double nextSpawnTime;
    private Participant part;

    public void SetParticipant(Participant part) => this.part = part;

    public void Init() => part.InitModel(this);

    public void NotifyParticipants() => part.SetModel(this);

    public void Push(double nextSpawnTime) => this.nextSpawnTime = nextSpawnTime;

    public void Pull(out double nextSpawnTime) => nextSpawnTime = this.nextSpawnTime;

    public interface Participant
    {
      void InitModel(DirectedAnimalSpawnerModel model);

      void SetModel(DirectedAnimalSpawnerModel model);
    }
  }
}
