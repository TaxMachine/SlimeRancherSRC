// Decompiled with JetBrains decompiler
// Type: UITemplates
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UITemplates : SRBehaviour
{
  public GameObject purchaseButtonPrefab;
  public GameObject teleportButtonPrefab;
  public GameObject labelPrefab;
  public GameObject errorDialogPrefab;
  public GameObject confirmDialogPrefab;
  public GameObject purchaseUIPrefab;
  public GameObject decorizerUIPrefab;
  public GameObject availUpgradePrefab;
  public GameObject mailPrefab;
  public GameObject storyCreditsPrefab;
  public GameObject aboutCreditsPrefab;
  public GameObject rancherChatPrefab;
  public GameObject rancherChoicePrefab;
  public SECTR_AudioCue clickCue;
  public SECTR_AudioCue errorCue;
  public SECTR_AudioCue purchaseCue;
  public SECTR_AudioCue purchaseExpansionCue;
  public SECTR_AudioCue purchasePlotCue;
  public SECTR_AudioCue purchaseUpgradeCue;
  public SECTR_AudioCue purchasePersonalUpgradeCue;
  public SECTR_AudioCue purchaseBlueprintCue;
  public SECTR_AudioCue fabricateGadgetCue;
  public SECTR_AudioCue placeGadgetCue;
  public SECTR_AudioCue removeGadgetCue;
  public Sprite currencyIcon;
  public MessageBundle uiBundle;
  public MessageBundle rangeBundle;
  public GameObject loadingUI;
  public GameObject demoUI;
  [FormerlySerializedAs("buttonIconList")]
  public IconEntry[] xboxButtonIconList;
  public IconEntry[] ps4ButtonIconList;
  public Sprite[] steamButtonIcons;
  public IconEntry[] mouseIconList;
  public Sprite unknownButtonIcon;
  private Dictionary<InputDeviceStyle, Dictionary<string, Sprite>> deviceButtonIconDict;

  public GameObject CreateCreditsPrefab(bool aboutCredits) => Instantiate(aboutCredits ? aboutCreditsPrefab : storyCreditsPrefab);

  public void Awake()
  {
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(InitBundles);
    deviceButtonIconDict = new Dictionary<InputDeviceStyle, Dictionary<string, Sprite>>()
    {
      {
        InputDeviceStyle.XboxOne,
        xboxButtonIconList.ToDictionary(e => e.keyStr, e => e.icon)
      },
      {
        InputDeviceStyle.PlayStation4,
        ps4ButtonIconList.ToDictionary(e => e.keyStr, e => e.icon)
      },
      {
        InputDeviceStyle.Unknown,
        mouseIconList.ToDictionary(e => e.keyStr, e => e.icon)
      }
    };
  }

  public void InitBundles(MessageDirector msgDir)
  {
    uiBundle = msgDir.GetBundle("ui");
    rangeBundle = msgDir.GetBundle("range");
  }

  public GameObject CreatePurchaseButton(
    string itemName,
    Sprite costIcon,
    int cost,
    UnityAction onButton)
  {
    GameObject purchaseButton = Instantiate(purchaseButtonPrefab);
    Button component1 = purchaseButton.GetComponent<Button>();
    TMP_Text component2 = purchaseButton.transform.Find("ItemName").gameObject.GetComponent<TMP_Text>();
    Image component3 = purchaseButton.transform.Find("Bottom/CostIcon").gameObject.GetComponent<Image>();
    TMP_Text component4 = purchaseButton.transform.Find("Bottom/CostAmount").gameObject.GetComponent<TMP_Text>();
    component2.text = itemName;
    component3.sprite = costIcon;
    string str = cost.ToString();
    component4.text = str;
    component1.onClick.AddListener(onButton);
    return purchaseButton;
  }

  public GameObject CreateTeleportButton(
    string teleportName,
    bool enableTeleport,
    UnityAction onButton)
  {
    GameObject teleportButton = Instantiate(teleportButtonPrefab);
    Button component = teleportButton.GetComponent<Button>();
    component.interactable = enableTeleport;
    teleportButton.transform.Find("TeleportName").gameObject.GetComponent<TMP_Text>().text = rangeBundle.Get("m.teleporter." + teleportName);
    component.onClick.AddListener(onButton);
    return teleportButton;
  }

  public GameObject CreatePurchaseUI(
    Sprite titleIcon,
    string titleKey,
    PurchaseUI.Purchasable[] purchasables,
    bool hideNubuckCost,
    PurchaseUI.OnClose onClose,
    bool unavailInMainList = false)
  {
    GameObject purchaseUi = Instantiate(purchaseUIPrefab);
    PurchaseUI component = purchaseUi.GetComponent<PurchaseUI>();
    component.Init(titleIcon, titleKey, onClose);
    foreach (PurchaseUI.Purchasable purchasable in purchasables)
      component.AddButton(purchasable, unavailInMainList);
    if (hideNubuckCost)
      component.HideNubuckCost();
    component.SelectFirst();
    return purchaseUi;
  }

  public GameObject CreateRancherChoiceUI(List<string> rancherIds)
  {
    GameObject rancherChoiceUi = Instantiate(rancherChoicePrefab);
    rancherChoiceUi.GetComponent<RancherChoiceUI>().Init(rancherIds);
    return rancherChoiceUi;
  }

  public GameObject CreateErrorDialog(string errorMsg)
  {
    GameObject errorDialog = Instantiate(errorDialogPrefab);
    errorDialog.transform.Find("MainPanel/Message").GetComponent<TMP_Text>().text = uiBundle.Xlate(errorMsg);
    return errorDialog;
  }

  public GameObject CreateErrorDialogWithArgs(string errorMsg, params object[] args)
  {
    GameObject errorDialogWithArgs = Instantiate(errorDialogPrefab);
    errorDialogWithArgs.transform.Find("MainPanel/Message").GetComponent<TMP_Text>().text = uiBundle.Get(errorMsg, args);
    return errorDialogWithArgs;
  }

  public GameObject CreateConfirmDialog(string confirmMsg, ConfirmUI.OnConfirm onConfirm)
  {
    GameObject confirmDialog = Instantiate(confirmDialogPrefab);
    confirmDialog.transform.Find("MainPanel/Message").GetComponent<TMP_Text>().text = uiBundle.Xlate(confirmMsg);
    confirmDialog.GetComponent<ConfirmUI>().onConfirm = onConfirm;
    return confirmDialog;
  }

  public Sprite GetButtonIcon(InputDeviceStyle inputDevice, string keyStr, out bool iconFound)
  {
    int num = InputDirector.UsingGamepad() ? 1 : 0;
    InputDeviceStyle key = InputDeviceStyle.Unknown;
    if (num != 0)
      key = inputDevice == InputDeviceStyle.PlayStation2 || inputDevice == InputDeviceStyle.PlayStation3 || inputDevice == InputDeviceStyle.PlayStation4 ? InputDeviceStyle.PlayStation4 : InputDeviceStyle.XboxOne;
    iconFound = false;
    if (keyStr != null && deviceButtonIconDict.ContainsKey(key))
    {
      Dictionary<string, Sprite> dictionary = deviceButtonIconDict[key];
      if (dictionary.ContainsKey(keyStr))
      {
        iconFound = true;
        return dictionary[keyStr];
      }
    }
    return unknownButtonIcon;
  }

  internal Sprite GetSteamButtonIcon(int originId, out bool iconFound)
  {
    if (originId >= 0 && originId < steamButtonIcons.Length)
    {
      iconFound = true;
      return steamButtonIcons[originId];
    }
    iconFound = false;
    return unknownButtonIcon;
  }

  [Serializable]
  public class IconEntry
  {
    public string keyStr;
    public Sprite icon;
  }
}
