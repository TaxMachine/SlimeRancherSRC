// Decompiled with JetBrains decompiler
// Type: PurchaseUI_LoadingText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class PurchaseUI_LoadingText : SRBehaviour
{
  private TMP_Text text;
  private string message;

  public void Awake()
  {
    text = GetRequiredComponent<TMP_Text>();
    message = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui").Get("l.loading");
  }

  public void OnEnable() => StartCoroutine(UpdateText_Coroutine());

  private IEnumerator UpdateText_Coroutine()
  {
    for (int maxLoops = 0; maxLoops < int.MaxValue; ++maxLoops)
    {
      for (int dotCount = 0; dotCount <= 3; ++dotCount)
      {
        text.text = message;
        for (int index = 0; index < dotCount; ++index)
          text.text += ".";
        yield return new WaitForSecondsRealtime(0.5f);
      }
    }
  }
}
