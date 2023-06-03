// Decompiled with JetBrains decompiler
// Type: TutorialButtonLine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButtonLine : MonoBehaviour
{
  public TMP_Text keyText;
  public Image btnImage;
  public TMP_Text desc;
  private InputDirector inputDirector;
  private string inputKey;

  public void Awake() => inputDirector = SRSingleton<GameContext>.Instance.InputDirector;

  public void Init(string inputKey, string descStr)
  {
    this.inputKey = inputKey;
    keyText.text = inputKey;
    desc.text = descStr;
    UpdateInstructionIcon();
  }

  private void UpdateInstructionIcon()
  {
    bool flag = InputDirector.UsingGamepad();
    bool iconFound;
    btnImage.sprite = inputDirector.GetActiveDeviceIcon(inputKey, false, out iconFound);
    btnImage.gameObject.SetActive(iconFound | flag);
    keyText.gameObject.SetActive(!iconFound && !flag);
  }
}
