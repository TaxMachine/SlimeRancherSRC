// Decompiled with JetBrains decompiler
// Type: InstrumentPopupUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentPopupUI : PopupUI<EchoNoteGameMetadata>, PopupDirector.Popup
{
  [Tooltip("Lifetime of the popup (seconds).")]
  public float lifetime;
  [Tooltip("Text representing the instrument name.")]
  public TMP_Text instrumentName;
  [Tooltip("Image representing the instrument icon.")]
  public Image instrumentIcon;

  public void Awake()
  {
    SRSingleton<SceneContext>.Instance.PopupDirector.PopupActivated(this);
    Destroyer.Destroy(gameObject, lifetime, nameof (InstrumentPopupUI));
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.PopupDirector.PopupDeactivated(this);
  }

  public override void Init(EchoNoteGameMetadata instrument)
  {
    base.Init(instrument);
    instrumentIcon.sprite = instrument.icon;
  }

  public override void OnBundleAvailable(MessageDirector messageDirector) => instrumentName.text = messageDirector.GetBundle("actor").Get(idEntry.xlateKey);

  public bool ShouldClear() => false;

  public class PopupCreator : PopupDirector.PopupCreator
  {
    private readonly EchoNoteGameMetadata instrument;

    public PopupCreator(EchoNoteGameMetadata instrument) => this.instrument = instrument;

    public override void Create() => Instantiate(SRSingleton<SceneContext>.Instance.InstrumentDirector.popupUI).Init(instrument);

    public override bool Equals(object other) => other is PopupCreator popupCreator && popupCreator.instrument == instrument;

    public override int GetHashCode() => instrument.GetHashCode();

    public override bool ShouldClear() => false;
  }
}
