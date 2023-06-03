// Decompiled with JetBrains decompiler
// Type: Sentry.Gpu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace Sentry
{
  [Serializable]
  public class Gpu
  {
    public string name;
    public int id;
    public int vendor_id;
    public string vendor_name;
    public int memory_size;
    public string api_type;
    public bool multi_threaded_rendering;
    public string version;
    public string npot_support;
  }
}
