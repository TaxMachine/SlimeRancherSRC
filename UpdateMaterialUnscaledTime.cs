// Decompiled with JetBrains decompiler
// Type: UpdateMaterialUnscaledTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UpdateMaterialUnscaledTime : MonoBehaviour
{
  public Material[] mats;
  private int unscaledTimeVarId;
  private List<Material> adjustedMats = new List<Material>();

  public void Awake()
  {
    unscaledTimeVarId = Shader.PropertyToID("_UnscaledTime");
    foreach (Renderer componentsInChild in GetComponentsInChildren<Renderer>())
    {
      Material[] sharedMaterials = componentsInChild.sharedMaterials;
      Material[] materialArray = new Material[sharedMaterials.Length];
      for (int index1 = 0; index1 < sharedMaterials.Length; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < mats.Length; ++index2)
        {
          if (sharedMaterials[index1] == mats[index2])
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          Material material = new Material(sharedMaterials[index1]);
          adjustedMats.Add(material);
          materialArray[index1] = material;
        }
        else
          materialArray[index1] = sharedMaterials[index1];
      }
      componentsInChild.materials = materialArray;
    }
  }

  public void Update()
  {
    foreach (Material adjustedMat in adjustedMats)
      adjustedMat.SetFloat(unscaledTimeVarId, Time.unscaledTime);
  }

  public void OnDestroy()
  {
    foreach (Object adjustedMat in adjustedMats)
      Destroyer.Destroy(adjustedMat, "UpdateMaterialUnscaledTime.OnDestroy");
    adjustedMats.Clear();
  }
}
