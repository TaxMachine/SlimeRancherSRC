// Decompiled with JetBrains decompiler
// Type: ModDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ModDirector : MonoBehaviour
{
  private ModsListener modsListeners;
  [Tooltip("Chance of a tarr slime spawning in place of a normal slime at night during the mod.")]
  public float nightTarrChance = 0.05f;
  [Tooltip("Chance of a slime producing a random plort instead of its normal one during the mod.")]
  public float randomPlortChance = 0.1f;
  [Tooltip("Factor by which we increase the number of slimes spawned during the mod.")]
  public float increasedSlimeSpawnsFactor = 1.3f;
  [Tooltip("Factor by which slimes' hunger increases during the mod.")]
  public float hungerFactor = 2f;
  [Tooltip("Any mods we should activate immediately on startup, for testing")]
  public Mod[] initMods;
  private List<Mod> activeMods = new List<Mod>();
  private TimeDirector timeDir;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    foreach (Mod initMod in initMods)
      activeMods.Add(initMod);
  }

  public void InitForLevel() => NotifyModsChanged();

  public void ActivateMod(Mod mod)
  {
    if (activeMods.Contains(mod))
      return;
    activeMods.Add(mod);
    NotifyModsChanged();
  }

  public void DeactivateMod(Mod mod)
  {
    if (!activeMods.Contains(mod))
      return;
    activeMods.Remove(mod);
    NotifyModsChanged();
  }

  public bool IsModActive(Mod mod) => false;

  public void RegisterModsListener(ModsListener listener)
  {
    modsListeners += listener;
    listener();
  }

  public void UnregisterModsListener(ModsListener listener) => modsListeners -= listener;

  public float SlimeCountFactor() => 1f;

  public float SlimeHungerFactor() => 1f;

  public float ChanceOfTarrSpawn() => 0.0f;

  private bool IsNight()
  {
    float num = timeDir.CurrDayFraction();
    return num < 0.25 || num > 0.75;
  }

  public float ChanceRandomPlort() => 0.0f;

  public bool PlortsUnstable() => false;

  public float PlortPriceFactor(Identifiable.Id plortId) => 1f;

  public bool VampiricChickens() => false;

  private void NotifyModsChanged()
  {
    if (modsListeners == null)
      return;
    modsListeners();
  }

  public delegate void ModsListener();

  public enum Mod
  {
    VAMPIRIC_CHICKENS,
    NIGHT_TARR_SLIMES,
    RANDOM_PLORTS,
    INCREASED_SLIME_SPAWNS,
    SLIME_HUNGER,
    UNSTABLE_PLORTS,
  }
}
