// Decompiled with JetBrains decompiler
// Type: MapMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MapMarker : MonoBehaviour
{
  private RectTransform rect;

  public void Awake() => rect = GetComponent<RectTransform>();

  public virtual Quaternion GetRotation() => rect.localRotation;

  public virtual void Rotate(Quaternion rotation) => rect.rotation = rotation;

  public virtual Vector2 GetSize() => rect.sizeDelta;

  public virtual void SetSize(float height, float width) => rect.sizeDelta = new Vector2(width, height);

  public virtual void SetAnchoredPosition(Vector3 position) => rect.anchoredPosition = position;

  public virtual Vector3 GetLocalPosition() => gameObject.transform.localPosition;
}
