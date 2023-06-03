// Decompiled with JetBrains decompiler
// Type: TFHC_Shader_Samples.highlightAnimated
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace TFHC_Shader_Samples
{
  public class highlightAnimated : MonoBehaviour
  {
    private Material mat;

    private void Start() => mat = GetComponent<Renderer>().material;

    private void OnMouseEnter() => switchhighlighted(true);

    private void OnMouseExit() => switchhighlighted(false);

    private void switchhighlighted(bool highlighted) => mat.SetFloat("_Highlighted", highlighted ? 1f : 0.0f);
  }
}
