// Decompiled with JetBrains decompiler
// Type: ControlIconUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlIconUI : MonoBehaviour
{
  [SerializeField]
  private TMP_Text ctrlText;
  [SerializeField]
  private Image ctrlImage;
  [SerializeField]
  private string action;

  public void UpdateIcon()
  {
    string activeDeviceString = SRSingleton<GameContext>.Instance.InputDirector.GetActiveDeviceString(action, false);
    bool flag = InputDirector.UsingGamepad();
    bool iconFound;
    ctrlImage.sprite = SRSingleton<GameContext>.Instance.InputDirector.GetActiveDeviceIcon(action, false, out iconFound);
    ctrlText.text = XlateKeyText.XlateKey(activeDeviceString);
    ctrlImage.gameObject.SetActive(iconFound | flag);
    ctrlText.gameObject.SetActive(!iconFound && !flag);
  }
}
