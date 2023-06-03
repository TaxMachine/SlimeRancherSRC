// Decompiled with JetBrains decompiler
// Type: rail.IRailStreamFile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailStreamFile : IRailComponent
  {
    string GetFilename();

    RailResult AsyncRead(int offset, uint bytes_to_read, string user_data);

    RailResult AsyncWrite(byte[] buff, uint bytes_to_write, string user_data);

    ulong GetSize();

    RailResult Close();

    void Cancel();
  }
}
