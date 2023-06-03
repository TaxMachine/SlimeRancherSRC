// Decompiled with JetBrains decompiler
// Type: EnergyMeter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class EnergyMeter : SRBehaviour
{
  [Tooltip("GameObject containing an FX that is active while the energy meeting is recharging. (optional)")]
  public GameObject onEnergyRechargingFX;
  [Tooltip("FX activated when the dash pad is active.")]
  public GameObject dashPadFX;
  private TimeDirector timeDirector;
  private PlayerModel model;
  private PlayerState state;
  private StatusBar statusBar;

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    model = SRSingleton<SceneContext>.Instance.GameModel.GetPlayerModel();
    state = SRSingleton<SceneContext>.Instance.PlayerState;
    statusBar = GetComponent<StatusBar>();
  }

  public void Update()
  {
    statusBar.currValue = state.GetCurrEnergy();
    statusBar.maxValue = state.GetMaxEnergy();
    if (!(bool) (Object) onEnergyRechargingFX)
      return;
    onEnergyRechargingFX.SetActive(timeDirector.HasReached(model.energyRecoverAfter) && model.currEnergy < (double) model.maxEnergy);
  }

  public GameObject Play(GameObject fxPrefab)
  {
    Transform transform = statusBar.statusImage.transform;
    GameObject fxObject = Instantiate(fxPrefab, transform);
    PlayFX(fxObject);
    return fxObject;
  }
}
