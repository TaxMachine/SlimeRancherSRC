// Decompiled with JetBrains decompiler
// Type: StandaloneStartScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class StandaloneStartScreen : MonoBehaviour
{
  private bool isLoading;
  private bool pastFirstFrame;

  public void Update()
  {
    if (!pastFirstFrame)
    {
      pastFirstFrame = true;
    }
    else
    {
      if (isLoading)
        return;
      isLoading = true;
      SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
  }
}
