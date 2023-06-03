// Decompiled with JetBrains decompiler
// Type: Identifiable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identifiable : SRBehaviour, ActorModel.Participant
{
  public static IdComparer idComparer = new IdComparer();
  public Id id;
  private ActorModel model;
  private RegionMember member;
  private const string VEGGIE_SUFFIX = "_VEGGIE";
  private const string FRUIT_SUFFIX = "_FRUIT";
  private const string TOFU_SUFFIX = "_TOFU";
  private const string HEN_SUFFIX = "HEN";
  private const string ROOSTER_SUFFIX = "ROOSTER";
  private const string CHICK_SUFFIX = "_CHICK";
  private const string SLIME_SUFFIX = "_SLIME";
  private const string LARGO_SUFFIX = "_LARGO";
  private const string GORDO_SUFFIX = "_GORDO";
  private const string PLORT_SUFFIX = "_PLORT";
  private const string LIQUID_SUFFIX = "_LIQUID";
  private const string CRAFT_SUFFIX = "_CRAFT";
  private const string FASHION_SUFFIX = "_FASHION";
  private const string ECHO_SUFFIX = "_ECHO";
  private const string ECHO_NOTE_PREFIX = "ECHO_NOTE_";
  private const string ORNAMENT_SUFFIX = "_ORNAMENT";
  private const string TOY_SUFFIX = "_TOY";
  private const string TANGLE_STRING = "TANGLE";
  public static HashSet<Id> VEGGIE_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> FRUIT_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> MEAT_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> TOFU_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> SLIME_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> LARGO_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> GORDO_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> PLORT_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> FOOD_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> NON_SLIMES_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> CHICK_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> LIQUID_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> CRAFT_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> FASHION_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> ECHO_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> ECHO_NOTE_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> ORNAMENT_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> TOY_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> EATERS_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> ALLERGY_FREE_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> STANDARD_CRATE_CLASS = new HashSet<Id>(idComparer)
  {
    Id.CRATE_DESERT_01,
    Id.CRATE_MOSS_01,
    Id.CRATE_QUARRY_01,
    Id.CRATE_REEF_01,
    Id.CRATE_RUINS_01,
    Id.CRATE_WILDS_01
  };
  public static HashSet<Id> ELDER_CLASS = new HashSet<Id>(idComparer)
  {
    Id.ELDER_HEN,
    Id.ELDER_ROOSTER
  };
  public static HashSet<Id> TARR_CLASS = new HashSet<Id>(idComparer)
  {
    Id.TARR_SLIME,
    Id.GLITCH_TARR_SLIME
  };
  public static HashSet<Id> BOOP_CLASS = new HashSet<Id>(idComparer)
  {
    Id.TABBY_SLIME,
    Id.PINK_TABBY_LARGO,
    Id.BOOM_TABBY_LARGO,
    Id.RAD_TABBY_LARGO,
    Id.ROCK_TABBY_LARGO,
    Id.PHOSPHOR_TABBY_LARGO,
    Id.TABBY_HUNTER_LARGO,
    Id.HONEY_TABBY_LARGO,
    Id.TABBY_CRYSTAL_LARGO,
    Id.QUANTUM_TABBY_LARGO,
    Id.TANGLE_TABBY_LARGO,
    Id.MOSAIC_TABBY_LARGO,
    Id.TABBY_DERVISH_LARGO,
    Id.SABER_TABBY_LARGO
  };
  public static readonly HashSet<Id> SCENE_OBJECTS = new HashSet<Id>(idComparer)
  {
    Id.PLAYER,
    Id.FIRE_COLUMN,
    Id.SCARECROW,
    Id.PORTABLE_SCARECROW,
    Id.GLITCH_TARR_PORTAL
  };
  public OnDestroyListener NotifyOnDestroy;
  private static readonly Dictionary<Id, Gadget.Id> GADGET_NAME_DICT = new Dictionary<Id, Gadget.Id>(idComparer)
  {
    {
      Id.HANDLEBAR_FASHION,
      Gadget.Id.FASHION_POD_HANDLEBAR
    },
    {
      Id.SHADY_FASHION,
      Gadget.Id.FASHION_POD_SHADY
    },
    {
      Id.CLIP_ON_FASHION,
      Gadget.Id.FASHION_POD_CLIP_ON
    },
    {
      Id.GOOGLY_FASHION,
      Gadget.Id.FASHION_POD_GOOGLY
    },
    {
      Id.SERIOUS_FASHION,
      Gadget.Id.FASHION_POD_SERIOUS
    },
    {
      Id.SMART_FASHION,
      Gadget.Id.FASHION_POD_SMART
    },
    {
      Id.CUTE_FASHION,
      Gadget.Id.FASHION_POD_CUTE
    },
    {
      Id.ROYAL_FASHION,
      Gadget.Id.FASHION_POD_ROYAL
    },
    {
      Id.DANDY_FASHION,
      Gadget.Id.FASHION_POD_DANDY
    },
    {
      Id.PIRATEY_FASHION,
      Gadget.Id.FASHION_POD_PIRATEY
    },
    {
      Id.HEROIC_FASHION,
      Gadget.Id.FASHION_POD_HEROIC
    },
    {
      Id.SCIFI_FASHION,
      Gadget.Id.FASHION_POD_SCIFI
    },
    {
      Id.SCUBA_FASHION,
      Gadget.Id.FASHION_POD_SCUBA
    },
    {
      Id.PARTY_GLASSES_FASHION,
      Gadget.Id.FASHION_POD_PARTY_GLASSES
    },
    {
      Id.REMOVER_FASHION,
      Gadget.Id.FASHION_POD_REMOVER
    }
  };

  static Identifiable()
  {
    foreach (Id id in Enum.GetValues(typeof (Id)))
    {
      string name = Enum.GetName(typeof (Id), id);
      if (name.EndsWith("_VEGGIE"))
      {
        VEGGIE_CLASS.Add(id);
        FOOD_CLASS.Add(id);
        NON_SLIMES_CLASS.Add(id);
      }
      else if (name.EndsWith("_FRUIT"))
      {
        FRUIT_CLASS.Add(id);
        FOOD_CLASS.Add(id);
        NON_SLIMES_CLASS.Add(id);
      }
      else if (name.EndsWith("_TOFU"))
      {
        TOFU_CLASS.Add(id);
        FOOD_CLASS.Add(id);
        NON_SLIMES_CLASS.Add(id);
      }
      else if (name.EndsWith("_SLIME"))
        SLIME_CLASS.Add(id);
      else if (name.EndsWith("_LARGO"))
        LARGO_CLASS.Add(id);
      else if (name.EndsWith("_GORDO"))
        GORDO_CLASS.Add(id);
      else if (name.EndsWith("_PLORT"))
      {
        PLORT_CLASS.Add(id);
        NON_SLIMES_CLASS.Add(id);
      }
      else if (name.EndsWith("HEN") || name.EndsWith("ROOSTER"))
      {
        MEAT_CLASS.Add(id);
        FOOD_CLASS.Add(id);
        NON_SLIMES_CLASS.Add(id);
      }
      else if (id == Id.CHICK || name.EndsWith("_CHICK"))
      {
        NON_SLIMES_CLASS.Add(id);
        CHICK_CLASS.Add(id);
      }
      else if (name.EndsWith("_LIQUID"))
        LIQUID_CLASS.Add(id);
      else if (name.EndsWith("_CRAFT"))
      {
        CRAFT_CLASS.Add(id);
        NON_SLIMES_CLASS.Add(id);
      }
      else if (name.EndsWith("_FASHION"))
        FASHION_CLASS.Add(id);
      else if (name.EndsWith("_ECHO"))
        ECHO_CLASS.Add(id);
      else if (name.StartsWith("ECHO_NOTE_"))
        ECHO_NOTE_CLASS.Add(id);
      else if (name.EndsWith("_ORNAMENT"))
        ORNAMENT_CLASS.Add(id);
      else if (name.EndsWith("_TOY") || id == Id.KOOKADOBA_BALL)
        TOY_CLASS.Add(id);
      if (name.Contains("TANGLE"))
        ALLERGY_FREE_CLASS.Add(id);
    }
    EATERS_CLASS.UnionWith(SLIME_CLASS);
    EATERS_CLASS.UnionWith(LARGO_CLASS);
  }

  public bool isPhysicsInitialized { get; private set; }

  private IEnumerator SetPhysicsInitialized()
  {
    yield return new WaitForFixedUpdate();
    isPhysicsInitialized = true;
  }

  public void Awake()
  {
    member = GetComponent<RegionMember>();
    if (id == Id.PLAYER)
      SRSingleton<SceneContext>.Instance.Player = gameObject;
    if (!SCENE_OBJECTS.Contains(id))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterStartingActor(gameObject, GetStartingActorRegion());
  }

  private RegionRegistry.RegionSetId GetStartingActorRegion()
  {
    switch (id)
    {
      case Id.PLAYER:
        return RegionRegistry.RegionSetId.HOME;
      case Id.SCARECROW:
      case Id.PORTABLE_SCARECROW:
        return GetComponentInParent<Region>().setId;
      case Id.FIRE_COLUMN:
        return RegionRegistry.RegionSetId.DESERT;
      case Id.GLITCH_TARR_PORTAL:
        return RegionRegistry.RegionSetId.SLIMULATIONS;
      default:
        throw new ArgumentException(string.Format("Failed to get RegionSetId for starting actor. [id={0}]", id));
    }
  }

  public void OnEnable() => StartCoroutine(SetPhysicsInitialized());

  public void OnDisable() => isPhysicsInitialized = false;

  public void OnDestroy()
  {
    if (model != null)
      CellDirector.UnregisterFromAll(member, gameObject, model);
    member.regionsChanged -= OnMemberChanged_UpdateRegistration;
    if (NotifyOnDestroy == null)
      return;
    NotifyOnDestroy(this);
  }

  private static CellDirector GetDirector(Region region) => region.cellDir;

  public static bool IsSlime(Id id) => SLIME_CLASS.Contains(id) || LARGO_CLASS.Contains(id);

  public static bool IsGordo(Id id) => GORDO_CLASS.Contains(id);

  public static bool IsLargo(Id id) => LARGO_CLASS.Contains(id);

  public static bool IsPlort(Id id) => PLORT_CLASS.Contains(id);

  public static bool IsAnimal(Id id) => MEAT_CLASS.Contains(id) || CHICK_CLASS.Contains(id);

  public static bool IsChick(Id id) => CHICK_CLASS.Contains(id);

  public static bool IsFood(Id id) => FOOD_CLASS.Contains(id);

  public static bool IsVeggie(Id id) => VEGGIE_CLASS.Contains(id);

  public static bool IsFruit(Id id) => FRUIT_CLASS.Contains(id);

  public static bool IsCraft(Id id) => CRAFT_CLASS.Contains(id);

  public static bool IsEcho(Id id) => ECHO_CLASS.Contains(id);

  public static bool IsEchoNote(Id id) => ECHO_NOTE_CLASS.Contains(id);

  public static bool IsOrnament(Id id) => ORNAMENT_CLASS.Contains(id);

  public static bool IsToy(Id id) => TOY_CLASS.Contains(id);

  public static bool IsLiquid(Id id) => LIQUID_CLASS.Contains(id);

  public static bool IsWater(Id id) => id != Id.GLITCH_DEBUG_SPRAY_LIQUID && LIQUID_CLASS.Contains(id);

  public static bool IsFashion(Id id) => FASHION_CLASS.Contains(id);

  public static bool IsNonSlimeResource(Id id) => NON_SLIMES_CLASS.Contains(id);

  public static bool IsAllergyFree(Id id) => ALLERGY_FREE_CLASS.Contains(id);

  public static bool IsTarr(Id id) => TARR_CLASS.Contains(id);

  public static Id Combine(List<Id> ids)
  {
    if (ids.Count == 0)
      return Id.NONE;
    if (ids.Count == 1)
      return ids[0];
    if (ids.Count != 2)
      return Id.TARR_SLIME;
    string name1 = Enum.GetName(typeof (Id), ids[0]);
    string name2 = Enum.GetName(typeof (Id), ids[1]);
    string str1 = name1.EndsWith("_SLIME") && name2.EndsWith("_SLIME") ? name1.Substring(0, name1.Length - "_SLIME".Length) : throw new ArgumentException("Illegal identifiable to combine, must be slime: " + ids[0] + "," + ids[1]);
    string str2 = name2.Substring(0, name2.Length - "_SLIME".Length);
    try
    {
      return (Id) Enum.Parse(typeof (Id), str1 + "_" + str2 + "_LARGO");
    }
    catch (Exception ex1)
    {
      try
      {
        return (Id) Enum.Parse(typeof (Id), str2 + "_" + str1 + "_LARGO");
      }
      catch (Exception ex2)
      {
        return Id.TARR_SLIME;
      }
    }
  }

  public static Id GetId(GameObject gameObject)
  {
    Identifiable component = gameObject.GetComponent<Identifiable>();
    return component != null ? component.id : Id.NONE;
  }

  public static long GetActorId(GameObject gameObject)
  {
    Identifiable component = gameObject.GetComponent<Identifiable>();
    return component != null ? component.GetActorId() : throw new InvalidOperationException("Tried to get an actor ID for an object that had none: " + gameObject.name);
  }

  public static string GetName(Id id, bool reportMissing = true)
  {
    Gadget.Id id1;
    if (GADGET_NAME_DICT.TryGetValue(id, out id1))
      return Gadget.GetName(id1, reportMissing);
    string resourceString1 = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("actor").GetResourceString(string.Format("l.{0}", Enum.GetName(typeof (Id), id).ToLowerInvariant()), false);
    if (resourceString1 != null)
      return resourceString1;
    string resourceString2 = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("pedia").GetResourceString(string.Format("t.{0}", Enum.GetName(typeof (Id), id).ToLowerInvariant()), false);
    if (resourceString2 != null)
      return resourceString2;
    PediaDirector.Id? pediaId = SRSingleton<SceneContext>.Instance.PediaDirector.GetPediaId(id);
    if (pediaId.HasValue)
    {
      SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("pedia");
      string.Format("t.{0}", Enum.GetName(typeof (PediaDirector.Id), pediaId).ToLowerInvariant());
      if (resourceString2 != null)
        return resourceString2;
    }
    if (reportMissing)
      Log.Warning("Missing identifiable translation.", nameof (id), id);
    return null;
  }

  public long GetActorId() => model.actorId;

  public void InitModel(ActorModel model)
  {
  }

  public void SetModel(ActorModel model)
  {
    this.model = model;
    member.regionsChanged += OnMemberChanged_UpdateRegistration;
    foreach (Region region in member.regions)
      region.cellDir.Register(gameObject, model);
  }

  private void OnMemberChanged_UpdateRegistration(List<Region> left, List<Region> joined)
  {
    if (left != null)
    {
      foreach (Region region in left)
      {
        try
        {
          GetDirector(region).Unregister(gameObject, model);
        }
        catch (MissingReferenceException ex)
        {
        }
      }
    }
    if (joined == null)
      return;
    foreach (Region region in joined)
    {
      try
      {
        GetDirector(region).Register(gameObject, model);
      }
      catch (MissingReferenceException ex)
      {
      }
    }
  }

  public static bool IsEdible(GameObject gameObject)
  {
    Identifiable component = gameObject.GetComponent<Identifiable>();
    return component != null && component.IsEdible();
  }

  public bool IsEdible() => model != null && model.IsEdible();

  public enum Id
  {
    NONE = 0,
    RAD_SLIME = 1,
    ROCK_SLIME = 2,
    PINK_SLIME = 3,
    RAD_PLORT = 4,
    ROCK_PLORT = 5,
    PINK_PLORT = 6,
    GOLD_PLORT = 7,
    CUBERRY_FRUIT = 8,
    MANGO_FRUIT = 9,
    TARR_SLIME = 10, // 0x0000000A
    GOLD_SLIME = 11, // 0x0000000B
    PINK_ROCK_LARGO = 12, // 0x0000000C
    RAD_ROCK_LARGO = 13, // 0x0000000D
    PINK_RAD_LARGO = 14, // 0x0000000E
    PLAYER = 15, // 0x0000000F
    HEN = 16, // 0x00000010
    ROOSTER = 17, // 0x00000011
    CHICK = 18, // 0x00000012
    CARROT_VEGGIE = 19, // 0x00000013
    OCAOCA_VEGGIE = 20, // 0x00000014
    BOOM_SLIME = 21, // 0x00000015
    PINK_BOOM_LARGO = 22, // 0x00000016
    BOOM_ROCK_LARGO = 23, // 0x00000017
    BOOM_RAD_LARGO = 24, // 0x00000018
    BOOM_PLORT = 25, // 0x00000019
    PEAR_FRUIT = 26, // 0x0000001A
    POGO_FRUIT = 27, // 0x0000001B
    PARSNIP_VEGGIE = 28, // 0x0000001C
    BEET_VEGGIE = 29, // 0x0000001D
    SCARECROW = 30, // 0x0000001E
    PHOSPHOR_SLIME = 31, // 0x0000001F
    PHOSPHOR_ROCK_LARGO = 32, // 0x00000020
    BOOM_PHOSPHOR_LARGO = 33, // 0x00000021
    PHOSPHOR_RAD_LARGO = 34, // 0x00000022
    PINK_PHOSPHOR_LARGO = 35, // 0x00000023
    PHOSPHOR_PLORT = 36, // 0x00000024
    TABBY_SLIME = 37, // 0x00000025
    TABBY_PLORT = 38, // 0x00000026
    PINK_TABBY_LARGO = 39, // 0x00000027
    BOOM_TABBY_LARGO = 40, // 0x00000028
    RAD_TABBY_LARGO = 41, // 0x00000029
    ROCK_TABBY_LARGO = 42, // 0x0000002A
    PHOSPHOR_TABBY_LARGO = 43, // 0x0000002B
    CRATE_REEF_01 = 44, // 0x0000002C
    CRATE_QUARRY_01 = 45, // 0x0000002D
    CRATE_MOSS_01 = 46, // 0x0000002E
    CRATE_DESERT_01 = 47, // 0x0000002F
    WATER_LIQUID = 48, // 0x00000030
    ELDER_HEN = 49, // 0x00000031
    ELDER_ROOSTER = 50, // 0x00000032
    HUNTER_SLIME = 51, // 0x00000033
    HUNTER_PLORT = 52, // 0x00000034
    PINK_HUNTER_LARGO = 53, // 0x00000035
    BOOM_HUNTER_LARGO = 54, // 0x00000036
    RAD_HUNTER_LARGO = 55, // 0x00000037
    ROCK_HUNTER_LARGO = 56, // 0x00000038
    PHOSPHOR_HUNTER_LARGO = 57, // 0x00000039
    TABBY_HUNTER_LARGO = 58, // 0x0000003A
    HONEY_SLIME = 59, // 0x0000003B
    HONEY_PLORT = 60, // 0x0000003C
    PINK_HONEY_LARGO = 61, // 0x0000003D
    HONEY_HUNTER_LARGO = 62, // 0x0000003E
    HONEY_BOOM_LARGO = 63, // 0x0000003F
    HONEY_RAD_LARGO = 64, // 0x00000040
    HONEY_ROCK_LARGO = 65, // 0x00000041
    HONEY_PHOSPHOR_LARGO = 66, // 0x00000042
    HONEY_TABBY_LARGO = 67, // 0x00000043
    STONY_HEN = 68, // 0x00000044
    BRIAR_HEN = 69, // 0x00000045
    STONY_CHICK = 70, // 0x00000046
    BRIAR_CHICK = 71, // 0x00000047
    PUDDLE_SLIME = 72, // 0x00000048
    PUDDLE_PLORT = 73, // 0x00000049
    DAILY_EXCHANGE_CRATE = 74, // 0x0000004A
    SPECIAL_EXCHANGE_CRATE = 75, // 0x0000004B
    KEY = 76, // 0x0000004C
    LUCKY_SLIME = 77, // 0x0000004D
    CRYSTAL_PLORT = 78, // 0x0000004E
    CRYSTAL_SLIME = 79, // 0x0000004F
    PINK_CRYSTAL_LARGO = 80, // 0x00000050
    ROCK_CRYSTAL_LARGO = 81, // 0x00000051
    TABBY_CRYSTAL_LARGO = 82, // 0x00000052
    PHOSPHOR_CRYSTAL_LARGO = 83, // 0x00000053
    BOOM_CRYSTAL_LARGO = 84, // 0x00000054
    RAD_CRYSTAL_LARGO = 85, // 0x00000055
    HONEY_CRYSTAL_LARGO = 86, // 0x00000056
    HUNTER_CRYSTAL_LARGO = 87, // 0x00000057
    ONION_VEGGIE = 88, // 0x00000058
    QUANTUM_SLIME = 89, // 0x00000059
    PINK_QUANTUM_LARGO = 90, // 0x0000005A
    QUANTUM_BOOM_LARGO = 91, // 0x0000005B
    QUANTUM_CRYSTAL_LARGO = 92, // 0x0000005C
    QUANTUM_HONEY_LARGO = 93, // 0x0000005D
    QUANTUM_HUNTER_LARGO = 94, // 0x0000005E
    QUANTUM_PHOSPHOR_LARGO = 95, // 0x0000005F
    QUANTUM_RAD_LARGO = 96, // 0x00000060
    QUANTUM_ROCK_LARGO = 97, // 0x00000061
    QUANTUM_TABBY_LARGO = 98, // 0x00000062
    QUANTUM_PLORT = 99, // 0x00000063
    LEMON_FRUIT = 100, // 0x00000064
    LEMON_PHASE = 101, // 0x00000065
    DERVISH_SLIME = 102, // 0x00000066
    DERVISH_PLORT = 103, // 0x00000067
    MOSAIC_SLIME = 104, // 0x00000068
    MOSAIC_PLORT = 105, // 0x00000069
    TANGLE_SLIME = 106, // 0x0000006A
    TANGLE_PLORT = 107, // 0x0000006B
    FIRE_SLIME = 108, // 0x0000006C
    FIRE_PLORT = 109, // 0x0000006D
    PAINTED_HEN = 110, // 0x0000006E
    PAINTED_CHICK = 111, // 0x0000006F
    POLLEN_CLOUD = 112, // 0x00000070
    MAGIC_WATER_LIQUID = 113, // 0x00000071
    FIRE_COLUMN = 114, // 0x00000072
    PINK_TANGLE_LARGO = 115, // 0x00000073
    QUANTUM_TANGLE_LARGO = 116, // 0x00000074
    HONEY_TANGLE_LARGO = 117, // 0x00000075
    PHOSPHOR_TANGLE_LARGO = 118, // 0x00000076
    TANGLE_BOOM_LARGO = 119, // 0x00000077
    TANGLE_RAD_LARGO = 120, // 0x00000078
    TANGLE_ROCK_LARGO = 121, // 0x00000079
    TANGLE_TABBY_LARGO = 122, // 0x0000007A
    TANGLE_HUNTER_LARGO = 123, // 0x0000007B
    TANGLE_CRYSTAL_LARGO = 124, // 0x0000007C
    PINK_MOSAIC_LARGO = 125, // 0x0000007D
    QUANTUM_MOSAIC_LARGO = 126, // 0x0000007E
    HONEY_MOSAIC_LARGO = 127, // 0x0000007F
    PHOSPHOR_MOSAIC_LARGO = 128, // 0x00000080
    MOSAIC_TANGLE_LARGO = 129, // 0x00000081
    MOSAIC_BOOM_LARGO = 130, // 0x00000082
    MOSAIC_RAD_LARGO = 131, // 0x00000083
    MOSAIC_ROCK_LARGO = 132, // 0x00000084
    MOSAIC_TABBY_LARGO = 133, // 0x00000085
    MOSAIC_HUNTER_LARGO = 134, // 0x00000086
    MOSAIC_CRYSTAL_LARGO = 135, // 0x00000087
    PINK_DERVISH_LARGO = 136, // 0x00000088
    QUANTUM_DERVISH_LARGO = 137, // 0x00000089
    HONEY_DERVISH_LARGO = 138, // 0x0000008A
    PHOSPHOR_DERVISH_LARGO = 139, // 0x0000008B
    TANGLE_DERVISH_LARGO = 140, // 0x0000008C
    MOSAIC_DERVISH_LARGO = 141, // 0x0000008D
    BOOM_DERVISH_LARGO = 142, // 0x0000008E
    RAD_DERVISH_LARGO = 143, // 0x0000008F
    ROCK_DERVISH_LARGO = 144, // 0x00000090
    TABBY_DERVISH_LARGO = 145, // 0x00000091
    HUNTER_DERVISH_LARGO = 146, // 0x00000092
    CRYSTAL_DERVISH_LARGO = 147, // 0x00000093
    GINGER_VEGGIE = 148, // 0x00000094
    SPICY_TOFU = 149, // 0x00000095
    SABER_SLIME = 150, // 0x00000096
    SABER_PINK_LARGO = 151, // 0x00000097
    SABER_QUANTUM_LARGO = 152, // 0x00000098
    SABER_HONEY_LARGO = 153, // 0x00000099
    SABER_PHOSPHOR_LARGO = 154, // 0x0000009A
    SABER_TANGLE_LARGO = 155, // 0x0000009B
    SABER_MOSAIC_LARGO = 156, // 0x0000009C
    SABER_BOOM_LARGO = 157, // 0x0000009D
    SABER_RAD_LARGO = 158, // 0x0000009E
    SABER_ROCK_LARGO = 159, // 0x0000009F
    SABER_TABBY_LARGO = 160, // 0x000000A0
    SABER_HUNTER_LARGO = 161, // 0x000000A1
    SABER_CRYSTAL_LARGO = 162, // 0x000000A2
    SABER_DERVISH_LARGO = 163, // 0x000000A3
    SABER_PLORT = 164, // 0x000000A4
    KOOKADOBA_FRUIT = 165, // 0x000000A5
    QUICKSILVER_SLIME = 166, // 0x000000A6
    QUICKSILVER_PLORT = 167, // 0x000000A7
    KOOKADOBA_BALL = 168, // 0x000000A8
    CRATE_RUINS_01 = 169, // 0x000000A9
    CRATE_WILDS_01 = 170, // 0x000000AA
    VALLEY_AMMO_1 = 171, // 0x000000AB
    VALLEY_AMMO_2 = 172, // 0x000000AC
    VALLEY_AMMO_3 = 173, // 0x000000AD
    VALLEY_AMMO_4 = 174, // 0x000000AE
    PORTABLE_SCARECROW = 175, // 0x000000AF
    RAD_GORDO = 10000, // 0x00002710
    ROCK_GORDO = 10001, // 0x00002711
    PINK_GORDO = 10002, // 0x00002712
    BOOM_GORDO = 10003, // 0x00002713
    PHOSPHOR_GORDO = 10004, // 0x00002714
    TABBY_GORDO = 10005, // 0x00002715
    HUNTER_GORDO = 10006, // 0x00002716
    HONEY_GORDO = 10007, // 0x00002717
    PUDDLE_GORDO = 10008, // 0x00002718
    CRYSTAL_GORDO = 10009, // 0x00002719
    QUANTUM_GORDO = 10010, // 0x0000271A
    DERVISH_GORDO = 10011, // 0x0000271B
    MOSAIC_GORDO = 10012, // 0x0000271C
    TANGLE_GORDO = 10013, // 0x0000271D
    GOLD_GORDO = 10014, // 0x0000271E
    PRIMORDY_OIL_CRAFT = 11000, // 0x00002AF8
    DEEP_BRINE_CRAFT = 11001, // 0x00002AF9
    SPIRAL_STEAM_CRAFT = 11002, // 0x00002AFA
    LAVA_DUST_CRAFT = 11003, // 0x00002AFB
    BUZZ_WAX_CRAFT = 11004, // 0x00002AFC
    WILD_HONEY_CRAFT = 11005, // 0x00002AFD
    HEXACOMB_CRAFT = 11006, // 0x00002AFE
    ROYAL_JELLY_CRAFT = 11007, // 0x00002AFF
    JELLYSTONE_CRAFT = 11008, // 0x00002B00
    INDIGONIUM_CRAFT = 11009, // 0x00002B01
    SLIME_FOSSIL_CRAFT = 11010, // 0x00002B02
    STRANGE_DIAMOND_CRAFT = 11011, // 0x00002B03
    RED_ECHO = 11012, // 0x00002B04
    GREEN_ECHO = 11013, // 0x00002B05
    BLUE_ECHO = 11014, // 0x00002B06
    GOLD_ECHO = 11015, // 0x00002B07
    SILKY_SAND_CRAFT = 11016, // 0x00002B08
    PEPPER_JAM_CRAFT = 11017, // 0x00002B09
    GLASS_SHARD_CRAFT = 11018, // 0x00002B0A
    MANIFOLD_CUBE_CRAFT = 11019, // 0x00002B0B
    HANDLEBAR_FASHION = 12000, // 0x00002EE0
    SHADY_FASHION = 12001, // 0x00002EE1
    CLIP_ON_FASHION = 12002, // 0x00002EE2
    GOOGLY_FASHION = 12003, // 0x00002EE3
    SERIOUS_FASHION = 12004, // 0x00002EE4
    SMART_FASHION = 12005, // 0x00002EE5
    CUTE_FASHION = 12006, // 0x00002EE6
    ROYAL_FASHION = 12007, // 0x00002EE7
    DANDY_FASHION = 12008, // 0x00002EE8
    PARTY_FASHION = 12009, // 0x00002EE9
    PIRATEY_FASHION = 12010, // 0x00002EEA
    HEROIC_FASHION = 12011, // 0x00002EEB
    SCIFI_FASHION = 12012, // 0x00002EEC
    SCUBA_FASHION = 12013, // 0x00002EED
    PARTY_GLASSES_FASHION = 12014, // 0x00002EEE
    REMOVER_FASHION = 12099, // 0x00002F43
    BEACH_BALL_TOY = 13000, // 0x000032C8
    BIG_ROCK_TOY = 13001, // 0x000032C9
    YARN_BALL_TOY = 13002, // 0x000032CA
    NIGHT_LIGHT_TOY = 13003, // 0x000032CB
    POWER_CELL_TOY = 13004, // 0x000032CC
    BOMB_BALL_TOY = 13005, // 0x000032CD
    BUZZY_BEE_TOY = 13006, // 0x000032CE
    RUBBER_DUCKY_TOY = 13007, // 0x000032CF
    CRYSTAL_BALL_TOY = 13008, // 0x000032D0
    STUFFED_CHICKEN_TOY = 13009, // 0x000032D1
    PUZZLE_CUBE_TOY = 13010, // 0x000032D2
    DISCO_BALL_TOY = 13011, // 0x000032D3
    GYRO_TOP_TOY = 13012, // 0x000032D4
    SOL_MATE_TOY = 13013, // 0x000032D5
    CHARCOAL_BRICK_TOY = 13014, // 0x000032D6
    STEGO_BUDDY_TOY = 13015, // 0x000032D7
    TREASURE_CHEST_TOY = 13016, // 0x000032D8
    BOP_GOBLIN_TOY = 13017, // 0x000032D9
    ROBOT_TOY = 13018, // 0x000032DA
    OCTO_BUDDY_TOY = 13019, // 0x000032DB
    PINK_ORNAMENT = 14000, // 0x000036B0
    ROCK_ORNAMENT = 14001, // 0x000036B1
    TABBY_ORNAMENT = 14002, // 0x000036B2
    PHOSPHOR_ORNAMENT = 14003, // 0x000036B3
    RAD_ORNAMENT = 14004, // 0x000036B4
    BOOM_ORNAMENT = 14005, // 0x000036B5
    HONEY_ORNAMENT = 14006, // 0x000036B6
    HUNTER_ORNAMENT = 14007, // 0x000036B7
    QUANTUM_ORNAMENT = 14008, // 0x000036B8
    PUDDLE_ORNAMENT = 14009, // 0x000036B9
    TANGLE_ORNAMENT = 14010, // 0x000036BA
    DERVISH_ORNAMENT = 14011, // 0x000036BB
    MOSAIC_ORNAMENT = 14012, // 0x000036BC
    LUCKY_ORNAMENT = 14013, // 0x000036BD
    GOLD_ORNAMENT = 14014, // 0x000036BE
    TARR_ORNAMENT = 14015, // 0x000036BF
    STACHE_ORNAMENT = 14016, // 0x000036C0
    CRYSTAL_ORNAMENT = 14017, // 0x000036C1
    QUICKSILVER_ORNAMENT = 14018, // 0x000036C2
    FIRE_ORNAMENT = 14019, // 0x000036C3
    HENHEN_ORNAMENT = 14020, // 0x000036C4
    SEVENZ_ORNAMENT = 14021, // 0x000036C5
    CHEEVO_ORNAMENT = 14022, // 0x000036C6
    CLOUD_ORNAMENT = 14023, // 0x000036C7
    CLOVER_ORNAMENT = 14024, // 0x000036C8
    HEART_ORNAMENT = 14025, // 0x000036C9
    BRIAR_HEN_ORNAMENT = 14026, // 0x000036CA
    ELDER_HEN_ORNAMENT = 14027, // 0x000036CB
    PAINTED_HEN_ORNAMENT = 14028, // 0x000036CC
    STONY_HEN_ORNAMENT = 14029, // 0x000036CD
    JACK_ORNAMENT = 14030, // 0x000036CE
    NEWBUCK_ORNAMENT = 14031, // 0x000036CF
    PINK_PARTY_ORNAMENT = 14032, // 0x000036D0
    RAINBOW_ORNAMENT = 14033, // 0x000036D1
    SNOWFLAKE_ORNAMENT = 14034, // 0x000036D2
    STAR_ORNAMENT = 14035, // 0x000036D3
    STRIPES_GREEN_ORNAMENT = 14036, // 0x000036D4
    STRIPES_PURPLE_ORNAMENT = 14037, // 0x000036D5
    GLITCH_ORNAMENT = 14038, // 0x000036D6
    SABER_ORNAMENT = 14039, // 0x000036D7
    IMPOSTER_ORNAMENT = 14040, // 0x000036D8
    DRONE_ORNAMENT = 14041, // 0x000036D9
    DRONE_SLEEPY_ORNAMENT = 14042, // 0x000036DA
    STRIPES_RED_ORNAMENT = 14043, // 0x000036DB
    STRIPES_PINK_ORNAMENT = 14044, // 0x000036DC
    STRIPES_BLUE_ORNAMENT = 14045, // 0x000036DD
    STRIPES_TEAL_ORNAMENT = 14046, // 0x000036DE
    SUNNY_ORNAMENT = 14047, // 0x000036DF
    WILDFLOWER_ORNAMENT = 14048, // 0x000036E0
    FIREFLOWER_ORNAMENT = 14049, // 0x000036E1
    TARR_LANTERN_ORNAMENT = 14050, // 0x000036E2
    TWINKLE_ORNAMENT = 14051, // 0x000036E3
    SLIME_MOON_ORNAMENT = 14052, // 0x000036E4
    DUCKY_ORNAMENT = 14053, // 0x000036E5
    STEGO_ORNAMENT = 14054, // 0x000036E6
    BUZZY_ORNAMENT = 14055, // 0x000036E7
    IMPOSTER_TABBY_ORNAMENT = 14056, // 0x000036E8
    TREEFOX_ORNAMENT = 14057, // 0x000036E9
    PARTY_GORDO = 15000, // 0x00003A98
    CRATE_PARTY_01 = 15100, // 0x00003AFC
    GLITCH_SLIME = 16000, // 0x00003E80
    GLITCH_DEBUG_SPRAY_LIQUID = 16001, // 0x00003E81
    GLITCH_BUG_REPORT = 16002, // 0x00003E82
    GLITCH_TARR_SLIME = 16003, // 0x00003E83
    GLITCH_TARR_PORTAL = 16004, // 0x00003E84
    ECHO_NOTE_01 = 17000, // 0x00004268
    ECHO_NOTE_02 = 17001, // 0x00004269
    ECHO_NOTE_03 = 17002, // 0x0000426A
    ECHO_NOTE_04 = 17003, // 0x0000426B
    ECHO_NOTE_05 = 17004, // 0x0000426C
    ECHO_NOTE_06 = 17005, // 0x0000426D
    ECHO_NOTE_07 = 17006, // 0x0000426E
    ECHO_NOTE_08 = 17007, // 0x0000426F
    ECHO_NOTE_09 = 17008, // 0x00004270
    ECHO_NOTE_10 = 17009, // 0x00004271
    ECHO_NOTE_11 = 17010, // 0x00004272
    ECHO_NOTE_12 = 17011, // 0x00004273
    ECHO_NOTE_13 = 17012, // 0x00004274
  }

  public class IdComparer : IEqualityComparer<Id>
  {
    public bool Equals(Id id1, Id id2) => id1 == id2;

    public int GetHashCode(Id id) => (int) id;
  }

  public delegate void OnDestroyListener(Identifiable obj);
}
