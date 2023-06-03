// Decompiled with JetBrains decompiler
// Type: SlimeFace
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Slimes/Slime Face")]
public class SlimeFace : ScriptableObject
{
  public SlimeExpressionFace[] ExpressionFaces;
  private Dictionary<SlimeExpression, SlimeExpressionFace> _expressionToFaceLookup = new Dictionary<SlimeExpression, SlimeExpressionFace>(DefaultSlimeExpressionComparer);
  public static SlimeExpressionComparer DefaultSlimeExpressionComparer = new SlimeExpressionComparer();

  public SlimeExpressionFace GetExpressionFace(SlimeExpression expression) => _expressionToFaceLookup[expression];

  public void OnEnable()
  {
    _expressionToFaceLookup.Clear();
    for (int index = 0; index < ExpressionFaces.Length; ++index)
    {
      SlimeExpressionFace expressionFace = ExpressionFaces[index];
      _expressionToFaceLookup.Add(expressionFace.SlimeExpression, expressionFace);
    }
  }

  public enum SlimeExpression
  {
    None,
    Alarm,
    Angry,
    AttackTelegraph,
    Awe,
    Blink,
    Blush,
    BlushBlink,
    ChompClosed,
    ChompOpen,
    Elated,
    Feral,
    Fried,
    Glitch,
    Grimace,
    Happy,
    Hungry,
    Invoke,
    Scared,
    Starving,
    Wince,
  }

  public class SlimeExpressionComparer : IEqualityComparer<SlimeExpression>
  {
    public bool Equals(SlimeExpression slimeExpr1, SlimeExpression slimeExpr2) => slimeExpr1 == slimeExpr2;

    public int GetHashCode(SlimeExpression slimeExpr) => (int) slimeExpr;
  }
}
