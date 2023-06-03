// Decompiled with JetBrains decompiler
// Type: EndGameUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUI : BaseUI
{
  public TMP_Text currencyText;
  public TMP_Text deathsText;
  public TMP_Text spentText;
  public Button takeScreenshotButton;
  public EndGameUIPlortLine[] plortLines;
  public TMP_Text noPlortsText;

  public override void Awake()
  {
    base.Awake();
    takeScreenshotButton.gameObject.SetActive(true);
    currencyText.text = SRSingleton<SceneContext>.Instance.PlayerState.GetCurrency().ToString();
    AchievementsDirector achievementsDirector = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    deathsText.text = achievementsDirector.GetGameIntStat(AchievementsDirector.GameIntStat.DEATHS).ToString();
    spentText.text = achievementsDirector.GetGameIntStat(AchievementsDirector.GameIntStat.CURRENCY_SPENT).ToString();
    Dictionary<Identifiable.Id, int> gameIdDictStat = achievementsDirector.GetGameIdDictStat(AchievementsDirector.GameIdDictStat.PLORT_TYPES_SOLD);
    List<PlortEntry> plortEntryList = new List<PlortEntry>();
    foreach (KeyValuePair<Identifiable.Id, int> keyValuePair in gameIdDictStat)
      plortEntryList.Add(new PlortEntry(keyValuePair.Key, keyValuePair.Value));
    plortEntryList.Sort();
    plortEntryList.Reverse();
    for (int index = 0; index < plortLines.Length; ++index)
    {
      if (plortEntryList.Count > index)
        plortLines[index].Init(plortEntryList[index].id, plortEntryList[index].count, plortEntryList[index].price);
      else
        plortLines[index].gameObject.SetActive(false);
    }
    noPlortsText.gameObject.SetActive(plortEntryList.Count == 0);
  }

  public void OnScreenshot() => SRSingleton<GameContext>.Instance.TakeScreenshot();

  public void OnOK()
  {
    if (!SRSingleton<GameContext>.Instance.AutoSaveDirector.SaveAllNow())
      return;
    SRSingleton<SceneContext>.Instance.OnSessionEnded();
    SceneManager.LoadScene("MainMenu");
  }

  protected override bool Closeable() => false;

  private class PlortEntry : IComparable<PlortEntry>
  {
    public Identifiable.Id id;
    public int count;
    public int price;

    public PlortEntry(Identifiable.Id id, int count)
    {
      this.id = id;
      this.count = count;
      price = count * SRSingleton<SceneContext>.Instance.EconomyDirector.GetCurrValue(id).Value;
    }

    public int CompareTo(PlortEntry that) => price.CompareTo(that.price);
  }
}
