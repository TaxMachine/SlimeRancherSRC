// Decompiled with JetBrains decompiler
// Type: Smear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class Smear : MonoBehaviour
{
  private Queue<Vector3> m_recentPositions = new Queue<Vector3>();
  public int FramesBufferSize;
  public Renderer Renderer;
  private Material m_instancedMaterial;

  private Material InstancedMaterial
  {
    get => m_instancedMaterial;
    set => m_instancedMaterial = value;
  }

  private void Start() => InstancedMaterial = Renderer.material;

  private void LateUpdate()
  {
    if (m_recentPositions.Count > FramesBufferSize)
      InstancedMaterial.SetVector("_PrevPosition", m_recentPositions.Dequeue());
    InstancedMaterial.SetVector("_Position", transform.position);
    m_recentPositions.Enqueue(transform.position);
  }
}
