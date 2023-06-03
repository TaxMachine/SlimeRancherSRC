// Decompiled with JetBrains decompiler
// Type: MiracleMix
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class MiracleMix : MonoBehaviour
{
  public float ripenessModifier = -0.5f;
  private HashSet<ResourceCycle> preservedResources = new HashSet<ResourceCycle>();

  public void OnTriggerEnter(Collider other)
  {
    if (other.isTrigger)
      return;
    Identifiable component1 = other.GetComponent<Identifiable>();
    if (!(component1 != null) || !IsPreservable(component1))
      return;
    ResourceCycle component2 = other.GetComponent<ResourceCycle>();
    preservedResources.Add(component2);
    component2.AttachPreservative(this);
  }

  public bool IsPreservable(Identifiable ident) => Identifiable.VEGGIE_CLASS.Contains(ident.id) || Identifiable.FRUIT_CLASS.Contains(ident.id);

  public void OnTriggerExit(Collider other)
  {
    if (other.isTrigger)
      return;
    Identifiable component = other.GetComponent<Identifiable>();
    if (!(component != null) || !IsPreservable(component))
      return;
    RemoveResourceCycle(other.GetComponent<ResourceCycle>());
  }

  public void RemoveResourceCycle(ResourceCycle cycle)
  {
    preservedResources.Remove(cycle);
    cycle.DetachPreservative(this);
  }

  public void OnDestroy()
  {
    foreach (ResourceCycle preservedResource in preservedResources)
    {
      if (preservedResource != null)
        preservedResource.DetachPreservative(this);
    }
    preservedResources.Clear();
  }

  public float PreservativeRipenessModifier() => ripenessModifier;
}
