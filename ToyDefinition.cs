// Decompiled with JetBrains decompiler
// Type: ToyDefinition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(menuName = "Toy/Toy Definition")]
public class ToyDefinition : ScriptableObject
{
  [SerializeField]
  private Identifiable.Id toyId;
  [SerializeField]
  private Sprite icon;
  [SerializeField]
  private int cost;
  [SerializeField]
  private string nameKey;

  public Identifiable.Id ToyId => toyId;

  public Sprite Icon => icon;

  public int Cost => cost;

  public string NameKey => nameKey;
}
