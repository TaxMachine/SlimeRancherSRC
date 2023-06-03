// Decompiled with JetBrains decompiler
// Type: RotationRowUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RotationRowUI : MonoBehaviour
{
  [SerializeField]
  private GameObject rotationRow;
  [SerializeField]
  private ControlIconUI[] gamepadControls;
  private InputDirector inputDirector;

  private void Awake()
  {
    inputDirector = SRSingleton<GameContext>.Instance.InputDirector;
    inputDirector.onKeysChanged += OnKeysChanged;
    UpdateControls();
  }

  private void OnDestroy()
  {
    if (!(inputDirector != null))
      return;
    inputDirector.onKeysChanged -= OnKeysChanged;
  }

  private void OnKeysChanged() => UpdateControls();

  public void ShowRow() => rotationRow.SetActive(true);

  public void HideRow() => rotationRow.SetActive(false);

  public void UpdateControls()
  {
    foreach (ControlIconUI gamepadControl in gamepadControls)
      gamepadControl.UpdateIcon();
  }
}
