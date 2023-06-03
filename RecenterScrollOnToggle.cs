// Decompiled with JetBrains decompiler
// Type: RecenterScrollOnToggle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (RectTransform))]
[RequireComponent(typeof (Toggle))]
public class RecenterScrollOnToggle : MonoBehaviour
{
  private RecenterableScroll scrollCenterScript;
  private Toggle toggle;
  private bool toggleWasOn;

  public void Start()
  {
    scrollCenterScript = GetComponentInParent<RecenterableScroll>();
    toggle = GetComponent<Toggle>();
  }

  public void Update()
  {
    bool isOn = toggle.isOn;
    if (isOn == toggleWasOn)
      return;
    if (isOn)
      scrollCenterScript.ScrollToItem((RectTransform) transform);
    toggleWasOn = isOn;
  }
}
