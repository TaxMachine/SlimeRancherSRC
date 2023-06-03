// Decompiled with JetBrains decompiler
// Type: SectrTestWorldGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SectrTestWorldGenerator : MonoBehaviour
{
  private readonly string[] PATHS = new string[2]
  {
    "SectrTestDynamic_OtherChunk",
    "SectrTestDynamic_OtherChunk2"
  };

  private void Awake()
  {
    for (int x = -30; x <= 30; x += 10)
    {
      for (int z = 10; z <= 50; z += 10)
        CreateRandomSector(x, z, 10f, 10f);
    }
  }

  private void CreateRandomSector(float x, float z, float width, float height)
  {
    Vector3 center = new Vector3(x, 0.0f, z);
    Vector3 size = new Vector3(width, 20f, height);
    GameObject gameObject = new GameObject("TerrainBlock(" + x + "," + z + ")");
    gameObject.transform.position = center;
    SECTR_Sector sectrSector = gameObject.AddComponent<SECTR_Sector>();
    sectrSector.OverrideBounds = true;
    sectrSector.BoundsOverride = new Bounds(center, size);
    sectrSector.Frozen = true;
    SECTR_Chunk sectrChunk = gameObject.AddComponent<SECTR_Chunk>();
    string str = PickSubSceneName();
    sectrChunk.ScenePath = str;
    sectrChunk.NodeName = "Assets/Scene/test/SectrTestDynamic/Chunks/" + str + ".unity";
  }

  private string PickSubSceneName() => PATHS[Random.Range(0, PATHS.Length)];

  private void Update()
  {
  }
}
