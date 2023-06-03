// Decompiled with JetBrains decompiler
// Type: SlimeVarietyModules
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SlimeVarietyModules : SRBehaviour
{
  public GameObject baseModule;
  public GameObject[] slimeModules;
  private List<Component> addedComponents = new List<Component>();

  public void Assemble()
  {
    if (addedComponents.Count > 0)
      Log.Error("Why are we assembling an already assembled slime? Skipping: " + gameObject.name);
    else
      MergeGeneralComponents();
  }

  private void MergeGeneralComponents()
  {
    foreach (GameObject slimeModule in slimeModules)
    {
      if (slimeModule != null)
      {
        foreach (Component component in slimeModule.GetComponents<Component>())
        {
          if (component is Collider || GetComponent(component.GetType()) == null)
            addedComponents.Add(gameObject.AddComponent(component.GetType()).GetCopyOf(component));
        }
        int childCount = slimeModule.transform.childCount;
        for (int index = 0; index < childCount; ++index)
        {
          GameObject gameObject = Instantiate(slimeModule.transform.GetChild(index).gameObject);
          Vector3 localPosition = gameObject.transform.localPosition;
          Quaternion localRotation = gameObject.transform.localRotation;
          gameObject.transform.parent = transform;
          gameObject.transform.localPosition = localPosition;
          gameObject.transform.localRotation = localRotation;
        }
      }
    }
    if (baseModule != null)
    {
      bool flag = GetComponent<RejectBaseNontriggerColliders>() != null;
      foreach (Component component in baseModule.GetComponents<Component>())
      {
        if (component is Collider && (((Collider) component).isTrigger || !flag) || GetComponent(component.GetType()) == null)
          addedComponents.Add(gameObject.AddComponent(component.GetType()).GetCopyOf(component));
      }
      int childCount = baseModule.transform.childCount;
      for (int index = 0; index < childCount; ++index)
      {
        GameObject gameObject = Instantiate(baseModule.transform.GetChild(index).gameObject);
        Vector3 localPosition = gameObject.transform.localPosition;
        Quaternion localRotation = gameObject.transform.localRotation;
        gameObject.transform.parent = transform;
        gameObject.transform.localPosition = localPosition;
        gameObject.transform.localRotation = localRotation;
      }
    }
    GetComponent<SlimeSubbehaviourPlexer>().CollectSubbehaviours();
  }
}
