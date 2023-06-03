// Decompiled with JetBrains decompiler
// Type: SECTR_TriggerLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SECTR/Stream/SECTR Trigger Loader")]
public class SECTR_TriggerLoader : SECTR_Loader
{
  protected int loadedCount;
  protected bool chunksReferenced;
  [SECTR_ToolTip("List of Sectors to load when entering this trigger.")]
  public List<SECTR_Sector> Sectors = new List<SECTR_Sector>();
  [SECTR_ToolTip("Should the Sectors be unloaded when trigger is exited.")]
  public bool UnloadOnExit = true;

  public override bool Loaded
  {
    get
    {
      bool loaded = true;
      int count = Sectors.Count;
      for (int index = 0; index < count & loaded; ++index)
      {
        SECTR_Sector sector = Sectors[index];
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

  private void OnDisable()
  {
    _RefChunks();
    loadedCount = 0;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (loadedCount == 0)
      _RefChunks();
    ++loadedCount;
  }

  private void OnTriggerExit(Collider other)
  {
    if (loadedCount <= 0)
      return;
    --loadedCount;
    if (loadedCount != 0 || !UnloadOnExit)
      return;
    _UnrefChunks();
  }

  private void _RefChunks()
  {
    if (chunksReferenced)
      return;
    int count = Sectors.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Sector sector = Sectors[index];
      if ((bool) (Object) sector)
      {
        SECTR_Chunk component = sector.GetComponent<SECTR_Chunk>();
        if ((bool) (Object) component)
          component.AddReference();
      }
    }
    chunksReferenced = true;
  }

  private void _UnrefChunks()
  {
    if (!chunksReferenced)
      return;
    int count = Sectors.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Sector sector = Sectors[index];
      if ((bool) (Object) sector)
      {
        SECTR_Chunk component = sector.GetComponent<SECTR_Chunk>();
        if ((bool) (Object) component)
          component.RemoveReference();
      }
    }
    chunksReferenced = false;
  }
}
