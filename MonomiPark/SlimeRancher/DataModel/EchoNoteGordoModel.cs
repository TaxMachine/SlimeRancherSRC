// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.EchoNoteGordoModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class EchoNoteGordoModel
  {
    public State state;
    private readonly GameObject gameObject;

    public EchoNoteGordoModel(GameObject gameObject) => this.gameObject = gameObject;

    public void Init()
    {
      foreach (Participant componentsInChild in gameObject.GetComponentsInChildren<Participant>(true))
        componentsInChild.InitModel(this);
    }

    public void NotifyParticipants()
    {
      foreach (Participant componentsInChild in gameObject.GetComponentsInChildren<Participant>(true))
        componentsInChild.SetModel(this);
    }

    public void Activate(bool isFirstActivation)
    {
      state = isFirstActivation ? State.NOT_POPPED : state;
      gameObject.SetActive(true);
    }

    public void Push(EchoNoteGordoV01 persistence) => state = persistence.state;

    public EchoNoteGordoV01 Pull() => new EchoNoteGordoV01()
    {
      state = state
    };

    public interface Participant
    {
      void InitModel(EchoNoteGordoModel model);

      void SetModel(EchoNoteGordoModel model);
    }

    public enum State
    {
      NOT_POPPED,
      POPPING_1,
      POPPING_2,
      POPPED,
    }
  }
}
