// Decompiled with JetBrains decompiler
// Type: ExchangeProgressItemEntryUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class ExchangeProgressItemEntryUI : MonoBehaviour
{
  public Image icon;
  public Text progressText;
  private LookupDirector lookupDir;
  private ExchangeDirector exchangeDir;
  private MessageBundle uiBundle;

  public void Awake()
  {
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    exchangeDir = SRSingleton<SceneContext>.Instance.ExchangeDirector;
    uiBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui");
  }

  public void SetEntry(ExchangeDirector.RequestedItemEntry entry)
  {
    if (entry == null)
    {
      gameObject.SetActive(false);
    }
    else
    {
      gameObject.SetActive(true);
      icon.sprite = entry.specReward == ExchangeDirector.NonIdentReward.NONE ? lookupDir.GetIcon(entry.id) : exchangeDir.GetSpecRewardIcon(entry.specReward);
      progressText.text = uiBundle.Get("l.exchange_progress", entry.progress, entry.count);
    }
  }
}
