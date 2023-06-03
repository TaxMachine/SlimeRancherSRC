// Decompiled with JetBrains decompiler
// Type: SlimeAppearance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Slimes/Slime Appearance")]
public class SlimeAppearance : ScriptableObject
{
  public string NameXlateKey;
  public Sprite Icon;
  public RuntimeAnimatorController AnimatorOverride;
  public SlimeAppearanceStructure[] Structures;
  public SlimeFace Face;
  public SlimeAppearance QubitAppearance;
  public SlimeAppearance ShockedAppearance;
  public SlimeAppearance[] DependentAppearances;
  public Palette ColorPalette;
  public GlintAppearance GlintAppearance;
  public TornadoAppearance TornadoAppearance;
  public VineAppearance VineAppearance;
  public CrystalAppearance CrystalAppearance;
  public ExplosionAppearance ExplosionAppearance;
  public DeathAppearance DeathAppearance;
  public AppearanceSaveSet SaveSet;
  public static SlimeAppearanceEqualityComparer DefaultComparer = new SlimeAppearanceEqualityComparer();
  public static BoneComparer DefaultBoneComparer = new BoneComparer();

  public void MaybeShowPopupUI()
  {
    if (SaveSet != AppearanceSaveSet.SECRET_STYLE)
      return;
    PopupDirector popupDirector = SRSingleton<SceneContext>.Instance.PopupDirector;
    popupDirector.QueueForPopup(new SlimeAppearancePopupUI.PopupCreator(this));
    popupDirector.MaybePopupNext();
  }

  public static SlimeAppearance CombineAppearances(
    SlimeAppearance appearance1,
    SlimeAppearance appearance2)
  {
    SlimeAppearance instance = CreateInstance<SlimeAppearance>();
    instance.Face = appearance1.Face;
    HashSet<SlimeAppearanceStructure> source = new HashSet<SlimeAppearanceStructure>(new SlimeAppearanceStructureComparer());
    foreach (SlimeAppearanceStructure structure in appearance1.Structures)
      source.Add(structure);
    foreach (SlimeAppearanceStructure structure in appearance2.Structures)
    {
      if (!source.Contains(structure))
        source.Add(structure);
    }
    instance.Structures = source.Select(s => new SlimeAppearanceStructure(s)).ToArray();
    instance.DependentAppearances = new SlimeAppearance[2]
    {
      appearance1,
      appearance2
    };
    instance.GlintAppearance = appearance1.GlintAppearance ?? appearance2.GlintAppearance;
    instance.TornadoAppearance = appearance1.TornadoAppearance ?? appearance2.TornadoAppearance;
    instance.VineAppearance = appearance1.VineAppearance ?? appearance2.VineAppearance;
    instance.CrystalAppearance = appearance1.CrystalAppearance ?? appearance2.CrystalAppearance;
    instance.ExplosionAppearance = appearance1.ExplosionAppearance ?? appearance2.ExplosionAppearance;
    return instance;
  }

  public enum SlimeBone
  {
    None,
    Root,
    Attachment,
    Slime,
    Core,
    JiggleBack,
    JiggleBottom,
    JiggleFront,
    JiggleLeft,
    JiggleRight,
    JiggleTop,
    Spinner,
    LeftWing,
    RightWing,
  }

  public enum AppearanceSaveSet
  {
    NONE,
    CLASSIC,
    SECRET_STYLE,
  }

  public class BoneComparer : IEqualityComparer<SlimeBone>
  {
    public bool Equals(SlimeBone bone1, SlimeBone bone2) => bone1 == bone2;

    public int GetHashCode(SlimeBone bone) => (int) bone;
  }

  private class SlimeAppearanceStructureComparer : IEqualityComparer<SlimeAppearanceStructure>
  {
    public bool Equals(SlimeAppearanceStructure x, SlimeAppearanceStructure y) => x.Element.GetHashCode() == y.Element.GetHashCode();

    public int GetHashCode(SlimeAppearanceStructure obj) => obj.Element.GetHashCode();
  }

  [Serializable]
  public struct Palette
  {
    private static int TopColorPropertyId = Shader.PropertyToID("_TopColor");
    private static int MiddleColorPropertyId = Shader.PropertyToID("_MiddleColor");
    private static int BottomColorPropertyId = Shader.PropertyToID("_BottomColor");
    public static Palette Default = new Palette()
    {
      Top = Color.grey,
      Middle = Color.grey,
      Bottom = Color.grey,
      Ammo = Color.grey
    };
    public Color Top;
    public Color Middle;
    public Color Bottom;
    public Color Ammo;

    public static Palette FromMaterial(Material material) => new Palette()
    {
      Top = material.GetColor(TopColorPropertyId),
      Middle = material.GetColor(MiddleColorPropertyId),
      Bottom = material.GetColor(BottomColorPropertyId),
      Ammo = Color.black
    };
  }
}
