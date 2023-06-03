// Decompiled with JetBrains decompiler
// Type: LangReplaceImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangReplaceImage : MonoBehaviour
{
  public Entry[] replacements;
  private Image img;
  private MessageDirector msgDir;
  private Dictionary<string, Sprite> replacementDict = new Dictionary<string, Sprite>();
  private Sprite orig;

  public void Awake()
  {
    img = GetComponent<Image>();
    orig = img.sprite;
    msgDir = SRSingleton<GameContext>.Instance.MessageDirector;
    foreach (Entry replacement in replacements)
      replacementDict[replacement.lang] = replacement.sprite;
    msgDir.RegisterBundlesListener(OnBundlesAvailable);
  }

  public void OnBundlesAvailable(MessageDirector messageDir) => UpdateImage();

  private void UpdateImage()
  {
    string key = msgDir.GetCultureLang().ToString();
    if (replacementDict.ContainsKey(key))
      img.sprite = replacementDict[key];
    else
      img.sprite = orig;
  }

  [Serializable]
  public class Entry
  {
    public string lang;
    public Sprite sprite;
  }
}
