// Decompiled with JetBrains decompiler
// Type: SECTR_GroupLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SECTR_GroupLoader : SECTR_Loader
{
  [SECTR_ToolTip("The Sectors to load and unload together.")]
  public List<SECTR_Sector> Sectors = new List<SECTR_Sector>();

  public override bool Loaded
  {
    get
    {
      bool loaded = true;
      int count = Sectors.Count;
      for (int index = 0; index < count; ++index)
      {
        SECTR_Sector sector = Sectors[index];
        if ((bool) (Object) sector && sector.Frozen)
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

  private void OnEnable()
  {
    int count = Sectors.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Sector sector = Sectors[index];
      if ((bool) (Object) sector)
      {
        SECTR_Chunk component = sector.GetComponent<SECTR_Chunk>();
        if ((bool) (Object) component)
          component.ReferenceChange += ChunkChanged;
      }
    }
  }

  private void OnDisable()
  {
    int count = Sectors.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Sector sector = Sectors[index];
      if ((bool) (Object) sector)
      {
        SECTR_Chunk component = sector.GetComponent<SECTR_Chunk>();
        if ((bool) (Object) component)
          component.ReferenceChange -= ChunkChanged;
      }
    }
  }

  private void ChunkChanged(SECTR_Chunk source, bool loaded)
  {
    int count = Sectors.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Sector sector = Sectors[index];
      if ((bool) (Object) sector)
      {
        SECTR_Chunk component = sector.GetComponent<SECTR_Chunk>();
        if ((bool) (Object) component && component != source)
        {
          component.ReferenceChange -= ChunkChanged;
          if (loaded)
            component.AddReference();
          else
            component.RemoveReference();
          component.ReferenceChange += ChunkChanged;
        }
      }
    }
  }
}
