// Decompiled with JetBrains decompiler
// Type: CFX_ShurikenThreadFix
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class CFX_ShurikenThreadFix : MonoBehaviour
{
  private ParticleSystem[] systems;

  private void OnEnable()
  {
    systems = GetComponentsInChildren<ParticleSystem>();
    foreach (ParticleSystem system in systems)
    {
      var systemEmission = system.emission;
      systemEmission.enabled = false;
    }

    StartCoroutine("WaitFrame");
  }

  private IEnumerator WaitFrame()
  {
    yield return null;
    foreach (ParticleSystem system in systems)
    {
      var systemEmission = system.emission;
      systemEmission.enabled = true;
      system.Play(true);
    }
  }
}
