// Decompiled with JetBrains decompiler
// Type: ExchangeItemEntryUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeItemEntryUI : MonoBehaviour
{
  public Image icon;
  public TMP_Text progressText;
  public TMP_Text nameText;
  private LookupDirector lookupDir;
  private MessageBundle uiBundle;

  public void Awake()
  {
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    uiBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui");
  }

  public void SetEntry(ExchangeDirector.ItemEntry entry)
  {
    if (entry == null)
    {
      gameObject.SetActive(false);
    }
    else
    {
      gameObject.SetActive(true);
      icon.sprite = lookupDir.GetIcon(entry.id);
      progressText.text = uiBundle.Get("l.ammo", entry.count);
      nameText.text = Identifiable.GetName(entry.id);
    }
  }
}
