// Decompiled with JetBrains decompiler
// Type: rail.IRailBrowser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailBrowser : IRailComponent
  {
    bool GetCurrentUrl(out string url);

    bool ReloadWithUrl(string new_url);

    bool ReloadWithUrl();

    void StopLoad();

    bool AddJavascriptEventListener(string event_name);

    bool RemoveAllJavascriptEventListener();

    void AllowNavigateNewPage(bool allow);

    void Close();
  }
}
