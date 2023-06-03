// Decompiled with JetBrains decompiler
// Type: RancherChatUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using Assets.Script.Util.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RancherChatUI : BaseUI
{
  [Tooltip("Rancher image.")]
  public Image rancherImage;
  [Tooltip("Text showing the rancher's name.")]
  public TMP_Text rancherName;
  [Tooltip("Message background image.")]
  public Image messageBackground;
  [Tooltip("Message text.")]
  public TMP_Text messageText;
  [Tooltip("Message prefab parent.")]
  public Transform messagePrefabParent;
  [Tooltip("Image showing the button prompt.")]
  public Image promptImage;
  [Tooltip("Left mouse sprite to show on the button prompt when a mouse is active.")]
  public Sprite promptImageLeftMouse;
  private const string ANIMATION_READY = "rancherChat_loop";
  private static readonly int ANIMATION_NEXT = Animator.StringToHash("next");
  private const float ANIMATION_NEXT_WAIT = 0.1f;
  private static readonly int ANIMATION_EXIT = Animator.StringToHash("exit");
  private const float ANIMATION_EXIT_WAIT = 0.5f;
  private InputDirector inputDirector;
  private MessageBundle bundle;
  private Animator animator;
  private RancherChatMetadata metadata;
  private int index;

  public static RancherChatUI Instantiate(RancherChatMetadata metadata)
  {
    RancherChatUI requiredComponent = Object.Instantiate(SRSingleton<GameContext>.Instance.UITemplates.rancherChatPrefab).GetRequiredComponent<RancherChatUI>();
    requiredComponent.metadata = metadata;
    requiredComponent.Refresh();
    return requiredComponent;
  }

  public override void Awake()
  {
    base.Awake();
    animator = GetRequiredComponent<Animator>();
    inputDirector = SRSingleton<GameContext>.Instance.InputDirector;
    inputDirector.onKeysChanged += OnKeysChanged;
    OnKeysChanged();
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!(inputDirector != null))
      return;
    inputDirector.onKeysChanged -= OnKeysChanged;
    inputDirector = null;
  }

  public override void OnBundlesAvailable(MessageDirector messageDirector)
  {
    base.OnBundlesAvailable(messageDirector);
    bundle = messageDirector.GetBundle("exchange");
    Refresh();
  }

  protected override bool Closeable() => false;

  private void OnKeysChanged() => promptImage.sprite = InputDirector.UsingGamepad() ? inputDirector.GetActiveDeviceIcon("Submit", true, out bool _) : promptImageLeftMouse;

  public void OnButtonPressed_Next()
  {
    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("rancherChat_loop"))
      return;
    StartCoroutine(OnButtonPressed_Next_Coroutine());
  }

  private IEnumerator OnButtonPressed_Next_Coroutine()
  {
    RancherChatUI rancherChatUi = this;
    ++rancherChatUi.index;
    if (rancherChatUi.index >= rancherChatUi.metadata.entries.Length)
    {
      rancherChatUi.animator.SetTrigger(ANIMATION_EXIT);
      yield return new WaitForSecondsRealtime(0.5f);
      rancherChatUi.Close();
    }
    else
    {
      rancherChatUi.animator.SetTrigger(ANIMATION_NEXT);
      yield return new WaitForSecondsRealtime(0.1f);
      rancherChatUi.Refresh();
    }
  }

  private void Refresh()
  {
    if (!(metadata != null) || index >= metadata.entries.Length)
      return;
    RancherChatMetadata.Entry entry = metadata.entries[index];
    rancherName.text = bundle.Get(string.Format("m.rancher.{0}", entry.rancherName.ToString().ToLowerInvariant()));
    rancherImage.sprite = entry.rancherImage;
    if (entry.messagePrefab != null)
    {
      messageText.gameObject.SetActive(false);
      messagePrefabParent.gameObject.SetActive(true);
      messagePrefabParent.DestroyChildren("RancherChatUI.Refresh");
      Object.Instantiate(entry.messagePrefab, messagePrefabParent);
    }
    else
    {
      messageText.gameObject.SetActive(true);
      messagePrefabParent.gameObject.SetActive(false);
      messagePrefabParent.DestroyChildren("RancherChatUI.Refresh");
      messageText.text = bundle.Get(entry.messageText);
    }
    messageBackground.material = entry.messageBackground;
  }
}
