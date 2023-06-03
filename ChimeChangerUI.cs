// Decompiled with JetBrains decompiler
// Type: ChimeChangerUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class ChimeChangerUI : MonoBehaviour
{
  public Image iconImage;

  public void Start()
  {
    ShowInstrument(SRSingleton<SceneContext>.Instance.InstrumentDirector.currentInstrument);
    SRSingleton<SceneContext>.Instance.InstrumentDirector.onInstrumentChanged += ShowInstrument;
  }

  public void ShowInstrument(EchoNoteGameMetadata instrument) => iconImage.sprite = instrument.icon;

  private void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.InstrumentDirector != null))
      return;
    SRSingleton<SceneContext>.Instance.InstrumentDirector.onInstrumentChanged -= ShowInstrument;
  }
}
