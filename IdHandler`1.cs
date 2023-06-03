// Decompiled with JetBrains decompiler
// Type: IdHandler`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public abstract class IdHandler<M> : IdHandler, IdHandlerModel.Participant where M : IdHandlerModel
{
  private GameModel.Unregistrant unregistrant;

  public void InitModel(IdHandlerModel model) => InitModel(model as M);

  public void SetModel(IdHandlerModel model) => SetModel(model as M);

  public string GetId() => id;

  public virtual void Awake()
  {
    if (!Application.isPlaying || !(SRSingleton<SceneContext>.Instance != null))
      return;
    unregistrant = Register(SRSingleton<SceneContext>.Instance.GameModel);
  }

  public virtual void OnDestroy()
  {
    if (unregistrant == null)
      return;
    unregistrant();
    unregistrant = null;
  }

  protected abstract GameModel.Unregistrant Register(GameModel game);

  protected abstract void InitModel(M model);

  protected abstract void SetModel(M model);
}
