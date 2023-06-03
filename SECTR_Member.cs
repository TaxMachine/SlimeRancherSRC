// Decompiled with JetBrains decompiler
// Type: SECTR_Member
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[AddComponentMenu("SECTR/Core/SECTR Member")]
public class SECTR_Member : MonoBehaviour
{
  [SerializeField]
  [HideInInspector]
  private List<Child> children = new List<Child>(16);
  [SerializeField]
  [HideInInspector]
  private List<Child> renderers = new List<Child>(16);
  [SerializeField]
  [HideInInspector]
  private List<Child> lights = new List<Child>(16);
  [SerializeField]
  [HideInInspector]
  private List<Child> terrains = new List<Child>(2);
  [SerializeField]
  [HideInInspector]
  private List<Child> shadowLights = SECTR_Modules.VIS ? new List<Child>(16) : null;
  [SerializeField]
  [HideInInspector]
  private List<Child> shadowCasters = SECTR_Modules.VIS ? new List<Child>(16) : null;
  [SerializeField]
  [HideInInspector]
  private Bounds totalBounds;
  [SerializeField]
  [HideInInspector]
  private Bounds renderBounds;
  [SerializeField]
  [HideInInspector]
  private Bounds lightBounds;
  [SerializeField]
  [HideInInspector]
  private bool hasRenderBounds;
  [SerializeField]
  [HideInInspector]
  private bool hasLightBounds;
  [SerializeField]
  [HideInInspector]
  private bool shadowCaster;
  [SerializeField]
  [HideInInspector]
  private bool shadowLight;
  [SerializeField]
  [HideInInspector]
  private bool frozen;
  [HideInInspector]
  public bool isFrozen;
  [SerializeField]
  [HideInInspector]
  private bool hibernate;
  [SerializeField]
  [HideInInspector]
  private bool neverJoin;
  [SerializeField]
  [HideInInspector]
  protected List<Light> bakedOnlyLights = SECTR_Modules.VIS ? new List<Light>(8) : null;
  [SerializeField]
  [HideInInspector]
  protected bool legacyBakeMode;
  protected bool isSector;
  private bool started;
  private bool usedStartSector;
  [NonSerialized]
  public List<SECTR_Sector> sectors = new List<SECTR_Sector>(4);
  private List<SECTR_Sector> newSectors = new List<SECTR_Sector>(4);
  private List<SECTR_Sector> leftSectors = new List<SECTR_Sector>(4);
  private Dictionary<Light, Light> bakedOnlyTable;
  private SECTR_Member childProxy;
  private Vector3 lastPosition = Vector3.zero;
  private Stack<Child> childPool = new Stack<Child>(32);
  private static LightmapSettings lightmapSettings;
  private static List<SECTR_Member> allMembers = new List<SECTR_Member>(256);
  private Vector3? lastMembershipPos;
  [SECTR_ToolTip("Set to true if Sector membership should only change when crossing a portal.")]
  public bool PortalDetermined;
  [SECTR_ToolTip("If set, forces the initial Sector to be the specified Sector.", "PortalDetermined")]
  public SECTR_Sector ForceStartSector;
  [SECTR_ToolTip("Determines how often the bounds are recomputed. More frequent updates requires more CPU.")]
  public BoundsUpdateModes BoundsUpdateMode = BoundsUpdateModes.Always;
  [SECTR_ToolTip("Adds a buffer on bounding box to compensate for minor imprecisions.")]
  public float ExtraBounds = 0.01f;
  [SECTR_ToolTip("Override computed bounds with the user specified bounds. Advanced users only.")]
  public bool OverrideBounds;
  [SECTR_ToolTip("User specified override bounds. Auto-populated with the current bounds when override is inactive.", "OverrideBounds")]
  public Bounds BoundsOverride;
  [SECTR_ToolTip("Optional shadow casting directional light to use in membership calculations. Bounds will be extruded away from light, if set.")]
  public Light DirShadowCaster;
  [SECTR_ToolTip("Distance by which to extend the bounds away from the shadow casting light.", "DirShadowCaster")]
  public float DirShadowDistance = 100f;
  [SECTR_ToolTip("Determines if this SectorCuller should cull individual children, or cull all children based on the aggregate bounds.")]
  public ChildCullModes ChildCulling;
  [HideInInspector]
  public bool isHibernating;
  private SECTR_Hibernator memberHibernator;
  [HideInInspector]
  [NonSerialized]
  public Transform memberTransform;

  public void ForceUpdateBounds() => OffsetLateUpdate();

  public static List<SECTR_Member> All => allMembers;

  public bool CullEachChild
  {
    get
    {
      if (ChildCulling == ChildCullModes.Individual)
        return true;
      return ChildCulling == ChildCullModes.Default && isSector;
    }
  }

  public List<SECTR_Sector> Sectors => sectors;

  public List<Child> Children => !(bool) (UnityEngine.Object) childProxy ? children : childProxy.children;

  public List<Child> Renderers => !(bool) (UnityEngine.Object) childProxy ? renderers : childProxy.renderers;

  public bool ShadowCaster => !(bool) (UnityEngine.Object) childProxy ? shadowCaster : childProxy.shadowCaster;

  public List<Child> ShadowCasters => !(bool) (UnityEngine.Object) childProxy ? shadowCasters : childProxy.shadowCasters;

  public List<Child> Lights => !(bool) (UnityEngine.Object) childProxy ? lights : childProxy.lights;

  public bool ShadowLight => !(bool) (UnityEngine.Object) childProxy ? shadowLight : childProxy.shadowLight;

  public List<Child> ShadowLights => !(bool) (UnityEngine.Object) childProxy ? shadowLights : childProxy.shadowLights;

  public List<Child> Terrains => !(bool) (UnityEngine.Object) childProxy ? terrains : childProxy.terrains;

  public Bounds TotalBounds => totalBounds;

  public Bounds RenderBounds => !(bool) (UnityEngine.Object) childProxy ? renderBounds : childProxy.renderBounds;

  public bool HasRenderBounds => !(bool) (UnityEngine.Object) childProxy ? hasRenderBounds : childProxy.hasRenderBounds;

  public Bounds LightBounds => !(bool) (UnityEngine.Object) childProxy ? lightBounds : childProxy.lightBounds;

  public bool HasLightBounds => !(bool) (UnityEngine.Object) childProxy ? hasLightBounds : childProxy.hasLightBounds;

  public bool Frozen
  {
    set
    {
      if (!isSector)
        return;
      frozen = value;
      isFrozen = value;
    }
    get => frozen;
  }

  public bool Hibernate
  {
    set
    {
      if (!isSector)
        return;
      hibernate = value;
      isHibernating = value;
    }
    get => hibernate;
  }

  public SECTR_Member ChildProxy
  {
    set => childProxy = value;
  }

  public bool NeverJoin
  {
    set => neverJoin = true;
  }

  public bool IsSector => isSector;

  public void ForceUpdate(bool updateChildren, bool checkAllSectorSets = false)
  {
    if (updateChildren)
      _UpdateChildren();
    lastPosition = transform.position;
    if (isSector || neverJoin)
      return;
    _UpdateSectorMembership(checkAllSectorSets);
  }

  public void SectorDisabled(SECTR_Sector sector)
  {
    if (!(bool) (UnityEngine.Object) sector)
      return;
    sectors.Remove(sector);
    if (Changed == null)
      return;
    leftSectors.Clear();
    leftSectors.Add(sector);
    Changed(leftSectors, null);
  }

  public event MembershipChanged Changed;

  public virtual void Start()
  {
    started = true;
    ForceUpdate(true, true);
  }

  protected virtual void OnEnable()
  {
    if (SRSingleton<SceneContext>.Instance != null && SRSingleton<SceneContext>.Instance.SECTRDirector != null && GetComponent<SECTR_Hibernator>() == null)
      SRSingleton<SceneContext>.Instance.SECTRDirector.RegisterMember(this);
    allMembers.Add(this);
    if (bakedOnlyLights != null)
    {
      int count = bakedOnlyLights.Count;
      bakedOnlyTable = new Dictionary<Light, Light>(count);
      for (int index = 0; index < count; ++index)
      {
        Light bakedOnlyLight = bakedOnlyLights[index];
        if ((bool) (UnityEngine.Object) bakedOnlyLight)
          bakedOnlyTable[bakedOnlyLight] = bakedOnlyLight;
      }
    }
    if (!started || !Application.isPlaying)
      return;
    ForceUpdate(true, true);
  }

  protected virtual void OnDisable()
  {
    if (SRSingleton<SceneContext>.Instance != null && SRSingleton<SceneContext>.Instance.SECTRDirector != null && GetComponent<SECTR_Hibernator>() == null)
      SRSingleton<SceneContext>.Instance.SECTRDirector.DeregisterMember(this);
    if (Changed != null && sectors.Count > 0)
      Changed(sectors, null);
    if (!isSector && !neverJoin)
    {
      int count = sectors.Count;
      for (int index = 0; index < count; ++index)
      {
        SECTR_Sector sector = sectors[index];
        if ((bool) (UnityEngine.Object) sector)
          sector.Deregister(this);
      }
      sectors.Clear();
    }
    bakedOnlyTable = null;
    allMembers.Remove(this);
  }

  private void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.SECTRDirector != null))
      return;
    SRSingleton<SceneContext>.Instance.SECTRDirector.DeregisterMember(this);
  }

  public virtual void Awake()
  {
    memberTransform = transform;
    memberHibernator = GetComponent<SECTR_Hibernator>();
  }

  public bool IsHibernating => memberHibernator != null && memberHibernator.isHibernating;

  public virtual void OffsetLateUpdate()
  {
    if (!isSector && !neverJoin && (!lastMembershipPos.HasValue || (transform.localPosition - lastMembershipPos.Value).sqrMagnitude > 1.0))
    {
      if (lastMembershipPos.HasValue)
      {
        Vector3 vector3 = transform.localPosition - lastMembershipPos.Value;
        if (vector3.x != 0.0 || vector3.y != 0.0 || vector3.z != 0.0)
        {
          totalBounds.center += vector3;
          renderBounds.center += vector3;
          lightBounds.center += vector3;
        }
      }
      _UpdateSectorMembership();
      lastMembershipPos = new Vector3?(transform.localPosition);
    }
    lastPosition = transform.localPosition;
  }

  public virtual void NonOffsetLateUpdate()
  {
    _UpdateChildren();
    Vector3 position = transform.position;
    if (!isSector && !neverJoin)
    {
      _UpdateSectorMembership();
      lastMembershipPos = new Vector3?(position);
    }
    if (!isSector && !neverJoin && (!lastMembershipPos.HasValue || (position - lastMembershipPos.Value).sqrMagnitude > 1.0))
    {
      _UpdateSectorMembership();
      lastMembershipPos = new Vector3?(position);
    }
    lastPosition = position;
  }

  private void _UpdateChildren()
  {
    if (frozen || (bool) (UnityEngine.Object) childProxy)
      return;
    bool dirShadowCaster = SECTR_Modules.VIS && (bool) (UnityEngine.Object) DirShadowCaster && DirShadowCaster.type == LightType.Directional && DirShadowCaster.shadows != 0;
    Vector3 shadowVec = dirShadowCaster ? DirShadowCaster.transform.forward * DirShadowDistance : Vector3.zero;
    int count = children.Count;
    hasLightBounds = false;
    hasRenderBounds = false;
    shadowCaster = false;
    shadowLight = false;
    renderers.Clear();
    lights.Clear();
    terrains.Clear();
    if (SECTR_Modules.VIS)
    {
      shadowCasters.Clear();
      shadowLights.Clear();
    }
    if ((BoundsUpdateMode == BoundsUpdateModes.Start || BoundsUpdateMode == BoundsUpdateModes.Offset) && count > 0)
    {
      for (int index = 0; index < count; ++index)
      {
        Child child = children[index];
        child.Init(child.gameObject, child.renderer, child.light, child.terrain, child.member, dirShadowCaster, shadowVec);
        Renderer renderer = child.renderer;
        if (renderer != null && !(renderer is ParticleSystemRenderer))
        {
          if (!hasRenderBounds)
          {
            renderBounds = child.rendererBounds;
            hasRenderBounds = true;
          }
          else
            renderBounds.Encapsulate(child.rendererBounds);
          renderers.Add(child);
        }
        if ((bool) (UnityEngine.Object) child.terrain)
        {
          if (!hasRenderBounds)
          {
            renderBounds = child.terrainBounds;
            hasRenderBounds = true;
          }
          else
            renderBounds.Encapsulate(child.terrainBounds);
          terrains.Add(child);
        }
        if ((bool) (UnityEngine.Object) child.light)
        {
          if (SECTR_Modules.VIS && child.shadowLight)
          {
            shadowLights.Add(child);
            shadowLight = true;
          }
          if (!hasLightBounds)
          {
            lightBounds = child.lightBounds;
            hasLightBounds = true;
          }
          else
            lightBounds.Encapsulate(child.lightBounds);
          lights.Add(child);
        }
        if (SECTR_Modules.VIS && (child.terrainCastsShadows || child.rendererCastsShadows))
        {
          shadowCasters.Add(child);
          shadowCaster = true;
        }
      }
    }
    else
    {
      for (int index = 0; index < count; ++index)
        childPool.Push(children[index]);
      children.Clear();
      _AddChildren(transform, dirShadowCaster, shadowVec);
    }
    lastPosition = transform.position;
    Bounds bounds = new Bounds(transform.position, Vector3.zero);
    if (hasRenderBounds && (isSector || neverJoin))
      totalBounds = renderBounds;
    else if (hasRenderBounds && hasLightBounds)
    {
      totalBounds = renderBounds;
      totalBounds.Encapsulate(lightBounds);
    }
    else if (hasRenderBounds)
    {
      totalBounds = renderBounds;
      lightBounds = bounds;
    }
    else if (hasLightBounds)
    {
      totalBounds = lightBounds;
      renderBounds = bounds;
    }
    else
    {
      totalBounds = bounds;
      lightBounds = bounds;
      renderBounds = bounds;
    }
    totalBounds.Expand(ExtraBounds);
    if (isSector)
      totalBounds.max += Vector3.up * 160f;
    if (!OverrideBounds)
      return;
    totalBounds = BoundsOverride;
  }

  private void _AddChildren(Transform childTransform, bool dirShadowCaster, Vector3 shadowVec)
  {
    if (!childTransform.gameObject.activeSelf || !(childTransform == transform) && !(childTransform.GetComponent<SECTR_Member>() == null) || !(childTransform.GetComponent<IgnoreSectrBounds>() == null))
      return;
    Light light = childTransform.GetComponent<Light>();
    Renderer component = childTransform.GetComponent<Renderer>();
    Terrain terrain = null;
    if (isSector)
      terrain = childTransform.GetComponent<Terrain>();
    if (bakedOnlyLights != null && (bool) (UnityEngine.Object) light && light.bakingOutput.isBaked && LightmapSettings.lightmaps.Length != 0 && bakedOnlyTable != null && bakedOnlyTable.ContainsKey(light))
      light = null;
    if ((bool) (UnityEngine.Object) component || (bool) (UnityEngine.Object) light || (bool) (UnityEngine.Object) terrain)
    {
      Child child = childPool.Count <= 0 ? new Child() : childPool.Pop();
      child.Init(childTransform.gameObject, component, light, terrain, this, dirShadowCaster, shadowVec);
      if ((bool) (UnityEngine.Object) child.renderer)
      {
        bool flag = true;
        if (component.GetType() == typeof (ParticleSystemRenderer))
          flag = false;
        if (flag)
        {
          if (!hasRenderBounds)
          {
            renderBounds = child.rendererBounds;
            hasRenderBounds = true;
          }
          else
            renderBounds.Encapsulate(child.rendererBounds);
        }
        renderers.Add(child);
      }
      if ((bool) (UnityEngine.Object) child.light)
      {
        if (SECTR_Modules.VIS && child.shadowLight)
        {
          shadowLights.Add(child);
          shadowLight = true;
        }
        if (!hasLightBounds)
        {
          lightBounds = child.lightBounds;
          hasLightBounds = true;
        }
        else
          lightBounds.Encapsulate(child.lightBounds);
        lights.Add(child);
      }
      if ((bool) (UnityEngine.Object) child.terrain)
      {
        if (!hasRenderBounds)
        {
          renderBounds = child.terrainBounds;
          hasRenderBounds = true;
        }
        else
          renderBounds.Encapsulate(child.terrainBounds);
        terrains.Add(child);
      }
      if (SECTR_Modules.VIS && (child.terrainCastsShadows || child.rendererCastsShadows))
      {
        shadowCasters.Add(child);
        shadowCaster = true;
      }
      children.Add(child);
    }
    int childCount = childTransform.transform.childCount;
    for (int index = 0; index < childCount; ++index)
      _AddChildren(childTransform.GetChild(index), dirShadowCaster, shadowVec);
  }

  private void _UpdateSectorMembership(bool checkAllSectorSets = false)
  {
    if (frozen || isSector || neverJoin)
      return;
    newSectors.Clear();
    leftSectors.Clear();
    if (PortalDetermined && sectors.Count > 0)
    {
      int count1 = sectors.Count;
      for (int index = 0; index < count1; ++index)
      {
        SECTR_Sector sector = sectors[index];
        SECTR_Portal sectrPortal = _CrossedPortal(sector);
        if ((bool) (UnityEngine.Object) sectrPortal)
        {
          SECTR_Sector sectrSector = sectrPortal.FrontSector == sector ? sectrPortal.BackSector : sectrPortal.FrontSector;
          if (!newSectors.Contains(sectrSector))
            newSectors.Add(sectrSector);
          leftSectors.Add(sector);
        }
      }
      int count2 = newSectors.Count;
      for (int index = 0; index < count2; ++index)
      {
        SECTR_Sector newSector = newSectors[index];
        newSector.Register(this);
        sectors.Add(newSector);
      }
      int count3 = leftSectors.Count;
      for (int index = 0; index < count3; ++index)
      {
        SECTR_Sector leftSector = leftSectors[index];
        leftSector.Deregister(this);
        sectors.Remove(leftSector);
      }
    }
    else if (PortalDetermined && (bool) (UnityEngine.Object) ForceStartSector && !usedStartSector)
    {
      ForceStartSector.Register(this);
      sectors.Add(ForceStartSector);
      newSectors.Add(ForceStartSector);
      usedStartSector = true;
    }
    else
    {
      SECTR_Sector.GetContaining(ref newSectors, TotalBounds, checkAllSectorSets);
      int index1 = 0;
      int count4 = sectors.Count;
      while (index1 < count4)
      {
        SECTR_Sector sector = sectors[index1];
        if (newSectors.Contains(sector))
        {
          newSectors.Remove(sector);
          ++index1;
        }
        else
        {
          sector.Deregister(this);
          leftSectors.Add(sector);
          sectors.RemoveAt(index1);
          --count4;
        }
      }
      int count5 = newSectors.Count;
      if (count5 > 0)
      {
        for (int index2 = 0; index2 < count5; ++index2)
        {
          SECTR_Sector newSector = newSectors[index2];
          newSector.Register(this);
          sectors.Add(newSector);
        }
      }
    }
    if (Changed == null || leftSectors.Count <= 0 && newSectors.Count <= 0)
      return;
    Changed(leftSectors, newSectors);
  }

  private SECTR_Portal _CrossedPortal(SECTR_Sector sector)
  {
    if ((bool) (UnityEngine.Object) sector)
    {
      Vector3 lhs = transform.position - lastPosition;
      int count = sector.Portals.Count;
      for (int index = 0; index < count; ++index)
      {
        SECTR_Portal portal = sector.Portals[index];
        if ((bool) (UnityEngine.Object) portal)
        {
          int num = portal.FrontSector == sector ? 1 : 0;
          Plane plane = num != 0 ? portal.HullPlane : portal.ReverseHullPlane;
          if ((bool) (num != 0 ? portal.BackSector : (UnityEngine.Object) portal.FrontSector) && Vector3.Dot(lhs, plane.normal) < 0.0 && plane.GetSide(transform.position) != plane.GetSide(lastPosition) && portal.IsPointInHull(transform.position, lhs.magnitude))
            return portal;
        }
      }
    }
    return null;
  }

  [Serializable]
  public class Child
  {
    public GameObject gameObject;
    public int gameObjectHash;
    public SECTR_Member member;
    public Renderer renderer;
    public int renderHash;
    public Light light;
    public int lightHash;
    public Terrain terrain;
    public int terrainHash;
    public Bounds rendererBounds;
    public Bounds lightBounds;
    public Bounds terrainBounds;
    public bool shadowLight;
    public bool rendererCastsShadows;
    public bool terrainCastsShadows;
    public LayerMask layer;
    public Vector3 shadowLightPosition;
    public float shadowLightRange;
    public LightType shadowLightType;
    public int shadowCullingMask;

    public void Init(
      GameObject gameObject,
      Renderer renderer,
      Light light,
      Terrain terrain,
      SECTR_Member member,
      bool dirShadowCaster,
      Vector3 shadowVec)
    {
      if (gameObject == null)
        return;
      this.gameObject = gameObject;
      gameObjectHash = this.gameObject.GetInstanceID();
      this.member = member;
      this.renderer = !(bool) (UnityEngine.Object) renderer || !renderer.enabled ? null : renderer;
      this.light = !(bool) (UnityEngine.Object) light || !light.enabled || light.type != LightType.Point && light.type != LightType.Spot ? null : light;
      this.terrain = !(bool) (UnityEngine.Object) terrain || !terrain.enabled ? null : terrain;
      Bounds bounds1 = new Bounds(gameObject.transform.position, Vector3.zero);
      Vector3 lossyScale;
      Bounds bounds2;
      if ((bool) (UnityEngine.Object) this.renderer)
      {
        lossyScale = gameObject.transform.lossyScale;
        if (lossyScale.sqrMagnitude > (double) Mathf.Epsilon)
        {
          bounds2 = this.renderer.bounds;
          goto label_5;
        }
      }
      bounds2 = bounds1;
label_5:
      rendererBounds = bounds2;
      Bounds bounds3;
      if ((bool) (UnityEngine.Object) this.light)
      {
        lossyScale = gameObject.transform.lossyScale;
        if (lossyScale.sqrMagnitude > 0.0)
        {
          bounds3 = SECTR_Geometry.ComputeBounds(this.light);
          goto label_9;
        }
      }
      bounds3 = bounds1;
label_9:
      lightBounds = bounds3;
      Bounds bounds4;
      if ((bool) (UnityEngine.Object) this.terrain)
      {
        lossyScale = gameObject.transform.lossyScale;
        if (lossyScale.sqrMagnitude > 0.0)
        {
          bounds4 = SECTR_Geometry.ComputeBounds(this.terrain);
          goto label_13;
        }
      }
      bounds4 = bounds1;
label_13:
      terrainBounds = bounds4;
      layer = gameObject.layer;
      if (SECTR_Modules.VIS)
      {
        renderHash = (bool) (UnityEngine.Object) this.renderer ? this.renderer.GetInstanceID() : 0;
        lightHash = (bool) (UnityEngine.Object) this.light ? this.light.GetInstanceID() : 0;
        terrainHash = (bool) (UnityEngine.Object) this.terrain ? this.terrain.GetInstanceID() : 0;
        bool flag = !member.legacyBakeMode || LightmapSettings.lightmapsMode == LightmapsMode.CombinedDirectional;
        shadowLight = (bool) (UnityEngine.Object) this.light && light.shadows != LightShadows.None && !light.bakingOutput.isBaked | flag;
        rendererCastsShadows = this.renderer != null && renderer.shadowCastingMode != ShadowCastingMode.Off && renderer.lightmapIndex == -1 | flag;
        terrainCastsShadows = (bool) (UnityEngine.Object) this.terrain && terrain.shadowCastingMode != ShadowCastingMode.Off && terrain.lightmapIndex == -1 | flag;
        if (dirShadowCaster)
        {
          if (rendererCastsShadows)
            rendererBounds = SECTR_Geometry.ProjectBounds(rendererBounds, shadowVec);
          if (terrainCastsShadows)
            terrainBounds = SECTR_Geometry.ProjectBounds(terrainBounds, shadowVec);
        }
        if (shadowLight)
        {
          shadowLightPosition = light.transform.position;
          shadowLightRange = light.range;
          shadowLightType = light.type;
          shadowCullingMask = light.cullingMask;
        }
        else
        {
          shadowLightPosition = Vector3.zero;
          shadowLightRange = 0.0f;
          shadowLightType = LightType.Area;
          shadowCullingMask = 0;
        }
      }
      else
      {
        renderHash = 0;
        lightHash = 0;
        terrainHash = 0;
        shadowLight = false;
        rendererCastsShadows = false;
        terrainCastsShadows = false;
        shadowLightPosition = Vector3.zero;
        shadowLightRange = 0.0f;
        shadowLightType = LightType.Area;
        shadowCullingMask = 0;
      }
    }

    public override bool Equals(object obj) => (object) (obj as Child) != null && this == (Child) obj;

    public override int GetHashCode() => gameObjectHash;

    public static bool operator ==(Child x, Child y) => x.gameObjectHash == y.gameObjectHash;

    public static bool operator !=(Child x, Child y) => !(x == y);
  }

  public enum BoundsUpdateModes
  {
    Start,
    Movement,
    Always,
    Static,
    Offset,
  }

  public enum ChildCullModes
  {
    Default,
    Group,
    Individual,
  }

  public delegate void MembershipChanged(List<SECTR_Sector> left, List<SECTR_Sector> joined);
}
