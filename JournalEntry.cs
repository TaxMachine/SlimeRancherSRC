// Decompiled with JetBrains decompiler
// Type: JournalEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class JournalEntry : UIActivator
{
  [Tooltip("The key used to specify which journal entry to display on interaction.")]
  public string entryKey;
  [Tooltip("If set, ensure the player has these progresses when they read this journal.")]
  public ProgressDirector.ProgressType[] ensureProgress;

  public void Start()
  {
    if (SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().suppressStory)
      gameObject.SetActive(false);
    else
      gameObject.SetActive(true);
  }

  public override GameObject Activate()
  {
    GameObject gameObject = Instantiate(uiPrefab);
    gameObject.GetComponent<JournalUI>().SetJournalKey(entryKey);
    foreach (ProgressDirector.ProgressType type in ensureProgress)
      SRSingleton<SceneContext>.Instance.ProgressDirector.SetProgress(type, 1);
    return gameObject;
  }
}
