// Decompiled with JetBrains decompiler
// Type: SECTR_CullingCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("SECTR/Vis/SECTR Culling Camera")]
public class SECTR_CullingCamera : MonoBehaviour
{
  private List<Renderer> hiddenRenderers = new List<Renderer>(16);
  private List<Light> hiddenLights = new List<Light>(16);
  private List<Terrain> hiddenTerrains = new List<Terrain>(2);
  private int renderersCulled;
  private int lightsCulled;
  private int terrainsCulled;
  private bool didCull;
  private List<SECTR_Sector> initialSectors = new List<SECTR_Sector>(4);
  private Stack<VisibilityNode> nodeStack = new Stack<VisibilityNode>(10);
  private List<ClipVertex> portalVertices = new List<ClipVertex>(16);
  private List<Plane> newFrustum = new List<Plane>(16);
  private List<Plane> cullingPlanes = new List<Plane>(16);
  private List<List<Plane>> occluderFrustums = new List<List<Plane>>(10);
  private Dictionary<SECTR_Occluder, SECTR_Occluder> activeOccluders = new Dictionary<SECTR_Occluder, SECTR_Occluder>(10);
  private List<ClipVertex> occluderVerts = new List<ClipVertex>(10);
  private Dictionary<SECTR_Member.Child, int> shadowLights = new Dictionary<SECTR_Member.Child, int>(10);
  private List<SECTR_Sector> shadowSectors = new List<SECTR_Sector>(4);
  private Dictionary<SECTR_Sector, List<SECTR_Member.Child>> shadowSectorTable = new Dictionary<SECTR_Sector, List<SECTR_Member.Child>>(4);
  private Dictionary<int, int> visibleRenderers = new Dictionary<int, int>(1024);
  private Dictionary<int, int> visibleLights = new Dictionary<int, int>(256);
  private Dictionary<int, int> visibleTerrains = new Dictionary<int, int>(32);
  private Stack<List<Plane>> frustumPool = new Stack<List<Plane>>(32);
  private Stack<List<SECTR_Member.Child>> shadowLightPool = new Stack<List<SECTR_Member.Child>>(32);
  private Stack<Dictionary<int, int>> threadVisibleListPool = new Stack<Dictionary<int, int>>(32);
  private Stack<Dictionary<SECTR_Member.Child, int>> threadShadowLightPool = new Stack<Dictionary<SECTR_Member.Child, int>>(32);
  private Stack<List<Plane>> threadFrustumPool = new Stack<List<Plane>>(32);
  private Stack<List<List<Plane>>> threadOccluderPool = new Stack<List<List<Plane>>>(32);
  private List<Thread> workerThreads = new List<Thread>();
  private Queue<ThreadCullData> cullingWorkQueue = new Queue<ThreadCullData>(32);
  private int remainingThreadWork;
  private static List<SECTR_CullingCamera> allCullingCameras = new List<SECTR_CullingCamera>(4);
  [SECTR_ToolTip("Forces culling into a mode designed for 2D and iso games where the camera is always outside the scene.")]
  public bool SimpleCulling;
  [SECTR_ToolTip("The layer that culled objects should be assigned to.", false, true)]
  [HideInInspector]
  public int InvisibleLayer;
  [SECTR_ToolTip("Distance to draw clipped frustums.", 0.0f, 100f)]
  public float GizmoDistance = 10f;
  [SECTR_ToolTip("Material to use to render the debug frustum mesh.")]
  public Material GizmoMaterial;
  [SECTR_ToolTip("Makes the Editor camera display the Game view's culling while playing in editor.")]
  public bool CullInEditor;
  [SECTR_ToolTip("Set to false to disable shadow culling post pass.", true)]
  public bool CullShadows = true;
  [SECTR_ToolTip("Use another camera for culling properties.", true)]
  public Camera cullingProxy;
  [SECTR_ToolTip("Number of worker threads for culling. Do not set this too high or you may see hitching.", 0.0f, -1f)]
  public int NumWorkerThreads;

  public static List<SECTR_CullingCamera> All => allCullingCameras;

  public Camera CullingCamera
  {
    set => cullingProxy = value;
  }

  public int RenderersCulled => renderersCulled;

  public int LightsCulled => lightsCulled;

  public int TerrainsCulled => terrainsCulled;

  public void ResetStats()
  {
    renderersCulled = 0;
    lightsCulled = 0;
    terrainsCulled = 0;
  }

  private void OnEnable()
  {
    allCullingCameras.Add(this);
    int num = Mathf.Min(NumWorkerThreads, SystemInfo.processorCount);
    for (int index = 0; index < num; ++index)
    {
      Thread thread = new Thread(_CullingWorker);
      thread.IsBackground = true;
      thread.Priority = System.Threading.ThreadPriority.Highest;
      thread.Start();
      workerThreads.Add(thread);
    }
  }

  private void OnDisable()
  {
    allCullingCameras.Remove(this);
    int count = workerThreads.Count;
    for (int index = 0; index < count; ++index)
      workerThreads[index].Abort();
  }

  private void OnDestroy()
  {
  }

  private void OnPreCull()
  {
    Camera camera = cullingProxy != null ? cullingProxy : GetComponent<Camera>();
    Vector3 position = camera.transform.position;
    float num1 = Mathf.Cos((float) (Mathf.Max(camera.fieldOfView, camera.fieldOfView * camera.aspect) * 0.5 * (Math.PI / 180.0)));
    float num2 = (float) (camera.nearClipPlane / (double) num1 * 1.0010000467300415);
    int invisibleLayer = InvisibleLayer;
    bool simpleCulling = SimpleCulling;
    if ((bool) (UnityEngine.Object) cullingProxy)
    {
      SECTR_CullingCamera component = cullingProxy.GetComponent<SECTR_CullingCamera>();
      if ((bool) (UnityEngine.Object) component)
      {
        invisibleLayer = component.InvisibleLayer;
        simpleCulling = component.SimpleCulling;
      }
    }
    int count1 = SECTR_LOD.All.Count;
    for (int index = 0; index < count1; ++index)
      SECTR_LOD.All[index].SelectLOD(camera);
    SECTR_Member component1 = GetComponent<SECTR_Member>();
    if (simpleCulling)
    {
      initialSectors.Clear();
      initialSectors.AddRange(SECTR_Sector.All);
    }
    else if ((bool) (UnityEngine.Object) component1 && component1.enabled)
    {
      initialSectors.Clear();
      initialSectors.AddRange(component1.Sectors);
    }
    else
      SECTR_Sector.GetContaining(ref initialSectors, new Bounds(position, new Vector3(num2, num2, num2)));
    int count2 = initialSectors.Count;
    if (!enabled || !camera.enabled || count2 <= 0)
      return;
    didCull = true;
    int count3 = workerThreads.Count;
    float shadowDistance = QualitySettings.shadowDistance;
    int count4 = SECTR_Member.All.Count;
    for (int index1 = 0; index1 < count4; ++index1)
    {
      SECTR_Member sectrMember = SECTR_Member.All[index1];
      if (sectrMember.ShadowLight)
      {
        int count5 = sectrMember.ShadowLights.Count;
        for (int index2 = 0; index2 < count5; ++index2)
        {
          SECTR_Member.Child shadowLight = sectrMember.ShadowLights[index2];
          if ((bool) (UnityEngine.Object) shadowLight.light)
          {
            shadowLight.shadowLightPosition = shadowLight.light.transform.position;
            shadowLight.shadowLightRange = shadowLight.light.range;
          }
          sectrMember.ShadowLights[index2] = shadowLight;
        }
      }
    }
    nodeStack.Clear();
    shadowLights.Clear();
    visibleRenderers.Clear();
    visibleLights.Clear();
    visibleTerrains.Clear();
    Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
    for (int index = 0; index < count2; ++index)
      nodeStack.Push(new VisibilityNode(this, initialSectors[index], null, frustumPlanes, true));
    while (nodeStack.Count > 0)
    {
      VisibilityNode visibilityNode = nodeStack.Pop();
      if (visibilityNode.frustumPlanes != null)
      {
        cullingPlanes.Clear();
        cullingPlanes.AddRange(visibilityNode.frustumPlanes);
        int count6 = cullingPlanes.Count;
        for (int index = 0; index < count6; ++index)
        {
          Plane cullingPlane1 = cullingPlanes[index];
          Plane cullingPlane2 = cullingPlanes[(index + 1) % cullingPlanes.Count];
          float num3 = Vector3.Dot(cullingPlane1.normal, cullingPlane2.normal);
          if (num3 < -0.89999997615814209 && num3 > -0.99000000953674316)
          {
            Vector3 lhs = cullingPlane1.normal + cullingPlane2.normal;
            Vector3 rhs = Vector3.Cross(cullingPlane1.normal, cullingPlane2.normal);
            Vector3 inNormal = lhs - Vector3.Dot(lhs, rhs) * rhs;
            inNormal.Normalize();
            Matrix4x4 matrix4x4 = new Matrix4x4();
            matrix4x4.SetRow(0, new Vector4(cullingPlane1.normal.x, cullingPlane1.normal.y, cullingPlane1.normal.z, 0.0f));
            matrix4x4.SetRow(1, new Vector4(cullingPlane2.normal.x, cullingPlane2.normal.y, cullingPlane2.normal.z, 0.0f));
            matrix4x4.SetRow(2, new Vector4(rhs.x, rhs.y, rhs.z, 0.0f));
            matrix4x4.SetRow(3, new Vector4(0.0f, 0.0f, 0.0f, 1f));
            Vector3 inPoint = matrix4x4.inverse.MultiplyPoint3x4(new Vector3(-cullingPlane1.distance, -cullingPlane2.distance, 0.0f));
            cullingPlanes.Insert(++index, new Plane(inNormal, inPoint));
          }
        }
        int count7 = cullingPlanes.Count;
        int num4 = 0;
        for (int index = 0; index < count7; ++index)
          num4 |= 1 << index;
        SECTR_Sector sector1 = visibilityNode.sector;
        Vector3 vector3_1;
        if (SECTR_Occluder.All.Count > 0)
        {
          List<SECTR_Occluder> occludersInSector = SECTR_Occluder.GetOccludersInSector(sector1);
          if (occludersInSector != null)
          {
            int count8 = occludersInSector.Count;
            for (int index3 = 0; index3 < count8; ++index3)
            {
              SECTR_Occluder key = occludersInSector[index3];
              if ((bool) (UnityEngine.Object) key.HullMesh && !activeOccluders.ContainsKey(key))
              {
                Matrix4x4 cullingMatrix = key.GetCullingMatrix(position);
                Vector3[] vertsCw = key.VertsCW;
                vector3_1 = cullingMatrix.MultiplyVector(-key.MeshNormal);
                Vector3 normalized = vector3_1.normalized;
                if (vertsCw != null && !SECTR_Geometry.IsPointInFrontOfPlane(position, key.Center, normalized))
                {
                  int length = vertsCw.Length;
                  occluderVerts.Clear();
                  Bounds bounds = new Bounds(key.transform.position, Vector3.zero);
                  for (int index4 = 0; index4 < length; ++index4)
                  {
                    Vector3 point = cullingMatrix.MultiplyPoint3x4(vertsCw[index4]);
                    bounds.Encapsulate(point);
                    occluderVerts.Add(new ClipVertex(new Vector4(point.x, point.y, point.z, 1f), 0.0f));
                  }
                  if (SECTR_Geometry.FrustumIntersectsBounds(key.BoundingBox, cullingPlanes, num4, out int _))
                  {
                    List<Plane> newFrustum;
                    if (frustumPool.Count > 0)
                    {
                      newFrustum = frustumPool.Pop();
                      newFrustum.Clear();
                    }
                    else
                      newFrustum = new List<Plane>(length + 1);
                    _BuildFrustumFromHull(camera, true, occluderVerts, ref newFrustum);
                    newFrustum.Add(new Plane(normalized, key.Center));
                    occluderFrustums.Add(newFrustum);
                    activeOccluders[key] = key;
                  }
                }
              }
            }
          }
        }
        if (count3 > 0)
        {
          lock (cullingWorkQueue)
          {
            cullingWorkQueue.Enqueue(new ThreadCullData(sector1, this, position, cullingPlanes, occluderFrustums, num4, shadowDistance, simpleCulling));
            Monitor.Pulse(cullingWorkQueue);
          }
          Interlocked.Increment(ref remainingThreadWork);
        }
        else
          _FrustumCullSector(sector1, position, cullingPlanes, occluderFrustums, num4, shadowDistance, simpleCulling, ref visibleRenderers, ref visibleLights, ref visibleTerrains, ref shadowLights);
        int count9 = simpleCulling ? 0 : visibilityNode.sector.Portals.Count;
        for (int index5 = 0; index5 < count9; ++index5)
        {
          SECTR_Portal portal = visibilityNode.sector.Portals[index5];
          bool flag1 = (portal.Flags & SECTR_Portal.PortalFlags.PassThrough) != 0;
          if ((bool) (UnityEngine.Object) portal.HullMesh | flag1 && (portal.Flags & SECTR_Portal.PortalFlags.Closed) == 0)
          {
            bool forwardTraversal = visibilityNode.sector == portal.FrontSector;
            SECTR_Sector sector2 = forwardTraversal ? portal.BackSector : portal.FrontSector;
            bool flag2 = !(bool) (UnityEngine.Object) sector2;
            if (!flag2)
              flag2 = SECTR_Geometry.IsPointInFrontOfPlane(position, portal.Center, portal.Normal) != forwardTraversal;
            if (!flag2 && (bool) (UnityEngine.Object) visibilityNode.portal)
            {
              vector3_1 = portal.Center - visibilityNode.portal.Center;
              flag2 = Vector3.Dot(vector3_1.normalized, visibilityNode.forwardTraversal ? visibilityNode.portal.ReverseNormal : visibilityNode.portal.Normal) < 0.0;
            }
            if (!flag2 && !flag1)
            {
              int count10 = occluderFrustums.Count;
              for (int index6 = 0; index6 < count10; ++index6)
              {
                if (SECTR_Geometry.FrustumContainsBounds(portal.BoundingBox, occluderFrustums[index6]))
                {
                  flag2 = true;
                  break;
                }
              }
            }
            if (!flag2)
            {
              if (!flag1)
              {
                portalVertices.Clear();
                Matrix4x4 localToWorldMatrix = portal.transform.localToWorldMatrix;
                Vector3[] vector3Array = forwardTraversal ? portal.VertsCCW : portal.VertsCW;
                if (vector3Array != null)
                {
                  int length = vector3Array.Length;
                  for (int index7 = 0; index7 < length; ++index7)
                  {
                    Vector3 vector3_2 = localToWorldMatrix.MultiplyPoint3x4(vector3Array[index7]);
                    portalVertices.Add(new ClipVertex(new Vector4(vector3_2.x, vector3_2.y, vector3_2.z, 1f), 0.0f));
                  }
                }
              }
              newFrustum.Clear();
              if (!flag1 && !portal.IsPointInHull(position, num2))
              {
                int count11 = visibilityNode.frustumPlanes.Count;
                for (int index8 = 0; index8 < count11; ++index8)
                {
                  Plane frustumPlane = visibilityNode.frustumPlanes[index8];
                  Vector4 a = new Vector4(frustumPlane.normal.x, frustumPlane.normal.y, frustumPlane.normal.z, frustumPlane.distance);
                  bool flag3 = true;
                  bool flag4 = true;
                  for (int index9 = 0; index9 < portalVertices.Count; ++index9)
                  {
                    Vector4 vertex = portalVertices[index9].vertex;
                    float side = Vector4.Dot(a, vertex);
                    portalVertices[index9] = new ClipVertex(vertex, side);
                    flag3 = flag3 && side > 0.0;
                    flag4 = flag4 && side <= -1.0 / 1000.0;
                  }
                  if (flag4)
                  {
                    portalVertices.Clear();
                    break;
                  }
                  if (!flag3)
                  {
                    int count12 = portalVertices.Count;
                    for (int index10 = 0; index10 < count12; ++index10)
                    {
                      int index11 = (index10 + 1) % portalVertices.Count;
                      float side1 = portalVertices[index10].side;
                      float side2 = portalVertices[index11].side;
                      if (side1 > 0.0 && side2 <= -1.0 / 1000.0 || side2 > 0.0 && side1 <= -1.0 / 1000.0)
                      {
                        Vector4 vertex1 = portalVertices[index10].vertex;
                        Vector4 vertex2 = portalVertices[index11].vertex;
                        float num5 = side1 / Vector4.Dot(a, vertex1 - vertex2);
                        Vector4 vertex3 = (vertex1 + num5 * (vertex2 - vertex1)) with
                        {
                          w = 1f
                        };
                        portalVertices.Insert(index10 + 1, new ClipVertex(vertex3, 0.0f));
                        ++count12;
                      }
                    }
                    int index12 = 0;
                    while (index12 < count12)
                    {
                      if (portalVertices[index12].side < -1.0 / 1000.0)
                      {
                        portalVertices.RemoveAt(index12);
                        --count12;
                      }
                      else
                        ++index12;
                    }
                  }
                }
                _BuildFrustumFromHull(camera, forwardTraversal, portalVertices, ref newFrustum);
              }
              else
                newFrustum.AddRange(frustumPlanes);
              if (newFrustum.Count > 2)
                nodeStack.Push(new VisibilityNode(this, sector2, portal, newFrustum, forwardTraversal));
            }
          }
        }
      }
      if (visibilityNode.frustumPlanes != null)
      {
        visibilityNode.frustumPlanes.Clear();
        frustumPool.Push(visibilityNode.frustumPlanes);
      }
    }
    if (count3 > 0)
    {
      while (remainingThreadWork > 0)
      {
        while (cullingWorkQueue.Count > 0)
        {
          ThreadCullData cullData = new ThreadCullData();
          lock (cullingWorkQueue)
          {
            if (cullingWorkQueue.Count > 0)
              cullData = cullingWorkQueue.Dequeue();
          }
          if (cullData.cullingMode == ThreadCullData.CullingModes.Graph)
          {
            _FrustumCullSectorThread(cullData);
            Interlocked.Decrement(ref remainingThreadWork);
          }
        }
      }
      remainingThreadWork = 0;
    }
    if (shadowLights.Count > 0 && CullShadows)
    {
      shadowSectorTable.Clear();
      Dictionary<SECTR_Member.Child, int>.Enumerator enumerator1 = shadowLights.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        SECTR_Member.Child key1 = enumerator1.Current.Key;
        List<SECTR_Sector> sectrSectorList;
        if ((bool) (UnityEngine.Object) key1.member && key1.member.IsSector)
        {
          shadowSectors.Clear();
          shadowSectors.Add((SECTR_Sector) key1.member);
          sectrSectorList = shadowSectors;
        }
        else if ((bool) (UnityEngine.Object) key1.member && key1.member.Sectors.Count > 0)
        {
          sectrSectorList = key1.member.Sectors;
        }
        else
        {
          SECTR_Sector.GetContaining(ref shadowSectors, key1.lightBounds);
          sectrSectorList = shadowSectors;
        }
        int count13 = sectrSectorList.Count;
        for (int index = 0; index < count13; ++index)
        {
          SECTR_Sector key2 = sectrSectorList[index];
          List<SECTR_Member.Child> childList;
          if (!shadowSectorTable.TryGetValue(key2, out childList))
          {
            childList = shadowLightPool.Count > 0 ? shadowLightPool.Pop() : new List<SECTR_Member.Child>(16);
            shadowSectorTable[key2] = childList;
          }
          childList.Add(key1);
        }
      }
      Dictionary<SECTR_Sector, List<SECTR_Member.Child>>.Enumerator enumerator2 = shadowSectorTable.GetEnumerator();
      while (enumerator2.MoveNext())
      {
        SECTR_Sector key = enumerator2.Current.Key;
        List<SECTR_Member.Child> sectorShadowLights = enumerator2.Current.Value;
        if (count3 > 0)
        {
          lock (cullingWorkQueue)
          {
            cullingWorkQueue.Enqueue(new ThreadCullData(key, position, sectorShadowLights));
            Monitor.Pulse(cullingWorkQueue);
          }
          Interlocked.Increment(ref remainingThreadWork);
        }
        else
          _ShadowCullSector(key, sectorShadowLights, ref visibleRenderers, ref visibleTerrains);
      }
      if (count3 > 0)
      {
        while (remainingThreadWork > 0)
        {
          while (cullingWorkQueue.Count > 0)
          {
            ThreadCullData cullData = new ThreadCullData();
            lock (cullingWorkQueue)
            {
              if (cullingWorkQueue.Count > 0)
                cullData = cullingWorkQueue.Dequeue();
            }
            if (cullData.cullingMode == ThreadCullData.CullingModes.Shadow)
            {
              _ShadowCullSectorThread(cullData);
              Interlocked.Decrement(ref remainingThreadWork);
            }
          }
        }
        remainingThreadWork = 0;
      }
      enumerator2 = shadowSectorTable.GetEnumerator();
      while (enumerator2.MoveNext())
      {
        List<SECTR_Member.Child> childList = enumerator2.Current.Value;
        childList.Clear();
        shadowLightPool.Push(childList);
      }
    }
    _ApplyCulling(invisibleLayer);
    int count14 = occluderFrustums.Count;
    for (int index = 0; index < count14; ++index)
    {
      occluderFrustums[index].Clear();
      frustumPool.Push(occluderFrustums[index]);
    }
    occluderFrustums.Clear();
    activeOccluders.Clear();
  }

  private void OnPostRender()
  {
    if (!didCull)
      return;
    UndoCulling();
    didCull = false;
  }

  private void _CullingWorker()
  {
    while (true)
    {
      ThreadCullData cullData;
      do
      {
        cullData = new ThreadCullData();
        lock (cullingWorkQueue)
        {
          while (cullingWorkQueue.Count == 0)
            Monitor.Wait(cullingWorkQueue);
          cullData = cullingWorkQueue.Dequeue();
        }
        switch (cullData.cullingMode)
        {
          case ThreadCullData.CullingModes.Graph:
            _FrustumCullSectorThread(cullData);
            break;
          case ThreadCullData.CullingModes.Shadow:
            _ShadowCullSectorThread(cullData);
            break;
        }
      }
      while (cullData.cullingMode != ThreadCullData.CullingModes.Graph && cullData.cullingMode != ThreadCullData.CullingModes.Shadow);
      Interlocked.Decrement(ref remainingThreadWork);
    }
  }

  private void _FrustumCullSectorThread(ThreadCullData cullData)
  {
    Dictionary<int, int> visibleRenderers = null;
    Dictionary<int, int> visibleLights = null;
    Dictionary<int, int> visibleTerrains = null;
    Dictionary<SECTR_Member.Child, int> shadowLights = null;
    lock (threadVisibleListPool)
    {
      visibleRenderers = threadVisibleListPool.Count > 0 ? threadVisibleListPool.Pop() : new Dictionary<int, int>(32);
      visibleLights = threadVisibleListPool.Count > 0 ? threadVisibleListPool.Pop() : new Dictionary<int, int>(32);
      visibleTerrains = threadVisibleListPool.Count > 0 ? threadVisibleListPool.Pop() : new Dictionary<int, int>(32);
    }
    lock (threadShadowLightPool)
      shadowLights = threadShadowLightPool.Count > 0 ? threadShadowLightPool.Pop() : new Dictionary<SECTR_Member.Child, int>(32);
    _FrustumCullSector(cullData.sector, cullData.cameraPos, cullData.cullingPlanes, cullData.occluderFrustums, cullData.baseMask, cullData.shadowDistance, cullData.cullingSimpleCulling, ref visibleRenderers, ref visibleLights, ref visibleTerrains, ref shadowLights);
    lock (this.visibleRenderers)
    {
      Dictionary<int, int>.Enumerator enumerator = visibleRenderers.GetEnumerator();
      while (enumerator.MoveNext())
      {
        int key = enumerator.Current.Key;
        this.visibleRenderers[key] = key;
      }
    }
    lock (this.visibleLights)
    {
      Dictionary<int, int>.Enumerator enumerator = visibleLights.GetEnumerator();
      while (enumerator.MoveNext())
      {
        int key = enumerator.Current.Key;
        this.visibleLights[key] = key;
      }
    }
    lock (this.visibleTerrains)
    {
      Dictionary<int, int>.Enumerator enumerator = visibleTerrains.GetEnumerator();
      while (enumerator.MoveNext())
      {
        int key = enumerator.Current.Key;
        this.visibleTerrains[key] = key;
      }
    }
    lock (this.shadowLights)
    {
      Dictionary<SECTR_Member.Child, int>.Enumerator enumerator = shadowLights.GetEnumerator();
      while (enumerator.MoveNext())
        this.shadowLights[enumerator.Current.Key] = 0;
    }
    lock (threadVisibleListPool)
    {
      visibleRenderers.Clear();
      visibleLights.Clear();
      visibleTerrains.Clear();
      threadVisibleListPool.Push(visibleRenderers);
      threadVisibleListPool.Push(visibleLights);
      threadVisibleListPool.Push(visibleTerrains);
    }
    lock (threadShadowLightPool)
    {
      shadowLights.Clear();
      threadShadowLightPool.Push(shadowLights);
    }
    lock (threadFrustumPool)
    {
      cullData.cullingPlanes.Clear();
      threadFrustumPool.Push(cullData.cullingPlanes);
      int count = cullData.occluderFrustums.Count;
      for (int index = 0; index < count; ++index)
      {
        cullData.occluderFrustums[index].Clear();
        threadFrustumPool.Push(cullData.occluderFrustums[index]);
      }
    }
    lock (threadOccluderPool)
    {
      cullData.occluderFrustums.Clear();
      threadOccluderPool.Push(cullData.occluderFrustums);
    }
  }

  private void _ShadowCullSectorThread(ThreadCullData cullData)
  {
    Dictionary<int, int> visibleRenderers = null;
    Dictionary<int, int> visibleTerrains = null;
    lock (threadVisibleListPool)
    {
      visibleRenderers = threadVisibleListPool.Count > 0 ? threadVisibleListPool.Pop() : new Dictionary<int, int>(32);
      visibleTerrains = threadVisibleListPool.Count > 0 ? threadVisibleListPool.Pop() : new Dictionary<int, int>(32);
    }
    _ShadowCullSector(cullData.sector, cullData.sectorShadowLights, ref visibleRenderers, ref visibleTerrains);
    lock (this.visibleRenderers)
    {
      Dictionary<int, int>.Enumerator enumerator = visibleRenderers.GetEnumerator();
      while (enumerator.MoveNext())
      {
        int key = enumerator.Current.Key;
        this.visibleRenderers[key] = key;
      }
    }
    lock (this.visibleTerrains)
    {
      Dictionary<int, int>.Enumerator enumerator = visibleTerrains.GetEnumerator();
      while (enumerator.MoveNext())
      {
        int key = enumerator.Current.Key;
        this.visibleTerrains[key] = key;
      }
    }
    lock (threadVisibleListPool)
    {
      visibleRenderers.Clear();
      visibleTerrains.Clear();
      threadVisibleListPool.Push(visibleRenderers);
      threadVisibleListPool.Push(visibleTerrains);
    }
  }

  private static void _FrustumCullSector(
    SECTR_Sector sector,
    Vector3 cameraPos,
    List<Plane> cullingPlanes,
    List<List<Plane>> occluderFrustums,
    int baseMask,
    float shadowDistance,
    bool forceGroupCull,
    ref Dictionary<int, int> visibleRenderers,
    ref Dictionary<int, int> visibleLights,
    ref Dictionary<int, int> visibleTerrains,
    ref Dictionary<SECTR_Member.Child, int> shadowLights)
  {
    _FrustumCull(sector, cameraPos, cullingPlanes, occluderFrustums, baseMask, shadowDistance, forceGroupCull, ref visibleRenderers, ref visibleLights, ref visibleTerrains, ref shadowLights);
    int count = sector.Members.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Member member = sector.Members[index];
      if (member.HasRenderBounds || member.HasLightBounds)
        _FrustumCull(member, cameraPos, cullingPlanes, occluderFrustums, baseMask, shadowDistance, forceGroupCull, ref visibleRenderers, ref visibleLights, ref visibleTerrains, ref shadowLights);
    }
  }

  private static void _FrustumCull(
    SECTR_Member member,
    Vector3 cameraPos,
    List<Plane> frustumPlanes,
    List<List<Plane>> occluders,
    int baseMask,
    float shadowDistance,
    bool forceGroupCull,
    ref Dictionary<int, int> visibleRenderers,
    ref Dictionary<int, int> visibleLights,
    ref Dictionary<int, int> visibleTerrains,
    ref Dictionary<SECTR_Member.Child, int> shadowLights)
  {
    int inMask1 = baseMask;
    int inMask2 = baseMask;
    int outMask1 = 0;
    int outMask2 = 0;
    bool flag1 = member.CullEachChild && !forceGroupCull;
    bool flag2 = member.HasRenderBounds && SECTR_Geometry.FrustumIntersectsBounds(member.RenderBounds, frustumPlanes, inMask1, out outMask1);
    bool flag3 = member.HasLightBounds && SECTR_Geometry.FrustumIntersectsBounds(member.LightBounds, frustumPlanes, inMask2, out outMask2);
    int count1 = occluders.Count;
    for (int index = 0; index < count1 && flag2 | flag3; ++index)
    {
      List<Plane> occluder = occluders[index];
      if (flag2)
        flag2 = !SECTR_Geometry.FrustumContainsBounds(member.RenderBounds, occluder);
      if (flag3)
        flag3 = !SECTR_Geometry.FrustumContainsBounds(member.LightBounds, occluder);
    }
    if (flag2)
    {
      int count2 = member.Renderers.Count;
      for (int index = 0; index < count2; ++index)
      {
        SECTR_Member.Child renderer = member.Renderers[index];
        if (renderer.renderHash != 0 && !visibleRenderers.ContainsKey(renderer.renderHash) && (!flag1 || _IsVisible(renderer.rendererBounds, frustumPlanes, outMask1, occluders)))
          visibleRenderers.Add(renderer.renderHash, renderer.renderHash);
      }
      int count3 = member.Terrains.Count;
      for (int index = 0; index < count3; ++index)
      {
        SECTR_Member.Child terrain = member.Terrains[index];
        if (terrain.terrainHash != 0 && !visibleTerrains.ContainsKey(terrain.terrainHash) && (!flag1 || _IsVisible(terrain.terrainBounds, frustumPlanes, outMask1, occluders)))
          visibleTerrains.Add(terrain.terrainHash, terrain.terrainHash);
      }
    }
    if (!flag3)
      return;
    int count4 = member.Lights.Count;
    for (int index = 0; index < count4; ++index)
    {
      SECTR_Member.Child light = member.Lights[index];
      if (light.lightHash != 0 && !visibleLights.ContainsKey(light.lightHash) && (!flag1 || _IsVisible(light.lightBounds, frustumPlanes, outMask1, occluders)))
      {
        visibleLights.Add(light.lightHash, light.lightHash);
        if (light.shadowLight && !shadowLights.ContainsKey(light) && Vector3.Distance(cameraPos, light.shadowLightPosition) - (double) light.shadowLightRange <= shadowDistance)
          shadowLights.Add(light, 0);
      }
    }
  }

  private static void _ShadowCullSector(
    SECTR_Sector sector,
    List<SECTR_Member.Child> sectorShadowLights,
    ref Dictionary<int, int> visibleRenderers,
    ref Dictionary<int, int> visibleTerrains)
  {
    if (sector.ShadowCaster)
      _ShadowCull(sector, sectorShadowLights, ref visibleRenderers, ref visibleTerrains);
    int count = sector.Members.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Member member = sector.Members[index];
      if (member.ShadowCaster)
        _ShadowCull(member, sectorShadowLights, ref visibleRenderers, ref visibleTerrains);
    }
  }

  private static void _ShadowCull(
    SECTR_Member member,
    List<SECTR_Member.Child> shadowLights,
    ref Dictionary<int, int> visibleRenderers,
    ref Dictionary<int, int> visibleTerrains)
  {
    int count1 = shadowLights.Count;
    int count2 = member.ShadowCasters.Count;
    if (member.CullEachChild)
    {
      for (int index1 = 0; index1 < count2; ++index1)
      {
        SECTR_Member.Child shadowCaster = member.ShadowCasters[index1];
        if (shadowCaster.renderHash != 0 && !visibleRenderers.ContainsKey(shadowCaster.renderHash))
        {
          for (int index2 = 0; index2 < count1; ++index2)
          {
            SECTR_Member.Child shadowLight = shadowLights[index2];
            if ((shadowLight.shadowCullingMask & 1 << shadowCaster.layer) != 0 && (shadowLight.shadowLightType == LightType.Spot && shadowCaster.rendererBounds.Intersects(shadowLight.lightBounds) || shadowLight.shadowLightType == LightType.Point && SECTR_Geometry.BoundsIntersectsSphere(shadowCaster.rendererBounds, shadowLight.shadowLightPosition, shadowLight.shadowLightRange)))
            {
              visibleRenderers.Add(shadowCaster.renderHash, shadowCaster.renderHash);
              break;
            }
          }
        }
        if (shadowCaster.terrainHash != 0 && !visibleTerrains.ContainsKey(shadowCaster.terrainHash))
        {
          for (int index3 = 0; index3 < count1; ++index3)
          {
            SECTR_Member.Child shadowLight = shadowLights[index3];
            if ((shadowLight.shadowCullingMask & 1 << shadowCaster.layer) != 0 && (shadowLight.shadowLightType == LightType.Spot && shadowCaster.terrainBounds.Intersects(shadowLight.lightBounds) || shadowLight.shadowLightType == LightType.Point && SECTR_Geometry.BoundsIntersectsSphere(shadowCaster.terrainBounds, shadowLight.shadowLightPosition, shadowLight.shadowLightRange)))
            {
              visibleTerrains.Add(shadowCaster.terrainHash, shadowCaster.terrainHash);
              break;
            }
          }
        }
      }
    }
    else
    {
      for (int index4 = 0; index4 < count1; ++index4)
      {
        SECTR_Member.Child shadowLight = shadowLights[index4];
        if ((shadowLight.shadowLightType == LightType.Spot ? (member.RenderBounds.Intersects(shadowLight.lightBounds) ? 1 : 0) : (SECTR_Geometry.BoundsIntersectsSphere(member.RenderBounds, shadowLight.shadowLightPosition, shadowLight.shadowLightRange) ? 1 : 0)) != 0)
        {
          int shadowCullingMask = shadowLight.shadowCullingMask;
          for (int index5 = 0; index5 < count2; ++index5)
          {
            SECTR_Member.Child shadowCaster = member.ShadowCasters[index5];
            if (shadowCaster.renderHash != 0 && shadowCaster.terrainHash != 0)
            {
              if ((shadowCullingMask & 1 << shadowCaster.layer) != 0)
              {
                if (!visibleRenderers.ContainsKey(shadowCaster.renderHash))
                  visibleRenderers.Add(shadowCaster.renderHash, shadowCaster.renderHash);
                if (!visibleTerrains.ContainsKey(shadowCaster.terrainHash))
                  visibleTerrains.Add(shadowCaster.terrainHash, shadowCaster.terrainHash);
              }
            }
            else if (shadowCaster.renderHash != 0 && !visibleRenderers.ContainsKey(shadowCaster.renderHash) && (shadowCullingMask & 1 << shadowCaster.layer) != 0)
              visibleRenderers.Add(shadowCaster.renderHash, shadowCaster.renderHash);
            else if (shadowCaster.terrainHash != 0 && !visibleTerrains.ContainsKey(shadowCaster.terrainHash) && (shadowCullingMask & 1 << shadowCaster.layer) != 0)
              visibleTerrains.Add(shadowCaster.terrainHash, shadowCaster.terrainHash);
          }
        }
      }
    }
  }

  private static bool _IsVisible(
    Bounds childBounds,
    List<Plane> frustumPlanes,
    int parentMask,
    List<List<Plane>> occluders)
  {
    if (!SECTR_Geometry.FrustumIntersectsBounds(childBounds, frustumPlanes, parentMask, out int _))
      return false;
    int count = occluders.Count;
    for (int index = 0; index < count; ++index)
    {
      if (SECTR_Geometry.FrustumContainsBounds(childBounds, occluders[index]))
        return false;
    }
    return true;
  }

  private void _ApplyCulling(int cullingInvisibleLayer)
  {
    int count1 = SECTR_Member.All.Count;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      SECTR_Member sectrMember = SECTR_Member.All[index1];
      int count2 = sectrMember.Children.Count;
      for (int index2 = 0; index2 < count2; ++index2)
      {
        SECTR_Member.Child child = sectrMember.Children[index2];
        Renderer renderer = child.renderer;
        if ((bool) (UnityEngine.Object) renderer && !visibleRenderers.ContainsKey(child.renderHash) && renderer.enabled)
        {
          hiddenRenderers.Add(renderer);
          renderer.enabled = false;
        }
        Light light = child.light;
        if ((bool) (UnityEngine.Object) light && !visibleLights.ContainsKey(child.lightHash) && light.enabled)
        {
          hiddenLights.Add(light);
          light.enabled = false;
        }
        Terrain terrain = child.terrain;
        if ((bool) (UnityEngine.Object) terrain && !visibleTerrains.ContainsKey(child.terrainHash) && terrain.enabled)
        {
          hiddenTerrains.Add(terrain);
          terrain.enabled = false;
        }
      }
    }
  }

  private void UndoCulling()
  {
    int count1 = hiddenRenderers.Count;
    for (int index = 0; index < count1; ++index)
      hiddenRenderers[index].enabled = true;
    hiddenRenderers.Clear();
    int count2 = hiddenLights.Count;
    for (int index = 0; index < count2; ++index)
      hiddenLights[index].enabled = true;
    hiddenLights.Clear();
    int count3 = hiddenTerrains.Count;
    for (int index = 0; index < count3; ++index)
      hiddenTerrains[index].enabled = true;
    hiddenTerrains.Clear();
    renderersCulled = count1;
    lightsCulled = count2;
    terrainsCulled = count3;
  }

  private void _BuildFrustumFromHull(
    Camera cullingCamera,
    bool forwardTraversal,
    List<ClipVertex> portalVertices,
    ref List<Plane> newFrustum)
  {
    int count = portalVertices.Count;
    if (count <= 2)
      return;
    for (int index = 0; index < count; ++index)
    {
      Vector3 vertex = portalVertices[index].vertex;
      Vector3 vector3_1 = (Vector3) portalVertices[(index + 1) % count].vertex - vertex;
      if (Vector3.SqrMagnitude(vector3_1) > 1.0 / 1000.0)
      {
        Vector3 vector3_2 = vertex - cullingCamera.transform.position;
        Vector3 inNormal = forwardTraversal ? Vector3.Cross(vector3_1, vector3_2) : Vector3.Cross(vector3_2, vector3_1);
        inNormal.Normalize();
        newFrustum.Add(new Plane(inNormal, vertex));
      }
    }
  }

  private struct VisibilityNode
  {
    public SECTR_Sector sector;
    public SECTR_Portal portal;
    public List<Plane> frustumPlanes;
    public bool forwardTraversal;

    public VisibilityNode(
      SECTR_CullingCamera cullingCamera,
      SECTR_Sector sector,
      SECTR_Portal portal,
      Plane[] frustumPlanes,
      bool forwardTraversal)
    {
      this.sector = sector;
      this.portal = portal;
      if (frustumPlanes == null)
        this.frustumPlanes = null;
      else if (cullingCamera.frustumPool.Count > 0)
      {
        this.frustumPlanes = cullingCamera.frustumPool.Pop();
        this.frustumPlanes.AddRange(frustumPlanes);
      }
      else
        this.frustumPlanes = new List<Plane>(frustumPlanes);
      this.forwardTraversal = forwardTraversal;
    }

    public VisibilityNode(
      SECTR_CullingCamera cullingCamera,
      SECTR_Sector sector,
      SECTR_Portal portal,
      List<Plane> frustumPlanes,
      bool forwardTraversal)
    {
      this.sector = sector;
      this.portal = portal;
      if (frustumPlanes == null)
        this.frustumPlanes = null;
      else if (cullingCamera.frustumPool.Count > 0)
      {
        this.frustumPlanes = cullingCamera.frustumPool.Pop();
        this.frustumPlanes.AddRange(frustumPlanes);
      }
      else
        this.frustumPlanes = new List<Plane>(frustumPlanes);
      this.forwardTraversal = forwardTraversal;
    }
  }

  private struct ClipVertex
  {
    public Vector4 vertex;
    public float side;

    public ClipVertex(Vector4 vertex, float side)
    {
      this.vertex = vertex;
      this.side = side;
    }
  }

  private struct ThreadCullData
  {
    public SECTR_Sector sector;
    public Vector3 cameraPos;
    public List<Plane> cullingPlanes;
    public List<List<Plane>> occluderFrustums;
    public int baseMask;
    public float shadowDistance;
    public bool cullingSimpleCulling;
    public List<SECTR_Member.Child> sectorShadowLights;
    public CullingModes cullingMode;

    public ThreadCullData(
      SECTR_Sector sector,
      SECTR_CullingCamera cullingCamera,
      Vector3 cameraPos,
      List<Plane> cullingPlanes,
      List<List<Plane>> occluderFrustums,
      int baseMask,
      float shadowDistance,
      bool cullingSimpleCulling)
    {
      this.sector = sector;
      this.cameraPos = cameraPos;
      this.baseMask = baseMask;
      this.shadowDistance = shadowDistance;
      this.cullingSimpleCulling = cullingSimpleCulling;
      sectorShadowLights = null;
      lock (cullingCamera.threadOccluderPool)
        this.occluderFrustums = cullingCamera.threadOccluderPool.Count > 0 ? cullingCamera.threadOccluderPool.Pop() : new List<List<Plane>>(occluderFrustums.Count);
      lock (cullingCamera.threadFrustumPool)
      {
        if (cullingCamera.threadFrustumPool.Count > 0)
        {
          this.cullingPlanes = cullingCamera.threadFrustumPool.Pop();
          this.cullingPlanes.AddRange(cullingPlanes);
        }
        else
          this.cullingPlanes = new List<Plane>(cullingPlanes);
        int count = occluderFrustums.Count;
        for (int index = 0; index < count; ++index)
        {
          List<Plane> planeList;
          if (cullingCamera.threadFrustumPool.Count > 0)
          {
            planeList = cullingCamera.threadFrustumPool.Pop();
            planeList.AddRange(occluderFrustums[index]);
          }
          else
            planeList = new List<Plane>(occluderFrustums[index]);
          this.occluderFrustums.Add(planeList);
        }
      }
      cullingMode = CullingModes.Graph;
    }

    public ThreadCullData(
      SECTR_Sector sector,
      Vector3 cameraPos,
      List<SECTR_Member.Child> sectorShadowLights)
    {
      this.sector = sector;
      this.cameraPos = cameraPos;
      cullingPlanes = null;
      occluderFrustums = null;
      baseMask = 0;
      shadowDistance = 0.0f;
      cullingSimpleCulling = false;
      this.sectorShadowLights = sectorShadowLights;
      cullingMode = CullingModes.Shadow;
    }

    public enum CullingModes
    {
      None,
      Graph,
      Shadow,
    }
  }
}
