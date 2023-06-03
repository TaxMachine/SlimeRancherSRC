// Decompiled with JetBrains decompiler
// Type: Gadget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class Gadget : MonoBehaviour
{
  public static IdComparer idComparer = new IdComparer();
  public static List<Id> ALL_FASHIONS = new List<Id>()
  {
    Id.FASHION_POD_HANDLEBAR,
    Id.FASHION_POD_SHADY,
    Id.FASHION_POD_CLIP_ON,
    Id.FASHION_POD_GOOGLY,
    Id.FASHION_POD_SERIOUS,
    Id.FASHION_POD_SMART,
    Id.FASHION_POD_DANDY,
    Id.FASHION_POD_CUTE,
    Id.FASHION_POD_ROYAL,
    Id.FASHION_POD_PARTY_GLASSES,
    Id.FASHION_POD_SCUBA,
    Id.FASHION_POD_REMOVER
  };
  public static HashSet<Id> EXTRACTOR_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> TELEPORTER_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> WARP_DEPOT_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> LAMP_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> MISC_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> FASHION_POD_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> SNARE_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> ECHO_NET_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> DRONE_CLASS = new HashSet<Id>(idComparer);
  public static HashSet<Id> DECO_CLASS = new HashSet<Id>(idComparer);
  public Id id;
  protected Transform rotationTransform;

  public static void RegisterFashion(Id id)
  {
    ALL_FASHIONS.RemoveAll(it => it == id);
    ALL_FASHIONS.Add(id);
  }

  public virtual void Awake() => rotationTransform = transform;

  static Gadget()
  {
    foreach (Id id in Enum.GetValues(typeof (Id)))
    {
      if (id >= Id.EXTRACTOR_DRILL_NOVICE && id < Id.TELEPORTER_PINK)
        EXTRACTOR_CLASS.Add(id);
      else if (id >= Id.TELEPORTER_PINK && id < Id.WARP_DEPOT_PINK)
        TELEPORTER_CLASS.Add(id);
      else if (id >= Id.WARP_DEPOT_PINK && id < Id.MARKET_LINK)
        WARP_DEPOT_CLASS.Add(id);
      else if (id >= Id.MARKET_LINK && id < Id.ECHO_NET)
        MISC_CLASS.Add(id);
      else if (id >= Id.ECHO_NET && id < Id.LAMP_PINK)
        ECHO_NET_CLASS.Add(id);
      else if (id >= Id.LAMP_PINK && id < Id.FASHION_POD_HANDLEBAR)
        LAMP_CLASS.Add(id);
      else if (id >= Id.FASHION_POD_HANDLEBAR && id < Id.GORDO_SNARE_NOVICE)
        FASHION_POD_CLASS.Add(id);
      else if (id >= Id.GORDO_SNARE_NOVICE && id < Id.SPONGE_TREE)
        SNARE_CLASS.Add(id);
      else if (id >= Id.SPONGE_TREE && id < Id.DRONE)
        DECO_CLASS.Add(id);
      else if (id >= Id.DRONE && id <= Id.DRONE_ADVANCED)
        DRONE_CLASS.Add(id);
    }
  }

  public static bool IsExtractor(Id gadgetId) => EXTRACTOR_CLASS.Contains(gadgetId);

  public static bool IsTeleporter(Id gadgetId) => TELEPORTER_CLASS.Contains(gadgetId);

  public static bool IsWarpDepot(Id gadgetId) => WARP_DEPOT_CLASS.Contains(gadgetId);

  public static bool IsMisc(Id gadgetId) => MISC_CLASS.Contains(gadgetId);

  public static bool IsEchoNet(Id gadgetId) => ECHO_NET_CLASS.Contains(gadgetId);

  public static bool IsDrone(Id gadgetId) => DRONE_CLASS.Contains(gadgetId);

  public static bool IsLamp(Id gadgetId) => LAMP_CLASS.Contains(gadgetId);

  public static bool IsFashionPod(Id gadgetId) => FASHION_POD_CLASS.Contains(gadgetId);

  public static bool IsSnare(Id gadgetId) => SNARE_CLASS.Contains(gadgetId);

  public static bool IsDeco(Id gadgetId) => DECO_CLASS.Contains(gadgetId);

  public bool DestroysLinkedPairOnRemoval()
  {
    LinkDestroyer componentInChildren = GetComponentInChildren<LinkDestroyer>();
    return componentInChildren != null && componentInChildren.ShouldDestroyPair();
  }

  public bool DestroysOnRemoval() => SRSingleton<GameContext>.Instance.LookupDirector.GetGadgetDefinition(id).destroyOnRemoval;

  public static bool IsLinkDestroyerType(Id id)
  {
    string str = id.ToString();
    return str.StartsWith("TELEPORTER_") || str.StartsWith("WARP_DEPOT_");
  }

  public void DestroyGadget()
  {
    GadgetSite componentInParent = GetComponentInParent<GadgetSite>();
    if (!(componentInParent != null))
      return;
    componentInParent.DestroyAttached();
  }

  public void AddRotation(float adjustment) => SetRotation(GetRotation() + adjustment);

  public void SetRotation(float rotation) => rotationTransform.localRotation = Quaternion.Euler(0.0f, rotation, 0.0f);

  public float GetRotation() => rotationTransform.localRotation.eulerAngles.y;

  public virtual void OnUserDestroyed()
  {
  }

  public static string GetName(Id id, bool reportMissing = true)
  {
    string resourceString = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("pedia").GetResourceString(string.Format("m.gadget.name.{0}", Enum.GetName(typeof (Id), id).ToLowerInvariant()));
    if (reportMissing && resourceString == null)
      Log.Warning("Missing gadget translation.", nameof (id), id);
    return resourceString;
  }

  public enum Id
  {
    NONE = 0,
    EXTRACTOR_DRILL_NOVICE = 100, // 0x00000064
    EXTRACTOR_DRILL_ADVANCED = 101, // 0x00000065
    EXTRACTOR_DRILL_MASTER = 102, // 0x00000066
    EXTRACTOR_DRILL_TITAN = 103, // 0x00000067
    EXTRACTOR_PUMP_NOVICE = 110, // 0x0000006E
    EXTRACTOR_PUMP_ADVANCED = 111, // 0x0000006F
    EXTRACTOR_PUMP_MASTER = 112, // 0x00000070
    EXTRACTOR_PUMP_ABYSSAL = 113, // 0x00000071
    EXTRACTOR_APIARY_NOVICE = 120, // 0x00000078
    EXTRACTOR_APIARY_ADVANCED = 121, // 0x00000079
    EXTRACTOR_APIARY_MASTER = 122, // 0x0000007A
    EXTRACTOR_APIARY_ROYAL = 123, // 0x0000007B
    TELEPORTER_PINK = 200, // 0x000000C8
    TELEPORTER_BLUE = 201, // 0x000000C9
    TELEPORTER_VIOLET = 202, // 0x000000CA
    TELEPORTER_GREY = 203, // 0x000000CB
    TELEPORTER_GREEN = 204, // 0x000000CC
    TELEPORTER_RED = 205, // 0x000000CD
    TELEPORTER_AMBER = 206, // 0x000000CE
    TELEPORTER_GOLD = 207, // 0x000000CF
    TELEPORTER_BERRY = 208, // 0x000000D0
    TELEPORTER_COCOA = 209, // 0x000000D1
    TELEPORTER_BUTTERSCOTCH = 210, // 0x000000D2
    WARP_DEPOT_PINK = 300, // 0x0000012C
    WARP_DEPOT_BLUE = 301, // 0x0000012D
    WARP_DEPOT_VIOLET = 302, // 0x0000012E
    WARP_DEPOT_GREY = 303, // 0x0000012F
    WARP_DEPOT_GREEN = 304, // 0x00000130
    WARP_DEPOT_RED = 305, // 0x00000131
    WARP_DEPOT_AMBER = 306, // 0x00000132
    WARP_DEPOT_GOLD = 307, // 0x00000133
    WARP_DEPOT_BERRY = 308, // 0x00000134
    WARP_DEPOT_COCOA = 309, // 0x00000135
    WARP_DEPOT_BUTTERSCOTCH = 310, // 0x00000136
    MARKET_LINK = 390, // 0x00000186
    MED_STATION = 400, // 0x00000190
    RAPID_MED_STATION = 401, // 0x00000191
    HYDRO_TURRET = 410, // 0x0000019A
    SUPER_HYDRO_TURRET = 411, // 0x0000019B
    HYDRO_SHOWER = 420, // 0x000001A4
    SUPER_HYDRO_SHOWER = 421, // 0x000001A5
    TAMING_BELL = 430, // 0x000001AE
    ELITE_TAMING_BELL = 431, // 0x000001AF
    SPRING_PAD = 460, // 0x000001CC
    POTTED_TACTUS = 461, // 0x000001CD
    REFINERY_LINK = 462, // 0x000001CE
    SLIME_HOOP = 463, // 0x000001CF
    SLIME_STAGE = 464, // 0x000001D0
    ECHO_NET = 465, // 0x000001D1
    LAMP_PINK = 500, // 0x000001F4
    LAMP_BLUE = 501, // 0x000001F5
    LAMP_VIOLET = 502, // 0x000001F6
    LAMP_GREY = 503, // 0x000001F7
    LAMP_GREEN = 504, // 0x000001F8
    LAMP_RED = 505, // 0x000001F9
    LAMP_AMBER = 506, // 0x000001FA
    LAMP_GOLD = 507, // 0x000001FB
    LAMP_BERRY = 508, // 0x000001FC
    LAMP_COCOA = 509, // 0x000001FD
    LAMP_BUTTERSCOTCH = 510, // 0x000001FE
    FASHION_POD_HANDLEBAR = 600, // 0x00000258
    FASHION_POD_SHADY = 601, // 0x00000259
    FASHION_POD_CLIP_ON = 602, // 0x0000025A
    FASHION_POD_GOOGLY = 603, // 0x0000025B
    FASHION_POD_SERIOUS = 604, // 0x0000025C
    FASHION_POD_SMART = 605, // 0x0000025D
    FASHION_POD_CUTE = 606, // 0x0000025E
    FASHION_POD_ROYAL = 607, // 0x0000025F
    FASHION_POD_DANDY = 608, // 0x00000260
    FASHION_POD_PIRATEY = 609, // 0x00000261
    FASHION_POD_HEROIC = 610, // 0x00000262
    FASHION_POD_SCIFI = 611, // 0x00000263
    FASHION_POD_PARTY_GLASSES = 612, // 0x00000264
    FASHION_POD_SCUBA = 613, // 0x00000265
    FASHION_POD_REMOVER = 699, // 0x000002BB
    GORDO_SNARE_NOVICE = 700, // 0x000002BC
    GORDO_SNARE_ADVANCED = 701, // 0x000002BD
    GORDO_SNARE_MASTER = 702, // 0x000002BE
    SPONGE_TREE = 11000, // 0x00002AF8
    SPONGE_SHRUB = 11001, // 0x00002AF9
    PINK_CORAL_COLUMNS = 11002, // 0x00002AFA
    CORAL_GRASS_PATCH = 11003, // 0x00002AFB
    MOSSY_TREE = 12000, // 0x00002EE0
    MOSSY_TREE_STUMP = 12001, // 0x00002EE1
    GLOW_CONES = 12002, // 0x00002EE2
    WILDFLOWER_PATCH = 12003, // 0x00002EE3
    JUMBO_SHROOM = 12004, // 0x00002EE4
    MINTY_GRASS_PATCH = 13000, // 0x000032C8
    BLUE_CORAL_COLUMNS = 13001, // 0x000032C9
    HEXIUM_FORMATION = 13002, // 0x000032CA
    CRYSTAL_CLUSTER = 13003, // 0x000032CB
    FIREFLOWER_PATCH = 13004, // 0x000032CC
    SUNBURST_TREE = 14000, // 0x000036B0
    VERDANT_GRASS_PATCH = 14001, // 0x000036B1
    STAR_FLOWER_PATCH = 14002, // 0x000036B2
    RUINED_PILLAR = 14003, // 0x000036B3
    GLOW_STICKS = 14004, // 0x000036B4
    CRYSTAL_SCONCE = 14005, // 0x000036B5
    FIERY_GLASS_SCULPTURE = 15000, // 0x00003A98
    THUNDERING_GLASS_SCULPTURE = 15001, // 0x00003A99
    TOWERING_GLASS_SCULPTURE = 15002, // 0x00003A9A
    PALM_TREE = 15003, // 0x00003A9B
    PALM_SPROUT = 15004, // 0x00003A9C
    COIL_GRASS = 15005, // 0x00003A9D
    RUINED_DESERT_COLUMN = 15006, // 0x00003A9E
    RUINED_DESERT_BLOCKS = 15007, // 0x00003A9F
    DESERT_DECO_9 = 15008, // 0x00003AA0
    WILDS_ROCKS_1 = 16000, // 0x00003E80
    WILDS_ROCKS_2 = 16001, // 0x00003E81
    WILDS_ROCKS_3 = 16002, // 0x00003E82
    WILDS_TREE = 16003, // 0x00003E83
    WILDS_CORAL_COLUMN = 16004, // 0x00003E84
    WILDS_GRASS_PATCH = 16005, // 0x00003E85
    WILDS_FLOWER_PATCH = 16006, // 0x00003E86
    MAGNETICORE_ARRAY_SMALL = 17000, // 0x00004268
    MAGNETICORE_ARRAY_TALL = 17001, // 0x00004269
    MAGNETICORE_ARRAY_STURDY = 17002, // 0x0000426A
    MAGNETICORE_ARRAY_ORNATE = 17003, // 0x0000426B
    NIMBLE_GRASS_PATCH = 17004, // 0x0000426C
    NIMBLE_NEEDLE_TREE = 17005, // 0x0000426D
    DECO_BATTERY_TOWER = 17100, // 0x000042CC
    DECO_DIGI_PANEL = 17101, // 0x000042CD
    DECO_DIGI_SHRUB = 17102, // 0x000042CE
    DECO_DIGI_TREE = 17103, // 0x000042CF
    DECO_FIELD_KIT = 17104, // 0x000042D0
    DECO_SUPPLY_DROP = 17105, // 0x000042D1
    DRONE = 18000, // 0x00004650
    DRONE_ADVANCED = 18001, // 0x00004651
    CHICKEN_CLONER = 19000, // 0x00004A38
    PORTABLE_WATER_TAP = 19001, // 0x00004A39
    PORTABLE_SLIME_BAIT_FRUIT = 19002, // 0x00004A3A
    PORTABLE_SCARECROW = 19003, // 0x00004A3B
    DASH_PAD = 19005, // 0x00004A3D
    PORTABLE_SLIME_BAIT_VEGGIE = 19006, // 0x00004A3E
    PORTABLE_SLIME_BAIT_MEAT = 19007, // 0x00004A3F
  }

  public class IdComparer : IEqualityComparer<Id>
  {
    public bool Equals(Id id1, Id id2) => id1 == id2;

    public int GetHashCode(Id obj) => (int) obj;
  }

  public interface LinkDestroyer
  {
    bool ShouldDestroyPair();

    LinkDestroyer GetLinked();
  }
}
