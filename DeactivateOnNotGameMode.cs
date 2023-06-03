// Decompiled with JetBrains decompiler
// Type: DeactivateOnNotGameMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnNotGameMode : SRBehaviour, GameModel.Participant
{
  [Tooltip("List of game modes that will activate the object.")]
  public List<PlayerState.GameMode> whiteList;
  private PlayerState.GameMode? mode;

  public void Awake() => SRSingleton<SceneContext>.Instance.GameModel.RegisterGameModelParticipant(this);

  public void OnEnable()
  {
    if (!mode.HasValue || whiteList.Contains(mode.Value))
      return;
    gameObject.SetActive(false);
  }

  public void InitModel(GameModel model)
  {
  }

  public void SetModel(GameModel model)
  {
    mode = new PlayerState.GameMode?(model.currGameMode);
    OnEnable();
  }
}
