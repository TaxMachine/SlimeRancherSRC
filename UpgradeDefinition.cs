// Decompiled with JetBrains decompiler
// Type: UpgradeDefinition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(menuName = "Player/Upgrade Definition")]
public class UpgradeDefinition : ScriptableObject
{
  [SerializeField]
  private PlayerState.Upgrade upgrade;
  [SerializeField]
  private Sprite icon;
  [SerializeField]
  private int cost;

  public PlayerState.Upgrade Upgrade => upgrade;

  public Sprite Icon => icon;

  public int Cost => cost;
}
