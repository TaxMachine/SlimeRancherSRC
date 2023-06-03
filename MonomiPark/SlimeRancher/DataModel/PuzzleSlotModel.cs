// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.PuzzleSlotModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class PuzzleSlotModel
  {
    public bool filled;
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

    public void Push(bool filled) => this.filled = filled;

    public void Pull(out bool filled) => filled = this.filled;

    public void OnNewGameLoaded(PlayerState.GameMode currGameMode)
    {
      UnlockedOnGameMode component = gameObj.GetComponent<UnlockedOnGameMode>();
      if (!(component != null) || !component.IsUnlockedFor(currGameMode))
        return;
      filled = true;
      foreach (Participant componentsInChild in gameObj.GetComponentsInChildren<Participant>(true))
        componentsInChild.OnFilledChangedFromModel();
    }

    public interface Participant
    {
      void InitModel(PuzzleSlotModel model);

      void SetModel(PuzzleSlotModel model);

      void OnFilledChangedFromModel();
    }
  }
}
