// Decompiled with JetBrains decompiler
// Type: flagwave_random
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class flagwave_random : MonoBehaviour
{
  public void OnEnable()
  {
    Animation component = GetComponent<Animation>();
    component["flagwave_loop"].normalizedTime = Randoms.SHARED.GetInRange(0, 1);
    component["flagwave_loop"].normalizedSpeed = Randoms.SHARED.GetInRange(0.16f, 0.18f);
  }
}
