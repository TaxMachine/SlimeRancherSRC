// Decompiled with JetBrains decompiler
// Type: Recycler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Recycler : MonoBehaviour, DestroyRequestHandler
{
  private bool hasRecycled;
  public RecycleEvent OnBeforeRecycle;
  public RecycleEvent OnAfterRecycle;
  public ObjectPool pool;

  public void OnEnable() => hasRecycled = false;

  public void OnDestroyRequest(GameObject obj)
  {
    if (hasRecycled)
      return;
    hasRecycled = true;
    if (OnBeforeRecycle != null)
      OnBeforeRecycle(obj);
    pool.Recycle(obj);
    if (OnAfterRecycle == null)
      return;
    OnAfterRecycle(obj);
  }

  public delegate void RecycleEvent(GameObject obj);
}
