// Decompiled with JetBrains decompiler
// Type: PediaUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PediaUI : BaseUI
{
  public RectTransform listingPanel;
  public ScrollRect listingScroller;
  public TMP_Text titleText;
  public TMP_Text introText;
  public Image image;
  public ScrollRect descScroller;
  public TMP_Text longDescText;
  public TMP_Text dietText;
  public TMP_Text favoriteText;
  public TMP_Text biologyText;
  public TMP_Text risksText;
  public TMP_Text plortText;
  public TMP_Text instructionsText;
  public TMP_Text vacDescText;
  public TMP_Text resourceTypeText;
  public TMP_Text favoredByLabel;
  public TMP_Text favoredByText;
  public TMP_Text howToUseText;
  public GameObject howToUseArea;
  public TMP_Text resourceDescText;
  public RectTransform upgradesPanel;
  public TMP_Text ranchDescText;
  public Toggle vacpackTab;
  public Toggle slimesTab;
  public Toggle resourcesTab;
  public Toggle ranchTab;
  public Toggle worldTab;
  public Toggle scienceTab;
  public TabByMenuKeys tabs;
  public RectTransform genericDescPanel;
  public RectTransform vacpackDescPanel;
  public RectTransform slimesDescPanel;
  public RectTransform resourcesDescPanel;
  public RectTransform ranchDescPanel;
  private PediaDirector pediaDir;
  private MessageBundle pediaBundle;
  private static PediaDirector.Id[] TUTORIALS_ENTRIES = new PediaDirector.Id[12]
  {
    PediaDirector.Id.BASICS,
    PediaDirector.Id.VACING,
    PediaDirector.Id.CAPTURETANKS,
    PediaDirector.Id.ENERGY,
    PediaDirector.Id.CORRALLING,
    PediaDirector.Id.FEEDING,
    PediaDirector.Id.PLORTS,
    PediaDirector.Id.SSBASICS,
    PediaDirector.Id.GADGETMODE,
    PediaDirector.Id.WILDS_TUTORIAL,
    PediaDirector.Id.VALLEY_TUTORIAL,
    PediaDirector.Id.SLIMULATIONS_TUTORIAL
  };
  private static PediaDirector.Id[] SLIMES_ENTRIES = new PediaDirector.Id[26]
  {
    PediaDirector.Id.PINK_SLIME,
    PediaDirector.Id.ROCK_SLIME,
    PediaDirector.Id.TABBY_SLIME,
    PediaDirector.Id.PHOSPHOR_SLIME,
    PediaDirector.Id.RAD_SLIME,
    PediaDirector.Id.BOOM_SLIME,
    PediaDirector.Id.HONEY_SLIME,
    PediaDirector.Id.PUDDLE_SLIME,
    PediaDirector.Id.CRYSTAL_SLIME,
    PediaDirector.Id.HUNTER_SLIME,
    PediaDirector.Id.QUANTUM_SLIME,
    PediaDirector.Id.FIRE_SLIME,
    PediaDirector.Id.DERVISH_SLIME,
    PediaDirector.Id.TANGLE_SLIME,
    PediaDirector.Id.MOSAIC_SLIME,
    PediaDirector.Id.SABER_SLIME,
    PediaDirector.Id.QUICKSILVER_SLIME,
    PediaDirector.Id.GLITCH_SLIME,
    PediaDirector.Id.GOLD_SLIME,
    PediaDirector.Id.LUCKY_SLIME,
    PediaDirector.Id.LARGO_SLIME,
    PediaDirector.Id.GORDO_SLIME,
    PediaDirector.Id.PARTY_GORDO_SLIME,
    PediaDirector.Id.ECHO_NOTE_GORDO_SLIME,
    PediaDirector.Id.FERAL_SLIME,
    PediaDirector.Id.TARR_SLIME
  };
  private static PediaDirector.Id[] RESOURCES_ENTRIES = new PediaDirector.Id[44]
  {
    PediaDirector.Id.CARROT,
    PediaDirector.Id.OCAOCA,
    PediaDirector.Id.BEET,
    PediaDirector.Id.PARSNIP,
    PediaDirector.Id.ONION,
    PediaDirector.Id.GINGER,
    PediaDirector.Id.POGO,
    PediaDirector.Id.MANGO,
    PediaDirector.Id.CUBERRY,
    PediaDirector.Id.LEMON,
    PediaDirector.Id.PEAR,
    PediaDirector.Id.KOOKADOBA,
    PediaDirector.Id.CHICKADOO,
    PediaDirector.Id.HENHEN,
    PediaDirector.Id.ROOSTRO,
    PediaDirector.Id.STONY_CHICKADOO,
    PediaDirector.Id.STONY_HEN,
    PediaDirector.Id.BRIAR_CHICKADOO,
    PediaDirector.Id.BRIAR_HEN,
    PediaDirector.Id.PAINTED_CHICKADOO,
    PediaDirector.Id.PAINTED_HEN,
    PediaDirector.Id.ELDER_HEN,
    PediaDirector.Id.ELDER_ROOSTRO,
    PediaDirector.Id.SPICY_TOFU,
    PediaDirector.Id.MANIFOLD_CUBE_CRAFT,
    PediaDirector.Id.PRIMORDY_OIL_CRAFT,
    PediaDirector.Id.DEEP_BRINE_CRAFT,
    PediaDirector.Id.SILKY_SAND_CRAFT,
    PediaDirector.Id.SPIRAL_STEAM_CRAFT,
    PediaDirector.Id.LAVA_DUST_CRAFT,
    PediaDirector.Id.BUZZ_WAX_CRAFT,
    PediaDirector.Id.WILD_HONEY_CRAFT,
    PediaDirector.Id.PEPPER_JAM_CRAFT,
    PediaDirector.Id.HEXACOMB_CRAFT,
    PediaDirector.Id.ROYAL_JELLY_CRAFT,
    PediaDirector.Id.JELLYSTONE_CRAFT,
    PediaDirector.Id.INDIGONIUM_CRAFT,
    PediaDirector.Id.GLASS_SHARD_CRAFT,
    PediaDirector.Id.SLIME_FOSSIL_CRAFT,
    PediaDirector.Id.STRANGE_DIAMOND_CRAFT,
    PediaDirector.Id.ECHOES,
    PediaDirector.Id.SLIME_TOYS,
    PediaDirector.Id.ECHO_NOTES,
    PediaDirector.Id.ORNAMENTS
  };
  private static PediaDirector.Id[] RANCH_ENTRIES = new PediaDirector.Id[16]
  {
    PediaDirector.Id.CORRAL,
    PediaDirector.Id.COOP,
    PediaDirector.Id.GARDEN,
    PediaDirector.Id.SILO,
    PediaDirector.Id.INCINERATOR,
    PediaDirector.Id.POND,
    PediaDirector.Id.PLORT_MARKET,
    PediaDirector.Id.OVERGROWTH,
    PediaDirector.Id.GROTTO,
    PediaDirector.Id.DOCKS,
    PediaDirector.Id.LAB,
    PediaDirector.Id.OGDEN_RETREAT,
    PediaDirector.Id.MOCHI_MANOR,
    PediaDirector.Id.VIKTOR_LAB,
    PediaDirector.Id.PARTNER,
    PediaDirector.Id.CHROMA
  };
  private static PediaDirector.Id[] WORLD_ENTRIES = new PediaDirector.Id[11]
  {
    PediaDirector.Id.THE_RANCH,
    PediaDirector.Id.REEF,
    PediaDirector.Id.QUARRY,
    PediaDirector.Id.MOSS,
    PediaDirector.Id.RUINS,
    PediaDirector.Id.DESERT,
    PediaDirector.Id.WILDS,
    PediaDirector.Id.VALLEY,
    PediaDirector.Id.SLIMULATIONS_WORLD,
    PediaDirector.Id.SEA,
    PediaDirector.Id.KEYS
  };
  public static PediaDirector.Id[] SCIENCE_ENTRIES = new PediaDirector.Id[9]
  {
    PediaDirector.Id.REFINERY,
    PediaDirector.Id.FABRICATOR,
    PediaDirector.Id.BLUEPRINTS,
    PediaDirector.Id.EXTRACTORS,
    PediaDirector.Id.UTILITIES,
    PediaDirector.Id.WARP_TECH,
    PediaDirector.Id.DECORATIONS,
    PediaDirector.Id.CURIOS,
    PediaDirector.Id.DRONES
  };
  private Dictionary<PediaDirector.Id, Toggle> listItems = new Dictionary<PediaDirector.Id, Toggle>();

  public override void Awake()
  {
    base.Awake();
    pediaDir = SRSingleton<SceneContext>.Instance.PediaDirector;
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(InitBundles);
    bool flag = true;
    foreach (PediaDirector.Id id in SCIENCE_ENTRIES)
    {
      if (pediaDir.IsUnlocked(id))
        flag = false;
    }
    if (flag)
      scienceTab.gameObject.SetActive(false);
    SelectEntry(TUTORIALS_ENTRIES[0], true, TUTORIALS_ENTRIES[0]);
  }

  public void InitBundles(MessageDirector msgDir) => pediaBundle = msgDir.GetBundle("pedia");

  public void SelectVacpack(GameObject toggleObj)
  {
    if (!toggleObj.GetComponent<Toggle>().isOn)
      return;
    SelectEntry(TUTORIALS_ENTRIES[0], true, TUTORIALS_ENTRIES[0]);
  }

  public void SelectSlimes(GameObject toggleObj)
  {
    if (!toggleObj.GetComponent<Toggle>().isOn)
      return;
    SelectEntry(SLIMES_ENTRIES[0], true, SLIMES_ENTRIES[0]);
  }

  public void SelectResources(GameObject toggleObj)
  {
    if (!toggleObj.GetComponent<Toggle>().isOn)
      return;
    SelectEntry(RESOURCES_ENTRIES[0], true, RESOURCES_ENTRIES[0]);
  }

  public void SelectRanch(GameObject toggleObj)
  {
    if (!toggleObj.GetComponent<Toggle>().isOn)
      return;
    SelectEntry(RANCH_ENTRIES[0], true, RANCH_ENTRIES[0]);
  }

  public void SelectWorld(GameObject toggleObj)
  {
    if (!toggleObj.GetComponent<Toggle>().isOn)
      return;
    SelectEntry(WORLD_ENTRIES[0], true, WORLD_ENTRIES[0]);
  }

  public void SelectScience(GameObject toggleObj)
  {
    if (!toggleObj.GetComponent<Toggle>().isOn)
      return;
    SelectEntry(SCIENCE_ENTRIES[0], true, SCIENCE_ENTRIES[0]);
  }

  public void SelectEntry(PediaDirector.Id id, bool selectTab, PediaDirector.Id listingId)
  {
    PediaDirector.IdEntry idEntry = pediaDir.Get(id);
    if (selectTab)
      SelectTabForId(id);
    if (idEntry == null)
      Debug.Log("Missing Pedia entry, using fallback icons and text.");
    string lowerName = idEntry == null ? "*UNKNOWN*" : Enum.GetName(typeof (PediaDirector.Id), idEntry.id).ToLowerInvariant();
    titleText.text = pediaBundle.Get("t." + lowerName);
    introText.text = pediaBundle.Get("m.intro." + lowerName);
    image.sprite = idEntry == null ? pediaDir.unknownIcon : idEntry.icon;
    genericDescPanel.gameObject.SetActive(false);
    vacpackDescPanel.gameObject.SetActive(false);
    slimesDescPanel.gameObject.SetActive(false);
    resourcesDescPanel.gameObject.SetActive(false);
    ranchDescPanel.gameObject.SetActive(false);
    if (Array.IndexOf(TUTORIALS_ENTRIES, id) != -1 && id != PediaDirector.Id.SPLASH)
      PopulateVacpackDesc(lowerName);
    else if (Array.IndexOf(SLIMES_ENTRIES, id) != -1)
      PopulateSlimesDesc(lowerName);
    else if (Array.IndexOf(RESOURCES_ENTRIES, id) != -1)
      PopulateResourcesDesc(lowerName);
    else if (Array.IndexOf(RANCH_ENTRIES, id) != -1)
      PopulateRanchDesc(lowerName);
    else
      PopulateGenericDesc(lowerName);
    if (listItems.ContainsKey(listingId))
    {
      Toggle listItem = listItems[listingId];
      if (listItem != null && EventSystem.current.currentSelectedGameObject != listItem.gameObject)
        listItem.Select();
      listItem.isOn = true;
    }
    StartCoroutine(DelayedResetScroller(descScroller));
  }

  private void PopulateVacpackDesc(string lowerName)
  {
    vacpackDescPanel.gameObject.SetActive(true);
    string str = "m.instructions.gamepad.";
    instructionsText.text = !InputDirector.UsingGamepad() || !pediaBundle.Exists(str + lowerName) ? pediaBundle.Get("m.instructions." + lowerName) : pediaBundle.Get(str + lowerName);
    vacDescText.text = pediaBundle.Get("m.desc." + lowerName);
  }

  private void PopulateSlimesDesc(string lowerName)
  {
    slimesDescPanel.gameObject.SetActive(true);
    dietText.text = pediaBundle.Get("m.diet." + lowerName);
    favoriteText.text = pediaBundle.Get("m.favorite." + lowerName);
    biologyText.text = pediaBundle.Get("m.slimeology." + lowerName);
    risksText.text = pediaBundle.Get("m.risks." + lowerName);
    plortText.text = pediaBundle.Get("m.plortonomics." + lowerName);
  }

  private void PopulateResourcesDesc(string lowerName)
  {
    resourcesDescPanel.gameObject.SetActive(true);
    resourceTypeText.text = pediaBundle.Get("m.resource_type." + lowerName);
    favoredByLabel.text = !pediaBundle.Exists("l.favored_by." + lowerName) ? uiBundle.Get("l.favored_by") : pediaBundle.Get("l.favored_by." + lowerName);
    favoredByText.text = pediaBundle.Get("m.favored_by." + lowerName);
    bool flag = pediaBundle.Exists("m.how_to_use." + lowerName);
    if (flag)
      howToUseText.text = pediaBundle.Get("m.how_to_use." + lowerName);
    howToUseArea.SetActive(flag);
    resourceDescText.text = pediaBundle.Get("m.desc." + lowerName);
  }

  private void PopulateRanchDesc(string lowerName)
  {
    ranchDescPanel.gameObject.SetActive(true);
    for (int index = upgradesPanel.transform.childCount - 1; index >= 0; --index)
      Destroyer.Destroy(upgradesPanel.GetChild(index).gameObject, "PediaUI.PopulateRanchDesc");
    foreach (LandPlot.Upgrade upgrade in Enum.GetValues(typeof (LandPlot.Upgrade)))
    {
      string lowerInvariant = Enum.GetName(typeof (LandPlot.Upgrade), upgrade).ToLowerInvariant();
      string key = "m.upgrade.name." + lowerName + "." + lowerInvariant;
      if (pediaBundle.Exists(key))
      {
        GameObject gameObject1 = new GameObject("UpgradeNameText");
        gameObject1.AddComponent<TextMeshProUGUI>().text = pediaBundle.Get(key);
        gameObject1.AddComponent<MeshTextStyler>().SetStyle("LargeBold");
        gameObject1.transform.SetParent(upgradesPanel, false);
        GameObject gameObject2 = new GameObject("UpgradeDescText");
        gameObject2.AddComponent<TextMeshProUGUI>().text = pediaBundle.Get("m.upgrade.desc." + lowerName + "." + lowerInvariant);
        gameObject2.AddComponent<MeshTextStyler>().SetStyle("Default");
        gameObject2.transform.SetParent(upgradesPanel, false);
      }
    }
    ranchDescText.text = pediaBundle.Get("m.desc." + lowerName);
  }

  private void PopulateGenericDesc(string lowerName)
  {
    genericDescPanel.gameObject.SetActive(true);
    longDescText.text = pediaBundle.Get("m.desc." + lowerName);
  }

  private void SelectTabForId(PediaDirector.Id id)
  {
    if (id != PediaDirector.Id.LOCKED)
    {
      if (Array.IndexOf(TUTORIALS_ENTRIES, id) != -1 || id == PediaDirector.Id.TUTORIALS)
      {
        vacpackTab.isOn = true;
        BuildListing(TUTORIALS_ENTRIES);
      }
      else if (Array.IndexOf(SLIMES_ENTRIES, id) != -1 || id == PediaDirector.Id.SLIMES)
      {
        slimesTab.isOn = true;
        BuildListing(SLIMES_ENTRIES);
      }
      else if (Array.IndexOf(RESOURCES_ENTRIES, id) != -1 || id == PediaDirector.Id.RESOURCES)
      {
        resourcesTab.isOn = true;
        BuildListing(RESOURCES_ENTRIES);
      }
      else if (Array.IndexOf(RANCH_ENTRIES, id) != -1 || id == PediaDirector.Id.RANCH)
      {
        ranchTab.isOn = true;
        BuildListing(RANCH_ENTRIES);
      }
      else if (Array.IndexOf(WORLD_ENTRIES, id) != -1 || id == PediaDirector.Id.WORLD)
      {
        worldTab.isOn = true;
        BuildListing(WORLD_ENTRIES);
      }
      else if (Array.IndexOf(SCIENCE_ENTRIES, id) != -1 || id == PediaDirector.Id.SCIENCE)
      {
        scienceTab.isOn = true;
        BuildListing(SCIENCE_ENTRIES);
      }
      else
        Log.Debug("Could not find tab for pedia ID, skipping.", nameof (id), id);
    }
    tabs.RecalcSelected();
  }

  private void BuildListing(PediaDirector.Id[] ids)
  {
    for (int index = 0; index < listingPanel.childCount; ++index)
      Destroyer.Destroy(listingPanel.GetChild(index).gameObject, "PediaUI.BuildListing");
    listItems.Clear();
    ToggleGroup component = listingPanel.GetComponent<ToggleGroup>();
    bool flag = true;
    foreach (PediaDirector.Id id in ids)
    {
      if (!PediaDirector.HIDDEN_ENTRIES.Contains(id) || pediaDir.IsUnlocked(id))
      {
        GameObject listing = CreateListing(pediaDir.pediaListingPrefab, pediaDir.Get(id), id);
        listing.transform.SetParent(listingPanel, false);
        if (flag && listing.activeSelf)
        {
          flag = false;
          listing.AddComponent<InitSelected>();
        }
        listing.GetComponent<Toggle>().group = component;
        listItems[id] = listing.GetComponent<Toggle>();
      }
    }
    StartCoroutine(DelayedResetScroller(listingScroller));
  }

  private IEnumerator DelayedResetScroller(ScrollRect scroller)
  {
    yield return new WaitForEndOfFrame();
    scroller.verticalNormalizedPosition = 1f;
  }

  private GameObject CreateListing(
    GameObject prefab,
    PediaDirector.IdEntry entry,
    PediaDirector.Id listingId)
  {
    GameObject listing = Instantiate(prefab);
    TMP_Text component1 = listing.transform.Find("NameText").GetComponent<TMP_Text>();
    Image component2 = listing.transform.Find("Image").GetComponent<Image>();
    if (entry == null)
      Debug.Log("Missing Pedia entry, using fallback icons and text.");
    string str = pediaBundle.Xlate("t." + (entry == null ? "*UNKNOWN*" : Enum.GetName(typeof (PediaDirector.Id), entry.id).ToLowerInvariant()));
    component1.text = str;
    component2.sprite = entry == null ? pediaDir.unknownIcon : entry.icon;
    PediaListingUI listingUI = listing.GetComponent<PediaListingUI>();
    listingUI.id = entry == null ? PediaDirector.Id.PINK_SLIME : entry.id;
    OnSelectDelegator.Create(listing, () => SelectEntry(listingUI.id, false, listingId));
    return listing;
  }

  protected override bool Closeable() => true;
}
