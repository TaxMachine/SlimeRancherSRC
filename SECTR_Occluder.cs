// Decompiled with JetBrains decompiler
// Type: SECTR_Occluder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SECTR_Member))]
[AddComponentMenu("SECTR/Vis/SECTR Occluder")]
public class SECTR_Occluder : SECTR_Hull
{
  private SECTR_Member cachedMember;
  private List<SECTR_Sector> currentSectors = new List<SECTR_Sector>(4);
  private static List<SECTR_Occluder> allOccluders = new List<SECTR_Occluder>(32);
  private static Dictionary<SECTR_Sector, List<SECTR_Occluder>> occluderTable = new Dictionary<SECTR_Sector, List<SECTR_Occluder>>(32);
  [SECTR_ToolTip("The axes that should orient towards the camera during culling (if any).")]
  public OrientationAxis AutoOrient;

  public static List<SECTR_Occluder> All => allOccluders;

  public static List<SECTR_Occluder> GetOccludersInSector(SECTR_Sector sector)
  {
    List<SECTR_Occluder> occludersInSector = null;
    occluderTable.TryGetValue(sector, out occludersInSector);
    return occludersInSector;
  }

  public SECTR_Member Member => cachedMember;

  public Vector3 MeshNormal
  {
    get
    {
      ComputeVerts();
      return meshNormal;
    }
  }

  public Matrix4x4 GetCullingMatrix(Vector3 cameraPos)
  {
    if (AutoOrient == OrientationAxis.None)
      return transform.localToWorldMatrix;
    ComputeVerts();
    Vector3 position = transform.position;
    Vector3 toDirection = cameraPos - position;
    switch (AutoOrient)
    {
      case OrientationAxis.XZ:
        toDirection.y = 0.0f;
        break;
      case OrientationAxis.XY:
        toDirection.z = 0.0f;
        break;
      case OrientationAxis.YZ:
        toDirection.x = 0.0f;
        break;
    }
    return Matrix4x4.TRS(position, Quaternion.FromToRotation(meshNormal, toDirection), transform.lossyScale);
  }

  private void OnEnable()
  {
    cachedMember = GetComponent<SECTR_Member>();
    cachedMember.Changed += _MembershipChanged;
    allOccluders.Add(this);
  }

  private void OnDisable()
  {
    allOccluders.Remove(this);
    cachedMember.Changed -= _MembershipChanged;
    cachedMember = null;
  }

  private void _MembershipChanged(List<SECTR_Sector> left, List<SECTR_Sector> joined)
  {
    if (joined != null)
    {
      int count = joined.Count;
      for (int index = 0; index < count; ++index)
      {
        SECTR_Sector key = joined[index];
        if ((bool) (Object) key)
        {
          List<SECTR_Occluder> sectrOccluderList;
          if (!occluderTable.TryGetValue(key, out sectrOccluderList))
          {
            sectrOccluderList = new List<SECTR_Occluder>(4);
            occluderTable[key] = sectrOccluderList;
          }
          sectrOccluderList.Add(this);
          currentSectors.Add(key);
        }
      }
    }
    if (left == null)
      return;
    int count1 = left.Count;
    for (int index = 0; index < count1; ++index)
    {
      SECTR_Sector key = left[index];
      if ((bool) (Object) key && currentSectors.Contains(key))
      {
        List<SECTR_Occluder> sectrOccluderList;
        if (occluderTable.TryGetValue(key, out sectrOccluderList))
          sectrOccluderList.Remove(this);
        currentSectors.Remove(key);
      }
    }
  }

  public enum OrientationAxis
  {
    None,
    XYZ,
    XZ,
    XY,
    YZ,
  }
}
