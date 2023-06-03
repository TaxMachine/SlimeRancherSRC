// Decompiled with JetBrains decompiler
// Type: LinkLikeButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof (TMP_Text))]
public class LinkLikeButton : 
  MonoBehaviour,
  IPointerEnterHandler,
  IEventSystemHandler,
  IPointerExitHandler
{
  public Color highlightColor;
  private TMP_Text theText;
  private Color normalColor = Color.blue;

  public void Awake()
  {
    theText = GetComponent<TMP_Text>();
    normalColor = theText.color;
  }

  public void OnPointerEnter(PointerEventData eventData) => theText.color = highlightColor;

  public void OnPointerExit(PointerEventData eventData) => theText.color = normalColor;
}
