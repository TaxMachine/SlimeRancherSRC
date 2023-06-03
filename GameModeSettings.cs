// Decompiled with JetBrains decompiler
// Type: GameModeSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Mode")]
public class GameModeSettings : ScriptableObject
{
  [Tooltip("Starting money")]
  public int initCurrency;
  [Tooltip("Currency penalty on death as portion of total")]
  public float pctCurrencyLostOnDeath;
  [Tooltip("Time penalty on death in hours")]
  public float hoursLostOnDeath;
  [Tooltip("Whether to have a til-dawn sleep on death")]
  public bool hoursTilDawnOnDeath;
  [Tooltip("The day on which to begin our exchange offers")]
  public int exchangeStartDay;
  [Tooltip("Whether all exchange rewards are gold plorts")]
  public bool exchangeRewardsGoldPlorts;
  [Tooltip("Whether the plort market prices incorporate noise and saturation")]
  public bool plortMarketDynamic;
  [Tooltip("The day on which we should end the game, or never if 0.")]
  public double endAtNoonDay;
  [Tooltip("The UI to use at the end of the game.")]
  public GameObject endGameUIPrefab;
  [Tooltip("Whether the game mode assumes the player has experience, suppress tutorials, etc")]
  public bool assumeExperiencedUser;
  [Tooltip("Whether our upgrades bypass their normal staggering time delays.")]
  public bool immediateUpgrades;
  [Tooltip("Whether we enable 7Z Partner rewards.")]
  public bool enablePartnerRewards = true;
  [Tooltip("Disables Hobson journal entries and Casey emails when true.")]
  public bool suppressStory;
  [Tooltip("All damage will be multiplied by this factor.")]
  public float playerDamageMultiplier = 1f;
  [Tooltip("Prevents Tarr from forming and direct attacking from ferals.")]
  public bool preventHostiles;
  [Tooltip("Enables blueprints to be discovered.")]
  public bool blueprintsEnabled = true;
  [Tooltip("Enables Ogden missions to be unlockable.")]
  public bool enableOgdenMissions = true;
  [Tooltip("Enables Mochi missions to be unlockable.")]
  public bool enableMochiMissions = true;
  [Tooltip("Enables Viktor missions to be unlockable.")]
  public bool enableViktorMissions = true;
  [Tooltip("Prefab to instantiate when a new game is loaded.")]
  public GameObject newGamePrefab;
  [Tooltip("Enables event/party gordos.")]
  public bool enableEventGordos = true;
  [Tooltip("Enables Wiggly Wonderland echo note cluster nodes.")]
  public bool enableEchoNoteGordos = true;
  [Tooltip("Enables DLC.")]
  public bool enableDLC = true;
  [Tooltip("Minimum plorts required to add a score multiplier.")]
  public const int scoreMultiplierPlortsRequired = 25;
  [Tooltip("Default score multiplier applied for each plort type deposited.")]
  public const float scoreMultiplierDefault = 0.05f;
  [Tooltip("Custom score multipliers applied for each plort type deposited.")]
  public static Dictionary<Identifiable.Id, float> scoreMultiplierMap = new Dictionary<Identifiable.Id, float>(Identifiable.idComparer)
  {
    {
      Identifiable.Id.DERVISH_PLORT,
      0.08f
    },
    {
      Identifiable.Id.FIRE_PLORT,
      0.08f
    },
    {
      Identifiable.Id.MOSAIC_PLORT,
      0.08f
    },
    {
      Identifiable.Id.QUANTUM_PLORT,
      0.08f
    },
    {
      Identifiable.Id.TANGLE_PLORT,
      0.08f
    }
  };

  public bool AllowMail() => !assumeExperiencedUser || !suppressStory;

  public double? EndTime() => endAtNoonDay > 0.0 ? new double?(86400.0 * (endAtNoonDay - 0.5)) : new double?();

  public static float GetScoreMultiplier(Identifiable.Id id) => !scoreMultiplierMap.ContainsKey(id) ? 0.05f : scoreMultiplierMap.Get(id);

  public static bool PlortBonusReached(int plortsCollected) => plortsCollected >= 25;
}
