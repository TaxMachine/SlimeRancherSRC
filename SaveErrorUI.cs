// Decompiled with JetBrains decompiler
// Type: SaveErrorUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveErrorUI : BaseUI
{
  private ErrorType errorType;
  public GameObject retryButton;

  public void SetException(Exception e, string path)
  {
    switch (e)
    {
      case UnauthorizedAccessException _:
        Error(MessageUtil.Tcompose("e.savefile_inaccessible", new string[1]
        {
          Path.GetFullPath(path)
        }), true);
        break;
      case ArgumentException _:
      case NotSupportedException _:
      case PathTooLongException _:
        Error(MessageUtil.Tcompose("e.savefile_invalid_name", new string[1]
        {
          Path.GetFullPath(path)
        }), true);
        break;
      case DirectoryNotFoundException _:
        Error(MessageUtil.Tcompose("e.savefile_dir_not_found", new string[1]
        {
          Path.GetFullPath(path)
        }), true);
        break;
      default:
        Error(MessageUtil.Tcompose("e.savefile_unknown", new string[1]
        {
          Path.GetFullPath(path)
        }), true);
        break;
    }
    errorType = ErrorType.SAVE;
    retryButton.gameObject.SetActive(true);
  }

  public void SetLoadException(Exception e, string path)
  {
    Error(MessageUtil.Tcompose("e.pushfile_error", new string[1]
    {
      path
    }), true);
    errorType = ErrorType.LOAD;
    retryButton.gameObject.SetActive(false);
  }

  public void RetrySave()
  {
    if (errorType != ErrorType.SAVE)
      return;
    Close();
    SRSingleton<GameContext>.Instance.AutoSaveDirector.SaveAllNow();
  }

  protected override bool Closeable() => false;

  public void Quit()
  {
    Close();
    SceneManager.LoadScene("MainMenu");
  }

  private enum ErrorType
  {
    NONE,
    LOAD,
    SAVE,
  }
}
