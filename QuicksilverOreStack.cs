// Decompiled with JetBrains decompiler
// Type: QuicksilverOreStack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class QuicksilverOreStack : MonoBehaviour
{
  public List<GameObject> oreRocks;
  public float springStrength = 200f;
  public float springDamper = 5f;
  public ParticleSystem activeFX;
  public ParticleSystem idleFX;
  private bool isActive;
  private SpringJoint[] oreJoints;

  private void Start()
  {
    oreJoints = new SpringJoint[oreRocks.Count];
    for (int index = 0; index < oreRocks.Count; ++index)
    {
      SpringJoint component = oreRocks[index].gameObject.GetComponent<SpringJoint>();
      oreJoints[index] = component;
    }
    DeactivateFX();
  }

  private void ActivateSprings()
  {
    for (int index = 0; index < oreJoints.Length; ++index)
    {
      oreJoints[index].spring = springStrength;
      oreJoints[index].damper = springDamper;
      oreRocks[index].gameObject.GetComponent<Rigidbody>().WakeUp();
    }
    ActivateFX();
  }

  private void DeactivateSprings()
  {
    for (int index = 0; index < oreJoints.Length; ++index)
    {
      oreJoints[index].spring = 0.0f;
      oreJoints[index].damper = 0.0f;
      oreRocks[index].gameObject.GetComponent<Rigidbody>().WakeUp();
    }
    DeactivateFX();
  }

  private void ActivateFX()
  {
    activeFX.Play();
    idleFX.Stop();
  }

  private void DeactivateFX()
  {
    activeFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    idleFX.Play();
  }

  public void ToggleOre()
  {
    if (isActive)
      DeactivateSprings();
    else
      ActivateSprings();
  }
}
