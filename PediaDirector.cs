// Decompiled with JetBrains decompiler
// Type: PediaDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PediaDirector : SRBehaviour, PediaModel.Participant
{
  public static HashSet<Id> HIDDEN_ENTRIES = new HashSet<Id>()
  {
    Id.PARTY_GORDO_SLIME,
    Id.ECHO_NOTE_GORDO_SLIME,
    Id.ECHO_NOTES
  };
  public const Id DEFAULT_PEDIA_ENTRY = Id.BASICS;
  public Sprite unknownIcon;
  public IdEntry lockedEntry;
  public IdEntry[] entries;
  public Id[] initUnlocked;
  public IdentMapEntry[] identMapEntries;
  public GameObject pediaPopupPrefab;
  public GameObject pediaPanelPrefab;
  public GameObject pediaListingPrefab;
  private Dictionary<Identifiable.Id, Id> identDict = new Dictionary<Identifiable.Id, Id>(Identifiable.idComparer);
  private Dictionary<Id, IdEntry> entryDict = new Dictionary<Id, IdEntry>();
  private AchievementsDirector achieveDir;
  private PopupDirector popupDir;
  private GameObject pediaUiObject;
  private PediaModel pediaModel;

  public IEnumerable<Id> GetInitUnlocked() => initUnlocked;

  public void OnUnlockedChanged(HashSet<Id> unlocked)
  {
    if (!entries.All(e => unlocked.Contains(e.id) || HIDDEN_ENTRIES.Contains(e.id)))
      return;
    achieveDir.MaybeUpdateMaxStat(AchievementsDirector.IntStat.COMPLETED_SLIMEPEDIA, 1);
  }

  public void UnlockScience() => pediaModel.Unlock(Id.REFINERY, Id.FABRICATOR, Id.BLUEPRINTS, Id.EXTRACTORS, Id.UTILITIES, Id.WARP_TECH, Id.DECORATIONS, Id.CURIOS, Id.DRONES, Id.SSBASICS, Id.GADGETMODE);

  public void Unlock(params Id[] ids) => pediaModel.Unlock(ids);

  public void Awake()
  {
    foreach (IdEntry entry in entries)
      entryDict[entry.id] = entry;
    foreach (IdentMapEntry identMapEntry in identMapEntries)
      identDict[identMapEntry.identId] = identMapEntry.pediaId;
    popupDir = SRSingleton<SceneContext>.Instance.PopupDirector;
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
  }

  public void InitForLevel() => SRSingleton<SceneContext>.Instance.GameModel.RegisterPedia(this);

  public void InitModel(PediaModel pediaModel) => pediaModel.ResetUnlocked(initUnlocked);

  public void SetModel(PediaModel pediaModel) => this.pediaModel = pediaModel;

  public void Update()
  {
    if (!SRInput.Actions.pedia.WasPressed)
      return;
    Id pediaId = Id.BASICS;
    PediaPopupUI objectOfType = FindObjectOfType<PediaPopupUI>();
    if (objectOfType != null)
    {
      pediaId = objectOfType.GetId();
      Destroyer.Destroy(objectOfType.gameObject, "PediaDirector.Update");
    }
    ShowPedia(pediaId);
  }

  public IdEntry Get(Id id) => IsUnlocked(id) && entryDict.ContainsKey(id) ? entryDict.Get(id) : lockedEntry;

  public int GetUnlockedCount() => pediaModel.unlocked.Count;

  public void UnlockWithoutPopup(Id id) => pediaModel.Unlock(id);

  public void MaybeShowPopup(Id id)
  {
    if (IsUnlocked(id))
      return;
    if (SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().assumeExperiencedUser)
    {
      pediaModel.unlocked.Add(id);
    }
    else
    {
      PediaPopupCreator creator = new PediaPopupCreator(this, id);
      if (popupDir.IsQueued(creator))
        return;
      popupDir.QueueForPopup(creator);
      popupDir.MaybePopupNext();
    }
  }

  public void MaybeShowPopup(Identifiable.Id identId)
  {
    if (!identDict.ContainsKey(identId))
      return;
    MaybeShowPopup(identDict[identId]);
  }

  public GameObject ShowPedia(Id pediaId)
  {
    pediaUiObject = Instantiate(pediaPanelPrefab);
    PediaUI component = pediaUiObject.GetComponent<PediaUI>();
    component.SelectEntry(pediaId, true, pediaId);
    return component.gameObject;
  }

  public bool IsPediaOpen() => pediaUiObject != null;

  public Id? GetPediaId(Identifiable.Id identId) => identDict.ContainsKey(identId) ? new Id?(identDict[identId]) : new Id?();

  public bool IsUnlocked(Id id) => pediaModel.unlocked.Contains(id);

  public enum Id
  {
    TUTORIALS = 0,
    SLIMES = 1,
    RESOURCES = 2,
    RANCH = 3,
    WORLD = 4,
    SCIENCE = 5,
    SPLASH = 1000, // 0x000003E8
    BASICS = 1001, // 0x000003E9
    VACING = 1002, // 0x000003EA
    CAPTURETANKS = 1003, // 0x000003EB
    ENERGY = 1004, // 0x000003EC
    CORRALLING = 1005, // 0x000003ED
    FEEDING = 1006, // 0x000003EE
    PLORTS = 1007, // 0x000003EF
    SSBASICS = 1008, // 0x000003F0
    GADGETMODE = 1009, // 0x000003F1
    WILDS_TUTORIAL = 1010, // 0x000003F2
    VALLEY_TUTORIAL = 1011, // 0x000003F3
    SLIMULATIONS_TUTORIAL = 1012, // 0x000003F4
    PINK_SLIME = 2000, // 0x000007D0
    ROCK_SLIME = 2001, // 0x000007D1
    PHOSPHOR_SLIME = 2002, // 0x000007D2
    TABBY_SLIME = 2003, // 0x000007D3
    RAD_SLIME = 2004, // 0x000007D4
    BOOM_SLIME = 2005, // 0x000007D5
    HUNTER_SLIME = 2006, // 0x000007D6
    HONEY_SLIME = 2007, // 0x000007D7
    PUDDLE_SLIME = 2008, // 0x000007D8
    CRYSTAL_SLIME = 2009, // 0x000007D9
    TARR_SLIME = 2900, // 0x00000B54
    GOLD_SLIME = 2901, // 0x00000B55
    LUCKY_SLIME = 2902, // 0x00000B56
    LARGO_SLIME = 2980, // 0x00000BA4
    GORDO_SLIME = 2981, // 0x00000BA5
    FERAL_SLIME = 2982, // 0x00000BA6
    QUANTUM_SLIME = 2983, // 0x00000BA7
    FIRE_SLIME = 2984, // 0x00000BA8
    MOSAIC_SLIME = 2985, // 0x00000BA9
    DERVISH_SLIME = 2986, // 0x00000BAA
    TANGLE_SLIME = 2987, // 0x00000BAB
    SABER_SLIME = 2988, // 0x00000BAC
    QUICKSILVER_SLIME = 2989, // 0x00000BAD
    PARTY_GORDO_SLIME = 2990, // 0x00000BAE
    ECHO_NOTE_GORDO_SLIME = 2991, // 0x00000BAF
    GLITCH_SLIME = 2992, // 0x00000BB0
    CARROT = 3000, // 0x00000BB8
    OCAOCA = 3001, // 0x00000BB9
    BEET = 3002, // 0x00000BBA
    PARSNIP = 3003, // 0x00000BBB
    POGO = 3004, // 0x00000BBC
    MANGO = 3005, // 0x00000BBD
    CUBERRY = 3006, // 0x00000BBE
    PEAR = 3007, // 0x00000BBF
    HENHEN = 3008, // 0x00000BC0
    BRIAR_HEN = 3009, // 0x00000BC1
    STONY_HEN = 3010, // 0x00000BC2
    ROOSTRO = 3011, // 0x00000BC3
    CHICKADOO = 3012, // 0x00000BC4
    BRIAR_CHICKADOO = 3013, // 0x00000BC5
    STONY_CHICKADOO = 3014, // 0x00000BC6
    ELDER_HEN = 3015, // 0x00000BC7
    ELDER_ROOSTRO = 3016, // 0x00000BC8
    ONION = 3017, // 0x00000BC9
    LEMON = 3018, // 0x00000BCA
    PAINTED_HEN = 3019, // 0x00000BCB
    PAINTED_CHICKADOO = 3020, // 0x00000BCC
    GINGER = 3021, // 0x00000BCD
    KOOKADOBA = 3022, // 0x00000BCE
    SPICY_TOFU = 3023, // 0x00000BCF
    PRIMORDY_OIL_CRAFT = 3900, // 0x00000F3C
    DEEP_BRINE_CRAFT = 3901, // 0x00000F3D
    SPIRAL_STEAM_CRAFT = 3902, // 0x00000F3E
    LAVA_DUST_CRAFT = 3903, // 0x00000F3F
    BUZZ_WAX_CRAFT = 3904, // 0x00000F40
    WILD_HONEY_CRAFT = 3905, // 0x00000F41
    HEXACOMB_CRAFT = 3906, // 0x00000F42
    ROYAL_JELLY_CRAFT = 3907, // 0x00000F43
    JELLYSTONE_CRAFT = 3908, // 0x00000F44
    INDIGONIUM_CRAFT = 3909, // 0x00000F45
    SLIME_FOSSIL_CRAFT = 3910, // 0x00000F46
    STRANGE_DIAMOND_CRAFT = 3911, // 0x00000F47
    ECHOES = 3912, // 0x00000F48
    SLIME_TOYS = 3913, // 0x00000F49
    SILKY_SAND_CRAFT = 3914, // 0x00000F4A
    PEPPER_JAM_CRAFT = 3915, // 0x00000F4B
    GLASS_SHARD_CRAFT = 3916, // 0x00000F4C
    ECHO_NOTES = 3917, // 0x00000F4D
    MANIFOLD_CUBE_CRAFT = 3918, // 0x00000F4E
    ORNAMENTS = 3919, // 0x00000F4F
    CORRAL = 4000, // 0x00000FA0
    COOP = 4001, // 0x00000FA1
    GARDEN = 4002, // 0x00000FA2
    SILO = 4003, // 0x00000FA3
    INCINERATOR = 4004, // 0x00000FA4
    POND = 4005, // 0x00000FA5
    PLORT_MARKET = 4006, // 0x00000FA6
    OVERGROWTH = 4007, // 0x00000FA7
    GROTTO = 4008, // 0x00000FA8
    LAB = 4009, // 0x00000FA9
    CHROMA = 4010, // 0x00000FAA
    PARTNER = 4011, // 0x00000FAB
    DOCKS = 4012, // 0x00000FAC
    OGDEN_RETREAT = 4013, // 0x00000FAD
    MOCHI_MANOR = 4014, // 0x00000FAE
    VALLEY = 4015, // 0x00000FAF
    VIKTOR_LAB = 4016, // 0x00000FB0
    REEF = 5000, // 0x00001388
    QUARRY = 5001, // 0x00001389
    MOSS = 5002, // 0x0000138A
    DESERT = 5003, // 0x0000138B
    SEA = 5004, // 0x0000138C
    THE_RANCH = 5005, // 0x0000138D
    KEYS = 5006, // 0x0000138E
    EXTRACTORS = 5007, // 0x0000138F
    UTILITIES = 5008, // 0x00001390
    WARP_TECH = 5009, // 0x00001391
    DECORATIONS = 5010, // 0x00001392
    CURIOS = 5011, // 0x00001393
    REFINERY = 5012, // 0x00001394
    FABRICATOR = 5013, // 0x00001395
    BLUEPRINTS = 5014, // 0x00001396
    RUINS = 5015, // 0x00001397
    WILDS = 5016, // 0x00001398
    SLIMULATIONS_WORLD = 5017, // 0x00001399
    DRONES = 6010, // 0x0000177A
    LOCKED = 10000, // 0x00002710
  }

  [Serializable]
  public class IdEntry
  {
    public Id id;
    public Sprite icon;
  }

  [Serializable]
  public class IdentMapEntry
  {
    public Identifiable.Id identId;
    public Id pediaId;
  }

  private class PediaPopupCreator : PopupDirector.PopupCreator
  {
    private PediaDirector pediaDir;
    private Id id;

    public PediaPopupCreator(PediaDirector pediaDir, Id id)
    {
      this.pediaDir = pediaDir;
      this.id = id;
    }

    public override void Create()
    {
      pediaDir.pediaModel.Unlock(id);
      PediaPopupUI.CreatePediaPopup(pediaDir.Get(id));
    }

    public override bool Equals(object other) => other is PediaPopupCreator && ((PediaPopupCreator) other).id == id;

    public override int GetHashCode() => id.GetHashCode();

    public override bool ShouldClear() => false;
  }
}
