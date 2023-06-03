// Decompiled with JetBrains decompiler
// Type: VolumetricSphere
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class VolumetricSphere : MonoBehaviour
{
  [Header("Parameters")]
  [Tooltip("The radius of the sphere")]
  [Range(0.0f, 50f)]
  public float radius = 3f;
  [Tooltip("The density of the sphere")]
  [Range(0.0f, 10f)]
  public float density = 1f;
  [Tooltip("The curve of the fade-out")]
  [Range(0.2f, 5f)]
  public float exponent = 0.333333343f;
  [Tooltip("The maximum pixelization size")]
  [Range(1f, 10f)]
  public int maxPixelizationLevel = 5;
  [Tooltip("Enabled the interpolation between the layers of different pixels size")]
  public bool enableLayersInterpolation = true;
  [Header("Debug")]
  [Tooltip("Outputs the sphere mask")]
  public bool debugSphere;

  private void Update()
  {
    Shader.SetGlobalVector("_SpherePosition", transform.position);
    Shader.SetGlobalFloat("_SphereRadius", radius);
    Shader.SetGlobalFloat("_MaskDensity", density);
    Shader.SetGlobalFloat("_MaskExponent", exponent);
    Shader.SetGlobalInt("_MaxPixelizationLevel", maxPixelizationLevel);
    if (enableLayersInterpolation)
      Shader.EnableKeyword("_INTERPOLATE_LAYERS_ON");
    else
      Shader.DisableKeyword("_INTERPOLATE_LAYERS_ON");
    if (debugSphere)
      Shader.EnableKeyword("_DEBUG_MASK_ON");
    else
      Shader.DisableKeyword("_DEBUG_MASK_ON");
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, radius);
  }
}
