// Decompiled with JetBrains decompiler
// Type: RanchDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RanchDirector : MonoBehaviour, RanchModel.Participant
{
  public Material[] ranchMats;
  public Material[] houseMats;
  public Material[] vacMats;
  private const float PARTNER_UNLOCK_TIME = 259200f;
  public PaletteEntry[] palettes;
  private Dictionary<Palette, PaletteEntry> paletteDict = new Dictionary<Palette, PaletteEntry>();
  private Dictionary<Material, Material> standardMatDict = new Dictionary<Material, Material>();
  private Dictionary<Material, Material> ogdenMatDict = new Dictionary<Material, Material>();
  private Dictionary<Material, Material> mochiMatDict = new Dictionary<Material, Material>();
  private Dictionary<Material, Material> valleyMatDict = new Dictionary<Material, Material>();
  private Dictionary<Material, Material> viktorMatDict = new Dictionary<Material, Material>();
  private List<PaletteEntry> orderedPalettes;
  private Dictionary<PaletteType, Material[]> recolorMats = new Dictionary<PaletteType, Material[]>();
  private List<Material> dynamicVacRecolorMats = new List<Material>();
  private ProgressDirector progressDir;
  private RanchModel model;
  public static string PARTNER_MAIL_KEY = "partner_rewards";

  public void Awake()
  {
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    paletteDict = palettes.ToDictionary(e => e.palette, e => e);
    recolorMats[PaletteType.VALLEY] = new Material[ranchMats.Length + houseMats.Length];
    recolorMats[PaletteType.RANCH_TECH] = new Material[ranchMats.Length];
    recolorMats[PaletteType.OGDEN_TECH] = new Material[ranchMats.Length];
    recolorMats[PaletteType.MOCHI_TECH] = new Material[ranchMats.Length];
    recolorMats[PaletteType.VIKTOR_TECH] = new Material[ranchMats.Length];
    for (int index = 0; index < ranchMats.Length; ++index)
    {
      Material ranchMat = ranchMats[index];
      standardMatDict[ranchMat] = recolorMats[PaletteType.RANCH_TECH][index] = new Material(ranchMat);
      ogdenMatDict[ranchMat] = recolorMats[PaletteType.OGDEN_TECH][index] = new Material(ranchMat);
      mochiMatDict[ranchMat] = recolorMats[PaletteType.MOCHI_TECH][index] = new Material(ranchMat);
      valleyMatDict[ranchMat] = recolorMats[PaletteType.VALLEY][index] = new Material(ranchMat);
      viktorMatDict[ranchMat] = recolorMats[PaletteType.VIKTOR_TECH][index] = new Material(ranchMat);
    }
    recolorMats[PaletteType.HOUSE] = new Material[houseMats.Length];
    recolorMats[PaletteType.OGDEN_HOUSE] = new Material[houseMats.Length];
    recolorMats[PaletteType.MOCHI_HOUSE] = new Material[houseMats.Length];
    recolorMats[PaletteType.VIKTOR_HOUSE] = new Material[houseMats.Length];
    for (int index = 0; index < houseMats.Length; ++index)
    {
      Material houseMat = houseMats[index];
      standardMatDict[houseMat] = recolorMats[PaletteType.HOUSE][index] = new Material(houseMat);
      ogdenMatDict[houseMat] = recolorMats[PaletteType.OGDEN_HOUSE][index] = new Material(houseMat);
      mochiMatDict[houseMat] = recolorMats[PaletteType.MOCHI_HOUSE][index] = new Material(houseMat);
      valleyMatDict[houseMat] = recolorMats[PaletteType.VALLEY][index + ranchMats.Length] = new Material(houseMat);
      viktorMatDict[houseMat] = recolorMats[PaletteType.VIKTOR_HOUSE][index] = new Material(houseMat);
    }
    recolorMats[PaletteType.VAC] = new Material[vacMats.Length];
    for (int index = 0; index < vacMats.Length; ++index)
    {
      Material vacMat = vacMats[index];
      Material material = new Material(vacMat);
      recolorMats[PaletteType.VAC][index] = material;
      standardMatDict[vacMat] = material;
    }
  }

  public void InitModel(RanchModel model) => ResetDefaults(model);

  public void SetModel(RanchModel model) => this.model = model;

  public void InitForLevel() => SRSingleton<SceneContext>.Instance.GameModel.RegisterRanch(this);

  public void OnDestroy()
  {
    foreach (UnityEngine.Object instance in standardMatDict.Values)
      Destroyer.Destroy(instance, "RanchDirector.OnDestroy(1)");
    foreach (UnityEngine.Object instance in ogdenMatDict.Values)
      Destroyer.Destroy(instance, "RanchDirector.OnDestroy(2)");
    foreach (UnityEngine.Object instance in mochiMatDict.Values)
      Destroyer.Destroy(instance, "RanchDirector.OnDestroy(3)");
    foreach (UnityEngine.Object instance in valleyMatDict.Values)
      Destroyer.Destroy(instance, "RanchDirector.OnDestroy(4)");
    foreach (UnityEngine.Object instance in viktorMatDict.Values)
      Destroyer.Destroy(instance, "RanchDirector.OnDestroy(5)");
  }

  public void RegisterPalette(PaletteEntry entry)
  {
    paletteDict[entry.palette] = entry;
    orderedPalettes = null;
  }

  public bool IsPartnerUnlocked() => progressDir.HasProgress(ProgressDirector.ProgressType.CORPORATE_PARTNER_UNLOCK);

  private void ResetDefaults(RanchModel model)
  {
    model.SelectPalette(PaletteType.HOUSE, Palette.DEFAULT);
    model.SelectPalette(PaletteType.RANCH_TECH, Palette.DEFAULT);
    model.SelectPalette(PaletteType.OGDEN_HOUSE, Palette.OGDEN);
    model.SelectPalette(PaletteType.OGDEN_TECH, Palette.OGDEN);
    model.SelectPalette(PaletteType.MOCHI_HOUSE, Palette.MOCHI);
    model.SelectPalette(PaletteType.MOCHI_TECH, Palette.MOCHI);
    model.SelectPalette(PaletteType.VAC, Palette.DEFAULT);
    model.SelectPalette(PaletteType.VALLEY, Palette.MOCHI);
    model.SelectPalette(PaletteType.VIKTOR_TECH, Palette.VIKTOR);
    model.SelectPalette(PaletteType.VIKTOR_HOUSE, Palette.VIKTOR);
  }

  public void SetColorsForPalette(PaletteType type, Palette palette)
  {
    PaletteEntry entry = paletteDict[palette];
    foreach (Material recolorMaterial in GetRecolorMaterials(type))
      SetColors(recolorMaterial, entry);
    if (type != PaletteType.VAC)
      return;
    foreach (Material dynamicVacRecolorMat in dynamicVacRecolorMats)
      SetColors(dynamicVacRecolorMat, entry);
  }

  private void SetColors(Material mat, PaletteEntry entry)
  {
    mat.SetColor("_Color00", entry.redDark);
    mat.SetColor("_Color01", entry.redLight);
    mat.SetColor("_Color10", entry.greenDark);
    mat.SetColor("_Color11", entry.greenLight);
    mat.SetColor("_Color20", entry.blueDark);
    mat.SetColor("_Color21", entry.blueLight);
    mat.SetColor("_Color30", entry.blackDark);
    mat.SetColor("_Color31", entry.blackLight);
    mat.SetColor("_Color40", entry.magentaDark);
    mat.SetColor("_Color41", entry.magentaLight);
    mat.SetColor("_Color50", entry.yellowDark);
    mat.SetColor("_Color51", entry.yellowLight);
    mat.SetColor("_Color60", entry.cyanDark);
    mat.SetColor("_Color61", entry.cyanLight);
    mat.SetColor("_Color70", entry.whiteDark);
    mat.SetColor("_Color71", entry.whiteLight);
  }

  private Material[] GetRecolorMaterials(PaletteType type) => recolorMats[type];

  private Dictionary<Material, Material> GetZoneDict(ZoneDirector.Zone zone)
  {
    switch (zone)
    {
      case ZoneDirector.Zone.WILDS:
      case ZoneDirector.Zone.OGDEN_RANCH:
        return ogdenMatDict;
      case ZoneDirector.Zone.VALLEY:
        return valleyMatDict;
      case ZoneDirector.Zone.MOCHI_RANCH:
        return mochiMatDict;
      case ZoneDirector.Zone.SLIMULATIONS:
      case ZoneDirector.Zone.VIKTOR_LAB:
        return viktorMatDict;
      default:
        return standardMatDict;
    }
  }

  public Material GetRecolorMaterial(Material mat, ZoneDirector.Zone zone) => GetZoneDict(zone).ContainsKey(mat) ? GetZoneDict(zone)[mat] : null;

  public void RegisterVacRecolorMat(Material mat)
  {
    dynamicVacRecolorMats.Add(mat);
    if (!enabled || model == null)
      return;
    SetColors(mat, paletteDict[model.selectedPalettes[PaletteType.VAC]]);
  }

  public void UnregisterVacRecolorMat(Material mat) => dynamicVacRecolorMats.Remove(mat);

  public bool IsSelectedPalette(
    Palette palette,
    PaletteType paletteType)
  {
    return model.selectedPalettes[paletteType] == palette;
  }

  public bool HasPalette(Palette palette)
  {
    PaletteEntry paletteEntry = paletteDict[palette];
    if (progressDir.GetProgress(ProgressDirector.ProgressType.CORPORATE_PARTNER) < paletteEntry.requiresPartnerLevel)
      return false;
    return paletteEntry.requiresProgressCount <= 0 || progressDir.GetProgress(paletteEntry.requiresProgressType) >= paletteEntry.requiresProgressCount;
  }

  public List<PaletteEntry> GetOrderedPalettes()
  {
    if (orderedPalettes == null)
    {
      orderedPalettes = paletteDict.Values.ToList();
      orderedPalettes.Sort(Comparer.DEFAULT);
    }
    return orderedPalettes;
  }

  public void NoteSelected(PaletteType type, Palette palette) => SetColorsForPalette(type, palette);

  public enum PaletteType
  {
    RANCH_TECH,
    HOUSE,
    VAC,
    OGDEN_TECH,
    OGDEN_HOUSE,
    MOCHI_TECH,
    MOCHI_HOUSE,
    VALLEY,
    VIKTOR_TECH,
    VIKTOR_HOUSE,
  }

  public enum Palette
  {
    DEFAULT = 0,
    PALETTE01 = 1,
    PALETTE02 = 2,
    PALETTE03 = 3,
    PALETTE04 = 4,
    PALETTE05 = 5,
    PALETTE06 = 6,
    PALETTE07 = 7,
    PALETTE08 = 8,
    PALETTE09 = 9,
    PALETTE10 = 10, // 0x0000000A
    PALETTE11 = 11, // 0x0000000B
    PALETTE12 = 12, // 0x0000000C
    PALETTE13 = 13, // 0x0000000D
    PALETTE14 = 14, // 0x0000000E
    PALETTE15 = 15, // 0x0000000F
    PALETTE16 = 16, // 0x00000010
    PALETTE17 = 17, // 0x00000011
    PALETTE18 = 18, // 0x00000012
    PALETTE19 = 19, // 0x00000013
    PALETTE20 = 20, // 0x00000014
    PALETTE21 = 21, // 0x00000015
    PALETTE22 = 22, // 0x00000016
    PALETTE23 = 23, // 0x00000017
    PALETTE24 = 24, // 0x00000018
    PALETTE25 = 25, // 0x00000019
    PALETTE26 = 26, // 0x0000001A
    PALETTE27 = 27, // 0x0000001B
    PALETTE28 = 28, // 0x0000001C
    PALETTE29 = 29, // 0x0000001D
    PALETTE30 = 30, // 0x0000001E
    OGDEN = 1000, // 0x000003E8
    MOCHI = 1001, // 0x000003E9
    VIKTOR = 1002, // 0x000003EA
  }

  [Serializable]
  public class PaletteEntry
  {
    public Palette palette;
    public Sprite icon;
    public int order;
    public int requiresPartnerLevel;
    public ProgressDirector.ProgressType requiresProgressType = ProgressDirector.ProgressType.NONE;
    public int requiresProgressCount;
    public Color redDark;
    public Color redLight;
    public Color greenDark;
    public Color greenLight;
    public Color blueDark;
    public Color blueLight;
    public Color blackDark;
    public Color blackLight;
    public Color magentaDark;
    public Color magentaLight;
    public Color yellowDark;
    public Color yellowLight;
    public Color cyanDark;
    public Color cyanLight;
    public Color whiteDark;
    public Color whiteLight;
  }

  private class Comparer : SRComparer<PaletteEntry>
  {
    public static Comparer<PaletteEntry> DEFAULT = new Comparer().OrderByDescending(p => p.palette == Palette.DEFAULT).OrderByDescending(p => p.requiresPartnerLevel > 0).OrderBy(p => p.requiresPartnerLevel).OrderBy(p => p.order);
  }
}
