// Decompiled with JetBrains decompiler
// Type: BindingPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BindingPanel
{
  public static GameObject CreateBindingLine(
    string label,
    PlayerAction action,
    GameObject bindingLineObj,
    MessageBundle uiBundle,
    Dictionary<BindingLineUI, string> labelKeyDict,
    BindingLineUI.DisableDelegate disableDelegate)
  {
    bindingLineObj.transform.Find("ActionText").gameObject.GetComponent<TMP_Text>().text = uiBundle.Xlate(label);
    BindingLineUI component = bindingLineObj.GetComponent<BindingLineUI>();
    component.action = action;
    if (disableDelegate != null)
      component.disableDelegate = disableDelegate;
    labelKeyDict[component] = label;
    return bindingLineObj;
  }
}
