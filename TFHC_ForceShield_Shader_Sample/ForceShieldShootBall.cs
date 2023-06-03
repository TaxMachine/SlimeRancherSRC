// Decompiled with JetBrains decompiler
// Type: TFHC_ForceShield_Shader_Sample.ForceShieldShootBall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace TFHC_ForceShield_Shader_Sample
{
  public class ForceShieldShootBall : MonoBehaviour
  {
    public Rigidbody bullet;
    public Transform origshoot;
    public float speed = 1000f;
    private float distance = 10f;

    private void Update()
    {
      if (!Input.GetButtonDown("Fire1"))
        return;
      Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
      Rigidbody rigidbody = Instantiate(bullet, transform.position, Quaternion.identity);
      rigidbody.transform.LookAt(worldPoint);
      rigidbody.AddForce(rigidbody.transform.forward * speed);
    }
  }
}
