// Decompiled with JetBrains decompiler
// Type: UIInputLocker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIInputLocker : SRBehaviour
{
  public bool lockEvenSpecialScenes;

  public void OnEnable()
  {
    if (SRSingleton<SceneContext>.Instance != null)
      SRSingleton<SceneContext>.Instance.TimeDirector.Pause(pauseSpecialScenes: lockEvenSpecialScenes);
    if (Levels.isSpecial() && !lockEvenSpecialScenes)
      return;
    SECTR_AudioSystem.PauseNonUISFX(true);
  }

  public void OnDisable()
  {
    if (SRSingleton<SceneContext>.Instance != null)
      SRSingleton<SceneContext>.Instance.TimeDirector.Unpause(pauseSpecialScenes: lockEvenSpecialScenes);
    if (Levels.isSpecial() && !lockEvenSpecialScenes)
      return;
    SECTR_AudioSystem.PauseNonUISFX(false);
  }
}
