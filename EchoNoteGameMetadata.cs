// Decompiled with JetBrains decompiler
// Type: EchoNoteGameMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class EchoNoteGameMetadata : ScriptableObject
{
  [Tooltip("Instrument this note set corresponds to.")]
  public InstrumentModel.Instrument instrument;
  [Tooltip("Translation key of the name of the instrument.")]
  public string xlateKey;
  [Tooltip("Icon for this instrument.")]
  public Sprite icon;
  [Tooltip("SFX cue to use with echo notes.")]
  public SECTR_AudioCue cue;
}
