// Decompiled with JetBrains decompiler
// Type: PurchaseUI_LoadingSlime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class PurchaseUI_LoadingSlime : SRBehaviour
{
  [Tooltip("List of potential loading slime icons.")]
  public Sprite[] icons;

  public void Awake() => GetRequiredComponent<Image>().sprite = Randoms.SHARED.Pick(icons);
}
