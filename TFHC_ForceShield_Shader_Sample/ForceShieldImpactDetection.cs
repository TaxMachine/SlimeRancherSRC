// Decompiled with JetBrains decompiler
// Type: TFHC_ForceShield_Shader_Sample.ForceShieldImpactDetection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace TFHC_ForceShield_Shader_Sample
{
  public class ForceShieldImpactDetection : MonoBehaviour
  {
    private float hitTime;
    private Material mat;

    private void Start() => mat = GetComponent<Renderer>().material;

    private void Update()
    {
      if (hitTime <= 0.0)
        return;
      hitTime -= Time.deltaTime * 1000f;
      if (hitTime < 0.0)
        hitTime = 0.0f;
      mat.SetFloat("_HitTime", hitTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
      foreach (ContactPoint contact in collision.contacts)
      {
        mat.SetVector("_HitPosition", transform.InverseTransformPoint(contact.point));
        hitTime = 500f;
        mat.SetFloat("_HitTime", hitTime);
      }
    }
  }
}
