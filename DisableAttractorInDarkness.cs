// Decompiled with JetBrains decompiler
// Type: DisableAttractorInDarkness
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class DisableAttractorInDarkness : SRBehaviour, CaveTrigger.Listener
{
  public float startHour = 18f;
  public float endHour = 6f;
  private TimeDirector timeDir;
  private MosaicAttractor attractor;
  private HashSet<GameObject> caves = new HashSet<GameObject>();

  public void Start()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    attractor = GetComponentInChildren<MosaicAttractor>(true);
  }

  public void Update()
  {
    float num = timeDir.CurrHourOrStart();
    SetAttractorActive(!(endHour >= (double) startHour ? num <= (double) endHour && num >= (double) startHour : num >= (double) startHour || num <= (double) endHour) && caves.Count <= 0);
  }

  public void OnCaveEnter(GameObject caveObj, bool affectLighting, AmbianceDirector.Zone caveZone) => caves.Add(caveObj);

  public void OnCaveExit(GameObject caveObj, bool affectLighting, AmbianceDirector.Zone caveZone) => caves.Remove(caveObj);

  private void SetAttractorActive(bool active) => attractor.gameObject.SetActive(active);
}
