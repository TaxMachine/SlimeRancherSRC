// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.SpawnResourceModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class SpawnResourceModel : PositionalModel
  {
    public float storedWater;
    public double nextSpawnTime;
    public bool nextSpawnRipens;
    private Participant part;

    public void SetParticipant(Participant part) => this.part = part;

    public void Init() => part.InitModel(this);

    public void NotifyParticipants() => part.SetModel(this);

    public Joint NearestJoint(Vector3 position, float maxDist) => part.NearestJoint(position, maxDist);

    public void Push(float storedWater, double nextSpawnTime)
    {
      this.storedWater = storedWater;
      this.nextSpawnTime = nextSpawnTime;
    }

    public void Pull(out float storedWater, out double nextSpawnTime)
    {
      storedWater = this.storedWater;
      nextSpawnTime = this.nextSpawnTime;
    }

    public interface Participant
    {
      void InitModel(SpawnResourceModel model);

      void SetModel(SpawnResourceModel model);

      Joint NearestJoint(Vector3 pos, float maxDist);
    }
  }
}
