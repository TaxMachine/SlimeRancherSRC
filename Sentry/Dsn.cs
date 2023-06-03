// Decompiled with JetBrains decompiler
// Type: Sentry.Dsn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace Sentry
{
  public class Dsn
  {
    private Uri _uri;
    public Uri callUri;
    public string secretKey;
    public string publicKey;

    public Dsn(string dsn)
    {
      _uri = !(dsn == "") ? new Uri(dsn) : throw new ArgumentException("invalid argument - DSN cannot be empty");
      string[] strArray = !string.IsNullOrEmpty(_uri.UserInfo) ? _uri.UserInfo.Split(':') : throw new ArgumentException("Invalid DSN: No public key provided.");
      publicKey = strArray[0];
      if (string.IsNullOrEmpty(publicKey))
        throw new ArgumentException("Invalid DSN: No public key provided.");
      secretKey = null;
      if (strArray.Length > 1)
        secretKey = strArray[1];
      string str1 = _uri.AbsolutePath.Substring(0, _uri.AbsolutePath.LastIndexOf('/'));
      string str2 = _uri.AbsoluteUri.Substring(_uri.AbsoluteUri.LastIndexOf('/') + 1);
      if (string.IsNullOrEmpty(str2))
        throw new ArgumentException("Invalid DSN: A Project Id is required.");
      callUri = new UriBuilder()
      {
        Scheme = _uri.Scheme,
        Host = _uri.DnsSafeHost,
        Port = _uri.Port,
        Path = string.Format("{0}/api/{1}/store/", str1, str2)
      }.Uri;
    }
  }
}
