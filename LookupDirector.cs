// Decompiled with JetBrains decompiler
// Type: LookupDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookupDirector : SRBehaviour
{
  [SerializeField]
  private SlimeAppearanceDirector slimeAppearanceDirector;
  [SerializeField]
  private PrefabList identifiablePrefabs;
  [SerializeField]
  private PrefabList plotPrefabs;
  [SerializeField]
  private PrefabList resourceSpawnerPrefabs;
  [SerializeField]
  private GadgetDefinitionList gadgetDefinitions;
  [SerializeField]
  private VacItemDefinitionList vacItemDefinitions;
  [SerializeField]
  private LiquidDefinitionList liquidDefinitions;
  [SerializeField]
  private UpgradeDefinitionList upgradeDefinitions;
  [SerializeField]
  private PrefabList gordoEntries;
  [SerializeField]
  private ToyDefinitionList toyDefinitions;
  private readonly List<GadgetDefinition> gadgetDefinitionsDynamic = new List<GadgetDefinition>();
  private readonly List<VacItemDefinition> vacItemDefinitionsDynamic = new List<VacItemDefinition>();
  private Dictionary<Identifiable.Id, GameObject> identifiablePrefabDict = new Dictionary<Identifiable.Id, GameObject>(Identifiable.idComparer);
  private Dictionary<LandPlot.Id, GameObject> plotPrefabDict = new Dictionary<LandPlot.Id, GameObject>(LandPlot.idComparer);
  private Dictionary<SpawnResource.Id, GameObject> resourcePrefabDict = new Dictionary<SpawnResource.Id, GameObject>(SpawnResource.idComparer);
  private Dictionary<Gadget.Id, GadgetDefinition> gadgetDefinitionDict = new Dictionary<Gadget.Id, GadgetDefinition>(Gadget.idComparer);
  private Dictionary<Identifiable.Id, VacItemDefinition> vacItemDict = new Dictionary<Identifiable.Id, VacItemDefinition>(Identifiable.idComparer);
  private Dictionary<Identifiable.Id, LiquidDefinition> liquidDict = new Dictionary<Identifiable.Id, LiquidDefinition>(Identifiable.idComparer);
  private Dictionary<PlayerState.Upgrade, UpgradeDefinition> upgradeDefinitionDict = new Dictionary<PlayerState.Upgrade, UpgradeDefinition>(PlayerState.upgradeComparer);
  private Dictionary<Identifiable.Id, GameObject> gordoDict = new Dictionary<Identifiable.Id, GameObject>(Identifiable.idComparer);
  private Dictionary<Identifiable.Id, ToyDefinition> toyDict = new Dictionary<Identifiable.Id, ToyDefinition>(Identifiable.idComparer);

  public IEnumerable<GameObject> PlotPrefabs => plotPrefabs;

  public IEnumerable<GadgetDefinition> GadgetDefinitions => gadgetDefinitions.Concat(gadgetDefinitionsDynamic);

  public IEnumerable<VacItemDefinition> VacItemDefinitions => vacItemDefinitions.Concat(vacItemDefinitionsDynamic);

  public IEnumerable<GameObject> GordoEntries => gordoEntries;

  public void Awake()
  {
    identifiablePrefabDict.Clear();
    foreach (GameObject identifiablePrefab in identifiablePrefabs)
    {
      if (!(identifiablePrefab == null))
      {
        Identifiable component = identifiablePrefab.GetComponent<Identifiable>();
        if (identifiablePrefabDict.ContainsKey(component.id))
          Log.Error("LookupDirector Duplicate Identifiable ID: " + component.id);
        identifiablePrefabDict[component.id] = identifiablePrefab;
      }
    }
    plotPrefabDict.Clear();
    foreach (GameObject plotPrefab in plotPrefabs)
    {
      if (!(plotPrefab == null))
      {
        LandPlot component = plotPrefab.GetComponent<LandPlot>();
        if (plotPrefabDict.ContainsKey(component.typeId))
          Log.Error("LookupDirector Duplicate Plot ID: " + component.typeId);
        plotPrefabDict[component.typeId] = plotPrefab;
      }
    }
    resourcePrefabDict.Clear();
    foreach (GameObject resourceSpawnerPrefab in resourceSpawnerPrefabs)
    {
      if (!(resourceSpawnerPrefab == null))
      {
        SpawnResource component = resourceSpawnerPrefab.GetComponent<SpawnResource>();
        if (resourcePrefabDict.ContainsKey(component.id))
          Log.Error("LookupDirector Duplicate Resource ID: " + component.id);
        resourcePrefabDict[component.id] = resourceSpawnerPrefab;
      }
    }
    gadgetDefinitionDict.Clear();
    foreach (GadgetDefinition gadgetDefinition in gadgetDefinitions)
    {
      if (!(gadgetDefinition.prefab == null))
      {
        Gadget component = gadgetDefinition.prefab.GetComponent<Gadget>();
        if (gadgetDefinitionDict.ContainsKey(component.id))
          Log.Error("LookupDirector Duplicate Gadget ID: " + component.id);
        if (gadgetDefinition.id != component.id)
          Log.Error("LookupDirector Mismatch Gadget.", "entryId", gadgetDefinition.id, "gadgetId", component.id);
        gadgetDefinitionDict[component.id] = gadgetDefinition;
      }
    }
    vacItemDict.Clear();
    foreach (VacItemDefinition vacItemDefinition in vacItemDefinitions)
    {
      if (vacItemDict.ContainsKey(vacItemDefinition.Id))
        Log.Error("LookupDirector Duplicate Vac Item Definition ID: " + vacItemDefinition.Id);
      vacItemDict[vacItemDefinition.Id] = vacItemDefinition;
    }
    liquidDict.Clear();
    foreach (LiquidDefinition liquidDefinition in liquidDefinitions)
    {
      if (liquidDict.ContainsKey(liquidDefinition.Id))
        Log.Error("LookupDirector Duplicate Liquid ID: " + liquidDefinition.Id);
      liquidDict[liquidDefinition.Id] = liquidDefinition;
    }
    upgradeDefinitionDict.Clear();
    foreach (UpgradeDefinition upgradeDefinition in upgradeDefinitions)
    {
      if (upgradeDefinitionDict.ContainsKey(upgradeDefinition.Upgrade))
        Log.Error("LookupDirector Duplicate Upgrade ID: " + upgradeDefinition.Upgrade);
      upgradeDefinitionDict[upgradeDefinition.Upgrade] = upgradeDefinition;
    }
    gordoDict.Clear();
    foreach (GameObject gordoEntry in gordoEntries)
    {
      GordoIdentifiable component = gordoEntry.GetComponent<GordoIdentifiable>();
      if (gordoDict.ContainsKey(component.id))
        Log.Error("LookupDirector Duplicate Gordo ID: " + component.id);
      else
        gordoDict.Add(component.id, gordoEntry);
    }
    toyDict.Clear();
    foreach (ToyDefinition toyDefinition in toyDefinitions)
      toyDict.Add(toyDefinition.ToyId, toyDefinition);
  }

  public GameObject GetPrefab(Identifiable.Id id)
  {
    if (!identifiablePrefabDict.ContainsKey(id))
    {
      Log.Error("Missing prefab: " + id + " hasIdsCount: " + identifiablePrefabDict.Count);
      return null;
    }
    if (!(identifiablePrefabDict[id] == null))
      return identifiablePrefabDict[id];
    Log.Error("No prefab wired up for identifiable", nameof (id), id);
    return null;
  }

  public GameObject GetPlotPrefab(LandPlot.Id id) => plotPrefabDict[id];

  public GameObject GetResourcePrefab(SpawnResource.Id id)
  {
    try
    {
      return resourcePrefabDict[id];
    }
    catch (KeyNotFoundException ex)
    {
      throw new KeyNotFoundException(string.Format("Failed to find spawn resource entry: {0}", id), ex);
    }
  }

  public GadgetDefinition GetGadgetDefinition(Gadget.Id id)
  {
    try
    {
      return gadgetDefinitionDict[id];
    }
    catch (KeyNotFoundException ex)
    {
      throw new KeyNotFoundException(string.Format("Failed to find gadget definition: {0}", id), ex);
    }
  }

  public bool HasGadgetDefinition(Gadget.Id id) => gadgetDefinitionDict.ContainsKey(id);

  public void RegisterFashion(GameObject prefab, VacItemDefinition vac, GadgetDefinition gadget)
  {
    identifiablePrefabDict[vac.Id] = prefab;
    vacItemDict[vac.Id] = vac;
    vacItemDefinitionsDynamic.RemoveAll(e => e.Id == vac.Id);
    vacItemDefinitionsDynamic.Add(vac);
    gadgetDefinitionDict[gadget.id] = gadget;
    gadgetDefinitionsDynamic.RemoveAll(e => e.id == gadget.id);
    gadgetDefinitionsDynamic.Add(gadget);
  }

  public UpgradeDefinition GetUpgradeDefinition(PlayerState.Upgrade upgrade) => upgradeDefinitionDict[upgrade];

  public ToyDefinition GetToyDefinition(Identifiable.Id id) => toyDict[id];

  public void RegisterToy(ToyDefinition entry, GameObject prefab)
  {
    identifiablePrefabDict[entry.ToyId] = prefab;
    toyDict[entry.ToyId] = entry;
  }

  public Color GetColor(Identifiable.Id id)
  {
    VacItemDefinition vacItemDefinition;
    return vacItemDict.TryGetValue(id, out vacItemDefinition) ? vacItemDefinition.Color : Color.clear;
  }

  public Sprite GetIcon(Identifiable.Id id)
  {
    try
    {
      if (Identifiable.IsSlime(id))
        return slimeAppearanceDirector.GetCurrentSlimeIcon(id);
      if (id != Identifiable.Id.NONE)
        return vacItemDict[id].Icon;
    }
    catch (KeyNotFoundException ex)
    {
      Log.Error("Failed to find Identifiable Id when looking up icon.", "Id", id.ToString(), "Exception", ex);
    }
    return null;
  }

  public GameObject GetLiquidIncomingFX(Identifiable.Id id) => liquidDict.ContainsKey(id) ? liquidDict[id].InFx : null;

  public GameObject GetLiquidVacFailFX(Identifiable.Id id) => liquidDict.ContainsKey(id) ? liquidDict[id].VacFailFx : null;

  public GameObject GetGordo(Identifiable.Id id) => gordoDict.ContainsKey(id) ? gordoDict[id] : null;
}
