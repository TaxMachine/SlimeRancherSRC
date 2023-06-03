// Decompiled with JetBrains decompiler
// Type: MapDataEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MapDataEntry : SRBehaviour, TechActivator
{
  [Tooltip("The zone for which we are giving map data.")]
  public ZoneDirector.Zone zone;
  public GameObject hologram;
  public GameObject activeFx;
  private Collider collider;

  public void Start()
  {
    collider = GetRequiredComponent<Collider>();
    UpdateHologramState();
  }

  public void Activate()
  {
    if (!IsZoneLocked())
      return;
    SRSingleton<SceneContext>.Instance.PlayerState.UnlockMap(zone);
    UpdateHologramState();
    SRSingleton<SceneContext>.Instance.TutorialDirector.OnMapDataGained();
    SRSingleton<Map>.Instance.OpenMap(zone);
  }

  private void UpdateHologramState()
  {
    bool flag = IsZoneLocked();
    hologram.SetActive(flag);
    activeFx.SetActive(flag);
    collider.enabled = flag;
  }

  public GameObject GetCustomGuiPrefab() => null;

  private bool IsZoneLocked() => !SRSingleton<SceneContext>.Instance.PlayerState.HasUnlockedMap(zone);
}
