// Decompiled with JetBrains decompiler
// Type: UFPSCompilerHints
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

internal class UFPSCompilerHints
{
  public static bool CompilerHints()
  {
    vp_Message<int> vpMessage1 = new vp_Message<int>("test");
    vp_Message<string, int> vpMessage2 = new vp_Message<string, int>("test");
    return vpMessage1 != null || vpMessage2 != null || true;
  }
}
