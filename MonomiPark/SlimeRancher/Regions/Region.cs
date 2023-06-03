// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Regions.Region
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace MonomiPark.SlimeRancher.Regions
{
  [ExecuteInEditMode]
  public class Region : MonoBehaviour
  {
    public bool overrideBounds;
    public Bounds bounds;
    public GameObject root;
    public Mesh proxyMesh;
    public Material[] proxyMaterials;
    private GameObject proxyObj;
    private int nonProxyRefCount;
    private int nonHibernateRefCount;
    private RegionRegistry regionReg;
    private ExposedArrayList<RegionMember> members = new ExposedArrayList<RegionMember>();
    [HideInInspector]
    public CellDirector cellDir;
    private const float HEADROOM = 160f;
    private const int MIN_LOCAL_ARRAY_RESIZE_AMOUNT = 10;
    private static RegionMember[] Update_localMembers = new RegionMember[10];

    public RegionRegistry.RegionSetId setId { get; private set; }

    public bool Proxied => nonProxyRefCount == 0;

    public bool Hibernated => nonHibernateRefCount == 0;

    public void AddNonProxiedReference()
    {
      ++nonProxyRefCount;
      if (nonProxyRefCount != 1)
        return;
      Unproxy();
    }

    public void RemoveNonProxiedReference()
    {
      --nonProxyRefCount;
      if (nonProxyRefCount > 0)
        return;
      nonProxyRefCount = 0;
      Proxy();
    }

    public void AddNonHibernateReference()
    {
      ++nonHibernateRefCount;
      if (nonHibernateRefCount != 1)
        return;
      UpdateMembersHibernationStates();
    }

    public void RemoveNonHibernateReference()
    {
      --nonHibernateRefCount;
      if (nonHibernateRefCount > 0)
        return;
      nonHibernateRefCount = 0;
      UpdateMembersHibernationStates();
    }

    public event OnHibernationStateChanged onHibernationStateChanged;

    private void UpdateMembersHibernationStates()
    {
      if (members.Data.Length > Update_localMembers.Length)
        Array.Resize(ref Update_localMembers, Math.Max(members.Data.Length, Update_localMembers.Length + 10));
      int count = members.GetCount();
      members.Data.CopyTo(Update_localMembers, 0);
      for (int index = 0; index < count; ++index)
        Update_localMembers[index].UpdateHibernation();
      if (onHibernationStateChanged == null)
        return;
      onHibernationStateChanged(Hibernated);
    }

    public void AddMember(RegionMember regionMember) => members.Add(regionMember);

    public void RemoveMember(RegionMember regionMember) => members.Remove(regionMember);

    public void CheckReferences()
    {
      if (nonHibernateRefCount > 0)
        return;
      nonHibernateRefCount = 0;
    }

    public ZoneDirector.Zone GetZoneId() => cellDir != null ? cellDir.GetZoneId() : ZoneDirector.Zone.NONE;

    public void OnRegionSetDeactivated()
    {
      if (proxyObj != null)
      {
        Destroyer.Destroy(proxyObj, "Region.OnRegionSetDeactivated");
        proxyObj = null;
      }
      nonProxyRefCount = 0;
      nonHibernateRefCount = 0;
      root.SetActive(false);
      UpdateMembersHibernationStates();
    }

    private void Proxy()
    {
      if (proxyMesh != null && regionReg.IsCurrRegionSet(setId))
        CreateProxy();
      root.SetActive(false);
    }

    private void CreateProxy()
    {
      if (!(proxyObj == null) || !(proxyMesh != null))
        return;
      proxyObj = new GameObject(name + " Proxy");
      proxyObj.AddComponent<MeshFilter>().sharedMesh = proxyMesh;
      MeshRenderer meshRenderer = proxyObj.AddComponent<MeshRenderer>();
      meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
      meshRenderer.sharedMaterials = proxyMaterials;
      proxyObj.transform.position = transform.position;
      proxyObj.transform.rotation = transform.rotation;
      proxyObj.transform.localScale = transform.lossyScale;
      proxyObj.transform.SetParent(transform, true);
    }

    private void Unproxy()
    {
      if (proxyObj != null)
      {
        Destroyer.Destroy(proxyObj, "Region.Unproxy");
        proxyObj = null;
      }
      root.SetActive(true);
    }

    public void Awake()
    {
      if (!Application.isPlaying)
        return;
      cellDir = GetComponent<CellDirector>();
      regionReg = SRSingleton<SceneContext>.Instance.RegionRegistry;
      setId = ZoneDirector.GetRegionSetId(GetComponentsInParent<ZoneDirector>(true)[0].zone);
    }

    public void OnEnable()
    {
      if (!Application.isPlaying)
        return;
      regionReg.RegisterRegion(this, setId, bounds);
    }

    public void OnDisable()
    {
      if (!Application.isPlaying)
        return;
      regionReg.DeregisterRegion(this, setId);
    }

    public delegate void OnHibernationStateChanged(bool hibernated);
  }
}
