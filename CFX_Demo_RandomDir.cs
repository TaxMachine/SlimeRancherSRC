// Decompiled with JetBrains decompiler
// Type: CFX_Demo_RandomDir
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CFX_Demo_RandomDir : MonoBehaviour
{
  public Vector3 min = new Vector3(0.0f, 0.0f, 0.0f);
  public Vector3 max = new Vector3(0.0f, 360f, 0.0f);

  private void Start() => transform.eulerAngles = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
}
