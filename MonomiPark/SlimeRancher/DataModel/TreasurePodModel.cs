// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.TreasurePodModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class TreasurePodModel
  {
    public TreasurePod.State state;
    public Queue<Identifiable.Id> spawnQueue = new Queue<Identifiable.Id>();
    private GameObject gameObj;

    public void SetGameObject(GameObject gameObj) => this.gameObj = gameObj;

    public void Init()
    {
      foreach (Participant componentsInChild in gameObj.GetComponentsInChildren<Participant>(true))
        componentsInChild.InitModel(this);
    }

    public void NotifyParticipants()
    {
      foreach (Participant componentsInChild in gameObj.GetComponentsInChildren<Participant>(true))
        componentsInChild.SetModel(this);
    }

    public ZoneDirector.Zone GetZoneId() => gameObj.GetComponent<TreasurePod>().GetZoneId();

    public void Push(TreasurePod.State state, List<Identifiable.Id> spawnQueue)
    {
      this.state = state;
      this.spawnQueue = new Queue<Identifiable.Id>();
      for (int index = 0; index < spawnQueue.Count; ++index)
        this.spawnQueue.Enqueue(spawnQueue[index]);
    }

    public void Pull(out TreasurePod.State state, out List<Identifiable.Id> spawnQueue)
    {
      state = this.state;
      spawnQueue = new List<Identifiable.Id>();
      if (this.spawnQueue.Count <= 0)
        return;
      foreach (Identifiable.Id id in new List<Identifiable.Id>(this.spawnQueue))
      {
        if (id != Identifiable.Id.NONE)
          spawnQueue.Add(id);
      }
    }

    public interface Participant
    {
      void InitModel(TreasurePodModel model);

      void SetModel(TreasurePodModel model);
    }
  }
}
