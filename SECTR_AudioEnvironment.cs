// Decompiled with JetBrains decompiler
// Type: SECTR_AudioEnvironment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class SECTR_AudioEnvironment : MonoBehaviour
{
  private bool ambienceActive;
  [SECTR_ToolTip("The configuraiton of the ambient audio in this Reverb Zone.")]
  public SECTR_AudioAmbience Ambience = new SECTR_AudioAmbience();

  public bool Active => ambienceActive;

  private void OnDisable() => Deactivate();

  protected void Activate()
  {
    if (ambienceActive || !enabled)
      return;
    SECTR_AudioSystem.PushAmbience(Ambience);
    ambienceActive = true;
  }

  protected void Deactivate()
  {
    if (!ambienceActive)
      return;
    SECTR_AudioSystem.RemoveAmbience(Ambience);
    ambienceActive = false;
  }
}
