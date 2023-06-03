// Decompiled with JetBrains decompiler
// Type: rail.IRailFile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailFile : IRailComponent
  {
    string GetFilename();

    uint Read(byte[] buff, uint bytes_to_read, out int result);

    uint Read(byte[] buff, uint bytes_to_read);

    uint Write(byte[] buff, uint bytes_to_write, out int result);

    uint Write(byte[] buff, uint bytes_to_write);

    RailResult AsyncRead(uint bytes_to_read, string user_data);

    RailResult AsyncWrite(byte[] buffer, uint bytes_to_write, string user_data);

    uint GetSize();

    void Close();
  }
}
