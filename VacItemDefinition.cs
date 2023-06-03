// Decompiled with JetBrains decompiler
// Type: VacItemDefinition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(menuName = "Vac/Vac Item Definition")]
public class VacItemDefinition : ScriptableObject
{
  [SerializeField]
  private Identifiable.Id id;
  [SerializeField]
  private Color color;
  [SerializeField]
  private Sprite icon;

  public Identifiable.Id Id => id;

  public Color Color => color;

  public Sprite Icon => icon;

  public static VacItemDefinition CreateVacItemDefinition(
    Identifiable.Id id,
    Color color,
    Sprite icon)
  {
    VacItemDefinition instance = CreateInstance<VacItemDefinition>();
    instance.id = id;
    instance.color = color;
    instance.icon = icon;
    return instance;
  }
}
