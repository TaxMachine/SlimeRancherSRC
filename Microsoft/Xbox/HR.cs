// Decompiled with JetBrains decompiler
// Type: Microsoft.Xbox.HR
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace Microsoft.Xbox
{
  internal class HR
  {
    internal static bool SUCCEEDED(int hr) => hr >= 0;

    internal static bool FAILED(int hr) => hr < 0;
  }
}
