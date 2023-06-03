// Decompiled with JetBrains decompiler
// Type: MarketUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class MarketUI : MonoBehaviour
{
  public PlortEntry[] plorts;
  public GameObject pricesPanelGroup;
  public PricesPanelEntry[] pricesPanels;
  public GameObject priceEntryPrefab;
  public GameObject priceEntryEmptyPrefab;
  public GameObject shutdownPanel;
  public GameObject[] toShutdown;
  public Sprite upImg;
  public Sprite downImg;
  public Sprite unchImg;
  public Sprite bonusCompleteImg;
  private EconomyDirector econDir;
  private ProgressDirector progressDir;
  private LookupDirector lookupDir;
  private Dictionary<PlortEntry, GameObject> amountMap;

  public void Awake()
  {
    amountMap = new Dictionary<PlortEntry, GameObject>();
    econDir = SRSingleton<SceneContext>.Instance.EconomyDirector;
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
  }

  public void Start()
  {
    int index = 0;
    int num = 0;
    PlayerState.GameMode currGameMode = SRSingleton<SceneContext>.Instance.GameModel.currGameMode;
    foreach (PlortEntry plort in plorts)
    {
      if (plort.id != Identifiable.Id.SABER_PLORT || currGameMode != PlayerState.GameMode.TIME_LIMIT_V2)
      {
        GameObject gameObject = Instantiate(priceEntryPrefab);
        gameObject.GetComponent<PriceEntry>().itemIcon.sprite = lookupDir.GetIcon(plort.id);
        amountMap[plort] = gameObject;
        gameObject.transform.SetParent(pricesPanels[index].panel.transform, false);
        ++num;
        if (num >= pricesPanels[index].entryCount)
        {
          ++index;
          num = 0;
        }
      }
    }
    while (index < pricesPanels.Length)
    {
      Instantiate(priceEntryEmptyPrefab).transform.SetParent(pricesPanels[index].panel.transform, false);
      ++num;
      if (num >= pricesPanels[index].entryCount)
      {
        ++index;
        num = 0;
      }
    }
    EconUpdate();
    econDir.didUpdateDelegate += EconUpdate;
    econDir.onRegisterSold += PlortCountUpdate;
    progressDir.onProgressChanged += EconUpdate;
  }

  public void OnDestroy()
  {
    econDir.didUpdateDelegate -= EconUpdate;
    econDir.onRegisterSold -= PlortCountUpdate;
    progressDir.onProgressChanged -= EconUpdate;
  }

  private void EconUpdate()
  {
    foreach (KeyValuePair<PlortEntry, GameObject> amount in amountMap)
    {
      PriceEntry component = amount.Value.GetComponent<PriceEntry>();
      component.amountText.text = econDir.GetCurrValue(amount.Key.id).Value.ToString();
      PlortCountUpdate(amount.Key, component);
    }
  }

  public void Update()
  {
    bool flag = econDir.IsMarketShutdown();
    pricesPanelGroup.SetActive(!flag);
    shutdownPanel.SetActive(flag);
    foreach (GameObject gameObject in toShutdown)
      gameObject.SetActive(!flag);
  }

  private void PlortCountUpdate(Identifiable.Id id)
  {
    foreach (KeyValuePair<PlortEntry, GameObject> amount in amountMap)
    {
      if (amount.Key.id == id)
      {
        PlortCountUpdate(amount.Key, amount.Value.GetComponent<PriceEntry>());
        break;
      }
    }
  }

  private void PlortCountUpdate(PlortEntry plort, PriceEntry price)
  {
    int collected = 0;
    SRSingleton<SceneContext>.Instance.AchievementsDirector.GetGameIdDictStat(AchievementsDirector.GameIdDictStat.PLORT_TYPES_SOLD).TryGetValue(plort.id, out collected);
    price.bonusFill.minValue = 0.0f;
    price.bonusFill.currValue = collected;
    price.bonusFill.maxValue = 25f;
    price.bonusFill.enabled = SRSingleton<SceneContext>.Instance.GameModel.currGameMode == PlayerState.GameMode.TIME_LIMIT_V2;
    int change = econDir.GetChangeInValue(plort.id) ?? 0;
    price.changeIcon.sprite = GetChangeIcon(plort.id, change, collected);
    price.changeIcon.enabled = price.changeIcon.sprite != null;
    price.changeAmountText.text = GetChangeText(plort.id, change);
    float a = IsPlortUnlocked(plort, collected) ? 1f : 0.5f;
    price.amountText.color = AdjustAlpha(price.amountText.color, a);
    price.changeAmountText.color = AdjustAlpha(price.changeAmountText.color, a);
    price.changeIcon.color = AdjustAlpha(price.changeIcon.color, a);
    price.coinIcon.color = AdjustAlpha(price.coinIcon.color, a);
    price.itemIcon.color = AdjustAlpha(price.itemIcon.color, a);
  }

  private Color AdjustAlpha(Color c, float a)
  {
    c.a = a;
    return c;
  }

  private Sprite GetChangeIcon(Identifiable.Id id, int change, int collected)
  {
    if (SRSingleton<SceneContext>.Instance.GameModel.currGameMode == PlayerState.GameMode.TIME_LIMIT_V2)
      return !GameModeSettings.PlortBonusReached(collected) ? null : bonusCompleteImg;
    if (!SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().plortMarketDynamic)
      return null;
    if (change == 0)
      return unchImg;
    return change >= 0 ? upImg : downImg;
  }

  private string GetChangeText(Identifiable.Id id, int change) => SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().plortMarketDynamic ? Math.Abs(change).ToString() : string.Empty;

  private bool IsPlortUnlocked(PlortEntry plort, int collected)
  {
    if (collected > 0 || SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().assumeExperiencedUser || plort.toUnlock.Length == 0)
      return true;
    foreach (ProgressDirector.ProgressType type in plort.toUnlock)
    {
      if (progressDir.HasProgress(type))
        return true;
    }
    return false;
  }

  [Serializable]
  public class PlortEntry
  {
    public Identifiable.Id id;
    public ProgressDirector.ProgressType[] toUnlock;
  }

  [Serializable]
  public class PricesPanelEntry
  {
    public GameObject panel;
    public int entryCount;
  }
}
