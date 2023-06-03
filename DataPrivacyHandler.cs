// Decompiled with JetBrains decompiler
// Type: DataPrivacyHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class DataPrivacyHandler : MonoBehaviour
{
  public Button sourceButton;
  private bool urlOpened;

  public void OpenDataPrivacyUrl()
  {
    sourceButton.interactable = false;
    DataPrivacy.FetchPrivacyUrl(OpenUrl, OnFailure);
  }

  private void OnFailure(string reason)
  {
    sourceButton.interactable = true;
    Debug.LogWarning(string.Format("Failed to get data privacy url: {0}", reason));
  }

  private void OpenUrl(string url)
  {
    sourceButton.interactable = true;
    urlOpened = true;
    Application.OpenURL(url);
  }

  private void OnApplicationFocus(bool hasFocus)
  {
    if (!hasFocus || !urlOpened)
      return;
    urlOpened = false;
    RemoteSettings.ForceUpdate();
  }
}
