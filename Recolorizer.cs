// Decompiled with JetBrains decompiler
// Type: Recolorizer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Recolorizer : MonoBehaviour
{
  public void Start()
  {
    RanchDirector ranchDirector = SRSingleton<SceneContext>.Instance.RanchDirector;
    ZoneDirector componentInParent = GetComponentInParent<ZoneDirector>();
    ZoneDirector.Zone zone = componentInParent == null ? ZoneDirector.Zone.RANCH : componentInParent.zone;
    foreach (Renderer componentsInChild in gameObject.GetComponentsInChildren<Renderer>(true))
    {
      Material[] materialArray = new Material[componentsInChild.sharedMaterials.Length];
      for (int index = 0; index < componentsInChild.sharedMaterials.Length; ++index)
      {
        Material sharedMaterial = componentsInChild.sharedMaterials[index];
        if (sharedMaterial == null)
        {
          materialArray[index] = sharedMaterial;
        }
        else
        {
          Material recolorMaterial = ranchDirector.GetRecolorMaterial(sharedMaterial, zone);
          materialArray[index] = !(recolorMaterial != null) ? sharedMaterial : recolorMaterial;
        }
      }
      componentsInChild.sharedMaterials = materialArray;
    }
  }
}
