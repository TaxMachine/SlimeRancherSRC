// Decompiled with JetBrains decompiler
// Type: DroneProgramSource_PathGenerationFailure
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DroneProgramSource_PathGenerationFailure : SRBehaviour
{
  private const float MIN_DISTANCE = 3f;
  private const float MIN_DISTANCE_SQR = 9f;
  private Vector3 startPosition;

  public void Awake() => startPosition = transform.position;

  public void OnDestroy() => DroneProgramSource.BLACKLIST.Remove(gameObject);

  public void Update()
  {
    if ((transform.position - startPosition).sqrMagnitude < 9.0)
      return;
    Destroyer.Destroy(this, "DroneProgramSource_PathGenerationFailure.Update");
  }
}
