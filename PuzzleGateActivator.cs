// Decompiled with JetBrains decompiler
// Type: PuzzleGateActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGateActivator : MonoBehaviour
{
  public float sequenceStepDelay = 1f;
  public float stingerDelay = 6f;
  public SequenceEntry[] deactivateSequence;
  public GameObject deactivateAfterSequence;
  public AccessDoor gateDoor;
  [Tooltip("The sound to play after all slot cues are played.")]
  public SECTR_AudioCue stingerCue;
  private int playersPresent;

  public void OnTriggerEnter(Collider col)
  {
    if (!PhysicsUtil.IsPlayerMainCollider(col))
      return;
    ++playersPresent;
  }

  public void OnTriggerExit(Collider col)
  {
    if (!PhysicsUtil.IsPlayerMainCollider(col))
      return;
    --playersPresent;
  }

  public void Update()
  {
    if (playersPresent <= 0)
      return;
    TryToActivate();
  }

  public void TryToActivate()
  {
    if (gateDoor.CurrState != AccessDoor.State.CLOSED)
      return;
    gateDoor.CurrState = AccessDoor.State.OPEN;
    StartCoroutine(DoDeactivateSequence());
    AnalyticsUtil.CustomEvent("PuzzleOpened", new Dictionary<string, object>()
    {
      {
        "name",
        name
      }
    });
  }

  private IEnumerator DoDeactivateSequence()
  {
    SequenceEntry[] sequenceEntryArray = deactivateSequence;
    for (int index = 0; index < sequenceEntryArray.Length; ++index)
    {
      SequenceEntry sequenceEntry = sequenceEntryArray[index];
      SECTR_AudioSystem.Play(sequenceEntry.cue, sequenceEntry.toDeactivate.transform.position, false);
      sequenceEntry.toDeactivate.SetActive(false);
      yield return new WaitForSeconds(sequenceStepDelay);
    }
    sequenceEntryArray = null;
    if (deactivateAfterSequence != null)
      deactivateAfterSequence.SetActive(false);
    if (deactivateSequence.Length != 0)
    {
      yield return new WaitForSeconds(stingerDelay - sequenceStepDelay);
      SECTR_AudioSystem.Play(stingerCue, deactivateSequence[deactivateSequence.Length - 1].toDeactivate.transform.position, false);
    }
    yield return null;
  }

  [Serializable]
  public class SequenceEntry
  {
    public GameObject toDeactivate;
    public SECTR_AudioCue cue;
  }
}
