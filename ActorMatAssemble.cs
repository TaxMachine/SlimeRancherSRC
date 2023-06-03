// Decompiled with JetBrains decompiler
// Type: ActorMatAssemble
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ActorMatAssemble : MonoBehaviour
{
  public List<GameObject> objectsToAssemble;
  public float assembleDuration = 1.5f;
  private float assembleValue;
  private bool assembleDirection;
  private bool assembleComplete;

  public void Update()
  {
    if (assembleComplete)
      return;
    assembleValue += (float) (Time.deltaTime / (double) assembleDuration * (assembleDirection ? 1.0 : -1.0));
    assembleValue = Mathf.Clamp(assembleValue, 0.0f, 1f);
    assembleComplete = assembleValue == (assembleDirection ? 1.0 : 0.0);
    foreach (GameObject gameObject in objectsToAssemble)
    {
      gameObject.SetActive(assembleValue != 0.0);
      gameObject.GetComponent<Renderer>().material.SetFloat("_Assemble", assembleValue);
    }
  }

  public bool Assemble(bool direction)
  {
    assembleDirection = direction;
    assembleComplete = assembleValue == (assembleDirection ? 1.0 : 0.0);
    return !assembleComplete;
  }
}
