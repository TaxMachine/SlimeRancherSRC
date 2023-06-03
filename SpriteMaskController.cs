// Decompiled with JetBrains decompiler
// Type: SpriteMaskController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Sprites;

[ExecuteInEditMode]
public class SpriteMaskController : MonoBehaviour
{
  private SpriteRenderer m_spriteRenderer;
  private Vector4 m_uvs;

  private void OnEnable()
  {
    m_spriteRenderer = GetComponent<SpriteRenderer>();
    m_uvs = DataUtility.GetInnerUV(m_spriteRenderer.sprite);
    m_spriteRenderer.sharedMaterial.SetVector("_CustomUVS", m_uvs);
  }
}
