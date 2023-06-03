// Decompiled with JetBrains decompiler
// Type: SECTR_LOD
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof (SECTR_Member))]
public class SECTR_LOD : MonoBehaviour
{
  [SerializeField]
  [HideInInspector]
  private Vector3 boundsOffset;
  [SerializeField]
  [HideInInspector]
  private float boundsRadius;
  [SerializeField]
  [HideInInspector]
  private bool boundsUpdated;
  private int activeLOD = -1;
  private SECTR_Member cachedMember;
  private List<GameObject> toHide = new List<GameObject>(32);
  private List<LODEntry> toShow = new List<LODEntry>(32);
  private static List<SECTR_LOD> allLODs = new List<SECTR_LOD>(128);
  public List<LODSet> LODs = new List<LODSet>();

  public static List<SECTR_LOD> All => allLODs;

  public void SelectLOD(Camera renderCamera)
  {
    if (!(bool) (UnityEngine.Object) renderCamera)
      return;
    if (!boundsUpdated)
      _CalculateBounds();
    Vector3 b = transform.localToWorldMatrix.MultiplyPoint3x4(boundsOffset);
    float num1 = Vector3.Distance(renderCamera.transform.position, b);
    float num2 = (float) (boundsRadius / (Mathf.Tan((float) (renderCamera.fieldOfView * 0.5 * (Math.PI / 180.0))) * (double) num1) * 2.0);
    int lodIndex = -1;
    int count = LODs.Count;
    for (int index = 0; index < count; ++index)
    {
      float threshold = LODs[index].Threshold;
      if (index == activeLOD)
        threshold -= 0.05f;
      if (num2 >= (double) threshold)
      {
        lodIndex = index;
        break;
      }
    }
    if (lodIndex == activeLOD)
      return;
    _ActivateLOD(lodIndex);
  }

  private void OnEnable()
  {
    allLODs.Add(this);
    cachedMember = GetComponent<SECTR_Member>();
    SECTR_CullingCamera sectrCullingCamera = SECTR_CullingCamera.All.Count > 0 ? SECTR_CullingCamera.All[0] : null;
    if ((bool) (UnityEngine.Object) sectrCullingCamera)
      SelectLOD(sectrCullingCamera.GetComponent<Camera>());
    else
      _ActivateLOD(0);
  }

  private void OnDisable()
  {
    allLODs.Remove(this);
    cachedMember = null;
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.matrix = Matrix4x4.identity;
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.localToWorldMatrix.MultiplyPoint(boundsOffset), boundsRadius);
  }

  private void _ActivateLOD(int lodIndex)
  {
    toHide.Clear();
    toShow.Clear();
    if (activeLOD >= 0 && activeLOD < LODs.Count)
    {
      LODSet loD = LODs[activeLOD];
      int count = loD.LODEntries.Count;
      for (int index = 0; index < count; ++index)
      {
        LODEntry lodEntry = loD.LODEntries[index];
        if ((bool) (UnityEngine.Object) lodEntry.gameObject)
          toHide.Add(lodEntry.gameObject);
      }
    }
    if (lodIndex >= 0 && lodIndex < LODs.Count)
    {
      LODSet loD = LODs[lodIndex];
      int count = loD.LODEntries.Count;
      for (int index = 0; index < count; ++index)
      {
        LODEntry lodEntry = loD.LODEntries[index];
        if ((bool) (UnityEngine.Object) lodEntry.gameObject)
        {
          toHide.Remove(lodEntry.gameObject);
          toShow.Add(lodEntry);
        }
      }
    }
    int count1 = toHide.Count;
    for (int index = 0; index < count1; ++index)
      toHide[index].SetActive(false);
    int count2 = toShow.Count;
    for (int index = 0; index < count2; ++index)
    {
      LODEntry lodEntry = toShow[index];
      lodEntry.gameObject.SetActive(true);
      if ((bool) (UnityEngine.Object) lodEntry.lightmapSource)
      {
        Renderer component = lodEntry.gameObject.GetComponent<Renderer>();
        if ((bool) (UnityEngine.Object) component)
        {
          component.lightmapIndex = lodEntry.lightmapSource.lightmapIndex;
          component.lightmapScaleOffset = lodEntry.lightmapSource.lightmapScaleOffset;
        }
      }
    }
    cachedMember.ForceUpdate(true);
    activeLOD = lodIndex;
  }

  private void _CalculateBounds()
  {
    Bounds bounds = new Bounds();
    int count1 = LODs.Count;
    bool flag = false;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      LODSet loD = LODs[index1];
      int count2 = loD.LODEntries.Count;
      for (int index2 = 0; index2 < count2; ++index2)
      {
        GameObject gameObject = loD.LODEntries[index2].gameObject;
        Renderer component = (bool) (UnityEngine.Object) gameObject ? gameObject.GetComponent<Renderer>() : null;
        if ((bool) (UnityEngine.Object) component && component.bounds.extents != Vector3.zero)
        {
          if (!flag)
          {
            bounds = component.bounds;
            flag = true;
          }
          else
            bounds.Encapsulate(component.bounds);
        }
      }
    }
    boundsOffset = transform.worldToLocalMatrix.MultiplyPoint(bounds.center);
    boundsRadius = bounds.extents.magnitude;
    boundsUpdated = true;
  }

  [Serializable]
  public class LODEntry
  {
    public GameObject gameObject;
    public Renderer lightmapSource;
  }

  [Serializable]
  public class LODSet
  {
    [SerializeField]
    private List<LODEntry> lodEntries = new List<LODEntry>(16);
    [SerializeField]
    private float threshold;

    public List<LODEntry> LODEntries => lodEntries;

    public float Threshold
    {
      get => threshold;
      set => threshold = value;
    }

    public LODEntry Add(GameObject gameObject, Renderer lightmapSource)
    {
      if (GetEntry(gameObject) != null)
        return null;
      LODEntry lodEntry = new LODEntry();
      lodEntry.gameObject = gameObject;
      lodEntry.lightmapSource = lightmapSource;
      lodEntries.Add(lodEntry);
      return lodEntry;
    }

    public void Remove(GameObject gameObject)
    {
      int index = 0;
      while (index < lodEntries.Count)
      {
        if (lodEntries[index].gameObject == gameObject)
          lodEntries.RemoveAt(index);
        else
          ++index;
      }
    }

    public LODEntry GetEntry(GameObject gameObject)
    {
      int count = lodEntries.Count;
      for (int index = 0; index < count; ++index)
      {
        LODEntry lodEntry = lodEntries[index];
        if (lodEntry.gameObject == gameObject)
          return lodEntry;
      }
      return null;
    }
  }
}
