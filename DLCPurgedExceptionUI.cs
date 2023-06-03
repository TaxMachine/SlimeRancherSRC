// Decompiled with JetBrains decompiler
// Type: DLCPurgedExceptionUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DLCPurgedExceptionUI : BaseUI
{
  [Tooltip("Text showing the error message.")]
  public TMP_Text message;
  private DLCPurgedException exception;
  private Action onContinue;
  private Action onCancel;

  public static DLCPurgedExceptionUI OnExceptionCaught(
    DLCPurgedExceptionUI prefab,
    DLCPurgedException exception,
    Action onContinue,
    Action onCancel)
  {
    DLCPurgedExceptionUI purgedExceptionUi = Instantiate(prefab);
    purgedExceptionUi.exception = exception;
    purgedExceptionUi.onContinue = onContinue;
    purgedExceptionUi.onCancel = onCancel;
    purgedExceptionUi.RebuildUI();
    return purgedExceptionUi;
  }

  public override void Update()
  {
    if (!Closeable() || !SRInput.PauseActions.cancel.WasPressed)
      return;
    if (onCancel != null)
      onCancel();
    Close();
  }

  public override void OnBundlesAvailable(MessageDirector messageDirector)
  {
    base.OnBundlesAvailable(messageDirector);
    RebuildUI();
  }

  private void RebuildUI()
  {
    MessageDirector messageDirector = SRSingleton<GameContext>.Instance.MessageDirector;
    MessageBundle bundle = messageDirector.GetBundle("ui");
    MessageBundle pedia = messageDirector.GetBundle("pedia");
    TMP_Text message = this.message;
    string empty;
    if (exception == null)
      empty = string.Empty;
    else
      empty = bundle.Get("e.file_load_failed.dlc_purged", new string[1]
      {
        string.Join("\n", exception.packages.Select(p => pedia.Get(string.Format("m.dlc.{0}", p.ToString().ToLowerInvariant()))).ToArray())
      });
    message.SetText(empty);
  }

  public void OnContinue()
  {
    if (onContinue == null)
      return;
    onContinue();
  }

  public void OnShowPackageInStore()
  {
    if (exception == null || onCancel == null)
      return;
    SRSingleton<GameContext>.Instance.DLCDirector.ShowPackageInStore(exception.packages.First());
    onCancel();
  }

  public void OnCancel()
  {
    if (onCancel == null)
      return;
    onCancel();
  }
}
