// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.AccessDoorModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class AccessDoorModel
  {
    public AccessDoor.State state;
    public ProgressDirector.ProgressType[] progress;
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

    public void Push(AccessDoor.State state) => this.state = state;

    public void Pull(out AccessDoor.State state) => state = this.state;

    public bool IsUnlockedForGameMode(PlayerState.GameMode currGameMode)
    {
      UnlockedOnGameMode component = gameObj.GetComponent<UnlockedOnGameMode>();
      return component != null && component.IsUnlockedFor(currGameMode);
    }

    public interface Participant
    {
      void InitModel(AccessDoorModel model);

      void SetModel(AccessDoorModel model);
    }
  }
}
