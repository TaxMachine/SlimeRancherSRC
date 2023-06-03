// Decompiled with JetBrains decompiler
// Type: SlimeAppearanceStructure
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class SlimeAppearanceStructure
{
  public Material[] DefaultMaterials;
  public SlimeAppearanceElement Element;
  public SlimeAppearanceMaterials[] ElementMaterials;
  public bool SupportsFaces;
  public SlimeFaceRules[] FaceRules;

  public SlimeAppearanceStructure(SlimeAppearanceStructure slimeAppearanceStructure)
  {
    DefaultMaterials = new Material[slimeAppearanceStructure.DefaultMaterials.Length];
    Array.Copy(slimeAppearanceStructure.DefaultMaterials, DefaultMaterials, DefaultMaterials.Length);
    Element = slimeAppearanceStructure.Element;
    ElementMaterials = new SlimeAppearanceMaterials[slimeAppearanceStructure.ElementMaterials.Length];
    Array.Copy(slimeAppearanceStructure.ElementMaterials, ElementMaterials, ElementMaterials.Length);
    SupportsFaces = slimeAppearanceStructure.SupportsFaces;
    FaceRules = new SlimeFaceRules[slimeAppearanceStructure.FaceRules.Length];
    Array.Copy(slimeAppearanceStructure.FaceRules, FaceRules, FaceRules.Length);
  }

  public bool ElementMaterialCountIsValid() => !(Element != null) || ElementMaterials.Length == Element.Prefabs.Length;
}
