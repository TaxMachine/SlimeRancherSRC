// Decompiled with JetBrains decompiler
// Type: Destroyer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class Destroyer
{
  private static Dictionary<int, List<Action<Metadata>>> monitorsDict = new Dictionary<int, List<Action<Metadata>>>();

  public static void Monitor(UnityEngine.Object instance, Action<Metadata> action)
  {
    List<Action<Metadata>> actionList;
    if (!monitorsDict.TryGetValue(instance.GetInstanceID(), out actionList))
      monitorsDict.Add(instance.GetInstanceID(), actionList = new List<Action<Metadata>>());
    actionList.Add(action);
  }

  public static void DestroyActor(GameObject actorObj, string source, bool okIfNonActor = false)
  {
    if (actorObj.GetComponent<Identifiable>() != null)
    {
      SRSingleton<SceneContext>.Instance.GameModel.DestroyActorModel(actorObj);
      Destroy(actorObj, 0.0f, source, true);
    }
    else
      Destroy(actorObj, 0.0f, source);
  }

  public static void DestroyGadget(string siteId, GameObject gadgetObj, string source)
  {
    Destroy(gadgetObj, 0.0f, source, asGadgetOk: true);
    SRSingleton<SceneContext>.Instance.GameModel.DestroyGadgetModel(siteId, gadgetObj);
  }

  public static void Destroy(UnityEngine.Object instance, string source) => Destroy(instance, 0.0f, source);

  public static void Destroy(
    UnityEngine.Object instance,
    float t,
    string source,
    bool asActorOk = false,
    bool asGadgetOk = false)
  {
    if (!(instance != null))
      return;
    Destroy(instance, t, new Metadata()
    {
      objectName = instance.name,
      source = source,
      frame = Time.frameCount
    });
  }

  private static void Destroy(UnityEngine.Object instance, float t, Metadata metadata)
  {
    if (instance is GameObject)
      DOTween.Kill(((GameObject) instance).transform);
    List<Action<Metadata>> actionList;
    if (monitorsDict.TryGetValue(instance.GetInstanceID(), out actionList))
    {
      for (int index = 0; index < actionList.Count; ++index)
        actionList[index](metadata);
      monitorsDict.Remove(instance.GetInstanceID());
    }
    UnityEngine.Object.Destroy(instance, t);
  }

  public class Metadata
  {
    public string objectName;
    public string source;
    public int frame;

    public override string ToString() => string.Format("{0} [object={1}, source={2}, frame={3}]", typeof (Metadata), objectName, source, frame);
  }
}
