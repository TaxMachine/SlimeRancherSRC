// Decompiled with JetBrains decompiler
// Type: rail.RAIL_API_PINVOKE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace rail
{
  internal class RAIL_API_PINVOKE
  {
    protected static SWIGExceptionHelper swigExceptionHelper = new SWIGExceptionHelper();
    protected static SWIGStringHelper swigStringHelper = new SWIGStringHelper();

    [DllImport("rail_api", EntryPoint = "CSharp_USE_MANUAL_ALLOC_get")]
    public static extern int USE_MANUAL_ALLOC_get();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailString__SWIG_0")]
    public static extern IntPtr new_RailString__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailString__SWIG_1")]
    public static extern IntPtr new_RailString__SWIG_1(string jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailString_SetValue")]
    public static extern IntPtr RailString_SetValue(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailString_assign")]
    public static extern void RailString_assign(IntPtr jarg1, string jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailString_c_str")]
    public static extern string RailString_c_str(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailString_data")]
    public static extern string RailString_data(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailString_clear")]
    public static extern void RailString_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailString_size")]
    public static extern uint RailString_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailString")]
    public static extern void delete_RailString(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailID__SWIG_0")]
    public static extern IntPtr new_RailID__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailID__SWIG_1")]
    public static extern IntPtr new_RailID__SWIG_1(ulong jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailID_set_id")]
    public static extern void RailID_set_id(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailID_get_id")]
    public static extern ulong RailID_get_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailID_IsValid")]
    public static extern bool RailID_IsValid(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailID_GetDomain")]
    public static extern int RailID_GetDomain(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailID_id__set")]
    public static extern void RailID_id__set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailID_id__get")]
    public static extern ulong RailID_id__get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailID")]
    public static extern void delete_RailID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailGameID__SWIG_0")]
    public static extern IntPtr new_RailGameID__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailGameID__SWIG_1")]
    public static extern IntPtr new_RailGameID__SWIG_1(ulong jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailGameID_set_id")]
    public static extern void RailGameID_set_id(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailGameID_get_id")]
    public static extern ulong RailGameID_get_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailGameID_IsValid")]
    public static extern bool RailGameID_IsValid(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailGameID_id__set")]
    public static extern void RailGameID_id__set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailGameID_id__get")]
    public static extern ulong RailGameID_id__get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailGameID")]
    public static extern void delete_RailGameID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcID__SWIG_0")]
    public static extern IntPtr new_RailDlcID__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcID__SWIG_1")]
    public static extern IntPtr new_RailDlcID__SWIG_1(ulong jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcID_set_id")]
    public static extern void RailDlcID_set_id(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcID_get_id")]
    public static extern ulong RailDlcID_get_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcID_IsValid")]
    public static extern bool RailDlcID_IsValid(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcID_id__set")]
    public static extern void RailDlcID_id__set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcID_id__get")]
    public static extern ulong RailDlcID_id__get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailDlcID")]
    public static extern void delete_RailDlcID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceChannelID__SWIG_0")]
    public static extern IntPtr new_RailVoiceChannelID__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceChannelID__SWIG_1")]
    public static extern IntPtr new_RailVoiceChannelID__SWIG_1(ulong jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelID_set_id")]
    public static extern void RailVoiceChannelID_set_id(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelID_get_id")]
    public static extern ulong RailVoiceChannelID_get_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelID_IsValid")]
    public static extern bool RailVoiceChannelID_IsValid(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelID_id__set")]
    public static extern void RailVoiceChannelID_id__set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelID_id__get")]
    public static extern ulong RailVoiceChannelID_id__get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailVoiceChannelID")]
    public static extern void delete_RailVoiceChannelID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailKeyValue_key_set")]
    public static extern void RailKeyValue_key_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailKeyValue_key_get")]
    public static extern string RailKeyValue_key_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailKeyValue_value_set")]
    public static extern void RailKeyValue_value_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailKeyValue_value_get")]
    public static extern string RailKeyValue_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailKeyValue")]
    public static extern IntPtr new_RailKeyValue();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailKeyValue")]
    public static extern void delete_RailKeyValue(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSessionTicket")]
    public static extern IntPtr new_RailSessionTicket();

    [DllImport("rail_api", EntryPoint = "CSharp_RailSessionTicket_ticket_set")]
    public static extern void RailSessionTicket_ticket_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSessionTicket_ticket_get")]
    public static extern string RailSessionTicket_ticket_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailSessionTicket")]
    public static extern void delete_RailSessionTicket(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailComponent_GetComponentVersion")]
    public static extern ulong IRailComponent_GetComponentVersion(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailComponent_Release")]
    public static extern void IRailComponent_Release(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_EventBase")]
    public static extern void delete_EventBase(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_EventBase_get_event_id")]
    public static extern int EventBase_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_EventBase_rail_id_set")]
    public static extern void EventBase_rail_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_EventBase_rail_id_get")]
    public static extern IntPtr EventBase_rail_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_EventBase_game_id_set")]
    public static extern void EventBase_game_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_EventBase_game_id_get")]
    public static extern IntPtr EventBase_game_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_EventBase_user_data_set")]
    public static extern void EventBase_user_data_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_EventBase_user_data_get")]
    public static extern string EventBase_user_data_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_EventBase_result_set")]
    public static extern void EventBase_result_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_EventBase_result_get")]
    public static extern int EventBase_result_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailEvent_OnRailEvent")]
    public static extern void IRailEvent_OnRailEvent(IntPtr jarg1, int jarg2, IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailEvent")]
    public static extern void delete_IRailEvent(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailProductItem__SWIG_0")]
    public static extern IntPtr new_RailArrayRailProductItem__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailProductItem__SWIG_1")]
    public static extern IntPtr new_RailArrayRailProductItem__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailProductItem__SWIG_2")]
    public static extern IntPtr new_RailArrayRailProductItem__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailProductItem__SWIG_3")]
    public static extern IntPtr new_RailArrayRailProductItem__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_SetValue")]
    public static extern IntPtr RailArrayRailProductItem_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailProductItem")]
    public static extern void delete_RailArrayRailProductItem(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_assign")]
    public static extern void RailArrayRailProductItem_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_buf")]
    public static extern IntPtr RailArrayRailProductItem_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_size")]
    public static extern uint RailArrayRailProductItem_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_Item")]
    public static extern IntPtr RailArrayRailProductItem_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_resize")]
    public static extern void RailArrayRailProductItem_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_push_back")]
    public static extern void RailArrayRailProductItem_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_clear")]
    public static extern void RailArrayRailProductItem_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_erase")]
    public static extern void RailArrayRailProductItem_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayZoneInfo__SWIG_0")]
    public static extern IntPtr new_RailArrayZoneInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayZoneInfo__SWIG_1")]
    public static extern IntPtr new_RailArrayZoneInfo__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayZoneInfo__SWIG_2")]
    public static extern IntPtr new_RailArrayZoneInfo__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayZoneInfo__SWIG_3")]
    public static extern IntPtr new_RailArrayZoneInfo__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_SetValue")]
    public static extern IntPtr RailArrayZoneInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayZoneInfo")]
    public static extern void delete_RailArrayZoneInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_assign")]
    public static extern void RailArrayZoneInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_buf")]
    public static extern IntPtr RailArrayZoneInfo_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_size")]
    public static extern uint RailArrayZoneInfo_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_Item")]
    public static extern IntPtr RailArrayZoneInfo_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_resize")]
    public static extern void RailArrayZoneInfo_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_push_back")]
    public static extern void RailArrayZoneInfo_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_clear")]
    public static extern void RailArrayZoneInfo_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_erase")]
    public static extern void RailArrayZoneInfo_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetInfo__SWIG_0")]
    public static extern IntPtr new_RailArrayRailAssetInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetInfo__SWIG_1")]
    public static extern IntPtr new_RailArrayRailAssetInfo__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetInfo__SWIG_2")]
    public static extern IntPtr new_RailArrayRailAssetInfo__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetInfo__SWIG_3")]
    public static extern IntPtr new_RailArrayRailAssetInfo__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_SetValue")]
    public static extern IntPtr RailArrayRailAssetInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailAssetInfo")]
    public static extern void delete_RailArrayRailAssetInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_assign")]
    public static extern void RailArrayRailAssetInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_buf")]
    public static extern IntPtr RailArrayRailAssetInfo_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_size")]
    public static extern uint RailArrayRailAssetInfo_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_Item")]
    public static extern IntPtr RailArrayRailAssetInfo_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_resize")]
    public static extern void RailArrayRailAssetInfo_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_push_back")]
    public static extern void RailArrayRailAssetInfo_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_clear")]
    public static extern void RailArrayRailAssetInfo_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_erase")]
    public static extern void RailArrayRailAssetInfo_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetItem__SWIG_0")]
    public static extern IntPtr new_RailArrayRailAssetItem__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetItem__SWIG_1")]
    public static extern IntPtr new_RailArrayRailAssetItem__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetItem__SWIG_2")]
    public static extern IntPtr new_RailArrayRailAssetItem__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetItem__SWIG_3")]
    public static extern IntPtr new_RailArrayRailAssetItem__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_SetValue")]
    public static extern IntPtr RailArrayRailAssetItem_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailAssetItem")]
    public static extern void delete_RailArrayRailAssetItem(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_assign")]
    public static extern void RailArrayRailAssetItem_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_buf")]
    public static extern IntPtr RailArrayRailAssetItem_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_size")]
    public static extern uint RailArrayRailAssetItem_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_Item")]
    public static extern IntPtr RailArrayRailAssetItem_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_resize")]
    public static extern void RailArrayRailAssetItem_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_push_back")]
    public static extern void RailArrayRailAssetItem_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_clear")]
    public static extern void RailArrayRailAssetItem_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_erase")]
    public static extern void RailArrayRailAssetItem_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayPlayerPersonalInfo__SWIG_0")]
    public static extern IntPtr new_RailArrayPlayerPersonalInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayPlayerPersonalInfo__SWIG_1")]
    public static extern IntPtr new_RailArrayPlayerPersonalInfo__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayPlayerPersonalInfo__SWIG_2")]
    public static extern IntPtr new_RailArrayPlayerPersonalInfo__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayPlayerPersonalInfo__SWIG_3")]
    public static extern IntPtr new_RailArrayPlayerPersonalInfo__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_SetValue")]
    public static extern IntPtr RailArrayPlayerPersonalInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayPlayerPersonalInfo")]
    public static extern void delete_RailArrayPlayerPersonalInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_assign")]
    public static extern void RailArrayPlayerPersonalInfo_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_buf")]
    public static extern IntPtr RailArrayPlayerPersonalInfo_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_size")]
    public static extern uint RailArrayPlayerPersonalInfo_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_Item")]
    public static extern IntPtr RailArrayPlayerPersonalInfo_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_resize")]
    public static extern void RailArrayPlayerPersonalInfo_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_push_back")]
    public static extern void RailArrayPlayerPersonalInfo_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_clear")]
    public static extern void RailArrayPlayerPersonalInfo_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_erase")]
    public static extern void RailArrayPlayerPersonalInfo_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailWorkFileClass__SWIG_0")]
    public static extern IntPtr new_RailArrayEnumRailWorkFileClass__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailWorkFileClass__SWIG_1")]
    public static extern IntPtr new_RailArrayEnumRailWorkFileClass__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailWorkFileClass__SWIG_2")]
    public static extern IntPtr new_RailArrayEnumRailWorkFileClass__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailWorkFileClass__SWIG_3")]
    public static extern IntPtr new_RailArrayEnumRailWorkFileClass__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_SetValue")]
    public static extern IntPtr RailArrayEnumRailWorkFileClass_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayEnumRailWorkFileClass")]
    public static extern void delete_RailArrayEnumRailWorkFileClass(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_assign")]
    public static extern void RailArrayEnumRailWorkFileClass_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_buf")]
    public static extern IntPtr RailArrayEnumRailWorkFileClass_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_size")]
    public static extern uint RailArrayEnumRailWorkFileClass_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_Item")]
    public static extern IntPtr RailArrayEnumRailWorkFileClass_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_resize")]
    public static extern void RailArrayEnumRailWorkFileClass_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_push_back")]
    public static extern void RailArrayEnumRailWorkFileClass_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_clear")]
    public static extern void RailArrayEnumRailWorkFileClass_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_erase")]
    public static extern void RailArrayEnumRailWorkFileClass_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailWorkFileClass__SWIG_4")]
    public static extern IntPtr new_RailArrayEnumRailWorkFileClass__SWIG_4(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetProperty__SWIG_0")]
    public static extern IntPtr new_RailArrayRailAssetProperty__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetProperty__SWIG_1")]
    public static extern IntPtr new_RailArrayRailAssetProperty__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetProperty__SWIG_2")]
    public static extern IntPtr new_RailArrayRailAssetProperty__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetProperty__SWIG_3")]
    public static extern IntPtr new_RailArrayRailAssetProperty__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_SetValue")]
    public static extern IntPtr RailArrayRailAssetProperty_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailAssetProperty")]
    public static extern void delete_RailArrayRailAssetProperty(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_assign")]
    public static extern void RailArrayRailAssetProperty_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_buf")]
    public static extern IntPtr RailArrayRailAssetProperty_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_size")]
    public static extern uint RailArrayRailAssetProperty_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_Item")]
    public static extern IntPtr RailArrayRailAssetProperty_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_resize")]
    public static extern void RailArrayRailAssetProperty_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_push_back")]
    public static extern void RailArrayRailAssetProperty_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_clear")]
    public static extern void RailArrayRailAssetProperty_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_erase")]
    public static extern void RailArrayRailAssetProperty_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilterKey__SWIG_0")]
    public static extern IntPtr new_RailArrayGameServerListFilterKey__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilterKey__SWIG_1")]
    public static extern IntPtr new_RailArrayGameServerListFilterKey__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilterKey__SWIG_2")]
    public static extern IntPtr new_RailArrayGameServerListFilterKey__SWIG_2(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilterKey__SWIG_3")]
    public static extern IntPtr new_RailArrayGameServerListFilterKey__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_SetValue")]
    public static extern IntPtr RailArrayGameServerListFilterKey_SetValue(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayGameServerListFilterKey")]
    public static extern void delete_RailArrayGameServerListFilterKey(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_assign")]
    public static extern void RailArrayGameServerListFilterKey_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_buf")]
    public static extern IntPtr RailArrayGameServerListFilterKey_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_size")]
    public static extern uint RailArrayGameServerListFilterKey_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_Item")]
    public static extern IntPtr RailArrayGameServerListFilterKey_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_resize")]
    public static extern void RailArrayGameServerListFilterKey_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_push_back")]
    public static extern void RailArrayGameServerListFilterKey_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_clear")]
    public static extern void RailArrayGameServerListFilterKey_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_erase")]
    public static extern void RailArrayGameServerListFilterKey_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailStreamFileInfo__SWIG_0")]
    public static extern IntPtr new_RailArrayRailStreamFileInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailStreamFileInfo__SWIG_1")]
    public static extern IntPtr new_RailArrayRailStreamFileInfo__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailStreamFileInfo__SWIG_2")]
    public static extern IntPtr new_RailArrayRailStreamFileInfo__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailStreamFileInfo__SWIG_3")]
    public static extern IntPtr new_RailArrayRailStreamFileInfo__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_SetValue")]
    public static extern IntPtr RailArrayRailStreamFileInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailStreamFileInfo")]
    public static extern void delete_RailArrayRailStreamFileInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_assign")]
    public static extern void RailArrayRailStreamFileInfo_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_buf")]
    public static extern IntPtr RailArrayRailStreamFileInfo_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_size")]
    public static extern uint RailArrayRailStreamFileInfo_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_Item")]
    public static extern IntPtr RailArrayRailStreamFileInfo_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_resize")]
    public static extern void RailArrayRailStreamFileInfo_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_push_back")]
    public static extern void RailArrayRailStreamFileInfo_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_clear")]
    public static extern void RailArrayRailStreamFileInfo_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_erase")]
    public static extern void RailArrayRailStreamFileInfo_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilter__SWIG_0")]
    public static extern IntPtr new_RailArrayRoomInfoListFilter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilter__SWIG_1")]
    public static extern IntPtr new_RailArrayRoomInfoListFilter__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilter__SWIG_2")]
    public static extern IntPtr new_RailArrayRoomInfoListFilter__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilter__SWIG_3")]
    public static extern IntPtr new_RailArrayRoomInfoListFilter__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_SetValue")]
    public static extern IntPtr RailArrayRoomInfoListFilter_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRoomInfoListFilter")]
    public static extern void delete_RailArrayRoomInfoListFilter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_assign")]
    public static extern void RailArrayRoomInfoListFilter_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_buf")]
    public static extern IntPtr RailArrayRoomInfoListFilter_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_size")]
    public static extern uint RailArrayRoomInfoListFilter_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_Item")]
    public static extern IntPtr RailArrayRoomInfoListFilter_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_resize")]
    public static extern void RailArrayRoomInfoListFilter_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_push_back")]
    public static extern void RailArrayRoomInfoListFilter_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_clear")]
    public static extern void RailArrayRoomInfoListFilter_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_erase")]
    public static extern void RailArrayRoomInfoListFilter_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilter__SWIG_0")]
    public static extern IntPtr new_RailArrayGameServerListFilter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilter__SWIG_1")]
    public static extern IntPtr new_RailArrayGameServerListFilter__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilter__SWIG_2")]
    public static extern IntPtr new_RailArrayGameServerListFilter__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilter__SWIG_3")]
    public static extern IntPtr new_RailArrayGameServerListFilter__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_SetValue")]
    public static extern IntPtr RailArrayGameServerListFilter_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayGameServerListFilter")]
    public static extern void delete_RailArrayGameServerListFilter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_assign")]
    public static extern void RailArrayGameServerListFilter_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_buf")]
    public static extern IntPtr RailArrayGameServerListFilter_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_size")]
    public static extern uint RailArrayGameServerListFilter_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_Item")]
    public static extern IntPtr RailArrayGameServerListFilter_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_resize")]
    public static extern void RailArrayGameServerListFilter_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_push_back")]
    public static extern void RailArrayGameServerListFilter_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_clear")]
    public static extern void RailArrayGameServerListFilter_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_erase")]
    public static extern void RailArrayGameServerListFilter_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailSpaceWorkType__SWIG_0")]
    public static extern IntPtr new_RailArrayEnumRailSpaceWorkType__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailSpaceWorkType__SWIG_1")]
    public static extern IntPtr new_RailArrayEnumRailSpaceWorkType__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailSpaceWorkType__SWIG_2")]
    public static extern IntPtr new_RailArrayEnumRailSpaceWorkType__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailSpaceWorkType__SWIG_3")]
    public static extern IntPtr new_RailArrayEnumRailSpaceWorkType__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_SetValue")]
    public static extern IntPtr RailArrayEnumRailSpaceWorkType_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayEnumRailSpaceWorkType")]
    public static extern void delete_RailArrayEnumRailSpaceWorkType(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_assign")]
    public static extern void RailArrayEnumRailSpaceWorkType_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_buf")]
    public static extern IntPtr RailArrayEnumRailSpaceWorkType_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_size")]
    public static extern uint RailArrayEnumRailSpaceWorkType_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_Item")]
    public static extern IntPtr RailArrayEnumRailSpaceWorkType_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_resize")]
    public static extern void RailArrayEnumRailSpaceWorkType_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_push_back")]
    public static extern void RailArrayEnumRailSpaceWorkType_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_clear")]
    public static extern void RailArrayEnumRailSpaceWorkType_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_erase")]
    public static extern void RailArrayEnumRailSpaceWorkType_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailSpaceWorkType__SWIG_4")]
    public static extern IntPtr new_RailArrayEnumRailSpaceWorkType__SWIG_4(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArraySpaceWorkID__SWIG_0")]
    public static extern IntPtr new_RailArraySpaceWorkID__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArraySpaceWorkID__SWIG_1")]
    public static extern IntPtr new_RailArraySpaceWorkID__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArraySpaceWorkID__SWIG_2")]
    public static extern IntPtr new_RailArraySpaceWorkID__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArraySpaceWorkID__SWIG_3")]
    public static extern IntPtr new_RailArraySpaceWorkID__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_SetValue")]
    public static extern IntPtr RailArraySpaceWorkID_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArraySpaceWorkID")]
    public static extern void delete_RailArraySpaceWorkID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_assign")]
    public static extern void RailArraySpaceWorkID_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_buf")]
    public static extern IntPtr RailArraySpaceWorkID_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_size")]
    public static extern uint RailArraySpaceWorkID_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_Item")]
    public static extern IntPtr RailArraySpaceWorkID_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_resize")]
    public static extern void RailArraySpaceWorkID_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_push_back")]
    public static extern void RailArraySpaceWorkID_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_clear")]
    public static extern void RailArraySpaceWorkID_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_erase")]
    public static extern void RailArraySpaceWorkID_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListSorter__SWIG_0")]
    public static extern IntPtr new_RailArrayRoomInfoListSorter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListSorter__SWIG_1")]
    public static extern IntPtr new_RailArrayRoomInfoListSorter__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListSorter__SWIG_2")]
    public static extern IntPtr new_RailArrayRoomInfoListSorter__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListSorter__SWIG_3")]
    public static extern IntPtr new_RailArrayRoomInfoListSorter__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_SetValue")]
    public static extern IntPtr RailArrayRoomInfoListSorter_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRoomInfoListSorter")]
    public static extern void delete_RailArrayRoomInfoListSorter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_assign")]
    public static extern void RailArrayRoomInfoListSorter_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_buf")]
    public static extern IntPtr RailArrayRoomInfoListSorter_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_size")]
    public static extern uint RailArrayRoomInfoListSorter_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_Item")]
    public static extern IntPtr RailArrayRoomInfoListSorter_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_resize")]
    public static extern void RailArrayRoomInfoListSorter_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_push_back")]
    public static extern void RailArrayRoomInfoListSorter_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_clear")]
    public static extern void RailArrayRoomInfoListSorter_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_erase")]
    public static extern void RailArrayRoomInfoListSorter_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValueResult__SWIG_0")]
    public static extern IntPtr new_RailArrayRailKeyValueResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValueResult__SWIG_1")]
    public static extern IntPtr new_RailArrayRailKeyValueResult__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValueResult__SWIG_2")]
    public static extern IntPtr new_RailArrayRailKeyValueResult__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValueResult__SWIG_3")]
    public static extern IntPtr new_RailArrayRailKeyValueResult__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_SetValue")]
    public static extern IntPtr RailArrayRailKeyValueResult_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailKeyValueResult")]
    public static extern void delete_RailArrayRailKeyValueResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_assign")]
    public static extern void RailArrayRailKeyValueResult_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_buf")]
    public static extern IntPtr RailArrayRailKeyValueResult_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_size")]
    public static extern uint RailArrayRailKeyValueResult_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_Item")]
    public static extern IntPtr RailArrayRailKeyValueResult_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_resize")]
    public static extern void RailArrayRailKeyValueResult_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_push_back")]
    public static extern void RailArrayRailKeyValueResult_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_clear")]
    public static extern void RailArrayRailKeyValueResult_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_erase")]
    public static extern void RailArrayRailKeyValueResult_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailString__SWIG_0")]
    public static extern IntPtr new_RailArrayRailString__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailString__SWIG_1")]
    public static extern IntPtr new_RailArrayRailString__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailString__SWIG_2")]
    public static extern IntPtr new_RailArrayRailString__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailString__SWIG_3")]
    public static extern IntPtr new_RailArrayRailString__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_SetValue")]
    public static extern IntPtr RailArrayRailString_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailString")]
    public static extern void delete_RailArrayRailString(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_assign")]
    public static extern void RailArrayRailString_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_buf")]
    public static extern IntPtr RailArrayRailString_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_size")]
    public static extern uint RailArrayRailString_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_Item")]
    public static extern IntPtr RailArrayRailString_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_resize")]
    public static extern void RailArrayRailString_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_push_back")]
    public static extern void RailArrayRailString_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_clear")]
    public static extern void RailArrayRailString_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_erase")]
    public static extern void RailArrayRailString_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailString__SWIG_4")]
    public static extern IntPtr new_RailArrayRailString__SWIG_4(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValue__SWIG_0")]
    public static extern IntPtr new_RailArrayRailKeyValue__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValue__SWIG_1")]
    public static extern IntPtr new_RailArrayRailKeyValue__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValue__SWIG_2")]
    public static extern IntPtr new_RailArrayRailKeyValue__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValue__SWIG_3")]
    public static extern IntPtr new_RailArrayRailKeyValue__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_SetValue")]
    public static extern IntPtr RailArrayRailKeyValue_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailKeyValue")]
    public static extern void delete_RailArrayRailKeyValue(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_assign")]
    public static extern void RailArrayRailKeyValue_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_buf")]
    public static extern IntPtr RailArrayRailKeyValue_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_size")]
    public static extern uint RailArrayRailKeyValue_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_Item")]
    public static extern IntPtr RailArrayRailKeyValue_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_resize")]
    public static extern void RailArrayRailKeyValue_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_push_back")]
    public static extern void RailArrayRailKeyValue_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_clear")]
    public static extern void RailArrayRailKeyValue_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_erase")]
    public static extern void RailArrayRailKeyValue_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkVoteDetail__SWIG_0")]
    public static extern IntPtr new_RailArrayRailSpaceWorkVoteDetail__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkVoteDetail__SWIG_1")]
    public static extern IntPtr new_RailArrayRailSpaceWorkVoteDetail__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkVoteDetail__SWIG_2")]
    public static extern IntPtr new_RailArrayRailSpaceWorkVoteDetail__SWIG_2(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkVoteDetail__SWIG_3")]
    public static extern IntPtr new_RailArrayRailSpaceWorkVoteDetail__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_SetValue")]
    public static extern IntPtr RailArrayRailSpaceWorkVoteDetail_SetValue(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailSpaceWorkVoteDetail")]
    public static extern void delete_RailArrayRailSpaceWorkVoteDetail(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_assign")]
    public static extern void RailArrayRailSpaceWorkVoteDetail_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_buf")]
    public static extern IntPtr RailArrayRailSpaceWorkVoteDetail_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_size")]
    public static extern uint RailArrayRailSpaceWorkVoteDetail_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_Item")]
    public static extern IntPtr RailArrayRailSpaceWorkVoteDetail_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_resize")]
    public static extern void RailArrayRailSpaceWorkVoteDetail_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_push_back")]
    public static extern void RailArrayRailSpaceWorkVoteDetail_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_clear")]
    public static extern void RailArrayRailSpaceWorkVoteDetail_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_erase")]
    public static extern void RailArrayRailSpaceWorkVoteDetail_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPurchaseProductInfo__SWIG_0")]
    public static extern IntPtr new_RailArrayRailPurchaseProductInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPurchaseProductInfo__SWIG_1")]
    public static extern IntPtr new_RailArrayRailPurchaseProductInfo__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPurchaseProductInfo__SWIG_2")]
    public static extern IntPtr new_RailArrayRailPurchaseProductInfo__SWIG_2(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPurchaseProductInfo__SWIG_3")]
    public static extern IntPtr new_RailArrayRailPurchaseProductInfo__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_SetValue")]
    public static extern IntPtr RailArrayRailPurchaseProductInfo_SetValue(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailPurchaseProductInfo")]
    public static extern void delete_RailArrayRailPurchaseProductInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_assign")]
    public static extern void RailArrayRailPurchaseProductInfo_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_buf")]
    public static extern IntPtr RailArrayRailPurchaseProductInfo_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_size")]
    public static extern uint RailArrayRailPurchaseProductInfo_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_Item")]
    public static extern IntPtr RailArrayRailPurchaseProductInfo_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_resize")]
    public static extern void RailArrayRailPurchaseProductInfo_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_push_back")]
    public static extern void RailArrayRailPurchaseProductInfo_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_clear")]
    public static extern void RailArrayRailPurchaseProductInfo_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_erase")]
    public static extern void RailArrayRailPurchaseProductInfo_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfo__SWIG_0")]
    public static extern IntPtr new_RailArrayRoomInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfo__SWIG_1")]
    public static extern IntPtr new_RailArrayRoomInfo__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfo__SWIG_2")]
    public static extern IntPtr new_RailArrayRoomInfo__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfo__SWIG_3")]
    public static extern IntPtr new_RailArrayRoomInfo__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_SetValue")]
    public static extern IntPtr RailArrayRoomInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRoomInfo")]
    public static extern void delete_RailArrayRoomInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_assign")]
    public static extern void RailArrayRoomInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_buf")]
    public static extern IntPtr RailArrayRoomInfo_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_size")]
    public static extern uint RailArrayRoomInfo_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_Item")]
    public static extern IntPtr RailArrayRoomInfo_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_resize")]
    public static extern void RailArrayRoomInfo_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_push_back")]
    public static extern void RailArrayRoomInfo_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_clear")]
    public static extern void RailArrayRoomInfo_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_erase")]
    public static extern void RailArrayRoomInfo_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListSorter__SWIG_0")]
    public static extern IntPtr new_RailArrayGameServerListSorter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListSorter__SWIG_1")]
    public static extern IntPtr new_RailArrayGameServerListSorter__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListSorter__SWIG_2")]
    public static extern IntPtr new_RailArrayGameServerListSorter__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListSorter__SWIG_3")]
    public static extern IntPtr new_RailArrayGameServerListSorter__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_SetValue")]
    public static extern IntPtr RailArrayGameServerListSorter_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayGameServerListSorter")]
    public static extern void delete_RailArrayGameServerListSorter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_assign")]
    public static extern void RailArrayGameServerListSorter_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_buf")]
    public static extern IntPtr RailArrayGameServerListSorter_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_size")]
    public static extern uint RailArrayGameServerListSorter_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_Item")]
    public static extern IntPtr RailArrayGameServerListSorter_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_resize")]
    public static extern void RailArrayGameServerListSorter_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_push_back")]
    public static extern void RailArrayGameServerListSorter_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_clear")]
    public static extern void RailArrayGameServerListSorter_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_erase")]
    public static extern void RailArrayGameServerListSorter_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcID__SWIG_0")]
    public static extern IntPtr new_RailArrayRailDlcID__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcID__SWIG_1")]
    public static extern IntPtr new_RailArrayRailDlcID__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcID__SWIG_2")]
    public static extern IntPtr new_RailArrayRailDlcID__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcID__SWIG_3")]
    public static extern IntPtr new_RailArrayRailDlcID__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_SetValue")]
    public static extern IntPtr RailArrayRailDlcID_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailDlcID")]
    public static extern void delete_RailArrayRailDlcID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_assign")]
    public static extern void RailArrayRailDlcID_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_buf")]
    public static extern IntPtr RailArrayRailDlcID_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_size")]
    public static extern uint RailArrayRailDlcID_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_Item")]
    public static extern IntPtr RailArrayRailDlcID_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_resize")]
    public static extern void RailArrayRailDlcID_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_push_back")]
    public static extern void RailArrayRailDlcID_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_clear")]
    public static extern void RailArrayRailDlcID_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_erase")]
    public static extern void RailArrayRailDlcID_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailID__SWIG_0")]
    public static extern IntPtr new_RailArrayRailID__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailID__SWIG_1")]
    public static extern IntPtr new_RailArrayRailID__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailID__SWIG_2")]
    public static extern IntPtr new_RailArrayRailID__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailID__SWIG_3")]
    public static extern IntPtr new_RailArrayRailID__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_SetValue")]
    public static extern IntPtr RailArrayRailID_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailID")]
    public static extern void delete_RailArrayRailID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_assign")]
    public static extern void RailArrayRailID_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_buf")]
    public static extern IntPtr RailArrayRailID_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_size")]
    public static extern uint RailArrayRailID_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_Item")]
    public static extern IntPtr RailArrayRailID_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_resize")]
    public static extern void RailArrayRailID_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_push_back")]
    public static extern void RailArrayRailID_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_clear")]
    public static extern void RailArrayRailID_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_erase")]
    public static extern void RailArrayRailID_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailUserPlayedWith__SWIG_0")]
    public static extern IntPtr new_RailArrayRailUserPlayedWith__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailUserPlayedWith__SWIG_1")]
    public static extern IntPtr new_RailArrayRailUserPlayedWith__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailUserPlayedWith__SWIG_2")]
    public static extern IntPtr new_RailArrayRailUserPlayedWith__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailUserPlayedWith__SWIG_3")]
    public static extern IntPtr new_RailArrayRailUserPlayedWith__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_SetValue")]
    public static extern IntPtr RailArrayRailUserPlayedWith_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailUserPlayedWith")]
    public static extern void delete_RailArrayRailUserPlayedWith(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_assign")]
    public static extern void RailArrayRailUserPlayedWith_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_buf")]
    public static extern IntPtr RailArrayRailUserPlayedWith_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_size")]
    public static extern uint RailArrayRailUserPlayedWith_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_Item")]
    public static extern IntPtr RailArrayRailUserPlayedWith_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_resize")]
    public static extern void RailArrayRailUserPlayedWith_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_push_back")]
    public static extern void RailArrayRailUserPlayedWith_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_clear")]
    public static extern void RailArrayRailUserPlayedWith_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_erase")]
    public static extern void RailArrayRailUserPlayedWith_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerInfo__SWIG_0")]
    public static extern IntPtr new_RailArrayGameServerInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerInfo__SWIG_1")]
    public static extern IntPtr new_RailArrayGameServerInfo__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerInfo__SWIG_2")]
    public static extern IntPtr new_RailArrayGameServerInfo__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerInfo__SWIG_3")]
    public static extern IntPtr new_RailArrayGameServerInfo__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_SetValue")]
    public static extern IntPtr RailArrayGameServerInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayGameServerInfo")]
    public static extern void delete_RailArrayGameServerInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_assign")]
    public static extern void RailArrayGameServerInfo_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_buf")]
    public static extern IntPtr RailArrayGameServerInfo_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_size")]
    public static extern uint RailArrayGameServerInfo_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_Item")]
    public static extern IntPtr RailArrayGameServerInfo_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_resize")]
    public static extern void RailArrayGameServerInfo_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_push_back")]
    public static extern void RailArrayGameServerInfo_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_clear")]
    public static extern void RailArrayGameServerInfo_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_erase")]
    public static extern void RailArrayGameServerInfo_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilterKey__SWIG_0")]
    public static extern IntPtr new_RailArrayRoomInfoListFilterKey__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilterKey__SWIG_1")]
    public static extern IntPtr new_RailArrayRoomInfoListFilterKey__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilterKey__SWIG_2")]
    public static extern IntPtr new_RailArrayRoomInfoListFilterKey__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilterKey__SWIG_3")]
    public static extern IntPtr new_RailArrayRoomInfoListFilterKey__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_SetValue")]
    public static extern IntPtr RailArrayRoomInfoListFilterKey_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRoomInfoListFilterKey")]
    public static extern void delete_RailArrayRoomInfoListFilterKey(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_assign")]
    public static extern void RailArrayRoomInfoListFilterKey_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_buf")]
    public static extern IntPtr RailArrayRoomInfoListFilterKey_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_size")]
    public static extern uint RailArrayRoomInfoListFilterKey_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_Item")]
    public static extern IntPtr RailArrayRoomInfoListFilterKey_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_resize")]
    public static extern void RailArrayRoomInfoListFilterKey_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_push_back")]
    public static extern void RailArrayRoomInfoListFilterKey_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_clear")]
    public static extern void RailArrayRoomInfoListFilterKey_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_erase")]
    public static extern void RailArrayRoomInfoListFilterKey_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayMemberInfo__SWIG_0")]
    public static extern IntPtr new_RailArrayMemberInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayMemberInfo__SWIG_1")]
    public static extern IntPtr new_RailArrayMemberInfo__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayMemberInfo__SWIG_2")]
    public static extern IntPtr new_RailArrayMemberInfo__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayMemberInfo__SWIG_3")]
    public static extern IntPtr new_RailArrayMemberInfo__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_SetValue")]
    public static extern IntPtr RailArrayMemberInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayMemberInfo")]
    public static extern void delete_RailArrayMemberInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_assign")]
    public static extern void RailArrayMemberInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_buf")]
    public static extern IntPtr RailArrayMemberInfo_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_size")]
    public static extern uint RailArrayMemberInfo_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_Item")]
    public static extern IntPtr RailArrayMemberInfo_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_resize")]
    public static extern void RailArrayMemberInfo_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_push_back")]
    public static extern void RailArrayMemberInfo_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_clear")]
    public static extern void RailArrayMemberInfo_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_erase")]
    public static extern void RailArrayMemberInfo_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcOwned__SWIG_0")]
    public static extern IntPtr new_RailArrayRailDlcOwned__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcOwned__SWIG_1")]
    public static extern IntPtr new_RailArrayRailDlcOwned__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcOwned__SWIG_2")]
    public static extern IntPtr new_RailArrayRailDlcOwned__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcOwned__SWIG_3")]
    public static extern IntPtr new_RailArrayRailDlcOwned__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_SetValue")]
    public static extern IntPtr RailArrayRailDlcOwned_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailDlcOwned")]
    public static extern void delete_RailArrayRailDlcOwned(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_assign")]
    public static extern void RailArrayRailDlcOwned_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_buf")]
    public static extern IntPtr RailArrayRailDlcOwned_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_size")]
    public static extern uint RailArrayRailDlcOwned_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_Item")]
    public static extern IntPtr RailArrayRailDlcOwned_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_resize")]
    public static extern void RailArrayRailDlcOwned_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_push_back")]
    public static extern void RailArrayRailDlcOwned_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_clear")]
    public static extern void RailArrayRailDlcOwned_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_erase")]
    public static extern void RailArrayRailDlcOwned_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkDescriptor__SWIG_0")]
    public static extern IntPtr new_RailArrayRailSpaceWorkDescriptor__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkDescriptor__SWIG_1")]
    public static extern IntPtr new_RailArrayRailSpaceWorkDescriptor__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkDescriptor__SWIG_2")]
    public static extern IntPtr new_RailArrayRailSpaceWorkDescriptor__SWIG_2(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkDescriptor__SWIG_3")]
    public static extern IntPtr new_RailArrayRailSpaceWorkDescriptor__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_SetValue")]
    public static extern IntPtr RailArrayRailSpaceWorkDescriptor_SetValue(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailSpaceWorkDescriptor")]
    public static extern void delete_RailArrayRailSpaceWorkDescriptor(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_assign")]
    public static extern void RailArrayRailSpaceWorkDescriptor_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_buf")]
    public static extern IntPtr RailArrayRailSpaceWorkDescriptor_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_size")]
    public static extern uint RailArrayRailSpaceWorkDescriptor_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_Item")]
    public static extern IntPtr RailArrayRailSpaceWorkDescriptor_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_resize")]
    public static extern void RailArrayRailSpaceWorkDescriptor_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_push_back")]
    public static extern void RailArrayRailSpaceWorkDescriptor_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_clear")]
    public static extern void RailArrayRailSpaceWorkDescriptor_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_erase")]
    public static extern void RailArrayRailSpaceWorkDescriptor_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerPlayerInfo__SWIG_0")]
    public static extern IntPtr new_RailArrayGameServerPlayerInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerPlayerInfo__SWIG_1")]
    public static extern IntPtr new_RailArrayGameServerPlayerInfo__SWIG_1(uint jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerPlayerInfo__SWIG_2")]
    public static extern IntPtr new_RailArrayGameServerPlayerInfo__SWIG_2(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerPlayerInfo__SWIG_3")]
    public static extern IntPtr new_RailArrayGameServerPlayerInfo__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_SetValue")]
    public static extern IntPtr RailArrayGameServerPlayerInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayGameServerPlayerInfo")]
    public static extern void delete_RailArrayGameServerPlayerInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_assign")]
    public static extern void RailArrayGameServerPlayerInfo_assign(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_buf")]
    public static extern IntPtr RailArrayGameServerPlayerInfo_buf(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_size")]
    public static extern uint RailArrayGameServerPlayerInfo_size(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_Item")]
    public static extern IntPtr RailArrayGameServerPlayerInfo_Item(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_resize")]
    public static extern void RailArrayGameServerPlayerInfo_resize(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_push_back")]
    public static extern void RailArrayGameServerPlayerInfo_push_back(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_clear")]
    public static extern void RailArrayGameServerPlayerInfo_clear(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_erase")]
    public static extern void RailArrayGameServerPlayerInfo_erase(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsRequestAllAssetsFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsRequestAllAssetsFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsRequestAllAssetsFinished")]
    public static extern void delete_RailEventkRailEventAssetsRequestAllAssetsFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsRequestAllAssetsFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsRequestAllAssetsFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsRequestAllAssetsFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsRequestAllAssetsFinished_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsRequestAllAssetsFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsRequestAllAssetsFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsMergeToFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsMergeToFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsMergeToFinished")]
    public static extern void delete_RailEventkRailEventAssetsMergeToFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeToFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsMergeToFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeToFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsMergeToFinished_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsMergeToFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsMergeToFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSubscribeResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceSubscribeResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceSubscribeResult")]
    public static extern void delete_RailEventkRailEventUserSpaceSubscribeResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSubscribeResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUserSpaceSubscribeResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSubscribeResult_get_event_id")]
    public static extern int RailEventkRailEventUserSpaceSubscribeResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSubscribeResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceSubscribeResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMemberChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyMemberChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyMemberChanged")]
    public static extern void delete_RailEventkRailEventRoomNotifyMemberChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomNotifyMemberChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberChanged_get_event_id")]
    public static extern int RailEventkRailEventRoomNotifyMemberChanged_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMemberChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyMemberChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsStartConsumeFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsStartConsumeFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsStartConsumeFinished")]
    public static extern void delete_RailEventkRailEventAssetsStartConsumeFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsStartConsumeFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsStartConsumeFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsStartConsumeFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsStartConsumeFinished_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsStartConsumeFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsStartConsumeFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchasePurchaseProductsResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventInGamePurchasePurchaseProductsResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGamePurchasePurchaseProductsResult")]
    public static extern void delete_RailEventkRailEventInGamePurchasePurchaseProductsResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventInGamePurchasePurchaseProductsResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsResult_get_event_id")]
    public static extern int RailEventkRailEventInGamePurchasePurchaseProductsResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchasePurchaseProductsResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventInGamePurchasePurchaseProductsResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerListResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerListResult")]
    public static extern void delete_RailEventkRailEventGameServerListResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerListResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerListResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerListResult_get_event_id")]
    public static extern int RailEventkRailEventGameServerListResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerListResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerListResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomSetRoomMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomSetRoomMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomSetRoomMetadataResult")]
    public static extern void delete_RailEventkRailEventRoomSetRoomMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetRoomMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomSetRoomMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetRoomMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventRoomSetRoomMetadataResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomSetRoomMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomSetRoomMetadataResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserCloseResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventBrowserCloseResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserCloseResult")]
    public static extern void delete_RailEventkRailEventBrowserCloseResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCloseResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventBrowserCloseResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCloseResult_get_event_id")]
    public static extern int RailEventkRailEventBrowserCloseResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserCloseResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventBrowserCloseResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetworkCreateSessionRequest__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventNetworkCreateSessionRequest__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetworkCreateSessionRequest")]
    public static extern void delete_RailEventkRailEventNetworkCreateSessionRequest(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionRequest_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventNetworkCreateSessionRequest_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionRequest_get_event_id")]
    public static extern int RailEventkRailEventNetworkCreateSessionRequest_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetworkCreateSessionRequest__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventNetworkCreateSessionRequest__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsExchangeAssetsToFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsExchangeAssetsToFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsExchangeAssetsToFinished")]
    public static extern void delete_RailEventkRailEventAssetsExchangeAssetsToFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsToFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsExchangeAssetsToFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsToFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsExchangeAssetsToFinished_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsExchangeAssetsToFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsExchangeAssetsToFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByUser__SWIG_0")]
    public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByUser__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailPlatformNotifyEventJoinGameByUser")]
    public static extern void delete_RailEventkRailPlatformNotifyEventJoinGameByUser(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByUser_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailPlatformNotifyEventJoinGameByUser_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByUser_get_event_id")]
    public static extern int RailEventkRailPlatformNotifyEventJoinGameByUser_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByUser__SWIG_1")]
    public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByUser__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallProgress__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventDlcInstallProgress__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcInstallProgress")]
    public static extern void delete_RailEventkRailEventDlcInstallProgress(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallProgress_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventDlcInstallProgress_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallProgress_get_event_id")]
    public static extern int RailEventkRailEventDlcInstallProgress_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallProgress__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventDlcInstallProgress__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersNotifyInviter__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUsersNotifyInviter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersNotifyInviter")]
    public static extern void delete_RailEventkRailEventUsersNotifyInviter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersNotifyInviter_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUsersNotifyInviter_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersNotifyInviter_get_event_id")]
    public static extern int RailEventkRailEventUsersNotifyInviter_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersNotifyInviter__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUsersNotifyInviter__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardAttachSpaceWork__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventLeaderboardAttachSpaceWork__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventLeaderboardAttachSpaceWork")]
    public static extern void delete_RailEventkRailEventLeaderboardAttachSpaceWork(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardAttachSpaceWork_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventLeaderboardAttachSpaceWork_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardAttachSpaceWork_get_event_id")]
    public static extern int RailEventkRailEventLeaderboardAttachSpaceWork_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardAttachSpaceWork__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventLeaderboardAttachSpaceWork__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetUserRoomListResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomGetUserRoomListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomGetUserRoomListResult")]
    public static extern void delete_RailEventkRailEventRoomGetUserRoomListResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetUserRoomListResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomGetUserRoomListResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetUserRoomListResult_get_event_id")]
    public static extern int RailEventkRailEventRoomGetUserRoomListResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetUserRoomListResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomGetUserRoomListResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserTryNavigateNewPageRequest__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventBrowserTryNavigateNewPageRequest__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserTryNavigateNewPageRequest")]
    public static extern void delete_RailEventkRailEventBrowserTryNavigateNewPageRequest(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTryNavigateNewPageRequest_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventBrowserTryNavigateNewPageRequest_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTryNavigateNewPageRequest_get_event_id")]
    public static extern int RailEventkRailEventBrowserTryNavigateNewPageRequest_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserTryNavigateNewPageRequest__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventBrowserTryNavigateNewPageRequest__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcUninstallFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventDlcUninstallFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcUninstallFinished")]
    public static extern void delete_RailEventkRailEventDlcUninstallFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcUninstallFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventDlcUninstallFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcUninstallFinished_get_event_id")]
    public static extern int RailEventkRailEventDlcUninstallFinished_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcUninstallFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventDlcUninstallFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsSplitFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsSplitFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsSplitFinished")]
    public static extern void delete_RailEventkRailEventAssetsSplitFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsSplitFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsSplitFinished_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsSplitFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsSplitFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardUploaded__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventLeaderboardUploaded__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventLeaderboardUploaded")]
    public static extern void delete_RailEventkRailEventLeaderboardUploaded(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardUploaded_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventLeaderboardUploaded_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardUploaded_get_event_id")]
    public static extern int RailEventkRailEventLeaderboardUploaded_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardUploaded__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventLeaderboardUploaded__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelInviteMemmberResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventNetChannelInviteMemmberResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetChannelInviteMemmberResult")]
    public static extern void delete_RailEventkRailEventNetChannelInviteMemmberResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelInviteMemmberResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventNetChannelInviteMemmberResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelInviteMemmberResult_get_event_id")]
    public static extern int RailEventkRailEventNetChannelInviteMemmberResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelInviteMemmberResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventNetChannelInviteMemmberResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerCreated__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerCreated__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerCreated")]
    public static extern void delete_RailEventkRailEventGameServerCreated(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerCreated_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerCreated_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerCreated_get_event_id")]
    public static extern int RailEventkRailEventGameServerCreated_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerCreated__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerCreated__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallStart__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventDlcInstallStart__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcInstallStart")]
    public static extern void delete_RailEventkRailEventDlcInstallStart(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStart_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventDlcInstallStart_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStart_get_event_id")]
    public static extern int RailEventkRailEventDlcInstallStart_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallStart__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventDlcInstallStart__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersGetInviteDetailResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUsersGetInviteDetailResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersGetInviteDetailResult")]
    public static extern void delete_RailEventkRailEventUsersGetInviteDetailResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetInviteDetailResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUsersGetInviteDetailResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetInviteDetailResult_get_event_id")]
    public static extern int RailEventkRailEventUsersGetInviteDetailResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersGetInviteDetailResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUsersGetInviteDetailResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceQuerySpaceWorksResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceQuerySpaceWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceQuerySpaceWorksResult")]
    public static extern void delete_RailEventkRailEventUserSpaceQuerySpaceWorksResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceQuerySpaceWorksResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUserSpaceQuerySpaceWorksResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceQuerySpaceWorksResult_get_event_id")]
    public static extern int RailEventkRailEventUserSpaceQuerySpaceWorksResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceQuerySpaceWorksResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceQuerySpaceWorksResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementGlobalAchievementReceived__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAchievementGlobalAchievementReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAchievementGlobalAchievementReceived")]
    public static extern void delete_RailEventkRailEventAchievementGlobalAchievementReceived(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementGlobalAchievementReceived_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAchievementGlobalAchievementReceived_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementGlobalAchievementReceived_get_event_id")]
    public static extern int RailEventkRailEventAchievementGlobalAchievementReceived_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementGlobalAchievementReceived__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAchievementGlobalAchievementReceived__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncRenameStreamFileResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncRenameStreamFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncRenameStreamFileResult")]
    public static extern void delete_RailEventkRailEventStorageAsyncRenameStreamFileResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncRenameStreamFileResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStorageAsyncRenameStreamFileResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncRenameStreamFileResult_get_event_id")]
    public static extern int RailEventkRailEventStorageAsyncRenameStreamFileResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncRenameStreamFileResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncRenameStreamFileResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsReportPlayedWithUserListResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventFriendsReportPlayedWithUserListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsReportPlayedWithUserListResult")]
    public static extern void delete_RailEventkRailEventFriendsReportPlayedWithUserListResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsReportPlayedWithUserListResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventFriendsReportPlayedWithUserListResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsReportPlayedWithUserListResult_get_event_id")]
    public static extern int RailEventkRailEventFriendsReportPlayedWithUserListResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsReportPlayedWithUserListResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventFriendsReportPlayedWithUserListResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncWriteStreamFileResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncWriteStreamFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncWriteStreamFileResult")]
    public static extern void delete_RailEventkRailEventStorageAsyncWriteStreamFileResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteStreamFileResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStorageAsyncWriteStreamFileResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteStreamFileResult_get_event_id")]
    public static extern int RailEventkRailEventStorageAsyncWriteStreamFileResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncWriteStreamFileResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncWriteStreamFileResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomKickOffMemberResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomKickOffMemberResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomKickOffMemberResult")]
    public static extern void delete_RailEventkRailEventRoomKickOffMemberResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomKickOffMemberResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomKickOffMemberResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomKickOffMemberResult_get_event_id")]
    public static extern int RailEventkRailEventRoomKickOffMemberResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomKickOffMemberResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomKickOffMemberResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventShowFloatingWindow__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventShowFloatingWindow__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventShowFloatingWindow")]
    public static extern void delete_RailEventkRailEventShowFloatingWindow(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingWindow_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventShowFloatingWindow_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingWindow_get_event_id")]
    public static extern int RailEventkRailEventShowFloatingWindow_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventShowFloatingWindow__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventShowFloatingWindow__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomOwnerChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomOwnerChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyRoomOwnerChanged")]
    public static extern void delete_RailEventkRailEventRoomNotifyRoomOwnerChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomOwnerChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomNotifyRoomOwnerChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomOwnerChanged_get_event_id")]
    public static extern int RailEventkRailEventRoomNotifyRoomOwnerChanged_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomOwnerChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomOwnerChanged__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFinalize__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventFinalize__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFinalize")]
    public static extern void delete_RailEventkRailEventFinalize(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFinalize_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventFinalize_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFinalize_get_event_id")]
    public static extern int RailEventkRailEventFinalize_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFinalize__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventFinalize__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersCancelInviteResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUsersCancelInviteResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersCancelInviteResult")]
    public static extern void delete_RailEventkRailEventUsersCancelInviteResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersCancelInviteResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUsersCancelInviteResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersCancelInviteResult_get_event_id")]
    public static extern int RailEventkRailEventUsersCancelInviteResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersCancelInviteResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUsersCancelInviteResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomListResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomListResult")]
    public static extern void delete_RailEventkRailEventRoomListResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomListResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomListResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomListResult_get_event_id")]
    public static extern int RailEventkRailEventRoomListResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomListResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomListResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserDamageRectPaint__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventBrowserDamageRectPaint__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserDamageRectPaint")]
    public static extern void delete_RailEventkRailEventBrowserDamageRectPaint(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserDamageRectPaint_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventBrowserDamageRectPaint_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserDamageRectPaint_get_event_id")]
    public static extern int RailEventkRailEventBrowserDamageRectPaint_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserDamageRectPaint__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventBrowserDamageRectPaint__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsUpdateAssetPropertyFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsUpdateAssetPropertyFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsUpdateAssetPropertyFinished")]
    public static extern void delete_RailEventkRailEventAssetsUpdateAssetPropertyFinished(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateAssetPropertyFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsUpdateAssetPropertyFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateAssetPropertyFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsUpdateAssetPropertyFinished_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsUpdateAssetPropertyFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsUpdateAssetPropertyFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventPlayerGetGamePurchaseKey__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventPlayerGetGamePurchaseKey__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventPlayerGetGamePurchaseKey")]
    public static extern void delete_RailEventkRailEventPlayerGetGamePurchaseKey(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerGetGamePurchaseKey_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventPlayerGetGamePurchaseKey_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerGetGamePurchaseKey_get_event_id")]
    public static extern int RailEventkRailEventPlayerGetGamePurchaseKey_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventPlayerGetGamePurchaseKey__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventPlayerGetGamePurchaseKey__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByRoom__SWIG_0")]
    public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByRoom__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailPlatformNotifyEventJoinGameByRoom")]
    public static extern void delete_RailEventkRailPlatformNotifyEventJoinGameByRoom(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByRoom_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailPlatformNotifyEventJoinGameByRoom_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByRoom_get_event_id")]
    public static extern int RailEventkRailPlatformNotifyEventJoinGameByRoom_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByRoom__SWIG_1")]
    public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByRoom__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomSetMemberMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomSetMemberMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomSetMemberMetadataResult")]
    public static extern void delete_RailEventkRailEventRoomSetMemberMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetMemberMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomSetMemberMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetMemberMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventRoomSetMemberMetadataResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomSetMemberMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomSetMemberMetadataResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceModifyFavoritesWorksResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceModifyFavoritesWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceModifyFavoritesWorksResult")]
    public static extern void delete_RailEventkRailEventUserSpaceModifyFavoritesWorksResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceModifyFavoritesWorksResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUserSpaceModifyFavoritesWorksResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceModifyFavoritesWorksResult_get_event_id")]
    public static extern int RailEventkRailEventUserSpaceModifyFavoritesWorksResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceModifyFavoritesWorksResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceModifyFavoritesWorksResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelCreateResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventVoiceChannelCreateResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelCreateResult")]
    public static extern void delete_RailEventkRailEventVoiceChannelCreateResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelCreateResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventVoiceChannelCreateResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelCreateResult_get_event_id")]
    public static extern int RailEventkRailEventVoiceChannelCreateResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelCreateResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventVoiceChannelCreateResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsDirectConsumeFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsDirectConsumeFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsDirectConsumeFinished")]
    public static extern void delete_RailEventkRailEventAssetsDirectConsumeFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsDirectConsumeFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsDirectConsumeFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsDirectConsumeFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsDirectConsumeFinished_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsDirectConsumeFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsDirectConsumeFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomZoneListResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomZoneListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomZoneListResult")]
    public static extern void delete_RailEventkRailEventRoomZoneListResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomZoneListResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomZoneListResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomZoneListResult_get_event_id")]
    public static extern int RailEventkRailEventRoomZoneListResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomZoneListResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomZoneListResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult")]
    public static extern void delete_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_get_event_id")]
    public static extern int RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallStartResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventDlcInstallStartResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcInstallStartResult")]
    public static extern void delete_RailEventkRailEventDlcInstallStartResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStartResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventDlcInstallStartResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStartResult_get_event_id")]
    public static extern int RailEventkRailEventDlcInstallStartResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallStartResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventDlcInstallStartResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomDestroyed__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomDestroyed__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyRoomDestroyed")]
    public static extern void delete_RailEventkRailEventRoomNotifyRoomDestroyed(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDestroyed_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomNotifyRoomDestroyed_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDestroyed_get_event_id")]
    public static extern int RailEventkRailEventRoomNotifyRoomDestroyed_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomDestroyed__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomDestroyed__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserPaint__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventBrowserPaint__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserPaint")]
    public static extern void delete_RailEventkRailEventBrowserPaint(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserPaint_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventBrowserPaint_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserPaint_get_event_id")]
    public static extern int RailEventkRailEventBrowserPaint_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserPaint__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventBrowserPaint__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsExchangeAssetsFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsExchangeAssetsFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsExchangeAssetsFinished")]
    public static extern void delete_RailEventkRailEventAssetsExchangeAssetsFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsExchangeAssetsFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsExchangeAssetsFinished_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsExchangeAssetsFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsExchangeAssetsFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived")]
    public static extern void delete_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_get_event_id")]
    public static extern int RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsClearMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventFriendsClearMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsClearMetadataResult")]
    public static extern void delete_RailEventkRailEventFriendsClearMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsClearMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventFriendsClearMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsClearMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventFriendsClearMetadataResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsClearMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventFriendsClearMetadataResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelChannelNetDelay__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventNetChannelChannelNetDelay__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetChannelChannelNetDelay")]
    public static extern void delete_RailEventkRailEventNetChannelChannelNetDelay(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelChannelNetDelay_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventNetChannelChannelNetDelay_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelChannelNetDelay_get_event_id")]
    public static extern int RailEventkRailEventNetChannelChannelNetDelay_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelChannelNetDelay__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventNetChannelChannelNetDelay__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseAllProductsInfoReceived__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventInGamePurchaseAllProductsInfoReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGamePurchaseAllProductsInfoReceived")]
    public static extern void delete_RailEventkRailEventInGamePurchaseAllProductsInfoReceived(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllProductsInfoReceived_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventInGamePurchaseAllProductsInfoReceived_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllProductsInfoReceived_get_event_id")]
    public static extern int RailEventkRailEventInGamePurchaseAllProductsInfoReceived_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseAllProductsInfoReceived__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventInGamePurchaseAllProductsInfoReceived__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsMergeFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsMergeFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsMergeFinished")]
    public static extern void delete_RailEventkRailEventAssetsMergeFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsMergeFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsMergeFinished_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsMergeFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsMergeFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMemberkicked__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyMemberkicked__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyMemberkicked")]
    public static extern void delete_RailEventkRailEventRoomNotifyMemberkicked(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberkicked_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomNotifyMemberkicked_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberkicked_get_event_id")]
    public static extern int RailEventkRailEventRoomNotifyMemberkicked_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMemberkicked__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyMemberkicked__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomJoinRoomResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomJoinRoomResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomJoinRoomResult")]
    public static extern void delete_RailEventkRailEventRoomJoinRoomResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomJoinRoomResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomJoinRoomResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomJoinRoomResult_get_event_id")]
    public static extern int RailEventkRailEventRoomJoinRoomResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomJoinRoomResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomJoinRoomResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerAuthSessionTicket__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerAuthSessionTicket__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerAuthSessionTicket")]
    public static extern void delete_RailEventkRailEventGameServerAuthSessionTicket(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAuthSessionTicket_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerAuthSessionTicket_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAuthSessionTicket_get_event_id")]
    public static extern int RailEventkRailEventGameServerAuthSessionTicket_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerAuthSessionTicket__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerAuthSessionTicket__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerRegisterToServerListResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerRegisterToServerListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerRegisterToServerListResult")]
    public static extern void delete_RailEventkRailEventGameServerRegisterToServerListResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRegisterToServerListResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerRegisterToServerListResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRegisterToServerListResult_get_event_id")]
    public static extern int RailEventkRailEventGameServerRegisterToServerListResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerRegisterToServerListResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerRegisterToServerListResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePaymentResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePaymentResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGameStorePurchasePaymentResult")]
    public static extern void delete_RailEventkRailEventInGameStorePurchasePaymentResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePaymentResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventInGameStorePurchasePaymentResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePaymentResult_get_event_id")]
    public static extern int RailEventkRailEventInGameStorePurchasePaymentResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePaymentResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePaymentResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcCheckAllDlcsStateReadyResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventDlcCheckAllDlcsStateReadyResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcCheckAllDlcsStateReadyResult")]
    public static extern void delete_RailEventkRailEventDlcCheckAllDlcsStateReadyResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcCheckAllDlcsStateReadyResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventDlcCheckAllDlcsStateReadyResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcCheckAllDlcsStateReadyResult_get_event_id")]
    public static extern int RailEventkRailEventDlcCheckAllDlcsStateReadyResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcCheckAllDlcsStateReadyResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventDlcCheckAllDlcsStateReadyResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotTakeScreenshotFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventScreenshotTakeScreenshotFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventScreenshotTakeScreenshotFinished")]
    public static extern void delete_RailEventkRailEventScreenshotTakeScreenshotFinished(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventScreenshotTakeScreenshotFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotFinished_get_event_id")]
    public static extern int RailEventkRailEventScreenshotTakeScreenshotFinished_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotTakeScreenshotFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventScreenshotTakeScreenshotFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserReloadResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventBrowserReloadResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserReloadResult")]
    public static extern void delete_RailEventkRailEventBrowserReloadResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserReloadResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventBrowserReloadResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserReloadResult_get_event_id")]
    public static extern int RailEventkRailEventBrowserReloadResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserReloadResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventBrowserReloadResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelInviteJoinChannelRequest__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventNetChannelInviteJoinChannelRequest__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetChannelInviteJoinChannelRequest")]
    public static extern void delete_RailEventkRailEventNetChannelInviteJoinChannelRequest(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelInviteJoinChannelRequest_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventNetChannelInviteJoinChannelRequest_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelInviteJoinChannelRequest_get_event_id")]
    public static extern int RailEventkRailEventNetChannelInviteJoinChannelRequest_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelInviteJoinChannelRequest__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventNetChannelInviteJoinChannelRequest__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult")]
    public static extern void delete_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_get_event_id")]
    public static extern int RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetRoomMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomGetRoomMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomGetRoomMetadataResult")]
    public static extern void delete_RailEventkRailEventRoomGetRoomMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetRoomMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomGetRoomMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetRoomMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventRoomGetRoomMetadataResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetRoomMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomGetRoomMetadataResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventShowFloatingNotifyWindow__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventShowFloatingNotifyWindow__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventShowFloatingNotifyWindow")]
    public static extern void delete_RailEventkRailEventShowFloatingNotifyWindow(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingNotifyWindow_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventShowFloatingNotifyWindow_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingNotifyWindow_get_event_id")]
    public static extern int RailEventkRailEventShowFloatingNotifyWindow_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventShowFloatingNotifyWindow__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventShowFloatingNotifyWindow__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePayWindowDisplayed__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePayWindowDisplayed__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGameStorePurchasePayWindowDisplayed")]
    public static extern void delete_RailEventkRailEventInGameStorePurchasePayWindowDisplayed(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowDisplayed_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventInGameStorePurchasePayWindowDisplayed_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowDisplayed_get_event_id")]
    public static extern int RailEventkRailEventInGameStorePurchasePayWindowDisplayed_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePayWindowDisplayed__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePayWindowDisplayed__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceVoteSpaceWorkResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceVoteSpaceWorkResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceVoteSpaceWorkResult")]
    public static extern void delete_RailEventkRailEventUserSpaceVoteSpaceWorkResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceVoteSpaceWorkResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUserSpaceVoteSpaceWorkResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceVoteSpaceWorkResult_get_event_id")]
    public static extern int RailEventkRailEventUserSpaceVoteSpaceWorkResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceVoteSpaceWorkResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceVoteSpaceWorkResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMetadataChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyMetadataChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyMetadataChanged")]
    public static extern void delete_RailEventkRailEventRoomNotifyMetadataChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMetadataChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomNotifyMetadataChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMetadataChanged_get_event_id")]
    public static extern int RailEventkRailEventRoomNotifyMetadataChanged_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMetadataChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyMetadataChanged__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerSetMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerSetMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerSetMetadataResult")]
    public static extern void delete_RailEventkRailEventGameServerSetMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerSetMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerSetMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerSetMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventGameServerSetMetadataResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerSetMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerSetMetadataResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersInviteUsersResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUsersInviteUsersResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersInviteUsersResult")]
    public static extern void delete_RailEventkRailEventUsersInviteUsersResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteUsersResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUsersInviteUsersResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteUsersResult_get_event_id")]
    public static extern int RailEventkRailEventUsersInviteUsersResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersInviteUsersResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUsersInviteUsersResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceUpdateMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceUpdateMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceUpdateMetadataResult")]
    public static extern void delete_RailEventkRailEventUserSpaceUpdateMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceUpdateMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUserSpaceUpdateMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceUpdateMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventUserSpaceUpdateMetadataResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceUpdateMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceUpdateMetadataResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsUpdateConsumeFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsUpdateConsumeFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsUpdateConsumeFinished")]
    public static extern void delete_RailEventkRailEventAssetsUpdateConsumeFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateConsumeFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsUpdateConsumeFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateConsumeFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsUpdateConsumeFinished_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsUpdateConsumeFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsUpdateConsumeFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceDataCaptured__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventVoiceDataCaptured__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceDataCaptured")]
    public static extern void delete_RailEventkRailEventVoiceDataCaptured(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceDataCaptured_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventVoiceDataCaptured_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceDataCaptured_get_event_id")]
    public static extern int RailEventkRailEventVoiceDataCaptured_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceDataCaptured__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventVoiceDataCaptured__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseFinishOrderResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventInGamePurchaseFinishOrderResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGamePurchaseFinishOrderResult")]
    public static extern void delete_RailEventkRailEventInGamePurchaseFinishOrderResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseFinishOrderResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventInGamePurchaseFinishOrderResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseFinishOrderResult_get_event_id")]
    public static extern int RailEventkRailEventInGamePurchaseFinishOrderResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseFinishOrderResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventInGamePurchaseFinishOrderResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelMemberStateChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventNetChannelMemberStateChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetChannelMemberStateChanged")]
    public static extern void delete_RailEventkRailEventNetChannelMemberStateChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelMemberStateChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventNetChannelMemberStateChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelMemberStateChanged_get_event_id")]
    public static extern int RailEventkRailEventNetChannelMemberStateChanged_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelMemberStateChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventNetChannelMemberStateChanged__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerFavoriteGameServers__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerFavoriteGameServers__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerFavoriteGameServers")]
    public static extern void delete_RailEventkRailEventGameServerFavoriteGameServers(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerFavoriteGameServers_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerFavoriteGameServers_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerFavoriteGameServers_get_event_id")]
    public static extern int RailEventkRailEventGameServerFavoriteGameServers_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerFavoriteGameServers__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerFavoriteGameServers__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserJavascriptEvent__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventBrowserJavascriptEvent__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserJavascriptEvent")]
    public static extern void delete_RailEventkRailEventBrowserJavascriptEvent(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserJavascriptEvent_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventBrowserJavascriptEvent_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserJavascriptEvent_get_event_id")]
    public static extern int RailEventkRailEventBrowserJavascriptEvent_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserJavascriptEvent__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventBrowserJavascriptEvent__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementPlayerAchievementReceived__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAchievementPlayerAchievementReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAchievementPlayerAchievementReceived")]
    public static extern void delete_RailEventkRailEventAchievementPlayerAchievementReceived(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementReceived_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAchievementPlayerAchievementReceived_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementReceived_get_event_id")]
    public static extern int RailEventkRailEventAchievementPlayerAchievementReceived_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementPlayerAchievementReceived__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAchievementPlayerAchievementReceived__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomLeaveRoomResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomLeaveRoomResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomLeaveRoomResult")]
    public static extern void delete_RailEventkRailEventRoomLeaveRoomResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomLeaveRoomResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomLeaveRoomResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomLeaveRoomResult_get_event_id")]
    public static extern int RailEventkRailEventRoomLeaveRoomResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomLeaveRoomResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomLeaveRoomResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomCreated__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomCreated__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomCreated")]
    public static extern void delete_RailEventkRailEventRoomCreated(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomCreated_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomCreated_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomCreated_get_event_id")]
    public static extern int RailEventkRailEventRoomCreated_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomCreated__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomCreated__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageQueryQuotaResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStorageQueryQuotaResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageQueryQuotaResult")]
    public static extern void delete_RailEventkRailEventStorageQueryQuotaResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageQueryQuotaResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStorageQueryQuotaResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageQueryQuotaResult_get_event_id")]
    public static extern int RailEventkRailEventStorageQueryQuotaResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageQueryQuotaResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStorageQueryQuotaResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcOwnershipChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventDlcOwnershipChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcOwnershipChanged")]
    public static extern void delete_RailEventkRailEventDlcOwnershipChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcOwnershipChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventDlcOwnershipChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcOwnershipChanged_get_event_id")]
    public static extern int RailEventkRailEventDlcOwnershipChanged_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcOwnershipChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventDlcOwnershipChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserNavigeteResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventBrowserNavigeteResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserNavigeteResult")]
    public static extern void delete_RailEventkRailEventBrowserNavigeteResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserNavigeteResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventBrowserNavigeteResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserNavigeteResult_get_event_id")]
    public static extern int RailEventkRailEventBrowserNavigeteResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserNavigeteResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventBrowserNavigeteResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsGetInviteCommandLine__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventFriendsGetInviteCommandLine__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsGetInviteCommandLine")]
    public static extern void delete_RailEventkRailEventFriendsGetInviteCommandLine(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetInviteCommandLine_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventFriendsGetInviteCommandLine_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetInviteCommandLine_get_event_id")]
    public static extern int RailEventkRailEventFriendsGetInviteCommandLine_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsGetInviteCommandLine__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventFriendsGetInviteCommandLine__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsNumOfPlayerReceived__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStatsNumOfPlayerReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStatsNumOfPlayerReceived")]
    public static extern void delete_RailEventkRailEventStatsNumOfPlayerReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsNumOfPlayerReceived_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStatsNumOfPlayerReceived_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsNumOfPlayerReceived_get_event_id")]
    public static extern int RailEventkRailEventStatsNumOfPlayerReceived_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsNumOfPlayerReceived__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStatsNumOfPlayerReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventDlcInstallFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcInstallFinished")]
    public static extern void delete_RailEventkRailEventDlcInstallFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventDlcInstallFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallFinished_get_event_id")]
    public static extern int RailEventkRailEventDlcInstallFinished_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventDlcInstallFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsGlobalStatsReceived__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStatsGlobalStatsReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStatsGlobalStatsReceived")]
    public static extern void delete_RailEventkRailEventStatsGlobalStatsReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsGlobalStatsReceived_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStatsGlobalStatsReceived_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsGlobalStatsReceived_get_event_id")]
    public static extern int RailEventkRailEventStatsGlobalStatsReceived_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsGlobalStatsReceived__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStatsGlobalStatsReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsNotifyBuddyListChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventFriendsNotifyBuddyListChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsNotifyBuddyListChanged")]
    public static extern void delete_RailEventkRailEventFriendsNotifyBuddyListChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsNotifyBuddyListChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventFriendsNotifyBuddyListChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsNotifyBuddyListChanged_get_event_id")]
    public static extern int RailEventkRailEventFriendsNotifyBuddyListChanged_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsNotifyBuddyListChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventFriendsNotifyBuddyListChanged__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSearchSpaceWorkResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceSearchSpaceWorkResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceSearchSpaceWorkResult")]
    public static extern void delete_RailEventkRailEventUserSpaceSearchSpaceWorkResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSearchSpaceWorkResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUserSpaceSearchSpaceWorkResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSearchSpaceWorkResult_get_event_id")]
    public static extern int RailEventkRailEventUserSpaceSearchSpaceWorkResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSearchSpaceWorkResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceSearchSpaceWorkResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished")]
    public static extern void delete_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetMemberMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomGetMemberMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomGetMemberMetadataResult")]
    public static extern void delete_RailEventkRailEventRoomGetMemberMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetMemberMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomGetMemberMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetMemberMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventRoomGetMemberMetadataResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetMemberMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomGetMemberMetadataResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsGetMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventFriendsGetMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsGetMetadataResult")]
    public static extern void delete_RailEventkRailEventFriendsGetMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventFriendsGetMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventFriendsGetMetadataResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsGetMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventFriendsGetMetadataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncWriteFileResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncWriteFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncWriteFileResult")]
    public static extern void delete_RailEventkRailEventStorageAsyncWriteFileResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteFileResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStorageAsyncWriteFileResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteFileResult_get_event_id")]
    public static extern int RailEventkRailEventStorageAsyncWriteFileResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncWriteFileResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncWriteFileResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsPlayerStatsReceived__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStatsPlayerStatsReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStatsPlayerStatsReceived")]
    public static extern void delete_RailEventkRailEventStatsPlayerStatsReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsReceived_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStatsPlayerStatsReceived_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsReceived_get_event_id")]
    public static extern int RailEventkRailEventStatsPlayerStatsReceived_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsPlayerStatsReceived__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStatsPlayerStatsReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsCompleteConsumeFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsCompleteConsumeFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsCompleteConsumeFinished")]
    public static extern void delete_RailEventkRailEventAssetsCompleteConsumeFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsCompleteConsumeFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsCompleteConsumeFinished_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsCompleteConsumeFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsCompleteConsumeFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerGetMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerGetMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerGetMetadataResult")]
    public static extern void delete_RailEventkRailEventGameServerGetMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerGetMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventGameServerGetMetadataResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerGetMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerGetMetadataResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementPlayerAchievementStored__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAchievementPlayerAchievementStored__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAchievementPlayerAchievementStored")]
    public static extern void delete_RailEventkRailEventAchievementPlayerAchievementStored(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementStored_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAchievementPlayerAchievementStored_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementStored_get_event_id")]
    public static extern int RailEventkRailEventAchievementPlayerAchievementStored_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementPlayerAchievementStored__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAchievementPlayerAchievementStored__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePayWindowClosed__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePayWindowClosed__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGameStorePurchasePayWindowClosed")]
    public static extern void delete_RailEventkRailEventInGameStorePurchasePayWindowClosed(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowClosed_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventInGameStorePurchasePayWindowClosed_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowClosed_get_event_id")]
    public static extern int RailEventkRailEventInGameStorePurchasePayWindowClosed_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePayWindowClosed__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePayWindowClosed__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncReadStreamFileResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncReadStreamFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncReadStreamFileResult")]
    public static extern void delete_RailEventkRailEventStorageAsyncReadStreamFileResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadStreamFileResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStorageAsyncReadStreamFileResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadStreamFileResult_get_event_id")]
    public static extern int RailEventkRailEventStorageAsyncReadStreamFileResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncReadStreamFileResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncReadStreamFileResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserTitleChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventBrowserTitleChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserTitleChanged")]
    public static extern void delete_RailEventkRailEventBrowserTitleChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTitleChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventBrowserTitleChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTitleChanged_get_event_id")]
    public static extern int RailEventkRailEventBrowserTitleChanged_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserTitleChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventBrowserTitleChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceGetMySubscribedWorksResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceGetMySubscribedWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceGetMySubscribedWorksResult")]
    public static extern void delete_RailEventkRailEventUserSpaceGetMySubscribedWorksResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMySubscribedWorksResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUserSpaceGetMySubscribedWorksResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMySubscribedWorksResult_get_event_id")]
    public static extern int RailEventkRailEventUserSpaceGetMySubscribedWorksResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceGetMySubscribedWorksResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceGetMySubscribedWorksResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageShareToSpaceWorkResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStorageShareToSpaceWorkResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageShareToSpaceWorkResult")]
    public static extern void delete_RailEventkRailEventStorageShareToSpaceWorkResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageShareToSpaceWorkResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStorageShareToSpaceWorkResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageShareToSpaceWorkResult_get_event_id")]
    public static extern int RailEventkRailEventStorageShareToSpaceWorkResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageShareToSpaceWorkResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStorageShareToSpaceWorkResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelJoinChannelResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventNetChannelJoinChannelResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetChannelJoinChannelResult")]
    public static extern void delete_RailEventkRailEventNetChannelJoinChannelResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelJoinChannelResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventNetChannelJoinChannelResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelJoinChannelResult_get_event_id")]
    public static extern int RailEventkRailEventNetChannelJoinChannelResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelJoinChannelResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventNetChannelJoinChannelResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUtilsGetImageDataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUtilsGetImageDataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUtilsGetImageDataResult")]
    public static extern void delete_RailEventkRailEventUtilsGetImageDataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUtilsGetImageDataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUtilsGetImageDataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUtilsGetImageDataResult_get_event_id")]
    public static extern int RailEventkRailEventUtilsGetImageDataResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUtilsGetImageDataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUtilsGetImageDataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSystemStateChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventSystemStateChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventSystemStateChanged")]
    public static extern void delete_RailEventkRailEventSystemStateChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSystemStateChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventSystemStateChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSystemStateChanged_get_event_id")]
    public static extern int RailEventkRailEventSystemStateChanged_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSystemStateChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventSystemStateChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardReceived__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventLeaderboardReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventLeaderboardReceived")]
    public static extern void delete_RailEventkRailEventLeaderboardReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardReceived_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventLeaderboardReceived_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardReceived_get_event_id")]
    public static extern int RailEventkRailEventLeaderboardReceived_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardReceived__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventLeaderboardReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomClearRoomMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomClearRoomMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomClearRoomMetadataResult")]
    public static extern void delete_RailEventkRailEventRoomClearRoomMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomClearRoomMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomClearRoomMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomClearRoomMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventRoomClearRoomMetadataResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomClearRoomMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomClearRoomMetadataResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetAllDataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomGetAllDataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomGetAllDataResult")]
    public static extern void delete_RailEventkRailEventRoomGetAllDataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetAllDataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomGetAllDataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetAllDataResult_get_event_id")]
    public static extern int RailEventkRailEventRoomGetAllDataResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetAllDataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomGetAllDataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncListStreamFileResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncListStreamFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncListStreamFileResult")]
    public static extern void delete_RailEventkRailEventStorageAsyncListStreamFileResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncListStreamFileResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStorageAsyncListStreamFileResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncListStreamFileResult_get_event_id")]
    public static extern int RailEventkRailEventStorageAsyncListStreamFileResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncListStreamFileResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncListStreamFileResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardEntryReceived__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventLeaderboardEntryReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventLeaderboardEntryReceived")]
    public static extern void delete_RailEventkRailEventLeaderboardEntryReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardEntryReceived_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventLeaderboardEntryReceived_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardEntryReceived_get_event_id")]
    public static extern int RailEventkRailEventLeaderboardEntryReceived_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardEntryReceived__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventLeaderboardEntryReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomGameServerChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomGameServerChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyRoomGameServerChanged")]
    public static extern void delete_RailEventkRailEventRoomNotifyRoomGameServerChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomGameServerChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomNotifyRoomGameServerChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomGameServerChanged_get_event_id")]
    public static extern int RailEventkRailEventRoomNotifyRoomGameServerChanged_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomGameServerChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomGameServerChanged__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomDataReceived__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomDataReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyRoomDataReceived")]
    public static extern void delete_RailEventkRailEventRoomNotifyRoomDataReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDataReceived_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomNotifyRoomDataReceived_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDataReceived_get_event_id")]
    public static extern int RailEventkRailEventRoomNotifyRoomDataReceived_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomDataReceived__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomDataReceived__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcQueryIsOwnedDlcsResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventDlcQueryIsOwnedDlcsResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcQueryIsOwnedDlcsResult")]
    public static extern void delete_RailEventkRailEventDlcQueryIsOwnedDlcsResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcQueryIsOwnedDlcsResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventDlcQueryIsOwnedDlcsResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcQueryIsOwnedDlcsResult_get_event_id")]
    public static extern int RailEventkRailEventDlcQueryIsOwnedDlcsResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcQueryIsOwnedDlcsResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventDlcQueryIsOwnedDlcsResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSyncResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceSyncResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceSyncResult")]
    public static extern void delete_RailEventkRailEventUserSpaceSyncResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSyncResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUserSpaceSyncResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSyncResult_get_event_id")]
    public static extern int RailEventkRailEventUserSpaceSyncResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSyncResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceSyncResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcRefundChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventDlcRefundChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcRefundChanged")]
    public static extern void delete_RailEventkRailEventDlcRefundChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcRefundChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventDlcRefundChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcRefundChanged_get_event_id")]
    public static extern int RailEventkRailEventDlcRefundChanged_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcRefundChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventDlcRefundChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerPlayerListResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerPlayerListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerPlayerListResult")]
    public static extern void delete_RailEventkRailEventGameServerPlayerListResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerPlayerListResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerPlayerListResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerPlayerListResult_get_event_id")]
    public static extern int RailEventkRailEventGameServerPlayerListResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerPlayerListResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerPlayerListResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersGetUsersInfo__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUsersGetUsersInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersGetUsersInfo")]
    public static extern void delete_RailEventkRailEventUsersGetUsersInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetUsersInfo_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUsersGetUsersInfo_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetUsersInfo_get_event_id")]
    public static extern int RailEventkRailEventUsersGetUsersInfo_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersGetUsersInfo__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUsersGetUsersInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncDeleteStreamFileResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncDeleteStreamFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncDeleteStreamFileResult")]
    public static extern void delete_RailEventkRailEventStorageAsyncDeleteStreamFileResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncDeleteStreamFileResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStorageAsyncDeleteStreamFileResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncDeleteStreamFileResult_get_event_id")]
    public static extern int RailEventkRailEventStorageAsyncDeleteStreamFileResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncDeleteStreamFileResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncDeleteStreamFileResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsSetMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventFriendsSetMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsSetMetadataResult")]
    public static extern void delete_RailEventkRailEventFriendsSetMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsSetMetadataResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventFriendsSetMetadataResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsSetMetadataResult_get_event_id")]
    public static extern int RailEventkRailEventFriendsSetMetadataResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsSetMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventFriendsSetMetadataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserCreateResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventBrowserCreateResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserCreateResult")]
    public static extern void delete_RailEventkRailEventBrowserCreateResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCreateResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventBrowserCreateResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCreateResult_get_event_id")]
    public static extern int RailEventkRailEventBrowserCreateResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserCreateResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventBrowserCreateResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsSplitToFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAssetsSplitToFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsSplitToFinished")]
    public static extern void delete_RailEventkRailEventAssetsSplitToFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitToFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAssetsSplitToFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitToFinished_get_event_id")]
    public static extern int RailEventkRailEventAssetsSplitToFinished_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsSplitToFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAssetsSplitToFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotPublishScreenshotFinished__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventScreenshotPublishScreenshotFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventScreenshotPublishScreenshotFinished")]
    public static extern void delete_RailEventkRailEventScreenshotPublishScreenshotFinished(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotPublishScreenshotFinished_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventScreenshotPublishScreenshotFinished_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotPublishScreenshotFinished_get_event_id")]
    public static extern int RailEventkRailEventScreenshotPublishScreenshotFinished_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotPublishScreenshotFinished__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventScreenshotPublishScreenshotFinished__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelCreateChannelResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventNetChannelCreateChannelResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetChannelCreateChannelResult")]
    public static extern void delete_RailEventkRailEventNetChannelCreateChannelResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelCreateChannelResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventNetChannelCreateChannelResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelCreateChannelResult_get_event_id")]
    public static extern int RailEventkRailEventNetChannelCreateChannelResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelCreateChannelResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventNetChannelCreateChannelResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerAddFavoriteGameServer__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerAddFavoriteGameServer__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerAddFavoriteGameServer")]
    public static extern void delete_RailEventkRailEventGameServerAddFavoriteGameServer(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAddFavoriteGameServer_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerAddFavoriteGameServer_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAddFavoriteGameServer_get_event_id")]
    public static extern int RailEventkRailEventGameServerAddFavoriteGameServer_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerAddFavoriteGameServer__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerAddFavoriteGameServer__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSessionTicketGetSessionTicket__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventSessionTicketGetSessionTicket__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventSessionTicketGetSessionTicket")]
    public static extern void delete_RailEventkRailEventSessionTicketGetSessionTicket(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketGetSessionTicket_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventSessionTicketGetSessionTicket_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketGetSessionTicket_get_event_id")]
    public static extern int RailEventkRailEventSessionTicketGetSessionTicket_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSessionTicketGetSessionTicket__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventSessionTicketGetSessionTicket__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerRemoveFavoriteGameServer__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerRemoveFavoriteGameServer__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerRemoveFavoriteGameServer")]
    public static extern void delete_RailEventkRailEventGameServerRemoveFavoriteGameServer(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRemoveFavoriteGameServer_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerRemoveFavoriteGameServer_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRemoveFavoriteGameServer_get_event_id")]
    public static extern int RailEventkRailEventGameServerRemoveFavoriteGameServer_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerRemoveFavoriteGameServer__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerRemoveFavoriteGameServer__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGotRoomMembers__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventRoomGotRoomMembers__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomGotRoomMembers")]
    public static extern void delete_RailEventkRailEventRoomGotRoomMembers(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGotRoomMembers_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventRoomGotRoomMembers_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGotRoomMembers_get_event_id")]
    public static extern int RailEventkRailEventRoomGotRoomMembers_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGotRoomMembers__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventRoomGotRoomMembers__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsPlayerStatsStored__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStatsPlayerStatsStored__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStatsPlayerStatsStored")]
    public static extern void delete_RailEventkRailEventStatsPlayerStatsStored(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsStored_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStatsPlayerStatsStored_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsStored_get_event_id")]
    public static extern int RailEventkRailEventStatsPlayerStatsStored_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsPlayerStatsStored__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStatsPlayerStatsStored__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncReadFileResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncReadFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncReadFileResult")]
    public static extern void delete_RailEventkRailEventStorageAsyncReadFileResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadFileResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventStorageAsyncReadFileResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadFileResult_get_event_id")]
    public static extern int RailEventkRailEventStorageAsyncReadFileResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncReadFileResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventStorageAsyncReadFileResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSessionTicketAuthSessionTicket__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventSessionTicketAuthSessionTicket__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventSessionTicketAuthSessionTicket")]
    public static extern void delete_RailEventkRailEventSessionTicketAuthSessionTicket(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketAuthSessionTicket_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventSessionTicketAuthSessionTicket_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketAuthSessionTicket_get_event_id")]
    public static extern int RailEventkRailEventSessionTicketAuthSessionTicket_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSessionTicketAuthSessionTicket__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventSessionTicketAuthSessionTicket__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotTakeScreenshotRequest__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventScreenshotTakeScreenshotRequest__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventScreenshotTakeScreenshotRequest")]
    public static extern void delete_RailEventkRailEventScreenshotTakeScreenshotRequest(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotRequest_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventScreenshotTakeScreenshotRequest_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotRequest_get_event_id")]
    public static extern int RailEventkRailEventScreenshotTakeScreenshotRequest_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotTakeScreenshotRequest__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventScreenshotTakeScreenshotRequest__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceRemoveSpaceWorkResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceRemoveSpaceWorkResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceRemoveSpaceWorkResult")]
    public static extern void delete_RailEventkRailEventUserSpaceRemoveSpaceWorkResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceRemoveSpaceWorkResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUserSpaceRemoveSpaceWorkResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceRemoveSpaceWorkResult_get_event_id")]
    public static extern int RailEventkRailEventUserSpaceRemoveSpaceWorkResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceRemoveSpaceWorkResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUserSpaceRemoveSpaceWorkResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserStateChanged__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventBrowserStateChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserStateChanged")]
    public static extern void delete_RailEventkRailEventBrowserStateChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserStateChanged_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventBrowserStateChanged_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserStateChanged_get_event_id")]
    public static extern int RailEventkRailEventBrowserStateChanged_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserStateChanged__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventBrowserStateChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerGetSessionTicket__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventGameServerGetSessionTicket__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerGetSessionTicket")]
    public static extern void delete_RailEventkRailEventGameServerGetSessionTicket(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetSessionTicket_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventGameServerGetSessionTicket_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetSessionTicket_get_event_id")]
    public static extern int RailEventkRailEventGameServerGetSessionTicket_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerGetSessionTicket__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventGameServerGetSessionTicket__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersRespondInvation__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUsersRespondInvation__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersRespondInvation")]
    public static extern void delete_RailEventkRailEventUsersRespondInvation(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersRespondInvation_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUsersRespondInvation_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersRespondInvation_get_event_id")]
    public static extern int RailEventkRailEventUsersRespondInvation_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersRespondInvation__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUsersRespondInvation__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelChannelException__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventNetChannelChannelException__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetChannelChannelException")]
    public static extern void delete_RailEventkRailEventNetChannelChannelException(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelChannelException_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventNetChannelChannelException_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelChannelException_get_event_id")]
    public static extern int RailEventkRailEventNetChannelChannelException_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetChannelChannelException__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventNetChannelChannelException__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersInviteJoinGameResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventUsersInviteJoinGameResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersInviteJoinGameResult")]
    public static extern void delete_RailEventkRailEventUsersInviteJoinGameResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteJoinGameResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventUsersInviteJoinGameResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteJoinGameResult_get_event_id")]
    public static extern int RailEventkRailEventUsersInviteJoinGameResult_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersInviteJoinGameResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventUsersInviteJoinGameResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAppQuerySubscribeWishPlayStateResult__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventAppQuerySubscribeWishPlayStateResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAppQuerySubscribeWishPlayStateResult")]
    public static extern void delete_RailEventkRailEventAppQuerySubscribeWishPlayStateResult(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAppQuerySubscribeWishPlayStateResult_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventAppQuerySubscribeWishPlayStateResult_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAppQuerySubscribeWishPlayStateResult_get_event_id")]
    public static extern int RailEventkRailEventAppQuerySubscribeWishPlayStateResult_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAppQuerySubscribeWishPlayStateResult__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventAppQuerySubscribeWishPlayStateResult__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetworkCreateSessionFailed__SWIG_0")]
    public static extern IntPtr new_RailEventkRailEventNetworkCreateSessionFailed__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetworkCreateSessionFailed")]
    public static extern void delete_RailEventkRailEventNetworkCreateSessionFailed(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionFailed_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailEventNetworkCreateSessionFailed_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionFailed_get_event_id")]
    public static extern int RailEventkRailEventNetworkCreateSessionFailed_get_event_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetworkCreateSessionFailed__SWIG_1")]
    public static extern IntPtr new_RailEventkRailEventNetworkCreateSessionFailed__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByGameServer__SWIG_0")]
    public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByGameServer__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailPlatformNotifyEventJoinGameByGameServer")]
    public static extern void delete_RailEventkRailPlatformNotifyEventJoinGameByGameServer(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByGameServer_kInternalRailEventEventId_get")]
    public static extern int RailEventkRailPlatformNotifyEventJoinGameByGameServer_kInternalRailEventEventId_get();

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByGameServer_get_event_id")]
    public static extern int RailEventkRailPlatformNotifyEventJoinGameByGameServer_get_event_id(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByGameServer__SWIG_1")]
    public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByGameServer__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailBranchInfo__SWIG_0")]
    public static extern IntPtr new_RailBranchInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_name_set")]
    public static extern void RailBranchInfo_branch_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_name_get")]
    public static extern string RailBranchInfo_branch_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_type_set")]
    public static extern void RailBranchInfo_branch_type_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_type_get")]
    public static extern string RailBranchInfo_branch_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_id_set")]
    public static extern void RailBranchInfo_branch_id_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_id_get")]
    public static extern string RailBranchInfo_branch_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_build_number_set")]
    public static extern void RailBranchInfo_build_number_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_build_number_get")]
    public static extern string RailBranchInfo_build_number_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailBranchInfo__SWIG_1")]
    public static extern IntPtr new_RailBranchInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailBranchInfo")]
    public static extern void delete_RailBranchInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_QuerySubscribeWishPlayStateResult__SWIG_0")]
    public static extern IntPtr new_QuerySubscribeWishPlayStateResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_QuerySubscribeWishPlayStateResult_is_subscribed_set")]
    public static extern void QuerySubscribeWishPlayStateResult_is_subscribed_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_QuerySubscribeWishPlayStateResult_is_subscribed_get")]
    public static extern bool QuerySubscribeWishPlayStateResult_is_subscribed_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_QuerySubscribeWishPlayStateResult__SWIG_1")]
    public static extern IntPtr new_QuerySubscribeWishPlayStateResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_QuerySubscribeWishPlayStateResult")]
    public static extern void delete_QuerySubscribeWishPlayStateResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SpaceWorkID__SWIG_0")]
    public static extern IntPtr new_SpaceWorkID__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_SpaceWorkID__SWIG_1")]
    public static extern IntPtr new_SpaceWorkID__SWIG_1(ulong jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SpaceWorkID_set_id")]
    public static extern void SpaceWorkID_set_id(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SpaceWorkID_get_id")]
    public static extern ulong SpaceWorkID_get_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SpaceWorkID_IsValid")]
    public static extern bool SpaceWorkID_IsValid(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SpaceWorkID_id__set")]
    public static extern void SpaceWorkID_id__set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SpaceWorkID_id__get")]
    public static extern ulong SpaceWorkID_id__get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SpaceWorkID__SWIG_2")]
    public static extern IntPtr new_SpaceWorkID__SWIG_2(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_SpaceWorkID")]
    public static extern void delete_SpaceWorkID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_subscriber_list_set")]
    public static extern void RailSpaceWorkFilter_subscriber_list_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_subscriber_list_get")]
    public static extern IntPtr RailSpaceWorkFilter_subscriber_list_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_creator_list_set")]
    public static extern void RailSpaceWorkFilter_creator_list_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_creator_list_get")]
    public static extern IntPtr RailSpaceWorkFilter_creator_list_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_type_set")]
    public static extern void RailSpaceWorkFilter_type_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_type_get")]
    public static extern IntPtr RailSpaceWorkFilter_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_collector_list_set")]
    public static extern void RailSpaceWorkFilter_collector_list_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_collector_list_get")]
    public static extern IntPtr RailSpaceWorkFilter_collector_list_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_classes_set")]
    public static extern void RailSpaceWorkFilter_classes_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_classes_get")]
    public static extern IntPtr RailSpaceWorkFilter_classes_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkFilter__SWIG_0")]
    public static extern IntPtr new_RailSpaceWorkFilter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkFilter__SWIG_1")]
    public static extern IntPtr new_RailSpaceWorkFilter__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkFilter")]
    public static extern void delete_RailSpaceWorkFilter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_url_set")]
    public static extern void RailQueryWorkFileOptions_with_url_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_url_get")]
    public static extern bool RailQueryWorkFileOptions_with_url_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_description_set")]
    public static extern void RailQueryWorkFileOptions_with_description_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_description_get")]
    public static extern bool RailQueryWorkFileOptions_with_description_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_query_total_only_set")]
    public static extern void RailQueryWorkFileOptions_query_total_only_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_query_total_only_get")]
    public static extern bool RailQueryWorkFileOptions_query_total_only_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_uploader_ids_set")]
    public static extern void RailQueryWorkFileOptions_with_uploader_ids_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_uploader_ids_get")]
    public static extern bool RailQueryWorkFileOptions_with_uploader_ids_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_preveiw_url_set")]
    public static extern void RailQueryWorkFileOptions_with_preveiw_url_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_preveiw_url_get")]
    public static extern bool RailQueryWorkFileOptions_with_preveiw_url_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_vote_detail_set")]
    public static extern void RailQueryWorkFileOptions_with_vote_detail_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_vote_detail_get")]
    public static extern bool RailQueryWorkFileOptions_with_vote_detail_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailQueryWorkFileOptions__SWIG_0")]
    public static extern IntPtr new_RailQueryWorkFileOptions__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailQueryWorkFileOptions__SWIG_1")]
    public static extern IntPtr new_RailQueryWorkFileOptions__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailQueryWorkFileOptions")]
    public static extern void delete_RailQueryWorkFileOptions(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_finished_bytes_set")]
    public static extern void RailSpaceWorkSyncProgress_finished_bytes_set(
      IntPtr jarg1,
      ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_finished_bytes_get")]
    public static extern ulong RailSpaceWorkSyncProgress_finished_bytes_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_total_bytes_set")]
    public static extern void RailSpaceWorkSyncProgress_total_bytes_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_total_bytes_get")]
    public static extern ulong RailSpaceWorkSyncProgress_total_bytes_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_progress_set")]
    public static extern void RailSpaceWorkSyncProgress_progress_set(IntPtr jarg1, float jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_progress_get")]
    public static extern float RailSpaceWorkSyncProgress_progress_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_current_state_set")]
    public static extern void RailSpaceWorkSyncProgress_current_state_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_current_state_get")]
    public static extern int RailSpaceWorkSyncProgress_current_state_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkSyncProgress__SWIG_0")]
    public static extern IntPtr new_RailSpaceWorkSyncProgress__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkSyncProgress__SWIG_1")]
    public static extern IntPtr new_RailSpaceWorkSyncProgress__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkSyncProgress")]
    public static extern void delete_RailSpaceWorkSyncProgress(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkVoteDetail_vote_value_set")]
    public static extern void RailSpaceWorkVoteDetail_vote_value_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkVoteDetail_vote_value_get")]
    public static extern int RailSpaceWorkVoteDetail_vote_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkVoteDetail_voted_players_set")]
    public static extern void RailSpaceWorkVoteDetail_voted_players_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkVoteDetail_voted_players_get")]
    public static extern uint RailSpaceWorkVoteDetail_voted_players_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkVoteDetail__SWIG_0")]
    public static extern IntPtr new_RailSpaceWorkVoteDetail__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkVoteDetail__SWIG_1")]
    public static extern IntPtr new_RailSpaceWorkVoteDetail__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkVoteDetail")]
    public static extern void delete_RailSpaceWorkVoteDetail(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_id_set")]
    public static extern void RailSpaceWorkDescriptor_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_id_get")]
    public static extern IntPtr RailSpaceWorkDescriptor_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_name_set")]
    public static extern void RailSpaceWorkDescriptor_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_name_get")]
    public static extern string RailSpaceWorkDescriptor_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_description_set")]
    public static extern void RailSpaceWorkDescriptor_description_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_description_get")]
    public static extern string RailSpaceWorkDescriptor_description_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_detail_url_set")]
    public static extern void RailSpaceWorkDescriptor_detail_url_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_detail_url_get")]
    public static extern string RailSpaceWorkDescriptor_detail_url_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_uploader_ids_set")]
    public static extern void RailSpaceWorkDescriptor_uploader_ids_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_uploader_ids_get")]
    public static extern IntPtr RailSpaceWorkDescriptor_uploader_ids_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_preview_url_set")]
    public static extern void RailSpaceWorkDescriptor_preview_url_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_preview_url_get")]
    public static extern string RailSpaceWorkDescriptor_preview_url_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_vote_details_set")]
    public static extern void RailSpaceWorkDescriptor_vote_details_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_vote_details_get")]
    public static extern IntPtr RailSpaceWorkDescriptor_vote_details_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkDescriptor__SWIG_0")]
    public static extern IntPtr new_RailSpaceWorkDescriptor__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkDescriptor__SWIG_1")]
    public static extern IntPtr new_RailSpaceWorkDescriptor__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkDescriptor")]
    public static extern void delete_RailSpaceWorkDescriptor(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMySubscribedWorksResult_spacework_descriptors_set")]
    public static extern void AsyncGetMySubscribedWorksResult_spacework_descriptors_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMySubscribedWorksResult_spacework_descriptors_get")]
    public static extern IntPtr AsyncGetMySubscribedWorksResult_spacework_descriptors_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMySubscribedWorksResult_total_available_works_set")]
    public static extern void AsyncGetMySubscribedWorksResult_total_available_works_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMySubscribedWorksResult_total_available_works_get")]
    public static extern uint AsyncGetMySubscribedWorksResult_total_available_works_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetMySubscribedWorksResult__SWIG_0")]
    public static extern IntPtr new_AsyncGetMySubscribedWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetMySubscribedWorksResult__SWIG_1")]
    public static extern IntPtr new_AsyncGetMySubscribedWorksResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncGetMySubscribedWorksResult")]
    public static extern void delete_AsyncGetMySubscribedWorksResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMyFavoritesWorksResult_spacework_descriptors_set")]
    public static extern void AsyncGetMyFavoritesWorksResult_spacework_descriptors_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMyFavoritesWorksResult_spacework_descriptors_get")]
    public static extern IntPtr AsyncGetMyFavoritesWorksResult_spacework_descriptors_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMyFavoritesWorksResult_total_available_works_set")]
    public static extern void AsyncGetMyFavoritesWorksResult_total_available_works_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMyFavoritesWorksResult_total_available_works_get")]
    public static extern uint AsyncGetMyFavoritesWorksResult_total_available_works_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetMyFavoritesWorksResult__SWIG_0")]
    public static extern IntPtr new_AsyncGetMyFavoritesWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetMyFavoritesWorksResult__SWIG_1")]
    public static extern IntPtr new_AsyncGetMyFavoritesWorksResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncGetMyFavoritesWorksResult")]
    public static extern void delete_AsyncGetMyFavoritesWorksResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncQuerySpaceWorksResult_spacework_descriptors_set")]
    public static extern void AsyncQuerySpaceWorksResult_spacework_descriptors_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncQuerySpaceWorksResult_spacework_descriptors_get")]
    public static extern IntPtr AsyncQuerySpaceWorksResult_spacework_descriptors_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncQuerySpaceWorksResult_total_available_works_set")]
    public static extern void AsyncQuerySpaceWorksResult_total_available_works_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncQuerySpaceWorksResult_total_available_works_get")]
    public static extern uint AsyncQuerySpaceWorksResult_total_available_works_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncQuerySpaceWorksResult__SWIG_0")]
    public static extern IntPtr new_AsyncQuerySpaceWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncQuerySpaceWorksResult__SWIG_1")]
    public static extern IntPtr new_AsyncQuerySpaceWorksResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncQuerySpaceWorksResult")]
    public static extern void delete_AsyncQuerySpaceWorksResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncUpdateMetadataResult_id_set")]
    public static extern void AsyncUpdateMetadataResult_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncUpdateMetadataResult_id_get")]
    public static extern IntPtr AsyncUpdateMetadataResult_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncUpdateMetadataResult_type_set")]
    public static extern void AsyncUpdateMetadataResult_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncUpdateMetadataResult_type_get")]
    public static extern int AsyncUpdateMetadataResult_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncUpdateMetadataResult__SWIG_0")]
    public static extern IntPtr new_AsyncUpdateMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncUpdateMetadataResult__SWIG_1")]
    public static extern IntPtr new_AsyncUpdateMetadataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncUpdateMetadataResult")]
    public static extern void delete_AsyncUpdateMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SyncSpaceWorkResult_id_set")]
    public static extern void SyncSpaceWorkResult_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SyncSpaceWorkResult_id_get")]
    public static extern IntPtr SyncSpaceWorkResult_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SyncSpaceWorkResult__SWIG_0")]
    public static extern IntPtr new_SyncSpaceWorkResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_SyncSpaceWorkResult__SWIG_1")]
    public static extern IntPtr new_SyncSpaceWorkResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_SyncSpaceWorkResult")]
    public static extern void delete_SyncSpaceWorkResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_success_ids_set")]
    public static extern void AsyncSubscribeSpaceWorksResult_success_ids_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_success_ids_get")]
    public static extern IntPtr AsyncSubscribeSpaceWorksResult_success_ids_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_failure_ids_set")]
    public static extern void AsyncSubscribeSpaceWorksResult_failure_ids_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_failure_ids_get")]
    public static extern IntPtr AsyncSubscribeSpaceWorksResult_failure_ids_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_subscribe_set")]
    public static extern void AsyncSubscribeSpaceWorksResult_subscribe_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_subscribe_get")]
    public static extern bool AsyncSubscribeSpaceWorksResult_subscribe_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncSubscribeSpaceWorksResult__SWIG_0")]
    public static extern IntPtr new_AsyncSubscribeSpaceWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncSubscribeSpaceWorksResult__SWIG_1")]
    public static extern IntPtr new_AsyncSubscribeSpaceWorksResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncSubscribeSpaceWorksResult")]
    public static extern void delete_AsyncSubscribeSpaceWorksResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_success_ids_set")]
    public static extern void AsyncModifyFavoritesWorksResult_success_ids_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_success_ids_get")]
    public static extern IntPtr AsyncModifyFavoritesWorksResult_success_ids_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_failure_ids_set")]
    public static extern void AsyncModifyFavoritesWorksResult_failure_ids_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_failure_ids_get")]
    public static extern IntPtr AsyncModifyFavoritesWorksResult_failure_ids_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_modify_flag_set")]
    public static extern void AsyncModifyFavoritesWorksResult_modify_flag_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_modify_flag_get")]
    public static extern int AsyncModifyFavoritesWorksResult_modify_flag_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncModifyFavoritesWorksResult__SWIG_0")]
    public static extern IntPtr new_AsyncModifyFavoritesWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncModifyFavoritesWorksResult__SWIG_1")]
    public static extern IntPtr new_AsyncModifyFavoritesWorksResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncModifyFavoritesWorksResult")]
    public static extern void delete_AsyncModifyFavoritesWorksResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveSpaceWorkResult_id_set")]
    public static extern void AsyncRemoveSpaceWorkResult_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveSpaceWorkResult_id_get")]
    public static extern IntPtr AsyncRemoveSpaceWorkResult_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRemoveSpaceWorkResult__SWIG_0")]
    public static extern IntPtr new_AsyncRemoveSpaceWorkResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRemoveSpaceWorkResult__SWIG_1")]
    public static extern IntPtr new_AsyncRemoveSpaceWorkResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncRemoveSpaceWorkResult")]
    public static extern void delete_AsyncRemoveSpaceWorkResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncVoteSpaceWorkResult_id_set")]
    public static extern void AsyncVoteSpaceWorkResult_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncVoteSpaceWorkResult_id_get")]
    public static extern IntPtr AsyncVoteSpaceWorkResult_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncVoteSpaceWorkResult__SWIG_0")]
    public static extern IntPtr new_AsyncVoteSpaceWorkResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncVoteSpaceWorkResult__SWIG_1")]
    public static extern IntPtr new_AsyncVoteSpaceWorkResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncVoteSpaceWorkResult")]
    public static extern void delete_AsyncVoteSpaceWorkResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSearchSpaceWorksResult_spacework_descriptors_set")]
    public static extern void AsyncSearchSpaceWorksResult_spacework_descriptors_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSearchSpaceWorksResult_spacework_descriptors_get")]
    public static extern IntPtr AsyncSearchSpaceWorksResult_spacework_descriptors_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSearchSpaceWorksResult_total_available_works_set")]
    public static extern void AsyncSearchSpaceWorksResult_total_available_works_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSearchSpaceWorksResult_total_available_works_get")]
    public static extern uint AsyncSearchSpaceWorksResult_total_available_works_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncSearchSpaceWorksResult__SWIG_0")]
    public static extern IntPtr new_AsyncSearchSpaceWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncSearchSpaceWorksResult__SWIG_1")]
    public static extern IntPtr new_AsyncSearchSpaceWorksResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncSearchSpaceWorksResult")]
    public static extern void delete_AsyncSearchSpaceWorksResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_spacework_descriptors_set")]
    public static extern void QueryMySubscribedSpaceWorksResult_spacework_descriptors_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_spacework_descriptors_get")]
    public static extern IntPtr QueryMySubscribedSpaceWorksResult_spacework_descriptors_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_spacework_type_set")]
    public static extern void QueryMySubscribedSpaceWorksResult_spacework_type_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_spacework_type_get")]
    public static extern int QueryMySubscribedSpaceWorksResult_spacework_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_total_available_works_set")]
    public static extern void QueryMySubscribedSpaceWorksResult_total_available_works_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_total_available_works_get")]
    public static extern uint QueryMySubscribedSpaceWorksResult_total_available_works_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_QueryMySubscribedSpaceWorksResult__SWIG_0")]
    public static extern IntPtr new_QueryMySubscribedSpaceWorksResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_QueryMySubscribedSpaceWorksResult__SWIG_1")]
    public static extern IntPtr new_QueryMySubscribedSpaceWorksResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_QueryMySubscribedSpaceWorksResult")]
    public static extern void delete_QueryMySubscribedSpaceWorksResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_detail_set")]
    public static extern void RailSpaceWorkUpdateOptions_with_detail_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_detail_get")]
    public static extern bool RailSpaceWorkUpdateOptions_with_detail_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_metadata_set")]
    public static extern void RailSpaceWorkUpdateOptions_with_metadata_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_metadata_get")]
    public static extern bool RailSpaceWorkUpdateOptions_with_metadata_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_check_has_subscribed_set")]
    public static extern void RailSpaceWorkUpdateOptions_check_has_subscribed_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_check_has_subscribed_get")]
    public static extern bool RailSpaceWorkUpdateOptions_check_has_subscribed_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_check_has_favorited_set")]
    public static extern void RailSpaceWorkUpdateOptions_check_has_favorited_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_check_has_favorited_get")]
    public static extern bool RailSpaceWorkUpdateOptions_check_has_favorited_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_my_vote_set")]
    public static extern void RailSpaceWorkUpdateOptions_with_my_vote_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_my_vote_get")]
    public static extern bool RailSpaceWorkUpdateOptions_with_my_vote_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_vote_detail_set")]
    public static extern void RailSpaceWorkUpdateOptions_with_vote_detail_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_vote_detail_get")]
    public static extern bool RailSpaceWorkUpdateOptions_with_vote_detail_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkUpdateOptions__SWIG_0")]
    public static extern IntPtr new_RailSpaceWorkUpdateOptions__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkUpdateOptions__SWIG_1")]
    public static extern IntPtr new_RailSpaceWorkUpdateOptions__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkUpdateOptions")]
    public static extern void delete_RailSpaceWorkUpdateOptions(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_search_text_set")]
    public static extern void RailSpaceWorkSearchFilter_search_text_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_search_text_get")]
    public static extern string RailSpaceWorkSearchFilter_search_text_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_required_tags_set")]
    public static extern void RailSpaceWorkSearchFilter_required_tags_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_required_tags_get")]
    public static extern IntPtr RailSpaceWorkSearchFilter_required_tags_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_excluded_tags_set")]
    public static extern void RailSpaceWorkSearchFilter_excluded_tags_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_excluded_tags_get")]
    public static extern IntPtr RailSpaceWorkSearchFilter_excluded_tags_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkSearchFilter__SWIG_0")]
    public static extern IntPtr new_RailSpaceWorkSearchFilter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkSearchFilter__SWIG_1")]
    public static extern IntPtr new_RailSpaceWorkSearchFilter__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkSearchFilter")]
    public static extern void delete_RailSpaceWorkSearchFilter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_invite_type_set")]
    public static extern void RailInviteOptions_invite_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_invite_type_get")]
    public static extern int RailInviteOptions_invite_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_need_respond_in_game_set")]
    public static extern void RailInviteOptions_need_respond_in_game_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_need_respond_in_game_get")]
    public static extern bool RailInviteOptions_need_respond_in_game_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_additional_message_set")]
    public static extern void RailInviteOptions_additional_message_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_additional_message_get")]
    public static extern string RailInviteOptions_additional_message_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_expire_time_set")]
    public static extern void RailInviteOptions_expire_time_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_expire_time_get")]
    public static extern uint RailInviteOptions_expire_time_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInviteOptions__SWIG_0")]
    public static extern IntPtr new_RailInviteOptions__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInviteOptions__SWIG_1")]
    public static extern IntPtr new_RailInviteOptions__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailInviteOptions")]
    public static extern void delete_RailInviteOptions(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInfoData__SWIG_0")]
    public static extern IntPtr new_RailUsersInfoData__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInfoData_user_info_list_set")]
    public static extern void RailUsersInfoData_user_info_list_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInfoData_user_info_list_get")]
    public static extern IntPtr RailUsersInfoData_user_info_list_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInfoData__SWIG_1")]
    public static extern IntPtr new_RailUsersInfoData__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersInfoData")]
    public static extern void delete_RailUsersInfoData(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersNotifyInviter__SWIG_0")]
    public static extern IntPtr new_RailUsersNotifyInviter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersNotifyInviter_invitee_id_set")]
    public static extern void RailUsersNotifyInviter_invitee_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersNotifyInviter_invitee_id_get")]
    public static extern IntPtr RailUsersNotifyInviter_invitee_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersNotifyInviter__SWIG_1")]
    public static extern IntPtr new_RailUsersNotifyInviter__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersNotifyInviter")]
    public static extern void delete_RailUsersNotifyInviter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersRespondInvation__SWIG_0")]
    public static extern IntPtr new_RailUsersRespondInvation__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvation_inviter_id_set")]
    public static extern void RailUsersRespondInvation_inviter_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvation_inviter_id_get")]
    public static extern IntPtr RailUsersRespondInvation_inviter_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvation_response_set")]
    public static extern void RailUsersRespondInvation_response_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvation_response_get")]
    public static extern int RailUsersRespondInvation_response_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvation_original_invite_option_set")]
    public static extern void RailUsersRespondInvation_original_invite_option_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvation_original_invite_option_get")]
    public static extern IntPtr RailUsersRespondInvation_original_invite_option_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersRespondInvation__SWIG_1")]
    public static extern IntPtr new_RailUsersRespondInvation__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersRespondInvation")]
    public static extern void delete_RailUsersRespondInvation(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInviteJoinGameResult__SWIG_0")]
    public static extern IntPtr new_RailUsersInviteJoinGameResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_invitee_id_set")]
    public static extern void RailUsersInviteJoinGameResult_invitee_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_invitee_id_get")]
    public static extern IntPtr RailUsersInviteJoinGameResult_invitee_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_response_value_set")]
    public static extern void RailUsersInviteJoinGameResult_response_value_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_response_value_get")]
    public static extern int RailUsersInviteJoinGameResult_response_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_invite_type_set")]
    public static extern void RailUsersInviteJoinGameResult_invite_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_invite_type_get")]
    public static extern int RailUsersInviteJoinGameResult_invite_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInviteJoinGameResult__SWIG_1")]
    public static extern IntPtr new_RailUsersInviteJoinGameResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersInviteJoinGameResult")]
    public static extern void delete_RailUsersInviteJoinGameResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersGetInviteDetailResult__SWIG_0")]
    public static extern IntPtr new_RailUsersGetInviteDetailResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_inviter_id_set")]
    public static extern void RailUsersGetInviteDetailResult_inviter_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_inviter_id_get")]
    public static extern IntPtr RailUsersGetInviteDetailResult_inviter_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_command_line_set")]
    public static extern void RailUsersGetInviteDetailResult_command_line_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_command_line_get")]
    public static extern string RailUsersGetInviteDetailResult_command_line_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_invite_type_set")]
    public static extern void RailUsersGetInviteDetailResult_invite_type_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_invite_type_get")]
    public static extern int RailUsersGetInviteDetailResult_invite_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersGetInviteDetailResult__SWIG_1")]
    public static extern IntPtr new_RailUsersGetInviteDetailResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersGetInviteDetailResult")]
    public static extern void delete_RailUsersGetInviteDetailResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersCancelInviteResult__SWIG_0")]
    public static extern IntPtr new_RailUsersCancelInviteResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersCancelInviteResult_invite_type_set")]
    public static extern void RailUsersCancelInviteResult_invite_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersCancelInviteResult_invite_type_get")]
    public static extern int RailUsersCancelInviteResult_invite_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersCancelInviteResult__SWIG_1")]
    public static extern IntPtr new_RailUsersCancelInviteResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersCancelInviteResult")]
    public static extern void delete_RailUsersCancelInviteResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInviteUsersResult__SWIG_0")]
    public static extern IntPtr new_RailUsersInviteUsersResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteUsersResult_invite_type_set")]
    public static extern void RailUsersInviteUsersResult_invite_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteUsersResult_invite_type_get")]
    public static extern int RailUsersInviteUsersResult_invite_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInviteUsersResult__SWIG_1")]
    public static extern IntPtr new_RailUsersInviteUsersResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersInviteUsersResult")]
    public static extern void delete_RailUsersInviteUsersResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailImageDataDescriptor__SWIG_0")]
    public static extern IntPtr new_RailImageDataDescriptor__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_image_width_set")]
    public static extern void RailImageDataDescriptor_image_width_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_image_width_get")]
    public static extern uint RailImageDataDescriptor_image_width_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_image_height_set")]
    public static extern void RailImageDataDescriptor_image_height_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_image_height_get")]
    public static extern uint RailImageDataDescriptor_image_height_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_stride_in_bytes_set")]
    public static extern void RailImageDataDescriptor_stride_in_bytes_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_stride_in_bytes_get")]
    public static extern uint RailImageDataDescriptor_stride_in_bytes_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_bits_per_pixel_set")]
    public static extern void RailImageDataDescriptor_bits_per_pixel_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_bits_per_pixel_get")]
    public static extern uint RailImageDataDescriptor_bits_per_pixel_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_pixel_format_set")]
    public static extern void RailImageDataDescriptor_pixel_format_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_pixel_format_get")]
    public static extern int RailImageDataDescriptor_pixel_format_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailImageDataDescriptor__SWIG_1")]
    public static extern IntPtr new_RailImageDataDescriptor__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailImageDataDescriptor")]
    public static extern void delete_RailImageDataDescriptor(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailDirtyWordsCheckResult__SWIG_0")]
    public static extern IntPtr new_RailDirtyWordsCheckResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailDirtyWordsCheckResult_replace_string_set")]
    public static extern void RailDirtyWordsCheckResult_replace_string_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDirtyWordsCheckResult_replace_string_get")]
    public static extern string RailDirtyWordsCheckResult_replace_string_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDirtyWordsCheckResult_dirty_type_set")]
    public static extern void RailDirtyWordsCheckResult_dirty_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDirtyWordsCheckResult_dirty_type_get")]
    public static extern int RailDirtyWordsCheckResult_dirty_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailDirtyWordsCheckResult__SWIG_1")]
    public static extern IntPtr new_RailDirtyWordsCheckResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailDirtyWordsCheckResult")]
    public static extern void delete_RailDirtyWordsCheckResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailCrashInfo_exception_type_set")]
    public static extern void RailCrashInfo_exception_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailCrashInfo_exception_type_get")]
    public static extern int RailCrashInfo_exception_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailCrashInfo__SWIG_0")]
    public static extern IntPtr new_RailCrashInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailCrashInfo__SWIG_1")]
    public static extern IntPtr new_RailCrashInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailCrashInfo")]
    public static extern void delete_RailCrashInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_GetData")]
    public static extern string RailCrashBuffer_GetData(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_GetBufferLength")]
    public static extern uint RailCrashBuffer_GetBufferLength(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_GetValidLength")]
    public static extern uint RailCrashBuffer_GetValidLength(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_SetData__SWIG_0")]
    public static extern uint RailCrashBuffer_SetData__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      uint jarg3,
      uint jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_SetData__SWIG_1")]
    public static extern uint RailCrashBuffer_SetData__SWIG_1(
      IntPtr jarg1,
      string jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_AppendData")]
    public static extern uint RailCrashBuffer_AppendData(IntPtr jarg1, string jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailCrashBuffer")]
    public static extern void delete_RailCrashBuffer(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailGetImageDataResult__SWIG_0")]
    public static extern IntPtr new_RailGetImageDataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailGetImageDataResult_image_data_set")]
    public static extern void RailGetImageDataResult_image_data_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailGetImageDataResult_image_data_get")]
    public static extern string RailGetImageDataResult_image_data_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailGetImageDataResult_image_data_descriptor_set")]
    public static extern void RailGetImageDataResult_image_data_descriptor_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailGetImageDataResult_image_data_descriptor_get")]
    public static extern IntPtr RailGetImageDataResult_image_data_descriptor_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailGetImageDataResult__SWIG_1")]
    public static extern IntPtr new_RailGetImageDataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailGetImageDataResult")]
    public static extern void delete_RailGetImageDataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_TakeScreenshotResult__SWIG_0")]
    public static extern IntPtr new_TakeScreenshotResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_image_file_path_set")]
    public static extern void TakeScreenshotResult_image_file_path_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_image_file_path_get")]
    public static extern string TakeScreenshotResult_image_file_path_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_image_file_size_set")]
    public static extern void TakeScreenshotResult_image_file_size_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_image_file_size_get")]
    public static extern uint TakeScreenshotResult_image_file_size_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_thumbnail_filepath_set")]
    public static extern void TakeScreenshotResult_thumbnail_filepath_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_thumbnail_filepath_get")]
    public static extern string TakeScreenshotResult_thumbnail_filepath_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_thumbnail_file_size_set")]
    public static extern void TakeScreenshotResult_thumbnail_file_size_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_thumbnail_file_size_get")]
    public static extern uint TakeScreenshotResult_thumbnail_file_size_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_TakeScreenshotResult__SWIG_1")]
    public static extern IntPtr new_TakeScreenshotResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_TakeScreenshotResult")]
    public static extern void delete_TakeScreenshotResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ScreenshotRequestInfo__SWIG_0")]
    public static extern IntPtr new_ScreenshotRequestInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_ScreenshotRequestInfo__SWIG_1")]
    public static extern IntPtr new_ScreenshotRequestInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ScreenshotRequestInfo")]
    public static extern void delete_ScreenshotRequestInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PublishScreenshotResult__SWIG_0")]
    public static extern IntPtr new_PublishScreenshotResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_PublishScreenshotResult_work_id_set")]
    public static extern void PublishScreenshotResult_work_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PublishScreenshotResult_work_id_get")]
    public static extern IntPtr PublishScreenshotResult_work_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PublishScreenshotResult__SWIG_1")]
    public static extern IntPtr new_PublishScreenshotResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_PublishScreenshotResult")]
    public static extern void delete_PublishScreenshotResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSystemStateChanged__SWIG_0")]
    public static extern IntPtr new_RailSystemStateChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailSystemStateChanged_state_set")]
    public static extern void RailSystemStateChanged_state_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSystemStateChanged_state_get")]
    public static extern int RailSystemStateChanged_state_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSystemStateChanged__SWIG_1")]
    public static extern IntPtr new_RailSystemStateChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailSystemStateChanged")]
    public static extern void delete_RailSystemStateChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByGameServer__SWIG_0")]
    public static extern IntPtr new_RailPlatformNotifyEventJoinGameByGameServer__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_set")]
    public static extern void RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_get")]
    public static extern IntPtr RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByGameServer_commandline_info_set")]
    public static extern void RailPlatformNotifyEventJoinGameByGameServer_commandline_info_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByGameServer_commandline_info_get")]
    public static extern string RailPlatformNotifyEventJoinGameByGameServer_commandline_info_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByGameServer__SWIG_1")]
    public static extern IntPtr new_RailPlatformNotifyEventJoinGameByGameServer__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailPlatformNotifyEventJoinGameByGameServer")]
    public static extern void delete_RailPlatformNotifyEventJoinGameByGameServer(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByRoom__SWIG_0")]
    public static extern IntPtr new_RailPlatformNotifyEventJoinGameByRoom__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_zone_id_set")]
    public static extern void RailPlatformNotifyEventJoinGameByRoom_zone_id_set(
      IntPtr jarg1,
      ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_zone_id_get")]
    public static extern ulong RailPlatformNotifyEventJoinGameByRoom_zone_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_room_id_set")]
    public static extern void RailPlatformNotifyEventJoinGameByRoom_room_id_set(
      IntPtr jarg1,
      ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_room_id_get")]
    public static extern ulong RailPlatformNotifyEventJoinGameByRoom_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_commandline_info_set")]
    public static extern void RailPlatformNotifyEventJoinGameByRoom_commandline_info_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_commandline_info_get")]
    public static extern string RailPlatformNotifyEventJoinGameByRoom_commandline_info_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByRoom__SWIG_1")]
    public static extern IntPtr new_RailPlatformNotifyEventJoinGameByRoom__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailPlatformNotifyEventJoinGameByRoom")]
    public static extern void delete_RailPlatformNotifyEventJoinGameByRoom(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByUser__SWIG_0")]
    public static extern IntPtr new_RailPlatformNotifyEventJoinGameByUser__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_set")]
    public static extern void RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_get")]
    public static extern IntPtr RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByUser_commandline_info_set")]
    public static extern void RailPlatformNotifyEventJoinGameByUser_commandline_info_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByUser_commandline_info_get")]
    public static extern string RailPlatformNotifyEventJoinGameByUser_commandline_info_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByUser__SWIG_1")]
    public static extern IntPtr new_RailPlatformNotifyEventJoinGameByUser__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailPlatformNotifyEventJoinGameByUser")]
    public static extern void delete_RailPlatformNotifyEventJoinGameByUser(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFinalize__SWIG_0")]
    public static extern IntPtr new_RailFinalize__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFinalize__SWIG_1")]
    public static extern IntPtr new_RailFinalize__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailFinalize")]
    public static extern void delete_RailFinalize(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetInfo__SWIG_0")]
    public static extern IntPtr new_RailAssetInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_asset_id_set")]
    public static extern void RailAssetInfo_asset_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_asset_id_get")]
    public static extern ulong RailAssetInfo_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_product_id_set")]
    public static extern void RailAssetInfo_product_id_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_product_id_get")]
    public static extern uint RailAssetInfo_product_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_product_name_set")]
    public static extern void RailAssetInfo_product_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_product_name_get")]
    public static extern string RailAssetInfo_product_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_position_set")]
    public static extern void RailAssetInfo_position_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_position_get")]
    public static extern int RailAssetInfo_position_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_progress_set")]
    public static extern void RailAssetInfo_progress_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_progress_get")]
    public static extern string RailAssetInfo_progress_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_quantity_set")]
    public static extern void RailAssetInfo_quantity_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_quantity_get")]
    public static extern uint RailAssetInfo_quantity_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_state_set")]
    public static extern void RailAssetInfo_state_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_state_get")]
    public static extern uint RailAssetInfo_state_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_flag_set")]
    public static extern void RailAssetInfo_flag_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_flag_get")]
    public static extern uint RailAssetInfo_flag_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_origin_set")]
    public static extern void RailAssetInfo_origin_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_origin_get")]
    public static extern uint RailAssetInfo_origin_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetInfo__SWIG_1")]
    public static extern IntPtr new_RailAssetInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailAssetInfo")]
    public static extern void delete_RailAssetInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetItem__SWIG_0")]
    public static extern IntPtr new_RailAssetItem__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetItem_asset_id_set")]
    public static extern void RailAssetItem_asset_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetItem_asset_id_get")]
    public static extern ulong RailAssetItem_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetItem_quantity_set")]
    public static extern void RailAssetItem_quantity_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetItem_quantity_get")]
    public static extern uint RailAssetItem_quantity_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetItem__SWIG_1")]
    public static extern IntPtr new_RailAssetItem__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailAssetItem")]
    public static extern void delete_RailAssetItem(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailProductItem__SWIG_0")]
    public static extern IntPtr new_RailProductItem__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailProductItem_product_id_set")]
    public static extern void RailProductItem_product_id_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailProductItem_product_id_get")]
    public static extern uint RailProductItem_product_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailProductItem_quantity_set")]
    public static extern void RailProductItem_quantity_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailProductItem_quantity_get")]
    public static extern uint RailProductItem_quantity_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailProductItem__SWIG_1")]
    public static extern IntPtr new_RailProductItem__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailProductItem")]
    public static extern void delete_RailProductItem(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetProperty__SWIG_0")]
    public static extern IntPtr new_RailAssetProperty__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetProperty_asset_id_set")]
    public static extern void RailAssetProperty_asset_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetProperty_asset_id_get")]
    public static extern ulong RailAssetProperty_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetProperty_position_set")]
    public static extern void RailAssetProperty_position_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailAssetProperty_position_get")]
    public static extern uint RailAssetProperty_position_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetProperty__SWIG_1")]
    public static extern IntPtr new_RailAssetProperty__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailAssetProperty")]
    public static extern void delete_RailAssetProperty(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RequestAllAssetsFinished__SWIG_0")]
    public static extern IntPtr new_RequestAllAssetsFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RequestAllAssetsFinished_assetinfo_list_set")]
    public static extern void RequestAllAssetsFinished_assetinfo_list_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RequestAllAssetsFinished_assetinfo_list_get")]
    public static extern IntPtr RequestAllAssetsFinished_assetinfo_list_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RequestAllAssetsFinished__SWIG_1")]
    public static extern IntPtr new_RequestAllAssetsFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RequestAllAssetsFinished")]
    public static extern void delete_RequestAllAssetsFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_UpdateAssetsPropertyFinished__SWIG_0")]
    public static extern IntPtr new_UpdateAssetsPropertyFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_UpdateAssetsPropertyFinished_asset_property_list_set")]
    public static extern void UpdateAssetsPropertyFinished_asset_property_list_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_UpdateAssetsPropertyFinished_asset_property_list_get")]
    public static extern IntPtr UpdateAssetsPropertyFinished_asset_property_list_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_UpdateAssetsPropertyFinished__SWIG_1")]
    public static extern IntPtr new_UpdateAssetsPropertyFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_UpdateAssetsPropertyFinished")]
    public static extern void delete_UpdateAssetsPropertyFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DirectConsumeAssetsFinished__SWIG_0")]
    public static extern IntPtr new_DirectConsumeAssetsFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_DirectConsumeAssetsFinished_assets_set")]
    public static extern void DirectConsumeAssetsFinished_assets_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DirectConsumeAssetsFinished_assets_get")]
    public static extern IntPtr DirectConsumeAssetsFinished_assets_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DirectConsumeAssetsFinished__SWIG_1")]
    public static extern IntPtr new_DirectConsumeAssetsFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_DirectConsumeAssetsFinished")]
    public static extern void delete_DirectConsumeAssetsFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_StartConsumeAssetsFinished__SWIG_0")]
    public static extern IntPtr new_StartConsumeAssetsFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_StartConsumeAssetsFinished_asset_id_set")]
    public static extern void StartConsumeAssetsFinished_asset_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_StartConsumeAssetsFinished_asset_id_get")]
    public static extern ulong StartConsumeAssetsFinished_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_StartConsumeAssetsFinished__SWIG_1")]
    public static extern IntPtr new_StartConsumeAssetsFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_StartConsumeAssetsFinished")]
    public static extern void delete_StartConsumeAssetsFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_UpdateConsumeAssetsFinished__SWIG_0")]
    public static extern IntPtr new_UpdateConsumeAssetsFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_UpdateConsumeAssetsFinished_asset_id_set")]
    public static extern void UpdateConsumeAssetsFinished_asset_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_UpdateConsumeAssetsFinished_asset_id_get")]
    public static extern ulong UpdateConsumeAssetsFinished_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_UpdateConsumeAssetsFinished__SWIG_1")]
    public static extern IntPtr new_UpdateConsumeAssetsFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_UpdateConsumeAssetsFinished")]
    public static extern void delete_UpdateConsumeAssetsFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CompleteConsumeAssetsFinished__SWIG_0")]
    public static extern IntPtr new_CompleteConsumeAssetsFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CompleteConsumeAssetsFinished_asset_item_set")]
    public static extern void CompleteConsumeAssetsFinished_asset_item_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CompleteConsumeAssetsFinished_asset_item_get")]
    public static extern IntPtr CompleteConsumeAssetsFinished_asset_item_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CompleteConsumeAssetsFinished__SWIG_1")]
    public static extern IntPtr new_CompleteConsumeAssetsFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CompleteConsumeAssetsFinished")]
    public static extern void delete_CompleteConsumeAssetsFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SplitAssetsFinished__SWIG_0")]
    public static extern IntPtr new_SplitAssetsFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_source_asset_set")]
    public static extern void SplitAssetsFinished_source_asset_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_source_asset_get")]
    public static extern ulong SplitAssetsFinished_source_asset_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_to_quantity_set")]
    public static extern void SplitAssetsFinished_to_quantity_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_to_quantity_get")]
    public static extern uint SplitAssetsFinished_to_quantity_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_new_asset_id_set")]
    public static extern void SplitAssetsFinished_new_asset_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_new_asset_id_get")]
    public static extern ulong SplitAssetsFinished_new_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SplitAssetsFinished__SWIG_1")]
    public static extern IntPtr new_SplitAssetsFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_SplitAssetsFinished")]
    public static extern void delete_SplitAssetsFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SplitAssetsToFinished__SWIG_0")]
    public static extern IntPtr new_SplitAssetsToFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_source_asset_set")]
    public static extern void SplitAssetsToFinished_source_asset_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_source_asset_get")]
    public static extern ulong SplitAssetsToFinished_source_asset_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_to_quantity_set")]
    public static extern void SplitAssetsToFinished_to_quantity_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_to_quantity_get")]
    public static extern uint SplitAssetsToFinished_to_quantity_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_split_to_asset_id_set")]
    public static extern void SplitAssetsToFinished_split_to_asset_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_split_to_asset_id_get")]
    public static extern ulong SplitAssetsToFinished_split_to_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SplitAssetsToFinished__SWIG_1")]
    public static extern IntPtr new_SplitAssetsToFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_SplitAssetsToFinished")]
    public static extern void delete_SplitAssetsToFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_MergeAssetsFinished__SWIG_0")]
    public static extern IntPtr new_MergeAssetsFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsFinished_source_assets_set")]
    public static extern void MergeAssetsFinished_source_assets_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsFinished_source_assets_get")]
    public static extern IntPtr MergeAssetsFinished_source_assets_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsFinished_new_asset_id_set")]
    public static extern void MergeAssetsFinished_new_asset_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsFinished_new_asset_id_get")]
    public static extern ulong MergeAssetsFinished_new_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_MergeAssetsFinished__SWIG_1")]
    public static extern IntPtr new_MergeAssetsFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_MergeAssetsFinished")]
    public static extern void delete_MergeAssetsFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_MergeAssetsToFinished__SWIG_0")]
    public static extern IntPtr new_MergeAssetsToFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsToFinished_source_assets_set")]
    public static extern void MergeAssetsToFinished_source_assets_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsToFinished_source_assets_get")]
    public static extern IntPtr MergeAssetsToFinished_source_assets_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsToFinished_merge_to_asset_id_set")]
    public static extern void MergeAssetsToFinished_merge_to_asset_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsToFinished_merge_to_asset_id_get")]
    public static extern ulong MergeAssetsToFinished_merge_to_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_MergeAssetsToFinished__SWIG_1")]
    public static extern IntPtr new_MergeAssetsToFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_MergeAssetsToFinished")]
    public static extern void delete_MergeAssetsToFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CompleteConsumeByExchangeAssetsToFinished__SWIG_0")]
    public static extern IntPtr new_CompleteConsumeByExchangeAssetsToFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_CompleteConsumeByExchangeAssetsToFinished__SWIG_1")]
    public static extern IntPtr new_CompleteConsumeByExchangeAssetsToFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CompleteConsumeByExchangeAssetsToFinished")]
    public static extern void delete_CompleteConsumeByExchangeAssetsToFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ExchangeAssetsFinished__SWIG_0")]
    public static extern IntPtr new_ExchangeAssetsFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_old_assets_set")]
    public static extern void ExchangeAssetsFinished_old_assets_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_old_assets_get")]
    public static extern IntPtr ExchangeAssetsFinished_old_assets_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_to_product_info_set")]
    public static extern void ExchangeAssetsFinished_to_product_info_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_to_product_info_get")]
    public static extern IntPtr ExchangeAssetsFinished_to_product_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_new_asset_id_set")]
    public static extern void ExchangeAssetsFinished_new_asset_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_new_asset_id_get")]
    public static extern ulong ExchangeAssetsFinished_new_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ExchangeAssetsFinished__SWIG_1")]
    public static extern IntPtr new_ExchangeAssetsFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ExchangeAssetsFinished")]
    public static extern void delete_ExchangeAssetsFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ExchangeAssetsToFinished__SWIG_0")]
    public static extern IntPtr new_ExchangeAssetsToFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_old_assets_set")]
    public static extern void ExchangeAssetsToFinished_old_assets_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_old_assets_get")]
    public static extern IntPtr ExchangeAssetsToFinished_old_assets_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_to_product_info_set")]
    public static extern void ExchangeAssetsToFinished_to_product_info_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_to_product_info_get")]
    public static extern IntPtr ExchangeAssetsToFinished_to_product_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_exchange_to_asset_id_set")]
    public static extern void ExchangeAssetsToFinished_exchange_to_asset_id_set(
      IntPtr jarg1,
      ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_exchange_to_asset_id_get")]
    public static extern ulong ExchangeAssetsToFinished_exchange_to_asset_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ExchangeAssetsToFinished__SWIG_1")]
    public static extern IntPtr new_ExchangeAssetsToFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ExchangeAssetsToFinished")]
    public static extern void delete_ExchangeAssetsToFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateBrowserOptions__SWIG_0")]
    public static extern IntPtr new_CreateBrowserOptions__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_maximum_button_set")]
    public static extern void CreateBrowserOptions_has_maximum_button_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_maximum_button_get")]
    public static extern bool CreateBrowserOptions_has_maximum_button_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_minimum_button_set")]
    public static extern void CreateBrowserOptions_has_minimum_button_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_minimum_button_get")]
    public static extern bool CreateBrowserOptions_has_minimum_button_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_border_set")]
    public static extern void CreateBrowserOptions_has_border_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_border_get")]
    public static extern bool CreateBrowserOptions_has_border_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_is_movable_set")]
    public static extern void CreateBrowserOptions_is_movable_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_is_movable_get")]
    public static extern bool CreateBrowserOptions_is_movable_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_margin_top_set")]
    public static extern void CreateBrowserOptions_margin_top_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_margin_top_get")]
    public static extern int CreateBrowserOptions_margin_top_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_margin_left_set")]
    public static extern void CreateBrowserOptions_margin_left_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_margin_left_get")]
    public static extern int CreateBrowserOptions_margin_left_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateBrowserOptions__SWIG_1")]
    public static extern IntPtr new_CreateBrowserOptions__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateBrowserOptions")]
    public static extern void delete_CreateBrowserOptions(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateCustomerDrawBrowserOptions__SWIG_0")]
    public static extern IntPtr new_CreateCustomerDrawBrowserOptions__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_offset_x_set")]
    public static extern void CreateCustomerDrawBrowserOptions_content_offset_x_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_offset_x_get")]
    public static extern int CreateCustomerDrawBrowserOptions_content_offset_x_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_offset_y_set")]
    public static extern void CreateCustomerDrawBrowserOptions_content_offset_y_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_offset_y_get")]
    public static extern int CreateCustomerDrawBrowserOptions_content_offset_y_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_window_width_set")]
    public static extern void CreateCustomerDrawBrowserOptions_content_window_width_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_window_width_get")]
    public static extern uint CreateCustomerDrawBrowserOptions_content_window_width_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_cotent_window_height_set")]
    public static extern void CreateCustomerDrawBrowserOptions_cotent_window_height_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_cotent_window_height_get")]
    public static extern uint CreateCustomerDrawBrowserOptions_cotent_window_height_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_has_scroll_set")]
    public static extern void CreateCustomerDrawBrowserOptions_has_scroll_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_has_scroll_get")]
    public static extern bool CreateCustomerDrawBrowserOptions_has_scroll_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateCustomerDrawBrowserOptions__SWIG_1")]
    public static extern IntPtr new_CreateCustomerDrawBrowserOptions__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateCustomerDrawBrowserOptions")]
    public static extern void delete_CreateCustomerDrawBrowserOptions(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateBrowserResult__SWIG_0")]
    public static extern IntPtr new_CreateBrowserResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateBrowserResult__SWIG_1")]
    public static extern IntPtr new_CreateBrowserResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateBrowserResult")]
    public static extern void delete_CreateBrowserResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ReloadBrowserResult__SWIG_0")]
    public static extern IntPtr new_ReloadBrowserResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_ReloadBrowserResult__SWIG_1")]
    public static extern IntPtr new_ReloadBrowserResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ReloadBrowserResult")]
    public static extern void delete_ReloadBrowserResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CloseBrowserResult__SWIG_0")]
    public static extern IntPtr new_CloseBrowserResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_CloseBrowserResult__SWIG_1")]
    public static extern IntPtr new_CloseBrowserResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CloseBrowserResult")]
    public static extern void delete_CloseBrowserResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_JavascriptEventResult__SWIG_0")]
    public static extern IntPtr new_JavascriptEventResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_JavascriptEventResult_event_name_set")]
    public static extern void JavascriptEventResult_event_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_JavascriptEventResult_event_name_get")]
    public static extern string JavascriptEventResult_event_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_JavascriptEventResult_event_value_set")]
    public static extern void JavascriptEventResult_event_value_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_JavascriptEventResult_event_value_get")]
    public static extern string JavascriptEventResult_event_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_JavascriptEventResult__SWIG_1")]
    public static extern IntPtr new_JavascriptEventResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_JavascriptEventResult")]
    public static extern void delete_JavascriptEventResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserNeedsPaintRequest__SWIG_0")]
    public static extern IntPtr new_BrowserNeedsPaintRequest__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_data_set")]
    public static extern void BrowserNeedsPaintRequest_bgra_data_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_data_get")]
    public static extern string BrowserNeedsPaintRequest_bgra_data_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_offset_x_set")]
    public static extern void BrowserNeedsPaintRequest_offset_x_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_offset_x_get")]
    public static extern int BrowserNeedsPaintRequest_offset_x_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_offset_y_set")]
    public static extern void BrowserNeedsPaintRequest_offset_y_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_offset_y_get")]
    public static extern int BrowserNeedsPaintRequest_offset_y_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_width_set")]
    public static extern void BrowserNeedsPaintRequest_bgra_width_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_width_get")]
    public static extern uint BrowserNeedsPaintRequest_bgra_width_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_height_set")]
    public static extern void BrowserNeedsPaintRequest_bgra_height_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_height_get")]
    public static extern uint BrowserNeedsPaintRequest_bgra_height_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_scroll_x_pos_set")]
    public static extern void BrowserNeedsPaintRequest_scroll_x_pos_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_scroll_x_pos_get")]
    public static extern uint BrowserNeedsPaintRequest_scroll_x_pos_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_scroll_y_pos_set")]
    public static extern void BrowserNeedsPaintRequest_scroll_y_pos_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_scroll_y_pos_get")]
    public static extern uint BrowserNeedsPaintRequest_scroll_y_pos_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_page_scale_factor_set")]
    public static extern void BrowserNeedsPaintRequest_page_scale_factor_set(
      IntPtr jarg1,
      float jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_page_scale_factor_get")]
    public static extern float BrowserNeedsPaintRequest_page_scale_factor_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserNeedsPaintRequest__SWIG_1")]
    public static extern IntPtr new_BrowserNeedsPaintRequest__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserNeedsPaintRequest")]
    public static extern void delete_BrowserNeedsPaintRequest(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserDamageRectNeedsPaintRequest__SWIG_0")]
    public static extern IntPtr new_BrowserDamageRectNeedsPaintRequest__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_data_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_bgra_data_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_data_get")]
    public static extern string BrowserDamageRectNeedsPaintRequest_bgra_data_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_offset_x_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_offset_x_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_offset_x_get")]
    public static extern int BrowserDamageRectNeedsPaintRequest_offset_x_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_offset_y_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_offset_y_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_offset_y_get")]
    public static extern int BrowserDamageRectNeedsPaintRequest_offset_y_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_width_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_bgra_width_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_width_get")]
    public static extern uint BrowserDamageRectNeedsPaintRequest_bgra_width_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_height_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_bgra_height_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_height_get")]
    public static extern uint BrowserDamageRectNeedsPaintRequest_bgra_height_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_offset_x_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_update_offset_x_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_offset_x_get")]
    public static extern int BrowserDamageRectNeedsPaintRequest_update_offset_x_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_offset_y_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_update_offset_y_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_offset_y_get")]
    public static extern int BrowserDamageRectNeedsPaintRequest_update_offset_y_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_bgra_width_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_update_bgra_width_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_bgra_width_get")]
    public static extern uint BrowserDamageRectNeedsPaintRequest_update_bgra_width_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_bgra_height_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_update_bgra_height_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_bgra_height_get")]
    public static extern uint BrowserDamageRectNeedsPaintRequest_update_bgra_height_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_scroll_x_pos_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_scroll_x_pos_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_scroll_x_pos_get")]
    public static extern uint BrowserDamageRectNeedsPaintRequest_scroll_x_pos_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_scroll_y_pos_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_scroll_y_pos_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_scroll_y_pos_get")]
    public static extern uint BrowserDamageRectNeedsPaintRequest_scroll_y_pos_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_page_scale_factor_set")]
    public static extern void BrowserDamageRectNeedsPaintRequest_page_scale_factor_set(
      IntPtr jarg1,
      float jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_page_scale_factor_get")]
    public static extern float BrowserDamageRectNeedsPaintRequest_page_scale_factor_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserDamageRectNeedsPaintRequest__SWIG_1")]
    public static extern IntPtr new_BrowserDamageRectNeedsPaintRequest__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserDamageRectNeedsPaintRequest")]
    public static extern void delete_BrowserDamageRectNeedsPaintRequest(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderNavigateResult__SWIG_0")]
    public static extern IntPtr new_BrowserRenderNavigateResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderNavigateResult_url_set")]
    public static extern void BrowserRenderNavigateResult_url_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderNavigateResult_url_get")]
    public static extern string BrowserRenderNavigateResult_url_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderNavigateResult__SWIG_1")]
    public static extern IntPtr new_BrowserRenderNavigateResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserRenderNavigateResult")]
    public static extern void delete_BrowserRenderNavigateResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderStateChanged__SWIG_0")]
    public static extern IntPtr new_BrowserRenderStateChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderStateChanged_can_go_back_set")]
    public static extern void BrowserRenderStateChanged_can_go_back_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderStateChanged_can_go_back_get")]
    public static extern bool BrowserRenderStateChanged_can_go_back_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderStateChanged_can_go_forward_set")]
    public static extern void BrowserRenderStateChanged_can_go_forward_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderStateChanged_can_go_forward_get")]
    public static extern bool BrowserRenderStateChanged_can_go_forward_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderStateChanged__SWIG_1")]
    public static extern IntPtr new_BrowserRenderStateChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserRenderStateChanged")]
    public static extern void delete_BrowserRenderStateChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderTitleChanged__SWIG_0")]
    public static extern IntPtr new_BrowserRenderTitleChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderTitleChanged_new_title_set")]
    public static extern void BrowserRenderTitleChanged_new_title_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderTitleChanged_new_title_get")]
    public static extern string BrowserRenderTitleChanged_new_title_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderTitleChanged__SWIG_1")]
    public static extern IntPtr new_BrowserRenderTitleChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserRenderTitleChanged")]
    public static extern void delete_BrowserRenderTitleChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserTryNavigateNewPageRequest__SWIG_0")]
    public static extern IntPtr new_BrowserTryNavigateNewPageRequest__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_url_set")]
    public static extern void BrowserTryNavigateNewPageRequest_url_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_url_get")]
    public static extern string BrowserTryNavigateNewPageRequest_url_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_target_type_set")]
    public static extern void BrowserTryNavigateNewPageRequest_target_type_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_target_type_get")]
    public static extern string BrowserTryNavigateNewPageRequest_target_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_is_redirect_request_set")]
    public static extern void BrowserTryNavigateNewPageRequest_is_redirect_request_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_is_redirect_request_get")]
    public static extern bool BrowserTryNavigateNewPageRequest_is_redirect_request_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_BrowserTryNavigateNewPageRequest__SWIG_1")]
    public static extern IntPtr new_BrowserTryNavigateNewPageRequest__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserTryNavigateNewPageRequest")]
    public static extern void delete_BrowserTryNavigateNewPageRequest(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_dlc_id_set")]
    public static extern void RailDlcInfo_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_dlc_id_get")]
    public static extern IntPtr RailDlcInfo_dlc_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_game_id_set")]
    public static extern void RailDlcInfo_game_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_game_id_get")]
    public static extern IntPtr RailDlcInfo_game_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_version_set")]
    public static extern void RailDlcInfo_version_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_version_get")]
    public static extern string RailDlcInfo_version_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_name_set")]
    public static extern void RailDlcInfo_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_name_get")]
    public static extern string RailDlcInfo_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_description_set")]
    public static extern void RailDlcInfo_description_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_description_get")]
    public static extern string RailDlcInfo_description_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_original_price_set")]
    public static extern void RailDlcInfo_original_price_set(IntPtr jarg1, double jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_original_price_get")]
    public static extern double RailDlcInfo_original_price_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_discount_price_set")]
    public static extern void RailDlcInfo_discount_price_set(IntPtr jarg1, double jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_discount_price_get")]
    public static extern double RailDlcInfo_discount_price_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcInfo__SWIG_0")]
    public static extern IntPtr new_RailDlcInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcInfo__SWIG_1")]
    public static extern IntPtr new_RailDlcInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailDlcInfo")]
    public static extern void delete_RailDlcInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_progress_set")]
    public static extern void RailDlcInstallProgress_progress_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_progress_get")]
    public static extern uint RailDlcInstallProgress_progress_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_finished_bytes_set")]
    public static extern void RailDlcInstallProgress_finished_bytes_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_finished_bytes_get")]
    public static extern ulong RailDlcInstallProgress_finished_bytes_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_total_bytes_set")]
    public static extern void RailDlcInstallProgress_total_bytes_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_total_bytes_get")]
    public static extern ulong RailDlcInstallProgress_total_bytes_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_speed_set")]
    public static extern void RailDlcInstallProgress_speed_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_speed_get")]
    public static extern uint RailDlcInstallProgress_speed_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcInstallProgress__SWIG_0")]
    public static extern IntPtr new_RailDlcInstallProgress__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcInstallProgress__SWIG_1")]
    public static extern IntPtr new_RailDlcInstallProgress__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailDlcInstallProgress")]
    public static extern void delete_RailDlcInstallProgress(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcOwned_is_owned_set")]
    public static extern void RailDlcOwned_is_owned_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcOwned_is_owned_get")]
    public static extern bool RailDlcOwned_is_owned_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcOwned_dlc_id_set")]
    public static extern void RailDlcOwned_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailDlcOwned_dlc_id_get")]
    public static extern IntPtr RailDlcOwned_dlc_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcOwned__SWIG_0")]
    public static extern IntPtr new_RailDlcOwned__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcOwned__SWIG_1")]
    public static extern IntPtr new_RailDlcOwned__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailDlcOwned")]
    public static extern void delete_RailDlcOwned(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStart_dlc_id_set")]
    public static extern void DlcInstallStart_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStart_dlc_id_get")]
    public static extern IntPtr DlcInstallStart_dlc_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallStart__SWIG_0")]
    public static extern IntPtr new_DlcInstallStart__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallStart__SWIG_1")]
    public static extern IntPtr new_DlcInstallStart__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_DlcInstallStart")]
    public static extern void delete_DlcInstallStart(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStartResult_dlc_id_set")]
    public static extern void DlcInstallStartResult_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStartResult_dlc_id_get")]
    public static extern IntPtr DlcInstallStartResult_dlc_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStartResult_result_set")]
    public static extern void DlcInstallStartResult_result_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStartResult_result_get")]
    public static extern int DlcInstallStartResult_result_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallStartResult__SWIG_0")]
    public static extern IntPtr new_DlcInstallStartResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallStartResult__SWIG_1")]
    public static extern IntPtr new_DlcInstallStartResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_DlcInstallStartResult")]
    public static extern void delete_DlcInstallStartResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallProgress_dlc_id_set")]
    public static extern void DlcInstallProgress_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallProgress_dlc_id_get")]
    public static extern IntPtr DlcInstallProgress_dlc_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallProgress_progress_set")]
    public static extern void DlcInstallProgress_progress_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallProgress_progress_get")]
    public static extern IntPtr DlcInstallProgress_progress_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallProgress__SWIG_0")]
    public static extern IntPtr new_DlcInstallProgress__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallProgress__SWIG_1")]
    public static extern IntPtr new_DlcInstallProgress__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_DlcInstallProgress")]
    public static extern void delete_DlcInstallProgress(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallFinished_dlc_id_set")]
    public static extern void DlcInstallFinished_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallFinished_dlc_id_get")]
    public static extern IntPtr DlcInstallFinished_dlc_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallFinished_result_set")]
    public static extern void DlcInstallFinished_result_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallFinished_result_get")]
    public static extern int DlcInstallFinished_result_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallFinished__SWIG_0")]
    public static extern IntPtr new_DlcInstallFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallFinished__SWIG_1")]
    public static extern IntPtr new_DlcInstallFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_DlcInstallFinished")]
    public static extern void delete_DlcInstallFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcUninstallFinished_dlc_id_set")]
    public static extern void DlcUninstallFinished_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcUninstallFinished_dlc_id_get")]
    public static extern IntPtr DlcUninstallFinished_dlc_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcUninstallFinished_result_set")]
    public static extern void DlcUninstallFinished_result_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcUninstallFinished_result_get")]
    public static extern int DlcUninstallFinished_result_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcUninstallFinished__SWIG_0")]
    public static extern IntPtr new_DlcUninstallFinished__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcUninstallFinished__SWIG_1")]
    public static extern IntPtr new_DlcUninstallFinished__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_DlcUninstallFinished")]
    public static extern void delete_DlcUninstallFinished(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CheckAllDlcsStateReadyResult__SWIG_0")]
    public static extern IntPtr new_CheckAllDlcsStateReadyResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_CheckAllDlcsStateReadyResult__SWIG_1")]
    public static extern IntPtr new_CheckAllDlcsStateReadyResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CheckAllDlcsStateReadyResult")]
    public static extern void delete_CheckAllDlcsStateReadyResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_QueryIsOwnedDlcsResult_dlc_owned_list_set")]
    public static extern void QueryIsOwnedDlcsResult_dlc_owned_list_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_QueryIsOwnedDlcsResult_dlc_owned_list_get")]
    public static extern IntPtr QueryIsOwnedDlcsResult_dlc_owned_list_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_QueryIsOwnedDlcsResult__SWIG_0")]
    public static extern IntPtr new_QueryIsOwnedDlcsResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_QueryIsOwnedDlcsResult__SWIG_1")]
    public static extern IntPtr new_QueryIsOwnedDlcsResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_QueryIsOwnedDlcsResult")]
    public static extern void delete_QueryIsOwnedDlcsResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcOwnershipChanged__SWIG_0")]
    public static extern IntPtr new_DlcOwnershipChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_DlcOwnershipChanged_dlc_id_set")]
    public static extern void DlcOwnershipChanged_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcOwnershipChanged_dlc_id_get")]
    public static extern IntPtr DlcOwnershipChanged_dlc_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcOwnershipChanged_is_active_set")]
    public static extern void DlcOwnershipChanged_is_active_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcOwnershipChanged_is_active_get")]
    public static extern bool DlcOwnershipChanged_is_active_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcOwnershipChanged__SWIG_1")]
    public static extern IntPtr new_DlcOwnershipChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_DlcOwnershipChanged")]
    public static extern void delete_DlcOwnershipChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcRefundChanged__SWIG_0")]
    public static extern IntPtr new_DlcRefundChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_DlcRefundChanged_dlc_id_set")]
    public static extern void DlcRefundChanged_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcRefundChanged_dlc_id_get")]
    public static extern IntPtr DlcRefundChanged_dlc_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcRefundChanged_refund_state_set")]
    public static extern void DlcRefundChanged_refund_state_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcRefundChanged_refund_state_get")]
    public static extern int DlcRefundChanged_refund_state_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DlcRefundChanged__SWIG_1")]
    public static extern IntPtr new_DlcRefundChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_DlcRefundChanged")]
    public static extern void delete_DlcRefundChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailStoreOptions__SWIG_0")]
    public static extern IntPtr new_RailStoreOptions__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_store_type_set")]
    public static extern void RailStoreOptions_store_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_store_type_get")]
    public static extern int RailStoreOptions_store_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_window_margin_top_set")]
    public static extern void RailStoreOptions_window_margin_top_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_window_margin_top_get")]
    public static extern int RailStoreOptions_window_margin_top_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_window_margin_left_set")]
    public static extern void RailStoreOptions_window_margin_left_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_window_margin_left_get")]
    public static extern int RailStoreOptions_window_margin_left_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailStoreOptions__SWIG_1")]
    public static extern IntPtr new_RailStoreOptions__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailStoreOptions")]
    public static extern void delete_RailStoreOptions(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ShowFloatingWindowResult__SWIG_0")]
    public static extern IntPtr new_ShowFloatingWindowResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_result_set")]
    public static extern void ShowFloatingWindowResult_result_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_result_get")]
    public static extern int ShowFloatingWindowResult_result_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_is_show_set")]
    public static extern void ShowFloatingWindowResult_is_show_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_is_show_get")]
    public static extern bool ShowFloatingWindowResult_is_show_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_window_type_set")]
    public static extern void ShowFloatingWindowResult_window_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_window_type_get")]
    public static extern int ShowFloatingWindowResult_window_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ShowFloatingWindowResult__SWIG_1")]
    public static extern IntPtr new_ShowFloatingWindowResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ShowFloatingWindowResult")]
    public static extern void delete_ShowFloatingWindowResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ShowNotifyFloatingWindowResult__SWIG_0")]
    public static extern IntPtr new_ShowNotifyFloatingWindowResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyFloatingWindowResult_result_set")]
    public static extern void ShowNotifyFloatingWindowResult_result_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyFloatingWindowResult_result_get")]
    public static extern int ShowNotifyFloatingWindowResult_result_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyFloatingWindowResult_is_show_set")]
    public static extern void ShowNotifyFloatingWindowResult_is_show_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyFloatingWindowResult_is_show_get")]
    public static extern bool ShowNotifyFloatingWindowResult_is_show_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyFloatingWindowResult_notify_window_type_set")]
    public static extern void ShowNotifyFloatingWindowResult_notify_window_type_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyFloatingWindowResult_notify_window_type_get")]
    public static extern int ShowNotifyFloatingWindowResult_notify_window_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ShowNotifyFloatingWindowResult__SWIG_1")]
    public static extern IntPtr new_ShowNotifyFloatingWindowResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ShowNotifyFloatingWindowResult")]
    public static extern void delete_ShowNotifyFloatingWindowResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerInfo__SWIG_0")]
    public static extern IntPtr new_GameServerInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_Reset")]
    public static extern void GameServerInfo_Reset(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_rail_id_set")]
    public static extern void GameServerInfo_game_server_rail_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_rail_id_get")]
    public static extern IntPtr GameServerInfo_game_server_rail_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_owner_rail_id_set")]
    public static extern void GameServerInfo_owner_rail_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_owner_rail_id_get")]
    public static extern IntPtr GameServerInfo_owner_rail_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_is_dedicated_set")]
    public static extern void GameServerInfo_is_dedicated_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_is_dedicated_get")]
    public static extern bool GameServerInfo_is_dedicated_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_name_set")]
    public static extern void GameServerInfo_game_server_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_name_get")]
    public static extern string GameServerInfo_game_server_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_map_set")]
    public static extern void GameServerInfo_game_server_map_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_map_get")]
    public static extern string GameServerInfo_game_server_map_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_has_password_set")]
    public static extern void GameServerInfo_has_password_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_has_password_get")]
    public static extern bool GameServerInfo_has_password_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_is_friend_only_set")]
    public static extern void GameServerInfo_is_friend_only_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_is_friend_only_get")]
    public static extern bool GameServerInfo_is_friend_only_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_max_players_set")]
    public static extern void GameServerInfo_max_players_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_max_players_get")]
    public static extern uint GameServerInfo_max_players_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_current_players_set")]
    public static extern void GameServerInfo_current_players_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_current_players_get")]
    public static extern uint GameServerInfo_current_players_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_bot_players_set")]
    public static extern void GameServerInfo_bot_players_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_bot_players_get")]
    public static extern uint GameServerInfo_bot_players_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_zone_id_set")]
    public static extern void GameServerInfo_zone_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_zone_id_get")]
    public static extern ulong GameServerInfo_zone_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_channel_id_set")]
    public static extern void GameServerInfo_channel_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_channel_id_get")]
    public static extern ulong GameServerInfo_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_host_set")]
    public static extern void GameServerInfo_server_host_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_host_get")]
    public static extern string GameServerInfo_server_host_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_fullname_set")]
    public static extern void GameServerInfo_server_fullname_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_fullname_get")]
    public static extern string GameServerInfo_server_fullname_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_description_set")]
    public static extern void GameServerInfo_server_description_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_description_get")]
    public static extern string GameServerInfo_server_description_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_tags_set")]
    public static extern void GameServerInfo_server_tags_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_tags_get")]
    public static extern string GameServerInfo_server_tags_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_version_set")]
    public static extern void GameServerInfo_server_version_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_version_get")]
    public static extern string GameServerInfo_server_version_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_spectator_host_set")]
    public static extern void GameServerInfo_spectator_host_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_spectator_host_get")]
    public static extern string GameServerInfo_spectator_host_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_info_set")]
    public static extern void GameServerInfo_server_info_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_info_get")]
    public static extern string GameServerInfo_server_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_mods_set")]
    public static extern void GameServerInfo_server_mods_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_mods_get")]
    public static extern IntPtr GameServerInfo_server_mods_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_kvs_set")]
    public static extern void GameServerInfo_server_kvs_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_kvs_get")]
    public static extern IntPtr GameServerInfo_server_kvs_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerInfo__SWIG_1")]
    public static extern IntPtr new_GameServerInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerInfo")]
    public static extern void delete_GameServerInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateGameServerOptions__SWIG_0")]
    public static extern IntPtr new_CreateGameServerOptions__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerOptions_enable_team_voice_set")]
    public static extern void CreateGameServerOptions_enable_team_voice_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerOptions_enable_team_voice_get")]
    public static extern bool CreateGameServerOptions_enable_team_voice_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerOptions_has_password_set")]
    public static extern void CreateGameServerOptions_has_password_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerOptions_has_password_get")]
    public static extern bool CreateGameServerOptions_has_password_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateGameServerOptions__SWIG_1")]
    public static extern IntPtr new_CreateGameServerOptions__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateGameServerOptions")]
    public static extern void delete_CreateGameServerOptions(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListSorter__SWIG_0")]
    public static extern IntPtr new_GameServerListSorter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sorter_key_type_set")]
    public static extern void GameServerListSorter_sorter_key_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sorter_key_type_get")]
    public static extern int GameServerListSorter_sorter_key_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_key_set")]
    public static extern void GameServerListSorter_sort_key_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_key_get")]
    public static extern string GameServerListSorter_sort_key_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_value_type_set")]
    public static extern void GameServerListSorter_sort_value_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_value_type_get")]
    public static extern int GameServerListSorter_sort_value_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_type_set")]
    public static extern void GameServerListSorter_sort_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_type_get")]
    public static extern int GameServerListSorter_sort_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListSorter__SWIG_1")]
    public static extern IntPtr new_GameServerListSorter__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerListSorter")]
    public static extern void delete_GameServerListSorter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListFilterKey__SWIG_0")]
    public static extern IntPtr new_GameServerListFilterKey__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_key_name_set")]
    public static extern void GameServerListFilterKey_key_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_key_name_get")]
    public static extern string GameServerListFilterKey_key_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_value_type_set")]
    public static extern void GameServerListFilterKey_value_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_value_type_get")]
    public static extern int GameServerListFilterKey_value_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_comparison_type_set")]
    public static extern void GameServerListFilterKey_comparison_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_comparison_type_get")]
    public static extern int GameServerListFilterKey_comparison_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_filter_value_set")]
    public static extern void GameServerListFilterKey_filter_value_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_filter_value_get")]
    public static extern string GameServerListFilterKey_filter_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListFilterKey__SWIG_1")]
    public static extern IntPtr new_GameServerListFilterKey__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerListFilterKey")]
    public static extern void delete_GameServerListFilterKey(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListFilter__SWIG_0")]
    public static extern IntPtr new_GameServerListFilter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filters_set")]
    public static extern void GameServerListFilter_filters_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filters_get")]
    public static extern IntPtr GameServerListFilter_filters_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_owner_id_set")]
    public static extern void GameServerListFilter_owner_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_owner_id_get")]
    public static extern IntPtr GameServerListFilter_owner_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_delicated_server_set")]
    public static extern void GameServerListFilter_filter_delicated_server_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_delicated_server_get")]
    public static extern int GameServerListFilter_filter_delicated_server_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_game_server_name_set")]
    public static extern void GameServerListFilter_filter_game_server_name_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_game_server_name_get")]
    public static extern string GameServerListFilter_filter_game_server_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_zone_id_set")]
    public static extern void GameServerListFilter_filter_zone_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_zone_id_get")]
    public static extern ulong GameServerListFilter_filter_zone_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_game_server_map_set")]
    public static extern void GameServerListFilter_filter_game_server_map_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_game_server_map_get")]
    public static extern string GameServerListFilter_filter_game_server_map_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_password_set")]
    public static extern void GameServerListFilter_filter_password_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_password_get")]
    public static extern int GameServerListFilter_filter_password_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_friends_created_set")]
    public static extern void GameServerListFilter_filter_friends_created_set(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_friends_created_get")]
    public static extern int GameServerListFilter_filter_friends_created_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_tags_contained_set")]
    public static extern void GameServerListFilter_tags_contained_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_tags_contained_get")]
    public static extern string GameServerListFilter_tags_contained_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_tags_not_contained_set")]
    public static extern void GameServerListFilter_tags_not_contained_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_tags_not_contained_get")]
    public static extern string GameServerListFilter_tags_not_contained_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListFilter__SWIG_1")]
    public static extern IntPtr new_GameServerListFilter__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerListFilter")]
    public static extern void delete_GameServerListFilter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerPlayerInfo__SWIG_0")]
    public static extern IntPtr new_GameServerPlayerInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_id_set")]
    public static extern void GameServerPlayerInfo_member_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_id_get")]
    public static extern IntPtr GameServerPlayerInfo_member_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_nickname_set")]
    public static extern void GameServerPlayerInfo_member_nickname_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_nickname_get")]
    public static extern string GameServerPlayerInfo_member_nickname_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_score_set")]
    public static extern void GameServerPlayerInfo_member_score_set(IntPtr jarg1, long jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_score_get")]
    public static extern long GameServerPlayerInfo_member_score_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerPlayerInfo__SWIG_1")]
    public static extern IntPtr new_GameServerPlayerInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerPlayerInfo")]
    public static extern void delete_GameServerPlayerInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncAcquireGameServerSessionTicketResponse__SWIG_0")]
    public static extern IntPtr new_AsyncAcquireGameServerSessionTicketResponse__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncAcquireGameServerSessionTicketResponse_session_ticket_set")]
    public static extern void AsyncAcquireGameServerSessionTicketResponse_session_ticket_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncAcquireGameServerSessionTicketResponse_session_ticket_get")]
    public static extern IntPtr AsyncAcquireGameServerSessionTicketResponse_session_ticket_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncAcquireGameServerSessionTicketResponse__SWIG_1")]
    public static extern IntPtr new_AsyncAcquireGameServerSessionTicketResponse__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncAcquireGameServerSessionTicketResponse")]
    public static extern void delete_AsyncAcquireGameServerSessionTicketResponse(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerStartSessionWithPlayerResponse__SWIG_0")]
    public static extern IntPtr new_GameServerStartSessionWithPlayerResponse__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerStartSessionWithPlayerResponse_remote_rail_id_set")]
    public static extern void GameServerStartSessionWithPlayerResponse_remote_rail_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerStartSessionWithPlayerResponse_remote_rail_id_get")]
    public static extern IntPtr GameServerStartSessionWithPlayerResponse_remote_rail_id_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerStartSessionWithPlayerResponse__SWIG_1")]
    public static extern IntPtr new_GameServerStartSessionWithPlayerResponse__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerStartSessionWithPlayerResponse")]
    public static extern void delete_GameServerStartSessionWithPlayerResponse(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateGameServerResult__SWIG_0")]
    public static extern IntPtr new_CreateGameServerResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerResult_game_server_id_set")]
    public static extern void CreateGameServerResult_game_server_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerResult_game_server_id_get")]
    public static extern IntPtr CreateGameServerResult_game_server_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateGameServerResult__SWIG_1")]
    public static extern IntPtr new_CreateGameServerResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateGameServerResult")]
    public static extern void delete_CreateGameServerResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SetGameServerMetadataResult__SWIG_0")]
    public static extern IntPtr new_SetGameServerMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_SetGameServerMetadataResult_game_server_id_set")]
    public static extern void SetGameServerMetadataResult_game_server_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SetGameServerMetadataResult_game_server_id_get")]
    public static extern IntPtr SetGameServerMetadataResult_game_server_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SetGameServerMetadataResult__SWIG_1")]
    public static extern IntPtr new_SetGameServerMetadataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_SetGameServerMetadataResult")]
    public static extern void delete_SetGameServerMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerMetadataResult__SWIG_0")]
    public static extern IntPtr new_GetGameServerMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerMetadataResult_game_server_id_set")]
    public static extern void GetGameServerMetadataResult_game_server_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerMetadataResult_game_server_id_get")]
    public static extern IntPtr GetGameServerMetadataResult_game_server_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerMetadataResult_key_value_set")]
    public static extern void GetGameServerMetadataResult_key_value_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerMetadataResult_key_value_get")]
    public static extern IntPtr GetGameServerMetadataResult_key_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerMetadataResult__SWIG_1")]
    public static extern IntPtr new_GetGameServerMetadataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GetGameServerMetadataResult")]
    public static extern void delete_GetGameServerMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerRegisterToServerListResult__SWIG_0")]
    public static extern IntPtr new_GameServerRegisterToServerListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_GameServerRegisterToServerListResult__SWIG_1")]
    public static extern IntPtr new_GameServerRegisterToServerListResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerRegisterToServerListResult")]
    public static extern void delete_GameServerRegisterToServerListResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerPlayerListResult__SWIG_0")]
    public static extern IntPtr new_GetGameServerPlayerListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerPlayerListResult_game_server_id_set")]
    public static extern void GetGameServerPlayerListResult_game_server_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerPlayerListResult_game_server_id_get")]
    public static extern IntPtr GetGameServerPlayerListResult_game_server_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerPlayerListResult_server_player_info_set")]
    public static extern void GetGameServerPlayerListResult_server_player_info_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerPlayerListResult_server_player_info_get")]
    public static extern IntPtr GetGameServerPlayerListResult_server_player_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerPlayerListResult__SWIG_1")]
    public static extern IntPtr new_GetGameServerPlayerListResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GetGameServerPlayerListResult")]
    public static extern void delete_GetGameServerPlayerListResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerListResult__SWIG_0")]
    public static extern IntPtr new_GetGameServerListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_start_index_set")]
    public static extern void GetGameServerListResult_start_index_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_start_index_get")]
    public static extern uint GetGameServerListResult_start_index_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_end_index_set")]
    public static extern void GetGameServerListResult_end_index_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_end_index_get")]
    public static extern uint GetGameServerListResult_end_index_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_total_num_set")]
    public static extern void GetGameServerListResult_total_num_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_total_num_get")]
    public static extern uint GetGameServerListResult_total_num_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_server_info_set")]
    public static extern void GetGameServerListResult_server_info_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_server_info_get")]
    public static extern IntPtr GetGameServerListResult_server_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerListResult__SWIG_1")]
    public static extern IntPtr new_GetGameServerListResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GetGameServerListResult")]
    public static extern void delete_GetGameServerListResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetFavoriteGameServersResult__SWIG_0")]
    public static extern IntPtr new_AsyncGetFavoriteGameServersResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetFavoriteGameServersResult_server_id_array_set")]
    public static extern void AsyncGetFavoriteGameServersResult_server_id_array_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetFavoriteGameServersResult_server_id_array_get")]
    public static extern IntPtr AsyncGetFavoriteGameServersResult_server_id_array_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetFavoriteGameServersResult__SWIG_1")]
    public static extern IntPtr new_AsyncGetFavoriteGameServersResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncGetFavoriteGameServersResult")]
    public static extern void delete_AsyncGetFavoriteGameServersResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncAddFavoriteGameServerResult__SWIG_0")]
    public static extern IntPtr new_AsyncAddFavoriteGameServerResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncAddFavoriteGameServerResult_server_id_set")]
    public static extern void AsyncAddFavoriteGameServerResult_server_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncAddFavoriteGameServerResult_server_id_get")]
    public static extern IntPtr AsyncAddFavoriteGameServerResult_server_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncAddFavoriteGameServerResult__SWIG_1")]
    public static extern IntPtr new_AsyncAddFavoriteGameServerResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncAddFavoriteGameServerResult")]
    public static extern void delete_AsyncAddFavoriteGameServerResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRemoveFavoriteGameServerResult__SWIG_0")]
    public static extern IntPtr new_AsyncRemoveFavoriteGameServerResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveFavoriteGameServerResult_server_id_set")]
    public static extern void AsyncRemoveFavoriteGameServerResult_server_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveFavoriteGameServerResult_server_id_get")]
    public static extern IntPtr AsyncRemoveFavoriteGameServerResult_server_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRemoveFavoriteGameServerResult__SWIG_1")]
    public static extern IntPtr new_AsyncRemoveFavoriteGameServerResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncRemoveFavoriteGameServerResult")]
    public static extern void delete_AsyncRemoveFavoriteGameServerResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailKeyValueResult__SWIG_0")]
    public static extern IntPtr new_RailKeyValueResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_err_set")]
    public static extern void RailKeyValueResult_err_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_err_get")]
    public static extern int RailKeyValueResult_err_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_key_set")]
    public static extern void RailKeyValueResult_key_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_key_get")]
    public static extern string RailKeyValueResult_key_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_value_set")]
    public static extern void RailKeyValueResult_value_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_value_get")]
    public static extern string RailKeyValueResult_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailKeyValueResult__SWIG_1")]
    public static extern IntPtr new_RailKeyValueResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailKeyValueResult")]
    public static extern void delete_RailKeyValueResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUserPlayedWith__SWIG_0")]
    public static extern IntPtr new_RailUserPlayedWith__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailUserPlayedWith_rail_id_set")]
    public static extern void RailUserPlayedWith_rail_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUserPlayedWith_rail_id_get")]
    public static extern IntPtr RailUserPlayedWith_rail_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUserPlayedWith_user_rich_content_set")]
    public static extern void RailUserPlayedWith_user_rich_content_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUserPlayedWith_user_rich_content_get")]
    public static extern string RailUserPlayedWith_user_rich_content_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailUserPlayedWith__SWIG_1")]
    public static extern IntPtr new_RailUserPlayedWith__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailUserPlayedWith")]
    public static extern void delete_RailUserPlayedWith(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsSetMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailFriendsSetMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsSetMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailFriendsSetMetadataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsSetMetadataResult")]
    public static extern void delete_RailFriendsSetMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsClearMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailFriendsClearMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsClearMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailFriendsClearMetadataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsClearMetadataResult")]
    public static extern void delete_RailFriendsClearMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsGetMetadataResult__SWIG_0")]
    public static extern IntPtr new_RailFriendsGetMetadataResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetMetadataResult_friend_id_set")]
    public static extern void RailFriendsGetMetadataResult_friend_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetMetadataResult_friend_id_get")]
    public static extern IntPtr RailFriendsGetMetadataResult_friend_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetMetadataResult_friend_kvs_set")]
    public static extern void RailFriendsGetMetadataResult_friend_kvs_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetMetadataResult_friend_kvs_get")]
    public static extern IntPtr RailFriendsGetMetadataResult_friend_kvs_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsGetMetadataResult__SWIG_1")]
    public static extern IntPtr new_RailFriendsGetMetadataResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsGetMetadataResult")]
    public static extern void delete_RailFriendsGetMetadataResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsGetInviteCommandLine__SWIG_0")]
    public static extern IntPtr new_RailFriendsGetInviteCommandLine__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetInviteCommandLine_friend_id_set")]
    public static extern void RailFriendsGetInviteCommandLine_friend_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetInviteCommandLine_friend_id_get")]
    public static extern IntPtr RailFriendsGetInviteCommandLine_friend_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetInviteCommandLine_invite_command_line_set")]
    public static extern void RailFriendsGetInviteCommandLine_invite_command_line_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetInviteCommandLine_invite_command_line_get")]
    public static extern string RailFriendsGetInviteCommandLine_invite_command_line_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsGetInviteCommandLine__SWIG_1")]
    public static extern IntPtr new_RailFriendsGetInviteCommandLine__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsGetInviteCommandLine")]
    public static extern void delete_RailFriendsGetInviteCommandLine(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsReportPlayedWithUserListResult__SWIG_0")]
    public static extern IntPtr new_RailFriendsReportPlayedWithUserListResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsReportPlayedWithUserListResult__SWIG_1")]
    public static extern IntPtr new_RailFriendsReportPlayedWithUserListResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsReportPlayedWithUserListResult")]
    public static extern void delete_RailFriendsReportPlayedWithUserListResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsBuddyListChanged__SWIG_0")]
    public static extern IntPtr new_RailFriendsBuddyListChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsBuddyListChanged__SWIG_1")]
    public static extern IntPtr new_RailFriendsBuddyListChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsBuddyListChanged")]
    public static extern void delete_RailFriendsBuddyListChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DiscountInfo__SWIG_0")]
    public static extern IntPtr new_DiscountInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_DiscountInfo_off_set")]
    public static extern void DiscountInfo_off_set(IntPtr jarg1, float jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DiscountInfo_off_get")]
    public static extern float DiscountInfo_off_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DiscountInfo_discount_price_set")]
    public static extern void DiscountInfo_discount_price_set(IntPtr jarg1, float jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DiscountInfo_discount_price_get")]
    public static extern float DiscountInfo_discount_price_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DiscountInfo_type_set")]
    public static extern void DiscountInfo_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DiscountInfo_type_get")]
    public static extern int DiscountInfo_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DiscountInfo_start_time_set")]
    public static extern void DiscountInfo_start_time_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DiscountInfo_start_time_get")]
    public static extern uint DiscountInfo_start_time_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DiscountInfo_end_time_set")]
    public static extern void DiscountInfo_end_time_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_DiscountInfo_end_time_get")]
    public static extern uint DiscountInfo_end_time_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_DiscountInfo__SWIG_1")]
    public static extern IntPtr new_DiscountInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_DiscountInfo")]
    public static extern void delete_DiscountInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPurchaseProductExtraInfo__SWIG_0")]
    public static extern IntPtr new_RailPurchaseProductExtraInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductExtraInfo_exchange_rule_set")]
    public static extern void RailPurchaseProductExtraInfo_exchange_rule_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductExtraInfo_exchange_rule_get")]
    public static extern string RailPurchaseProductExtraInfo_exchange_rule_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductExtraInfo_bundle_rule_set")]
    public static extern void RailPurchaseProductExtraInfo_bundle_rule_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductExtraInfo_bundle_rule_get")]
    public static extern string RailPurchaseProductExtraInfo_bundle_rule_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPurchaseProductExtraInfo__SWIG_1")]
    public static extern IntPtr new_RailPurchaseProductExtraInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailPurchaseProductExtraInfo")]
    public static extern void delete_RailPurchaseProductExtraInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPurchaseProductInfo__SWIG_0")]
    public static extern IntPtr new_RailPurchaseProductInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_product_id_set")]
    public static extern void RailPurchaseProductInfo_product_id_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_product_id_get")]
    public static extern uint RailPurchaseProductInfo_product_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_is_purchasable_set")]
    public static extern void RailPurchaseProductInfo_is_purchasable_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_is_purchasable_get")]
    public static extern bool RailPurchaseProductInfo_is_purchasable_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_name_set")]
    public static extern void RailPurchaseProductInfo_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_name_get")]
    public static extern string RailPurchaseProductInfo_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_description_set")]
    public static extern void RailPurchaseProductInfo_description_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_description_get")]
    public static extern string RailPurchaseProductInfo_description_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_category_set")]
    public static extern void RailPurchaseProductInfo_category_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_category_get")]
    public static extern string RailPurchaseProductInfo_category_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_product_thumbnail_set")]
    public static extern void RailPurchaseProductInfo_product_thumbnail_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_product_thumbnail_get")]
    public static extern string RailPurchaseProductInfo_product_thumbnail_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_extra_info_set")]
    public static extern void RailPurchaseProductInfo_extra_info_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_extra_info_get")]
    public static extern IntPtr RailPurchaseProductInfo_extra_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_original_price_set")]
    public static extern void RailPurchaseProductInfo_original_price_set(IntPtr jarg1, float jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_original_price_get")]
    public static extern float RailPurchaseProductInfo_original_price_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_currency_type_set")]
    public static extern void RailPurchaseProductInfo_currency_type_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_currency_type_get")]
    public static extern string RailPurchaseProductInfo_currency_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_discount_set")]
    public static extern void RailPurchaseProductInfo_discount_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_discount_get")]
    public static extern IntPtr RailPurchaseProductInfo_discount_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPurchaseProductInfo__SWIG_1")]
    public static extern IntPtr new_RailPurchaseProductInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailPurchaseProductInfo")]
    public static extern void delete_RailPurchaseProductInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseRequestAllPurchasableProductsResponse__SWIG_0")]
    public static extern IntPtr new_RailInGamePurchaseRequestAllPurchasableProductsResponse__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_set")]
    public static extern void RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_get")]
    public static extern IntPtr RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseRequestAllPurchasableProductsResponse__SWIG_1")]
    public static extern IntPtr new_RailInGamePurchaseRequestAllPurchasableProductsResponse__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGamePurchaseRequestAllPurchasableProductsResponse")]
    public static extern void delete_RailInGamePurchaseRequestAllPurchasableProductsResponse(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseRequestAllProductsResponse__SWIG_0")]
    public static extern IntPtr new_RailInGamePurchaseRequestAllProductsResponse__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllProductsResponse_all_products_set")]
    public static extern void RailInGamePurchaseRequestAllProductsResponse_all_products_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllProductsResponse_all_products_get")]
    public static extern IntPtr RailInGamePurchaseRequestAllProductsResponse_all_products_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseRequestAllProductsResponse__SWIG_1")]
    public static extern IntPtr new_RailInGamePurchaseRequestAllProductsResponse__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGamePurchaseRequestAllProductsResponse")]
    public static extern void delete_RailInGamePurchaseRequestAllProductsResponse(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchasePurchaseProductsResponse__SWIG_0")]
    public static extern IntPtr new_RailInGamePurchasePurchaseProductsResponse__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsResponse_order_id_set")]
    public static extern void RailInGamePurchasePurchaseProductsResponse_order_id_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsResponse_order_id_get")]
    public static extern string RailInGamePurchasePurchaseProductsResponse_order_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsResponse_deliveried_products_set")]
    public static extern void RailInGamePurchasePurchaseProductsResponse_deliveried_products_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsResponse_deliveried_products_get")]
    public static extern IntPtr RailInGamePurchasePurchaseProductsResponse_deliveried_products_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchasePurchaseProductsResponse__SWIG_1")]
    public static extern IntPtr new_RailInGamePurchasePurchaseProductsResponse__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGamePurchasePurchaseProductsResponse")]
    public static extern void delete_RailInGamePurchasePurchaseProductsResponse(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchasePurchaseProductsToAssetsResponse__SWIG_0")]
    public static extern IntPtr new_RailInGamePurchasePurchaseProductsToAssetsResponse__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_set")]
    public static extern void RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_get")]
    public static extern string RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsToAssetsResponse_deliveried_assets_set")]
    public static extern void RailInGamePurchasePurchaseProductsToAssetsResponse_deliveried_assets_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsToAssetsResponse_deliveried_assets_get")]
    public static extern IntPtr RailInGamePurchasePurchaseProductsToAssetsResponse_deliveried_assets_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchasePurchaseProductsToAssetsResponse__SWIG_1")]
    public static extern IntPtr new_RailInGamePurchasePurchaseProductsToAssetsResponse__SWIG_1(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGamePurchasePurchaseProductsToAssetsResponse")]
    public static extern void delete_RailInGamePurchasePurchaseProductsToAssetsResponse(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseFinishOrderResponse__SWIG_0")]
    public static extern IntPtr new_RailInGamePurchaseFinishOrderResponse__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseFinishOrderResponse_order_id_set")]
    public static extern void RailInGamePurchaseFinishOrderResponse_order_id_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseFinishOrderResponse_order_id_get")]
    public static extern string RailInGamePurchaseFinishOrderResponse_order_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseFinishOrderResponse__SWIG_1")]
    public static extern IntPtr new_RailInGamePurchaseFinishOrderResponse__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGamePurchaseFinishOrderResponse")]
    public static extern void delete_RailInGamePurchaseFinishOrderResponse(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchasePayWindowDisplayed__SWIG_0")]
    public static extern IntPtr new_RailInGameStorePurchasePayWindowDisplayed__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowDisplayed_order_id_set")]
    public static extern void RailInGameStorePurchasePayWindowDisplayed_order_id_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowDisplayed_order_id_get")]
    public static extern string RailInGameStorePurchasePayWindowDisplayed_order_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchasePayWindowDisplayed__SWIG_1")]
    public static extern IntPtr new_RailInGameStorePurchasePayWindowDisplayed__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGameStorePurchasePayWindowDisplayed")]
    public static extern void delete_RailInGameStorePurchasePayWindowDisplayed(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchasePayWindowClosed__SWIG_0")]
    public static extern IntPtr new_RailInGameStorePurchasePayWindowClosed__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowClosed_order_id_set")]
    public static extern void RailInGameStorePurchasePayWindowClosed_order_id_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowClosed_order_id_get")]
    public static extern string RailInGameStorePurchasePayWindowClosed_order_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchasePayWindowClosed__SWIG_1")]
    public static extern IntPtr new_RailInGameStorePurchasePayWindowClosed__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGameStorePurchasePayWindowClosed")]
    public static extern void delete_RailInGameStorePurchasePayWindowClosed(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchaseResult__SWIG_0")]
    public static extern IntPtr new_RailInGameStorePurchaseResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchaseResult_order_id_set")]
    public static extern void RailInGameStorePurchaseResult_order_id_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchaseResult_order_id_get")]
    public static extern string RailInGameStorePurchaseResult_order_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchaseResult__SWIG_1")]
    public static extern IntPtr new_RailInGameStorePurchaseResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGameStorePurchaseResult")]
    public static extern void delete_RailInGameStorePurchaseResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateSessionRequest__SWIG_0")]
    public static extern IntPtr new_CreateSessionRequest__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CreateSessionRequest_local_peer_set")]
    public static extern void CreateSessionRequest_local_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateSessionRequest_local_peer_get")]
    public static extern IntPtr CreateSessionRequest_local_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateSessionRequest_remote_peer_set")]
    public static extern void CreateSessionRequest_remote_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateSessionRequest_remote_peer_get")]
    public static extern IntPtr CreateSessionRequest_remote_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateSessionRequest__SWIG_1")]
    public static extern IntPtr new_CreateSessionRequest__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateSessionRequest")]
    public static extern void delete_CreateSessionRequest(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateSessionFailed__SWIG_0")]
    public static extern IntPtr new_CreateSessionFailed__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CreateSessionFailed_local_peer_set")]
    public static extern void CreateSessionFailed_local_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateSessionFailed_local_peer_get")]
    public static extern IntPtr CreateSessionFailed_local_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateSessionFailed__SWIG_1")]
    public static extern IntPtr new_CreateSessionFailed__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateSessionFailed")]
    public static extern void delete_CreateSessionFailed(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RAIL_DEFAULT_MAX_ROOM_MEMBERS_get")]
    public static extern int RAIL_DEFAULT_MAX_ROOM_MEMBERS_get();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomOptions__SWIG_0")]
    public static extern IntPtr new_RoomOptions__SWIG_0(ulong jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_type_set")]
    public static extern void RoomOptions_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_type_get")]
    public static extern int RoomOptions_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_max_members_set")]
    public static extern void RoomOptions_max_members_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_max_members_get")]
    public static extern uint RoomOptions_max_members_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_zone_id_set")]
    public static extern void RoomOptions_zone_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_zone_id_get")]
    public static extern ulong RoomOptions_zone_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_password_set")]
    public static extern void RoomOptions_password_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_password_get")]
    public static extern string RoomOptions_password_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_enable_team_voice_set")]
    public static extern void RoomOptions_enable_team_voice_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_enable_team_voice_get")]
    public static extern bool RoomOptions_enable_team_voice_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomOptions__SWIG_1")]
    public static extern IntPtr new_RoomOptions__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RoomOptions")]
    public static extern void delete_RoomOptions(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListSorter__SWIG_0")]
    public static extern IntPtr new_RoomInfoListSorter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_value_type_set")]
    public static extern void RoomInfoListSorter_property_value_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_value_type_get")]
    public static extern int RoomInfoListSorter_property_value_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_sort_type_set")]
    public static extern void RoomInfoListSorter_property_sort_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_sort_type_get")]
    public static extern int RoomInfoListSorter_property_sort_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_key_set")]
    public static extern void RoomInfoListSorter_property_key_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_key_get")]
    public static extern string RoomInfoListSorter_property_key_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_close_to_value_set")]
    public static extern void RoomInfoListSorter_close_to_value_set(IntPtr jarg1, double jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_close_to_value_get")]
    public static extern double RoomInfoListSorter_close_to_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListSorter__SWIG_1")]
    public static extern IntPtr new_RoomInfoListSorter__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RoomInfoListSorter")]
    public static extern void delete_RoomInfoListSorter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListFilterKey__SWIG_0")]
    public static extern IntPtr new_RoomInfoListFilterKey__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_key_name_set")]
    public static extern void RoomInfoListFilterKey_key_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_key_name_get")]
    public static extern string RoomInfoListFilterKey_key_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_value_type_set")]
    public static extern void RoomInfoListFilterKey_value_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_value_type_get")]
    public static extern int RoomInfoListFilterKey_value_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_comparison_type_set")]
    public static extern void RoomInfoListFilterKey_comparison_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_comparison_type_get")]
    public static extern int RoomInfoListFilterKey_comparison_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_filter_value_set")]
    public static extern void RoomInfoListFilterKey_filter_value_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_filter_value_get")]
    public static extern string RoomInfoListFilterKey_filter_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListFilterKey__SWIG_1")]
    public static extern IntPtr new_RoomInfoListFilterKey__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RoomInfoListFilterKey")]
    public static extern void delete_RoomInfoListFilterKey(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListFilter__SWIG_0")]
    public static extern IntPtr new_RoomInfoListFilter__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_key_filters_set")]
    public static extern void RoomInfoListFilter_key_filters_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_key_filters_get")]
    public static extern IntPtr RoomInfoListFilter_key_filters_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_room_name_contained_set")]
    public static extern void RoomInfoListFilter_room_name_contained_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_room_name_contained_get")]
    public static extern string RoomInfoListFilter_room_name_contained_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_filter_password_set")]
    public static extern void RoomInfoListFilter_filter_password_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_filter_password_get")]
    public static extern int RoomInfoListFilter_filter_password_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_filter_friends_owned_set")]
    public static extern void RoomInfoListFilter_filter_friends_owned_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_filter_friends_owned_get")]
    public static extern int RoomInfoListFilter_filter_friends_owned_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_available_slot_at_least_set")]
    public static extern void RoomInfoListFilter_available_slot_at_least_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_available_slot_at_least_get")]
    public static extern uint RoomInfoListFilter_available_slot_at_least_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListFilter__SWIG_1")]
    public static extern IntPtr new_RoomInfoListFilter__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RoomInfoListFilter")]
    public static extern void delete_RoomInfoListFilter(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ZoneInfo__SWIG_0")]
    public static extern IntPtr new_ZoneInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_zone_id_set")]
    public static extern void ZoneInfo_zone_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_zone_id_get")]
    public static extern ulong ZoneInfo_zone_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_idc_id_set")]
    public static extern void ZoneInfo_idc_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_idc_id_get")]
    public static extern ulong ZoneInfo_idc_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_country_code_set")]
    public static extern void ZoneInfo_country_code_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_country_code_get")]
    public static extern uint ZoneInfo_country_code_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_status_set")]
    public static extern void ZoneInfo_status_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_status_get")]
    public static extern int ZoneInfo_status_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_name_set")]
    public static extern void ZoneInfo_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_name_get")]
    public static extern string ZoneInfo_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_description_set")]
    public static extern void ZoneInfo_description_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_description_get")]
    public static extern string ZoneInfo_description_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ZoneInfo__SWIG_1")]
    public static extern IntPtr new_ZoneInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ZoneInfo")]
    public static extern void delete_ZoneInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfo__SWIG_0")]
    public static extern IntPtr new_RoomInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_zone_id_set")]
    public static extern void RoomInfo_zone_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_zone_id_get")]
    public static extern ulong RoomInfo_zone_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_id_set")]
    public static extern void RoomInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_id_get")]
    public static extern ulong RoomInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_owner_id_set")]
    public static extern void RoomInfo_owner_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_owner_id_get")]
    public static extern IntPtr RoomInfo_owner_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_state_set")]
    public static extern void RoomInfo_room_state_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_state_get")]
    public static extern int RoomInfo_room_state_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_max_members_set")]
    public static extern void RoomInfo_max_members_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_max_members_get")]
    public static extern uint RoomInfo_max_members_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_current_members_set")]
    public static extern void RoomInfo_current_members_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_current_members_get")]
    public static extern uint RoomInfo_current_members_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_create_time_set")]
    public static extern void RoomInfo_create_time_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_create_time_get")]
    public static extern uint RoomInfo_create_time_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_name_set")]
    public static extern void RoomInfo_room_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_name_get")]
    public static extern string RoomInfo_room_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_has_password_set")]
    public static extern void RoomInfo_has_password_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_has_password_get")]
    public static extern bool RoomInfo_has_password_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_is_joinable_set")]
    public static extern void RoomInfo_is_joinable_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_is_joinable_get")]
    public static extern bool RoomInfo_is_joinable_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_type_set")]
    public static extern void RoomInfo_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_type_get")]
    public static extern int RoomInfo_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_game_server_rail_id_set")]
    public static extern void RoomInfo_game_server_rail_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_game_server_rail_id_get")]
    public static extern ulong RoomInfo_game_server_rail_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_game_server_channel_id_set")]
    public static extern void RoomInfo_game_server_channel_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_game_server_channel_id_get")]
    public static extern ulong RoomInfo_game_server_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_kvs_set")]
    public static extern void RoomInfo_room_kvs_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_kvs_get")]
    public static extern IntPtr RoomInfo_room_kvs_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfo__SWIG_1")]
    public static extern IntPtr new_RoomInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RoomInfo")]
    public static extern void delete_RoomInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_MemberInfo__SWIG_0")]
    public static extern IntPtr new_MemberInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_room_id_set")]
    public static extern void MemberInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_room_id_get")]
    public static extern ulong MemberInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_id_set")]
    public static extern void MemberInfo_member_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_id_get")]
    public static extern ulong MemberInfo_member_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_index_set")]
    public static extern void MemberInfo_member_index_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_index_get")]
    public static extern uint MemberInfo_member_index_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_name_set")]
    public static extern void MemberInfo_member_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_name_get")]
    public static extern string MemberInfo_member_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_MemberInfo__SWIG_1")]
    public static extern IntPtr new_MemberInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_MemberInfo")]
    public static extern void delete_MemberInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ZoneInfoList__SWIG_0")]
    public static extern IntPtr new_ZoneInfoList__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfoList_zone_info_set")]
    public static extern void ZoneInfoList_zone_info_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfoList_zone_info_get")]
    public static extern IntPtr ZoneInfoList_zone_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ZoneInfoList__SWIG_1")]
    public static extern IntPtr new_ZoneInfoList__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ZoneInfoList")]
    public static extern void delete_ZoneInfoList(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoList__SWIG_0")]
    public static extern IntPtr new_RoomInfoList__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_zone_id_set")]
    public static extern void RoomInfoList_zone_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_zone_id_get")]
    public static extern ulong RoomInfoList_zone_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_begin_index_set")]
    public static extern void RoomInfoList_begin_index_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_begin_index_get")]
    public static extern uint RoomInfoList_begin_index_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_end_index_set")]
    public static extern void RoomInfoList_end_index_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_end_index_get")]
    public static extern uint RoomInfoList_end_index_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_total_room_num_in_zone_set")]
    public static extern void RoomInfoList_total_room_num_in_zone_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_total_room_num_in_zone_get")]
    public static extern uint RoomInfoList_total_room_num_in_zone_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_room_info_set")]
    public static extern void RoomInfoList_room_info_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_room_info_get")]
    public static extern IntPtr RoomInfoList_room_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoList__SWIG_1")]
    public static extern IntPtr new_RoomInfoList__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RoomInfoList")]
    public static extern void delete_RoomInfoList(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomAllData__SWIG_0")]
    public static extern IntPtr new_RoomAllData__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RoomAllData_room_info_set")]
    public static extern void RoomAllData_room_info_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomAllData_room_info_get")]
    public static extern IntPtr RoomAllData_room_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomAllData__SWIG_1")]
    public static extern IntPtr new_RoomAllData__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RoomAllData")]
    public static extern void delete_RoomAllData(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateRoomInfo__SWIG_0")]
    public static extern IntPtr new_CreateRoomInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CreateRoomInfo_zone_id_set")]
    public static extern void CreateRoomInfo_zone_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateRoomInfo_zone_id_get")]
    public static extern ulong CreateRoomInfo_zone_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateRoomInfo_room_id_set")]
    public static extern void CreateRoomInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateRoomInfo_room_id_get")]
    public static extern ulong CreateRoomInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateRoomInfo__SWIG_1")]
    public static extern IntPtr new_CreateRoomInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateRoomInfo")]
    public static extern void delete_CreateRoomInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomMembersInfo__SWIG_0")]
    public static extern IntPtr new_RoomMembersInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_room_id_set")]
    public static extern void RoomMembersInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_room_id_get")]
    public static extern ulong RoomMembersInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_member_num_set")]
    public static extern void RoomMembersInfo_member_num_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_member_num_get")]
    public static extern uint RoomMembersInfo_member_num_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_member_info_set")]
    public static extern void RoomMembersInfo_member_info_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_member_info_get")]
    public static extern IntPtr RoomMembersInfo_member_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomMembersInfo__SWIG_1")]
    public static extern IntPtr new_RoomMembersInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RoomMembersInfo")]
    public static extern void delete_RoomMembersInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_JoinRoomInfo__SWIG_0")]
    public static extern IntPtr new_JoinRoomInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_JoinRoomInfo_zone_id_set")]
    public static extern void JoinRoomInfo_zone_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_JoinRoomInfo_zone_id_get")]
    public static extern ulong JoinRoomInfo_zone_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_JoinRoomInfo_room_id_set")]
    public static extern void JoinRoomInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_JoinRoomInfo_room_id_get")]
    public static extern ulong JoinRoomInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_JoinRoomInfo__SWIG_1")]
    public static extern IntPtr new_JoinRoomInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_JoinRoomInfo")]
    public static extern void delete_JoinRoomInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_KickOffMemberInfo__SWIG_0")]
    public static extern IntPtr new_KickOffMemberInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_KickOffMemberInfo_room_id_set")]
    public static extern void KickOffMemberInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_KickOffMemberInfo_room_id_get")]
    public static extern ulong KickOffMemberInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_KickOffMemberInfo_kicked_id_set")]
    public static extern void KickOffMemberInfo_kicked_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_KickOffMemberInfo_kicked_id_get")]
    public static extern ulong KickOffMemberInfo_kicked_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_KickOffMemberInfo__SWIG_1")]
    public static extern IntPtr new_KickOffMemberInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_KickOffMemberInfo")]
    public static extern void delete_KickOffMemberInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SetRoomMetadataInfo__SWIG_0")]
    public static extern IntPtr new_SetRoomMetadataInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_SetRoomMetadataInfo_room_id_set")]
    public static extern void SetRoomMetadataInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SetRoomMetadataInfo_room_id_get")]
    public static extern ulong SetRoomMetadataInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SetRoomMetadataInfo__SWIG_1")]
    public static extern IntPtr new_SetRoomMetadataInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_SetRoomMetadataInfo")]
    public static extern void delete_SetRoomMetadataInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GetRoomMetadataInfo__SWIG_0")]
    public static extern IntPtr new_GetRoomMetadataInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GetRoomMetadataInfo_room_id_set")]
    public static extern void GetRoomMetadataInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetRoomMetadataInfo_room_id_get")]
    public static extern ulong GetRoomMetadataInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetRoomMetadataInfo_key_value_set")]
    public static extern void GetRoomMetadataInfo_key_value_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetRoomMetadataInfo_key_value_get")]
    public static extern IntPtr GetRoomMetadataInfo_key_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GetRoomMetadataInfo__SWIG_1")]
    public static extern IntPtr new_GetRoomMetadataInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GetRoomMetadataInfo")]
    public static extern void delete_GetRoomMetadataInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ClearRoomMetadataInfo__SWIG_0")]
    public static extern IntPtr new_ClearRoomMetadataInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ClearRoomMetadataInfo_room_id_set")]
    public static extern void ClearRoomMetadataInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ClearRoomMetadataInfo_room_id_get")]
    public static extern ulong ClearRoomMetadataInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ClearRoomMetadataInfo__SWIG_1")]
    public static extern IntPtr new_ClearRoomMetadataInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ClearRoomMetadataInfo")]
    public static extern void delete_ClearRoomMetadataInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GetMemberMetadataInfo__SWIG_0")]
    public static extern IntPtr new_GetMemberMetadataInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_room_id_set")]
    public static extern void GetMemberMetadataInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_room_id_get")]
    public static extern ulong GetMemberMetadataInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_member_id_set")]
    public static extern void GetMemberMetadataInfo_member_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_member_id_get")]
    public static extern ulong GetMemberMetadataInfo_member_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_key_value_set")]
    public static extern void GetMemberMetadataInfo_key_value_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_key_value_get")]
    public static extern IntPtr GetMemberMetadataInfo_key_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GetMemberMetadataInfo__SWIG_1")]
    public static extern IntPtr new_GetMemberMetadataInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GetMemberMetadataInfo")]
    public static extern void delete_GetMemberMetadataInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SetMemberMetadataInfo__SWIG_0")]
    public static extern IntPtr new_SetMemberMetadataInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_SetMemberMetadataInfo_room_id_set")]
    public static extern void SetMemberMetadataInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SetMemberMetadataInfo_room_id_get")]
    public static extern ulong SetMemberMetadataInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SetMemberMetadataInfo_member_id_set")]
    public static extern void SetMemberMetadataInfo_member_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_SetMemberMetadataInfo_member_id_get")]
    public static extern ulong SetMemberMetadataInfo_member_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_SetMemberMetadataInfo__SWIG_1")]
    public static extern IntPtr new_SetMemberMetadataInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_SetMemberMetadataInfo")]
    public static extern void delete_SetMemberMetadataInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaveRoomInfo__SWIG_0")]
    public static extern IntPtr new_LeaveRoomInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_LeaveRoomInfo_room_id_set")]
    public static extern void LeaveRoomInfo_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaveRoomInfo_room_id_get")]
    public static extern ulong LeaveRoomInfo_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaveRoomInfo_reason_set")]
    public static extern void LeaveRoomInfo_reason_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaveRoomInfo_reason_get")]
    public static extern int LeaveRoomInfo_reason_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaveRoomInfo__SWIG_1")]
    public static extern IntPtr new_LeaveRoomInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_LeaveRoomInfo")]
    public static extern void delete_LeaveRoomInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_UserRoomListInfo__SWIG_0")]
    public static extern IntPtr new_UserRoomListInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_UserRoomListInfo_room_info_set")]
    public static extern void UserRoomListInfo_room_info_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_UserRoomListInfo_room_info_get")]
    public static extern IntPtr UserRoomListInfo_room_info_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_UserRoomListInfo__SWIG_1")]
    public static extern IntPtr new_UserRoomListInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_UserRoomListInfo")]
    public static extern void delete_UserRoomListInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyMetadataChange__SWIG_0")]
    public static extern IntPtr new_NotifyMetadataChange__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyMetadataChange_room_id_set")]
    public static extern void NotifyMetadataChange_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyMetadataChange_room_id_get")]
    public static extern ulong NotifyMetadataChange_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyMetadataChange_changer_id_set")]
    public static extern void NotifyMetadataChange_changer_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyMetadataChange_changer_id_get")]
    public static extern ulong NotifyMetadataChange_changer_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyMetadataChange__SWIG_1")]
    public static extern IntPtr new_NotifyMetadataChange__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyMetadataChange")]
    public static extern void delete_NotifyMetadataChange(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomMemberChange__SWIG_0")]
    public static extern IntPtr new_NotifyRoomMemberChange__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_room_id_set")]
    public static extern void NotifyRoomMemberChange_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_room_id_get")]
    public static extern ulong NotifyRoomMemberChange_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_changer_id_set")]
    public static extern void NotifyRoomMemberChange_changer_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_changer_id_get")]
    public static extern ulong NotifyRoomMemberChange_changer_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_id_for_making_change_set")]
    public static extern void NotifyRoomMemberChange_id_for_making_change_set(
      IntPtr jarg1,
      ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_id_for_making_change_get")]
    public static extern ulong NotifyRoomMemberChange_id_for_making_change_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_state_change_set")]
    public static extern void NotifyRoomMemberChange_state_change_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_state_change_get")]
    public static extern int NotifyRoomMemberChange_state_change_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomMemberChange__SWIG_1")]
    public static extern IntPtr new_NotifyRoomMemberChange__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyRoomMemberChange")]
    public static extern void delete_NotifyRoomMemberChange(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomMemberKicked__SWIG_0")]
    public static extern IntPtr new_NotifyRoomMemberKicked__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_room_id_set")]
    public static extern void NotifyRoomMemberKicked_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_room_id_get")]
    public static extern ulong NotifyRoomMemberKicked_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_id_for_making_kick_set")]
    public static extern void NotifyRoomMemberKicked_id_for_making_kick_set(
      IntPtr jarg1,
      ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_id_for_making_kick_get")]
    public static extern ulong NotifyRoomMemberKicked_id_for_making_kick_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_kicked_id_set")]
    public static extern void NotifyRoomMemberKicked_kicked_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_kicked_id_get")]
    public static extern ulong NotifyRoomMemberKicked_kicked_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_due_to_kicker_lost_connect_set")]
    public static extern void NotifyRoomMemberKicked_due_to_kicker_lost_connect_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_due_to_kicker_lost_connect_get")]
    public static extern uint NotifyRoomMemberKicked_due_to_kicker_lost_connect_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomMemberKicked__SWIG_1")]
    public static extern IntPtr new_NotifyRoomMemberKicked__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyRoomMemberKicked")]
    public static extern void delete_NotifyRoomMemberKicked(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomDestroy__SWIG_0")]
    public static extern IntPtr new_NotifyRoomDestroy__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomDestroy_room_id_set")]
    public static extern void NotifyRoomDestroy_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomDestroy_room_id_get")]
    public static extern ulong NotifyRoomDestroy_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomDestroy__SWIG_1")]
    public static extern IntPtr new_NotifyRoomDestroy__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyRoomDestroy")]
    public static extern void delete_NotifyRoomDestroy(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomDataReceived__SWIG_0")]
    public static extern IntPtr new_RoomDataReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_room_id_set")]
    public static extern void RoomDataReceived_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_room_id_get")]
    public static extern ulong RoomDataReceived_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_message_type_set")]
    public static extern void RoomDataReceived_message_type_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_message_type_get")]
    public static extern uint RoomDataReceived_message_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_data_len_set")]
    public static extern void RoomDataReceived_data_len_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_data_len_get")]
    public static extern uint RoomDataReceived_data_len_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_data_buffer_set")]
    public static extern void RoomDataReceived_data_buffer_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_data_buffer_get")]
    public static extern string RoomDataReceived_data_buffer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RoomDataReceived__SWIG_1")]
    public static extern IntPtr new_RoomDataReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RoomDataReceived")]
    public static extern void delete_RoomDataReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomOwnerChange__SWIG_0")]
    public static extern IntPtr new_NotifyRoomOwnerChange__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_room_id_set")]
    public static extern void NotifyRoomOwnerChange_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_room_id_get")]
    public static extern ulong NotifyRoomOwnerChange_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_old_owner_id_set")]
    public static extern void NotifyRoomOwnerChange_old_owner_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_old_owner_id_get")]
    public static extern ulong NotifyRoomOwnerChange_old_owner_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_new_owner_id_set")]
    public static extern void NotifyRoomOwnerChange_new_owner_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_new_owner_id_get")]
    public static extern ulong NotifyRoomOwnerChange_new_owner_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_reason_set")]
    public static extern void NotifyRoomOwnerChange_reason_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_reason_get")]
    public static extern int NotifyRoomOwnerChange_reason_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomOwnerChange__SWIG_1")]
    public static extern IntPtr new_NotifyRoomOwnerChange__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyRoomOwnerChange")]
    public static extern void delete_NotifyRoomOwnerChange(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomGameServerChange__SWIG_0")]
    public static extern IntPtr new_NotifyRoomGameServerChange__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_room_id_set")]
    public static extern void NotifyRoomGameServerChange_room_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_room_id_get")]
    public static extern ulong NotifyRoomGameServerChange_room_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_game_server_rail_id_set")]
    public static extern void NotifyRoomGameServerChange_game_server_rail_id_set(
      IntPtr jarg1,
      ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_game_server_rail_id_get")]
    public static extern ulong NotifyRoomGameServerChange_game_server_rail_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_game_server_channel_id_set")]
    public static extern void NotifyRoomGameServerChange_game_server_channel_id_set(
      IntPtr jarg1,
      ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_game_server_channel_id_get")]
    public static extern ulong NotifyRoomGameServerChange_game_server_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomGameServerChange__SWIG_1")]
    public static extern IntPtr new_NotifyRoomGameServerChange__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyRoomGameServerChange")]
    public static extern void delete_NotifyRoomGameServerChange(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateChannelResult__SWIG_0")]
    public static extern IntPtr new_CreateChannelResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CreateChannelResult_local_peer_set")]
    public static extern void CreateChannelResult_local_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateChannelResult_local_peer_get")]
    public static extern IntPtr CreateChannelResult_local_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateChannelResult_channel_id_set")]
    public static extern void CreateChannelResult_channel_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateChannelResult_channel_id_get")]
    public static extern ulong CreateChannelResult_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateChannelResult__SWIG_1")]
    public static extern IntPtr new_CreateChannelResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateChannelResult")]
    public static extern void delete_CreateChannelResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_InviteJoinChannelRequest__SWIG_0")]
    public static extern IntPtr new_InviteJoinChannelRequest__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_InviteJoinChannelRequest_local_peer_set")]
    public static extern void InviteJoinChannelRequest_local_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteJoinChannelRequest_local_peer_get")]
    public static extern IntPtr InviteJoinChannelRequest_local_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteJoinChannelRequest_channel_id_set")]
    public static extern void InviteJoinChannelRequest_channel_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteJoinChannelRequest_channel_id_get")]
    public static extern ulong InviteJoinChannelRequest_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteJoinChannelRequest_remote_peer_set")]
    public static extern void InviteJoinChannelRequest_remote_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteJoinChannelRequest_remote_peer_get")]
    public static extern IntPtr InviteJoinChannelRequest_remote_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_InviteJoinChannelRequest__SWIG_1")]
    public static extern IntPtr new_InviteJoinChannelRequest__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_InviteJoinChannelRequest")]
    public static extern void delete_InviteJoinChannelRequest(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_JoinChannelResult__SWIG_0")]
    public static extern IntPtr new_JoinChannelResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_JoinChannelResult_local_peer_set")]
    public static extern void JoinChannelResult_local_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_JoinChannelResult_local_peer_get")]
    public static extern IntPtr JoinChannelResult_local_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_JoinChannelResult_channel_id_set")]
    public static extern void JoinChannelResult_channel_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_JoinChannelResult_channel_id_get")]
    public static extern ulong JoinChannelResult_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_JoinChannelResult__SWIG_1")]
    public static extern IntPtr new_JoinChannelResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_JoinChannelResult")]
    public static extern void delete_JoinChannelResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ChannelException__SWIG_0")]
    public static extern IntPtr new_ChannelException__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelException_local_peer_set")]
    public static extern void ChannelException_local_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelException_local_peer_get")]
    public static extern IntPtr ChannelException_local_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelException_channel_id_set")]
    public static extern void ChannelException_channel_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelException_channel_id_get")]
    public static extern ulong ChannelException_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelException_exception_type_set")]
    public static extern void ChannelException_exception_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelException_exception_type_get")]
    public static extern int ChannelException_exception_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ChannelException__SWIG_1")]
    public static extern IntPtr new_ChannelException__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ChannelException")]
    public static extern void delete_ChannelException(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ChannelNetDelay__SWIG_0")]
    public static extern IntPtr new_ChannelNetDelay__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelNetDelay_local_peer_set")]
    public static extern void ChannelNetDelay_local_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelNetDelay_local_peer_get")]
    public static extern IntPtr ChannelNetDelay_local_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelNetDelay_channel_id_set")]
    public static extern void ChannelNetDelay_channel_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelNetDelay_channel_id_get")]
    public static extern ulong ChannelNetDelay_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelNetDelay_net_delay_ms_set")]
    public static extern void ChannelNetDelay_net_delay_ms_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelNetDelay_net_delay_ms_get")]
    public static extern uint ChannelNetDelay_net_delay_ms_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ChannelNetDelay__SWIG_1")]
    public static extern IntPtr new_ChannelNetDelay__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ChannelNetDelay")]
    public static extern void delete_ChannelNetDelay(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_InviteMemmberResult__SWIG_0")]
    public static extern IntPtr new_InviteMemmberResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_InviteMemmberResult_local_peer_set")]
    public static extern void InviteMemmberResult_local_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteMemmberResult_local_peer_get")]
    public static extern IntPtr InviteMemmberResult_local_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteMemmberResult_channel_id_set")]
    public static extern void InviteMemmberResult_channel_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteMemmberResult_channel_id_get")]
    public static extern ulong InviteMemmberResult_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteMemmberResult_remote_peer_set")]
    public static extern void InviteMemmberResult_remote_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteMemmberResult_remote_peer_get")]
    public static extern IntPtr InviteMemmberResult_remote_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_InviteMemmberResult__SWIG_1")]
    public static extern IntPtr new_InviteMemmberResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_InviteMemmberResult")]
    public static extern void delete_InviteMemmberResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ChannelMemberStateChanged__SWIG_0")]
    public static extern IntPtr new_ChannelMemberStateChanged__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelMemberStateChanged_local_peer_set")]
    public static extern void ChannelMemberStateChanged_local_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelMemberStateChanged_local_peer_get")]
    public static extern IntPtr ChannelMemberStateChanged_local_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelMemberStateChanged_channel_id_set")]
    public static extern void ChannelMemberStateChanged_channel_id_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelMemberStateChanged_channel_id_get")]
    public static extern ulong ChannelMemberStateChanged_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelMemberStateChanged_remote_peer_set")]
    public static extern void ChannelMemberStateChanged_remote_peer_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelMemberStateChanged_remote_peer_get")]
    public static extern IntPtr ChannelMemberStateChanged_remote_peer_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelMemberStateChanged_member_state_set")]
    public static extern void ChannelMemberStateChanged_member_state_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelMemberStateChanged_member_state_get")]
    public static extern int ChannelMemberStateChanged_member_state_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ChannelMemberStateChanged__SWIG_1")]
    public static extern IntPtr new_ChannelMemberStateChanged__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ChannelMemberStateChanged")]
    public static extern void delete_ChannelMemberStateChanged(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSyncFileOption__SWIG_0")]
    public static extern IntPtr new_RailSyncFileOption__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailSyncFileOption_sync_file_not_to_remote_set")]
    public static extern void RailSyncFileOption_sync_file_not_to_remote_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSyncFileOption_sync_file_not_to_remote_get")]
    public static extern bool RailSyncFileOption_sync_file_not_to_remote_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailSyncFileOption__SWIG_1")]
    public static extern IntPtr new_RailSyncFileOption__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailSyncFileOption")]
    public static extern void delete_RailSyncFileOption(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailStreamFileOption__SWIG_0")]
    public static extern IntPtr new_RailStreamFileOption__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileOption_unavaliabe_when_new_file_writing_set")]
    public static extern void RailStreamFileOption_unavaliabe_when_new_file_writing_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileOption_unavaliabe_when_new_file_writing_get")]
    public static extern bool RailStreamFileOption_unavaliabe_when_new_file_writing_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileOption_open_type_set")]
    public static extern void RailStreamFileOption_open_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileOption_open_type_get")]
    public static extern int RailStreamFileOption_open_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailStreamFileOption__SWIG_1")]
    public static extern IntPtr new_RailStreamFileOption__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailStreamFileOption")]
    public static extern void delete_RailStreamFileOption(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailListStreamFileOption__SWIG_0")]
    public static extern IntPtr new_RailListStreamFileOption__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailListStreamFileOption_start_index_set")]
    public static extern void RailListStreamFileOption_start_index_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailListStreamFileOption_start_index_get")]
    public static extern uint RailListStreamFileOption_start_index_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailListStreamFileOption_num_files_set")]
    public static extern void RailListStreamFileOption_num_files_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailListStreamFileOption_num_files_get")]
    public static extern uint RailListStreamFileOption_num_files_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailListStreamFileOption__SWIG_1")]
    public static extern IntPtr new_RailListStreamFileOption__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailListStreamFileOption")]
    public static extern void delete_RailListStreamFileOption(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPublishFileToUserSpaceOption__SWIG_0")]
    public static extern IntPtr new_RailPublishFileToUserSpaceOption__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_type_set")]
    public static extern void RailPublishFileToUserSpaceOption_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_type_get")]
    public static extern int RailPublishFileToUserSpaceOption_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_space_work_name_set")]
    public static extern void RailPublishFileToUserSpaceOption_space_work_name_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_space_work_name_get")]
    public static extern string RailPublishFileToUserSpaceOption_space_work_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_description_set")]
    public static extern void RailPublishFileToUserSpaceOption_description_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_description_get")]
    public static extern string RailPublishFileToUserSpaceOption_description_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_preview_path_filename_set")]
    public static extern void RailPublishFileToUserSpaceOption_preview_path_filename_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_preview_path_filename_get")]
    public static extern string RailPublishFileToUserSpaceOption_preview_path_filename_get(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_version_set")]
    public static extern void RailPublishFileToUserSpaceOption_version_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_version_get")]
    public static extern string RailPublishFileToUserSpaceOption_version_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_tags_set")]
    public static extern void RailPublishFileToUserSpaceOption_tags_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_tags_get")]
    public static extern IntPtr RailPublishFileToUserSpaceOption_tags_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_level_set")]
    public static extern void RailPublishFileToUserSpaceOption_level_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_level_get")]
    public static extern int RailPublishFileToUserSpaceOption_level_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_key_value_set")]
    public static extern void RailPublishFileToUserSpaceOption_key_value_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_key_value_get")]
    public static extern IntPtr RailPublishFileToUserSpaceOption_key_value_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailPublishFileToUserSpaceOption__SWIG_1")]
    public static extern IntPtr new_RailPublishFileToUserSpaceOption__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailPublishFileToUserSpaceOption")]
    public static extern void delete_RailPublishFileToUserSpaceOption(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileInfo_filename_set")]
    public static extern void RailStreamFileInfo_filename_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileInfo_filename_get")]
    public static extern string RailStreamFileInfo_filename_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileInfo_file_size_set")]
    public static extern void RailStreamFileInfo_file_size_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileInfo_file_size_get")]
    public static extern ulong RailStreamFileInfo_file_size_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailStreamFileInfo__SWIG_0")]
    public static extern IntPtr new_RailStreamFileInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailStreamFileInfo__SWIG_1")]
    public static extern IntPtr new_RailStreamFileInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailStreamFileInfo")]
    public static extern void delete_RailStreamFileInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncQueryQuotaResult__SWIG_0")]
    public static extern IntPtr new_AsyncQueryQuotaResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncQueryQuotaResult_total_quota_set")]
    public static extern void AsyncQueryQuotaResult_total_quota_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncQueryQuotaResult_total_quota_get")]
    public static extern ulong AsyncQueryQuotaResult_total_quota_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncQueryQuotaResult_available_quota_set")]
    public static extern void AsyncQueryQuotaResult_available_quota_set(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncQueryQuotaResult_available_quota_get")]
    public static extern ulong AsyncQueryQuotaResult_available_quota_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncQueryQuotaResult__SWIG_1")]
    public static extern IntPtr new_AsyncQueryQuotaResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncQueryQuotaResult")]
    public static extern void delete_AsyncQueryQuotaResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ShareStorageToSpaceWorkResult__SWIG_0")]
    public static extern IntPtr new_ShareStorageToSpaceWorkResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_ShareStorageToSpaceWorkResult_space_work_id_set")]
    public static extern void ShareStorageToSpaceWorkResult_space_work_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_ShareStorageToSpaceWorkResult_space_work_id_get")]
    public static extern IntPtr ShareStorageToSpaceWorkResult_space_work_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_ShareStorageToSpaceWorkResult__SWIG_1")]
    public static extern IntPtr new_ShareStorageToSpaceWorkResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_ShareStorageToSpaceWorkResult")]
    public static extern void delete_ShareStorageToSpaceWorkResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncReadFileResult__SWIG_0")]
    public static extern IntPtr new_AsyncReadFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_filename_set")]
    public static extern void AsyncReadFileResult_filename_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_filename_get")]
    public static extern string AsyncReadFileResult_filename_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_data_set")]
    public static extern void AsyncReadFileResult_data_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_data_get")]
    public static extern string AsyncReadFileResult_data_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_offset_set")]
    public static extern void AsyncReadFileResult_offset_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_offset_get")]
    public static extern int AsyncReadFileResult_offset_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_try_read_length_set")]
    public static extern void AsyncReadFileResult_try_read_length_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_try_read_length_get")]
    public static extern uint AsyncReadFileResult_try_read_length_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncReadFileResult__SWIG_1")]
    public static extern IntPtr new_AsyncReadFileResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncReadFileResult")]
    public static extern void delete_AsyncReadFileResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncWriteFileResult__SWIG_0")]
    public static extern IntPtr new_AsyncWriteFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_filename_set")]
    public static extern void AsyncWriteFileResult_filename_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_filename_get")]
    public static extern string AsyncWriteFileResult_filename_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_offset_set")]
    public static extern void AsyncWriteFileResult_offset_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_offset_get")]
    public static extern int AsyncWriteFileResult_offset_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_try_write_length_set")]
    public static extern void AsyncWriteFileResult_try_write_length_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_try_write_length_get")]
    public static extern uint AsyncWriteFileResult_try_write_length_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_written_length_set")]
    public static extern void AsyncWriteFileResult_written_length_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_written_length_get")]
    public static extern uint AsyncWriteFileResult_written_length_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncWriteFileResult__SWIG_1")]
    public static extern IntPtr new_AsyncWriteFileResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncWriteFileResult")]
    public static extern void delete_AsyncWriteFileResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncReadStreamFileResult__SWIG_0")]
    public static extern IntPtr new_AsyncReadStreamFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_filename_set")]
    public static extern void AsyncReadStreamFileResult_filename_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_filename_get")]
    public static extern string AsyncReadStreamFileResult_filename_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_data_set")]
    public static extern void AsyncReadStreamFileResult_data_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_data_get")]
    public static extern string AsyncReadStreamFileResult_data_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_offset_set")]
    public static extern void AsyncReadStreamFileResult_offset_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_offset_get")]
    public static extern int AsyncReadStreamFileResult_offset_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_try_read_length_set")]
    public static extern void AsyncReadStreamFileResult_try_read_length_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_try_read_length_get")]
    public static extern uint AsyncReadStreamFileResult_try_read_length_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncReadStreamFileResult__SWIG_1")]
    public static extern IntPtr new_AsyncReadStreamFileResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncReadStreamFileResult")]
    public static extern void delete_AsyncReadStreamFileResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncWriteStreamFileResult__SWIG_0")]
    public static extern IntPtr new_AsyncWriteStreamFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_filename_set")]
    public static extern void AsyncWriteStreamFileResult_filename_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_filename_get")]
    public static extern string AsyncWriteStreamFileResult_filename_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_offset_set")]
    public static extern void AsyncWriteStreamFileResult_offset_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_offset_get")]
    public static extern int AsyncWriteStreamFileResult_offset_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_try_write_length_set")]
    public static extern void AsyncWriteStreamFileResult_try_write_length_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_try_write_length_get")]
    public static extern uint AsyncWriteStreamFileResult_try_write_length_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_written_length_set")]
    public static extern void AsyncWriteStreamFileResult_written_length_set(
      IntPtr jarg1,
      uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_written_length_get")]
    public static extern uint AsyncWriteStreamFileResult_written_length_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncWriteStreamFileResult__SWIG_1")]
    public static extern IntPtr new_AsyncWriteStreamFileResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncWriteStreamFileResult")]
    public static extern void delete_AsyncWriteStreamFileResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncListFileResult__SWIG_0")]
    public static extern IntPtr new_AsyncListFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_file_list_set")]
    public static extern void AsyncListFileResult_file_list_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_file_list_get")]
    public static extern IntPtr AsyncListFileResult_file_list_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_start_index_set")]
    public static extern void AsyncListFileResult_start_index_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_start_index_get")]
    public static extern uint AsyncListFileResult_start_index_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_try_list_file_num_set")]
    public static extern void AsyncListFileResult_try_list_file_num_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_try_list_file_num_get")]
    public static extern uint AsyncListFileResult_try_list_file_num_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_all_file_num_set")]
    public static extern void AsyncListFileResult_all_file_num_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_all_file_num_get")]
    public static extern uint AsyncListFileResult_all_file_num_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncListFileResult__SWIG_1")]
    public static extern IntPtr new_AsyncListFileResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncListFileResult")]
    public static extern void delete_AsyncListFileResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRenameStreamFileResult__SWIG_0")]
    public static extern IntPtr new_AsyncRenameStreamFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRenameStreamFileResult_old_filename_set")]
    public static extern void AsyncRenameStreamFileResult_old_filename_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRenameStreamFileResult_old_filename_get")]
    public static extern string AsyncRenameStreamFileResult_old_filename_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRenameStreamFileResult_new_filename_set")]
    public static extern void AsyncRenameStreamFileResult_new_filename_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRenameStreamFileResult_new_filename_get")]
    public static extern string AsyncRenameStreamFileResult_new_filename_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRenameStreamFileResult__SWIG_1")]
    public static extern IntPtr new_AsyncRenameStreamFileResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncRenameStreamFileResult")]
    public static extern void delete_AsyncRenameStreamFileResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncDeleteStreamFileResult__SWIG_0")]
    public static extern IntPtr new_AsyncDeleteStreamFileResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncDeleteStreamFileResult_filename_set")]
    public static extern void AsyncDeleteStreamFileResult_filename_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncDeleteStreamFileResult_filename_get")]
    public static extern string AsyncDeleteStreamFileResult_filename_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AsyncDeleteStreamFileResult__SWIG_1")]
    public static extern IntPtr new_AsyncDeleteStreamFileResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncDeleteStreamFileResult")]
    public static extern void delete_AsyncDeleteStreamFileResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerPersonalInfo__SWIG_0")]
    public static extern IntPtr new_PlayerPersonalInfo__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_id_set")]
    public static extern void PlayerPersonalInfo_rail_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_id_get")]
    public static extern IntPtr PlayerPersonalInfo_rail_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_level_set")]
    public static extern void PlayerPersonalInfo_rail_level_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_level_get")]
    public static extern uint PlayerPersonalInfo_rail_level_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_name_set")]
    public static extern void PlayerPersonalInfo_rail_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_name_get")]
    public static extern string PlayerPersonalInfo_rail_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_avatar_url_set")]
    public static extern void PlayerPersonalInfo_avatar_url_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_avatar_url_get")]
    public static extern string PlayerPersonalInfo_avatar_url_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_state_set")]
    public static extern void PlayerPersonalInfo_state_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_state_get")]
    public static extern int PlayerPersonalInfo_state_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_err_code_set")]
    public static extern void PlayerPersonalInfo_err_code_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_err_code_get")]
    public static extern int PlayerPersonalInfo_err_code_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerPersonalInfo__SWIG_1")]
    public static extern IntPtr new_PlayerPersonalInfo__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerPersonalInfo")]
    public static extern void delete_PlayerPersonalInfo(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AcquireSessionTicketResponse__SWIG_0")]
    public static extern IntPtr new_AcquireSessionTicketResponse__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_AcquireSessionTicketResponse_session_ticket_set")]
    public static extern void AcquireSessionTicketResponse_session_ticket_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_AcquireSessionTicketResponse_session_ticket_get")]
    public static extern IntPtr AcquireSessionTicketResponse_session_ticket_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_AcquireSessionTicketResponse__SWIG_1")]
    public static extern IntPtr new_AcquireSessionTicketResponse__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_AcquireSessionTicketResponse")]
    public static extern void delete_AcquireSessionTicketResponse(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_StartSessionWithPlayerResponse__SWIG_0")]
    public static extern IntPtr new_StartSessionWithPlayerResponse__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_StartSessionWithPlayerResponse_remote_rail_id_set")]
    public static extern void StartSessionWithPlayerResponse_remote_rail_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_StartSessionWithPlayerResponse_remote_rail_id_get")]
    public static extern IntPtr StartSessionWithPlayerResponse_remote_rail_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_StartSessionWithPlayerResponse__SWIG_1")]
    public static extern IntPtr new_StartSessionWithPlayerResponse__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_StartSessionWithPlayerResponse")]
    public static extern void delete_StartSessionWithPlayerResponse(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerGetGamePurchaseKeyResult__SWIG_0")]
    public static extern IntPtr new_PlayerGetGamePurchaseKeyResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerGetGamePurchaseKeyResult_purchase_key_set")]
    public static extern void PlayerGetGamePurchaseKeyResult_purchase_key_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerGetGamePurchaseKeyResult_purchase_key_get")]
    public static extern string PlayerGetGamePurchaseKeyResult_purchase_key_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerGetGamePurchaseKeyResult__SWIG_1")]
    public static extern IntPtr new_PlayerGetGamePurchaseKeyResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerGetGamePurchaseKeyResult")]
    public static extern void delete_PlayerGetGamePurchaseKeyResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceCaptureOption__SWIG_0")]
    public static extern IntPtr new_RailVoiceCaptureOption__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureOption_voice_data_format_set")]
    public static extern void RailVoiceCaptureOption_voice_data_format_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureOption_voice_data_format_get")]
    public static extern int RailVoiceCaptureOption_voice_data_format_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceCaptureOption__SWIG_1")]
    public static extern IntPtr new_RailVoiceCaptureOption__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RailVoiceCaptureOption")]
    public static extern void delete_RailVoiceCaptureOption(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateVoiceChannelOption__SWIG_0")]
    public static extern IntPtr new_CreateVoiceChannelOption__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CreateVoiceChannelOption_speaker_state_set")]
    public static extern void CreateVoiceChannelOption_speaker_state_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateVoiceChannelOption_speaker_state_get")]
    public static extern int CreateVoiceChannelOption_speaker_state_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateVoiceChannelOption__SWIG_1")]
    public static extern IntPtr new_CreateVoiceChannelOption__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateVoiceChannelOption")]
    public static extern void delete_CreateVoiceChannelOption(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateVoiceChannelResult__SWIG_0")]
    public static extern IntPtr new_CreateVoiceChannelResult__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_CreateVoiceChannelResult_voice_channel_id_set")]
    public static extern void CreateVoiceChannelResult_voice_channel_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateVoiceChannelResult_voice_channel_id_get")]
    public static extern IntPtr CreateVoiceChannelResult_voice_channel_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_CreateVoiceChannelResult__SWIG_1")]
    public static extern IntPtr new_CreateVoiceChannelResult__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_CreateVoiceChannelResult")]
    public static extern void delete_CreateVoiceChannelResult(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_VoiceDataCapturedEvent__SWIG_0")]
    public static extern IntPtr new_VoiceDataCapturedEvent__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_VoiceDataCapturedEvent_is_last_package_set")]
    public static extern void VoiceDataCapturedEvent_is_last_package_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_VoiceDataCapturedEvent_is_last_package_get")]
    public static extern bool VoiceDataCapturedEvent_is_last_package_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_VoiceDataCapturedEvent__SWIG_1")]
    public static extern IntPtr new_VoiceDataCapturedEvent__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_VoiceDataCapturedEvent")]
    public static extern void delete_VoiceDataCapturedEvent(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAchievementHelper_CreatePlayerAchievement")]
    public static extern IntPtr IRailAchievementHelper_CreatePlayerAchievement(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAchievementHelper_GetGlobalAchievement")]
    public static extern IntPtr IRailAchievementHelper_GetGlobalAchievement(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailAchievementHelper")]
    public static extern void delete_IRailAchievementHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_GetRailID")]
    public static extern IntPtr IRailPlayerAchievement_GetRailID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_AsyncRequestAchievement")]
    public static extern int IRailPlayerAchievement_AsyncRequestAchievement(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_HasAchieved")]
    public static extern int IRailPlayerAchievement_HasAchieved(
      IntPtr jarg1,
      string jarg2,
      out bool jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_GetAchievementInfo")]
    public static extern int IRailPlayerAchievement_GetAchievementInfo(
      IntPtr jarg1,
      string jarg2,
      IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_0")]
    public static extern int IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      uint jarg3,
      uint jarg4,
      string jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_1")]
    public static extern int IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_1(
      IntPtr jarg1,
      string jarg2,
      uint jarg3,
      uint jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_2")]
    public static extern int IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_2(
      IntPtr jarg1,
      string jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_MakeAchievement")]
    public static extern int IRailPlayerAchievement_MakeAchievement(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_CancelAchievement")]
    public static extern int IRailPlayerAchievement_CancelAchievement(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_AsyncStoreAchievement")]
    public static extern int IRailPlayerAchievement_AsyncStoreAchievement(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_ResetAllAchievements")]
    public static extern int IRailPlayerAchievement_ResetAllAchievements(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_GetAllAchievementsName")]
    public static extern int IRailPlayerAchievement_GetAllAchievementsName(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailPlayerAchievement")]
    public static extern void delete_IRailPlayerAchievement(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalAchievement_AsyncRequestAchievement")]
    public static extern int IRailGlobalAchievement_AsyncRequestAchievement(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalAchievement_GetGlobalAchievedPercent")]
    public static extern int IRailGlobalAchievement_GetGlobalAchievedPercent(
      IntPtr jarg1,
      string jarg2,
      out double jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalAchievement_GetGlobalAchievedPercentDescending")]
    public static extern int IRailGlobalAchievement_GetGlobalAchievedPercentDescending(
      IntPtr jarg1,
      int jarg2,
      IntPtr jarg3,
      out double jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailGlobalAchievement")]
    public static extern void delete_IRailGlobalAchievement(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerAchievementReceived__SWIG_0")]
    public static extern IntPtr new_PlayerAchievementReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerAchievementReceived__SWIG_1")]
    public static extern IntPtr new_PlayerAchievementReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerAchievementReceived")]
    public static extern void delete_PlayerAchievementReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerAchievementStored__SWIG_0")]
    public static extern IntPtr new_PlayerAchievementStored__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_group_achievement_set")]
    public static extern void PlayerAchievementStored_group_achievement_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_group_achievement_get")]
    public static extern bool PlayerAchievementStored_group_achievement_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_achievement_name_set")]
    public static extern void PlayerAchievementStored_achievement_name_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_achievement_name_get")]
    public static extern string PlayerAchievementStored_achievement_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_current_progress_set")]
    public static extern void PlayerAchievementStored_current_progress_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_current_progress_get")]
    public static extern uint PlayerAchievementStored_current_progress_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_max_progress_set")]
    public static extern void PlayerAchievementStored_max_progress_set(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_max_progress_get")]
    public static extern uint PlayerAchievementStored_max_progress_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerAchievementStored__SWIG_1")]
    public static extern IntPtr new_PlayerAchievementStored__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerAchievementStored")]
    public static extern void delete_PlayerAchievementStored(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GlobalAchievementReceived__SWIG_0")]
    public static extern IntPtr new_GlobalAchievementReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_GlobalAchievementReceived_count_set")]
    public static extern void GlobalAchievementReceived_count_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_GlobalAchievementReceived_count_get")]
    public static extern int GlobalAchievementReceived_count_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GlobalAchievementReceived__SWIG_1")]
    public static extern IntPtr new_GlobalAchievementReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GlobalAchievementReceived")]
    public static extern void delete_GlobalAchievementReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssetsHelper_OpenAssets")]
    public static extern IntPtr IRailAssetsHelper_OpenAssets(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailAssetsHelper")]
    public static extern void delete_IRailAssetsHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncRequestAllAssets")]
    public static extern int IRailAssets_AsyncRequestAllAssets(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_QueryAssetInfo")]
    public static extern int IRailAssets_QueryAssetInfo(IntPtr jarg1, ulong jarg2, IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncUpdateAssetsProperty")]
    public static extern int IRailAssets_AsyncUpdateAssetsProperty(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncDirectConsumeAssets")]
    public static extern int IRailAssets_AsyncDirectConsumeAssets(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncStartConsumeAsset")]
    public static extern int IRailAssets_AsyncStartConsumeAsset(
      IntPtr jarg1,
      ulong jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncUpdateConsumeProgress")]
    public static extern int IRailAssets_AsyncUpdateConsumeProgress(
      IntPtr jarg1,
      ulong jarg2,
      string jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncCompleteConsumeAsset")]
    public static extern int IRailAssets_AsyncCompleteConsumeAsset(
      IntPtr jarg1,
      ulong jarg2,
      uint jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncExchangeAssets")]
    public static extern int IRailAssets_AsyncExchangeAssets(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncExchangeAssetsTo")]
    public static extern int IRailAssets_AsyncExchangeAssetsTo(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      ulong jarg4,
      string jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncSplitAsset")]
    public static extern int IRailAssets_AsyncSplitAsset(
      IntPtr jarg1,
      ulong jarg2,
      uint jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncSplitAssetTo")]
    public static extern int IRailAssets_AsyncSplitAssetTo(
      IntPtr jarg1,
      ulong jarg2,
      uint jarg3,
      ulong jarg4,
      string jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncMergeAsset")]
    public static extern int IRailAssets_AsyncMergeAsset(IntPtr jarg1, IntPtr jarg2, string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncMergeAssetTo")]
    public static extern int IRailAssets_AsyncMergeAssetTo(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailAssets")]
    public static extern void delete_IRailAssets(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_AsyncCreateBrowser__SWIG_0")]
    public static extern IntPtr IRailBrowserHelper_AsyncCreateBrowser__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      uint jarg3,
      uint jarg4,
      string jarg5,
      IntPtr jarg6,
      out int jarg7);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_AsyncCreateBrowser__SWIG_1")]
    public static extern IntPtr IRailBrowserHelper_AsyncCreateBrowser__SWIG_1(
      IntPtr jarg1,
      string jarg2,
      uint jarg3,
      uint jarg4,
      string jarg5,
      IntPtr jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_AsyncCreateBrowser__SWIG_2")]
    public static extern IntPtr IRailBrowserHelper_AsyncCreateBrowser__SWIG_2(
      IntPtr jarg1,
      string jarg2,
      uint jarg3,
      uint jarg4,
      string jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_0")]
    public static extern IntPtr IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      string jarg3,
      IntPtr jarg4,
      out int jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_1")]
    public static extern IntPtr IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_1(
      IntPtr jarg1,
      string jarg2,
      string jarg3,
      IntPtr jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_2")]
    public static extern IntPtr IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_2(
      IntPtr jarg1,
      string jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_NavigateWebPage")]
    public static extern int IRailBrowserHelper_NavigateWebPage(
      IntPtr jarg1,
      string jarg2,
      bool jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailBrowserHelper")]
    public static extern void delete_IRailBrowserHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_GetCurrentUrl")]
    public static extern bool IRailBrowser_GetCurrentUrl(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_ReloadWithUrl__SWIG_0")]
    public static extern bool IRailBrowser_ReloadWithUrl__SWIG_0(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_ReloadWithUrl__SWIG_1")]
    public static extern bool IRailBrowser_ReloadWithUrl__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_StopLoad")]
    public static extern void IRailBrowser_StopLoad(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_AddJavascriptEventListener")]
    public static extern bool IRailBrowser_AddJavascriptEventListener(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_RemoveAllJavascriptEventListener")]
    public static extern bool IRailBrowser_RemoveAllJavascriptEventListener(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_AllowNavigateNewPage")]
    public static extern void IRailBrowser_AllowNavigateNewPage(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_Close")]
    public static extern void IRailBrowser_Close(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailBrowser")]
    public static extern void delete_IRailBrowser(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_GetCurrentUrl")]
    public static extern bool IRailBrowserRender_GetCurrentUrl(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_ReloadWithUrl__SWIG_0")]
    public static extern bool IRailBrowserRender_ReloadWithUrl__SWIG_0(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_ReloadWithUrl__SWIG_1")]
    public static extern bool IRailBrowserRender_ReloadWithUrl__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_StopLoad")]
    public static extern void IRailBrowserRender_StopLoad(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_AddJavascriptEventListener")]
    public static extern bool IRailBrowserRender_AddJavascriptEventListener(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_RemoveAllJavascriptEventListener")]
    public static extern bool IRailBrowserRender_RemoveAllJavascriptEventListener(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_AllowNavigateNewPage")]
    public static extern void IRailBrowserRender_AllowNavigateNewPage(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_Close")]
    public static extern void IRailBrowserRender_Close(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_UpdateCustomDrawWindowPos")]
    public static extern void IRailBrowserRender_UpdateCustomDrawWindowPos(
      IntPtr jarg1,
      int jarg2,
      int jarg3,
      uint jarg4,
      uint jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_SetBrowserActive")]
    public static extern void IRailBrowserRender_SetBrowserActive(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_GoBack")]
    public static extern void IRailBrowserRender_GoBack(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_GoForward")]
    public static extern void IRailBrowserRender_GoForward(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_ExecuteJavascript")]
    public static extern bool IRailBrowserRender_ExecuteJavascript(
      IntPtr jarg1,
      string jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_DispatchWindowsMessage")]
    public static extern void IRailBrowserRender_DispatchWindowsMessage(
      IntPtr jarg1,
      uint jarg2,
      uint jarg3,
      uint jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_DispatchMouseMessage")]
    public static extern void IRailBrowserRender_DispatchMouseMessage(
      IntPtr jarg1,
      int jarg2,
      uint jarg3,
      uint jarg4,
      uint jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_MouseWheel")]
    public static extern void IRailBrowserRender_MouseWheel(
      IntPtr jarg1,
      int jarg2,
      uint jarg3,
      uint jarg4,
      uint jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_SetFocus")]
    public static extern void IRailBrowserRender_SetFocus(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_KeyDown")]
    public static extern void IRailBrowserRender_KeyDown(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_KeyUp")]
    public static extern void IRailBrowserRender_KeyUp(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_KeyChar")]
    public static extern void IRailBrowserRender_KeyChar(IntPtr jarg1, uint jarg2, bool jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailBrowserRender")]
    public static extern void delete_IRailBrowserRender(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_AsyncQueryIsOwnedDlcsOnServer")]
    public static extern int IRailDlcHelper_AsyncQueryIsOwnedDlcsOnServer(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_AsyncCheckAllDlcsStateReady")]
    public static extern int IRailDlcHelper_AsyncCheckAllDlcsStateReady(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_IsDlcInstalled__SWIG_0")]
    public static extern bool IRailDlcHelper_IsDlcInstalled__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_IsDlcInstalled__SWIG_1")]
    public static extern bool IRailDlcHelper_IsDlcInstalled__SWIG_1(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_IsOwnedDlc")]
    public static extern bool IRailDlcHelper_IsOwnedDlc(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_GetDlcCount")]
    public static extern uint IRailDlcHelper_GetDlcCount(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_GetDlcInfo")]
    public static extern bool IRailDlcHelper_GetDlcInfo(IntPtr jarg1, uint jarg2, IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_AsyncInstallDlc")]
    public static extern bool IRailDlcHelper_AsyncInstallDlc(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_AsyncRemoveDlc")]
    public static extern bool IRailDlcHelper_AsyncRemoveDlc(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailDlcHelper")]
    public static extern void delete_IRailDlcHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_AsyncShowRailFloatingWindow")]
    public static extern int IRailFloatingWindow_AsyncShowRailFloatingWindow(
      IntPtr jarg1,
      int jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_SetNotifyWindowPosition")]
    public static extern int IRailFloatingWindow_SetNotifyWindowPosition(
      IntPtr jarg1,
      int jarg2,
      int jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_AsyncShowStoreWindow")]
    public static extern int IRailFloatingWindow_AsyncShowStoreWindow(
      IntPtr jarg1,
      ulong jarg2,
      IntPtr jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_IsFloatingWindowAvailable")]
    public static extern bool IRailFloatingWindow_IsFloatingWindowAvailable(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_AsyncShowDefaultGameStoreWindow")]
    public static extern int IRailFloatingWindow_AsyncShowDefaultGameStoreWindow(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_AsyncShowChatWindowWithFriend")]
    public static extern int IRailFloatingWindow_AsyncShowChatWindowWithFriend(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailFloatingWindow")]
    public static extern void delete_IRailFloatingWindow(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncGetPersonalInfo")]
    public static extern int IRailFriends_AsyncGetPersonalInfo(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncGetFriendMetadata")]
    public static extern int IRailFriends_AsyncGetFriendMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncSetMyMetadata")]
    public static extern int IRailFriends_AsyncSetMyMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncClearAllMyMetadata")]
    public static extern int IRailFriends_AsyncClearAllMyMetadata(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncSetInviteCommandLine")]
    public static extern int IRailFriends_AsyncSetInviteCommandLine(
      IntPtr jarg1,
      string jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncGetInviteCommandLine")]
    public static extern int IRailFriends_AsyncGetInviteCommandLine(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncReportPlayedWithUserList")]
    public static extern int IRailFriends_AsyncReportPlayedWithUserList(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_GetFriendType")]
    public static extern int IRailFriends_GetFriendType(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_GetFriendsList")]
    public static extern int IRailFriends_GetFriendsList(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailFriends")]
    public static extern void delete_IRailFriends(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncGetGameServerPlayerList")]
    public static extern int IRailGameServerHelper_AsyncGetGameServerPlayerList(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncGetGameServerList")]
    public static extern int IRailGameServerHelper_AsyncGetGameServerList(
      IntPtr jarg1,
      uint jarg2,
      uint jarg3,
      IntPtr jarg4,
      IntPtr jarg5,
      string jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncCreateGameServer__SWIG_0")]
    public static extern IntPtr IRailGameServerHelper_AsyncCreateGameServer__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncCreateGameServer__SWIG_1")]
    public static extern IntPtr IRailGameServerHelper_AsyncCreateGameServer__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncCreateGameServer__SWIG_2")]
    public static extern IntPtr IRailGameServerHelper_AsyncCreateGameServer__SWIG_2(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncCreateGameServer__SWIG_3")]
    public static extern IntPtr IRailGameServerHelper_AsyncCreateGameServer__SWIG_3(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_0")]
    public static extern int IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_0(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_1")]
    public static extern int IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_0")]
    public static extern int IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_1")]
    public static extern int IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_0")]
    public static extern int IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_1")]
    public static extern int IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailGameServerHelper")]
    public static extern void delete_IRailGameServerHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerRailID")]
    public static extern IntPtr IRailGameServer_GetGameServerRailID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerName")]
    public static extern int IRailGameServer_GetGameServerName(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerFullname")]
    public static extern int IRailGameServer_GetGameServerFullname(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetOwnerRailID")]
    public static extern IntPtr IRailGameServer_GetOwnerRailID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetZoneID")]
    public static extern bool IRailGameServer_SetZoneID(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetZoneID")]
    public static extern ulong IRailGameServer_GetZoneID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetChannelID")]
    public static extern bool IRailGameServer_SetChannelID(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetChannelID")]
    public static extern ulong IRailGameServer_GetChannelID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetHost")]
    public static extern bool IRailGameServer_SetHost(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetHost")]
    public static extern bool IRailGameServer_GetHost(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetMapName")]
    public static extern bool IRailGameServer_SetMapName(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetMapName")]
    public static extern bool IRailGameServer_GetMapName(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetPasswordProtect")]
    public static extern bool IRailGameServer_SetPasswordProtect(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetPasswordProtect")]
    public static extern bool IRailGameServer_GetPasswordProtect(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetMaxPlayers")]
    public static extern bool IRailGameServer_SetMaxPlayers(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetMaxPlayers")]
    public static extern uint IRailGameServer_GetMaxPlayers(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetBotPlayers")]
    public static extern bool IRailGameServer_SetBotPlayers(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetBotPlayers")]
    public static extern uint IRailGameServer_GetBotPlayers(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetGameServerDescription")]
    public static extern bool IRailGameServer_SetGameServerDescription(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerDescription")]
    public static extern bool IRailGameServer_GetGameServerDescription(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetGameServerTags")]
    public static extern bool IRailGameServer_SetGameServerTags(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerTags")]
    public static extern bool IRailGameServer_GetGameServerTags(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetMods")]
    public static extern bool IRailGameServer_SetMods(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetMods")]
    public static extern bool IRailGameServer_GetMods(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetSpectatorHost")]
    public static extern bool IRailGameServer_SetSpectatorHost(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetSpectatorHost")]
    public static extern bool IRailGameServer_GetSpectatorHost(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetGameServerVersion")]
    public static extern bool IRailGameServer_SetGameServerVersion(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerVersion")]
    public static extern bool IRailGameServer_GetGameServerVersion(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetIsFriendOnly")]
    public static extern bool IRailGameServer_SetIsFriendOnly(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetIsFriendOnly")]
    public static extern bool IRailGameServer_GetIsFriendOnly(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_ClearAllMetadata")]
    public static extern bool IRailGameServer_ClearAllMetadata(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetMetadata")]
    public static extern int IRailGameServer_GetMetadata(IntPtr jarg1, string jarg2, IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetMetadata")]
    public static extern int IRailGameServer_SetMetadata(IntPtr jarg1, string jarg2, string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AsyncSetMetadata")]
    public static extern int IRailGameServer_AsyncSetMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AsyncGetMetadata")]
    public static extern int IRailGameServer_AsyncGetMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AsyncGetAllMetadata")]
    public static extern int IRailGameServer_AsyncGetAllMetadata(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AsyncAcquireGameServerSessionTicket")]
    public static extern int IRailGameServer_AsyncAcquireGameServerSessionTicket(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AsyncStartSessionWithPlayer")]
    public static extern int IRailGameServer_AsyncStartSessionWithPlayer(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_TerminateSessionOfPlayer")]
    public static extern void IRailGameServer_TerminateSessionOfPlayer(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AbandonGameServerSessionTicket")]
    public static extern void IRailGameServer_AbandonGameServerSessionTicket(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_ReportPlayerJoinGameServer")]
    public static extern int IRailGameServer_ReportPlayerJoinGameServer(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_ReportPlayerQuitGameServer")]
    public static extern int IRailGameServer_ReportPlayerQuitGameServer(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_UpdateGameServerPlayerList")]
    public static extern int IRailGameServer_UpdateGameServerPlayerList(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetCurrentPlayers")]
    public static extern uint IRailGameServer_GetCurrentPlayers(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_RemoveAllPlayers")]
    public static extern void IRailGameServer_RemoveAllPlayers(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_RegisterToGameServerList")]
    public static extern int IRailGameServer_RegisterToGameServerList(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_UnregisterFromGameServerList")]
    public static extern int IRailGameServer_UnregisterFromGameServerList(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_CloseGameServer")]
    public static extern int IRailGameServer_CloseGameServer(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetFriendsInGameServer")]
    public static extern int IRailGameServer_GetFriendsInGameServer(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_IsUserInGameServer")]
    public static extern bool IRailGameServer_IsUserInGameServer(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetServerInfo")]
    public static extern bool IRailGameServer_SetServerInfo(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetServerInfo")]
    public static extern bool IRailGameServer_GetServerInfo(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_EnableTeamVoice")]
    public static extern int IRailGameServer_EnableTeamVoice(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailGameServer")]
    public static extern void delete_IRailGameServer(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_AsyncRequestAllPurchasableProducts")]
    public static extern int IRailInGamePurchase_AsyncRequestAllPurchasableProducts(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_AsyncRequestAllProducts")]
    public static extern int IRailInGamePurchase_AsyncRequestAllProducts(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_GetProductInfo")]
    public static extern int IRailInGamePurchase_GetProductInfo(
      IntPtr jarg1,
      uint jarg2,
      IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_AsyncPurchaseProducts")]
    public static extern int IRailInGamePurchase_AsyncPurchaseProducts(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_AsyncFinishOrder")]
    public static extern int IRailInGamePurchase_AsyncFinishOrder(
      IntPtr jarg1,
      string jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_AsyncPurchaseProductsToAssets")]
    public static extern int IRailInGamePurchase_AsyncPurchaseProductsToAssets(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailInGamePurchase")]
    public static extern void delete_IRailInGamePurchase(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardParameters__SWIG_0")]
    public static extern IntPtr new_LeaderboardParameters__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardParameters_param_set")]
    public static extern void LeaderboardParameters_param_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardParameters_param_get")]
    public static extern string LeaderboardParameters_param_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardParameters__SWIG_1")]
    public static extern IntPtr new_LeaderboardParameters__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardParameters")]
    public static extern void delete_LeaderboardParameters(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RequestLeaderboardEntryParam__SWIG_0")]
    public static extern IntPtr new_RequestLeaderboardEntryParam__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_type_set")]
    public static extern void RequestLeaderboardEntryParam_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_type_get")]
    public static extern int RequestLeaderboardEntryParam_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_range_start_set")]
    public static extern void RequestLeaderboardEntryParam_range_start_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_range_start_get")]
    public static extern int RequestLeaderboardEntryParam_range_start_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_range_end_set")]
    public static extern void RequestLeaderboardEntryParam_range_end_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_range_end_get")]
    public static extern int RequestLeaderboardEntryParam_range_end_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_user_coordinate_set")]
    public static extern void RequestLeaderboardEntryParam_user_coordinate_set(
      IntPtr jarg1,
      bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_user_coordinate_get")]
    public static extern bool RequestLeaderboardEntryParam_user_coordinate_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_RequestLeaderboardEntryParam__SWIG_1")]
    public static extern IntPtr new_RequestLeaderboardEntryParam__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_RequestLeaderboardEntryParam")]
    public static extern void delete_RequestLeaderboardEntryParam(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardData__SWIG_0")]
    public static extern IntPtr new_LeaderboardData__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_score_set")]
    public static extern void LeaderboardData_score_set(IntPtr jarg1, double jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_score_get")]
    public static extern double LeaderboardData_score_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_rank_set")]
    public static extern void LeaderboardData_rank_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_rank_get")]
    public static extern int LeaderboardData_rank_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_spacework_id_set")]
    public static extern void LeaderboardData_spacework_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_spacework_id_get")]
    public static extern IntPtr LeaderboardData_spacework_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_additional_infomation_set")]
    public static extern void LeaderboardData_additional_infomation_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_additional_infomation_get")]
    public static extern string LeaderboardData_additional_infomation_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardData__SWIG_1")]
    public static extern IntPtr new_LeaderboardData__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardData")]
    public static extern void delete_LeaderboardData(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardEntry__SWIG_0")]
    public static extern IntPtr new_LeaderboardEntry__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntry_player_id_set")]
    public static extern void LeaderboardEntry_player_id_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntry_player_id_get")]
    public static extern IntPtr LeaderboardEntry_player_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntry_data_set")]
    public static extern void LeaderboardEntry_data_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntry_data_get")]
    public static extern IntPtr LeaderboardEntry_data_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardEntry__SWIG_1")]
    public static extern IntPtr new_LeaderboardEntry__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardEntry")]
    public static extern void delete_LeaderboardEntry(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_UploadLeaderboardParam__SWIG_0")]
    public static extern IntPtr new_UploadLeaderboardParam__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_UploadLeaderboardParam_type_set")]
    public static extern void UploadLeaderboardParam_type_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_UploadLeaderboardParam_type_get")]
    public static extern int UploadLeaderboardParam_type_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_UploadLeaderboardParam_data_set")]
    public static extern void UploadLeaderboardParam_data_set(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_UploadLeaderboardParam_data_get")]
    public static extern IntPtr UploadLeaderboardParam_data_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_UploadLeaderboardParam__SWIG_1")]
    public static extern IntPtr new_UploadLeaderboardParam__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_UploadLeaderboardParam")]
    public static extern void delete_UploadLeaderboardParam(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailLeaderboardHelper")]
    public static extern void delete_IRailLeaderboardHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardHelper_OpenLeaderboard")]
    public static extern IntPtr IRailLeaderboardHelper_OpenLeaderboard(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_GetLeaderboardName")]
    public static extern string IRailLeaderboard_GetLeaderboardName(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_GetTotalEntriesCount")]
    public static extern int IRailLeaderboard_GetTotalEntriesCount(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_AsyncGetLeaderboard")]
    public static extern int IRailLeaderboard_AsyncGetLeaderboard(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_GetLeaderboardParameters")]
    public static extern int IRailLeaderboard_GetLeaderboardParameters(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_CreateLeaderboardEntries")]
    public static extern IntPtr IRailLeaderboard_CreateLeaderboardEntries(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_AsyncUploadLeaderboard")]
    public static extern int IRailLeaderboard_AsyncUploadLeaderboard(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_GetLeaderboardSortType")]
    public static extern int IRailLeaderboard_GetLeaderboardSortType(IntPtr jarg1, out int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_GetLeaderboardDisplayType")]
    public static extern int IRailLeaderboard_GetLeaderboardDisplayType(IntPtr jarg1, out int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_AsyncAttachSpaceWork")]
    public static extern int IRailLeaderboard_AsyncAttachSpaceWork(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailLeaderboard")]
    public static extern void delete_IRailLeaderboard(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_GetRailID")]
    public static extern IntPtr IRailLeaderboardEntries_GetRailID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_GetLeaderboardName")]
    public static extern string IRailLeaderboardEntries_GetLeaderboardName(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_AsyncRequestLeaderboardEntries")]
    public static extern int IRailLeaderboardEntries_AsyncRequestLeaderboardEntries(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_GetEntriesParam")]
    public static extern IntPtr IRailLeaderboardEntries_GetEntriesParam(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_GetEntriesCount")]
    public static extern int IRailLeaderboardEntries_GetEntriesCount(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_GetLeaderboardEntry")]
    public static extern int IRailLeaderboardEntries_GetLeaderboardEntry(
      IntPtr jarg1,
      int jarg2,
      IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailLeaderboardEntries")]
    public static extern void delete_IRailLeaderboardEntries(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardReceived__SWIG_0")]
    public static extern IntPtr new_LeaderboardReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardReceived_leaderboard_name_set")]
    public static extern void LeaderboardReceived_leaderboard_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardReceived_leaderboard_name_get")]
    public static extern string LeaderboardReceived_leaderboard_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardReceived_does_exist_set")]
    public static extern void LeaderboardReceived_does_exist_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardReceived_does_exist_get")]
    public static extern bool LeaderboardReceived_does_exist_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardReceived__SWIG_1")]
    public static extern IntPtr new_LeaderboardReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardReceived")]
    public static extern void delete_LeaderboardReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardEntryReceived__SWIG_0")]
    public static extern IntPtr new_LeaderboardEntryReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntryReceived_leaderboard_name_set")]
    public static extern void LeaderboardEntryReceived_leaderboard_name_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntryReceived_leaderboard_name_get")]
    public static extern string LeaderboardEntryReceived_leaderboard_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardEntryReceived__SWIG_1")]
    public static extern IntPtr new_LeaderboardEntryReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardEntryReceived")]
    public static extern void delete_LeaderboardEntryReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardUploaded__SWIG_0")]
    public static extern IntPtr new_LeaderboardUploaded__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_leaderboard_name_set")]
    public static extern void LeaderboardUploaded_leaderboard_name_set(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_leaderboard_name_get")]
    public static extern string LeaderboardUploaded_leaderboard_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_score_set")]
    public static extern void LeaderboardUploaded_score_set(IntPtr jarg1, double jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_score_get")]
    public static extern double LeaderboardUploaded_score_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_better_score_set")]
    public static extern void LeaderboardUploaded_better_score_set(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_better_score_get")]
    public static extern bool LeaderboardUploaded_better_score_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_new_rank_set")]
    public static extern void LeaderboardUploaded_new_rank_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_new_rank_get")]
    public static extern int LeaderboardUploaded_new_rank_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_old_rank_set")]
    public static extern void LeaderboardUploaded_old_rank_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_old_rank_get")]
    public static extern int LeaderboardUploaded_old_rank_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardUploaded__SWIG_1")]
    public static extern IntPtr new_LeaderboardUploaded__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardUploaded")]
    public static extern void delete_LeaderboardUploaded(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardAttachSpaceWork__SWIG_0")]
    public static extern IntPtr new_LeaderboardAttachSpaceWork__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardAttachSpaceWork_leaderboard_name_set")]
    public static extern void LeaderboardAttachSpaceWork_leaderboard_name_set(
      IntPtr jarg1,
      string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardAttachSpaceWork_leaderboard_name_get")]
    public static extern string LeaderboardAttachSpaceWork_leaderboard_name_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardAttachSpaceWork_spacework_id_set")]
    public static extern void LeaderboardAttachSpaceWork_spacework_id_set(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardAttachSpaceWork_spacework_id_get")]
    public static extern IntPtr LeaderboardAttachSpaceWork_spacework_id_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardAttachSpaceWork__SWIG_1")]
    public static extern IntPtr new_LeaderboardAttachSpaceWork__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardAttachSpaceWork")]
    public static extern void delete_LeaderboardAttachSpaceWork(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_AcceptSessionRequest")]
    public static extern int IRailNetwork_AcceptSessionRequest(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_SendData__SWIG_0")]
    public static extern int IRailNetwork_SendData__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg4,
      uint jarg5,
      uint jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_SendData__SWIG_1")]
    public static extern int IRailNetwork_SendData__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg4,
      uint jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_SendReliableData__SWIG_0")]
    public static extern int IRailNetwork_SendReliableData__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg4,
      uint jarg5,
      uint jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_SendReliableData__SWIG_1")]
    public static extern int IRailNetwork_SendReliableData__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg4,
      uint jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_IsDataReady__SWIG_0")]
    public static extern bool IRailNetwork_IsDataReady__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      out uint jarg3,
      out uint jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_IsDataReady__SWIG_1")]
    public static extern bool IRailNetwork_IsDataReady__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      out uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_ReadData__SWIG_0")]
    public static extern int IRailNetwork_ReadData__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg4,
      uint jarg5,
      uint jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_ReadData__SWIG_1")]
    public static extern int IRailNetwork_ReadData__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg4,
      uint jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_BlockMessageType")]
    public static extern int IRailNetwork_BlockMessageType(IntPtr jarg1, IntPtr jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_UnblockMessageType")]
    public static extern int IRailNetwork_UnblockMessageType(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_CloseSession")]
    public static extern int IRailNetwork_CloseSession(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_ResolveHostname")]
    public static extern int IRailNetwork_ResolveHostname(IntPtr jarg1, string jarg2, IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailNetwork")]
    public static extern void delete_IRailNetwork(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_AsyncCreateChannel")]
    public static extern int IRailNetChannel_AsyncCreateChannel(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_AsyncJoinChannel")]
    public static extern int IRailNetChannel_AsyncJoinChannel(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_AsyncInviteMemberToChannel")]
    public static extern int IRailNetChannel_AsyncInviteMemberToChannel(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      IntPtr jarg4,
      string jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_GetAllMembers")]
    public static extern int IRailNetChannel_GetAllMembers(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      IntPtr jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_SendDataToChannel__SWIG_0")]
    public static extern int IRailNetChannel_SendDataToChannel__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg4,
      uint jarg5,
      uint jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_SendDataToChannel__SWIG_1")]
    public static extern int IRailNetChannel_SendDataToChannel__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg4,
      uint jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_SendDataToMember__SWIG_0")]
    public static extern int IRailNetChannel_SendDataToMember__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      IntPtr jarg4,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg5,
      uint jarg6,
      uint jarg7);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_SendDataToMember__SWIG_1")]
    public static extern int IRailNetChannel_SendDataToMember__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      IntPtr jarg4,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg5,
      uint jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_IsDataReady__SWIG_0")]
    public static extern bool IRailNetChannel_IsDataReady__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      out ulong jarg3,
      out uint jarg4,
      out uint jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_IsDataReady__SWIG_1")]
    public static extern bool IRailNetChannel_IsDataReady__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      out ulong jarg3,
      out uint jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_ReadData__SWIG_0")]
    public static extern int IRailNetChannel_ReadData__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      IntPtr jarg4,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg5,
      uint jarg6,
      uint jarg7);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_ReadData__SWIG_1")]
    public static extern int IRailNetChannel_ReadData__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      IntPtr jarg4,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg5,
      uint jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_BlockMessageType")]
    public static extern int IRailNetChannel_BlockMessageType(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      uint jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_UnblockMessageType")]
    public static extern int IRailNetChannel_UnblockMessageType(
      IntPtr jarg1,
      IntPtr jarg2,
      ulong jarg3,
      uint jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailNetChannel_ExitChannel")]
    public static extern int IRailNetChannel_ExitChannel(IntPtr jarg1, IntPtr jarg2, ulong jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailNetChannel")]
    public static extern void delete_IRailNetChannel(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AlreadyLoggedIn")]
    public static extern bool IRailPlayer_AlreadyLoggedIn(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_GetRailID")]
    public static extern IntPtr IRailPlayer_GetRailID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_GetPlayerDataPath")]
    public static extern int IRailPlayer_GetPlayerDataPath(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AsyncAcquireSessionTicket")]
    public static extern int IRailPlayer_AsyncAcquireSessionTicket(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AsyncStartSessionWithPlayer")]
    public static extern int IRailPlayer_AsyncStartSessionWithPlayer(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_TerminateSessionOfPlayer")]
    public static extern void IRailPlayer_TerminateSessionOfPlayer(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AbandonSessionTicket")]
    public static extern void IRailPlayer_AbandonSessionTicket(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_GetPlayerName")]
    public static extern int IRailPlayer_GetPlayerName(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_GetPlayerOwnershipType")]
    public static extern int IRailPlayer_GetPlayerOwnershipType(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AsyncGetGamePurchaseKey")]
    public static extern int IRailPlayer_AsyncGetGamePurchaseKey(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailPlayer")]
    public static extern void delete_IRailPlayer(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailZoneHelper_AsyncGetZoneList")]
    public static extern int IRailZoneHelper_AsyncGetZoneList(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailZoneHelper_AsyncGetRoomListInZone")]
    public static extern int IRailZoneHelper_AsyncGetRoomListInZone(
      IntPtr jarg1,
      ulong jarg2,
      uint jarg3,
      uint jarg4,
      IntPtr jarg5,
      IntPtr jarg6,
      string jarg7);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailZoneHelper")]
    public static extern void delete_IRailZoneHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_set_current_zone_id")]
    public static extern void IRailRoomHelper_set_current_zone_id(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_get_current_zone_id")]
    public static extern ulong IRailRoomHelper_get_current_zone_id(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_CreateRoom")]
    public static extern IntPtr IRailRoomHelper_CreateRoom(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3,
      out int jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_AsyncCreateRoom")]
    public static extern IntPtr IRailRoomHelper_AsyncCreateRoom(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_OpenRoom")]
    public static extern IntPtr IRailRoomHelper_OpenRoom(
      IntPtr jarg1,
      ulong jarg2,
      ulong jarg3,
      out int jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_AsyncGetUserRoomList")]
    public static extern int IRailRoomHelper_AsyncGetUserRoomList(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailRoomHelper")]
    public static extern void delete_IRailRoomHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetRoomId")]
    public static extern ulong IRailRoom_GetRoomId(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetRoomName")]
    public static extern int IRailRoom_GetRoomName(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetZoneId")]
    public static extern ulong IRailRoom_GetZoneId(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetOwnerId")]
    public static extern IntPtr IRailRoom_GetOwnerId(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetHasPassword")]
    public static extern int IRailRoom_GetHasPassword(IntPtr jarg1, out bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetRoomType")]
    public static extern int IRailRoom_GetRoomType(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetNewOwner")]
    public static extern bool IRailRoom_SetNewOwner(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncGetRoomMembers")]
    public static extern int IRailRoom_AsyncGetRoomMembers(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_Leave")]
    public static extern void IRailRoom_Leave(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncJoinRoom")]
    public static extern int IRailRoom_AsyncJoinRoom(IntPtr jarg1, string jarg2, string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncGetAllRoomData")]
    public static extern int IRailRoom_AsyncGetAllRoomData(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncKickOffMember")]
    public static extern int IRailRoom_AsyncKickOffMember(IntPtr jarg1, IntPtr jarg2, string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetRoomMetadata")]
    public static extern bool IRailRoom_GetRoomMetadata(IntPtr jarg1, string jarg2, IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetRoomMetadata")]
    public static extern bool IRailRoom_SetRoomMetadata(IntPtr jarg1, string jarg2, string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncSetRoomMetadata")]
    public static extern int IRailRoom_AsyncSetRoomMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncGetRoomMetadata")]
    public static extern int IRailRoom_AsyncGetRoomMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncClearRoomMetadata")]
    public static extern int IRailRoom_AsyncClearRoomMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetMemberMetadata")]
    public static extern bool IRailRoom_GetMemberMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3,
      IntPtr jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetMemberMetadata")]
    public static extern bool IRailRoom_SetMemberMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncGetMemberMetadata")]
    public static extern int IRailRoom_AsyncGetMemberMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncSetMemberMetadata")]
    public static extern int IRailRoom_AsyncSetMemberMetadata(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SendDataToMember__SWIG_0")]
    public static extern int IRailRoom_SendDataToMember__SWIG_0(
      IntPtr jarg1,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2,
      uint jarg3,
      uint jarg4,
      IntPtr jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SendDataToMember__SWIG_1")]
    public static extern int IRailRoom_SendDataToMember__SWIG_1(
      IntPtr jarg1,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2,
      uint jarg3,
      uint jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SendDataToMember__SWIG_2")]
    public static extern int IRailRoom_SendDataToMember__SWIG_2(
      IntPtr jarg1,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2,
      uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetNumOfMembers")]
    public static extern uint IRailRoom_GetNumOfMembers(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetMemberByIndex")]
    public static extern IntPtr IRailRoom_GetMemberByIndex(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetMemberNameByIndex")]
    public static extern int IRailRoom_GetMemberNameByIndex(IntPtr jarg1, uint jarg2, IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetMaxMembers")]
    public static extern uint IRailRoom_GetMaxMembers(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetGameServerID")]
    public static extern bool IRailRoom_SetGameServerID(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetGameServerChannelID")]
    public static extern bool IRailRoom_SetGameServerChannelID(IntPtr jarg1, ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetGameServerID")]
    public static extern bool IRailRoom_GetGameServerID(IntPtr jarg1, out ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetGameServerChannelID")]
    public static extern bool IRailRoom_GetGameServerChannelID(IntPtr jarg1, out ulong jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetRoomJoinable")]
    public static extern bool IRailRoom_SetRoomJoinable(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetRoomJoinable")]
    public static extern bool IRailRoom_GetRoomJoinable(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetFriendsInRoom")]
    public static extern int IRailRoom_GetFriendsInRoom(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_IsUserInRoom")]
    public static extern bool IRailRoom_IsUserInRoom(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_EnableTeamVoice")]
    public static extern int IRailRoom_EnableTeamVoice(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailRoom")]
    public static extern void delete_IRailRoom(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshotHelper_CreateScreenshotWithRawData")]
    public static extern IntPtr IRailScreenshotHelper_CreateScreenshotWithRawData(
      IntPtr jarg1,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2,
      uint jarg3,
      uint jarg4,
      uint jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshotHelper_CreateScreenshotWithLocalImage")]
    public static extern IntPtr IRailScreenshotHelper_CreateScreenshotWithLocalImage(
      IntPtr jarg1,
      string jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshotHelper_AsyncTakeScreenshot")]
    public static extern void IRailScreenshotHelper_AsyncTakeScreenshot(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshotHelper_HookScreenshotHotKey")]
    public static extern void IRailScreenshotHelper_HookScreenshotHotKey(IntPtr jarg1, bool jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailScreenshotHelper")]
    public static extern void delete_IRailScreenshotHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshot_SetLocation")]
    public static extern bool IRailScreenshot_SetLocation(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshot_SetUsers")]
    public static extern bool IRailScreenshot_SetUsers(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshot_AssociatePublishedFiles")]
    public static extern bool IRailScreenshot_AssociatePublishedFiles(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshot_AsyncPublishScreenshot")]
    public static extern int IRailScreenshot_AsyncPublishScreenshot(
      IntPtr jarg1,
      string jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailScreenshot")]
    public static extern void delete_IRailScreenshot(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStatisticHelper_CreatePlayerStats")]
    public static extern IntPtr IRailStatisticHelper_CreatePlayerStats(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStatisticHelper_GetGlobalStats")]
    public static extern IntPtr IRailStatisticHelper_GetGlobalStats(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStatisticHelper_AsyncGetNumberOfPlayer")]
    public static extern int IRailStatisticHelper_AsyncGetNumberOfPlayer(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailStatisticHelper")]
    public static extern void delete_IRailStatisticHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_GetRailID")]
    public static extern IntPtr IRailPlayerStats_GetRailID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_AsyncRequestStats")]
    public static extern int IRailPlayerStats_AsyncRequestStats(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_GetStatValue__SWIG_0")]
    public static extern int IRailPlayerStats_GetStatValue__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      out int jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_GetStatValue__SWIG_1")]
    public static extern int IRailPlayerStats_GetStatValue__SWIG_1(
      IntPtr jarg1,
      string jarg2,
      out double jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_SetStatValue__SWIG_0")]
    public static extern int IRailPlayerStats_SetStatValue__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      int jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_SetStatValue__SWIG_1")]
    public static extern int IRailPlayerStats_SetStatValue__SWIG_1(
      IntPtr jarg1,
      string jarg2,
      double jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_UpdateAverageStatValue")]
    public static extern int IRailPlayerStats_UpdateAverageStatValue(
      IntPtr jarg1,
      string jarg2,
      double jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_AsyncStoreStats")]
    public static extern int IRailPlayerStats_AsyncStoreStats(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_ResetAllStats")]
    public static extern int IRailPlayerStats_ResetAllStats(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailPlayerStats")]
    public static extern void delete_IRailPlayerStats(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_AsyncRequestGlobalStats")]
    public static extern int IRailGlobalStats_AsyncRequestGlobalStats(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_GetGlobalStatValue__SWIG_0")]
    public static extern int IRailGlobalStats_GetGlobalStatValue__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      out long jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_GetGlobalStatValue__SWIG_1")]
    public static extern int IRailGlobalStats_GetGlobalStatValue__SWIG_1(
      IntPtr jarg1,
      string jarg2,
      out double jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_GetGlobalStatValueHistory__SWIG_0")]
    public static extern int IRailGlobalStats_GetGlobalStatValueHistory__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      [MarshalAs(UnmanagedType.LPArray), Out] long[] jarg3,
      uint jarg4,
      out int jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_GetGlobalStatValueHistory__SWIG_1")]
    public static extern int IRailGlobalStats_GetGlobalStatValueHistory__SWIG_1(
      IntPtr jarg1,
      string jarg2,
      [MarshalAs(UnmanagedType.LPArray), Out] double[] jarg3,
      uint jarg4,
      out int jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailGlobalStats")]
    public static extern void delete_IRailGlobalStats(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerStatsReceived__SWIG_0")]
    public static extern IntPtr new_PlayerStatsReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerStatsReceived__SWIG_1")]
    public static extern IntPtr new_PlayerStatsReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerStatsReceived")]
    public static extern void delete_PlayerStatsReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerStatsStored__SWIG_0")]
    public static extern IntPtr new_PlayerStatsStored__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_PlayerStatsStored__SWIG_1")]
    public static extern IntPtr new_PlayerStatsStored__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerStatsStored")]
    public static extern void delete_PlayerStatsStored(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NumberOfPlayerReceived__SWIG_0")]
    public static extern IntPtr new_NumberOfPlayerReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_NumberOfPlayerReceived_online_number_set")]
    public static extern void NumberOfPlayerReceived_online_number_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NumberOfPlayerReceived_online_number_get")]
    public static extern int NumberOfPlayerReceived_online_number_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NumberOfPlayerReceived_offline_number_set")]
    public static extern void NumberOfPlayerReceived_offline_number_set(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_NumberOfPlayerReceived_offline_number_get")]
    public static extern int NumberOfPlayerReceived_offline_number_get(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_NumberOfPlayerReceived__SWIG_1")]
    public static extern IntPtr new_NumberOfPlayerReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_NumberOfPlayerReceived")]
    public static extern void delete_NumberOfPlayerReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_new_GlobalStatsRequestReceived__SWIG_0")]
    public static extern IntPtr new_GlobalStatsRequestReceived__SWIG_0();

    [DllImport("rail_api", EntryPoint = "CSharp_new_GlobalStatsRequestReceived__SWIG_1")]
    public static extern IntPtr new_GlobalStatsRequestReceived__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_GlobalStatsRequestReceived")]
    public static extern void delete_GlobalStatsRequestReceived(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailStorageHelper")]
    public static extern void delete_IRailStorageHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_OpenFile__SWIG_0")]
    public static extern IntPtr IRailStorageHelper_OpenFile__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      out int jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_OpenFile__SWIG_1")]
    public static extern IntPtr IRailStorageHelper_OpenFile__SWIG_1(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_CreateFile__SWIG_0")]
    public static extern IntPtr IRailStorageHelper_CreateFile__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      out int jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_CreateFile__SWIG_1")]
    public static extern IntPtr IRailStorageHelper_CreateFile__SWIG_1(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_IsFileExist")]
    public static extern bool IRailStorageHelper_IsFileExist(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_ListFiles")]
    public static extern bool IRailStorageHelper_ListFiles(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_RemoveFile")]
    public static extern int IRailStorageHelper_RemoveFile(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_IsFileSyncedToCloud")]
    public static extern bool IRailStorageHelper_IsFileSyncedToCloud(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_GetFileTimestamp")]
    public static extern int IRailStorageHelper_GetFileTimestamp(
      IntPtr jarg1,
      string jarg2,
      out ulong jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_GetFileCount")]
    public static extern uint IRailStorageHelper_GetFileCount(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_GetFileNameAndSize")]
    public static extern int IRailStorageHelper_GetFileNameAndSize(
      IntPtr jarg1,
      uint jarg2,
      IntPtr jarg3,
      out ulong jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_AsyncQueryQuota")]
    public static extern int IRailStorageHelper_AsyncQueryQuota(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_SetSyncFileOption")]
    public static extern int IRailStorageHelper_SetSyncFileOption(
      IntPtr jarg1,
      string jarg2,
      IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_IsCloudStorageEnabledForApp")]
    public static extern bool IRailStorageHelper_IsCloudStorageEnabledForApp(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_IsCloudStorageEnabledForPlayer")]
    public static extern bool IRailStorageHelper_IsCloudStorageEnabledForPlayer(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_AsyncPublishFileToUserSpace")]
    public static extern int IRailStorageHelper_AsyncPublishFileToUserSpace(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_OpenStreamFile__SWIG_0")]
    public static extern IntPtr IRailStorageHelper_OpenStreamFile__SWIG_0(
      IntPtr jarg1,
      string jarg2,
      IntPtr jarg3,
      out int jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_OpenStreamFile__SWIG_1")]
    public static extern IntPtr IRailStorageHelper_OpenStreamFile__SWIG_1(
      IntPtr jarg1,
      string jarg2,
      IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_AsyncListStreamFiles")]
    public static extern int IRailStorageHelper_AsyncListStreamFiles(
      IntPtr jarg1,
      string jarg2,
      IntPtr jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_AsyncRenameStreamFile")]
    public static extern int IRailStorageHelper_AsyncRenameStreamFile(
      IntPtr jarg1,
      string jarg2,
      string jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_AsyncDeleteStreamFile")]
    public static extern int IRailStorageHelper_AsyncDeleteStreamFile(
      IntPtr jarg1,
      string jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailFile")]
    public static extern void delete_IRailFile(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFile_GetFilename")]
    public static extern string IRailFile_GetFilename(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFile_Read__SWIG_0")]
    public static extern uint IRailFile_Read__SWIG_0(
      IntPtr jarg1,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2,
      uint jarg3,
      out int jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFile_Read__SWIG_1")]
    public static extern uint IRailFile_Read__SWIG_1(IntPtr jarg1, [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFile_Write__SWIG_0")]
    public static extern uint IRailFile_Write__SWIG_0(
      IntPtr jarg1,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2,
      uint jarg3,
      out int jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFile_Write__SWIG_1")]
    public static extern uint IRailFile_Write__SWIG_1(IntPtr jarg1, [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2, uint jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFile_AsyncRead")]
    public static extern int IRailFile_AsyncRead(IntPtr jarg1, uint jarg2, string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFile_AsyncWrite")]
    public static extern int IRailFile_AsyncWrite(
      IntPtr jarg1,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2,
      uint jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFile_GetSize")]
    public static extern uint IRailFile_GetSize(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFile_Close")]
    public static extern void IRailFile_Close(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailStreamFile")]
    public static extern void delete_IRailStreamFile(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_GetFilename")]
    public static extern string IRailStreamFile_GetFilename(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_AsyncRead")]
    public static extern int IRailStreamFile_AsyncRead(
      IntPtr jarg1,
      int jarg2,
      uint jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_AsyncWrite")]
    public static extern int IRailStreamFile_AsyncWrite(
      IntPtr jarg1,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2,
      uint jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_GetSize")]
    public static extern ulong IRailStreamFile_GetSize(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_Close")]
    public static extern int IRailStreamFile_Close(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_Cancel")]
    public static extern void IRailStreamFile_Cancel(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncGetUsersInfo")]
    public static extern int IRailUsersHelper_AsyncGetUsersInfo(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncInviteUsers")]
    public static extern int IRailUsersHelper_AsyncInviteUsers(
      IntPtr jarg1,
      string jarg2,
      IntPtr jarg3,
      IntPtr jarg4,
      string jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncGetInviteDetail")]
    public static extern int IRailUsersHelper_AsyncGetInviteDetail(
      IntPtr jarg1,
      IntPtr jarg2,
      int jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncCancelInvite")]
    public static extern int IRailUsersHelper_AsyncCancelInvite(
      IntPtr jarg1,
      int jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncCancelAllInvites")]
    public static extern int IRailUsersHelper_AsyncCancelAllInvites(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailUsersHelper")]
    public static extern void delete_IRailUsersHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_0")]
    public static extern int IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_0(
      IntPtr jarg1,
      uint jarg2,
      uint jarg3,
      int jarg4,
      IntPtr jarg5,
      string jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_1")]
    public static extern int IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_1(
      IntPtr jarg1,
      uint jarg2,
      uint jarg3,
      int jarg4,
      IntPtr jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_2")]
    public static extern int IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_2(
      IntPtr jarg1,
      uint jarg2,
      uint jarg3,
      int jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_0")]
    public static extern int IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_0(
      IntPtr jarg1,
      uint jarg2,
      uint jarg3,
      int jarg4,
      IntPtr jarg5,
      string jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_1")]
    public static extern int IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_1(
      IntPtr jarg1,
      uint jarg2,
      uint jarg3,
      int jarg4,
      IntPtr jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_2")]
    public static extern int IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_2(
      IntPtr jarg1,
      uint jarg2,
      uint jarg3,
      int jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_0")]
    public static extern int IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3,
      uint jarg4,
      int jarg5,
      IntPtr jarg6,
      string jarg7);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_1")]
    public static extern int IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3,
      uint jarg4,
      int jarg5,
      IntPtr jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_2")]
    public static extern int IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_2(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3,
      uint jarg4,
      int jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_3")]
    public static extern int IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_3(
      IntPtr jarg1,
      IntPtr jarg2,
      uint jarg3,
      uint jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncSubscribeSpaceWorks")]
    public static extern int IRailUserSpaceHelper_AsyncSubscribeSpaceWorks(
      IntPtr jarg1,
      IntPtr jarg2,
      bool jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_OpenSpaceWork")]
    public static extern IntPtr IRailUserSpaceHelper_OpenSpaceWork(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_CreateSpaceWork")]
    public static extern IntPtr IRailUserSpaceHelper_CreateSpaceWork(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_GetMySubscribedWorks")]
    public static extern int IRailUserSpaceHelper_GetMySubscribedWorks(
      IntPtr jarg1,
      uint jarg2,
      uint jarg3,
      int jarg4,
      IntPtr jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_GetMySubscribedWorksCount")]
    public static extern uint IRailUserSpaceHelper_GetMySubscribedWorksCount(
      IntPtr jarg1,
      int jarg2,
      out int jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncRemoveSpaceWork")]
    public static extern int IRailUserSpaceHelper_AsyncRemoveSpaceWork(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncModifyFavoritesWorks")]
    public static extern int IRailUserSpaceHelper_AsyncModifyFavoritesWorks(
      IntPtr jarg1,
      IntPtr jarg2,
      int jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncVoteSpaceWork")]
    public static extern int IRailUserSpaceHelper_AsyncVoteSpaceWork(
      IntPtr jarg1,
      IntPtr jarg2,
      int jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncSearchSpaceWork")]
    public static extern int IRailUserSpaceHelper_AsyncSearchSpaceWork(
      IntPtr jarg1,
      IntPtr jarg2,
      IntPtr jarg3,
      IntPtr jarg4,
      uint jarg5,
      uint jarg6,
      string jarg7);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailUserSpaceHelper")]
    public static extern void delete_IRailUserSpaceHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_Close")]
    public static extern void IRailSpaceWork_Close(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetSpaceWorkID")]
    public static extern IntPtr IRailSpaceWork_GetSpaceWorkID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_Editable")]
    public static extern bool IRailSpaceWork_Editable(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_StartSync")]
    public static extern int IRailSpaceWork_StartSync(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetSyncProgress")]
    public static extern int IRailSpaceWork_GetSyncProgress(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_CancelSync")]
    public static extern int IRailSpaceWork_CancelSync(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetWorkLocalFolder")]
    public static extern int IRailSpaceWork_GetWorkLocalFolder(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_AsyncUpdateMetadata")]
    public static extern int IRailSpaceWork_AsyncUpdateMetadata(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetName")]
    public static extern int IRailSpaceWork_GetName(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetDescription")]
    public static extern int IRailSpaceWork_GetDescription(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetUrl")]
    public static extern int IRailSpaceWork_GetUrl(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetCreateTime")]
    public static extern uint IRailSpaceWork_GetCreateTime(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetLastUpdateTime")]
    public static extern uint IRailSpaceWork_GetLastUpdateTime(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetWorkFileSize")]
    public static extern ulong IRailSpaceWork_GetWorkFileSize(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetTags")]
    public static extern int IRailSpaceWork_GetTags(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetPreviewImage")]
    public static extern int IRailSpaceWork_GetPreviewImage(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetVersion")]
    public static extern int IRailSpaceWork_GetVersion(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetDownloadCount")]
    public static extern ulong IRailSpaceWork_GetDownloadCount(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetSubscribedCount")]
    public static extern ulong IRailSpaceWork_GetSubscribedCount(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetShareLevel")]
    public static extern int IRailSpaceWork_GetShareLevel(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetScore")]
    public static extern ulong IRailSpaceWork_GetScore(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetMetadata")]
    public static extern int IRailSpaceWork_GetMetadata(IntPtr jarg1, string jarg2, IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetMyVote")]
    public static extern int IRailSpaceWork_GetMyVote(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_IsFavorite")]
    public static extern bool IRailSpaceWork_IsFavorite(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_IsSubscribed")]
    public static extern bool IRailSpaceWork_IsSubscribed(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetName")]
    public static extern int IRailSpaceWork_SetName(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetDescription")]
    public static extern int IRailSpaceWork_SetDescription(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetTags")]
    public static extern int IRailSpaceWork_SetTags(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetPreviewImage")]
    public static extern int IRailSpaceWork_SetPreviewImage(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetVersion")]
    public static extern int IRailSpaceWork_SetVersion(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetShareLevel__SWIG_0")]
    public static extern int IRailSpaceWork_SetShareLevel__SWIG_0(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetShareLevel__SWIG_1")]
    public static extern int IRailSpaceWork_SetShareLevel__SWIG_1(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetMetadata")]
    public static extern int IRailSpaceWork_SetMetadata(IntPtr jarg1, string jarg2, string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetContentFromFolder")]
    public static extern int IRailSpaceWork_SetContentFromFolder(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetAllMetadata")]
    public static extern int IRailSpaceWork_GetAllMetadata(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetAdditionalPreviewUrls")]
    public static extern int IRailSpaceWork_GetAdditionalPreviewUrls(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetAssociatedSpaceWorks")]
    public static extern int IRailSpaceWork_GetAssociatedSpaceWorks(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetLanguages")]
    public static extern int IRailSpaceWork_GetLanguages(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_RemoveMetadata")]
    public static extern int IRailSpaceWork_RemoveMetadata(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetAdditionalPreviews")]
    public static extern int IRailSpaceWork_SetAdditionalPreviews(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetAssociatedSpaceWorks")]
    public static extern int IRailSpaceWork_SetAssociatedSpaceWorks(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetLanguages")]
    public static extern int IRailSpaceWork_SetLanguages(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetPreviewUrl")]
    public static extern int IRailSpaceWork_GetPreviewUrl(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetVoteDetail")]
    public static extern int IRailSpaceWork_GetVoteDetail(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetUploaderIDs")]
    public static extern int IRailSpaceWork_GetUploaderIDs(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetUpdateOptions")]
    public static extern int IRailSpaceWork_SetUpdateOptions(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetStatistic")]
    public static extern int IRailSpaceWork_GetStatistic(IntPtr jarg1, int jarg2, out ulong jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailSpaceWork")]
    public static extern void delete_IRailSpaceWork(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetTimeCountSinceGameLaunch")]
    public static extern uint IRailUtils_GetTimeCountSinceGameLaunch(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetTimeCountSinceComputerLaunch")]
    public static extern uint IRailUtils_GetTimeCountSinceComputerLaunch(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetTimeFromServer")]
    public static extern uint IRailUtils_GetTimeFromServer(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetGameID")]
    public static extern IntPtr IRailUtils_GetGameID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_AsyncGetImageData")]
    public static extern int IRailUtils_AsyncGetImageData(
      IntPtr jarg1,
      string jarg2,
      uint jarg3,
      uint jarg4,
      string jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetErrorString")]
    public static extern void IRailUtils_GetErrorString(IntPtr jarg1, int jarg2, IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_DirtyWordsFilter")]
    public static extern int IRailUtils_DirtyWordsFilter(
      IntPtr jarg1,
      string jarg2,
      bool jarg3,
      IntPtr jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetRailPlatformType")]
    public static extern int IRailUtils_GetRailPlatformType(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetLaunchAppParameters")]
    public static extern int IRailUtils_GetLaunchAppParameters(
      IntPtr jarg1,
      int jarg2,
      IntPtr jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetPlatformLanguageCode")]
    public static extern int IRailUtils_GetPlatformLanguageCode(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_SetWarningMessageCallback")]
    public static extern int IRailUtils_SetWarningMessageCallback(
      IntPtr jarg1,
      RailWarningMessageCallbackFunction jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailUtils")]
    public static extern void delete_IRailUtils(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailApps_MarkGameContentDamaged")]
    public static extern int IRailApps_MarkGameContentDamaged(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailApps_GetGameInstallPath")]
    public static extern int IRailApps_GetGameInstallPath(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailApps_GetGameLanguageCode")]
    public static extern int IRailApps_GetGameLanguageCode(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailApps_SetGameState")]
    public static extern int IRailApps_SetGameState(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailApps_GetGameState")]
    public static extern int IRailApps_GetGameState(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailApps_GetGameEarliestPurchaseTime")]
    public static extern uint IRailApps_GetGameEarliestPurchaseTime(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailApps_GetCurrentBranchInfo")]
    public static extern int IRailApps_GetCurrentBranchInfo(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailApps_AsyncQuerySubscribeWishPlayState")]
    public static extern int IRailApps_AsyncQuerySubscribeWishPlayState(IntPtr jarg1, string jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailApps")]
    public static extern void delete_IRailApps(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailVoiceHelper")]
    public static extern void delete_IRailVoiceHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_0")]
    public static extern IntPtr IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3,
      string jarg4,
      out int jarg5);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_1")]
    public static extern IntPtr IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_1(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3,
      string jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_2")]
    public static extern IntPtr IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_2(
      IntPtr jarg1,
      IntPtr jarg2,
      string jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_3")]
    public static extern IntPtr IRailVoiceHelper_AsyncCreateVoiceChannel__SWIG_3(
      IntPtr jarg1,
      IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_OpenVoiceChannel")]
    public static extern IntPtr IRailVoiceHelper_OpenVoiceChannel(
      IntPtr jarg1,
      IntPtr jarg2,
      out int jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_SetupVoiceCapture__SWIG_0")]
    public static extern int IRailVoiceHelper_SetupVoiceCapture__SWIG_0(
      IntPtr jarg1,
      IntPtr jarg2,
      RailCaptureVoiceCallback jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_SetupVoiceCapture__SWIG_1")]
    public static extern int IRailVoiceHelper_SetupVoiceCapture__SWIG_1(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_StartVoiceCapturing__SWIG_0")]
    public static extern int IRailVoiceHelper_StartVoiceCapturing__SWIG_0(
      IntPtr jarg1,
      uint jarg2,
      bool jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_StartVoiceCapturing__SWIG_1")]
    public static extern int IRailVoiceHelper_StartVoiceCapturing__SWIG_1(IntPtr jarg1, uint jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_StartVoiceCapturing__SWIG_2")]
    public static extern int IRailVoiceHelper_StartVoiceCapturing__SWIG_2(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_StopVoiceCapturing")]
    public static extern int IRailVoiceHelper_StopVoiceCapturing(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_GetCapturedVoiceData")]
    public static extern int IRailVoiceHelper_GetCapturedVoiceData(
      IntPtr jarg1,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2,
      uint jarg3,
      out uint jarg4);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_DecodeVoice")]
    public static extern int IRailVoiceHelper_DecodeVoice(
      IntPtr jarg1,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg2,
      uint jarg3,
      [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] jarg4,
      uint jarg5,
      out uint jarg6);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailVoiceChannel")]
    public static extern void delete_IRailVoiceChannel(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_GetVoiceChannelID")]
    public static extern IntPtr IRailVoiceChannel_GetVoiceChannelID(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_GetVoiceChannelName")]
    public static extern int IRailVoiceChannel_GetVoiceChannelName(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_JoinVoiceChannel")]
    public static extern int IRailVoiceChannel_JoinVoiceChannel(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_LeaveVoiceChannel")]
    public static extern int IRailVoiceChannel_LeaveVoiceChannel(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_GetSpeakerState")]
    public static extern int IRailVoiceChannel_GetSpeakerState(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_MuteSpeaker")]
    public static extern int IRailVoiceChannel_MuteSpeaker(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_ResumeSpeaker")]
    public static extern int IRailVoiceChannel_ResumeSpeaker(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_GetUsers")]
    public static extern int IRailVoiceChannel_GetUsers(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_AddUsers")]
    public static extern int IRailVoiceChannel_AddUsers(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_RemoveUsers")]
    public static extern int IRailVoiceChannel_RemoveUsers(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_CloseChannel")]
    public static extern int IRailVoiceChannel_CloseChannel(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSystemHelper_GetRateOfGameRevenue")]
    public static extern float IRailSystemHelper_GetRateOfGameRevenue(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSystemHelper_SetTerminationTimeoutOwnershipExpired")]
    public static extern int IRailSystemHelper_SetTerminationTimeoutOwnershipExpired(
      IntPtr jarg1,
      int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSystemHelper_GetPlatformSystemState")]
    public static extern int IRailSystemHelper_GetPlatformSystemState(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailSystemHelper")]
    public static extern void delete_IRailSystemHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailPlayer")]
    public static extern IntPtr IRailFactory_RailPlayer(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailUsersHelper")]
    public static extern IntPtr IRailFactory_RailUsersHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailFriends")]
    public static extern IntPtr IRailFactory_RailFriends(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailFloatingWindow")]
    public static extern IntPtr IRailFactory_RailFloatingWindow(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailBrowserHelper")]
    public static extern IntPtr IRailFactory_RailBrowserHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailInGamePurchase")]
    public static extern IntPtr IRailFactory_RailInGamePurchase(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailZoneHelper")]
    public static extern IntPtr IRailFactory_RailZoneHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailRoomHelper")]
    public static extern IntPtr IRailFactory_RailRoomHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailGameServerHelper")]
    public static extern IntPtr IRailFactory_RailGameServerHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailStorageHelper")]
    public static extern IntPtr IRailFactory_RailStorageHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailUserSpaceHelper")]
    public static extern IntPtr IRailFactory_RailUserSpaceHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailStatisticHelper")]
    public static extern IntPtr IRailFactory_RailStatisticHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailLeaderboardHelper")]
    public static extern IntPtr IRailFactory_RailLeaderboardHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailAchievementHelper")]
    public static extern IntPtr IRailFactory_RailAchievementHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailNetChannelHelper")]
    public static extern IntPtr IRailFactory_RailNetChannelHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailNetworkHelper")]
    public static extern IntPtr IRailFactory_RailNetworkHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailApps")]
    public static extern IntPtr IRailFactory_RailApps(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailUtils")]
    public static extern IntPtr IRailFactory_RailUtils(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailAssetsHelper")]
    public static extern IntPtr IRailFactory_RailAssetsHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailDlcHelper")]
    public static extern IntPtr IRailFactory_RailDlcHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailScreenshotHelper")]
    public static extern IntPtr IRailFactory_RailScreenshotHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailVoiceHelper")]
    public static extern IntPtr IRailFactory_RailVoiceHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailSystemHelper")]
    public static extern IntPtr IRailFactory_RailSystemHelper(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_delete_IRailFactory")]
    public static extern void delete_IRailFactory(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailNeedRestartAppForCheckingEnvironment")]
    public static extern bool RailNeedRestartAppForCheckingEnvironment(
      IntPtr jarg1,
      int jarg2,
      [MarshalAs(UnmanagedType.LPArray), In] string[] jarg3);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInitialize")]
    public static extern bool RailInitialize();

    [DllImport("rail_api", EntryPoint = "CSharp_RailFinalize")]
    public static extern void RailFinalize();

    [DllImport("rail_api", EntryPoint = "CSharp_RailFireEvents")]
    public static extern void RailFireEvents();

    [DllImport("rail_api", EntryPoint = "CSharp_RailFactory")]
    public static extern IntPtr RailFactory();

    [DllImport("rail_api", EntryPoint = "CSharp_RailGetSdkVersion")]
    public static extern void RailGetSdkVersion(IntPtr jarg1, IntPtr jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CSharpRailRegisterEvent")]
    public static extern void CSharpRailRegisterEvent(int jarg1, RailEventCallBackFunction jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CSharpRailUnRegisterEvent")]
    public static extern void CSharpRailUnRegisterEvent(int jarg1, RailEventCallBackFunction jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_CSharpRailUnRegisterAllEvent")]
    public static extern void CSharpRailUnRegisterAllEvent();

    [DllImport("rail_api", EntryPoint = "CSharp_NewInt")]
    public static extern IntPtr NewInt();

    [DllImport("rail_api", EntryPoint = "CSharp_DeleteInt")]
    public static extern void DeleteInt(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetInt")]
    public static extern int GetInt(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SetInt")]
    public static extern void SetInt(IntPtr jarg1, int jarg2);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsRequestAllAssetsFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsRequestAllAssetsFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeToFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsMergeToFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSubscribeResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUserSpaceSubscribeResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomNotifyMemberChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsStartConsumeFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsStartConsumeFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventInGamePurchasePurchaseProductsResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerListResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerListResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetRoomMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomSetRoomMetadataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCloseResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventBrowserCloseResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionRequest_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventNetworkCreateSessionRequest_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsToFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsExchangeAssetsToFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByUser_SWIGUpcast")]
    public static extern IntPtr RailEventkRailPlatformNotifyEventJoinGameByUser_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallProgress_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventDlcInstallProgress_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersNotifyInviter_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUsersNotifyInviter_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardAttachSpaceWork_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventLeaderboardAttachSpaceWork_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetUserRoomListResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomGetUserRoomListResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTryNavigateNewPageRequest_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventBrowserTryNavigateNewPageRequest_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcUninstallFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventDlcUninstallFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsSplitFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardUploaded_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventLeaderboardUploaded_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelInviteMemmberResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventNetChannelInviteMemmberResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerCreated_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerCreated_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStart_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventDlcInstallStart_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetInviteDetailResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUsersGetInviteDetailResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceQuerySpaceWorksResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUserSpaceQuerySpaceWorksResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementGlobalAchievementReceived_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAchievementGlobalAchievementReceived_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncRenameStreamFileResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStorageAsyncRenameStreamFileResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsReportPlayedWithUserListResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventFriendsReportPlayedWithUserListResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteStreamFileResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStorageAsyncWriteStreamFileResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomKickOffMemberResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomKickOffMemberResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingWindow_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventShowFloatingWindow_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomOwnerChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomNotifyRoomOwnerChanged_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFinalize_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventFinalize_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersCancelInviteResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUsersCancelInviteResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomListResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomListResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserDamageRectPaint_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventBrowserDamageRectPaint_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateAssetPropertyFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsUpdateAssetPropertyFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerGetGamePurchaseKey_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventPlayerGetGamePurchaseKey_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByRoom_SWIGUpcast")]
    public static extern IntPtr RailEventkRailPlatformNotifyEventJoinGameByRoom_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetMemberMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomSetMemberMetadataResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceModifyFavoritesWorksResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUserSpaceModifyFavoritesWorksResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelCreateResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventVoiceChannelCreateResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsDirectConsumeFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsDirectConsumeFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomZoneListResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomZoneListResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStartResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventDlcInstallStartResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDestroyed_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomNotifyRoomDestroyed_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserPaint_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventBrowserPaint_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsExchangeAssetsFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsClearMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventFriendsClearMetadataResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelChannelNetDelay_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventNetChannelChannelNetDelay_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllProductsInfoReceived_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventInGamePurchaseAllProductsInfoReceived_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsMergeFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberkicked_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomNotifyMemberkicked_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomJoinRoomResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomJoinRoomResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAuthSessionTicket_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerAuthSessionTicket_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRegisterToServerListResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerRegisterToServerListResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePaymentResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventInGameStorePurchasePaymentResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcCheckAllDlcsStateReadyResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventDlcCheckAllDlcsStateReadyResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventScreenshotTakeScreenshotFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserReloadResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventBrowserReloadResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelInviteJoinChannelRequest_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventNetChannelInviteJoinChannelRequest_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetRoomMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomGetRoomMetadataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingNotifyWindow_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventShowFloatingNotifyWindow_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowDisplayed_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventInGameStorePurchasePayWindowDisplayed_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceVoteSpaceWorkResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUserSpaceVoteSpaceWorkResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMetadataChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomNotifyMetadataChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerSetMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerSetMetadataResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteUsersResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUsersInviteUsersResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceUpdateMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUserSpaceUpdateMetadataResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateConsumeFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsUpdateConsumeFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceDataCaptured_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventVoiceDataCaptured_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseFinishOrderResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventInGamePurchaseFinishOrderResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelMemberStateChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventNetChannelMemberStateChanged_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerFavoriteGameServers_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerFavoriteGameServers_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserJavascriptEvent_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventBrowserJavascriptEvent_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementReceived_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAchievementPlayerAchievementReceived_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomLeaveRoomResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomLeaveRoomResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomCreated_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomCreated_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageQueryQuotaResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStorageQueryQuotaResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcOwnershipChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventDlcOwnershipChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserNavigeteResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventBrowserNavigeteResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetInviteCommandLine_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventFriendsGetInviteCommandLine_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsNumOfPlayerReceived_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStatsNumOfPlayerReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventDlcInstallFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsGlobalStatsReceived_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStatsGlobalStatsReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsNotifyBuddyListChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventFriendsNotifyBuddyListChanged_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSearchSpaceWorkResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUserSpaceSearchSpaceWorkResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetMemberMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomGetMemberMetadataResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventFriendsGetMetadataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteFileResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStorageAsyncWriteFileResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsReceived_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStatsPlayerStatsReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsCompleteConsumeFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerGetMetadataResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementStored_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAchievementPlayerAchievementStored_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowClosed_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventInGameStorePurchasePayWindowClosed_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadStreamFileResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStorageAsyncReadStreamFileResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTitleChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventBrowserTitleChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMySubscribedWorksResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUserSpaceGetMySubscribedWorksResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageShareToSpaceWorkResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStorageShareToSpaceWorkResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelJoinChannelResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventNetChannelJoinChannelResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUtilsGetImageDataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUtilsGetImageDataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSystemStateChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventSystemStateChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardReceived_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventLeaderboardReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomClearRoomMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomClearRoomMetadataResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetAllDataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomGetAllDataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncListStreamFileResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStorageAsyncListStreamFileResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardEntryReceived_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventLeaderboardEntryReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomGameServerChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomNotifyRoomGameServerChanged_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDataReceived_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomNotifyRoomDataReceived_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcQueryIsOwnedDlcsResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventDlcQueryIsOwnedDlcsResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSyncResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUserSpaceSyncResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcRefundChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventDlcRefundChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerPlayerListResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerPlayerListResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetUsersInfo_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUsersGetUsersInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncDeleteStreamFileResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStorageAsyncDeleteStreamFileResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsSetMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventFriendsSetMetadataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCreateResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventBrowserCreateResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitToFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAssetsSplitToFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotPublishScreenshotFinished_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventScreenshotPublishScreenshotFinished_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelCreateChannelResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventNetChannelCreateChannelResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAddFavoriteGameServer_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerAddFavoriteGameServer_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketGetSessionTicket_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventSessionTicketGetSessionTicket_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRemoveFavoriteGameServer_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerRemoveFavoriteGameServer_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGotRoomMembers_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventRoomGotRoomMembers_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsStored_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStatsPlayerStatsStored_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadFileResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventStorageAsyncReadFileResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketAuthSessionTicket_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventSessionTicketAuthSessionTicket_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotRequest_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventScreenshotTakeScreenshotRequest_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceRemoveSpaceWorkResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUserSpaceRemoveSpaceWorkResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserStateChanged_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventBrowserStateChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetSessionTicket_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventGameServerGetSessionTicket_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersRespondInvation_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUsersRespondInvation_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetChannelChannelException_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventNetChannelChannelException_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteJoinGameResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventUsersInviteJoinGameResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAppQuerySubscribeWishPlayStateResult_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventAppQuerySubscribeWishPlayStateResult_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionFailed_SWIGUpcast")]
    public static extern IntPtr RailEventkRailEventNetworkCreateSessionFailed_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByGameServer_SWIGUpcast")]
    public static extern IntPtr RailEventkRailPlatformNotifyEventJoinGameByGameServer_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_QuerySubscribeWishPlayStateResult_SWIGUpcast")]
    public static extern IntPtr QuerySubscribeWishPlayStateResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMySubscribedWorksResult_SWIGUpcast")]
    public static extern IntPtr AsyncGetMySubscribedWorksResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMyFavoritesWorksResult_SWIGUpcast")]
    public static extern IntPtr AsyncGetMyFavoritesWorksResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncQuerySpaceWorksResult_SWIGUpcast")]
    public static extern IntPtr AsyncQuerySpaceWorksResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncUpdateMetadataResult_SWIGUpcast")]
    public static extern IntPtr AsyncUpdateMetadataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SyncSpaceWorkResult_SWIGUpcast")]
    public static extern IntPtr SyncSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_SWIGUpcast")]
    public static extern IntPtr AsyncSubscribeSpaceWorksResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_SWIGUpcast")]
    public static extern IntPtr AsyncModifyFavoritesWorksResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveSpaceWorkResult_SWIGUpcast")]
    public static extern IntPtr AsyncRemoveSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncVoteSpaceWorkResult_SWIGUpcast")]
    public static extern IntPtr AsyncVoteSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncSearchSpaceWorksResult_SWIGUpcast")]
    public static extern IntPtr AsyncSearchSpaceWorksResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInfoData_SWIGUpcast")]
    public static extern IntPtr RailUsersInfoData_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersNotifyInviter_SWIGUpcast")]
    public static extern IntPtr RailUsersNotifyInviter_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvation_SWIGUpcast")]
    public static extern IntPtr RailUsersRespondInvation_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_SWIGUpcast")]
    public static extern IntPtr RailUsersInviteJoinGameResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_SWIGUpcast")]
    public static extern IntPtr RailUsersGetInviteDetailResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersCancelInviteResult_SWIGUpcast")]
    public static extern IntPtr RailUsersCancelInviteResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteUsersResult_SWIGUpcast")]
    public static extern IntPtr RailUsersInviteUsersResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailGetImageDataResult_SWIGUpcast")]
    public static extern IntPtr RailGetImageDataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_SWIGUpcast")]
    public static extern IntPtr TakeScreenshotResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ScreenshotRequestInfo_SWIGUpcast")]
    public static extern IntPtr ScreenshotRequestInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PublishScreenshotResult_SWIGUpcast")]
    public static extern IntPtr PublishScreenshotResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailSystemStateChanged_SWIGUpcast")]
    public static extern IntPtr RailSystemStateChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByGameServer_SWIGUpcast")]
    public static extern IntPtr RailPlatformNotifyEventJoinGameByGameServer_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_SWIGUpcast")]
    public static extern IntPtr RailPlatformNotifyEventJoinGameByRoom_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByUser_SWIGUpcast")]
    public static extern IntPtr RailPlatformNotifyEventJoinGameByUser_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFinalize_SWIGUpcast")]
    public static extern IntPtr RailFinalize_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RequestAllAssetsFinished_SWIGUpcast")]
    public static extern IntPtr RequestAllAssetsFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_UpdateAssetsPropertyFinished_SWIGUpcast")]
    public static extern IntPtr UpdateAssetsPropertyFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DirectConsumeAssetsFinished_SWIGUpcast")]
    public static extern IntPtr DirectConsumeAssetsFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_StartConsumeAssetsFinished_SWIGUpcast")]
    public static extern IntPtr StartConsumeAssetsFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_UpdateConsumeAssetsFinished_SWIGUpcast")]
    public static extern IntPtr UpdateConsumeAssetsFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CompleteConsumeAssetsFinished_SWIGUpcast")]
    public static extern IntPtr CompleteConsumeAssetsFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_SWIGUpcast")]
    public static extern IntPtr SplitAssetsFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_SWIGUpcast")]
    public static extern IntPtr SplitAssetsToFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsFinished_SWIGUpcast")]
    public static extern IntPtr MergeAssetsFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsToFinished_SWIGUpcast")]
    public static extern IntPtr MergeAssetsToFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CompleteConsumeByExchangeAssetsToFinished_SWIGUpcast")]
    public static extern IntPtr CompleteConsumeByExchangeAssetsToFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_SWIGUpcast")]
    public static extern IntPtr ExchangeAssetsFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_SWIGUpcast")]
    public static extern IntPtr ExchangeAssetsToFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserResult_SWIGUpcast")]
    public static extern IntPtr CreateBrowserResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ReloadBrowserResult_SWIGUpcast")]
    public static extern IntPtr ReloadBrowserResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CloseBrowserResult_SWIGUpcast")]
    public static extern IntPtr CloseBrowserResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_JavascriptEventResult_SWIGUpcast")]
    public static extern IntPtr JavascriptEventResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_SWIGUpcast")]
    public static extern IntPtr BrowserNeedsPaintRequest_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_SWIGUpcast")]
    public static extern IntPtr BrowserDamageRectNeedsPaintRequest_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderNavigateResult_SWIGUpcast")]
    public static extern IntPtr BrowserRenderNavigateResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderStateChanged_SWIGUpcast")]
    public static extern IntPtr BrowserRenderStateChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderTitleChanged_SWIGUpcast")]
    public static extern IntPtr BrowserRenderTitleChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_SWIGUpcast")]
    public static extern IntPtr BrowserTryNavigateNewPageRequest_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStart_SWIGUpcast")]
    public static extern IntPtr DlcInstallStart_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStartResult_SWIGUpcast")]
    public static extern IntPtr DlcInstallStartResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallProgress_SWIGUpcast")]
    public static extern IntPtr DlcInstallProgress_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcInstallFinished_SWIGUpcast")]
    public static extern IntPtr DlcInstallFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcUninstallFinished_SWIGUpcast")]
    public static extern IntPtr DlcUninstallFinished_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CheckAllDlcsStateReadyResult_SWIGUpcast")]
    public static extern IntPtr CheckAllDlcsStateReadyResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_QueryIsOwnedDlcsResult_SWIGUpcast")]
    public static extern IntPtr QueryIsOwnedDlcsResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcOwnershipChanged_SWIGUpcast")]
    public static extern IntPtr DlcOwnershipChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_DlcRefundChanged_SWIGUpcast")]
    public static extern IntPtr DlcRefundChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_SWIGUpcast")]
    public static extern IntPtr ShowFloatingWindowResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyFloatingWindowResult_SWIGUpcast")]
    public static extern IntPtr ShowNotifyFloatingWindowResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncAcquireGameServerSessionTicketResponse_SWIGUpcast")]
    public static extern IntPtr AsyncAcquireGameServerSessionTicketResponse_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerStartSessionWithPlayerResponse_SWIGUpcast")]
    public static extern IntPtr GameServerStartSessionWithPlayerResponse_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerResult_SWIGUpcast")]
    public static extern IntPtr CreateGameServerResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SetGameServerMetadataResult_SWIGUpcast")]
    public static extern IntPtr SetGameServerMetadataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerMetadataResult_SWIGUpcast")]
    public static extern IntPtr GetGameServerMetadataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GameServerRegisterToServerListResult_SWIGUpcast")]
    public static extern IntPtr GameServerRegisterToServerListResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerPlayerListResult_SWIGUpcast")]
    public static extern IntPtr GetGameServerPlayerListResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_SWIGUpcast")]
    public static extern IntPtr GetGameServerListResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncGetFavoriteGameServersResult_SWIGUpcast")]
    public static extern IntPtr AsyncGetFavoriteGameServersResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncAddFavoriteGameServerResult_SWIGUpcast")]
    public static extern IntPtr AsyncAddFavoriteGameServerResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveFavoriteGameServerResult_SWIGUpcast")]
    public static extern IntPtr AsyncRemoveFavoriteGameServerResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsSetMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailFriendsSetMetadataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsClearMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailFriendsClearMetadataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetMetadataResult_SWIGUpcast")]
    public static extern IntPtr RailFriendsGetMetadataResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetInviteCommandLine_SWIGUpcast")]
    public static extern IntPtr RailFriendsGetInviteCommandLine_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsReportPlayedWithUserListResult_SWIGUpcast")]
    public static extern IntPtr RailFriendsReportPlayedWithUserListResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailFriendsBuddyListChanged_SWIGUpcast")]
    public static extern IntPtr RailFriendsBuddyListChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllPurchasableProductsResponse_SWIGUpcast")]
    public static extern IntPtr RailInGamePurchaseRequestAllPurchasableProductsResponse_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllProductsResponse_SWIGUpcast")]
    public static extern IntPtr RailInGamePurchaseRequestAllProductsResponse_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsResponse_SWIGUpcast")]
    public static extern IntPtr RailInGamePurchasePurchaseProductsResponse_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsToAssetsResponse_SWIGUpcast")]
    public static extern IntPtr RailInGamePurchasePurchaseProductsToAssetsResponse_SWIGUpcast(
      IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseFinishOrderResponse_SWIGUpcast")]
    public static extern IntPtr RailInGamePurchaseFinishOrderResponse_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowDisplayed_SWIGUpcast")]
    public static extern IntPtr RailInGameStorePurchasePayWindowDisplayed_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowClosed_SWIGUpcast")]
    public static extern IntPtr RailInGameStorePurchasePayWindowClosed_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchaseResult_SWIGUpcast")]
    public static extern IntPtr RailInGameStorePurchaseResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateSessionRequest_SWIGUpcast")]
    public static extern IntPtr CreateSessionRequest_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateSessionFailed_SWIGUpcast")]
    public static extern IntPtr CreateSessionFailed_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ZoneInfoList_SWIGUpcast")]
    public static extern IntPtr ZoneInfoList_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_SWIGUpcast")]
    public static extern IntPtr RoomInfoList_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomAllData_SWIGUpcast")]
    public static extern IntPtr RoomAllData_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateRoomInfo_SWIGUpcast")]
    public static extern IntPtr CreateRoomInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_SWIGUpcast")]
    public static extern IntPtr RoomMembersInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_JoinRoomInfo_SWIGUpcast")]
    public static extern IntPtr JoinRoomInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_KickOffMemberInfo_SWIGUpcast")]
    public static extern IntPtr KickOffMemberInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SetRoomMetadataInfo_SWIGUpcast")]
    public static extern IntPtr SetRoomMetadataInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetRoomMetadataInfo_SWIGUpcast")]
    public static extern IntPtr GetRoomMetadataInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ClearRoomMetadataInfo_SWIGUpcast")]
    public static extern IntPtr ClearRoomMetadataInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_SWIGUpcast")]
    public static extern IntPtr GetMemberMetadataInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_SetMemberMetadataInfo_SWIGUpcast")]
    public static extern IntPtr SetMemberMetadataInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaveRoomInfo_SWIGUpcast")]
    public static extern IntPtr LeaveRoomInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_UserRoomListInfo_SWIGUpcast")]
    public static extern IntPtr UserRoomListInfo_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyMetadataChange_SWIGUpcast")]
    public static extern IntPtr NotifyMetadataChange_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_SWIGUpcast")]
    public static extern IntPtr NotifyRoomMemberChange_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_SWIGUpcast")]
    public static extern IntPtr NotifyRoomMemberKicked_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomDestroy_SWIGUpcast")]
    public static extern IntPtr NotifyRoomDestroy_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_SWIGUpcast")]
    public static extern IntPtr RoomDataReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_SWIGUpcast")]
    public static extern IntPtr NotifyRoomOwnerChange_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_SWIGUpcast")]
    public static extern IntPtr NotifyRoomGameServerChange_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateChannelResult_SWIGUpcast")]
    public static extern IntPtr CreateChannelResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteJoinChannelRequest_SWIGUpcast")]
    public static extern IntPtr InviteJoinChannelRequest_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_JoinChannelResult_SWIGUpcast")]
    public static extern IntPtr JoinChannelResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelException_SWIGUpcast")]
    public static extern IntPtr ChannelException_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelNetDelay_SWIGUpcast")]
    public static extern IntPtr ChannelNetDelay_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_InviteMemmberResult_SWIGUpcast")]
    public static extern IntPtr InviteMemmberResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ChannelMemberStateChanged_SWIGUpcast")]
    public static extern IntPtr ChannelMemberStateChanged_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncQueryQuotaResult_SWIGUpcast")]
    public static extern IntPtr AsyncQueryQuotaResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_ShareStorageToSpaceWorkResult_SWIGUpcast")]
    public static extern IntPtr ShareStorageToSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_SWIGUpcast")]
    public static extern IntPtr AsyncReadFileResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_SWIGUpcast")]
    public static extern IntPtr AsyncWriteFileResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_SWIGUpcast")]
    public static extern IntPtr AsyncReadStreamFileResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_SWIGUpcast")]
    public static extern IntPtr AsyncWriteStreamFileResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_SWIGUpcast")]
    public static extern IntPtr AsyncListFileResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncRenameStreamFileResult_SWIGUpcast")]
    public static extern IntPtr AsyncRenameStreamFileResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AsyncDeleteStreamFileResult_SWIGUpcast")]
    public static extern IntPtr AsyncDeleteStreamFileResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_AcquireSessionTicketResponse_SWIGUpcast")]
    public static extern IntPtr AcquireSessionTicketResponse_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_StartSessionWithPlayerResponse_SWIGUpcast")]
    public static extern IntPtr StartSessionWithPlayerResponse_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerGetGamePurchaseKeyResult_SWIGUpcast")]
    public static extern IntPtr PlayerGetGamePurchaseKeyResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_CreateVoiceChannelResult_SWIGUpcast")]
    public static extern IntPtr CreateVoiceChannelResult_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_VoiceDataCapturedEvent_SWIGUpcast")]
    public static extern IntPtr VoiceDataCapturedEvent_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_SWIGUpcast")]
    public static extern IntPtr IRailPlayerAchievement_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalAchievement_SWIGUpcast")]
    public static extern IntPtr IRailGlobalAchievement_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementReceived_SWIGUpcast")]
    public static extern IntPtr PlayerAchievementReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_SWIGUpcast")]
    public static extern IntPtr PlayerAchievementStored_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GlobalAchievementReceived_SWIGUpcast")]
    public static extern IntPtr GlobalAchievementReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_SWIGUpcast")]
    public static extern IntPtr IRailAssets_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_SWIGUpcast")]
    public static extern IntPtr IRailBrowser_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_SWIGUpcast")]
    public static extern IntPtr IRailBrowserRender_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SWIGUpcast")]
    public static extern IntPtr IRailGameServer_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_SWIGUpcast")]
    public static extern IntPtr IRailLeaderboard_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_SWIGUpcast")]
    public static extern IntPtr IRailLeaderboardEntries_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardReceived_SWIGUpcast")]
    public static extern IntPtr LeaderboardReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntryReceived_SWIGUpcast")]
    public static extern IntPtr LeaderboardEntryReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_SWIGUpcast")]
    public static extern IntPtr LeaderboardUploaded_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_LeaderboardAttachSpaceWork_SWIGUpcast")]
    public static extern IntPtr LeaderboardAttachSpaceWork_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SWIGUpcast")]
    public static extern IntPtr IRailRoom_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshot_SWIGUpcast")]
    public static extern IntPtr IRailScreenshot_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_SWIGUpcast")]
    public static extern IntPtr IRailPlayerStats_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_SWIGUpcast")]
    public static extern IntPtr IRailGlobalStats_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerStatsReceived_SWIGUpcast")]
    public static extern IntPtr PlayerStatsReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_PlayerStatsStored_SWIGUpcast")]
    public static extern IntPtr PlayerStatsStored_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_NumberOfPlayerReceived_SWIGUpcast")]
    public static extern IntPtr NumberOfPlayerReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_GlobalStatsRequestReceived_SWIGUpcast")]
    public static extern IntPtr GlobalStatsRequestReceived_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailFile_SWIGUpcast")]
    public static extern IntPtr IRailFile_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_SWIGUpcast")]
    public static extern IntPtr IRailStreamFile_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SWIGUpcast")]
    public static extern IntPtr IRailSpaceWork_SWIGUpcast(IntPtr jarg1);

    [DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_SWIGUpcast")]
    public static extern IntPtr IRailVoiceChannel_SWIGUpcast(IntPtr jarg1);

    protected class SWIGExceptionHelper
    {
      private static ExceptionDelegate applicationDelegate = SetPendingApplicationException;
      private static ExceptionDelegate arithmeticDelegate = SetPendingArithmeticException;
      private static ExceptionDelegate divideByZeroDelegate = SetPendingDivideByZeroException;
      private static ExceptionDelegate indexOutOfRangeDelegate = SetPendingIndexOutOfRangeException;
      private static ExceptionDelegate invalidCastDelegate = SetPendingInvalidCastException;
      private static ExceptionDelegate invalidOperationDelegate = SetPendingInvalidOperationException;
      private static ExceptionDelegate ioDelegate = SetPendingIOException;
      private static ExceptionDelegate nullReferenceDelegate = SetPendingNullReferenceException;
      private static ExceptionDelegate outOfMemoryDelegate = SetPendingOutOfMemoryException;
      private static ExceptionDelegate overflowDelegate = SetPendingOverflowException;
      private static ExceptionDelegate systemDelegate = SetPendingSystemException;
      private static ExceptionArgumentDelegate argumentDelegate = SetPendingArgumentException;
      private static ExceptionArgumentDelegate argumentNullDelegate = SetPendingArgumentNullException;
      private static ExceptionArgumentDelegate argumentOutOfRangeDelegate = SetPendingArgumentOutOfRangeException;

      [DllImport("rail_api")]
      public static extern void SWIGRegisterExceptionCallbacks_rail_api(
        ExceptionDelegate applicationDelegate,
        ExceptionDelegate arithmeticDelegate,
        ExceptionDelegate divideByZeroDelegate,
        ExceptionDelegate indexOutOfRangeDelegate,
        ExceptionDelegate invalidCastDelegate,
        ExceptionDelegate invalidOperationDelegate,
        ExceptionDelegate ioDelegate,
        ExceptionDelegate nullReferenceDelegate,
        ExceptionDelegate outOfMemoryDelegate,
        ExceptionDelegate overflowDelegate,
        ExceptionDelegate systemExceptionDelegate);

      [DllImport("rail_api", EntryPoint = "SWIGRegisterExceptionArgumentCallbacks_rail_api")]
      public static extern void SWIGRegisterExceptionCallbacksArgument_rail_api(
        ExceptionArgumentDelegate argumentDelegate,
        ExceptionArgumentDelegate argumentNullDelegate,
        ExceptionArgumentDelegate argumentOutOfRangeDelegate);

      private static void SetPendingApplicationException(string message) => SWIGPendingException.Set(new ApplicationException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingArithmeticException(string message) => SWIGPendingException.Set(new ArithmeticException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingDivideByZeroException(string message) => SWIGPendingException.Set(new DivideByZeroException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingIndexOutOfRangeException(string message) => SWIGPendingException.Set(new IndexOutOfRangeException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingInvalidCastException(string message) => SWIGPendingException.Set(new InvalidCastException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingInvalidOperationException(string message) => SWIGPendingException.Set(new InvalidOperationException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingIOException(string message) => SWIGPendingException.Set(new IOException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingNullReferenceException(string message) => SWIGPendingException.Set(new NullReferenceException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingOutOfMemoryException(string message) => SWIGPendingException.Set(new OutOfMemoryException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingOverflowException(string message) => SWIGPendingException.Set(new OverflowException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingSystemException(string message) => SWIGPendingException.Set(new SystemException(message, SWIGPendingException.Retrieve()));

      private static void SetPendingArgumentException(string message, string paramName) => SWIGPendingException.Set(new ArgumentException(message, paramName, SWIGPendingException.Retrieve()));

      private static void SetPendingArgumentNullException(string message, string paramName)
      {
        Exception exception = SWIGPendingException.Retrieve();
        if (exception != null)
          message = message + " Inner Exception: " + exception.Message;
        SWIGPendingException.Set(new ArgumentNullException(paramName, message));
      }

      private static void SetPendingArgumentOutOfRangeException(string message, string paramName)
      {
        Exception exception = SWIGPendingException.Retrieve();
        if (exception != null)
          message = message + " Inner Exception: " + exception.Message;
        SWIGPendingException.Set((Exception) new ArgumentOutOfRangeException(paramName, message));
      }

      static SWIGExceptionHelper()
      {
        SWIGRegisterExceptionCallbacks_rail_api(applicationDelegate, arithmeticDelegate, divideByZeroDelegate, indexOutOfRangeDelegate, invalidCastDelegate, invalidOperationDelegate, ioDelegate, nullReferenceDelegate, outOfMemoryDelegate, overflowDelegate, systemDelegate);
        SWIGRegisterExceptionCallbacksArgument_rail_api(argumentDelegate, argumentNullDelegate, argumentOutOfRangeDelegate);
      }

      public delegate void ExceptionDelegate(string message);

      public delegate void ExceptionArgumentDelegate(string message, string paramName);
    }

    public class SWIGPendingException
    {
      [ThreadStatic]
      private static Exception pendingException;
      private static int numExceptionsPending;

      public static bool Pending
      {
        get
        {
          bool pending = false;
          if (numExceptionsPending > 0 && pendingException != null)
            pending = true;
          return pending;
        }
      }

      public static void Set(Exception e)
      {
        pendingException = pendingException == null ? e : throw new ApplicationException("FATAL: An earlier pending exception from unmanaged code was missed and thus not thrown (" + pendingException.ToString() + ")", e);
        lock (typeof (RAIL_API_PINVOKE))
          ++numExceptionsPending;
      }

      public static Exception Retrieve()
      {
        Exception exception = (Exception) null;
        if (numExceptionsPending > 0 && pendingException != null)
        {
          exception = pendingException;
          pendingException = (Exception) null;
          lock (typeof (RAIL_API_PINVOKE))
            --numExceptionsPending;
        }
        return exception;
      }
    }

    protected class SWIGStringHelper
    {
      private static SWIGStringDelegate stringDelegate = CreateString;

      [DllImport("rail_api")]
      public static extern void SWIGRegisterStringCallback_rail_api(
        SWIGStringDelegate stringDelegate);

      private static string CreateString(string cString) => cString;

      static SWIGStringHelper() => SWIGRegisterStringCallback_rail_api(stringDelegate);

      public delegate string SWIGStringDelegate(string message);
    }
  }
}
