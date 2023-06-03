// Decompiled with JetBrains decompiler
// Type: DecorizerUIEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecorizerUIEntry : SRBehaviour
{
  [Tooltip("Main button component.")]
  public Button button;
  [Tooltip("Text component to display the item name.")]
  public TMP_Text name;
  [Tooltip("Image component to display the item image.")]
  public Image image;
  [Tooltip("Text component to display the item content count.")]
  public TMP_Text count;
}
