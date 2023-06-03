// Decompiled with JetBrains decompiler
// Type: UIActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIActivator : MonoBehaviour
{
  public GameObject uiPrefab;
  public GameObject blockInExpoPrefab;

  public virtual bool CanActivate() => true;

  public virtual GameObject Activate()
  {
    GameObject gameObject = Instantiate(uiPrefab);
    LandPlotUI component1 = gameObject.GetComponent<LandPlotUI>();
    if (component1 != null)
      component1.SetActivator(this.gameObject.GetComponentInParent<LandPlot>());
    AccessDoorUI component2 = gameObject.GetComponent<AccessDoorUI>();
    if (component2 != null)
      component2.SetAccessDoor(this.gameObject.GetComponentInParent<AccessDoor>());
    LocationalUI component3 = gameObject.GetComponent<LocationalUI>();
    if (component3 == null)
      return gameObject;
    component3.SetPosition(transform.position);
    return gameObject;
  }
}
