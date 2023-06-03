// Decompiled with JetBrains decompiler
// Type: BugReportUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine.UI;

public class BugReportUI : BaseUI
{
  public InputField summaryField;
  public InputField descField;
  public Button submitButton;
  private const string ERR_REQUIRE_SUMMARY = "e.require_summary";
  private const string MSG_SENDING_REPORT = "m.sending_report";
  private bool ignoreCallback;

  public override void OnDestroy()
  {
    base.OnDestroy();
    ignoreCallback = true;
  }

  public void Submit()
  {
    string text = summaryField.text;
    if (text.Length <= 0)
    {
      Error("e.require_summary");
    }
    else
    {
      submitButton.interactable = false;
      Status("m.sending_report");
      SentrySdk.CaptureFeedback(text, descField.text, OnBugReportComplete);
    }
  }

  private void OnBugReportComplete(Exception exception)
  {
    if (ignoreCallback)
      return;
    if (exception != null)
    {
      submitButton.interactable = true;
      Error(exception.Message);
    }
    else
      Close();
  }
}
