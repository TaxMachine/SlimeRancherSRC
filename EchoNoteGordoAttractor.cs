// Decompiled with JetBrains decompiler
// Type: EchoNoteGordoAttractor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EchoNoteGordoAttractor : Attractor
{
  [Tooltip("Factor applied to the slimes to determine aweness.")]
  [Range(0.0f, 1f)]
  public float attractionFactor;

  public void Awake() => SetAweFactor(attractionFactor);

  public override bool CauseMoveTowards() => true;
}
