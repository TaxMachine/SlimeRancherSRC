// Decompiled with JetBrains decompiler
// Type: ChimeChangerActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class ChimeChangerActivator : SRBehaviour, TechActivator
{
  public ChimeSongList chimeSongList;
  private List<List<List<int>>> clipIndexes;
  private Animator buttonAnimator;
  private int buttonAnimation;
  private const float TIME_BETWEEN_ACTIVATIONS = 0.4f;
  private float nextActivationTime;
  private Coroutine currentCoroutine;
  private int lastClip = -1;
  private Regex clipRegex;

  public void Awake()
  {
    buttonAnimator = GetComponentInParent<Animator>();
    buttonAnimation = Animator.StringToHash("ButtonPressed");
    clipRegex = new Regex("([0-9]+)");
    clipIndexes = chimeSongList.Select(jingle => jingle.clips.Select(clip => ParseClips(clip).ToList()).ToList()).ToList();
  }

  public void Activate()
  {
    if (nextActivationTime >= (double) Time.time)
      return;
    nextActivationTime = Time.time + 0.4f;
    buttonAnimator.SetTrigger(buttonAnimation);
    if (currentCoroutine != null)
      StopCoroutine(currentCoroutine);
    SRSingleton<GameContext>.Instance.MusicDirector.SetWigglyMode();
    SRSingleton<SceneContext>.Instance.InstrumentDirector.SelectNextInstrument();
    AnalyticsUtil.CustomEvent("ChimeChangerActivated", new Dictionary<string, object>()
    {
      {
        "NewInstrument",
        SRSingleton<SceneContext>.Instance.InstrumentDirector.currentInstrument.xlateKey
      }
    });
    int clipIndex = Randoms.SHARED.GetInt(chimeSongList.Count);
    if (clipIndex == lastClip)
      clipIndex = (clipIndex + 1) % chimeSongList.Count;
    currentCoroutine = StartCoroutine(PlayClips(clipIndex));
    lastClip = clipIndex;
  }

  private IEnumerator PlayClips(int clipIndex)
  {
    ChimeChangerActivator changerActivator = this;
    foreach (List<int> intList in changerActivator.clipIndexes[clipIndex])
    {
      foreach (int num in intList)
        SECTR_AudioSystem.Play(SRSingleton<SceneContext>.Instance.InstrumentDirector.currentInstrument.cue, null, changerActivator.transform.position, false, new int?(num - 1));
      yield return new WaitForSeconds(changerActivator.chimeSongList[clipIndex].distance / 10f);
    }
  }

  public GameObject GetCustomGuiPrefab() => null;

  private IEnumerable<int> ParseClips(string input)
  {
    foreach (Capture match in clipRegex.Matches(input))
      yield return int.Parse(match.Value);
  }
}
