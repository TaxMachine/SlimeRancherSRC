// Decompiled with JetBrains decompiler
// Type: DynamicObjectContainer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectContainer : SRSingleton<DynamicObjectContainer>
{
  public override void Awake()
  {
    base.Awake();
    Destroyer.Monitor(gameObject, metadata =>
    {
      InvalidOperationException operationException = new InvalidOperationException(string.Format("DynamicObjectContainer is being destroyed. [metadata={0}]", metadata));
      Log.Error(operationException.ToString());
      SentrySdk.CaptureMessage("DynamicObjectContainer is being destroyed!");
      throw operationException;
    });
  }

  private List<GameObject> GetChildren()
  {
    List<GameObject> children = new List<GameObject>();
    foreach (Identifiable componentsInChild in GetComponentsInChildren<Identifiable>())
      children.Add(componentsInChild.gameObject);
    return children;
  }

  public void RegisterDynamicObjectActors()
  {
    List<GameObject> children = GetChildren();
    foreach (GameObject actorObj in children)
      SRSingleton<SceneContext>.Instance.GameModel.RegisterStartingActor(actorObj, RegionRegistry.RegionSetId.HOME);
    foreach (GameObject gameObject in children)
      gameObject.transform.SetParent(null);
  }

  public void DestroyDynamicObjectActors()
  {
    foreach (UnityEngine.Object child in GetChildren())
      Destroyer.Destroy(child, 0.0f, "DynamicObjectContainer.Awake", true);
  }
}
