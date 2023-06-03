// Decompiled with JetBrains decompiler
// Type: SECTR_Chunk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof (SECTR_Sector))]
[AddComponentMenu("SECTR/Stream/SECTR Chunk")]
public class SECTR_Chunk : MonoBehaviour
{
  private AsyncOperation asyncLoadOp;
  private LoadState loadState;
  private int refCount;
  private int wakeRefCount;
  private GameObject chunkRoot;
  private GameObject chunkSector;
  private bool recenterChunk;
  private SECTR_Sector cachedSector;
  private GameObject proxy;
  private bool quitting;
  private static bool sceneActivating;
  [SECTR_ToolTip("The path of the scene to load")]
  public string ScenePath;
  [SECTR_ToolTip("The unique name of the root object in the exported Sector.")]
  public string NodeName;
  [SECTR_ToolTip("Exports the Chunk in a way that allows it to be shared by multiple Sectors, but may take more CPU to load.")]
  public bool ExportForReuse;
  [SECTR_ToolTip("A mesh to display when this Chunk is unloaded. Will be hidden when loaded.")]
  public Mesh ProxyMesh;
  [SECTR_ToolTip("The per-submesh materials for the proxy.")]
  public Material[] ProxyMaterials;
  private bool canProxy;

  public SECTR_Sector Sector => cachedSector;

  public void SetCanProxy(bool canProxy)
  {
    this.canProxy = canProxy;
    if (!canProxy && proxy != null)
    {
      Destroyer.Destroy(proxy, "SECTR_Chunk.SetCanProxy");
      proxy = null;
    }
    else
    {
      if (((!(proxy == null) ? 0 : ((bool) (Object) ProxyMesh ? 1 : 0)) & (canProxy ? 1 : 0)) == 0)
        return;
      _CreateProxy();
    }
  }

  public void AddReference()
  {
    if (refCount == 0)
    {
      _Load();
      if (Changed != null)
        Changed(this, true);
    }
    ++refCount;
    if (ReferenceChange == null)
      return;
    ReferenceChange(this, true);
  }

  public void RemoveReference()
  {
    if (ReferenceChange != null)
      ReferenceChange(this, false);
    --refCount;
    if (refCount > 0)
      return;
    if (Changed != null)
      Changed(this, false);
    _Unload();
    refCount = 0;
  }

  public void AddWakeReference()
  {
    if (wakeRefCount == 0 && cachedSector != null)
      cachedSector.Hibernate = false;
    ++wakeRefCount;
  }

  public void RemoveWakeReference()
  {
    --wakeRefCount;
    if (wakeRefCount > 0)
      return;
    if (cachedSector != null)
      cachedSector.Hibernate = true;
    wakeRefCount = 0;
  }

  public void CheckReferences()
  {
    if (refCount <= 0)
    {
      _Unload();
      refCount = 0;
    }
    if (wakeRefCount > 0)
      return;
    if (cachedSector != null)
      cachedSector.Hibernate = true;
    wakeRefCount = 0;
  }

  public bool IsLoaded() => loadState == LoadState.Active;

  public bool IsUnloaded() => loadState == LoadState.Unloaded;

  public float LoadProgress()
  {
    switch (loadState)
    {
      case LoadState.Loading:
        return asyncLoadOp == null ? 0.5f : asyncLoadOp.progress * 0.8f;
      case LoadState.Loaded:
        return 0.9f;
      case LoadState.Active:
        return 1f;
      default:
        return 0.0f;
    }
  }

  public event LoadCallback Changed;

  public event LoadCallback ReferenceChange;

  private void Awake() => SECTR_LightmapRef.InitRefCounts();

  private void OnEnable()
  {
    cachedSector = GetComponent<SECTR_Sector>();
    cachedSector.Hibernate = wakeRefCount <= 0;
    if (!cachedSector.Frozen || !canProxy)
      return;
    _CreateProxy();
  }

  private void OnDisable()
  {
    if (!quitting && asyncLoadOp != null && !asyncLoadOp.isDone)
      Debug.LogError("Chunk unloaded with async operation active. Do not disable chunks until async operations are complete or Unity will likely crash.");
    if (loadState != LoadState.Unloaded)
    {
      _FindChunkRoot();
      if ((bool) (Object) chunkRoot)
        _DestoryChunk(false);
    }
    cachedSector = null;
  }

  private void OnApplicationQuit() => quitting = true;

  private void FixedUpdate()
  {
    switch (loadState)
    {
      case LoadState.Loading:
        _TrySceneActivation();
        if (asyncLoadOp != null && !asyncLoadOp.isDone)
          break;
        sceneActivating = false;
        asyncLoadOp = null;
        loadState = LoadState.Loaded;
        FixedUpdate();
        break;
      case LoadState.Loaded:
        _SetupChunk();
        break;
      case LoadState.Unloading:
        _TrySceneActivation();
        _FindChunkRoot();
        if (!(bool) (Object) chunkRoot)
          break;
        _DestoryChunk(true);
        break;
    }
  }

  private void _Load()
  {
    if (!enabled || loadState != LoadState.Unloaded && loadState != LoadState.Unloading)
      return;
    chunkRoot.SetActive(true);
    loadState = LoadState.Loaded;
  }

  public void SetRoot(GameObject root)
  {
    chunkRoot = root;
    chunkSector = root;
    loadState = LoadState.Active;
  }

  private void _Unload()
  {
    if (!enabled || loadState == LoadState.Unloaded)
      return;
    cachedSector.Frozen = true;
    if ((bool) (Object) chunkRoot)
      _DestoryChunk(true);
    else
      loadState = LoadState.Unloading;
  }

  private void _DestoryChunk(bool createProxy)
  {
    if ((bool) (Object) cachedSector.TopTerrain || (bool) (Object) cachedSector.BottomTerrain || (bool) (Object) cachedSector.RightTerrain || (bool) (Object) cachedSector.LeftTerrain)
      cachedSector.DisonnectTerrainNeighbors();
    chunkRoot.SetActive(false);
    loadState = LoadState.Unloaded;
    if (!createProxy || !(bool) (Object) ProxyMesh || !canProxy)
      return;
    _CreateProxy();
  }

  private void _FindChunkRoot()
  {
    if (!(chunkRoot == null))
      return;
    SECTR_ChunkRef chunkRef = SECTR_ChunkRef.FindChunkRef(NodeName);
    if ((bool) (Object) chunkRef && (bool) (Object) chunkRef.RealSector)
    {
      recenterChunk = chunkRef.Recentered;
      if (recenterChunk)
      {
        chunkRef.RealSector.parent = transform;
        chunkRoot = chunkRef.RealSector.gameObject;
        chunkSector = chunkRoot;
        Destroyer.Destroy(chunkRef.gameObject, "SECTR_Chunk._FindChunkRoot#1");
      }
      else
      {
        chunkRoot = chunkRef.gameObject;
        chunkSector = chunkRef.RealSector.gameObject;
        Destroyer.Destroy(chunkRef, "SECTR_Chunk._FindChunkRoot#2");
      }
    }
    else
    {
      if (quitting)
        return;
      chunkRoot = GameObject.Find(NodeName);
      chunkSector = chunkRoot;
      recenterChunk = false;
    }
  }

  private void _SetupChunk()
  {
    _FindChunkRoot();
    if (!(bool) (Object) chunkRoot)
      return;
    if (!chunkRoot.activeSelf)
      chunkRoot.SetActive(true);
    if (recenterChunk)
    {
      Transform transform = chunkRoot.transform;
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
      transform.localScale = Vector3.one;
    }
    SECTR_Member sectrMember = chunkSector.GetComponent<SECTR_Member>();
    if (!(bool) (Object) sectrMember)
    {
      sectrMember = chunkSector.gameObject.AddComponent<SECTR_Member>();
      sectrMember.BoundsUpdateMode = SECTR_Member.BoundsUpdateModes.Static;
      sectrMember.ForceUpdate(true);
    }
    else if (recenterChunk)
      sectrMember.ForceUpdate(true);
    cachedSector.ChildProxy = sectrMember;
    cachedSector.Frozen = false;
    if ((bool) (Object) cachedSector.TopTerrain || (bool) (Object) cachedSector.BottomTerrain || (bool) (Object) cachedSector.LeftTerrain || (bool) (Object) cachedSector.RightTerrain)
    {
      cachedSector.ConnectTerrainNeighbors();
      if ((bool) (Object) cachedSector.TopTerrain)
        cachedSector.TopTerrain.ConnectTerrainNeighbors();
      if ((bool) (Object) cachedSector.BottomTerrain)
        cachedSector.BottomTerrain.ConnectTerrainNeighbors();
      if ((bool) (Object) cachedSector.LeftTerrain)
        cachedSector.LeftTerrain.ConnectTerrainNeighbors();
      if ((bool) (Object) cachedSector.RightTerrain)
        cachedSector.RightTerrain.ConnectTerrainNeighbors();
    }
    if ((bool) (Object) proxy)
    {
      Destroyer.Destroy(proxy, "SECTR_Chunk._SetupChunk");
      proxy = null;
    }
    loadState = LoadState.Active;
  }

  private void _CreateProxy()
  {
    if (!(proxy == null) || !(bool) (Object) ProxyMesh || quitting)
      return;
    proxy = new GameObject(name + " Proxy");
    proxy.AddComponent<MeshFilter>().sharedMesh = ProxyMesh;
    MeshRenderer meshRenderer = proxy.AddComponent<MeshRenderer>();
    meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
    meshRenderer.sharedMaterials = ProxyMaterials;
    proxy.transform.position = transform.position;
    proxy.transform.rotation = transform.rotation;
    proxy.transform.localScale = transform.lossyScale;
    proxy.transform.SetParent(transform, true);
  }

  private void _TrySceneActivation()
  {
    if (asyncLoadOp == null || asyncLoadOp.allowSceneActivation || sceneActivating || asyncLoadOp.progress < 0.89999997615814209)
      return;
    sceneActivating = true;
    asyncLoadOp.allowSceneActivation = true;
  }

  private enum LoadState
  {
    Unloaded,
    Loading,
    Loaded,
    Unloading,
    Active,
  }

  public delegate void LoadCallback(SECTR_Chunk source, bool loaded);
}
