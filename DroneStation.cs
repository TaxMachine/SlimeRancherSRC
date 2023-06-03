// Decompiled with JetBrains decompiler
// Type: DroneStation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (DroneStationBattery))]
public class DroneStation : SRBehaviour
{
  [Tooltip("Transform guide: resting position/rotation.")]
  public Transform guideRest;

  public DroneStationAnimator animator { get; private set; }

  public DroneStationBattery battery { get; private set; }

  public DroneGadget gadget { get; private set; }

  public void Awake()
  {
    gadget = GetComponentInParent<DroneGadget>();
    animator = GetComponentInChildren<DroneStationAnimator>();
    battery = GetComponent<DroneStationBattery>();
  }
}
