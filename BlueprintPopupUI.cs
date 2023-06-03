﻿// Decompiled with JetBrains decompiler
// Type: BlueprintPopupUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintPopupUI : PopupUI<GadgetDefinition>, PopupDirector.Popup
{
  [Tooltip("If not killed before then, how long this popup will stick around.")]
  public float lifetime = 10f;
  protected float timeOfDeath;
  protected PopupDirector popupDir;

  public virtual void Awake()
  {
    timeOfDeath = Time.time + lifetime;
    popupDir = SRSingleton<SceneContext>.Instance.PopupDirector;
    popupDir.PopupActivated(this);
  }

  public override void OnBundleAvailable(MessageDirector msgDir)
  {
    TMP_Text component1 = transform.Find("UIContainer/MainPanel/TitlePanel/Title").GetComponent<TMP_Text>();
    TMP_Text component2 = transform.Find("UIContainer/MainPanel/IntroPanel/Intro").GetComponent<TMP_Text>();
    Image component3 = transform.Find("UIContainer/MainPanel/EntryImage").GetComponent<Image>();
    MessageBundle bundle = msgDir.GetBundle("pedia");
    string lowerInvariant = Enum.GetName(typeof (Gadget.Id), idEntry.id).ToLowerInvariant();
    string str = bundle.Get("m.gadget.name." + lowerInvariant);
    component1.text = str;
    component2.text = bundle.Get("m.gadget.desc." + lowerInvariant);
    component3.sprite = idEntry.icon;
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    popupDir.PopupDeactivated(this);
  }

  public void Update()
  {
    if (Time.time < (double) timeOfDeath)
      return;
    Destroyer.Destroy(gameObject, "BlueprintPopupUI.Update");
  }

  public Gadget.Id GetId() => idEntry.id;

  public bool ShouldClear() => false;

  public static GameObject CreateBlueprintPopup(GadgetDefinition gadgetDefinition)
  {
    GameObject blueprintPopup = Instantiate(SRSingleton<SceneContext>.Instance.GadgetDirector.gadgetPopupPrefab);
    blueprintPopup.GetComponent<BlueprintPopupUI>().Init(gadgetDefinition);
    return blueprintPopup;
  }
}
