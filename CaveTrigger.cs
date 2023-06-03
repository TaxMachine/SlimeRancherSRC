// Decompiled with JetBrains decompiler
// Type: CaveTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CaveTrigger : WeatherBlockingTrigger
{
  public CaveLightController[] lights = new CaveLightController[0];
  public bool affectsLighting = true;
  public AmbianceDirector.Zone caveZone = AmbianceDirector.Zone.CAVE;
  private PlayerCaveLighting playerListener;
  private float triggerness;
  private AmbianceDirector ambianceDir;

  public void Awake() => ambianceDir = SRSingleton<SceneContext>.Instance.AmbianceDirector;

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger)
      return;
    Listener interfaceComponent = col.gameObject.GetInterfaceComponent<Listener>();
    if (interfaceComponent == null)
      return;
    interfaceComponent.OnCaveEnter(gameObject, affectsLighting, caveZone);
    if (!(interfaceComponent is PlayerCaveLighting))
      return;
    playerListener = (PlayerCaveLighting) interfaceComponent;
  }

  public void OnTriggerExit(Collider col)
  {
    if (col.isTrigger)
      return;
    Listener interfaceComponent = col.gameObject.GetInterfaceComponent<Listener>();
    if (interfaceComponent == null)
      return;
    interfaceComponent.OnCaveExit(gameObject, affectsLighting, caveZone);
    if (interfaceComponent != playerListener)
      return;
    playerListener = null;
  }

  public void OnDisable()
  {
    if (!(playerListener != null))
      return;
    playerListener.OnCaveExit(gameObject, affectsLighting, caveZone);
    playerListener = null;
  }

  public void Update()
  {
    if (playerListener != null && triggerness < 1.0)
      triggerness = Mathf.Min(1f, triggerness + Time.deltaTime / ambianceDir.zoneSettingTransitionTime);
    else if (playerListener == null && triggerness > 0.0)
      triggerness = Mathf.Max(0.0f, triggerness - Time.deltaTime / ambianceDir.zoneSettingTransitionTime);
    for (int index = 0; index < lights.Length; ++index)
      lights[index].SetTriggerness(this, triggerness);
  }

  public interface Listener
  {
    void OnCaveEnter(GameObject gameObject, bool affectLighting, AmbianceDirector.Zone caveZone);

    void OnCaveExit(GameObject gameObject, bool affectLighting, AmbianceDirector.Zone caveZone);
  }
}
