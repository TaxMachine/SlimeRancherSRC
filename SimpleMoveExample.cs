// Decompiled with JetBrains decompiler
// Type: SimpleMoveExample
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SimpleMoveExample : MonoBehaviour
{
  private Vector3 m_previous;
  private Vector3 m_target;
  private Vector3 m_originalPosition;
  public Vector3 BoundingVolume = new Vector3(3f, 1f, 3f);
  public float Speed = 10f;

  private void Start()
  {
    m_originalPosition = transform.position;
    m_previous = transform.position;
    m_target = transform.position;
  }

  private void Update()
  {
    transform.position = Vector3.Slerp(m_previous, m_target, Time.deltaTime * Speed);
    m_previous = transform.position;
    if (Vector3.Distance(m_target, transform.position) >= 0.10000000149011612)
      return;
    m_target = transform.position + Random.onUnitSphere * Random.Range(0.7f, 4f);
    m_target.Set(Mathf.Clamp(m_target.x, m_originalPosition.x - BoundingVolume.x, m_originalPosition.x + BoundingVolume.x), Mathf.Clamp(m_target.y, m_originalPosition.y - BoundingVolume.y, m_originalPosition.y + BoundingVolume.y), Mathf.Clamp(m_target.z, m_originalPosition.z - BoundingVolume.z, m_originalPosition.z + BoundingVolume.z));
  }
}
