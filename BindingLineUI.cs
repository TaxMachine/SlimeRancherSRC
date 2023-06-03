// Decompiled with JetBrains decompiler
// Type: BindingLineUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BindingLineUI : MonoBehaviour
{
  public DisableDelegate disableDelegate;
  public PlayerAction action;
  public UnityEngine.UI.Button leftBtn;
  public UnityEngine.UI.Button rightBtn;
  public SRInput.ButtonType leftBtnMode;
  public SRInput.ButtonType rightBtnMode = SRInput.ButtonType.SECONDARY;
  public MessageBundle uiBundle;
  public MessageBundle keysBundle;
  private SRInput.ButtonType? mode;
  private OptionsUI ui;
  private bool isCurrentlyBinding;

  public void Start()
  {
    uiBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui");
    ui = GetComponentInParent<OptionsUI>();
    Refresh();
  }

  public void BindPrimary() => Bind(leftBtnMode);

  public void BindSecondary() => Bind(rightBtnMode);

  public void Bind(SRInput.ButtonType btnMode)
  {
    if (isCurrentlyBinding)
      return;
    BindingSource binding = SRInput.GetBinding(action, btnMode);
    if (binding != null)
      action.ListenForBindingReplacing(binding);
    else
      action.ListenForBinding();
    EnterBindingState(btnMode);
    SetMode(new SRInput.ButtonType?(btnMode));
    SetButtonText(uiBundle.Get("m.press_key"));
  }

  private void OnBindingRejected(
    PlayerAction arg1,
    BindingSource arg2,
    BindingSourceRejectionType arg3)
  {
    StartCoroutine(ResetBindingState());
  }

  private void OnBindingAdded(PlayerAction arg1, BindingSource arg2) => StartCoroutine(ResetBindingState());

  private void EnterBindingState(SRInput.ButtonType btnMode)
  {
    SRInput.Actions.ListenOptions.OnBindingAdded += OnBindingAdded;
    SRInput.Actions.ListenOptions.OnBindingRejected += OnBindingRejected;
    if (btnMode == SRInput.ButtonType.GAMEPAD || btnMode == SRInput.ButtonType.GAMEPAD_SEC)
      SRInput.Actions.ListenOptions.OnBindingFound += IsGamepadBinding;
    else
      SRInput.Actions.ListenOptions.OnBindingFound += IsKeyboardMouseBinding;
    isCurrentlyBinding = true;
    SelectImageForAction componentInChildren = gameObject.GetComponentInChildren<SelectImageForAction>();
    if (!(componentInChildren != null))
      return;
    componentInChildren.gameObject.SetActive(false);
  }

  private bool IsGamepadBinding(PlayerAction action, BindingSource binding) => binding.BindingSourceType != BindingSourceType.KeyBindingSource && binding.BindingSourceType != BindingSourceType.MouseBindingSource;

  private bool IsKeyboardMouseBinding(PlayerAction action, BindingSource binding) => binding.BindingSourceType == BindingSourceType.KeyBindingSource || binding.BindingSourceType == BindingSourceType.MouseBindingSource;

  private IEnumerator ResetBindingState()
  {
    BindingLineUI bindingLineUi = this;
    SRInput.Actions.ListenOptions.OnBindingAdded -= bindingLineUi.OnBindingAdded;
    SRInput.Actions.ListenOptions.OnBindingRejected -= bindingLineUi.OnBindingRejected;
    yield return new WaitForEndOfFrame();
    bindingLineUi.isCurrentlyBinding = false;
    SelectImageForAction componentInChildren = bindingLineUi.gameObject.GetComponentInChildren<SelectImageForAction>(true);
    if (componentInChildren != null)
      componentInChildren.gameObject.SetActive(true);
  }

  public IEnumerator Delay(UnityAction action)
  {
    yield return new WaitForEndOfFrame();
    action();
  }

  public void Refresh()
  {
    SetButtonText(leftBtn, XlateKeyText.XlateKey(GetCurrKey(leftBtnMode)));
    if (rightBtn != null)
      SetButtonText(rightBtn, XlateKeyText.XlateKey(GetCurrKey(rightBtnMode)));
    if (!mode.HasValue)
      return;
    StartCoroutine(DelayedResetMode());
  }

  public IEnumerator DelayedResetMode()
  {
    yield return new WaitForEndOfFrame();
    action.StopListeningForBinding();
    SetMode(new SRInput.ButtonType?());
  }

  private string GetCurrKey(SRInput.ButtonType mode) => SRInput.GetButtonKey(action, mode) ?? Key.None.ToString();

  private void SetButtonText(string text)
  {
    SRInput.ButtonType? mode = this.mode;
    SRInput.ButtonType leftBtnMode = this.leftBtnMode;
    SetButtonText(mode.GetValueOrDefault() == leftBtnMode & mode.HasValue ? leftBtn : rightBtn, text);
  }

  private static void SetButtonText(UnityEngine.UI.Button btn, string text)
  {
    foreach (TMP_Text componentsInChild in btn.GetComponentsInChildren<TMP_Text>(true))
      componentsInChild.text = text;
  }

  private void SetMode(SRInput.ButtonType? mode)
  {
    this.mode = mode;
    bool hasValue = mode.HasValue;
    ui.PreventClosing(hasValue);
    TabByMenuKeys.disabledForBinding = hasValue;
    EventSystem.current.sendNavigationEvents = !hasValue;
    ((SRStandaloneInputModule) EventSystem.current.currentInputModule).processMouseEvents = !hasValue;
  }

  public delegate bool DisableDelegate();
}
