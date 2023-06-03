// Decompiled with JetBrains decompiler
// Type: vp_Bob
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_Bob : MonoBehaviour
{
  public Vector3 BobAmp = new Vector3(0.0f, 0.1f, 0.0f);
  public Vector3 BobRate = new Vector3(0.0f, 4f, 0.0f);
  public float BobOffset;
  public float GroundOffset;
  public bool RandomizeBobOffset;
  public bool LocalMotion;
  protected Transform m_Transform;
  protected Vector3 m_InitialPosition;
  protected Vector3 m_Offset;

  protected virtual void Awake()
  {
    m_Transform = transform;
    m_InitialPosition = m_Transform.position;
  }

  protected virtual void OnEnable()
  {
    m_Transform.position = m_InitialPosition;
    if (!RandomizeBobOffset)
      return;
    BobOffset = Random.value;
  }

  protected virtual void Update()
  {
    if (BobRate.x != 0.0 && BobAmp.x != 0.0)
      m_Offset.x = vp_MathUtility.Sinus(BobRate.x, BobAmp.x, BobOffset);
    if (BobRate.y != 0.0 && BobAmp.y != 0.0)
      m_Offset.y = vp_MathUtility.Sinus(BobRate.y, BobAmp.y, BobOffset);
    if (BobRate.z != 0.0 && BobAmp.z != 0.0)
      m_Offset.z = vp_MathUtility.Sinus(BobRate.z, BobAmp.z, BobOffset);
    if (!LocalMotion)
    {
      m_Transform.position = m_InitialPosition + m_Offset + Vector3.up * GroundOffset;
    }
    else
    {
      m_Transform.position = m_InitialPosition + Vector3.up * GroundOffset;
      m_Transform.localPosition += m_Transform.TransformDirection(m_Offset);
    }
  }
}
