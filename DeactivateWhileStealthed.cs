// Decompiled with JetBrains decompiler
// Type: DeactivateWhileStealthed
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DeactivateWhileStealthed : MonoBehaviour
{
  private ParticleSystem particleSys;
  private SlimeStealth stealth;

  public void Start()
  {
    particleSys = GetComponent<ParticleSystem>();
    stealth = GetComponentInParent<SlimeStealth>();
  }

  public void Update()
  {
    if (!(stealth != null))
      return;
    var particleSysEmission = particleSys.emission;
    particleSysEmission.enabled = !stealth.IsStealthed;
  }
}
