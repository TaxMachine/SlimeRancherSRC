// Decompiled with JetBrains decompiler
// Type: SECTR_Sector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("SECTR/Core/SECTR Sector")]
public class SECTR_Sector : SECTR_Member
{
  private List<SECTR_Portal> portals = new List<SECTR_Portal>(8);
  private List<SECTR_Member> members = new List<SECTR_Member>(32);
  private bool visited;
  private static List<SECTR_Sector> allSectors = new List<SECTR_Sector>(128);
  private static Dictionary<SectorSetId, BoundsQuadtree<SECTR_Sector>> sectorsTrees = new Dictionary<SectorSetId, BoundsQuadtree<SECTR_Sector>>(SectorSetIdComparer.Instance)
  {
    {
      SectorSetId.HOME,
      new BoundsQuadtree<SECTR_Sector>(1000f, Vector3.zero, 250f, 1.2f)
    },
    {
      SectorSetId.DESERT,
      new BoundsQuadtree<SECTR_Sector>(1000f, Vector3.up * 1000f, 250f, 1.2f)
    }
  };
  private static Dictionary<SectorSetId, List<GameObject>> managedWithSets = new Dictionary<SectorSetId, List<GameObject>>();
  private static BoundsQuadtree<SECTR_Sector> currSectorsTree = null;
  [SECTR_ToolTip("The terrain Sector attached on the top side of this Sector.")]
  public SECTR_Sector TopTerrain;
  [SECTR_ToolTip("The terrain Sector attached on the bottom side of this Sector.")]
  public SECTR_Sector BottomTerrain;
  [SECTR_ToolTip("The terrain Sector attached on the left side of this Sector.")]
  public SECTR_Sector LeftTerrain;
  [SECTR_ToolTip("The terrain Sector attached on the right side of this Sector.")]
  public SECTR_Sector RightTerrain;
  public CellDirector cellDir;

  private SECTR_Sector() => isSector = true;

  public static List<SECTR_Sector> All => allSectors;

  public static SectorSetId GetSectorSetId() => currSectorsTree == sectorsTrees[SectorSetId.DESERT] ? SectorSetId.DESERT : SectorSetId.HOME;

  public static void SetCurrSectorSet(SectorSetId setId, bool forceActivation = false)
  {
    if (!forceActivation && currSectorsTree == sectorsTrees[setId])
      return;
    foreach (KeyValuePair<SectorSetId, BoundsQuadtree<SECTR_Sector>> sectorsTree in sectorsTrees)
    {
      bool canProxy = sectorsTree.Key == setId;
      List<SECTR_Sector> result = new List<SECTR_Sector>();
      result = sectorsTree.Value.GetAll(ref result);
      foreach (Component component in result)
        component.GetComponent<SECTR_Chunk>().SetCanProxy(canProxy);
      if (managedWithSets.ContainsKey(sectorsTree.Key))
      {
        foreach (GameObject gameObject in managedWithSets[sectorsTree.Key])
          gameObject.SetActive(canProxy);
      }
    }
    currSectorsTree = sectorsTrees[setId];
  }

  public static void SetCurrSectorSetForPos(Vector3 pos) => SetCurrSectorSet(GetSectorSetForPos(pos));

  public static SectorSetId GetSectorSetForPos(Vector3 pos) => pos.y > 900.0 ? SectorSetId.DESERT : SectorSetId.HOME;

  public static bool IsCurrSectorSet(SectorSetId setId) => currSectorsTree == null || currSectorsTree == sectorsTrees[setId];

  public static void ManageWithSectorSet(GameObject obj, SectorSetId setId)
  {
    if (!managedWithSets.ContainsKey(setId))
      managedWithSets[setId] = new List<GameObject>();
    managedWithSets[setId].Add(obj);
  }

  public static void ReleaseFromSectorSet(GameObject obj, SectorSetId setId)
  {
    if (!managedWithSets.ContainsKey(setId))
      return;
    managedWithSets[setId].Remove(obj);
  }

  public static void GetContaining(ref List<SECTR_Sector> sectors, Vector3 position)
  {
    sectors.Clear();
    int count = allSectors.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Sector allSector = allSectors[index];
      if (allSector.TotalBounds.Contains(position))
        sectors.Add(allSector);
    }
  }

  public static void GetContaining(
    ref List<SECTR_Sector> sectors,
    Bounds bounds,
    bool checkAllSectorSets = false)
  {
    sectors.Clear();
    if (checkAllSectorSets)
    {
      foreach (BoundsQuadtree<SECTR_Sector> boundsQuadtree in sectorsTrees.Values)
        boundsQuadtree.GetColliding(bounds, ref sectors);
    }
    else
    {
      if (currSectorsTree == null)
        return;
      currSectorsTree.GetColliding(bounds, ref sectors);
    }
  }

  public bool Visited
  {
    get => visited;
    set => visited = value;
  }

  public List<SECTR_Portal> Portals => portals;

  public List<SECTR_Member> Members => members;

  public void ConnectTerrainNeighbors()
  {
    Terrain componentInChildren = GetComponentInChildren<Terrain>();
    if (!(bool) (Object) componentInChildren)
      return;
    componentInChildren.SetNeighbors((bool) (Object) LeftTerrain ? LeftTerrain.GetComponentInChildren<Terrain>() : null, (bool) (Object) TopTerrain ? TopTerrain.GetComponentInChildren<Terrain>() : null, (bool) (Object) RightTerrain ? RightTerrain.GetComponentInChildren<Terrain>() : null, (bool) (Object) BottomTerrain ? BottomTerrain.GetComponentInChildren<Terrain>() : null);
  }

  public void DisonnectTerrainNeighbors()
  {
    Terrain componentInChildren1 = GetComponentInChildren<Terrain>();
    if ((bool) (Object) componentInChildren1)
      componentInChildren1.SetNeighbors(null, null, null, null);
    if ((bool) (Object) TopTerrain)
    {
      Terrain componentInChildren2 = TopTerrain.GetComponentInChildren<Terrain>();
      if ((bool) (Object) componentInChildren2)
        componentInChildren2.SetNeighbors((bool) (Object) TopTerrain.LeftTerrain ? TopTerrain.LeftTerrain.GetComponentInChildren<Terrain>() : null, (bool) (Object) TopTerrain.TopTerrain ? TopTerrain.TopTerrain.GetComponentInChildren<Terrain>() : null, (bool) (Object) TopTerrain.RightTerrain ? TopTerrain.RightTerrain.GetComponentInChildren<Terrain>() : null, null);
    }
    if ((bool) (Object) BottomTerrain)
    {
      Terrain componentInChildren3 = BottomTerrain.GetComponentInChildren<Terrain>();
      if ((bool) (Object) componentInChildren3)
        componentInChildren3.SetNeighbors((bool) (Object) BottomTerrain.LeftTerrain ? BottomTerrain.LeftTerrain.GetComponentInChildren<Terrain>() : null, null, (bool) (Object) BottomTerrain.RightTerrain ? BottomTerrain.RightTerrain.GetComponentInChildren<Terrain>() : null, (bool) (Object) BottomTerrain.BottomTerrain ? BottomTerrain.BottomTerrain.GetComponentInChildren<Terrain>() : null);
    }
    if ((bool) (Object) LeftTerrain)
    {
      Terrain componentInChildren4 = LeftTerrain.GetComponentInChildren<Terrain>();
      if ((bool) (Object) componentInChildren4)
        componentInChildren4.SetNeighbors((bool) (Object) LeftTerrain.LeftTerrain ? LeftTerrain.LeftTerrain.GetComponentInChildren<Terrain>() : null, (bool) (Object) LeftTerrain.TopTerrain ? LeftTerrain.TopTerrain.GetComponentInChildren<Terrain>() : null, null, (bool) (Object) LeftTerrain.BottomTerrain ? LeftTerrain.BottomTerrain.GetComponentInChildren<Terrain>() : null);
    }
    if (!(bool) (Object) RightTerrain)
      return;
    Terrain componentInChildren5 = RightTerrain.GetComponentInChildren<Terrain>();
    if (!(bool) (Object) componentInChildren5)
      return;
    componentInChildren5.SetNeighbors(null, (bool) (Object) RightTerrain.TopTerrain ? RightTerrain.TopTerrain.GetComponentInChildren<Terrain>() : null, (bool) (Object) RightTerrain.RightTerrain ? RightTerrain.RightTerrain.GetComponentInChildren<Terrain>() : null, (bool) (Object) RightTerrain.BottomTerrain ? RightTerrain.BottomTerrain.GetComponentInChildren<Terrain>() : null);
  }

  public void Register(SECTR_Portal portal)
  {
    if (portals.Contains(portal))
      return;
    portals.Add(portal);
  }

  public void Deregister(SECTR_Portal portal) => portals.Remove(portal);

  public void Register(SECTR_Member member) => members.Add(member);

  public void Deregister(SECTR_Member member) => members.Remove(member);

  public override void Awake()
  {
    base.Awake();
    cellDir = GetComponent<CellDirector>();
    if (!Application.isPlaying || currSectorsTree != null)
      return;
    SetCurrSectorSet(SectorSetId.HOME, true);
  }

  protected override void OnEnable()
  {
    allSectors.Add(this);
    SectorSetId setId = GetSetId();
    sectorsTrees[setId].Add(this, TotalBounds);
    if (Application.isPlaying)
      GetComponent<SECTR_Chunk>().SetCanProxy(currSectorsTree == sectorsTrees[setId]);
    if ((bool) (Object) TopTerrain || (bool) (Object) BottomTerrain || (bool) (Object) RightTerrain || (bool) (Object) LeftTerrain)
      ConnectTerrainNeighbors();
    base.OnEnable();
  }

  protected override void OnDisable()
  {
    List<SECTR_Member> sectrMemberList = new List<SECTR_Member>(members);
    int count = sectrMemberList.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Member sectrMember = sectrMemberList[index];
      if ((bool) (Object) sectrMember)
        sectrMember.SectorDisabled(this);
    }
    allSectors.Remove(this);
    sectorsTrees[GetSetId()].Remove(this);
    base.OnDisable();
  }

  public SectorSetId GetSetId()
  {
    ZoneDirector[] componentsInParent = GetComponentsInParent<ZoneDirector>(true);
    return componentsInParent.Length == 0 || componentsInParent[0].zone != ZoneDirector.Zone.DESERT ? SectorSetId.HOME : SectorSetId.DESERT;
  }

  public override void NonOffsetLateUpdate()
  {
    Bounds totalBounds1 = TotalBounds;
    base.NonOffsetLateUpdate();
    Bounds totalBounds2 = TotalBounds;
    if (!(totalBounds1 != totalBounds2))
      return;
    SectorSetId setId = GetSetId();
    sectorsTrees[setId].Remove(this);
    sectorsTrees[setId].Add(this, TotalBounds);
  }

  public override void OffsetLateUpdate()
  {
    Bounds totalBounds1 = TotalBounds;
    base.OffsetLateUpdate();
    Bounds totalBounds2 = TotalBounds;
    if (!(totalBounds1 != totalBounds2))
      return;
    SectorSetId setId = GetSetId();
    sectorsTrees[setId].Remove(this);
    sectorsTrees[setId].Add(this, TotalBounds);
  }

  public enum SectorSetId
  {
    UNSET = -1, // 0xFFFFFFFF
    HOME = 0,
    DESERT = 1,
  }

  private class SectorSetIdComparer : IEqualityComparer<SectorSetId>
  {
    public static readonly SectorSetIdComparer Instance = new SectorSetIdComparer();

    public bool Equals(SectorSetId x, SectorSetId y) => x == y;

    public int GetHashCode(SectorSetId id) => (int) id;
  }
}
