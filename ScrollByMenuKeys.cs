// Decompiled with JetBrains decompiler
// Type: ScrollByMenuKeys
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (ScrollRect))]
public class ScrollByMenuKeys : MonoBehaviour
{
  public float scrollPerFrame = 5f;
  public bool scrollWithMainMenuKeysAlso;
  private ScrollRect scroller;

  public void Awake() => scroller = GetComponent<ScrollRect>();

  public void Update()
  {
    float num = ((RectTransform) scroller.content.transform).rect.height - ((RectTransform) scroller.transform).rect.height;
    if (num <= 0.0)
      return;
    if (SRInput.PauseActions.menuScrollUp.IsPressed || scrollWithMainMenuKeysAlso && SRInput.PauseActions.menuUp.IsPressed)
    {
      if (num <= 0.0)
        return;
      scroller.verticalNormalizedPosition = Mathf.Clamp01(scroller.verticalNormalizedPosition + scrollPerFrame / num);
    }
    else
    {
      if (!SRInput.PauseActions.menuScrollDown.IsPressed && (!scrollWithMainMenuKeysAlso || !SRInput.PauseActions.menuDown.IsPressed) || num <= 0.0)
        return;
      scroller.verticalNormalizedPosition = Mathf.Clamp01(scroller.verticalNormalizedPosition - scrollPerFrame / num);
    }
  }
}
