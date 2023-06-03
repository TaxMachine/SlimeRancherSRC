// Decompiled with JetBrains decompiler
// Type: SECTR_Portal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("SECTR/Core/SECTR Portal")]
public class SECTR_Portal : SECTR_Hull
{
  [SerializeField]
  [HideInInspector]
  private SECTR_Sector frontSector;
  [SerializeField]
  [HideInInspector]
  private SECTR_Sector backSector;
  private bool visited;
  private static List<SECTR_Portal> allPortals = new List<SECTR_Portal>(128);
  [SECTR_ToolTip("Flags for this Portal. Used in graph traversals and the like.", null, typeof (PortalFlags))]
  public PortalFlags Flags;

  public static List<SECTR_Portal> All => allPortals;

  public SECTR_Sector FrontSector
  {
    set
    {
      if (!(frontSector != value))
        return;
      if ((bool) (Object) frontSector)
        frontSector.Deregister(this);
      frontSector = value;
      if (!(bool) (Object) frontSector)
        return;
      frontSector.Register(this);
    }
    get => !(bool) (Object) frontSector || !frontSector.enabled ? null : frontSector;
  }

  public SECTR_Sector BackSector
  {
    set
    {
      if (!(backSector != value))
        return;
      if ((bool) (Object) backSector)
        backSector.Deregister(this);
      backSector = value;
      if (!(bool) (Object) backSector)
        return;
      backSector.Register(this);
    }
    get => !(bool) (Object) backSector || !backSector.enabled ? null : backSector;
  }

  public bool Visited
  {
    get => visited;
    set => visited = value;
  }

  public IEnumerable<SECTR_Sector> GetSectors()
  {
    yield return FrontSector;
    yield return BackSector;
  }

  public void SetFlag(PortalFlags flag, bool on)
  {
    if (on)
      Flags |= flag;
    else
      Flags &= ~flag;
  }

  private void OnEnable()
  {
    allPortals.Add(this);
    if ((bool) (Object) frontSector)
      frontSector.Register(this);
    if (!(bool) (Object) backSector)
      return;
    backSector.Register(this);
  }

  private void OnDisable()
  {
    allPortals.Remove(this);
    if ((bool) (Object) frontSector)
      frontSector.Deregister(this);
    if (!(bool) (Object) backSector)
      return;
    backSector.Deregister(this);
  }

  [System.Flags]
  public enum PortalFlags
  {
    Closed = 1,
    Locked = 2,
    PassThrough = 4,
  }
}
