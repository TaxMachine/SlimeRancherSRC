// Decompiled with JetBrains decompiler
// Type: DLCDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DLCDirector
{
  public static HashSet<string> SECRET_STYLE_TREASURE_PODS = new HashSet<string>()
  {
    "pod1067506426",
    "pod0573382639",
    "pod0528457170",
    "pod0209361044",
    "pod2089428629",
    "pod0797820130",
    "pod0793461898",
    "pod1736587205",
    "pod1843001748",
    "pod1327729579",
    "pod1761631840",
    "pod0942230423",
    "pod1507800227",
    "pod0403498756",
    "pod0732526653",
    "pod1897070320",
    "pod1003003618",
    "pod0084486208",
    "pod0463402699",
    "pod1284546475"
  };
  private DLCProvider provider;
  private Dictionary<DLCPackage.Id, PackageLoader> packageLoadersDict = new Dictionary<DLCPackage.Id, PackageLoader>(DLCPackage.IdComparer.Instance);

  public event OnPackageInstalledDelegate onPackageInstalled = _param1 => { };

  public IEnumerable<DLCPackage.Id> Installed => GetSupportedPackages().Where(package => GetPackageState(package) >= DLCPackage.State.INSTALLED);

  public bool SetProvider(DLCProvider provider)
  {
    if (this.provider != null)
    {
      Log.Error("Attempting to replace existing DLC provider.");
      return false;
    }
    this.provider = provider;
    return true;
  }

  public IEnumerable<DLCPackage.Id> GetSupportedPackages()
  {
    if (provider != null)
    {
      foreach (DLCPackage.Id supportedPackage in provider.GetSupported())
        yield return supportedPackage;
    }
  }

  public bool HasReached(DLCPackage.Id id, DLCPackage.State state) => provider != null && provider.GetSupported().Contains(id) && provider.GetState(id) >= state;

  public DLCPackage.State GetPackageState(DLCPackage.Id id) => provider == null ? DLCPackage.State.UNDEFINED : provider.GetState(id);

  public void ShowPackageInStore(DLCPackage.Id id)
  {
    if (provider == null)
      return;
    provider.ShowInStore(id);
  }

  public bool IsPackageInstalledAndEnabled(DLCPackage.Id id) => SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().enableDLC && HasReached(id, DLCPackage.State.INSTALLED);

  public void InitForLevel()
  {
    if (provider == null || Levels.isSpecial())
      return;
    RegisterPackages();
  }

  public IEnumerator RefreshPackagesAsync()
  {
    if (provider != null)
      yield return provider.Refresh();
  }

  public IEnumerator RegisterPackagesAsync()
  {
    yield return RefreshPackagesAsync();
    RegisterPackages();
  }

  public void RegisterPackages()
  {
    foreach (DLCPackage.Id id in Installed)
    {
      foreach (DLCContentMetadata dlcContentMetadata in GetPackageLoader(id).content)
        dlcContentMetadata.Register();
      onPackageInstalled(id);
    }
  }

  public IEnumerable<DLCPackageMetadata> LoadPackageMetadatas() => GetSupportedPackages().Select(id => GetPackageLoader(id).package);

  private PackageLoader GetPackageLoader(DLCPackage.Id id) => packageLoadersDict.ContainsKey(id) ? packageLoadersDict[id] : (packageLoadersDict[id] = new PackageLoader(id));

  public void Purge(GameV12 game)
  {
    DLCPackage.Id[] array = Enum.GetValues(typeof (DLCPackage.Id)).Cast<DLCPackage.Id>().Where(p => GetPackageState(p) < DLCPackage.State.INSTALLED && PurgePackage(game, p)).ToArray();
    if (array.Any())
      throw new DLCPurgedException(array);
  }

  private bool PurgePackage(GameV12 game, DLCPackage.Id package)
  {
    int num = 0;
    switch (package)
    {
      case DLCPackage.Id.PLAYSET_PIRATE:
        num = num + PurgeChromaPack(game, RanchDirector.Palette.PALETTE27) + PurgeFashion(game, Gadget.Id.FASHION_POD_PIRATEY, Identifiable.Id.PIRATEY_FASHION) + PurgeToy(game, Identifiable.Id.TREASURE_CHEST_TOY);
        break;
      case DLCPackage.Id.PLAYSET_HEROIC:
        num = num + PurgeChromaPack(game, RanchDirector.Palette.PALETTE28) + PurgeFashion(game, Gadget.Id.FASHION_POD_HEROIC, Identifiable.Id.HEROIC_FASHION) + PurgeToy(game, Identifiable.Id.BOP_GOBLIN_TOY);
        break;
      case DLCPackage.Id.PLAYSET_SCIFI:
        num = num + PurgeChromaPack(game, RanchDirector.Palette.PALETTE29) + PurgeFashion(game, Gadget.Id.FASHION_POD_SCIFI, Identifiable.Id.SCIFI_FASHION) + PurgeToy(game, Identifiable.Id.ROBOT_TOY);
        break;
      case DLCPackage.Id.SECRET_STYLE:
        foreach (KeyValuePair<Identifiable.Id, List<SlimeAppearance.AppearanceSaveSet>> unlock in game.appearances.unlocks)
        {
          num += unlock.Value.RemoveAll(it => it == SlimeAppearance.AppearanceSaveSet.SECRET_STYLE);
          game.appearances.selections[unlock.Key] = SlimeAppearance.AppearanceSaveSet.CLASSIC;
        }
        using (Dictionary<string, TreasurePodV01>.Enumerator enumerator = game.world.treasurePods.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, TreasurePodV01> current = enumerator.Current;
            if (SECRET_STYLE_TREASURE_PODS.Contains(current.Key) && current.Value.state != TreasurePod.State.LOCKED)
            {
              current.Value.state = TreasurePod.State.LOCKED;
              ++num;
            }
          }
          break;
        }
      default:
        throw new InvalidOperationException();
    }
    return num > 0;
  }

  private int PurgeChromaPack(GameV12 game, RanchDirector.Palette palette)
  {
    int num = 0;
    foreach (RanchDirector.PaletteType key in game.ranch.palettes.Keys.ToList())
    {
      if (game.ranch.palettes[key] == palette)
      {
        game.ranch.palettes[key] = RanchDirector.Palette.DEFAULT;
        ++num;
      }
    }
    return num;
  }

  private int PurgeFashion(GameV12 game, Gadget.Id gadget, Identifiable.Id fashion)
  {
    int num = 0;
    foreach (GordoV01 gordoV01 in game.world.gordos.Values)
      num += gordoV01.fashions.RemoveAll(it => it == fashion);
    foreach (ActorDataV09 actor in game.actors)
      num += actor.fashions.RemoveAll(it => it == fashion);
    foreach (string key in game.world.placedGadgets.Keys.ToList())
    {
      if (game.world.placedGadgets[key].gadgetId == gadget)
        num += Convert.ToInt32(game.world.placedGadgets.Remove(key));
    }
    foreach (PlacedGadgetV08 placedGadgetV08 in game.world.placedGadgets.Values)
    {
      num += placedGadgetV08.fashions.RemoveAll(it => it == fashion);
      if (placedGadgetV08.drone != null)
        num += placedGadgetV08.drone.drone.fashions.RemoveAll(it => it == fashion);
    }
    return num + game.actors.RemoveAll(it => (Identifiable.Id) it.typeId == fashion) + game.player.ammo[PlayerState.AmmoMode.DEFAULT].RemoveAll(d => d.id == fashion) + game.player.blueprints.RemoveAll(it => it == gadget) + game.player.availBlueprints.RemoveAll(it => it == gadget) + Convert.ToInt32(game.player.blueprintLocks.Remove(gadget)) + Convert.ToInt32(game.player.gadgets.Remove(gadget));
  }

  private int PurgeToy(GameV12 game, Identifiable.Id toy) => game.actors.RemoveAll(it => (Identifiable.Id) it.typeId == toy);

  public delegate void OnPackageInstalledDelegate(DLCPackage.Id package);

  private class PackageLoader
  {
    public readonly DLCPackageMetadata package;
    public readonly DLCContentMetadata[] content;

    public PackageLoader(DLCPackage.Id id)
    {
      string path1 = Path.Combine("DLC", id.ToString().ToLowerInvariant());
      package = Resources.Load<DLCPackageMetadata>(Path.Combine(path1, nameof (package)));
      content = Resources.LoadAll<DLCContentMetadata>(Path.Combine(path1, "package_metadata"));
      if (package == null)
        throw new Exception(string.Format("Failed to load DLC package. [id={0}]", id));
      if (content == null)
        throw new Exception(string.Format("Failed to load DLC package contents. [id={0}]", id));
    }
  }
}
