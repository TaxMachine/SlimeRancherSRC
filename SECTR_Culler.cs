// Decompiled with JetBrains decompiler
// Type: SECTR_Culler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof (SECTR_Member))]
[AddComponentMenu("")]
public class SECTR_Culler : MonoBehaviour
{
  private SECTR_Member cachedMember;
  [SECTR_ToolTip("Overrides the culling information on Member.")]
  public bool CullEachChild;

  private void OnEnable()
  {
    cachedMember = GetComponent<SECTR_Member>();
    cachedMember.ChildCulling = CullEachChild ? SECTR_Member.ChildCullModes.Individual : SECTR_Member.ChildCullModes.Group;
  }

  private void OnDisable()
  {
  }
}
