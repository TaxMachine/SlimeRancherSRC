// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.MasterSwitchModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class MasterSwitchModel
  {
    public SwitchHandler.State state;
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

    public void Push(SwitchHandler.State state) => this.state = state;

    public void Pull(out SwitchHandler.State state) => state = this.state;

    public interface Participant
    {
      void InitModel(MasterSwitchModel model);

      void SetModel(MasterSwitchModel model);
    }
  }
}
