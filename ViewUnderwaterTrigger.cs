// Decompiled with JetBrains decompiler
// Type: ViewUnderwaterTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ViewUnderwaterTrigger : MonoBehaviour
{
  private AmbianceDirector ambianceDir;
  private vp_FPPlayerEventHandler playerEvents;

  public void Awake()
  {
    ambianceDir = SRSingleton<SceneContext>.Instance.AmbianceDirector;
    playerEvents = GetComponentInParent<vp_FPPlayerEventHandler>();
  }

  public void OnTriggerEnter(Collider col)
  {
    LiquidSource component = col.GetComponent<LiquidSource>();
    if (component != null && component.CountsAsUnderwater())
    {
      ambianceDir.EnterWater();
      playerEvents.Underwater.TryStart();
    }
    if (!(bool) (Object) col.GetComponent<JellySea>())
      return;
    ambianceDir.EnterSea();
  }

  public void OnTriggerExit(Collider col)
  {
    LiquidSource component = col.GetComponent<LiquidSource>();
    if (component != null && component.CountsAsUnderwater())
    {
      ambianceDir.ExitWater();
      playerEvents.Underwater.TryStop();
    }
    if (!(bool) (Object) col.GetComponent<JellySea>())
      return;
    ambianceDir.ExitSea();
  }
}
