// Decompiled with JetBrains decompiler
// Type: SENBDLCameraAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SENBDLCameraAnimation : MonoBehaviour
{
  private Vector3 randomRotation;
  private Vector3 randomModRotation;

  private void Start()
  {
    randomRotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    randomRotation = Vector3.Normalize(randomRotation);
    randomModRotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    randomModRotation = Vector3.Normalize(randomModRotation);
  }

  private void Update()
  {
    float num = (float) (15.0 + Mathf.Pow((float) (Mathf.Cos((float) (Time.time * 3.1415927410125732 / 15.0)) * 0.5 + 0.5), 3f) * 35.0);
    transform.position = Quaternion.Euler(randomRotation * Time.time * 25f) * (Vector3.up * num);
    transform.LookAt(Vector3.zero);
  }
}
