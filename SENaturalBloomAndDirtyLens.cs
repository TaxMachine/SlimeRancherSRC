// Decompiled with JetBrains decompiler
// Type: SENaturalBloomAndDirtyLens
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Sonic Ether/SE Natural Bloom and Dirty Lens")]
public class SENaturalBloomAndDirtyLens : MonoBehaviour
{
  [Range(0.0f, 0.4f)]
  public float bloomIntensity = 0.05f;
  public Shader shader;
  private Material material;
  public Texture2D lensDirtTexture;
  [Range(0.0f, 0.95f)]
  public float lensDirtIntensity = 0.05f;
  private bool isSupported;
  private float blurSize = 4f;
  public bool inputIsHDR;

  private void Start()
  {
    isSupported = true;
    if (!(bool) (Object) material)
      material = new Material(shader);
    if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
      return;
    isSupported = false;
  }

  private void OnDisable()
  {
    if (!(bool) (Object) material)
      return;
    DestroyImmediate(material);
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!isSupported)
    {
      Graphics.Blit(source, destination);
    }
    else
    {
      if (!(bool) (Object) material)
        material = new Material(shader);
      material.hideFlags = HideFlags.HideAndDontSave;
      material.SetFloat("_BloomIntensity", Mathf.Exp(bloomIntensity) - 1f);
      material.SetFloat("_LensDirtIntensity", Mathf.Exp(lensDirtIntensity) - 1f);
      source.filterMode = FilterMode.Bilinear;
      int width = source.width / 2;
      int height = source.height / 2;
      RenderTexture source1 = source;
      int num1 = 2;
      for (int index1 = 0; index1 < 6; ++index1)
      {
        RenderTexture renderTexture1 = RenderTexture.GetTemporary(width, height, 0, source.format);
        renderTexture1.filterMode = FilterMode.Bilinear;
        Graphics.Blit(source1, renderTexture1, material, 1);
        source1 = renderTexture1;
        float num2 = index1 <= 1 ? 0.5f : 1f;
        if (index1 == 2)
          num2 = 0.75f;
        for (int index2 = 0; index2 < num1; ++index2)
        {
          material.SetFloat("_BlurSize", (blurSize * 0.5f + index2) * num2);
          RenderTexture temporary1 = RenderTexture.GetTemporary(width, height, 0, source.format);
          temporary1.filterMode = FilterMode.Bilinear;
          Graphics.Blit(renderTexture1, temporary1, material, 2);
          RenderTexture.ReleaseTemporary(renderTexture1);
          RenderTexture renderTexture2 = temporary1;
          RenderTexture temporary2 = RenderTexture.GetTemporary(width, height, 0, source.format);
          temporary2.filterMode = FilterMode.Bilinear;
          Graphics.Blit(renderTexture2, temporary2, material, 3);
          RenderTexture.ReleaseTemporary(renderTexture2);
          renderTexture1 = temporary2;
        }
        switch (index1)
        {
          case 0:
            material.SetTexture("_Bloom0", renderTexture1);
            break;
          case 1:
            material.SetTexture("_Bloom1", renderTexture1);
            break;
          case 2:
            material.SetTexture("_Bloom2", renderTexture1);
            break;
          case 3:
            material.SetTexture("_Bloom3", renderTexture1);
            break;
          case 4:
            material.SetTexture("_Bloom4", renderTexture1);
            break;
          case 5:
            material.SetTexture("_Bloom5", renderTexture1);
            break;
        }
        RenderTexture.ReleaseTemporary(renderTexture1);
        width /= 2;
        height /= 2;
      }
      material.SetTexture("_LensDirt", lensDirtTexture);
      Graphics.Blit(source, destination, material, 0);
    }
  }
}
