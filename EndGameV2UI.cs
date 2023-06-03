// Decompiled with JetBrains decompiler
// Type: EndGameV2UI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameV2UI : EndGameUI
{
  [Tooltip("Text displaying the plort bonus percentage.")]
  public TMP_Text plortBonusText;
  [Tooltip("Text displaying the score.")]
  public TMP_Text scoreText;
  [Tooltip("Parent GameObject containing the plort bonus images.")]
  public GameObject plortBonusLines;
  [Tooltip("Text displayed if there are no plort bonuses.")]
  public TMP_Text plortBonusNoneText;

  public override void Awake()
  {
    base.Awake();
    SRSingleton<GameContext>.Instance.MusicDirector.SetRushCreditsMode(true);
    AchievementsDirector achievementsDirector = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    Dictionary<Identifiable.Id, int> gameIdDictStat = achievementsDirector.GetGameIdDictStat(AchievementsDirector.GameIdDictStat.PLORT_TYPES_SOLD);
    InitPlortBonuses(gameIdDictStat);
    int currency = SRSingleton<SceneContext>.Instance.PlayerState.GetCurrency();
    float scoreMultiplier = GetScoreMultiplier(gameIdDictStat);
    int val = Mathf.CeilToInt(currency * scoreMultiplier);
    currencyText.text = string.Format("{0}", currency);
    plortBonusText.text = uiBundle.Get("m.percentage", Mathf.Round((float) ((scoreMultiplier - 1.0) * 100.0)));
    scoreText.text = string.Format("{0}", val);
    AnalyticsUtil.CustomEvent("TimeLimitV2GameEnd", new Dictionary<string, object>()
    {
      {
        "currency",
        currency
      },
      {
        "multiplier",
        scoreMultiplier
      },
      {
        "score",
        val
      }
    });
    achievementsDirector.MaybeUpdateMaxStat(AchievementsDirector.IntStat.TIME_LIMIT_V2_CURRENCY, val);
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!(SRSingleton<GameContext>.Instance != null))
      return;
    SRSingleton<GameContext>.Instance.MusicDirector.SetRushCreditsMode(false);
  }

  private void InitPlortBonuses(
    IEnumerable<KeyValuePair<Identifiable.Id, int>> plorts)
  {
    Image[] componentsInChildren = plortBonusLines.GetComponentsInChildren<Image>(true);
    List<Identifiable.Id> idList = new List<Identifiable.Id>();
    foreach (KeyValuePair<Identifiable.Id, int> plort in plorts)
    {
      if (plort.Value >= 25)
        idList.Add(plort.Key);
    }
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      Image image = componentsInChildren[index];
      image.gameObject.SetActive(index < idList.Count);
      if (image.gameObject.activeSelf)
        image.sprite = SRSingleton<GameContext>.Instance.LookupDirector.GetIcon(idList[index]);
    }
    plortBonusNoneText.gameObject.SetActive(idList.Count == 0);
  }

  private static float GetScoreMultiplier(
    IEnumerable<KeyValuePair<Identifiable.Id, int>> plorts)
  {
    float scoreMultiplier = 1f;
    foreach (KeyValuePair<Identifiable.Id, int> plort in plorts)
      scoreMultiplier += plort.Value >= 25 ? GameModeSettings.GetScoreMultiplier(plort.Key) : 0.0f;
    return scoreMultiplier;
  }
}
