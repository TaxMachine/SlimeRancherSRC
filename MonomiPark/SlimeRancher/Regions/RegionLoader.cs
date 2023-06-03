// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Regions.RegionLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonomiPark.SlimeRancher.Regions
{
  public class RegionLoader : SRBehaviour, PlayerModel.Participant
  {
    [Tooltip("The dimensions of the volume in which stuff should be unhibernated/awake.")]
    public Vector3 WakeSize = new Vector3(20f, 10f, 20f);
    [Tooltip("The dimensions of the volume in which regions should be loaded.")]
    public Vector3 LoadSize = new Vector3(20f, 10f, 20f);
    [Tooltip("The distance from the load size that you need to move for a Region to unload (as a percentage).")]
    [Range(0.0f, 1f)]
    public float UnloadBuffer = 0.1f;
    private Vector3 lastRegionCheckPos;
    private List<Region> nonProxiedRegions = new List<Region>(16);
    private List<Region> nonHibernatedRegions = new List<Region>(16);
    private RegionRegistry regionReg;
    private static List<Region> loadRegions = new List<Region>(16);
    private static List<Region> unloadRegions = new List<Region>(16);
    private const float REGION_UPDATE_DIST = 1f;
    private const float REGION_UPDATE_DIST_SQR = 1f;

    public void Awake()
    {
      regionReg = SRSingleton<SceneContext>.Instance.RegionRegistry;
      SRSingleton<SceneContext>.Instance.GameModel.RegisterPlayerParticipant(this);
    }

    public void RegionSetChanged(
      RegionRegistry.RegionSetId previous,
      RegionRegistry.RegionSetId current)
    {
      LoadRegion(current, previous.ToEnumerable());
    }

    public void Start()
    {
      RegionRegistry.RegionSetId current = regionReg.GetCurrentRegionSetId();
      LoadRegion(current, Enum.GetValues(typeof (RegionRegistry.RegionSetId)).Cast<RegionRegistry.RegionSetId>().Where(r => r != RegionRegistry.RegionSetId.UNSET).Where(r => r != current));
    }

    public void Update()
    {
      if ((transform.position - lastRegionCheckPos).sqrMagnitude < 1.0)
        return;
      ForceUpdate();
    }

    private void LoadRegion(
      RegionRegistry.RegionSetId current,
      IEnumerable<RegionRegistry.RegionSetId> previousEnumerable)
    {
      foreach (RegionRegistry.RegionSetId previous in previousEnumerable)
      {
        foreach (Region region in regionReg.GetRegions(previous))
          region.OnRegionSetDeactivated();
      }
      ForceUpdate();
      foreach (Region region in regionReg.GetRegions(current))
      {
        if (!nonProxiedRegions.Contains(region))
          region.RemoveNonProxiedReference();
      }
    }

    private void ForceUpdate()
    {
      UpdateProxied(transform.position);
      UpdateHibernated(transform.position);
      lastRegionCheckPos = transform.position;
    }

    private void UpdateProxied(Vector3 position)
    {
      Bounds bounds1 = new Bounds(position, LoadSize);
      Bounds bounds2 = new Bounds(position, LoadSize * (1f + UnloadBuffer));
      regionReg.GetContaining(ref loadRegions, bounds1);
      regionReg.GetContaining(ref unloadRegions, bounds2);
      int index1 = 0;
      int count1 = nonProxiedRegions.Count;
      while (index1 < count1)
      {
        Region nonProxiedRegion = nonProxiedRegions[index1];
        if (loadRegions.Contains(nonProxiedRegion))
        {
          loadRegions.Remove(nonProxiedRegion);
          ++index1;
        }
        else if (!unloadRegions.Contains(nonProxiedRegion))
        {
          nonProxiedRegion.RemoveNonProxiedReference();
          nonProxiedRegions.RemoveAt(index1);
          --count1;
        }
        else
          ++index1;
      }
      int count2 = loadRegions.Count;
      if (count2 <= 0)
        return;
      for (int index2 = 0; index2 < count2; ++index2)
      {
        Region loadRegion = loadRegions[index2];
        if (!nonProxiedRegions.Contains(loadRegion))
        {
          loadRegion.AddNonProxiedReference();
          nonProxiedRegions.Add(loadRegion);
        }
      }
    }

    private void UpdateHibernated(Vector3 position)
    {
      Bounds bounds1 = new Bounds(position, WakeSize);
      Bounds bounds2 = new Bounds(position, WakeSize * (1f + UnloadBuffer));
      regionReg.GetContaining(ref loadRegions, bounds1);
      regionReg.GetContaining(ref unloadRegions, bounds2);
      int index1 = 0;
      int count1 = nonHibernatedRegions.Count;
      while (index1 < count1)
      {
        Region hibernatedRegion = nonHibernatedRegions[index1];
        if (loadRegions.Contains(hibernatedRegion))
        {
          loadRegions.Remove(hibernatedRegion);
          ++index1;
        }
        else if (!unloadRegions.Contains(hibernatedRegion))
        {
          hibernatedRegion.RemoveNonHibernateReference();
          nonHibernatedRegions.RemoveAt(index1);
          --count1;
        }
        else
          ++index1;
      }
      int count2 = loadRegions.Count;
      if (count2 <= 0)
        return;
      for (int index2 = 0; index2 < count2; ++index2)
      {
        Region loadRegion = loadRegions[index2];
        if (!nonHibernatedRegions.Contains(loadRegion))
        {
          loadRegion.AddNonHibernateReference();
          nonHibernatedRegions.Add(loadRegion);
        }
      }
    }

    public void InitModel(PlayerModel model)
    {
    }

    public void SetModel(PlayerModel model)
    {
    }

    public void TransformChanged(Vector3 pos, Quaternion rot)
    {
    }

    public void RegisteredPotentialAmmoChanged(
      Dictionary<PlayerState.AmmoMode, List<GameObject>> registeredPotentialAmmo)
    {
    }

    public void KeyAdded()
    {
    }
  }
}
