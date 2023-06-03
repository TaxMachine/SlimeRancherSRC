// Decompiled with JetBrains decompiler
// Type: ModelTransformUpdater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class ModelTransformUpdater : MonoBehaviour, PlayerModel.Participant
{
  private PlayerModel model;
  private vp_FPPlayerEventHandler playerEventHandler;

  public void Awake()
  {
    SRSingleton<SceneContext>.Instance.GameModel.RegisterPlayerParticipant(this);
    playerEventHandler = GetComponent<vp_FPPlayerEventHandler>();
  }

  public void InitModel(PlayerModel model)
  {
    model.position = transform.position;
    model.rotation = transform.rotation;
  }

  public void SetModel(PlayerModel model) => this.model = model;

  public void RegionSetChanged(
    RegionRegistry.RegionSetId previous,
    RegionRegistry.RegionSetId current)
  {
  }

  public void TransformChanged(Vector3 pos, Quaternion rot)
  {
    playerEventHandler.Position.Set(pos);
    playerEventHandler.Rotation.Set(rot.eulerAngles);
  }

  public void RegisteredPotentialAmmoChanged(
    Dictionary<PlayerState.AmmoMode, List<GameObject>> registeredPotentialAmmo)
  {
  }

  public void KeyAdded()
  {
  }

  public void LateUpdate()
  {
    model.position = transform.position;
    model.rotation = transform.rotation;
  }
}
