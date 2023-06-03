// Decompiled with JetBrains decompiler
// Type: SelectImageForAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Image))]
public class SelectImageForAction : MonoBehaviour
{
  public string action;
  public bool isPauseAction;
  private Image img;
  private InputDirector inputDir;

  public void Awake()
  {
    img = GetComponent<Image>();
    inputDir = SRSingleton<GameContext>.Instance.InputDirector;
    inputDir.onKeysChanged += OnKeysChanged;
  }

  public void OnDestroy() => inputDir.onKeysChanged -= OnKeysChanged;

  public void OnKeysChanged() => UpdateButtonImage();

  public void Start() => UpdateButtonImage();

  public void UpdateButtonImage() => img.sprite = inputDir.GetActiveDeviceIcon(action, isPauseAction, out bool _);
}
