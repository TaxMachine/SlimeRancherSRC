// Decompiled with JetBrains decompiler
// Type: SECTR_TriggerSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("SECTR/Audio/SECTR Trigger Source")]
public class SECTR_TriggerSource : SECTR_PointSource
{
  private Collider activator;

  public SECTR_TriggerSource()
  {
    Loop = false;
    PlayOnStart = false;
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    if (IsPlaying || !(bool) (Object) activator)
      return;
    Play();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!(activator == null))
      return;
    Play();
    activator = other;
  }

  private void OnTriggerExit(Collider other)
  {
    if (!(activator == other))
      return;
    Stop(false);
    activator = null;
  }
}
