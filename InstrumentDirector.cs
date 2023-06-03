// Decompiled with JetBrains decompiler
// Type: InstrumentDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstrumentDirector : SRBehaviour, InstrumentModel.Participant
{
  [Tooltip("Order in which instruments are unlocked.")]
  public InstrumentModel.Instrument[] unlockOrder;
  [Tooltip("Echo note metadata for all available instruments.")]
  public EchoNoteGameMetadata[] instruments;
  [Tooltip("EchoNotes metadata for the currently selected instrument.")]
  public EchoNoteGameMetadata currentInstrument;
  public InstrumentPopupUI popupUI;
  private readonly Dictionary<InstrumentModel.Instrument, EchoNoteGameMetadata> instrumentNoteData = new Dictionary<InstrumentModel.Instrument, EchoNoteGameMetadata>();
  private InstrumentModel model;

  public event OnInstrumentChangedDelegate onInstrumentChanged = _param1 => { };

  public event OnInstrumentUnlockedDelegate onInstrumentUnlocked = () => { };

  private void Awake()
  {
    foreach (EchoNoteGameMetadata instrument in instruments)
    {
      if (instrumentNoteData.ContainsKey(instrument.instrument))
        throw new Exception("Duplicate instrument data for instrument type: " + Enum.GetName(typeof (InstrumentModel.Instrument), instrument.instrument));
      instrumentNoteData[instrument.instrument] = instrument.instrument != InstrumentModel.Instrument.NONE ? instrument : throw new Exception("Invalid instrument data - no instrument type set: " + instrument);
    }
  }

  public void InitForLevel() => SRSingleton<SceneContext>.Instance.GameModel.RegisterInstrument(this);

  public void InitModel(InstrumentModel model)
  {
  }

  public void SetModel(InstrumentModel model)
  {
    this.model = model;
    if (model.GetCurrentlySelectedInstrument() == InstrumentModel.Instrument.NONE && SRSingleton<SceneContext>.Instance.PediaDirector.IsUnlocked(PediaDirector.Id.ECHO_NOTES))
    {
      model.UnlockInstrument(InstrumentModel.Instrument.MARIMBA);
      model.SelectInstrument(InstrumentModel.Instrument.MARIMBA);
    }
    UpdateCurrentEchoNotes();
  }

  public void UnlockInstrument(InstrumentModel.Instrument instrument)
  {
    if (IsUnlocked(instrument))
      return;
    model.UnlockInstrument(instrument);
    if (model.GetCurrentlySelectedInstrument() == InstrumentModel.Instrument.NONE)
      SelectInstrument(instrument);
    if (model.GetUnlockedInstruments().Count > 1)
    {
      PopupDirector popupDirector = SRSingleton<SceneContext>.Instance.PopupDirector;
      popupDirector.QueueForPopup(new InstrumentPopupUI.PopupCreator(instrumentNoteData[instrument]));
      popupDirector.MaybePopupNext();
    }
    onInstrumentUnlocked();
  }

  public HashSet<InstrumentModel.Instrument> GetUnlockedInstruments() => model.GetUnlockedInstruments();

  public bool IsUnlocked(InstrumentModel.Instrument instrument) => model.GetUnlockedInstruments().Contains(instrument);

  public void UnlockNextInstrument()
  {
    foreach (InstrumentModel.Instrument instrument in unlockOrder)
    {
      if (!IsUnlocked(instrument))
      {
        UnlockInstrument(instrument);
        break;
      }
    }
  }

  public void SelectInstrument(InstrumentModel.Instrument instrument)
  {
    model.SelectInstrument(instrument);
    UpdateCurrentEchoNotes();
  }

  public void SelectNextInstrument()
  {
    if (model.GetCurrentlySelectedInstrument() == InstrumentModel.Instrument.NONE)
      throw new Exception("Trying to select next instrument with no instruments available.");
    List<InstrumentModel.Instrument> list = unlockOrder.Where(IsUnlocked).ToList();
    list.AddRange(model.GetUnlockedInstruments().Where(instrument => !unlockOrder.Contains(instrument)));
    int num = list.IndexOf(model.GetCurrentlySelectedInstrument());
    SelectInstrument(list[(num + 1) % list.Count]);
    UpdateCurrentEchoNotes();
  }

  private void UpdateCurrentEchoNotes()
  {
    currentInstrument = model.GetCurrentlySelectedInstrument() == InstrumentModel.Instrument.NONE ? instrumentNoteData[unlockOrder[0]] : instrumentNoteData[model.GetCurrentlySelectedInstrument()];
    onInstrumentChanged(currentInstrument);
  }

  public delegate void OnInstrumentChangedDelegate(EchoNoteGameMetadata instrument);

  public delegate void OnInstrumentUnlockedDelegate();
}
