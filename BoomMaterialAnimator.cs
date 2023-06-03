// Decompiled with JetBrains decompiler
// Type: BoomMaterialAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class BoomMaterialAnimator : SRBehaviour
{
  private Material[] boomMaterials;
  private BoomMaterialInformer boomSlime;
  private float lastReadiness = float.PositiveInfinity;
  private float lastRecoveriness = float.PositiveInfinity;
  private const string CRACK_AMOUNT_PROP = "_CrackAmount";
  private const string CHAR_PROP = "_Char";

  public void Awake()
  {
    List<Material> materialList = new List<Material>();
    foreach (Renderer componentsInChild in GetComponentsInChildren<Renderer>())
    {
      if (componentsInChild.sharedMaterial.HasProperty("_CrackAmount"))
        materialList.Add(componentsInChild.material);
    }
    boomMaterials = materialList.ToArray();
    boomSlime = GetComponent<BoomMaterialInformer>();
    Update();
  }

  public void Update()
  {
    float readiness = boomSlime.GetReadiness();
    float recoveriness = boomSlime.GetRecoveriness();
    if (Mathf.Abs(recoveriness - lastRecoveriness) < 0.05000000074505806 && Mathf.Abs(readiness - lastReadiness) < 0.05000000074505806)
      return;
    float num = recoveriness > 0.40000000596046448 ? 1f : recoveriness * 2.5f;
    foreach (Material boomMaterial in boomMaterials)
    {
      boomMaterial.SetFloat("_CrackAmount", (1f - num) * Mathf.Lerp(0.1f, 1f, readiness));
      boomMaterial.SetFloat("_Char", num);
    }
    lastRecoveriness = recoveriness;
    lastReadiness = readiness;
  }

  public void OnDestroy()
  {
    foreach (Object boomMaterial in boomMaterials)
      Destroyer.Destroy(boomMaterial, "BoomMaterialAnimator.OnDestroy");
  }

  public interface BoomMaterialInformer
  {
    float GetReadiness();

    float GetRecoveriness();
  }
}
