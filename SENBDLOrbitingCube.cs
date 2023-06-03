// Decompiled with JetBrains decompiler
// Type: SENBDLOrbitingCube
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SENBDLOrbitingCube : MonoBehaviour
{
  private Transform transf;
  private Vector3 rotationVector;
  private float rotationSpeed;
  private Vector3 spherePosition;
  private Vector3 randomSphereRotation;
  private float sphereRotationSpeed;

  private Vector3 Vec3(float x) => new Vector3(x, x, x);

  private void Start()
  {
    transf = transform;
    rotationVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    rotationVector = Vector3.Normalize(rotationVector);
    spherePosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    spherePosition = Vector3.Normalize(spherePosition);
    spherePosition *= Random.Range(16.5f, 18f);
    randomSphereRotation = new Vector3(Random.Range(-1.1f, 1f), Random.Range(0.0f, 0.1f), Random.Range(0.5f, 1f));
    randomSphereRotation = Vector3.Normalize(randomSphereRotation);
    sphereRotationSpeed = Random.Range(10f, 20f);
    rotationSpeed = Random.Range(1f, 90f);
    transf.localScale = Vec3(Random.Range(1f, 2f));
  }

  private void Update()
  {
    Quaternion quaternion = Quaternion.Euler(randomSphereRotation * Time.time * sphereRotationSpeed);
    transf.position = quaternion * spherePosition + spherePosition * (float) (Mathf.Sin(Time.time - spherePosition.magnitude / 10f) * 0.5 + 0.5) + quaternion * spherePosition * (float) (Mathf.Sin((float) (Time.time * 3.141526460647583 / 4.0 - spherePosition.magnitude / 10.0)) * 0.5 + 0.5);
    transf.rotation = Quaternion.Euler(rotationVector * Time.time * rotationSpeed);
  }
}
