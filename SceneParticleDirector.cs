// Decompiled with JetBrains decompiler
// Type: SceneParticleDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneParticleDirector : MonoBehaviour
{
  private List<PooledSceneParticle> waitingForSecondFrame = new List<PooledSceneParticle>();
  private bool hasAlreadyNotified;

  public void InitForLevel() => StartCoroutine(NotifyOnSecondFrame());

  public void AddSecondFrameListener(PooledSceneParticle particle)
  {
    if (hasAlreadyNotified)
      particle.OnSecondFrame();
    else
      waitingForSecondFrame.Add(particle);
  }

  private IEnumerator NotifyOnSecondFrame()
  {
    yield return null;
    foreach (PooledSceneParticle pooledSceneParticle in waitingForSecondFrame)
      pooledSceneParticle.OnSecondFrame();
    waitingForSecondFrame.Clear();
    hasAlreadyNotified = true;
  }
}
