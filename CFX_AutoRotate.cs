// Decompiled with JetBrains decompiler
// Type: CFX_AutoRotate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CFX_AutoRotate : MonoBehaviour
{
  public Vector3 rotation;
  public Space space = Space.Self;

  private void Update() => transform.Rotate(rotation * Time.deltaTime, space);
}
