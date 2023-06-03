// Decompiled with JetBrains decompiler
// Type: SECTR_Modules
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public static class SECTR_Modules
{
  public static bool AUDIO = false;
  public static bool VIS = false;
  public static bool STREAM = false;
  public static bool DEV = false;
  public static string VERSION = "1.1.4f";

  static SECTR_Modules()
  {
    AUDIO = System.Type.GetType("SECTR_AudioSystem") != null;
    VIS = System.Type.GetType("SECTR_CullingCamera") != null;
    STREAM = System.Type.GetType("SECTR_Chunk") != null;
    DEV = System.Type.GetType("SECTR_Tests") != null;
  }

  public static bool HasPro() => Application.HasProLicense();

  public static bool HasComplete() => AUDIO && VIS && STREAM;
}
