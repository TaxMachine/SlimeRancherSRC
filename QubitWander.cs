// Decompiled with JetBrains decompiler
// Type: QubitWander
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class QubitWander : MonoBehaviour
{
  public static float TravelTime = 5f;
  public static float LifeSpan = 16f;
  public float WanderAmplitude = 1f;
  public float WanderFrequency = 2f;
  public Vector3 EndPosition;
  private bool hasArrived;
  private float travelEndTime;
  private float dissipationTime;
  private Vector3 startPosition = Vector3.zero;
  private Vector3 orthogonal;
  public GenerateQuantumQubit parentQuantumGenerator;

  private void Start()
  {
    travelEndTime = Time.time + TravelTime;
    dissipationTime = Time.time + LifeSpan;
    startPosition = gameObject.transform.position;
    Vector3 normalized = (EndPosition - startPosition).normalized;
    LookAtDestination();
    orthogonal = new Vector3(-normalized.z, 0.0f, normalized.x);
  }

  private void Update()
  {
    if (!hasArrived && travelEndTime > (double) Time.time)
    {
      float num = travelEndTime - Time.time;
      gameObject.transform.position = Vector3.Lerp(startPosition, EndPosition, (float) (1.0 - num / (double) TravelTime)) + orthogonal * WanderAmplitude * Mathf.Sin(WanderFrequency * num);
      LookAtDestination();
    }
    else if (!hasArrived)
    {
      gameObject.transform.position = EndPosition;
      hasArrived = true;
    }
    else
    {
      if (dissipationTime >= (double) Time.time)
        return;
      parentQuantumGenerator.DissipateQubit(this);
    }
  }

  private void LookAtDestination() => gameObject.transform.LookAt(new Vector3(EndPosition.x, gameObject.transform.position.y, EndPosition.z));

  public bool HasArrived() => hasArrived;
}
