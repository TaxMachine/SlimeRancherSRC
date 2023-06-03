// Decompiled with JetBrains decompiler
// Type: rail.AsyncReadStreamFileResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public class AsyncReadStreamFileResult : EventBase
  {
    public uint try_read_length;
    public int offset;
    public string data;
    public string filename;
  }
}
