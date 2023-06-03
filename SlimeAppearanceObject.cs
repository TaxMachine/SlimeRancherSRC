// Decompiled with JetBrains decompiler
// Type: SlimeAppearanceObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class SlimeAppearanceObject : MonoBehaviour
{
  public SlimeAppearance.SlimeBone ParentBone;
  public SlimeAppearance.SlimeBone RootBone;
  public SlimeAppearance.SlimeBone[] AttachedBones;
  public bool IgnoreLODIndex;
  public int LODIndex;
  [Tooltip("Indicates that this object should be referenced by the slime's rubber bone effect. Only the highest quality LOD body of the slime should check this.")]
  public bool AttachRubberBoneEffect;
  [Tooltip("If this object is attached to the rubber bone effect of the slime, use this rubber type. Should generally be Slime or SlimeTarr")]
  public RubberBoneEffect.RubberType RubberType = RubberBoneEffect.RubberType.Slime;
}
