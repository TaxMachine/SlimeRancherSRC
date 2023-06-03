// Decompiled with JetBrains decompiler
// Type: Colorizer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class Colorizer : MonoBehaviour
{
  public Color TintColor;
  public bool UseInstanceWhenNotEditorMode = true;
  private Color oldColor;

  private void Start()
  {
  }

  private void Update()
  {
    if (oldColor != TintColor)
      ChangeColor(gameObject, TintColor);
    oldColor = TintColor;
  }

  private void ChangeColor(GameObject effect, Color color)
  {
    foreach (Renderer componentsInChild in effect.GetComponentsInChildren<Renderer>())
    {
      Material material = UseInstanceWhenNotEditorMode ? componentsInChild.material : componentsInChild.sharedMaterial;
      if (!(material == null) && material.HasProperty("_TintColor"))
      {
        Color color1 = material.GetColor("_TintColor");
        color.a = color1.a;
        material.SetColor("_TintColor", color);
      }
    }
    Light componentInChildren = effect.GetComponentInChildren<Light>();
    if (!(componentInChildren != null))
      return;
    componentInChildren.color = color;
  }
}
