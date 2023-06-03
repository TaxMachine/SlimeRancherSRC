// Decompiled with JetBrains decompiler
// Type: DragFloatReactor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class DragFloatReactor : SRBehaviour, FloatingReactor
{
  [Tooltip("The factor by which to increase the drag while we're floating.")]
  public float floatDragMultiplier = 25f;
  private float normDrag;
  private Rigidbody body;
  private bool isFloating;

  public void Awake()
  {
    body = GetComponent<Rigidbody>();
    normDrag = body.drag;
  }

  public void SetIsFloating(bool isFloating)
  {
    body.drag = (isFloating ? floatDragMultiplier : 1f) * normDrag;
    this.isFloating = isFloating;
  }

  public bool GetIsFloating() => isFloating;

  public static bool IsFloating(GameObject target)
  {
    DragFloatReactor component = target.GetComponent<DragFloatReactor>();
    return component != null && component.GetIsFloating();
  }
}
