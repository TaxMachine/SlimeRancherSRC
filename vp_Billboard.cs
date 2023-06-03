// Decompiled with JetBrains decompiler
// Type: vp_Billboard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_Billboard : MonoBehaviour
{
  public Transform m_CameraTransform;
  private Transform m_Transform;
  private Renderer m_Renderer;

  protected virtual void Start()
  {
    m_Transform = transform;
    m_Renderer = GetComponent<Renderer>();
    if (!(m_CameraTransform == null))
      return;
    m_CameraTransform = Camera.main.transform;
  }

  protected virtual void Update()
  {
    if (!(m_Renderer != null) || !m_Renderer.isVisible || !(m_CameraTransform != null))
      return;
    m_Transform.eulerAngles = m_CameraTransform.eulerAngles;
  }
}
