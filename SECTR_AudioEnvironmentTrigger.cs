// Decompiled with JetBrains decompiler
// Type: SECTR_AudioEnvironmentTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("SECTR/Audio/SECTR Audio Environment Trigger")]
public class SECTR_AudioEnvironmentTrigger : SECTR_AudioEnvironment
{
  private Collider activator;

  private void OnEnable()
  {
    if (!(bool) (Object) activator)
      return;
    Activate();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!(activator == null))
      return;
    Activate();
    activator = other;
  }

  private void OnTriggerExit(Collider other)
  {
    if (!(activator == other))
      return;
    Deactivate();
    activator = null;
  }
}
