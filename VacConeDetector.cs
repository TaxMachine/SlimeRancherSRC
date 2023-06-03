// Decompiled with JetBrains decompiler
// Type: VacConeDetector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class VacConeDetector : MonoBehaviour
{
  private GordoFaceAnimator faceAnim;
  private int vacTriggerCount;

  public void Awake() => faceAnim = GetComponent<GordoFaceAnimator>();

  public void OnEnable() => vacTriggerCount = 0;

  public void OnTriggerEnter(Collider col)
  {
    if (!(bool) (Object) col.GetComponentInParent<TrackCollisions>())
      return;
    ++vacTriggerCount;
    if (vacTriggerCount != 1)
      return;
    faceAnim.SetInVac(true);
  }

  public void OnTriggerExit(Collider col)
  {
    if (!(bool) (Object) col.GetComponentInParent<TrackCollisions>())
      return;
    --vacTriggerCount;
    if (vacTriggerCount != 0)
      return;
    faceAnim.SetInVac(false);
  }
}
