// Decompiled with JetBrains decompiler
// Type: GlitchTarrNodeDamage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class GlitchTarrNodeDamage : DamagePlayerOnTouch_Trigger
{
  private bool hasCheckedFirstCollision;

  public override void OnEnable()
  {
    base.OnEnable();
    StartCoroutine(WaitForFixedUpdate());
  }

  public override void RegistryUpdate()
  {
    if (!hasCheckedFirstCollision)
      return;
    base.RegistryUpdate();
  }

  private IEnumerator WaitForFixedUpdate()
  {
    GlitchTarrNodeDamage glitchTarrNodeDamage = this;
    glitchTarrNodeDamage.hasCheckedFirstCollision = false;
    yield return new WaitForFixedUpdate();
    glitchTarrNodeDamage.hasCheckedFirstCollision = true;
    if (glitchTarrNodeDamage.damageGameObject != null)
      glitchTarrNodeDamage.nextTime = SRSingleton<SceneContext>.Instance.TimeDirector.HoursFromNow(SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.tarrNodeSpawnDamagePreventionTime * 0.0166666675f);
  }
}
