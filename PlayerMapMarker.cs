// Decompiled with JetBrains decompiler
// Type: PlayerMapMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlayerMapMarker : MapMarker
{
  public RectTransform arrowRect;
  public RectTransform iconRect;

  public override void Rotate(Quaternion rotation) => arrowRect.rotation = Quaternion.Euler(0.0f, 0.0f, (float) -(rotation.eulerAngles.y + 45.0));
}
