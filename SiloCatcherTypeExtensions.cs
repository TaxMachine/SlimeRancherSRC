// Decompiled with JetBrains decompiler
// Type: SiloCatcherTypeExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public static class SiloCatcherTypeExtensions
{
  public static bool HasInput(this SiloCatcher.Type type) => type == SiloCatcher.Type.SILO_DEFAULT || type == SiloCatcher.Type.REFINERY || type == SiloCatcher.Type.DECORIZER || type == SiloCatcher.Type.VIKTOR_STORAGE;

  public static bool HasOutput(this SiloCatcher.Type type) => type == SiloCatcher.Type.SILO_DEFAULT || type == SiloCatcher.Type.SILO_OUTPUT_ONLY || type == SiloCatcher.Type.DECORIZER || type == SiloCatcher.Type.VIKTOR_STORAGE;
}
