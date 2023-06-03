// Decompiled with JetBrains decompiler
// Type: Assets.Script.Util.Extensions.GameObjectExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Util.Extensions
{
  public static class GameObjectExtensions
  {
    public static T GetComponentInParent<T>(this GameObject gameObject, bool includeInactive = false) where T : Component => gameObject.GetComponentsInParent<T>(includeInactive).FirstOrDefault();

    public static T GetRequiredComponent<T>(this GameObject gameObject) where T : Component => gameObject.GetComponent<T>();

    public static T GetRequiredComponentInParent<T>(
      this GameObject gameObject,
      bool includeInactive = false)
      where T : Component
    {
      return gameObject.GetComponentInParent<T>(includeInactive);
    }

    public static T GetRequiredComponentInChildren<T>(
      this GameObject gameObject,
      bool includeInactive = false)
      where T : Component
    {
      return gameObject.GetComponentInChildren<T>(includeInactive);
    }

    public static void StartCoroutine(this GameObject gameObject, IEnumerator coroutine)
    {
      CoroutineRunner coroutineRunner = gameObject.AddComponent<CoroutineRunner>();
      coroutineRunner.StartCoroutine(coroutineRunner.CoroutineWrapper(coroutine));
    }

    public static void DestroyChildren(this GameObject parent, string source) => DestroyChildren(parent.transform, source);

    public static void DestroyChildren(this Transform parent, string source)
    {
      for (int index = 0; index < parent.childCount; ++index)
        Destroyer.Destroy(parent.GetChild(index).gameObject, source);
    }

    private class CoroutineRunner : MonoBehaviour
    {
      public IEnumerator CoroutineWrapper(IEnumerator coroutine)
      {
        // ISSUE: reference to a compiler-generated field
        /*int num = this.\u003C\u003E1__state;
        GameObjectExtensions.CoroutineRunner instance = this;
        if (num != 0)
        {
          if (num != 1)
            return false;
          // ISSUE: reference to a compiler-generated field
          this.\u003C\u003E1__state = -1;
          Destroyer.Destroy((Object) instance, "CoroutineRunner.CoroutineWrapper");
          return false;
        }
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E2__current = (object) coroutine;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = 1;*/
        yield return true;
      }

      public void OnDisable() => Destroyer.Destroy(this, "CoroutineRunner.OnDisable");
    }
  }
}
