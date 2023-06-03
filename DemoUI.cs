// Decompiled with JetBrains decompiler
// Type: DemoUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoUI : BaseUI
{
  private const uint appId = 433340;
  private const string steamStoreUrl = "http://store.steampowered.com/app/433340/";

  protected override bool Closeable() => false;

  public void OpenStore()
  {
    Application.OpenURL("http://store.steampowered.com/app/433340/");
    Quit();
  }

  public void Quit()
  {
    Close();
    SRSingleton<SceneContext>.Instance.OnSessionEnded();
    SceneManager.LoadScene("MainMenu");
  }
}
