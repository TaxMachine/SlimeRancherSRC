// Decompiled with JetBrains decompiler
// Type: LoadErrorUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class LoadErrorUI : BaseUI
{
  public GameObject okButton;
  public GameObject closeButton;
  public GameObject contactSupportText;
  public TMP_Text message;
  public TMP_Text okButtonText;
  public TMP_Text closeButtonText;
  private Action onOkAction;
  private Action onCloseAction;

  public static LoadErrorUI OpenLoadErrorUI(
    LoadErrorUI prefab,
    string messageKey,
    bool showContactSupport,
    string okButtonKey,
    Action onOkAction,
    string closeButtonKey,
    Action onCloseAction)
  {
    LoadErrorUI loadErrorUi = Instantiate(prefab);
    if (onOkAction == null)
      loadErrorUi.okButton.SetActive(false);
    else
      loadErrorUi.okButtonText.SetText(loadErrorUi.uiBundle.Xlate(okButtonKey));
    if (showContactSupport)
      loadErrorUi.contactSupportText.SetActive(true);
    else
      loadErrorUi.contactSupportText.SetActive(false);
    loadErrorUi.message.SetText(loadErrorUi.uiBundle.Xlate(messageKey));
    loadErrorUi.closeButtonText.SetText(loadErrorUi.uiBundle.Xlate(closeButtonKey));
    loadErrorUi.onOkAction = onOkAction;
    loadErrorUi.onCloseAction = onCloseAction;
    return loadErrorUi;
  }

  public static void OpenLoadErrorUI(
    LoadErrorUI prefab,
    string message,
    bool showContactSupport,
    string closeButtonKey,
    Action onCloseAction)
  {
    OpenLoadErrorUI(prefab, message, showContactSupport, null, null, closeButtonKey, onCloseAction);
  }

  public void OnOk()
  {
    onOkAction();
    Close();
  }

  public void OnClose()
  {
    onCloseAction();
    Close();
  }

  protected override bool Closeable() => false;
}
