// Decompiled with JetBrains decompiler
// Type: rail.EnumRailComparisonType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public enum EnumRailComparisonType
  {
    kRailComparisonTypeEqualToOrLessThan = 1,
    kRailComparisonTypeLessThan = 2,
    kRailComparisonTypeEqual = 3,
    kRailComparisonTypeGreaterThan = 4,
    kRailComparisonTypeEqualToOrGreaterThan = 5,
    kRailComparisonTypeNotEqual = 6,
    kRailComparisonTypeIn = 7,
    kRailComparisonTypeNotIn = 8,
    kRailComparisonTypeFuzzyMatch = 9,
    kRailComparisonTypeContain = 10, // 0x0000000A
    kRailComparisonTypeNotContain = 11, // 0x0000000B
  }
}
