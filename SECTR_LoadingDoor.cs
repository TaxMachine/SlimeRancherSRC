// Decompiled with JetBrains decompiler
// Type: SECTR_LoadingDoor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SECTR/Stream/SECTR Loading Door")]
public class SECTR_LoadingDoor : SECTR_Door
{
  private Texture2D fadeTexture;
  private Dictionary<Collider, LoadRequest> loadRequests = new Dictionary<Collider, LoadRequest>(4);
  [SECTR_ToolTip("Specifies which layers are allow to cause loads (vs simply opening the door).")]
  public LayerMask LoadLayers = 16777215;
  [SECTR_ToolTip("Should screen fade to black before loading.")]
  public bool FadeBeforeLoad;
  [SECTR_ToolTip("How long to fade out before loading. Also, how long to fade back in.", "FadeBeforeLoad")]
  public float FadeTime = 1f;
  [SECTR_ToolTip("How long to stay faded out. Helps cover pops right at the moment of loading.", "FadeBeforeLoad")]
  public float HoldTime = 0.1f;
  [SECTR_ToolTip("The color to fade the screen to on load.", "FadeBeforeLoad")]
  public Color FadeColor = Color.black;

  protected override void OnEnable()
  {
    base.OnEnable();
    if (!FadeBeforeLoad)
      return;
    fadeTexture = new Texture2D(1, 1);
    fadeTexture.SetPixel(0, 0, FadeColor);
    fadeTexture.Apply();
  }

  protected override void OnTriggerEnter(Collider other)
  {
    base.OnTriggerEnter(other);
    if (!(bool) (Object) Portal || (LoadLayers & 1 << other.gameObject.layer) == 0)
      return;
    SECTR_Chunk oppositeChunk = _GetOppositeChunk(other.transform.position);
    if (!(bool) (Object) oppositeChunk)
      return;
    SECTR_Chunk sectrChunk = null;
    LoadRequest loadRequest;
    if (loadRequests.TryGetValue(other, out loadRequest))
    {
      if ((bool) (Object) loadRequest.chunkToUnload)
      {
        sectrChunk = loadRequest.chunkToUnload;
        loadRequest.chunkToUnload = null;
      }
    }
    else
      loadRequest = new LoadRequest();
    if (FadeBeforeLoad && !oppositeChunk.IsLoaded())
      loadRequest.fadeMode = FadeMode.FadeOut;
    loadRequest.enteredFront = oppositeChunk.Sector == Portal.BackSector;
    loadRequest.enteredBack = oppositeChunk.Sector == Portal.FrontSector;
    if (FadeBeforeLoad)
    {
      loadRequest.chunkToLoad = oppositeChunk;
    }
    else
    {
      oppositeChunk.AddReference();
      loadRequest.loadedChunk = oppositeChunk;
    }
    loadRequests[other] = loadRequest;
    if (!(bool) (Object) sectrChunk)
      return;
    sectrChunk.RemoveReference();
  }

  protected override void OnTriggerExit(Collider other)
  {
    base.OnTriggerExit(other);
    if (!(bool) (Object) Portal || (LoadLayers & 1 << other.gameObject.layer) == 0)
      return;
    SECTR_Chunk oppositeChunk = _GetOppositeChunk(other.transform.position);
    if (!(bool) (Object) oppositeChunk)
      return;
    LoadRequest loadRequest = loadRequests[other];
    if (FadeBeforeLoad && loadRequest.fadeMode == FadeMode.FadeOut)
      loadRequest.fadeMode = FadeMode.FadeIn;
    bool flag1 = oppositeChunk.Sector == Portal.FrontSector;
    bool flag2 = oppositeChunk.Sector == Portal.BackSector;
    loadRequest.chunkToUnload = !(bool) (Object) loadRequest.loadedChunk || !(loadRequest.enteredFront & flag2) && !(loadRequest.enteredBack & flag1) ? (loadRequest.enteredFront & flag1 || loadRequest.enteredBack & flag2 ? oppositeChunk : loadRequest.loadedChunk) : loadRequest.loadedChunk;
    if (loadRequests.Count <= 1 && !IsClosed())
      return;
    if ((bool) (Object) loadRequest.chunkToUnload)
      loadRequest.chunkToUnload.RemoveReference();
    loadRequests.Remove(other);
  }

  private void OnGUI()
  {
    if (!FadeBeforeLoad)
      return;
    float num = Time.deltaTime / FadeTime;
    float a = 0.0f;
    foreach (LoadRequest loadRequest in loadRequests.Values)
    {
      switch (loadRequest.fadeMode)
      {
        case FadeMode.FadeIn:
          loadRequest.fadeAmount -= num;
          if (loadRequest.fadeAmount <= 0.0)
          {
            loadRequest.fadeMode = FadeMode.None;
            break;
          }
          break;
        case FadeMode.FadeOut:
          loadRequest.fadeAmount += num;
          if (loadRequest.fadeAmount >= 1.0)
          {
            if ((bool) (Object) loadRequest.chunkToLoad)
            {
              loadRequest.chunkToLoad.AddReference();
              loadRequest.loadedChunk = loadRequest.chunkToLoad;
              loadRequest.chunkToLoad = null;
            }
            loadRequest.fadeMode = FadeMode.Hold;
            loadRequest.holdStart = Time.time;
            break;
          }
          break;
        case FadeMode.Hold:
          if (!CanOpen())
          {
            loadRequest.holdStart = Time.time;
            break;
          }
          if (Time.time >= loadRequest.holdStart + (double) HoldTime)
          {
            loadRequest.fadeMode = FadeMode.FadeIn;
            break;
          }
          break;
      }
      loadRequest.fadeAmount = Mathf.Clamp01(loadRequest.fadeAmount);
      a = Mathf.Max(a, loadRequest.fadeAmount);
    }
    if (a <= 0.0)
      return;
    GUI.color = new Color(1f, 1f, 1f, a);
    GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), fadeTexture);
  }

  protected override bool CanOpen() => !(bool) (Object) Portal || _IsSectorLoaded(Portal.FrontSector) && _IsSectorLoaded(Portal.BackSector);

  private void OnClose()
  {
    if (loadRequests.Count != 1)
      return;
    Dictionary<Collider, LoadRequest>.Enumerator enumerator = loadRequests.GetEnumerator();
    enumerator.MoveNext();
    LoadRequest loadRequest = enumerator.Current.Value;
    if (!(bool) (Object) loadRequest.chunkToUnload)
      return;
    loadRequest.chunkToUnload.RemoveReference();
    loadRequests.Clear();
  }

  private bool _IsSectorLoaded(SECTR_Sector sector)
  {
    if ((bool) (Object) sector && sector.Frozen)
    {
      SECTR_Chunk component = sector.GetComponent<SECTR_Chunk>();
      if ((bool) (Object) component && !component.IsLoaded())
        return false;
    }
    return true;
  }

  private SECTR_Chunk _GetOppositeChunk(Vector3 position)
  {
    if ((bool) (Object) Portal)
    {
      SECTR_Sector sectrSector = SECTR_Geometry.IsPointInFrontOfPlane(position, Portal.Center, Portal.Normal) ? Portal.BackSector : Portal.FrontSector;
      if ((bool) (Object) sectrSector)
        return sectrSector.GetComponent<SECTR_Chunk>();
    }
    return null;
  }

  private enum FadeMode
  {
    None,
    FadeIn,
    FadeOut,
    Hold,
  }

  private class LoadRequest
  {
    public SECTR_Chunk chunkToLoad;
    public SECTR_Chunk chunkToUnload;
    public SECTR_Chunk loadedChunk;
    public bool enteredFront;
    public bool enteredBack;
    public FadeMode fadeMode;
    public float fadeAmount;
    public float holdStart;
  }
}
