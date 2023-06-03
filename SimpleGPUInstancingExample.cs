// Decompiled with JetBrains decompiler
// Type: SimpleGPUInstancingExample
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SimpleGPUInstancingExample : MonoBehaviour
{
  public Transform Prefab;
  public Material InstancedMaterial;

  private void Awake()
  {
    InstancedMaterial.enableInstancing = true;
    float max = 4f;
    for (int index = 0; index < 1000; ++index)
    {
      Transform transform = Instantiate(Prefab, new Vector3(Random.Range(-max, max), max + Random.Range(-max, max), Random.Range(-max, max)), Quaternion.identity);
      MaterialPropertyBlock properties = new MaterialPropertyBlock();
      Color color = new Color(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
      properties.SetColor("_Color", color);
      transform.GetComponent<MeshRenderer>().SetPropertyBlock(properties);
    }
  }
}
