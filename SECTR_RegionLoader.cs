// Decompiled with JetBrains decompiler
// Type: SECTR_RegionLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SECTR/Stream/SECTR Region Loader")]
public class SECTR_RegionLoader : SECTR_Loader
{
  private List<SECTR_Sector> sectors = new List<SECTR_Sector>(16);
  private List<SECTR_Sector> wakeSectors = new List<SECTR_Sector>(16);
  private List<SECTR_Sector> loadSectors = new List<SECTR_Sector>(16);
  private List<SECTR_Sector> unloadSectors = new List<SECTR_Sector>(16);
  private bool firstRegionCheck = true;
  private Vector3 lastRegionCheckPos;
  private const float REGION_UPDATE_DIST = 1f;
  private const float REGION_UPDATE_DIST_SQR = 1f;
  [SECTR_ToolTip("The dimensions of the volume in which stuff should be unhibernated/awake.")]
  public Vector3 WakeSize = new Vector3(20f, 10f, 20f);
  [SECTR_ToolTip("The dimensions of the volume in which terrain chunks should be loaded.")]
  public Vector3 LoadSize = new Vector3(20f, 10f, 20f);
  [SECTR_ToolTip("The distance from the load size that you need to move for a Sector to unload (as a percentage).", 0.0f, 1f)]
  public float UnloadBuffer = 0.1f;
  [SECTR_ToolTip("If set, will only load Sectors in matching layers.")]
  public LayerMask LayersToLoad = (LayerMask) -1;

  public static int LayerMask { get; set; }

  public override bool Loaded
  {
    get
    {
      bool loaded = true;
      int count = sectors.Count;
      for (int index = 0; index < count & loaded; ++index)
      {
        SECTR_Sector sector = sectors[index];
        if (sector.Frozen)
        {
          SECTR_Chunk component = sector.GetComponent<SECTR_Chunk>();
          if ((bool) (Object) component && !component.IsLoaded())
          {
            loaded = false;
            break;
          }
        }
      }
      return loaded;
    }
  }

  private void Start() => LockSelf(true);

  private void OnDisable()
  {
    int count = sectors.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Sector sector = sectors[index];
      if ((bool) (Object) sector)
      {
        SECTR_Chunk component = sector.GetComponent<SECTR_Chunk>();
        if ((bool) (Object) component)
          component.RemoveReference();
      }
    }
    sectors.Clear();
  }

  private void Update()
  {
    Vector3 position = transform.position;
    if (firstRegionCheck || (position - lastRegionCheckPos).sqrMagnitude >= 1.0)
    {
      firstRegionCheck = false;
      lastRegionCheckPos = position;
      Bounds bounds1 = new Bounds(position, LoadSize);
      Bounds bounds2 = new Bounds(position, LoadSize * (1f + UnloadBuffer));
      SECTR_Sector.GetContaining(ref loadSectors, bounds1);
      SECTR_Sector.GetContaining(ref unloadSectors, bounds2);
      int index1 = 0;
      int count1 = sectors.Count;
      while (index1 < count1)
      {
        SECTR_Sector sector = sectors[index1];
        if (loadSectors.Contains(sector))
        {
          loadSectors.Remove(sector);
          ++index1;
        }
        else if (!unloadSectors.Contains(sector))
        {
          SECTR_Chunk component = sector.GetComponent<SECTR_Chunk>();
          if ((bool) (Object) component)
            component.RemoveReference();
          sectors.RemoveAt(index1);
          --count1;
        }
        else
          ++index1;
      }
      int count2 = loadSectors.Count;
      int num = LayersToLoad.value;
      if (count2 > 0)
      {
        for (int index2 = 0; index2 < count2; ++index2)
        {
          SECTR_Sector loadSector = loadSectors[index2];
          if (loadSector.Frozen && (num & 1 << loadSector.gameObject.layer) != 0)
          {
            SECTR_Chunk component = loadSector.GetComponent<SECTR_Chunk>();
            if ((bool) (Object) component)
              component.AddReference();
            sectors.Add(loadSector);
          }
        }
      }
      UpdateAwake();
    }
    if (!locked || !Loaded)
      return;
    LockSelf(false);
  }

  private void UpdateAwake()
  {
    Vector3 position = transform.position;
    Bounds bounds1 = new Bounds(position, WakeSize);
    Bounds bounds2 = new Bounds(position, WakeSize * (1f + UnloadBuffer));
    SECTR_Sector.GetContaining(ref loadSectors, bounds1);
    SECTR_Sector.GetContaining(ref unloadSectors, bounds2);
    int index1 = 0;
    int count1 = wakeSectors.Count;
    while (index1 < count1)
    {
      SECTR_Sector wakeSector = wakeSectors[index1];
      if (loadSectors.Contains(wakeSector))
      {
        loadSectors.Remove(wakeSector);
        ++index1;
      }
      else if (!unloadSectors.Contains(wakeSector))
      {
        SECTR_Chunk component = wakeSector.GetComponent<SECTR_Chunk>();
        if ((bool) (Object) component)
          component.RemoveWakeReference();
        wakeSectors.RemoveAt(index1);
        --count1;
      }
      else
        ++index1;
    }
    int count2 = loadSectors.Count;
    int num = LayersToLoad.value;
    if (count2 <= 0)
      return;
    for (int index2 = 0; index2 < count2; ++index2)
    {
      SECTR_Sector loadSector = loadSectors[index2];
      if (loadSector.Hibernate && (num & 1 << loadSector.gameObject.layer) != 0)
      {
        SECTR_Chunk component = loadSector.GetComponent<SECTR_Chunk>();
        if ((bool) (Object) component)
          component.AddWakeReference();
        wakeSectors.Add(loadSector);
      }
    }
  }
}
