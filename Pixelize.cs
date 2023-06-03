// Decompiled with JetBrains decompiler
// Type: Pixelize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Camera))]
[ImageEffectAllowedInSceneView]
[ExecuteInEditMode]
public class Pixelize : MonoBehaviour
{
  private Shader _screenAndMaskShader;
  private Material _screenAndMaskMaterial;
  private RenderTexture _temporaryRenderTexture;
  private Shader _combineLayersShader;
  private Material _combineLayersMaterial;

  private Shader ScreenAndMaskShader
  {
    get
    {
      if (_screenAndMaskShader == null)
        _screenAndMaskShader = Shader.Find("Hidden/PostProcess/Pixelize/ScreenAndMask");
      return _screenAndMaskShader;
    }
  }

  private Material ScreenAndMaskMaterial
  {
    get
    {
      if (_screenAndMaskMaterial == null)
        _screenAndMaskMaterial = new Material(ScreenAndMaskShader);
      return _screenAndMaskMaterial;
    }
  }

  private RenderTexture TemporaryRenderTarget
  {
    get
    {
      if (_temporaryRenderTexture == null)
        CreateTemporaryRenderTarget();
      return _temporaryRenderTexture;
    }
  }

  private Shader CombineLayersShader
  {
    get
    {
      if (_combineLayersShader == null)
        _combineLayersShader = Shader.Find("Hidden/PostProcess/Pixelize/CombineLayers");
      return _combineLayersShader;
    }
  }

  private Material CombineLayersMaterial
  {
    get
    {
      if (_combineLayersMaterial == null)
        _combineLayersMaterial = new Material(CombineLayersShader);
      return _combineLayersMaterial;
    }
  }

  private void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    CheckTemporaryRenderTarget();
    Graphics.Blit(src, TemporaryRenderTarget, ScreenAndMaskMaterial);
    Graphics.Blit(TemporaryRenderTarget, dest, CombineLayersMaterial);
  }

  private void CreateTemporaryRenderTarget()
  {
    _temporaryRenderTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
    _temporaryRenderTexture.useMipMap = true;
    _temporaryRenderTexture.autoGenerateMips = true;
    _temporaryRenderTexture.wrapMode = TextureWrapMode.Clamp;
    _temporaryRenderTexture.filterMode = FilterMode.Point;
    _temporaryRenderTexture.Create();
  }

  private void CheckTemporaryRenderTarget()
  {
    if (TemporaryRenderTarget.width == Screen.width && TemporaryRenderTarget.width == Screen.height)
      return;
    ReleaseTemporaryRenderTarget();
  }

  private void ReleaseTemporaryRenderTarget()
  {
    _temporaryRenderTexture.Release();
    _temporaryRenderTexture = null;
  }
}
