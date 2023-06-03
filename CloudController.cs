// Decompiled with JetBrains decompiler
// Type: CloudController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CloudController : MonoBehaviour, AmbianceDirector.DaynessListener
{
  private Material mat;

  public void Awake() => mat = GetComponent<Renderer>().material;

  public void Start() => SRSingleton<SceneContext>.Instance.AmbianceDirector.RegisterDaynessListener(this);

  public void OnDestroy()
  {
    if (SRSingleton<SceneContext>.Instance != null)
      SRSingleton<SceneContext>.Instance.AmbianceDirector.UnregisterDaynessListener(this);
    Destroyer.Destroy(mat, "CloudController.OnDestroy");
  }

  public void SetDayness(float dayness) => mat.SetFloat("_Dayness", dayness);
}
