// Decompiled with JetBrains decompiler
// Type: DiscordRpc
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using AOT;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

public class DiscordRpc
{
  [MonoPInvokeCallback(typeof (OnReadyInfo))]
  public static void ReadyCallback(ref DiscordUser connectedUser) => Callbacks.readyCallback(ref connectedUser);

  [MonoPInvokeCallback(typeof (OnDisconnectedInfo))]
  public static void DisconnectedCallback(int errorCode, string message) => Callbacks.disconnectedCallback(errorCode, message);

  [MonoPInvokeCallback(typeof (OnErrorInfo))]
  public static void ErrorCallback(int errorCode, string message) => Callbacks.errorCallback(errorCode, message);

  [MonoPInvokeCallback(typeof (OnJoinInfo))]
  public static void JoinCallback(string secret) => Callbacks.joinCallback(secret);

  [MonoPInvokeCallback(typeof (OnSpectateInfo))]
  public static void SpectateCallback(string secret) => Callbacks.spectateCallback(secret);

  [MonoPInvokeCallback(typeof (OnRequestInfo))]
  public static void RequestCallback(ref DiscordUser request) => Callbacks.requestCallback(ref request);

  private static EventHandlers Callbacks { get; set; }

  public static void Initialize(
    string applicationId,
    ref EventHandlers handlers,
    bool autoRegister,
    string optionalSteamId)
  {
    Callbacks = handlers;
    EventHandlers handlers1 = new EventHandlers();
    handlers1.readyCallback += ReadyCallback;
    handlers1.disconnectedCallback += DisconnectedCallback;
    handlers1.errorCallback += ErrorCallback;
    handlers1.joinCallback += JoinCallback;
    handlers1.spectateCallback += SpectateCallback;
    handlers1.requestCallback += RequestCallback;
    InitializeInternal(applicationId, ref handlers1, autoRegister, optionalSteamId);
  }

  [DllImport("discord-rpc", EntryPoint = "Discord_Initialize", CallingConvention = CallingConvention.Cdecl)]
  private static extern void InitializeInternal(
    string applicationId,
    ref EventHandlers handlers,
    bool autoRegister,
    string optionalSteamId);

  [DllImport("discord-rpc", EntryPoint = "Discord_Shutdown", CallingConvention = CallingConvention.Cdecl)]
  public static extern void Shutdown();

  [DllImport("discord-rpc", EntryPoint = "Discord_RunCallbacks", CallingConvention = CallingConvention.Cdecl)]
  public static extern void RunCallbacks();

  [DllImport("discord-rpc", EntryPoint = "Discord_UpdatePresence", CallingConvention = CallingConvention.Cdecl)]
  private static extern void UpdatePresenceNative(ref RichPresenceStruct presence);

  [DllImport("discord-rpc", EntryPoint = "Discord_ClearPresence", CallingConvention = CallingConvention.Cdecl)]
  public static extern void ClearPresence();

  [DllImport("discord-rpc", EntryPoint = "Discord_Respond", CallingConvention = CallingConvention.Cdecl)]
  public static extern void Respond(string userId, Reply reply);

  [DllImport("discord-rpc", EntryPoint = "Discord_UpdateHandlers", CallingConvention = CallingConvention.Cdecl)]
  public static extern void UpdateHandlers(ref EventHandlers handlers);

  public static void UpdatePresence(RichPresence presence)
  {
    RichPresenceStruct presence1 = presence.GetStruct();
    UpdatePresenceNative(ref presence1);
    presence.FreeMem();
  }

  public delegate void OnReadyInfo(ref DiscordUser connectedUser);

  public delegate void OnDisconnectedInfo(int errorCode, string message);

  public delegate void OnErrorInfo(int errorCode, string message);

  public delegate void OnJoinInfo(string secret);

  public delegate void OnSpectateInfo(string secret);

  public delegate void OnRequestInfo(ref DiscordUser request);

  public struct EventHandlers
  {
    public OnReadyInfo readyCallback;
    public OnDisconnectedInfo disconnectedCallback;
    public OnErrorInfo errorCallback;
    public OnJoinInfo joinCallback;
    public OnSpectateInfo spectateCallback;
    public OnRequestInfo requestCallback;
  }

  [Serializable]
  public struct RichPresenceStruct
  {
    public IntPtr state;
    public IntPtr details;
    public long startTimestamp;
    public long endTimestamp;
    public IntPtr largeImageKey;
    public IntPtr largeImageText;
    public IntPtr smallImageKey;
    public IntPtr smallImageText;
    public IntPtr partyId;
    public int partySize;
    public int partyMax;
    public IntPtr matchSecret;
    public IntPtr joinSecret;
    public IntPtr spectateSecret;
    public bool instance;
  }

  [Serializable]
  public struct DiscordUser
  {
    public string userId;
    public string username;
    public string discriminator;
    public string avatar;
  }

  public enum Reply
  {
    No,
    Yes,
    Ignore,
  }

  public class RichPresence
  {
    private RichPresenceStruct _presence;
    private readonly List<IntPtr> _buffers = new List<IntPtr>(10);
    public string state;
    public string details;
    public long startTimestamp;
    public long endTimestamp;
    public string largeImageKey;
    public string largeImageText;
    public string smallImageKey;
    public string smallImageText;
    public string partyId;
    public int partySize;
    public int partyMax;
    public string matchSecret;
    public string joinSecret;
    public string spectateSecret;
    public bool instance;

    internal RichPresenceStruct GetStruct()
    {
      if (_buffers.Count > 0)
        FreeMem();
      _presence.state = StrToPtr(state);
      _presence.details = StrToPtr(details);
      _presence.startTimestamp = startTimestamp;
      _presence.endTimestamp = endTimestamp;
      _presence.largeImageKey = StrToPtr(largeImageKey);
      _presence.largeImageText = StrToPtr(largeImageText);
      _presence.smallImageKey = StrToPtr(smallImageKey);
      _presence.smallImageText = StrToPtr(smallImageText);
      _presence.partyId = StrToPtr(partyId);
      _presence.partySize = partySize;
      _presence.partyMax = partyMax;
      _presence.matchSecret = StrToPtr(matchSecret);
      _presence.joinSecret = StrToPtr(joinSecret);
      _presence.spectateSecret = StrToPtr(spectateSecret);
      _presence.instance = instance;
      return _presence;
    }

    private IntPtr StrToPtr(string input)
    {
      if (string.IsNullOrEmpty(input))
        return IntPtr.Zero;
      int byteCount = Encoding.UTF8.GetByteCount(input);
      IntPtr ptr = Marshal.AllocHGlobal(byteCount + 1);
      for (int ofs = 0; ofs < byteCount + 1; ++ofs)
        Marshal.WriteByte(ptr, ofs, 0);
      _buffers.Add(ptr);
      Marshal.Copy(Encoding.UTF8.GetBytes(input), 0, ptr, byteCount);
      return ptr;
    }

    private static string StrToUtf8NullTerm(string toconv)
    {
      string s = toconv.Trim();
      byte[] bytes = Encoding.Default.GetBytes(s);
      if (bytes.Length != 0 && bytes[bytes.Length - 1] != 0)
        s += "\0\0";
      return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(s));
    }

    internal void FreeMem()
    {
      for (int index = _buffers.Count - 1; index >= 0; --index)
      {
        Marshal.FreeHGlobal(_buffers[index]);
        _buffers.RemoveAt(index);
      }
    }
  }
}
