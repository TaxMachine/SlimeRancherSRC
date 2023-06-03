// Decompiled with JetBrains decompiler
// Type: SECTR_NeighborLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SECTR_Member))]
[AddComponentMenu("SECTR/Stream/SECTR Neighbor Loader")]
public class SECTR_NeighborLoader : SECTR_Loader
{
  private SECTR_Member cachedMember;
  private List<SECTR_Sector> currentSectors = new List<SECTR_Sector>(4);
  private List<SECTR_Graph.Node> neighbors = new List<SECTR_Graph.Node>(8);
  [SECTR_ToolTip("Determines how far out to load neighbor sectors from the current sector. Depth of 0 means only the current Sector.")]
  public int MaxDepth = 1;

  public override bool Loaded
  {
    get
    {
      bool loaded = true;
      int count = currentSectors.Count;
      for (int index = 0; index < count & loaded; ++index)
      {
        SECTR_Sector currentSector = currentSectors[index];
        if (currentSector.Frozen)
        {
          SECTR_Chunk component = currentSector.GetComponent<SECTR_Chunk>();
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
    cachedMember = GetComponent<SECTR_Member>();
    cachedMember.Changed += _MembershipChanged;
  }

  private void OnDisable()
  {
    cachedMember.Changed -= _MembershipChanged;
    if (currentSectors.Count <= 0)
      return;
    _MembershipChanged(currentSectors, null);
  }

  private void Start() => LockSelf(true);

  private void Update()
  {
    if (!locked || !Loaded)
      return;
    LockSelf(false);
  }

  private void _MembershipChanged(List<SECTR_Sector> left, List<SECTR_Sector> joined)
  {
    if (joined != null)
    {
      int count1 = joined.Count;
      for (int index1 = 0; index1 < count1; ++index1)
      {
        SECTR_Sector root = joined[index1];
        if ((bool) (Object) root && !currentSectors.Contains(root))
        {
          SECTR_Graph.BreadthWalk(ref neighbors, root, 0, MaxDepth);
          int count2 = neighbors.Count;
          for (int index2 = 0; index2 < count2; ++index2)
          {
            SECTR_Chunk component = neighbors[index2].Sector.GetComponent<SECTR_Chunk>();
            if ((bool) (Object) component)
              component.AddReference();
          }
          currentSectors.Add(root);
        }
      }
    }
    if (left == null)
      return;
    int count3 = left.Count;
    for (int index3 = 0; index3 < count3; ++index3)
    {
      SECTR_Sector root = left[index3];
      if ((bool) (Object) root && currentSectors.Contains(root))
      {
        SECTR_Graph.BreadthWalk(ref neighbors, root, 0, MaxDepth);
        int count4 = neighbors.Count;
        for (int index4 = 0; index4 < count4; ++index4)
        {
          SECTR_Chunk component = neighbors[index4].Sector.GetComponent<SECTR_Chunk>();
          if ((bool) (Object) component)
            component.RemoveReference();
        }
        currentSectors.Remove(root);
      }
    }
  }
}
