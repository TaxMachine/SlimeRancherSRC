// Decompiled with JetBrains decompiler
// Type: GlitchVacuumable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GlitchVacuumable : Vacuumable
{
  protected override void SetCaptive(Joint toJoint)
  {
    base.SetCaptive(toJoint);
    if (!isCaptive())
      return;
    body.velocity = Vector3.zero;
    body.angularVelocity = Vector3.zero;
  }
}
