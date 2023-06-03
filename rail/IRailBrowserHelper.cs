// Decompiled with JetBrains decompiler
// Type: rail.IRailBrowserHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailBrowserHelper
  {
    IRailBrowser AsyncCreateBrowser(
      string url,
      uint window_width,
      uint window_height,
      string user_data,
      CreateBrowserOptions options,
      out int result);

    IRailBrowser AsyncCreateBrowser(
      string url,
      uint window_width,
      uint window_height,
      string user_data,
      CreateBrowserOptions options);

    IRailBrowser AsyncCreateBrowser(
      string url,
      uint window_width,
      uint window_height,
      string user_data);

    IRailBrowserRender CreateCustomerDrawBrowser(
      string url,
      string user_data,
      CreateCustomerDrawBrowserOptions options,
      out int result);

    IRailBrowserRender CreateCustomerDrawBrowser(
      string url,
      string user_data,
      CreateCustomerDrawBrowserOptions options);

    IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data);

    RailResult NavigateWebPage(string url, bool display_in_new_tab);
  }
}
