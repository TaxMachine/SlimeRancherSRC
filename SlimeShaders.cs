// Decompiled with JetBrains decompiler
// Type: SlimeShaders
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SlimeShaders : SRBehaviour
{
  public Material cloakMaterial;
  [Tooltip("The default whitelist of materials that can be cloaked.")]
  public Material[] defaultCloakableMaterials;
  public HashSet<Shader> cloakableShaders = new HashSet<Shader>();
  private int AlphaPropertyId;
  private int unscaledTimePropertyId;

  public void Awake()
  {
    unscaledTimePropertyId = Shader.PropertyToID("UnscaledTime");
    foreach (Material cloakableMaterial in defaultCloakableMaterials)
      cloakableShaders.Add(cloakableMaterial.shader);
  }

  public void Update() => Shader.SetGlobalFloat(unscaledTimePropertyId, Time.unscaledTime);

  public void RegisterAdditionalMaterials(Material[] materials)
  {
    foreach (Material material in materials)
      cloakableShaders.Add(material.shader);
  }
}
