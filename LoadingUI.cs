// Decompiled with JetBrains decompiler
// Type: LoadingUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
  public Image bouncySlime;
  public TMP_Text tipText;
  public Sprite[] bouncyIcons;
  public GameObject autoSavePanel;
  [Tooltip("List of GameObjects to deactivate during a loading error.")]
  public List<GameObject> deactivateOnLoadError;
  [NonSerialized]
  public bool isReturningToMenu;
  private DisableDuringLoading[] toDisable;

  public void Awake()
  {
    MessageBundle bundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui");
    int num1 = -1;
    while (true)
    {
      int num2 = num1 + 1;
      if (bundle.Exists("m.loadingtip." + num2))
        num1 = num2;
      else
        break;
    }
    if (num1 >= 0)
    {
      int num3 = Randoms.SHARED.GetInt(num1 + 1);
      tipText.text = bundle.Get("m.loadingtip." + num3);
    }
    if (bouncyIcons == null || bouncyIcons.Length == 0)
      return;
    bouncySlime.sprite = Randoms.SHARED.Pick(bouncyIcons);
  }

  public void OnEnable()
  {
    toDisable = FindObjectsOfType<DisableDuringLoading>();
    foreach (Component component in toDisable)
      component.gameObject.SetActive(false);
  }

  public void OnDisable()
  {
    if (!isReturningToMenu)
      return;
    foreach (DisableDuringLoading disableDuringLoading in toDisable)
    {
      if (disableDuringLoading != null && disableDuringLoading.gameObject != null)
        disableDuringLoading.gameObject.SetActive(true);
    }
  }

  public void OnLoadingError() => deactivateOnLoadError.ForEach(go => go.SetActive(false));

  public void OnLoadingStart() => deactivateOnLoadError.ForEach(go => go.SetActive(true));
}
