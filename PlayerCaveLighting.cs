// Decompiled with JetBrains decompiler
// Type: PlayerCaveLighting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlayerCaveLighting : SRBehaviour, CaveTrigger.Listener
{
  private AmbianceDirector ambianceDir;

  public void Awake() => ambianceDir = SRSingleton<SceneContext>.Instance.AmbianceDirector;

  public void OnCaveEnter(
    GameObject gameObject,
    bool affectLighting,
    AmbianceDirector.Zone caveZone)
  {
    if (!affectLighting)
      return;
    ambianceDir.EnterCave(caveZone);
  }

  public void OnCaveExit(
    GameObject gameObject,
    bool affectLighting,
    AmbianceDirector.Zone caveZone)
  {
    if (!affectLighting)
      return;
    ambianceDir.ExitCave(caveZone);
  }
}
