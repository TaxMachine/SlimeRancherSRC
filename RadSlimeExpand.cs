// Decompiled with JetBrains decompiler
// Type: RadSlimeExpand
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadSlimeExpand : SlimeSubbehaviour
{
  public SECTR_AudioCue radNormalCue;
  public SECTR_AudioCue radExpandedCue;
  private GameObject[] radObjects;
  private SECTR_PointSource radObjAudio;
  private CalmedByWaterSpray calmer;
  private float nextPossibleExpand;
  private Randoms rand;
  private bool expanding;
  private bool expanded;
  private float radObjScale;
  private const float EXPAND_MIN_DELAY = 30f;
  private const float EXPAND_MAX_DELAY = 180f;
  public const float EXPANDING_TIME = 3f;
  private const float EXPANDED_TIME = 10f;
  private const float CALMING_TIME = 0.2f;
  private const float EXPAND_FACTOR = 1.5f;
  private const float CALMED_FACTOR = 0.0f;
  private SlimeFaceAnimator slimeFaceAnimator;

  public override void Awake()
  {
    base.Awake();
    rand = new Randoms();
    calmer = GetComponent<CalmedByWaterSpray>();
    slimeFaceAnimator = GetComponent<SlimeFaceAnimator>();
  }

  public override void Start()
  {
    base.Start();
    ExtractRadAura();
    GetComponent<SlimeAppearanceApplicator>().OnAppearanceChanged += appearance => ExtractRadAura();
    nextPossibleExpand = Time.time + ExpandDelay();
  }

  public override float Relevancy(bool isGrounded) => Time.time <= (double) nextPossibleExpand || calmer.IsCalmed() ? 0.0f : 1f;

  public override void Action()
  {
  }

  public override void Selected() => StartCoroutine(ExpandThenShrink());

  public void FixedUpdate()
  {
    if (calmer.IsCalmed())
      nextPossibleExpand += Time.fixedDeltaTime;
    float b = 1f;
    if (expanding)
      b = 1.5f;
    else if (calmer.IsCalmed())
      b = 0.0f;
    else if (expanded)
      b = 1.5f;
    if (radObjects == null || radObjects.Length == 0)
      return;
    float num1 = radObjects[0].transform.localScale.x / radObjScale;
    float num2 = b < 1.0 || num1 < 1.0 ? Time.fixedDeltaTime / 0.2f : Time.fixedDeltaTime / 3f;
    float num3 = num1;
    if (b > (double) num1)
      num3 = Mathf.Min(num1 + num2, b);
    else if (b < (double) num1)
      num3 = Mathf.Max(num1 - num2, b);
    float num4 = radObjScale * num3;
    foreach (GameObject radObject in radObjects)
      radObject.transform.localScale = new Vector3(num4, num4, num4);
  }

  private void ExtractRadAura()
  {
    radObjects = GetComponentsInChildren<RadExpandMarker>().Select(radMarker => radMarker.gameObject).ToArray();
    if (radObjects.Length != 0)
      radObjScale = radObjects[0].transform.localScale.x;
    foreach (GameObject radObject in radObjects)
    {
      SECTR_PointSource component = radObject.GetComponent<SECTR_PointSource>();
      if (component != null)
      {
        radObjAudio = component;
        break;
      }
    }
  }

  private float ExpandDelay() => Mathf.Lerp(30f, 180f, Mathf.Clamp(rand.GetInRange(-0.1f, 0.1f) + (1f - emotions.GetCurr(SlimeEmotions.Emotion.AGITATION)), 0.0f, 1f));

  private IEnumerator ExpandThenShrink()
  {
    slimeFaceAnimator.SetTrigger("triggerConcentrate");
    expanding = true;
    if (radObjAudio != null)
    {
      radObjAudio.Cue = radExpandedCue;
      radObjAudio.Play();
    }
    yield return new WaitForSeconds(3f);
    expanding = false;
    expanded = true;
    yield return new WaitForSeconds(10f);
    expanded = false;
    if (radObjAudio != null)
    {
      radObjAudio.Cue = radNormalCue;
      radObjAudio.Play();
    }
    nextPossibleExpand = Time.time + ExpandDelay();
  }

  public override bool CanRethink() => !expanding;
}
