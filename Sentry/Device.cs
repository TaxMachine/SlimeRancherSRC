// Decompiled with JetBrains decompiler
// Type: Sentry.Device
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace Sentry
{
  [Serializable]
  public class Device
  {
    public string name;
    public string family;
    public string model;
    public string model_id;
    public string arch;
    public string cpu_description;
    public float battery_level;
    public string battery_status;
    public string orientation;
    public bool simulator;
    public long memory_size;
    public DateTimeOffset? boot_time;
    public string timezone;
    public string device_type;
  }
}
