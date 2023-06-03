// Decompiled with JetBrains decompiler
// Type: DroneNoClip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

[RequireComponent(typeof (Drone))]
public class DroneNoClip : SRBehaviour, GadgetModel.Participant
{
  private DroneModel model;

  public void InitModel(GadgetModel model)
  {
  }

  public void SetModel(GadgetModel model) => this.model = (DroneModel) model;

  public void OnEnable()
  {
    gameObject.layer = 14;
    model.noClip = true;
  }

  public void OnDisable()
  {
    gameObject.layer = 20;
    model.noClip = false;
  }
}
