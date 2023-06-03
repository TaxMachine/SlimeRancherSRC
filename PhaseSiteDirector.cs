// Decompiled with JetBrains decompiler
// Type: PhaseSiteDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhaseSiteDirector : SRBehaviour, WorldModel.Participant
{
  public List<PhaseSite> availablePhaseSites;
  public List<PhaseSite> occupiedPhaseSites = new List<PhaseSite>();
  public PhaseableObject phaseableObjectPrefab;
  public int numberOfPhaseableObjects;
  private const int MAX_SITE_SELECTION_ATTEMPTS = 10;
  private const float UPDATE_SPAWNABLE_RESOURCE_PERIOD = 10f;
  private float nextSpawnableResourceUpdate;
  private TimeDirector timeDirector;
  private WorldModel worldModel;
  private List<PhaseSite> local_occupiedPhaseSite = new List<PhaseSite>();

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterWorldParticipant(this);
  }

  public void InitModel(WorldModel model)
  {
  }

  public void SetModel(WorldModel model)
  {
    worldModel = model;
    ResetAllSites();
    foreach (PhaseSite site in new List<PhaseSite>(availablePhaseSites))
    {
      if (model.occupiedPhaseSites.Contains(site.id))
        PlacePhaseObject(site);
    }
    RefreshTotalPhaseableObjects();
  }

  public void ClearSites()
  {
    foreach (PhaseSite site in new List<PhaseSite>(occupiedPhaseSites))
      ClearSite(site);
  }

  public void Update()
  {
    foreach (PhaseSite occupiedPhaseSite in occupiedPhaseSites)
      local_occupiedPhaseSite.Add(occupiedPhaseSite);
    bool flag = false;
    if (nextSpawnableResourceUpdate < (double) Time.time)
    {
      flag = true;
      nextSpawnableResourceUpdate = Time.time + 10f;
    }
    foreach (PhaseSite source in local_occupiedPhaseSite)
    {
      if (source.phaseableObject.ReadyToPhase())
      {
        PhaseSite target = PickRandomAvailableSite();
        Phase(source, target);
      }
      else if (flag)
      {
        SpawnResource component = source.phaseableObject.GetComponent<SpawnResource>();
        if (component != null && !component.isActiveAndEnabled)
          component.UpdateToTime(timeDirector.WorldTime(), 0.0);
      }
    }
    local_occupiedPhaseSite.Clear();
  }

  public void PlacePhaseObject(string phaseSiteId)
  {
    PhaseSite site = availablePhaseSites.FirstOrDefault(s => string.Compare(phaseSiteId, s.id) == 0);
    if (!(site != null))
      return;
    PlacePhaseObject(site);
  }

  public void Phase(PhaseSite source, PhaseSite target)
  {
    source.phaseableObject.PhaseOut();
    PlacePhaseObject(target, source.phaseableObject);
    ClearSite(source);
    target.phaseableObject.PhaseIn();
  }

  public void ClearSite(PhaseSite site)
  {
    site.phaseableObject = null;
    availablePhaseSites.Add(site);
    occupiedPhaseSites.Remove(site);
    worldModel.occupiedPhaseSites.Remove(site.id);
  }

  public void PlacePhaseObject(PhaseSite site) => PlacePhaseObject(site, Instantiate(phaseableObjectPrefab, site.transform));

  public void PlacePhaseObject(PhaseSite site, PhaseableObject phasingObject)
  {
    site.phaseableObject = phasingObject;
    occupiedPhaseSites.Add(site);
    availablePhaseSites.Remove(site);
    worldModel.occupiedPhaseSites.Add(site.id);
  }

  public PhaseSite PickRandomAvailableSite() => availablePhaseSites.ElementAt(UnityEngine.Random.Range(0, availablePhaseSites.Count - 1));

  public void ResetAllSites()
  {
    foreach (PhaseSite site in new List<PhaseSite>(occupiedPhaseSites))
    {
      PhaseableObject phaseableObject = site.phaseableObject;
      ClearSite(site);
      Destroyer.Destroy(phaseableObject.gameObject, "PhaseSiteDirector.ResetAllSites");
    }
  }

  public void RefreshTotalPhaseableObjects()
  {
    while (occupiedPhaseSites.Count > numberOfPhaseableObjects)
    {
      PhaseSite site = occupiedPhaseSites.ElementAt(0);
      PhaseableObject phaseableObject = site.phaseableObject;
      ClearSite(site);
      Destroyer.Destroy(phaseableObject.gameObject, "PhaseSiteDirector.RefreshTotalPhaseableObjects");
    }
    while (occupiedPhaseSites.Count < numberOfPhaseableObjects)
      PlacePhaseObject(PickRandomAvailableSite());
  }
}
