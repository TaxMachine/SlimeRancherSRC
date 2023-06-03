// Decompiled with JetBrains decompiler
// Type: MusicBoxRegion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class MusicBoxRegion : SRBehaviour
{
  public const float EXTRA_CALMING_FACTOR = 1f;
  private List<SlimeEmotions> currentEmotions = new List<SlimeEmotions>();
  private List<SlimeEmotions> newEmotions = new List<SlimeEmotions>();

  public void OnDisable()
  {
    foreach (SlimeEmotions currentEmotion in this.currentEmotions)
    {
      if (currentEmotion != null)
      {
        currentEmotion.RemoveMusicBox(this);
        newEmotions.Add(currentEmotion);
      }
    }
    List<SlimeEmotions> currentEmotions = this.currentEmotions;
    currentEmotions.Clear();
    this.currentEmotions = newEmotions;
    newEmotions = currentEmotions;
  }

  public void OnTriggerEnter(Collider collider)
  {
    SlimeEmotions component = collider.gameObject.GetComponent<SlimeEmotions>();
    if (!(component != null))
      return;
    component.AddMusicBox(this);
    currentEmotions.Add(component);
  }

  public void OnTriggerExit(Collider collider)
  {
    SlimeEmotions component = collider.gameObject.GetComponent<SlimeEmotions>();
    if (!(component != null))
      return;
    component.RemoveMusicBox(this);
    currentEmotions.Remove(component);
  }
}
