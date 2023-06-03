// Decompiled with JetBrains decompiler
// Type: LangReplaceParticleMat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class LangReplaceParticleMat : MonoBehaviour
{
  public Entry[] replacements;
  private ParticleSystemRenderer partSys;
  private MessageDirector msgDir;
  private Dictionary<string, Material> replacementDict = new Dictionary<string, Material>();

  public void Awake()
  {
    partSys = GetComponent<ParticleSystemRenderer>();
    msgDir = SRSingleton<GameContext>.Instance.MessageDirector;
    foreach (Entry replacement in replacements)
      replacementDict[replacement.lang] = replacement.mat;
  }

  public void OnEnable()
  {
    string key = msgDir.GetCultureLang().ToString();
    if (!replacementDict.ContainsKey(key))
      return;
    partSys.sharedMaterial = replacementDict[key];
  }

  [Serializable]
  public class Entry
  {
    public string lang;
    public Material mat;
  }
}
