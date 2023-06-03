// Decompiled with JetBrains decompiler
// Type: TFHC_ForceShield_Shader_Sample.ForceShieldDestroyBall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace TFHC_ForceShield_Shader_Sample
{
  public class ForceShieldDestroyBall : MonoBehaviour
  {
    public float lifetime = 5f;

    private void Start() => Destroy(gameObject, lifetime);
  }
}
