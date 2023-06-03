// Decompiled with JetBrains decompiler
// Type: JournalUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;

public class JournalUI : BaseUI
{
  public TMP_Text journalText;
  public SECTR_AudioCue openCue;
  public SECTR_AudioCue closeCue;

  public void OnEnable() => Play(openCue);

  public void OnDisable() => Play(closeCue);

  public void SetJournalKey(string journalKey) => journalText.text = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("mail").Get("m.journal." + journalKey);
}
