// Decompiled with JetBrains decompiler
// Type: AvailUpgradePopupUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvailUpgradePopupUI : PopupUI<PlayerState.Upgrade>, PopupDirector.Popup
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
    string lowerInvariant = Enum.GetName(typeof (PlayerState.Upgrade), idEntry).ToLowerInvariant();
    string str = bundle.Get("m.upgrade.name.personal." + lowerInvariant);
    component1.text = str;
    component2.text = bundle.Get("m.upgrade.desc.personal." + lowerInvariant);
    component3.sprite = SRSingleton<GameContext>.Instance.LookupDirector.GetUpgradeDefinition(idEntry).Icon;
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
    Destroyer.Destroy(gameObject, "AvailUpgradePopupUI.Update");
  }

  public PlayerState.Upgrade GetId() => idEntry;

  public bool ShouldClear() => false;

  public static GameObject CreateAvailUpgradePopup(PlayerState.Upgrade upgrade)
  {
    GameObject availUpgradePopup = Instantiate(SRSingleton<GameContext>.Instance.UITemplates.availUpgradePrefab);
    availUpgradePopup.GetComponent<AvailUpgradePopupUI>().Init(upgrade);
    return availUpgradePopup;
  }
}
