// Decompiled with JetBrains decompiler
// Type: Assets.Script.Util.Extensions.ComponentExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace Assets.Script.Util.Extensions
{
  public static class ComponentExtensions
  {
    public static T GetComponentInParent<T>(this Component component, bool includeInactive = false) where T : Component => component.gameObject.GetComponentInParent<T>(includeInactive);

    public static T GetRequiredComponent<T>(this Component component) where T : Component => component.gameObject.GetRequiredComponent<T>();

    public static T GetRequiredComponentInParent<T>(this Component component, bool includeInactive = false) where T : Component => component.gameObject.GetRequiredComponentInParent<T>(includeInactive);

    public static T GetRequiredComponentInChildren<T>(
      this Component component,
      bool includeInactive = false)
      where T : Component
    {
      return component.gameObject.GetRequiredComponentInChildren<T>(includeInactive);
    }

    public static void DestroyChildren(this Component parent, string source) => parent.DestroyChildren(go => true, source);

    public static void DestroyChildren(
      this Component parent,
      Predicate<GameObject> predicate,
      string source)
    {
      for (int index = 0; index < parent.transform.childCount; ++index)
      {
        GameObject gameObject = parent.transform.GetChild(index).gameObject;
        if (predicate(gameObject))
        {
          if (gameObject.GetComponent<Identifiable>() != null)
            Destroyer.DestroyActor(gameObject, source);
          else
            Destroyer.Destroy(gameObject, source);
        }
      }
    }
  }
}
