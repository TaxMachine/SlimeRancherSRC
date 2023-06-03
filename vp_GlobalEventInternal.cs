// Decompiled with JetBrains decompiler
// Type: vp_GlobalEventInternal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;

internal static class vp_GlobalEventInternal
{
  public static Hashtable Callbacks = new Hashtable();

  public static UnregisterException ShowUnregisterException(string name) => new UnregisterException(string.Format("Attempting to Unregister the event {0} but vp_GlobalEvent has not registered this event.", name));

  public static SendException ShowSendException(string name) => new SendException(string.Format("Attempting to Send the event {0} but vp_GlobalEvent has not registered this event.", name));

  public class UnregisterException : Exception
  {
    public UnregisterException(string msg)
      : base(msg)
    {
    }
  }

  public class SendException : Exception
  {
    public SendException(string msg)
      : base(msg)
    {
    }
  }
}
