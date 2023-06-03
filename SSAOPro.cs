// Decompiled with JetBrains decompiler
// Type: SSAOPro
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ImageEffectAllowedInSceneView]
[HelpURL("http://www.thomashourdel.com/ssaopro/doc/")]
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/SSAO Pro")]
[RequireComponent(typeof (Camera))]
public class SSAOPro : MonoBehaviour
{
  public Texture2D NoiseTexture;
  public bool UseHighPrecisionDepthMap;
  public SampleCount Samples = SampleCount.Medium;
  [Range(1f, 4f)]
  public int Downsampling = 1;
  [Range(0.01f, 1.25f)]
  public float Radius = 0.12f;
  [Range(0.0f, 16f)]
  public float Intensity = 2.5f;
  [Range(0.0f, 10f)]
  public float Distance = 1f;
  [Range(0.0f, 1f)]
  public float Bias = 0.1f;
  [Range(0.0f, 1f)]
  public float LumContribution = 0.5f;
  [ColorUsage(false)]
  public Color OcclusionColor = Color.black;
  public float CutoffDistance = 150f;
  public float CutoffFalloff = 50f;
  public BlurMode Blur = BlurMode.HighQualityBilateral;
  public bool BlurDownsampling;
  [Range(1f, 4f)]
  public int BlurPasses = 1;
  [Range(1f, 20f)]
  public float BlurBilateralThreshold = 10f;
  public bool DebugAO;
  protected Shader m_ShaderSSAO;
  protected Material m_Material;
  protected Camera m_Camera;

  public Material Material
  {
    get
    {
      if (m_Material == null)
      {
        Material material = new Material(ShaderSSAO);
        material.hideFlags = HideFlags.HideAndDontSave;
        m_Material = material;
      }
      return m_Material;
    }
  }

  public Shader ShaderSSAO
  {
    get
    {
      if (m_ShaderSSAO == null)
        m_ShaderSSAO = Shader.Find("Hidden/SSAO Pro V2");
      return m_ShaderSSAO;
    }
  }

  private void OnEnable()
  {
    m_Camera = GetComponent<Camera>();
    if (ShaderSSAO == null)
    {
      Debug.LogWarning("Missing shader (SSAO).");
      enabled = false;
    }
    else if (!ShaderSSAO.isSupported)
    {
      Debug.LogWarning("Unsupported shader (SSAO).");
      enabled = false;
    }
    else
    {
      if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
        return;
      Debug.LogWarning("Depth textures aren't supported on this device.");
      enabled = false;
    }
  }

  private void OnPreRender() => m_Camera.depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.DepthNormals;

  private void OnDisable()
  {
    if (m_Material != null)
      DestroyImmediate(m_Material);
    m_Material = null;
  }

  [ImageEffectOpaque]
  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (ShaderSSAO == null || Mathf.Approximately(Intensity, 0.0f))
    {
      Graphics.Blit(source, destination);
    }
    else
    {
      Material.shaderKeywords = null;
      switch (Samples)
      {
        case SampleCount.Low:
          Material.EnableKeyword("SAMPLES_LOW");
          break;
        case SampleCount.Medium:
          Material.EnableKeyword("SAMPLES_MEDIUM");
          break;
        case SampleCount.High:
          Material.EnableKeyword("SAMPLES_HIGH");
          break;
        case SampleCount.Ultra:
          Material.EnableKeyword("SAMPLES_ULTRA");
          break;
      }
      int num1 = 0;
      if (NoiseTexture != null)
        num1 = 1;
      if (!Mathf.Approximately(LumContribution, 0.0f))
        num1 += 2;
      int pass1 = num1 + 1;
      Material.SetMatrix("_InverseViewProject", (m_Camera.projectionMatrix * m_Camera.worldToCameraMatrix).inverse);
      Material.SetMatrix("_CameraModelView", m_Camera.cameraToWorldMatrix);
      Material.SetTexture("_NoiseTex", NoiseTexture);
      Material.SetVector("_Params1", new Vector4(NoiseTexture == null ? 0.0f : NoiseTexture.width, Radius, Intensity, Distance));
      Material.SetVector("_Params2", new Vector4(Bias, LumContribution, CutoffDistance, CutoffFalloff));
      Material.SetColor("_OcclusionColor", OcclusionColor);
      if (Blur == BlurMode.None)
      {
        RenderTexture temporary = RenderTexture.GetTemporary(source.width / Downsampling, source.height / Downsampling, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(temporary, temporary, Material, 0);
        if (DebugAO)
        {
          Graphics.Blit(source, temporary, Material, pass1);
          Graphics.Blit(temporary, destination);
          RenderTexture.ReleaseTemporary(temporary);
        }
        else
        {
          Graphics.Blit(source, temporary, Material, pass1);
          Material.SetTexture("_SSAOTex", temporary);
          Graphics.Blit(source, destination, Material, 7);
          RenderTexture.ReleaseTemporary(temporary);
        }
      }
      else
      {
        Pass pass2 = Blur == BlurMode.HighQualityBilateral ? Pass.HighQualityBilateralBlur : Pass.GaussianBlur;
        int num2 = BlurDownsampling ? Downsampling : 1;
        RenderTexture temporary1 = RenderTexture.GetTemporary(source.width / num2, source.height / num2, 0, RenderTextureFormat.ARGB32);
        RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / Downsampling, source.height / Downsampling, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(temporary1, temporary1, Material, 0);
        Graphics.Blit(source, temporary1, Material, pass1);
        Material.SetFloat("_BilateralThreshold", BlurBilateralThreshold * 5f);
        for (int index = 0; index < BlurPasses; ++index)
        {
          Material.SetVector("_Direction", new Vector2(1f / source.width, 0.0f));
          Graphics.Blit(temporary1, temporary2, Material, (int) pass2);
          temporary1.DiscardContents();
          Material.SetVector("_Direction", new Vector2(0.0f, 1f / source.height));
          Graphics.Blit(temporary2, temporary1, Material, (int) pass2);
          temporary2.DiscardContents();
        }
        if (!DebugAO)
        {
          Material.SetTexture("_SSAOTex", temporary1);
          Graphics.Blit(source, destination, Material, 7);
        }
        else
          Graphics.Blit(temporary1, destination);
        RenderTexture.ReleaseTemporary(temporary1);
        RenderTexture.ReleaseTemporary(temporary2);
      }
    }
  }

  public enum BlurMode
  {
    None,
    Gaussian,
    HighQualityBilateral,
  }

  public enum SampleCount
  {
    VeryLow,
    Low,
    Medium,
    High,
    Ultra,
  }

  protected enum Pass
  {
    Clear = 0,
    GaussianBlur = 5,
    HighQualityBilateralBlur = 6,
    Composite = 7,
  }
}
