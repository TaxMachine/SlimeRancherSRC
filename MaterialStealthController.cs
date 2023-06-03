// Decompiled with JetBrains decompiler
// Type: MaterialStealthController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialStealthController
{
  private const float CLOAK_THRESHOLD = 0.99f;
  private static readonly int alphaPropertyId = Shader.PropertyToID("_Alpha");
  private static readonly int topColorPropertyId = Shader.PropertyToID("_TopColor");
  private static readonly int middleColorPropertyId = Shader.PropertyToID("_MiddleColor");
  private static readonly int bottomColorPropertyId = Shader.PropertyToID("_BottomColor");
  private readonly Material cloakMaterial;
  private readonly HashSet<Material> cloakingMats = new HashSet<Material>();
  private readonly List<Renderer> renderers = new List<Renderer>();
  private readonly Dictionary<Renderer, Material[]> rendererOriginalMaterials = new Dictionary<Renderer, Material[]>();
  private SlimeShaders slimeShaders;
  private readonly MaterialPropertyBlock colorsPropertyBlock = new MaterialPropertyBlock();

  public MaterialStealthController(GameObject gameObject)
  {
    slimeShaders = SRSingleton<GameContext>.Instance.SlimeShaders;
    cloakMaterial = slimeShaders.cloakMaterial;
    UpdateMaterials(gameObject);
  }

  public void UpdateMaterials(GameObject gameObject)
  {
    cloakingMats.Clear();
    renderers.Clear();
    rendererOriginalMaterials.Clear();
    foreach (Renderer componentsInChild in gameObject.GetComponentsInChildren<Renderer>())
    {
      foreach (Material sharedMaterial in componentsInChild.sharedMaterials)
      {
        if (sharedMaterial != null && slimeShaders.cloakableShaders.Contains(sharedMaterial.shader))
        {
          cloakingMats.Add(sharedMaterial);
          if (sharedMaterial.HasProperty(topColorPropertyId))
          {
            colorsPropertyBlock.SetColor(topColorPropertyId, sharedMaterial.GetColor(topColorPropertyId));
            colorsPropertyBlock.SetColor(middleColorPropertyId, sharedMaterial.GetColor(middleColorPropertyId));
            colorsPropertyBlock.SetColor(bottomColorPropertyId, sharedMaterial.GetColor(bottomColorPropertyId));
          }
        }
      }
      renderers.Add(componentsInChild);
      rendererOriginalMaterials[componentsInChild] = componentsInChild.sharedMaterials.ToArray();
    }
    cloakingMats.Add(cloakMaterial);
  }

  public void SetOpacity(float opacity)
  {
    bool flag1 = opacity >= 0.99000000953674316;
    bool flag2 = false;
    for (int index = 0; index < renderers.Count; ++index)
    {
      Renderer renderer = renderers[index];
      if (renderer == null)
      {
        flag2 = true;
      }
      else
      {
        Material[] sharedMaterials = renderer.sharedMaterials;
        for (int materialIndex = 0; materialIndex < sharedMaterials.Length; ++materialIndex)
        {
          Material material = sharedMaterials[materialIndex];
          if (cloakingMats.Contains(material))
          {
            if (!flag1 && material != cloakMaterial)
              sharedMaterials[materialIndex] = cloakMaterial;
            else if (flag1 && material == cloakMaterial)
              sharedMaterials[materialIndex] = rendererOriginalMaterials[renderer][materialIndex];
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(properties, materialIndex);
            properties.SetFloat(alphaPropertyId, flag1 ? 1f : opacity);
            properties.SetColor(topColorPropertyId, colorsPropertyBlock.GetColor(topColorPropertyId));
            properties.SetColor(middleColorPropertyId, colorsPropertyBlock.GetColor(middleColorPropertyId));
            properties.SetColor(bottomColorPropertyId, colorsPropertyBlock.GetColor(bottomColorPropertyId));
            renderer.SetPropertyBlock(properties, materialIndex);
          }
        }
        renderer.sharedMaterials = sharedMaterials;
      }
    }
    if (!flag2)
      return;
    renderers.RemoveAll(renderer => renderer == null);
  }
}
