// Decompiled with JetBrains decompiler
// Type: RecolorSlimeMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RecolorSlimeMaterial : MonoBehaviour
{
  protected MaterialPropertyBlock propertyBlock;
  protected Renderer slimeRenderer;
  private static int topColorNameId = Shader.PropertyToID("_TopColor");
  private static int middleColorNameId = Shader.PropertyToID("_MiddleColor");
  private static int bottomColorNameId = Shader.PropertyToID("_BottomColor");

  public virtual void Awake()
  {
    slimeRenderer = GetComponent<Renderer>();
    propertyBlock = new MaterialPropertyBlock();
  }

  protected virtual Material GetMaterial()
  {
    slimeRenderer = GetComponent<Renderer>();
    return slimeRenderer != null ? slimeRenderer.material : null;
  }

  public void SetColors(Color topColor, Color midColor, Color btmColor)
  {
    slimeRenderer.GetPropertyBlock(propertyBlock);
    propertyBlock.SetColor(topColorNameId, topColor);
    propertyBlock.SetColor(middleColorNameId, midColor);
    propertyBlock.SetColor(bottomColorNameId, btmColor);
    slimeRenderer.SetPropertyBlock(propertyBlock);
  }
}
