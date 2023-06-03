// Decompiled with JetBrains decompiler
// Type: BaseUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class BaseUI : SRBehaviour
{
  public OnDestroyDelegate onDestroy;
  public TMP_Text statusArea;
  private Color defaultStatusColor;
  protected MessageBundle uiBundle;
  protected const string ERR_INSUF_COINS = "e.insuf_coins";
  private const float STATUS_AREA_CLEAR_TIME = 5f;
  private float statusClearTime;

  public virtual void Awake()
  {
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(OnBundlesAvailable);
    if (!(statusArea != null))
      return;
    defaultStatusColor = statusArea.color;
    statusArea.text = "";
  }

  public virtual void OnBundlesAvailable(MessageDirector msgDir) => uiBundle = msgDir.GetBundle("ui");

  public virtual void OnDestroy()
  {
    if (SRSingleton<GameContext>.Instance != null)
      SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(OnBundlesAvailable);
    if (onDestroy == null)
      return;
    onDestroy();
  }

  public virtual void Close() => Destroyer.Destroy(gameObject, "BaseUI.Close");

  public void Status(string statusMsg) => SetStatusAndSetupClear(uiBundle.Xlate(statusMsg), defaultStatusColor, 5f);

  public void Error(string errorMsg, bool neverClear = false)
  {
    if (!(statusArea != null))
      return;
    if (neverClear)
      SetStatus(uiBundle.Xlate(errorMsg), Color.yellow);
    else
      SetStatusAndSetupClear(uiBundle.Xlate(errorMsg), Color.yellow, 5f);
  }

  public void ClearStatus()
  {
    statusArea.color = defaultStatusColor;
    statusArea.text = "";
  }

  private void SetStatus(string text, Color color)
  {
    Debug.Log("MST Setting status: " + text);
    statusArea.color = color;
    statusArea.text = text;
  }

  private void SetStatusAndSetupClear(string text, Color color, float clearTime)
  {
    SetStatus(text, color);
    statusClearTime = Time.unscaledTime + clearTime;
    StartCoroutine(TimedClearStatus(clearTime));
  }

  private IEnumerator TimedClearStatus(float seconds)
  {
    yield return new WaitForSecondsRealtime(seconds);
    if (statusClearTime <= (double) Time.unscaledTime)
      ClearStatus();
  }

  protected virtual bool Closeable() => !SRSingleton<SceneContext>.Instance.PediaDirector.IsPediaOpen();

  public virtual void Update()
  {
    if (!SRInput.PauseActions.cancel.WasPressed)
      return;
    OnCancelPressed();
  }

  public void Play(SECTR_AudioCue cue) => SECTR_AudioSystem.Play(cue, Vector3.zero, false);

  public void PlayErrorCue() => Play(SRSingleton<GameContext>.Instance.UITemplates.errorCue);

  protected virtual void OnCancelPressed()
  {
    if (!Closeable())
      return;
    Close();
  }

  public delegate void OnDestroyDelegate();
}
