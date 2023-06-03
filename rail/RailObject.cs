// Decompiled with JetBrains decompiler
// Type: rail.RailObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class RailObject
  {
    protected IntPtr swigCPtr_ = IntPtr.Zero;

    internal RailObject()
    {
    }

    internal static IntPtr getCPtr(RailObject obj) => obj != null ? obj.swigCPtr_ : IntPtr.Zero;

    ~RailObject()
    {
    }
  }
}
