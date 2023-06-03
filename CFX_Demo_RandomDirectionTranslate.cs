// Decompiled with JetBrains decompiler
// Type: CFX_Demo_RandomDirectionTranslate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CFX_Demo_RandomDirectionTranslate : MonoBehaviour
{
  public float speed = 30f;
  public Vector3 baseDir = Vector3.zero;
  public Vector3 axis = Vector3.forward;
  public bool gravity;
  private Vector3 dir;

  private void Start()
  {
    dir = new Vector3(Random.Range(0.0f, 360f), Random.Range(0.0f, 360f), Random.Range(0.0f, 360f)).normalized;
    dir.Scale(axis);
    dir += baseDir;
  }

  private void Update()
  {
    transform.Translate(dir * speed * Time.deltaTime);
    if (!gravity)
      return;
    transform.Translate(Physics.gravity * Time.deltaTime);
  }
}
