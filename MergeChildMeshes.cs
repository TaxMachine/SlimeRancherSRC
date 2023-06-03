// Decompiled with JetBrains decompiler
// Type: MergeChildMeshes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MergeChildMeshes : MonoBehaviour
{
  public Material mergedMaterial;

  private void Start()
  {
    gameObject.AddComponent<MeshRenderer>().material = mergedMaterial;
    MeshFilter[] componentsInChildren = GetComponentsInChildren<MeshFilter>();
    int length = componentsInChildren.Length;
    CombineInstance[] combine = new CombineInstance[length];
    for (int index = 0; index < length; ++index)
    {
      combine[index].mesh = componentsInChildren[index].sharedMesh;
      Log.Info("source mesh", "item", index, "vertexCount", componentsInChildren[index].sharedMesh.vertexCount);
      combine[index].transform = componentsInChildren[index].transform.localToWorldMatrix;
      componentsInChildren[index].gameObject.SetActive(false);
    }
    Log.Info("We wrote some stuff3", "count", combine.Length);
    Mesh mesh = new Mesh();
    MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
    meshFilter.mesh = mesh;
    meshFilter.mesh.CombineMeshes(combine);
    Log.Info("Our combined mesh", "vertexCount", mesh.vertexCount);
    transform.rotation = Quaternion.identity;
    transform.position = Vector3.zero;
    transform.gameObject.SetActive(true);
  }
}
