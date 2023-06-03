// Decompiled with JetBrains decompiler
// Type: ActivateOnInstrumentsUnlocked
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ActivateOnInstrumentsUnlocked : MonoBehaviour
{
  public void Start()
  {
    SRSingleton<SceneContext>.Instance.InstrumentDirector.onInstrumentUnlocked += OnInstrumentUnlocked;
    OnInstrumentUnlocked();
  }

  public void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.InstrumentDirector != null))
      return;
    SRSingleton<SceneContext>.Instance.InstrumentDirector.onInstrumentUnlocked -= OnInstrumentUnlocked;
  }

  private void OnInstrumentUnlocked() => gameObject.SetActive(SRSingleton<SceneContext>.Instance.InstrumentDirector.GetUnlockedInstruments().Count > 1);
}
