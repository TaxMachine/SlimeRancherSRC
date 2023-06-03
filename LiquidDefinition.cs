// Decompiled with JetBrains decompiler
// Type: LiquidDefinition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(menuName = "World/Liquid Definition")]
public class LiquidDefinition : ScriptableObject
{
  [SerializeField]
  private Identifiable.Id id;
  [SerializeField]
  private GameObject inFX;
  [SerializeField]
  private GameObject vacFailFX;

  public Identifiable.Id Id => id;

  public GameObject InFx => inFX;

  public GameObject VacFailFx => vacFailFX;
}
