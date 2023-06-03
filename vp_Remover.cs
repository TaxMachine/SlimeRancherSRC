// Decompiled with JetBrains decompiler
// Type: vp_Remover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_Remover : MonoBehaviour
{
  public float LifeTime = 10f;
  protected vp_Timer.Handle m_DestroyTimer = new vp_Timer.Handle();

  private void OnEnable() => vp_Timer.In(Mathf.Max(LifeTime, 0.1f), () => vp_Utility.Destroy(gameObject), m_DestroyTimer);

  private void OnDisable() => m_DestroyTimer.Cancel();
}
