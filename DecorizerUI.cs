// Decompiled with JetBrains decompiler
// Type: DecorizerUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Script.Util.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DecorizerUI : BaseUI
{
  [Tooltip("Main menu parent panel.")]
  public GameObject defaultParent;
  [Tooltip("Parent panel containing the retrieve categories ui.")]
  public GameObject retrieveCategoriesParent;
  [Tooltip("Parent panel containing the retrieve items ui.")]
  public GameObject retrieveParent;
  [Tooltip("Decorizer shared retrieve items grid parent.")]
  public GameObject retrieveContentsGrid;
  [Tooltip("Prefab of a Decorizer shared content item.")]
  public GameObject retrieveContentsEntry;
  [Tooltip("Parent panel containing the cleanup ui.")]
  public GameObject cleanupParent;
  [Header("SFX")]
  [Tooltip("SFX played when the UI is enabled. (optional)")]
  public SECTR_AudioCue onEnableCue;
  [Tooltip("SFX played when the UI is disabled. (optional)")]
  public SECTR_AudioCue onDisableCue;
  [Tooltip("SFX played when the \"retrieve\" button is pressed. (optional)")]
  public SECTR_AudioCue onButtonRetrieveCue;
  [Tooltip("SFX played when a \"retrieve\" category button is pressed. (optional)")]
  public SECTR_AudioCue onButtonRetrieveCategoryCue;
  [Tooltip("SFX played when a \"retrieve\" item button is pressed. (optional)")]
  public SECTR_AudioCue onButtonRetrieveEntryCue;
  [Tooltip("SFX played when the \"cleanup\" button is pressed. (optional)")]
  public SECTR_AudioCue onButtonCleanupCue;
  [Tooltip("SFX played when a \"cleanup\" item button is pressed. (optional)")]
  public SECTR_AudioCue onButtonCleanupEntryCue;
  private const int MAX_DISPLAY_COUNT = 999;
  private Stack<State> states;

  public DecorizerStorage storage { get; set; }

  public void OnEnable()
  {
    SECTR_AudioSystem.Play(onEnableCue, Vector3.zero, false);
    states = new Stack<State>();
    states.Push(State.DEFAULT);
    Refresh();
  }

  public void OnDisable() => SECTR_AudioSystem.Play(onDisableCue, Vector3.zero, false);

  private void Refresh()
  {
    State state = states.Peek();
    defaultParent.SetActive(state == State.DEFAULT);
    cleanupParent.SetActive(state == State.CLEANUP);
    retrieveCategoriesParent.SetActive(state == State.RETRIEVE_CATEGORIES);
    retrieveParent.SetActive(false);
    switch (state)
    {
      case State.RETRIEVE_ECHOES:
      case State.RETRIEVE_ECHO_NOTES:
      case State.RETRIEVE_ORNAMENTS:
        retrieveParent.SetActive(true);
        List<Identifiable.Id> list;
        if (state == State.RETRIEVE_ECHOES)
          list = Identifiable.ECHO_CLASS.ToList();
        else if (state == State.RETRIEVE_ECHO_NOTES)
        {
          list = Identifiable.ECHO_NOTE_CLASS.ToList();
        }
        else
        {
          if (state != State.RETRIEVE_ORNAMENTS)
            throw new InvalidOperationException(string.Format("Failed to get decorizer retrieve entries. [state={0}]", state));
          list = Identifiable.ORNAMENT_CLASS.ToList();
        }
        list.Sort((a, b) => Identifiable.GetName(a).CompareTo(Identifiable.GetName(b)));
        for (int index = 0; index < retrieveContentsGrid.transform.childCount; ++index)
          Destroyer.Destroy(retrieveContentsGrid.transform.GetChild(index).gameObject, "DecorizerUI.Refresh");
        bool flag = false;
        using (List<Identifiable.Id>.Enumerator enumerator = list.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Identifiable.Id id = enumerator.Current;
            GameObject gameObject = Instantiate(retrieveContentsEntry, retrieveContentsGrid.transform);
            DecorizerUIEntry component = gameObject.GetComponent<DecorizerUIEntry>();
            component.name.text = Identifiable.GetName(id, false);
            component.image.sprite = SRSingleton<GameContext>.Instance.LookupDirector.GetIcon(id);
            component.button.onClick.AddListener(() => OnButtonPressed_Retrieve_Entry(id));
            int count = SRSingleton<SceneContext>.Instance.GameModel.GetDecorizerModel().GetCount(id);
            component.count.text = count > 999 ? string.Format("{0}+", 999) : count.ToString();
            if (!flag)
            {
              gameObject.GetRequiredComponentInChildren<Selectable>().gameObject.AddComponent<InitSelected>();
              flag = true;
            }
          }
          break;
        }
    }
  }

  protected override void OnCancelPressed()
  {
    int num = (int) states.Pop();
    if (states.Count == 0)
      Close();
    else
      Refresh();
  }

  public void OnButtonPressed_Retrieve()
  {
    SECTR_AudioSystem.Play(onButtonRetrieveCue, Vector3.zero, false);
    states.Push(State.RETRIEVE_CATEGORIES);
    Refresh();
  }

  public void OnButtonPressed_Retrieve_Category_Echoes() => OnButtonPressed_Retrieve_Category(State.RETRIEVE_ECHOES);

  public void OnButtonPressed_Retrieve_Category_EchoNotes() => OnButtonPressed_Retrieve_Category(State.RETRIEVE_ECHO_NOTES);

  public void OnButtonPressed_Retrieve_Category_Ornaments() => OnButtonPressed_Retrieve_Category(State.RETRIEVE_ORNAMENTS);

  public void OnButtonPressed_Cleanup()
  {
    SECTR_AudioSystem.Play(onButtonCleanupCue, Vector3.zero, false);
    states.Push(State.CLEANUP);
    Refresh();
  }

  public void OnButtonPressed_Cleanup_All() => OnButtonPressed_Cleanup(DecorizerModel.ITEM_CLASSES.SelectMany(c => c));

  public void OnButtonPressed_Cleanup_Echoes() => OnButtonPressed_Cleanup(Identifiable.ECHO_CLASS);

  public void OnButtonPressed_Cleanup_EchoNotes() => OnButtonPressed_Cleanup(Identifiable.ECHO_NOTE_CLASS);

  public void OnButtonPressed_Cleanup_Ornaments() => OnButtonPressed_Cleanup(Identifiable.ORNAMENT_CLASS);

  private void OnButtonPressed_Retrieve_Category(State state)
  {
    SECTR_AudioSystem.Play(onButtonRetrieveCategoryCue, Vector3.zero, false);
    states.Push(state);
    Refresh();
  }

  private void OnButtonPressed_Cleanup(IEnumerable<Identifiable.Id> ids)
  {
    SECTR_AudioSystem.Play(onButtonCleanupEntryCue, Vector3.zero, false);
    storage.Cleanup(ids);
    Close();
  }

  private void OnButtonPressed_Retrieve_Entry(Identifiable.Id id)
  {
    if (SRSingleton<SceneContext>.Instance.GameModel.GetDecorizerModel().GetCount(id) <= 0)
      return;
    SECTR_AudioSystem.Play(onButtonRetrieveEntryCue, Vector3.zero, false);
    storage.selected = id;
    Close();
  }

  private enum State
  {
    DEFAULT,
    RETRIEVE_CATEGORIES,
    RETRIEVE_ECHOES,
    RETRIEVE_ECHO_NOTES,
    RETRIEVE_ORNAMENTS,
    CLEANUP,
  }
}
