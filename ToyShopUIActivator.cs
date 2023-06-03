// Decompiled with JetBrains decompiler
// Type: ToyShopUIActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class ToyShopUIActivator : UIActivator
{
  public GameObject ejector;

  public override GameObject Activate()
  {
    GameObject gameObject = base.Activate();
    ToyShopUI component = gameObject.GetComponent<ToyShopUI>();
    component.ejectionPoint = ejector;
    component.regionSetId = GetComponentInParent<Region>().setId;
    return gameObject;
  }
}
