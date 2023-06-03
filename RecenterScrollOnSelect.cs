// Decompiled with JetBrains decompiler
// Type: RecenterScrollOnSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof (RectTransform))]
public class RecenterScrollOnSelect : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
  public RectTransform transformToShow;
  private RecenterableScroll scrollCenterScript;

  public void Awake()
  {
    if (!(transformToShow == null))
      return;
    transformToShow = (RectTransform) transform;
  }

  public void OnSelect(BaseEventData eventData) => StartCoroutine(SelectAfterFrame());

  private IEnumerator SelectAfterFrame()
  {
    RecenterScrollOnSelect recenterScrollOnSelect = this;
    yield return new WaitForEndOfFrame();
    if (recenterScrollOnSelect.scrollCenterScript == null)
      recenterScrollOnSelect.scrollCenterScript = recenterScrollOnSelect.GetComponentInParent<RecenterableScroll>();
    if (recenterScrollOnSelect.scrollCenterScript != null)
      recenterScrollOnSelect.scrollCenterScript.ScrollToItem(recenterScrollOnSelect.transformToShow);
  }
}
