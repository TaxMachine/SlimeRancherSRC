// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GordoModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class GordoModel
  {
    public int gordoEatenCount;
    public List<Identifiable.Id> fashions = new List<Identifiable.Id>();
    private GameObject gameObj;
    internal int targetCount;

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

    public ZoneDirector.Zone GetZoneId() => gameObj.GetComponent<GordoEat>().GetZoneId();

    public bool HasPopped() => gordoEatenCount == -1;

    public bool DropsKey() => gameObj.GetComponent<GordoEat>().DropsKey();

    public void Push(int gordoEatenCount, List<Identifiable.Id> fashions)
    {
      this.gordoEatenCount = gordoEatenCount;
      this.fashions = fashions;
    }

    public void Pull(out int gordoEatenCount, out List<Identifiable.Id> fashions)
    {
      gordoEatenCount = this.gordoEatenCount;
      fashions = this.fashions;
    }

    public void OnNewGameLoaded(PlayerState.GameMode currGameMode)
    {
      GordoNearBurstOnGameMode component = gameObj.GetComponent<GordoNearBurstOnGameMode>();
      if (!(component != null) || !component.NearBurstForGameMode(currGameMode))
        return;
      gordoEatenCount = Mathf.Min(targetCount, (int) (targetCount - component.remaining));
    }

    public void SetGameObjectActive(bool isActive) => gameObj.SetActive(isActive);

    public Identifiable.Id GetGordoId() => gameObj.GetComponent<GordoIdentifiable>().id;

    public void EventGordoActivate(bool isFirstActivation)
    {
      if (isFirstActivation)
      {
        gordoEatenCount = 0;
        foreach (Participant componentsInChild in gameObj.GetComponentsInChildren<Participant>(true))
          componentsInChild.OnResetEatenCount();
      }
      SetGameObjectActive(true);
    }

    public interface Participant
    {
      void InitModel(GordoModel model);

      void SetModel(GordoModel model);

      void OnResetEatenCount();
    }
  }
}
