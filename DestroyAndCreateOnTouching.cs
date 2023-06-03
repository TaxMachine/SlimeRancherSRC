// Decompiled with JetBrains decompiler
// Type: DestroyAndCreateOnTouching
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class DestroyAndCreateOnTouching : SRBehaviour
{
  [Tooltip("Prefab to instantiate.")]
  public GameObject prefab;
  private bool hasCollided;
  private int delayTimeout;
  private object timeout;

  public void OnCollisionEnter(Collision col)
  {
    if (hasCollided)
      return;
    InstantiateDynamic(prefab, transform.position, transform.rotation);
    StartCoroutine(DestroyAfterFrame());
    hasCollided = true;
  }

  private IEnumerator DestroyAfterFrame()
  {
    // ISSUE: reference to a compiler-generated field
    int num = delayTimeout;
    DestroyAndCreateOnTouching createOnTouching = this;
    if (num != 0)
    {
      if (num != 1)
        yield return false;
      // ISSUE: reference to a compiler-generated field
      delayTimeout = -1;
      Destroyer.DestroyActor(createOnTouching.gameObject, "DestroyAndCreateOnTouching.DestroyAfterFrame");
      yield return false;
    }
    // ISSUE: reference to a compiler-generated field
    delayTimeout = -1;
    // ISSUE: reference to a compiler-generated field
    timeout = new WaitForEndOfFrame();
    // ISSUE: reference to a compiler-generated field
    delayTimeout = 1;
    yield return true;
  }
}
