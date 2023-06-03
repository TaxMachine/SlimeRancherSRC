// Decompiled with JetBrains decompiler
// Type: KeepAlignedUnderActor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class KeepAlignedUnderActor : MonoBehaviour
{
  private Transform alignWith;
  private Rigidbody body;

  public void Start() => body = GetComponent<Rigidbody>();

  public void AlignWith(Transform alignWith) => this.alignWith = alignWith;

  public void FixedUpdate()
  {
    if (alignWith == null)
      Destroyer.Destroy(gameObject, "KeepAlignedUnderActor.FixedUpdate");
    else
      body.MovePosition(alignWith.position);
  }
}
