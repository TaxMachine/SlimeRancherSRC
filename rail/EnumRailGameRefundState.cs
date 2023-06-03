// Decompiled with JetBrains decompiler
// Type: rail.EnumRailGameRefundState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public enum EnumRailGameRefundState
  {
    kRailGameRefundStateUnknown = 0,
    kRailGameRefundStateApplyReceived = 1000, // 0x000003E8
    kRailGameRefundStateUserCancelApply = 1100, // 0x0000044C
    kRailGameRefundStateAdminCancelApply = 1101, // 0x0000044D
    kRailGameRefundStateRefundSuccess = 1200, // 0x000004B0
    kRailGameRefundStateRefundFailed = 1201, // 0x000004B1
  }
}
