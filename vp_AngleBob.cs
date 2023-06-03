// Decompiled with JetBrains decompiler
// Type: vp_AngleBob
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_AngleBob : MonoBehaviour
{
  public Vector3 BobAmp = new Vector3(0.0f, 0.1f, 0.0f);
  public Vector3 BobRate = new Vector3(0.0f, 4f, 0.0f);
  public float YOffset;
  public bool RandomizeBobOffset;
  public bool LocalMotion;
  public bool FadeToTarget;
  protected Transform m_Transform;
  protected Vector3 m_InitialRotation;
  protected Vector3 m_Offset;

  protected virtual void Awake()
  {
    m_Transform = transform;
    m_InitialRotation = m_Transform.eulerAngles;
  }

  protected virtual void OnEnable()
  {
    m_Transform.eulerAngles = m_InitialRotation;
    if (!RandomizeBobOffset)
      return;
    YOffset = Random.value;
  }

  protected virtual void Update()
  {
    if (BobRate.x != 0.0 && BobAmp.x != 0.0)
      m_Offset.x = vp_MathUtility.Sinus(BobRate.x, BobAmp.x);
    if (BobRate.y != 0.0 && BobAmp.y != 0.0)
      m_Offset.y = vp_MathUtility.Sinus(BobRate.y, BobAmp.y);
    if (BobRate.z != 0.0 && BobAmp.z != 0.0)
      m_Offset.z = vp_MathUtility.Sinus(BobRate.z, BobAmp.z);
    if (!LocalMotion)
    {
      if (FadeToTarget)
        m_Transform.rotation = Quaternion.Lerp(m_Transform.rotation, Quaternion.Euler(m_InitialRotation + m_Offset + Vector3.up * YOffset), Time.deltaTime);
      else
        m_Transform.eulerAngles = m_InitialRotation + m_Offset + Vector3.up * YOffset;
    }
    else
    {
      m_Transform.eulerAngles = m_InitialRotation + Vector3.up * YOffset;
      m_Transform.localEulerAngles += m_Transform.TransformDirection(m_Offset);
    }
  }
}
