// Decompiled with JetBrains decompiler
// Type: vp_Spin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_Spin : MonoBehaviour
{
  public Vector3 RotationSpeed = new Vector3(0.0f, 90f, 0.0f);
  private Transform m_Transform;

  protected virtual void Start() => m_Transform = transform;

  protected virtual void Update() => m_Transform.Rotate(RotationSpeed * Time.deltaTime);
}
