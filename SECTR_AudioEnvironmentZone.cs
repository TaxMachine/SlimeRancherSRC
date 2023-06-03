// Decompiled with JetBrains decompiler
// Type: SECTR_AudioEnvironmentZone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (AudioReverbZone))]
[AddComponentMenu("SECTR/Audio/SECTR Audio Environment Zone")]
public class SECTR_AudioEnvironmentZone : SECTR_AudioEnvironment
{
  private AudioReverbZone cachedZone;

  private void OnEnable() => cachedZone = GetComponent<AudioReverbZone>();

  private void OnDisable()
  {
    cachedZone = null;
    Deactivate();
  }

  private void Update()
  {
    if (!SECTR_AudioSystem.Initialized)
      return;
    bool flag = Vector3.SqrMagnitude(SECTR_AudioSystem.Listener.position - transform.position) <= cachedZone.maxDistance * (double) cachedZone.maxDistance;
    if (flag == Active)
      return;
    if (flag)
      Activate();
    else
      Deactivate();
  }
}
