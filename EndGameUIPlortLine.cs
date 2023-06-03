// Decompiled with JetBrains decompiler
// Type: EndGameUIPlortLine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUIPlortLine : MonoBehaviour
{
  public Image icon;
  public TMP_Text countText;
  public TMP_Text currencyText;

  public void Init(Identifiable.Id id, int amount, int price)
  {
    icon.sprite = SRSingleton<GameContext>.Instance.LookupDirector.GetIcon(id);
    countText.text = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui").Get("m.plort_amount", amount);
    currencyText.text = price.ToString();
  }
}
