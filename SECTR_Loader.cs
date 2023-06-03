// Decompiled with JetBrains decompiler
// Type: SECTR_Loader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class SECTR_Loader : MonoBehaviour
{
  protected bool locked;

  public abstract bool Loaded { get; }

  protected void LockSelf(bool lockSelf)
  {
    if (lockSelf == locked)
      return;
    Rigidbody[] componentsInChildren1 = GetComponentsInChildren<Rigidbody>();
    int length1 = componentsInChildren1.Length;
    for (int index = 0; index < length1; ++index)
    {
      Rigidbody rigidbody = componentsInChildren1[index];
      if (lockSelf)
        rigidbody.Sleep();
      else
        rigidbody.WakeUp();
    }
    Collider[] componentsInChildren2 = GetComponentsInChildren<Collider>();
    int length2 = componentsInChildren2.Length;
    for (int index = 0; index < length2; ++index)
      componentsInChildren2[index].enabled = !lockSelf;
    locked = lockSelf;
  }
}
