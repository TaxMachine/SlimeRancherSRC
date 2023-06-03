// Decompiled with JetBrains decompiler
// Type: GadgetDefinition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gadget/Gadget Definition")]
public class GadgetDefinition : ScriptableObject
{
  public Gadget.Id id;
  public GameObject prefab;
  public Sprite icon;
  public PediaDirector.Id pediaLink;
  public int blueprintCost;
  public CraftCost[] craftCosts;
  [Tooltip("Limits at buy time the number we can ever have.")]
  public int buyCountLimit;
  [Tooltip("Limits at place time the number that can exist in the world.")]
  public int countLimit;
  [Tooltip("Include these other IDs in counting at placement time.")]
  public Gadget.Id[] countOtherIds;
  [Tooltip("Destroy the gadget instead of picking it up.")]
  public bool destroyOnRemoval;
  [Tooltip("Do we have to buy these two at a time?")]
  public bool buyInPairs;

  public List<Gadget.Id> GetGadgetsToCountIds() => new List<Gadget.Id>(countOtherIds)
  {
    id
  };

  [Serializable]
  public class CraftCost
  {
    public Identifiable.Id id;
    public int amount;
  }
}
