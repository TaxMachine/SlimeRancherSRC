// Decompiled with JetBrains decompiler
// Type: rail.RailConverter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class RailConverter
  {
    public static void Cpp2Csharp(IntPtr ptr, AcquireSessionTicketResponse ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.AcquireSessionTicketResponse_session_ticket_get(ptr), ret.session_ticket);
    }

    public static void Csharp2Cpp(AcquireSessionTicketResponse data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.session_ticket, RAIL_API_PINVOKE.AcquireSessionTicketResponse_session_ticket_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncAcquireGameServerSessionTicketResponse ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncAcquireGameServerSessionTicketResponse_session_ticket_get(ptr), ret.session_ticket);
    }

    public static void Csharp2Cpp(AsyncAcquireGameServerSessionTicketResponse data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.session_ticket, RAIL_API_PINVOKE.AsyncAcquireGameServerSessionTicketResponse_session_ticket_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncAddFavoriteGameServerResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncAddFavoriteGameServerResult_server_id_get(ptr), ret.server_id);
    }

    public static void Csharp2Cpp(AsyncAddFavoriteGameServerResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.server_id, RAIL_API_PINVOKE.AsyncAddFavoriteGameServerResult_server_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncDeleteStreamFileResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.filename = RAIL_API_PINVOKE.AsyncDeleteStreamFileResult_filename_get(ptr);
    }

    public static void Csharp2Cpp(AsyncDeleteStreamFileResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncDeleteStreamFileResult_filename_set(ptr, data.filename);
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncGetFavoriteGameServersResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncGetFavoriteGameServersResult_server_id_array_get(ptr), ret.server_id_array);
    }

    public static void Csharp2Cpp(AsyncGetFavoriteGameServersResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.server_id_array, RAIL_API_PINVOKE.AsyncGetFavoriteGameServersResult_server_id_array_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncGetMyFavoritesWorksResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.total_available_works = RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_total_available_works_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
    }

    public static void Csharp2Cpp(AsyncGetMyFavoritesWorksResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_total_available_works_set(ptr, data.total_available_works);
      Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_spacework_descriptors_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncGetMySubscribedWorksResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.total_available_works = RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_total_available_works_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
    }

    public static void Csharp2Cpp(AsyncGetMySubscribedWorksResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_total_available_works_set(ptr, data.total_available_works);
      Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_spacework_descriptors_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncListFileResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncListFileResult_file_list_get(ptr), ret.file_list);
      ret.try_list_file_num = RAIL_API_PINVOKE.AsyncListFileResult_try_list_file_num_get(ptr);
      ret.all_file_num = RAIL_API_PINVOKE.AsyncListFileResult_all_file_num_get(ptr);
      ret.start_index = RAIL_API_PINVOKE.AsyncListFileResult_start_index_get(ptr);
    }

    public static void Csharp2Cpp(AsyncListFileResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.file_list, RAIL_API_PINVOKE.AsyncListFileResult_file_list_get(ptr));
      RAIL_API_PINVOKE.AsyncListFileResult_try_list_file_num_set(ptr, data.try_list_file_num);
      RAIL_API_PINVOKE.AsyncListFileResult_all_file_num_set(ptr, data.all_file_num);
      RAIL_API_PINVOKE.AsyncListFileResult_start_index_set(ptr, data.start_index);
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncModifyFavoritesWorksResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_success_ids_get(ptr), ret.success_ids);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_failure_ids_get(ptr), ret.failure_ids);
      ret.modify_flag = (EnumRailModifyFavoritesSpaceWorkType) RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_modify_flag_get(ptr);
    }

    public static void Csharp2Cpp(AsyncModifyFavoritesWorksResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.success_ids, RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_success_ids_get(ptr));
      Csharp2Cpp(data.failure_ids, RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_failure_ids_get(ptr));
      RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_modify_flag_set(ptr, (int) data.modify_flag);
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncQueryQuotaResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.available_quota = RAIL_API_PINVOKE.AsyncQueryQuotaResult_available_quota_get(ptr);
      ret.total_quota = RAIL_API_PINVOKE.AsyncQueryQuotaResult_total_quota_get(ptr);
    }

    public static void Csharp2Cpp(AsyncQueryQuotaResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncQueryQuotaResult_available_quota_set(ptr, data.available_quota);
      RAIL_API_PINVOKE.AsyncQueryQuotaResult_total_quota_set(ptr, data.total_quota);
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncQuerySpaceWorksResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.total_available_works = RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_total_available_works_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
    }

    public static void Csharp2Cpp(AsyncQuerySpaceWorksResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_total_available_works_set(ptr, data.total_available_works);
      Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_spacework_descriptors_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncReadFileResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.try_read_length = RAIL_API_PINVOKE.AsyncReadFileResult_try_read_length_get(ptr);
      ret.offset = RAIL_API_PINVOKE.AsyncReadFileResult_offset_get(ptr);
      ret.data = RAIL_API_PINVOKE.AsyncReadFileResult_data_get(ptr);
      ret.filename = RAIL_API_PINVOKE.AsyncReadFileResult_filename_get(ptr);
    }

    public static void Csharp2Cpp(AsyncReadFileResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncReadFileResult_try_read_length_set(ptr, data.try_read_length);
      RAIL_API_PINVOKE.AsyncReadFileResult_offset_set(ptr, data.offset);
      RAIL_API_PINVOKE.AsyncReadFileResult_data_set(ptr, data.data);
      RAIL_API_PINVOKE.AsyncReadFileResult_filename_set(ptr, data.filename);
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncReadStreamFileResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.try_read_length = RAIL_API_PINVOKE.AsyncReadStreamFileResult_try_read_length_get(ptr);
      ret.offset = RAIL_API_PINVOKE.AsyncReadStreamFileResult_offset_get(ptr);
      ret.data = RAIL_API_PINVOKE.AsyncReadStreamFileResult_data_get(ptr);
      ret.filename = RAIL_API_PINVOKE.AsyncReadStreamFileResult_filename_get(ptr);
    }

    public static void Csharp2Cpp(AsyncReadStreamFileResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncReadStreamFileResult_try_read_length_set(ptr, data.try_read_length);
      RAIL_API_PINVOKE.AsyncReadStreamFileResult_offset_set(ptr, data.offset);
      RAIL_API_PINVOKE.AsyncReadStreamFileResult_data_set(ptr, data.data);
      RAIL_API_PINVOKE.AsyncReadStreamFileResult_filename_set(ptr, data.filename);
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncRemoveFavoriteGameServerResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncRemoveFavoriteGameServerResult_server_id_get(ptr), ret.server_id);
    }

    public static void Csharp2Cpp(AsyncRemoveFavoriteGameServerResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.server_id, RAIL_API_PINVOKE.AsyncRemoveFavoriteGameServerResult_server_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncRemoveSpaceWorkResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncRemoveSpaceWorkResult_id_get(ptr), ret.id);
    }

    public static void Csharp2Cpp(AsyncRemoveSpaceWorkResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.id, RAIL_API_PINVOKE.AsyncRemoveSpaceWorkResult_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncRenameStreamFileResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.old_filename = RAIL_API_PINVOKE.AsyncRenameStreamFileResult_old_filename_get(ptr);
      ret.new_filename = RAIL_API_PINVOKE.AsyncRenameStreamFileResult_new_filename_get(ptr);
    }

    public static void Csharp2Cpp(AsyncRenameStreamFileResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncRenameStreamFileResult_old_filename_set(ptr, data.old_filename);
      RAIL_API_PINVOKE.AsyncRenameStreamFileResult_new_filename_set(ptr, data.new_filename);
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncSearchSpaceWorksResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.total_available_works = RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_total_available_works_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
    }

    public static void Csharp2Cpp(AsyncSearchSpaceWorksResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_total_available_works_set(ptr, data.total_available_works);
      Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_spacework_descriptors_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncSubscribeSpaceWorksResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_success_ids_get(ptr), ret.success_ids);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_failure_ids_get(ptr), ret.failure_ids);
      ret.subscribe = RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_subscribe_get(ptr);
    }

    public static void Csharp2Cpp(AsyncSubscribeSpaceWorksResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.success_ids, RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_success_ids_get(ptr));
      Csharp2Cpp(data.failure_ids, RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_failure_ids_get(ptr));
      RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_subscribe_set(ptr, data.subscribe);
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncUpdateMetadataResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.type = (EnumRailSpaceWorkType) RAIL_API_PINVOKE.AsyncUpdateMetadataResult_type_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncUpdateMetadataResult_id_get(ptr), ret.id);
    }

    public static void Csharp2Cpp(AsyncUpdateMetadataResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncUpdateMetadataResult_type_set(ptr, (int) data.type);
      Csharp2Cpp(data.id, RAIL_API_PINVOKE.AsyncUpdateMetadataResult_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncVoteSpaceWorkResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.AsyncVoteSpaceWorkResult_id_get(ptr), ret.id);
    }

    public static void Csharp2Cpp(AsyncVoteSpaceWorkResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.id, RAIL_API_PINVOKE.AsyncVoteSpaceWorkResult_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncWriteFileResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.written_length = RAIL_API_PINVOKE.AsyncWriteFileResult_written_length_get(ptr);
      ret.offset = RAIL_API_PINVOKE.AsyncWriteFileResult_offset_get(ptr);
      ret.try_write_length = RAIL_API_PINVOKE.AsyncWriteFileResult_try_write_length_get(ptr);
      ret.filename = RAIL_API_PINVOKE.AsyncWriteFileResult_filename_get(ptr);
    }

    public static void Csharp2Cpp(AsyncWriteFileResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncWriteFileResult_written_length_set(ptr, data.written_length);
      RAIL_API_PINVOKE.AsyncWriteFileResult_offset_set(ptr, data.offset);
      RAIL_API_PINVOKE.AsyncWriteFileResult_try_write_length_set(ptr, data.try_write_length);
      RAIL_API_PINVOKE.AsyncWriteFileResult_filename_set(ptr, data.filename);
    }

    public static void Cpp2Csharp(IntPtr ptr, AsyncWriteStreamFileResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.written_length = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_written_length_get(ptr);
      ret.offset = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_offset_get(ptr);
      ret.try_write_length = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_try_write_length_get(ptr);
      ret.filename = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_filename_get(ptr);
    }

    public static void Csharp2Cpp(AsyncWriteStreamFileResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.AsyncWriteStreamFileResult_written_length_set(ptr, data.written_length);
      RAIL_API_PINVOKE.AsyncWriteStreamFileResult_offset_set(ptr, data.offset);
      RAIL_API_PINVOKE.AsyncWriteStreamFileResult_try_write_length_set(ptr, data.try_write_length);
      RAIL_API_PINVOKE.AsyncWriteStreamFileResult_filename_set(ptr, data.filename);
    }

    public static void Cpp2Csharp(IntPtr ptr, BrowserDamageRectNeedsPaintRequest ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.update_bgra_height = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_height_get(ptr);
      ret.scroll_x_pos = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_x_pos_get(ptr);
      ret.bgra_data = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_data_get(ptr);
      ret.update_bgra_width = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_width_get(ptr);
      ret.page_scale_factor = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_page_scale_factor_get(ptr);
      ret.update_offset_y = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_y_get(ptr);
      ret.update_offset_x = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_x_get(ptr);
      ret.offset_x = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_x_get(ptr);
      ret.offset_y = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_y_get(ptr);
      ret.bgra_height = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_height_get(ptr);
      ret.scroll_y_pos = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_y_pos_get(ptr);
      ret.bgra_width = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_width_get(ptr);
    }

    public static void Csharp2Cpp(BrowserDamageRectNeedsPaintRequest data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_height_set(ptr, data.update_bgra_height);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_x_pos_set(ptr, data.scroll_x_pos);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_data_set(ptr, data.bgra_data);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_width_set(ptr, data.update_bgra_width);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_page_scale_factor_set(ptr, data.page_scale_factor);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_y_set(ptr, data.update_offset_y);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_x_set(ptr, data.update_offset_x);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_x_set(ptr, data.offset_x);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_y_set(ptr, data.offset_y);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_height_set(ptr, data.bgra_height);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_y_pos_set(ptr, data.scroll_y_pos);
      RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_width_set(ptr, data.bgra_width);
    }

    public static void Cpp2Csharp(IntPtr ptr, BrowserNeedsPaintRequest ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.bgra_width = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_width_get(ptr);
      ret.scroll_y_pos = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_y_pos_get(ptr);
      ret.bgra_data = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_data_get(ptr);
      ret.page_scale_factor = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_page_scale_factor_get(ptr);
      ret.offset_x = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_x_get(ptr);
      ret.scroll_x_pos = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_x_pos_get(ptr);
      ret.bgra_height = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_height_get(ptr);
      ret.offset_y = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_y_get(ptr);
    }

    public static void Csharp2Cpp(BrowserNeedsPaintRequest data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_width_set(ptr, data.bgra_width);
      RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_y_pos_set(ptr, data.scroll_y_pos);
      RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_data_set(ptr, data.bgra_data);
      RAIL_API_PINVOKE.BrowserNeedsPaintRequest_page_scale_factor_set(ptr, data.page_scale_factor);
      RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_x_set(ptr, data.offset_x);
      RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_x_pos_set(ptr, data.scroll_x_pos);
      RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_height_set(ptr, data.bgra_height);
      RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_y_set(ptr, data.offset_y);
    }

    public static void Cpp2Csharp(IntPtr ptr, BrowserRenderNavigateResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.url = RAIL_API_PINVOKE.BrowserRenderNavigateResult_url_get(ptr);
    }

    public static void Csharp2Cpp(BrowserRenderNavigateResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.BrowserRenderNavigateResult_url_set(ptr, data.url);
    }

    public static void Cpp2Csharp(IntPtr ptr, BrowserRenderStateChanged ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.can_go_back = RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_back_get(ptr);
      ret.can_go_forward = RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_forward_get(ptr);
    }

    public static void Csharp2Cpp(BrowserRenderStateChanged data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_back_set(ptr, data.can_go_back);
      RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_forward_set(ptr, data.can_go_forward);
    }

    public static void Cpp2Csharp(IntPtr ptr, BrowserRenderTitleChanged ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.new_title = RAIL_API_PINVOKE.BrowserRenderTitleChanged_new_title_get(ptr);
    }

    public static void Csharp2Cpp(BrowserRenderTitleChanged data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.BrowserRenderTitleChanged_new_title_set(ptr, data.new_title);
    }

    public static void Cpp2Csharp(IntPtr ptr, BrowserTryNavigateNewPageRequest ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.url = RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_url_get(ptr);
      ret.target_type = RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_target_type_get(ptr);
      ret.is_redirect_request = RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_is_redirect_request_get(ptr);
    }

    public static void Csharp2Cpp(BrowserTryNavigateNewPageRequest data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_url_set(ptr, data.url);
      RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_target_type_set(ptr, data.target_type);
      RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_is_redirect_request_set(ptr, data.is_redirect_request);
    }

    public static void Cpp2Csharp(IntPtr ptr, ChannelException ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.exception_type = (ChannelExceptionType) RAIL_API_PINVOKE.ChannelException_exception_type_get(ptr);
      ret.channel_id = RAIL_API_PINVOKE.ChannelException_channel_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.ChannelException_local_peer_get(ptr), ret.local_peer);
    }

    public static void Csharp2Cpp(ChannelException data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.ChannelException_exception_type_set(ptr, (int) data.exception_type);
      RAIL_API_PINVOKE.ChannelException_channel_id_set(ptr, data.channel_id);
      Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.ChannelException_local_peer_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, ChannelMemberStateChanged ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.channel_id = RAIL_API_PINVOKE.ChannelMemberStateChanged_channel_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.ChannelMemberStateChanged_local_peer_get(ptr), ret.local_peer);
      ret.member_state = (RailChannelMemberState) RAIL_API_PINVOKE.ChannelMemberStateChanged_member_state_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.ChannelMemberStateChanged_remote_peer_get(ptr), ret.remote_peer);
    }

    public static void Csharp2Cpp(ChannelMemberStateChanged data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.ChannelMemberStateChanged_channel_id_set(ptr, data.channel_id);
      Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.ChannelMemberStateChanged_local_peer_get(ptr));
      RAIL_API_PINVOKE.ChannelMemberStateChanged_member_state_set(ptr, (int) data.member_state);
      Csharp2Cpp(data.remote_peer, RAIL_API_PINVOKE.ChannelMemberStateChanged_remote_peer_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, ChannelNetDelay ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.channel_id = RAIL_API_PINVOKE.ChannelNetDelay_channel_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.ChannelNetDelay_local_peer_get(ptr), ret.local_peer);
      ret.net_delay_ms = RAIL_API_PINVOKE.ChannelNetDelay_net_delay_ms_get(ptr);
    }

    public static void Csharp2Cpp(ChannelNetDelay data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.ChannelNetDelay_channel_id_set(ptr, data.channel_id);
      Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.ChannelNetDelay_local_peer_get(ptr));
      RAIL_API_PINVOKE.ChannelNetDelay_net_delay_ms_set(ptr, data.net_delay_ms);
    }

    public static void Cpp2Csharp(IntPtr ptr, CheckAllDlcsStateReadyResult ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(CheckAllDlcsStateReadyResult data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, ClearRoomMetadataInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.room_id = RAIL_API_PINVOKE.ClearRoomMetadataInfo_room_id_get(ptr);
    }

    public static void Csharp2Cpp(ClearRoomMetadataInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.ClearRoomMetadataInfo_room_id_set(ptr, data.room_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, CloseBrowserResult ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(CloseBrowserResult data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, CompleteConsumeAssetsFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.CompleteConsumeAssetsFinished_asset_item_get(ptr), ret.asset_item);
    }

    public static void Csharp2Cpp(CompleteConsumeAssetsFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.asset_item, RAIL_API_PINVOKE.CompleteConsumeAssetsFinished_asset_item_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, CompleteConsumeByExchangeAssetsToFinished ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(CompleteConsumeByExchangeAssetsToFinished data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, CreateBrowserOptions ret)
    {
      ret.has_minimum_button = RAIL_API_PINVOKE.CreateBrowserOptions_has_minimum_button_get(ptr);
      ret.has_border = RAIL_API_PINVOKE.CreateBrowserOptions_has_border_get(ptr);
      ret.is_movable = RAIL_API_PINVOKE.CreateBrowserOptions_is_movable_get(ptr);
      ret.has_maximum_button = RAIL_API_PINVOKE.CreateBrowserOptions_has_maximum_button_get(ptr);
      ret.margin_left = RAIL_API_PINVOKE.CreateBrowserOptions_margin_left_get(ptr);
      ret.margin_top = RAIL_API_PINVOKE.CreateBrowserOptions_margin_top_get(ptr);
    }

    public static void Csharp2Cpp(CreateBrowserOptions data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.CreateBrowserOptions_has_minimum_button_set(ptr, data.has_minimum_button);
      RAIL_API_PINVOKE.CreateBrowserOptions_has_border_set(ptr, data.has_border);
      RAIL_API_PINVOKE.CreateBrowserOptions_is_movable_set(ptr, data.is_movable);
      RAIL_API_PINVOKE.CreateBrowserOptions_has_maximum_button_set(ptr, data.has_maximum_button);
      RAIL_API_PINVOKE.CreateBrowserOptions_margin_left_set(ptr, data.margin_left);
      RAIL_API_PINVOKE.CreateBrowserOptions_margin_top_set(ptr, data.margin_top);
    }

    public static void Cpp2Csharp(IntPtr ptr, CreateBrowserResult ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(CreateBrowserResult data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, CreateChannelResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.channel_id = RAIL_API_PINVOKE.CreateChannelResult_channel_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.CreateChannelResult_local_peer_get(ptr), ret.local_peer);
    }

    public static void Csharp2Cpp(CreateChannelResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.CreateChannelResult_channel_id_set(ptr, data.channel_id);
      Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.CreateChannelResult_local_peer_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, CreateCustomerDrawBrowserOptions ret)
    {
      ret.content_offset_x = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_x_get(ptr);
      ret.content_offset_y = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_y_get(ptr);
      ret.has_scroll = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_has_scroll_get(ptr);
      ret.cotent_window_height = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_cotent_window_height_get(ptr);
      ret.content_window_width = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_window_width_get(ptr);
    }

    public static void Csharp2Cpp(CreateCustomerDrawBrowserOptions data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_x_set(ptr, data.content_offset_x);
      RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_y_set(ptr, data.content_offset_y);
      RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_has_scroll_set(ptr, data.has_scroll);
      RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_cotent_window_height_set(ptr, data.cotent_window_height);
      RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_window_width_set(ptr, data.content_window_width);
    }

    public static void Cpp2Csharp(IntPtr ptr, CreateGameServerOptions ret)
    {
      ret.has_password = RAIL_API_PINVOKE.CreateGameServerOptions_has_password_get(ptr);
      ret.enable_team_voice = RAIL_API_PINVOKE.CreateGameServerOptions_enable_team_voice_get(ptr);
    }

    public static void Csharp2Cpp(CreateGameServerOptions data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.CreateGameServerOptions_has_password_set(ptr, data.has_password);
      RAIL_API_PINVOKE.CreateGameServerOptions_enable_team_voice_set(ptr, data.enable_team_voice);
    }

    public static void Cpp2Csharp(IntPtr ptr, CreateGameServerResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.CreateGameServerResult_game_server_id_get(ptr), ret.game_server_id);
    }

    public static void Csharp2Cpp(CreateGameServerResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.CreateGameServerResult_game_server_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, CreateRoomInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.room_id = RAIL_API_PINVOKE.CreateRoomInfo_room_id_get(ptr);
      ret.zone_id = RAIL_API_PINVOKE.CreateRoomInfo_zone_id_get(ptr);
    }

    public static void Csharp2Cpp(CreateRoomInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.CreateRoomInfo_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.CreateRoomInfo_zone_id_set(ptr, data.zone_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, CreateSessionFailed ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionFailed_local_peer_get(ptr), ret.local_peer);
    }

    public static void Csharp2Cpp(CreateSessionFailed data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.CreateSessionFailed_local_peer_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, CreateSessionRequest ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionRequest_local_peer_get(ptr), ret.local_peer);
      Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionRequest_remote_peer_get(ptr), ret.remote_peer);
    }

    public static void Csharp2Cpp(CreateSessionRequest data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.CreateSessionRequest_local_peer_get(ptr));
      Csharp2Cpp(data.remote_peer, RAIL_API_PINVOKE.CreateSessionRequest_remote_peer_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, CreateVoiceChannelOption ret) => ret.speaker_state = (EnumRailVoiceChannelSpeakerState) RAIL_API_PINVOKE.CreateVoiceChannelOption_speaker_state_get(ptr);

    public static void Csharp2Cpp(CreateVoiceChannelOption data, IntPtr ptr) => RAIL_API_PINVOKE.CreateVoiceChannelOption_speaker_state_set(ptr, (int) data.speaker_state);

    public static void Cpp2Csharp(IntPtr ptr, CreateVoiceChannelResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.CreateVoiceChannelResult_voice_channel_id_get(ptr), ret.voice_channel_id);
    }

    public static void Csharp2Cpp(CreateVoiceChannelResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.CreateVoiceChannelResult_voice_channel_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, DirectConsumeAssetsFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.DirectConsumeAssetsFinished_assets_get(ptr), ret.assets);
    }

    public static void Csharp2Cpp(DirectConsumeAssetsFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.assets, RAIL_API_PINVOKE.DirectConsumeAssetsFinished_assets_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, DiscountInfo ret)
    {
      ret.type = (PurchaseProductDiscountType) RAIL_API_PINVOKE.DiscountInfo_type_get(ptr);
      ret.start_time = RAIL_API_PINVOKE.DiscountInfo_start_time_get(ptr);
      ret.off = RAIL_API_PINVOKE.DiscountInfo_off_get(ptr);
      ret.discount_price = RAIL_API_PINVOKE.DiscountInfo_discount_price_get(ptr);
      ret.end_time = RAIL_API_PINVOKE.DiscountInfo_end_time_get(ptr);
    }

    public static void Csharp2Cpp(DiscountInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.DiscountInfo_type_set(ptr, (int) data.type);
      RAIL_API_PINVOKE.DiscountInfo_start_time_set(ptr, data.start_time);
      RAIL_API_PINVOKE.DiscountInfo_off_set(ptr, data.off);
      RAIL_API_PINVOKE.DiscountInfo_discount_price_set(ptr, data.discount_price);
      RAIL_API_PINVOKE.DiscountInfo_end_time_set(ptr, data.end_time);
    }

    public static void Cpp2Csharp(IntPtr ptr, DlcInstallFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallFinished_dlc_id_get(ptr), ret.dlc_id);
      ret.result = (RailResult) RAIL_API_PINVOKE.DlcInstallFinished_result_get(ptr);
    }

    public static void Csharp2Cpp(DlcInstallFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallFinished_dlc_id_get(ptr));
      RAIL_API_PINVOKE.DlcInstallFinished_result_set(ptr, (int) data.result);
    }

    public static void Cpp2Csharp(IntPtr ptr, DlcInstallProgress ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallProgress_progress_get(ptr), ret.progress);
      Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallProgress_dlc_id_get(ptr), ret.dlc_id);
    }

    public static void Csharp2Cpp(DlcInstallProgress data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.progress, RAIL_API_PINVOKE.DlcInstallProgress_progress_get(ptr));
      Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallProgress_dlc_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, DlcInstallStart ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallStart_dlc_id_get(ptr), ret.dlc_id);
    }

    public static void Csharp2Cpp(DlcInstallStart data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallStart_dlc_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, DlcInstallStartResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallStartResult_dlc_id_get(ptr), ret.dlc_id);
      ret.result = (RailResult) RAIL_API_PINVOKE.DlcInstallStartResult_result_get(ptr);
    }

    public static void Csharp2Cpp(DlcInstallStartResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallStartResult_dlc_id_get(ptr));
      RAIL_API_PINVOKE.DlcInstallStartResult_result_set(ptr, (int) data.result);
    }

    public static void Cpp2Csharp(IntPtr ptr, DlcOwnershipChanged ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.DlcOwnershipChanged_dlc_id_get(ptr), ret.dlc_id);
      ret.is_active = RAIL_API_PINVOKE.DlcOwnershipChanged_is_active_get(ptr);
    }

    public static void Csharp2Cpp(DlcOwnershipChanged data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcOwnershipChanged_dlc_id_get(ptr));
      RAIL_API_PINVOKE.DlcOwnershipChanged_is_active_set(ptr, data.is_active);
    }

    public static void Cpp2Csharp(IntPtr ptr, DlcRefundChanged ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.DlcRefundChanged_dlc_id_get(ptr), ret.dlc_id);
      ret.refund_state = (EnumRailGameRefundState) RAIL_API_PINVOKE.DlcRefundChanged_refund_state_get(ptr);
    }

    public static void Csharp2Cpp(DlcRefundChanged data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcRefundChanged_dlc_id_get(ptr));
      RAIL_API_PINVOKE.DlcRefundChanged_refund_state_set(ptr, (int) data.refund_state);
    }

    public static void Cpp2Csharp(IntPtr ptr, DlcUninstallFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.DlcUninstallFinished_dlc_id_get(ptr), ret.dlc_id);
      ret.result = (RailResult) RAIL_API_PINVOKE.DlcUninstallFinished_result_get(ptr);
    }

    public static void Csharp2Cpp(DlcUninstallFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcUninstallFinished_dlc_id_get(ptr));
      RAIL_API_PINVOKE.DlcUninstallFinished_result_set(ptr, (int) data.result);
    }

    public static void Cpp2Csharp(IntPtr ptr, EventBase ret)
    {
      ret.result = (RailResult) RAIL_API_PINVOKE.EventBase_result_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.EventBase_game_id_get(ptr), ret.game_id);
      ret.user_data = RAIL_API_PINVOKE.EventBase_user_data_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.EventBase_rail_id_get(ptr), ret.rail_id);
    }

    public static void Csharp2Cpp(EventBase data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.EventBase_result_set(ptr, (int) data.result);
      Csharp2Cpp(data.game_id, RAIL_API_PINVOKE.EventBase_game_id_get(ptr));
      RAIL_API_PINVOKE.EventBase_user_data_set(ptr, data.user_data);
      Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.EventBase_rail_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, ExchangeAssetsFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsFinished_to_product_info_get(ptr), ret.to_product_info);
      Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsFinished_old_assets_get(ptr), ret.old_assets);
      ret.new_asset_id = RAIL_API_PINVOKE.ExchangeAssetsFinished_new_asset_id_get(ptr);
    }

    public static void Csharp2Cpp(ExchangeAssetsFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.to_product_info, RAIL_API_PINVOKE.ExchangeAssetsFinished_to_product_info_get(ptr));
      Csharp2Cpp(data.old_assets, RAIL_API_PINVOKE.ExchangeAssetsFinished_old_assets_get(ptr));
      RAIL_API_PINVOKE.ExchangeAssetsFinished_new_asset_id_set(ptr, data.new_asset_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, ExchangeAssetsToFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.exchange_to_asset_id = RAIL_API_PINVOKE.ExchangeAssetsToFinished_exchange_to_asset_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsToFinished_to_product_info_get(ptr), ret.to_product_info);
      Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsToFinished_old_assets_get(ptr), ret.old_assets);
    }

    public static void Csharp2Cpp(ExchangeAssetsToFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.ExchangeAssetsToFinished_exchange_to_asset_id_set(ptr, data.exchange_to_asset_id);
      Csharp2Cpp(data.to_product_info, RAIL_API_PINVOKE.ExchangeAssetsToFinished_to_product_info_get(ptr));
      Csharp2Cpp(data.old_assets, RAIL_API_PINVOKE.ExchangeAssetsToFinished_old_assets_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, GameServerInfo ret)
    {
      Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_server_kvs_get(ptr), ret.server_kvs);
      Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_owner_rail_id_get(ptr), ret.owner_rail_id);
      ret.game_server_name = RAIL_API_PINVOKE.GameServerInfo_game_server_name_get(ptr);
      ret.server_host = RAIL_API_PINVOKE.GameServerInfo_server_host_get(ptr);
      ret.is_dedicated = RAIL_API_PINVOKE.GameServerInfo_is_dedicated_get(ptr);
      ret.server_info = RAIL_API_PINVOKE.GameServerInfo_server_info_get(ptr);
      ret.server_tags = RAIL_API_PINVOKE.GameServerInfo_server_tags_get(ptr);
      ret.spectator_host = RAIL_API_PINVOKE.GameServerInfo_spectator_host_get(ptr);
      ret.server_description = RAIL_API_PINVOKE.GameServerInfo_server_description_get(ptr);
      ret.channel_id = RAIL_API_PINVOKE.GameServerInfo_channel_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_game_server_rail_id_get(ptr), ret.game_server_rail_id);
      ret.has_password = RAIL_API_PINVOKE.GameServerInfo_has_password_get(ptr);
      ret.server_version = RAIL_API_PINVOKE.GameServerInfo_server_version_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_server_mods_get(ptr), ret.server_mods);
      ret.bot_players = RAIL_API_PINVOKE.GameServerInfo_bot_players_get(ptr);
      ret.game_server_map = RAIL_API_PINVOKE.GameServerInfo_game_server_map_get(ptr);
      ret.max_players = RAIL_API_PINVOKE.GameServerInfo_max_players_get(ptr);
      ret.current_players = RAIL_API_PINVOKE.GameServerInfo_current_players_get(ptr);
      ret.server_fullname = RAIL_API_PINVOKE.GameServerInfo_server_fullname_get(ptr);
      ret.is_friend_only = RAIL_API_PINVOKE.GameServerInfo_is_friend_only_get(ptr);
      ret.zone_id = RAIL_API_PINVOKE.GameServerInfo_zone_id_get(ptr);
    }

    public static void Csharp2Cpp(GameServerInfo data, IntPtr ptr)
    {
      Csharp2Cpp(data.server_kvs, RAIL_API_PINVOKE.GameServerInfo_server_kvs_get(ptr));
      Csharp2Cpp(data.owner_rail_id, RAIL_API_PINVOKE.GameServerInfo_owner_rail_id_get(ptr));
      RAIL_API_PINVOKE.GameServerInfo_game_server_name_set(ptr, data.game_server_name);
      RAIL_API_PINVOKE.GameServerInfo_server_host_set(ptr, data.server_host);
      RAIL_API_PINVOKE.GameServerInfo_is_dedicated_set(ptr, data.is_dedicated);
      RAIL_API_PINVOKE.GameServerInfo_server_info_set(ptr, data.server_info);
      RAIL_API_PINVOKE.GameServerInfo_server_tags_set(ptr, data.server_tags);
      RAIL_API_PINVOKE.GameServerInfo_spectator_host_set(ptr, data.spectator_host);
      RAIL_API_PINVOKE.GameServerInfo_server_description_set(ptr, data.server_description);
      RAIL_API_PINVOKE.GameServerInfo_channel_id_set(ptr, data.channel_id);
      Csharp2Cpp(data.game_server_rail_id, RAIL_API_PINVOKE.GameServerInfo_game_server_rail_id_get(ptr));
      RAIL_API_PINVOKE.GameServerInfo_has_password_set(ptr, data.has_password);
      RAIL_API_PINVOKE.GameServerInfo_server_version_set(ptr, data.server_version);
      Csharp2Cpp(data.server_mods, RAIL_API_PINVOKE.GameServerInfo_server_mods_get(ptr));
      RAIL_API_PINVOKE.GameServerInfo_bot_players_set(ptr, data.bot_players);
      RAIL_API_PINVOKE.GameServerInfo_game_server_map_set(ptr, data.game_server_map);
      RAIL_API_PINVOKE.GameServerInfo_max_players_set(ptr, data.max_players);
      RAIL_API_PINVOKE.GameServerInfo_current_players_set(ptr, data.current_players);
      RAIL_API_PINVOKE.GameServerInfo_server_fullname_set(ptr, data.server_fullname);
      RAIL_API_PINVOKE.GameServerInfo_is_friend_only_set(ptr, data.is_friend_only);
      RAIL_API_PINVOKE.GameServerInfo_zone_id_set(ptr, data.zone_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, GameServerListFilter ret)
    {
      ret.tags_not_contained = RAIL_API_PINVOKE.GameServerListFilter_tags_not_contained_get(ptr);
      ret.filter_game_server_name = RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_name_get(ptr);
      ret.filter_delicated_server = (EnumRailOptionalValue) RAIL_API_PINVOKE.GameServerListFilter_filter_delicated_server_get(ptr);
      ret.filter_zone_id = RAIL_API_PINVOKE.GameServerListFilter_filter_zone_id_get(ptr);
      ret.tags_contained = RAIL_API_PINVOKE.GameServerListFilter_tags_contained_get(ptr);
      ret.filter_password = (EnumRailOptionalValue) RAIL_API_PINVOKE.GameServerListFilter_filter_password_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.GameServerListFilter_filters_get(ptr), ret.filters);
      ret.filter_game_server_map = RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_map_get(ptr);
      ret.filter_friends_created = (EnumRailOptionalValue) RAIL_API_PINVOKE.GameServerListFilter_filter_friends_created_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.GameServerListFilter_owner_id_get(ptr), ret.owner_id);
    }

    public static void Csharp2Cpp(GameServerListFilter data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.GameServerListFilter_tags_not_contained_set(ptr, data.tags_not_contained);
      RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_name_set(ptr, data.filter_game_server_name);
      RAIL_API_PINVOKE.GameServerListFilter_filter_delicated_server_set(ptr, (int) data.filter_delicated_server);
      RAIL_API_PINVOKE.GameServerListFilter_filter_zone_id_set(ptr, data.filter_zone_id);
      RAIL_API_PINVOKE.GameServerListFilter_tags_contained_set(ptr, data.tags_contained);
      RAIL_API_PINVOKE.GameServerListFilter_filter_password_set(ptr, (int) data.filter_password);
      Csharp2Cpp(data.filters, RAIL_API_PINVOKE.GameServerListFilter_filters_get(ptr));
      RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_map_set(ptr, data.filter_game_server_map);
      RAIL_API_PINVOKE.GameServerListFilter_filter_friends_created_set(ptr, (int) data.filter_friends_created);
      Csharp2Cpp(data.owner_id, RAIL_API_PINVOKE.GameServerListFilter_owner_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, GameServerListFilterKey ret)
    {
      ret.filter_value = RAIL_API_PINVOKE.GameServerListFilterKey_filter_value_get(ptr);
      ret.key_name = RAIL_API_PINVOKE.GameServerListFilterKey_key_name_get(ptr);
      ret.value_type = (EnumRailPropertyValueType) RAIL_API_PINVOKE.GameServerListFilterKey_value_type_get(ptr);
      ret.comparison_type = (EnumRailComparisonType) RAIL_API_PINVOKE.GameServerListFilterKey_comparison_type_get(ptr);
    }

    public static void Csharp2Cpp(GameServerListFilterKey data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.GameServerListFilterKey_filter_value_set(ptr, data.filter_value);
      RAIL_API_PINVOKE.GameServerListFilterKey_key_name_set(ptr, data.key_name);
      RAIL_API_PINVOKE.GameServerListFilterKey_value_type_set(ptr, (int) data.value_type);
      RAIL_API_PINVOKE.GameServerListFilterKey_comparison_type_set(ptr, (int) data.comparison_type);
    }

    public static void Cpp2Csharp(IntPtr ptr, GameServerListSorter ret)
    {
      ret.sort_key = RAIL_API_PINVOKE.GameServerListSorter_sort_key_get(ptr);
      ret.sort_type = (EnumRailSortType) RAIL_API_PINVOKE.GameServerListSorter_sort_type_get(ptr);
      ret.sorter_key_type = (GameServerListSorterKeyType) RAIL_API_PINVOKE.GameServerListSorter_sorter_key_type_get(ptr);
      ret.sort_value_type = (EnumRailPropertyValueType) RAIL_API_PINVOKE.GameServerListSorter_sort_value_type_get(ptr);
    }

    public static void Csharp2Cpp(GameServerListSorter data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.GameServerListSorter_sort_key_set(ptr, data.sort_key);
      RAIL_API_PINVOKE.GameServerListSorter_sort_type_set(ptr, (int) data.sort_type);
      RAIL_API_PINVOKE.GameServerListSorter_sorter_key_type_set(ptr, (int) data.sorter_key_type);
      RAIL_API_PINVOKE.GameServerListSorter_sort_value_type_set(ptr, (int) data.sort_value_type);
    }

    public static void Cpp2Csharp(IntPtr ptr, GameServerPlayerInfo ret)
    {
      ret.member_nickname = RAIL_API_PINVOKE.GameServerPlayerInfo_member_nickname_get(ptr);
      ret.member_score = RAIL_API_PINVOKE.GameServerPlayerInfo_member_score_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.GameServerPlayerInfo_member_id_get(ptr), ret.member_id);
    }

    public static void Csharp2Cpp(GameServerPlayerInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.GameServerPlayerInfo_member_nickname_set(ptr, data.member_nickname);
      RAIL_API_PINVOKE.GameServerPlayerInfo_member_score_set(ptr, data.member_score);
      Csharp2Cpp(data.member_id, RAIL_API_PINVOKE.GameServerPlayerInfo_member_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, GameServerRegisterToServerListResult ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(GameServerRegisterToServerListResult data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, GameServerStartSessionWithPlayerResponse ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.GameServerStartSessionWithPlayerResponse_remote_rail_id_get(ptr), ret.remote_rail_id);
    }

    public static void Csharp2Cpp(GameServerStartSessionWithPlayerResponse data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.remote_rail_id, RAIL_API_PINVOKE.GameServerStartSessionWithPlayerResponse_remote_rail_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, GetGameServerListResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerListResult_server_info_get(ptr), ret.server_info);
      ret.total_num = RAIL_API_PINVOKE.GetGameServerListResult_total_num_get(ptr);
      ret.start_index = RAIL_API_PINVOKE.GetGameServerListResult_start_index_get(ptr);
      ret.end_index = RAIL_API_PINVOKE.GetGameServerListResult_end_index_get(ptr);
    }

    public static void Csharp2Cpp(GetGameServerListResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.server_info, RAIL_API_PINVOKE.GetGameServerListResult_server_info_get(ptr));
      RAIL_API_PINVOKE.GetGameServerListResult_total_num_set(ptr, data.total_num);
      RAIL_API_PINVOKE.GetGameServerListResult_start_index_set(ptr, data.start_index);
      RAIL_API_PINVOKE.GetGameServerListResult_end_index_set(ptr, data.end_index);
    }

    public static void Cpp2Csharp(IntPtr ptr, GetGameServerMetadataResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerMetadataResult_game_server_id_get(ptr), ret.game_server_id);
      Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerMetadataResult_key_value_get(ptr), ret.key_value);
    }

    public static void Csharp2Cpp(GetGameServerMetadataResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.GetGameServerMetadataResult_game_server_id_get(ptr));
      Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.GetGameServerMetadataResult_key_value_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, GetGameServerPlayerListResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerPlayerListResult_game_server_id_get(ptr), ret.game_server_id);
      Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerPlayerListResult_server_player_info_get(ptr), ret.server_player_info);
    }

    public static void Csharp2Cpp(GetGameServerPlayerListResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.GetGameServerPlayerListResult_game_server_id_get(ptr));
      Csharp2Cpp(data.server_player_info, RAIL_API_PINVOKE.GetGameServerPlayerListResult_server_player_info_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, GetMemberMetadataInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.GetMemberMetadataInfo_key_value_get(ptr), ret.key_value);
      ret.room_id = RAIL_API_PINVOKE.GetMemberMetadataInfo_room_id_get(ptr);
      ret.member_id = RAIL_API_PINVOKE.GetMemberMetadataInfo_member_id_get(ptr);
    }

    public static void Csharp2Cpp(GetMemberMetadataInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.GetMemberMetadataInfo_key_value_get(ptr));
      RAIL_API_PINVOKE.GetMemberMetadataInfo_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.GetMemberMetadataInfo_member_id_set(ptr, data.member_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, GetRoomMetadataInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.GetRoomMetadataInfo_key_value_get(ptr), ret.key_value);
      ret.room_id = RAIL_API_PINVOKE.GetRoomMetadataInfo_room_id_get(ptr);
    }

    public static void Csharp2Cpp(GetRoomMetadataInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.GetRoomMetadataInfo_key_value_get(ptr));
      RAIL_API_PINVOKE.GetRoomMetadataInfo_room_id_set(ptr, data.room_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, GlobalAchievementReceived ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.count = RAIL_API_PINVOKE.GlobalAchievementReceived_count_get(ptr);
    }

    public static void Csharp2Cpp(GlobalAchievementReceived data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.GlobalAchievementReceived_count_set(ptr, data.count);
    }

    public static void Cpp2Csharp(IntPtr ptr, GlobalStatsRequestReceived ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(GlobalStatsRequestReceived data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, InviteJoinChannelRequest ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.channel_id = RAIL_API_PINVOKE.InviteJoinChannelRequest_channel_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.InviteJoinChannelRequest_local_peer_get(ptr), ret.local_peer);
      Cpp2Csharp(RAIL_API_PINVOKE.InviteJoinChannelRequest_remote_peer_get(ptr), ret.remote_peer);
    }

    public static void Csharp2Cpp(InviteJoinChannelRequest data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.InviteJoinChannelRequest_channel_id_set(ptr, data.channel_id);
      Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.InviteJoinChannelRequest_local_peer_get(ptr));
      Csharp2Cpp(data.remote_peer, RAIL_API_PINVOKE.InviteJoinChannelRequest_remote_peer_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, InviteMemmberResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.channel_id = RAIL_API_PINVOKE.InviteMemmberResult_channel_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.InviteMemmberResult_local_peer_get(ptr), ret.local_peer);
      Cpp2Csharp(RAIL_API_PINVOKE.InviteMemmberResult_remote_peer_get(ptr), ret.remote_peer);
    }

    public static void Csharp2Cpp(InviteMemmberResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.InviteMemmberResult_channel_id_set(ptr, data.channel_id);
      Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.InviteMemmberResult_local_peer_get(ptr));
      Csharp2Cpp(data.remote_peer, RAIL_API_PINVOKE.InviteMemmberResult_remote_peer_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, JavascriptEventResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.event_name = RAIL_API_PINVOKE.JavascriptEventResult_event_name_get(ptr);
      ret.event_value = RAIL_API_PINVOKE.JavascriptEventResult_event_value_get(ptr);
    }

    public static void Csharp2Cpp(JavascriptEventResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.JavascriptEventResult_event_name_set(ptr, data.event_name);
      RAIL_API_PINVOKE.JavascriptEventResult_event_value_set(ptr, data.event_value);
    }

    public static void Cpp2Csharp(IntPtr ptr, JoinChannelResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.channel_id = RAIL_API_PINVOKE.JoinChannelResult_channel_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.JoinChannelResult_local_peer_get(ptr), ret.local_peer);
    }

    public static void Csharp2Cpp(JoinChannelResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.JoinChannelResult_channel_id_set(ptr, data.channel_id);
      Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.JoinChannelResult_local_peer_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, JoinRoomInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.room_id = RAIL_API_PINVOKE.JoinRoomInfo_room_id_get(ptr);
      ret.zone_id = RAIL_API_PINVOKE.JoinRoomInfo_zone_id_get(ptr);
    }

    public static void Csharp2Cpp(JoinRoomInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.JoinRoomInfo_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.JoinRoomInfo_zone_id_set(ptr, data.zone_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, KickOffMemberInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.room_id = RAIL_API_PINVOKE.KickOffMemberInfo_room_id_get(ptr);
      ret.kicked_id = RAIL_API_PINVOKE.KickOffMemberInfo_kicked_id_get(ptr);
    }

    public static void Csharp2Cpp(KickOffMemberInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.KickOffMemberInfo_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.KickOffMemberInfo_kicked_id_set(ptr, data.kicked_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, LeaderboardAttachSpaceWork ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.leaderboard_name = RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_leaderboard_name_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_spacework_id_get(ptr), ret.spacework_id);
    }

    public static void Csharp2Cpp(LeaderboardAttachSpaceWork data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_leaderboard_name_set(ptr, data.leaderboard_name);
      Csharp2Cpp(data.spacework_id, RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_spacework_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, LeaderboardData ret)
    {
      ret.additional_infomation = RAIL_API_PINVOKE.LeaderboardData_additional_infomation_get(ptr);
      ret.score = RAIL_API_PINVOKE.LeaderboardData_score_get(ptr);
      ret.rank = RAIL_API_PINVOKE.LeaderboardData_rank_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardData_spacework_id_get(ptr), ret.spacework_id);
    }

    public static void Csharp2Cpp(LeaderboardData data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.LeaderboardData_additional_infomation_set(ptr, data.additional_infomation);
      RAIL_API_PINVOKE.LeaderboardData_score_set(ptr, data.score);
      RAIL_API_PINVOKE.LeaderboardData_rank_set(ptr, data.rank);
      Csharp2Cpp(data.spacework_id, RAIL_API_PINVOKE.LeaderboardData_spacework_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, LeaderboardEntry ret)
    {
      Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardEntry_player_id_get(ptr), ret.player_id);
      Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardEntry_data_get(ptr), ret.data);
    }

    public static void Csharp2Cpp(LeaderboardEntry data, IntPtr ptr)
    {
      Csharp2Cpp(data.player_id, RAIL_API_PINVOKE.LeaderboardEntry_player_id_get(ptr));
      Csharp2Cpp(data.data, RAIL_API_PINVOKE.LeaderboardEntry_data_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, LeaderboardEntryReceived ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.leaderboard_name = RAIL_API_PINVOKE.LeaderboardEntryReceived_leaderboard_name_get(ptr);
    }

    public static void Csharp2Cpp(LeaderboardEntryReceived data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.LeaderboardEntryReceived_leaderboard_name_set(ptr, data.leaderboard_name);
    }

    public static void Cpp2Csharp(IntPtr ptr, LeaderboardParameters ret) => ret.param = RAIL_API_PINVOKE.LeaderboardParameters_param_get(ptr);

    public static void Csharp2Cpp(LeaderboardParameters data, IntPtr ptr) => RAIL_API_PINVOKE.LeaderboardParameters_param_set(ptr, data.param);

    public static void Cpp2Csharp(IntPtr ptr, LeaderboardReceived ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.leaderboard_name = RAIL_API_PINVOKE.LeaderboardReceived_leaderboard_name_get(ptr);
      ret.does_exist = RAIL_API_PINVOKE.LeaderboardReceived_does_exist_get(ptr);
    }

    public static void Csharp2Cpp(LeaderboardReceived data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.LeaderboardReceived_leaderboard_name_set(ptr, data.leaderboard_name);
      RAIL_API_PINVOKE.LeaderboardReceived_does_exist_set(ptr, data.does_exist);
    }

    public static void Cpp2Csharp(IntPtr ptr, LeaderboardUploaded ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.old_rank = RAIL_API_PINVOKE.LeaderboardUploaded_old_rank_get(ptr);
      ret.leaderboard_name = RAIL_API_PINVOKE.LeaderboardUploaded_leaderboard_name_get(ptr);
      ret.score = RAIL_API_PINVOKE.LeaderboardUploaded_score_get(ptr);
      ret.better_score = RAIL_API_PINVOKE.LeaderboardUploaded_better_score_get(ptr);
      ret.new_rank = RAIL_API_PINVOKE.LeaderboardUploaded_new_rank_get(ptr);
    }

    public static void Csharp2Cpp(LeaderboardUploaded data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.LeaderboardUploaded_old_rank_set(ptr, data.old_rank);
      RAIL_API_PINVOKE.LeaderboardUploaded_leaderboard_name_set(ptr, data.leaderboard_name);
      RAIL_API_PINVOKE.LeaderboardUploaded_score_set(ptr, data.score);
      RAIL_API_PINVOKE.LeaderboardUploaded_better_score_set(ptr, data.better_score);
      RAIL_API_PINVOKE.LeaderboardUploaded_new_rank_set(ptr, data.new_rank);
    }

    public static void Cpp2Csharp(IntPtr ptr, LeaveRoomInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.reason = (EnumLeaveRoomReason) RAIL_API_PINVOKE.LeaveRoomInfo_reason_get(ptr);
      ret.room_id = RAIL_API_PINVOKE.LeaveRoomInfo_room_id_get(ptr);
    }

    public static void Csharp2Cpp(LeaveRoomInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.LeaveRoomInfo_reason_set(ptr, (int) data.reason);
      RAIL_API_PINVOKE.LeaveRoomInfo_room_id_set(ptr, data.room_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, MemberInfo ret)
    {
      ret.member_name = RAIL_API_PINVOKE.MemberInfo_member_name_get(ptr);
      ret.member_index = RAIL_API_PINVOKE.MemberInfo_member_index_get(ptr);
      ret.room_id = RAIL_API_PINVOKE.MemberInfo_room_id_get(ptr);
      ret.member_id = RAIL_API_PINVOKE.MemberInfo_member_id_get(ptr);
    }

    public static void Csharp2Cpp(MemberInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.MemberInfo_member_name_set(ptr, data.member_name);
      RAIL_API_PINVOKE.MemberInfo_member_index_set(ptr, data.member_index);
      RAIL_API_PINVOKE.MemberInfo_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.MemberInfo_member_id_set(ptr, data.member_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, MergeAssetsFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.MergeAssetsFinished_source_assets_get(ptr), ret.source_assets);
      ret.new_asset_id = RAIL_API_PINVOKE.MergeAssetsFinished_new_asset_id_get(ptr);
    }

    public static void Csharp2Cpp(MergeAssetsFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.source_assets, RAIL_API_PINVOKE.MergeAssetsFinished_source_assets_get(ptr));
      RAIL_API_PINVOKE.MergeAssetsFinished_new_asset_id_set(ptr, data.new_asset_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, MergeAssetsToFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.merge_to_asset_id = RAIL_API_PINVOKE.MergeAssetsToFinished_merge_to_asset_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.MergeAssetsToFinished_source_assets_get(ptr), ret.source_assets);
    }

    public static void Csharp2Cpp(MergeAssetsToFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.MergeAssetsToFinished_merge_to_asset_id_set(ptr, data.merge_to_asset_id);
      Csharp2Cpp(data.source_assets, RAIL_API_PINVOKE.MergeAssetsToFinished_source_assets_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, NotifyMetadataChange ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.changer_id = RAIL_API_PINVOKE.NotifyMetadataChange_changer_id_get(ptr);
      ret.room_id = RAIL_API_PINVOKE.NotifyMetadataChange_room_id_get(ptr);
    }

    public static void Csharp2Cpp(NotifyMetadataChange data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.NotifyMetadataChange_changer_id_set(ptr, data.changer_id);
      RAIL_API_PINVOKE.NotifyMetadataChange_room_id_set(ptr, data.room_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, NotifyRoomDestroy ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.room_id = RAIL_API_PINVOKE.NotifyRoomDestroy_room_id_get(ptr);
    }

    public static void Csharp2Cpp(NotifyRoomDestroy data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.NotifyRoomDestroy_room_id_set(ptr, data.room_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, NotifyRoomGameServerChange ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.game_server_rail_id = RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_rail_id_get(ptr);
      ret.room_id = RAIL_API_PINVOKE.NotifyRoomGameServerChange_room_id_get(ptr);
      ret.game_server_channel_id = RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_channel_id_get(ptr);
    }

    public static void Csharp2Cpp(NotifyRoomGameServerChange data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_rail_id_set(ptr, data.game_server_rail_id);
      RAIL_API_PINVOKE.NotifyRoomGameServerChange_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_channel_id_set(ptr, data.game_server_channel_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, NotifyRoomMemberChange ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.changer_id = RAIL_API_PINVOKE.NotifyRoomMemberChange_changer_id_get(ptr);
      ret.id_for_making_change = RAIL_API_PINVOKE.NotifyRoomMemberChange_id_for_making_change_get(ptr);
      ret.state_change = (EnumRoomMemberActionStatus) RAIL_API_PINVOKE.NotifyRoomMemberChange_state_change_get(ptr);
      ret.room_id = RAIL_API_PINVOKE.NotifyRoomMemberChange_room_id_get(ptr);
    }

    public static void Csharp2Cpp(NotifyRoomMemberChange data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.NotifyRoomMemberChange_changer_id_set(ptr, data.changer_id);
      RAIL_API_PINVOKE.NotifyRoomMemberChange_id_for_making_change_set(ptr, data.id_for_making_change);
      RAIL_API_PINVOKE.NotifyRoomMemberChange_state_change_set(ptr, (int) data.state_change);
      RAIL_API_PINVOKE.NotifyRoomMemberChange_room_id_set(ptr, data.room_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, NotifyRoomMemberKicked ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.id_for_making_kick = RAIL_API_PINVOKE.NotifyRoomMemberKicked_id_for_making_kick_get(ptr);
      ret.due_to_kicker_lost_connect = RAIL_API_PINVOKE.NotifyRoomMemberKicked_due_to_kicker_lost_connect_get(ptr);
      ret.room_id = RAIL_API_PINVOKE.NotifyRoomMemberKicked_room_id_get(ptr);
      ret.kicked_id = RAIL_API_PINVOKE.NotifyRoomMemberKicked_kicked_id_get(ptr);
    }

    public static void Csharp2Cpp(NotifyRoomMemberKicked data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.NotifyRoomMemberKicked_id_for_making_kick_set(ptr, data.id_for_making_kick);
      RAIL_API_PINVOKE.NotifyRoomMemberKicked_due_to_kicker_lost_connect_set(ptr, data.due_to_kicker_lost_connect);
      RAIL_API_PINVOKE.NotifyRoomMemberKicked_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.NotifyRoomMemberKicked_kicked_id_set(ptr, data.kicked_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, NotifyRoomOwnerChange ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.old_owner_id = RAIL_API_PINVOKE.NotifyRoomOwnerChange_old_owner_id_get(ptr);
      ret.reason = (EnumRoomOwnerChangeReason) RAIL_API_PINVOKE.NotifyRoomOwnerChange_reason_get(ptr);
      ret.room_id = RAIL_API_PINVOKE.NotifyRoomOwnerChange_room_id_get(ptr);
      ret.new_owner_id = RAIL_API_PINVOKE.NotifyRoomOwnerChange_new_owner_id_get(ptr);
    }

    public static void Csharp2Cpp(NotifyRoomOwnerChange data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.NotifyRoomOwnerChange_old_owner_id_set(ptr, data.old_owner_id);
      RAIL_API_PINVOKE.NotifyRoomOwnerChange_reason_set(ptr, (int) data.reason);
      RAIL_API_PINVOKE.NotifyRoomOwnerChange_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.NotifyRoomOwnerChange_new_owner_id_set(ptr, data.new_owner_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, NumberOfPlayerReceived ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.online_number = RAIL_API_PINVOKE.NumberOfPlayerReceived_online_number_get(ptr);
      ret.offline_number = RAIL_API_PINVOKE.NumberOfPlayerReceived_offline_number_get(ptr);
    }

    public static void Csharp2Cpp(NumberOfPlayerReceived data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.NumberOfPlayerReceived_online_number_set(ptr, data.online_number);
      RAIL_API_PINVOKE.NumberOfPlayerReceived_offline_number_set(ptr, data.offline_number);
    }

    public static void Cpp2Csharp(IntPtr ptr, PlayerAchievementReceived ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(PlayerAchievementReceived data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, PlayerAchievementStored ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.group_achievement = RAIL_API_PINVOKE.PlayerAchievementStored_group_achievement_get(ptr);
      ret.achievement_name = RAIL_API_PINVOKE.PlayerAchievementStored_achievement_name_get(ptr);
      ret.current_progress = RAIL_API_PINVOKE.PlayerAchievementStored_current_progress_get(ptr);
      ret.max_progress = RAIL_API_PINVOKE.PlayerAchievementStored_max_progress_get(ptr);
    }

    public static void Csharp2Cpp(PlayerAchievementStored data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.PlayerAchievementStored_group_achievement_set(ptr, data.group_achievement);
      RAIL_API_PINVOKE.PlayerAchievementStored_achievement_name_set(ptr, data.achievement_name);
      RAIL_API_PINVOKE.PlayerAchievementStored_current_progress_set(ptr, data.current_progress);
      RAIL_API_PINVOKE.PlayerAchievementStored_max_progress_set(ptr, data.max_progress);
    }

    public static void Cpp2Csharp(IntPtr ptr, PlayerGetGamePurchaseKeyResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.purchase_key = RAIL_API_PINVOKE.PlayerGetGamePurchaseKeyResult_purchase_key_get(ptr);
    }

    public static void Csharp2Cpp(PlayerGetGamePurchaseKeyResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.PlayerGetGamePurchaseKeyResult_purchase_key_set(ptr, data.purchase_key);
    }

    public static void Cpp2Csharp(IntPtr ptr, PlayerPersonalInfo ret)
    {
      ret.state = (EnumRailPlayerOnLineState) RAIL_API_PINVOKE.PlayerPersonalInfo_state_get(ptr);
      ret.avatar_url = RAIL_API_PINVOKE.PlayerPersonalInfo_avatar_url_get(ptr);
      ret.rail_level = RAIL_API_PINVOKE.PlayerPersonalInfo_rail_level_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.PlayerPersonalInfo_rail_id_get(ptr), ret.rail_id);
      ret.rail_name = RAIL_API_PINVOKE.PlayerPersonalInfo_rail_name_get(ptr);
      ret.err_code = (RailResult) RAIL_API_PINVOKE.PlayerPersonalInfo_err_code_get(ptr);
    }

    public static void Csharp2Cpp(PlayerPersonalInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.PlayerPersonalInfo_state_set(ptr, (int) data.state);
      RAIL_API_PINVOKE.PlayerPersonalInfo_avatar_url_set(ptr, data.avatar_url);
      RAIL_API_PINVOKE.PlayerPersonalInfo_rail_level_set(ptr, data.rail_level);
      Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.PlayerPersonalInfo_rail_id_get(ptr));
      RAIL_API_PINVOKE.PlayerPersonalInfo_rail_name_set(ptr, data.rail_name);
      RAIL_API_PINVOKE.PlayerPersonalInfo_err_code_set(ptr, (int) data.err_code);
    }

    public static void Cpp2Csharp(IntPtr ptr, PlayerStatsReceived ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(PlayerStatsReceived data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, PlayerStatsStored ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(PlayerStatsStored data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, PublishScreenshotResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.PublishScreenshotResult_work_id_get(ptr), ret.work_id);
    }

    public static void Csharp2Cpp(PublishScreenshotResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.work_id, RAIL_API_PINVOKE.PublishScreenshotResult_work_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, QueryIsOwnedDlcsResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.QueryIsOwnedDlcsResult_dlc_owned_list_get(ptr), ret.dlc_owned_list);
    }

    public static void Csharp2Cpp(QueryIsOwnedDlcsResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.dlc_owned_list, RAIL_API_PINVOKE.QueryIsOwnedDlcsResult_dlc_owned_list_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, QueryMySubscribedSpaceWorksResult ret)
    {
      ret.total_available_works = RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_total_available_works_get(ptr);
      ret.spacework_type = (EnumRailSpaceWorkType) RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_type_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
    }

    public static void Csharp2Cpp(QueryMySubscribedSpaceWorksResult data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_total_available_works_set(ptr, data.total_available_works);
      RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_type_set(ptr, (int) data.spacework_type);
      Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_descriptors_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, QuerySubscribeWishPlayStateResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.is_subscribed = RAIL_API_PINVOKE.QuerySubscribeWishPlayStateResult_is_subscribed_get(ptr);
    }

    public static void Csharp2Cpp(QuerySubscribeWishPlayStateResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.QuerySubscribeWishPlayStateResult_is_subscribed_set(ptr, data.is_subscribed);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailAssetInfo ret)
    {
      ret.asset_id = RAIL_API_PINVOKE.RailAssetInfo_asset_id_get(ptr);
      ret.origin = RAIL_API_PINVOKE.RailAssetInfo_origin_get(ptr);
      ret.product_id = RAIL_API_PINVOKE.RailAssetInfo_product_id_get(ptr);
      ret.flag = RAIL_API_PINVOKE.RailAssetInfo_flag_get(ptr);
      ret.state = RAIL_API_PINVOKE.RailAssetInfo_state_get(ptr);
      ret.progress = RAIL_API_PINVOKE.RailAssetInfo_progress_get(ptr);
      ret.position = RAIL_API_PINVOKE.RailAssetInfo_position_get(ptr);
      ret.product_name = RAIL_API_PINVOKE.RailAssetInfo_product_name_get(ptr);
      ret.quantity = RAIL_API_PINVOKE.RailAssetInfo_quantity_get(ptr);
    }

    public static void Csharp2Cpp(RailAssetInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailAssetInfo_asset_id_set(ptr, data.asset_id);
      RAIL_API_PINVOKE.RailAssetInfo_origin_set(ptr, data.origin);
      RAIL_API_PINVOKE.RailAssetInfo_product_id_set(ptr, data.product_id);
      RAIL_API_PINVOKE.RailAssetInfo_flag_set(ptr, data.flag);
      RAIL_API_PINVOKE.RailAssetInfo_state_set(ptr, data.state);
      RAIL_API_PINVOKE.RailAssetInfo_progress_set(ptr, data.progress);
      RAIL_API_PINVOKE.RailAssetInfo_position_set(ptr, data.position);
      RAIL_API_PINVOKE.RailAssetInfo_product_name_set(ptr, data.product_name);
      RAIL_API_PINVOKE.RailAssetInfo_quantity_set(ptr, data.quantity);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailAssetItem ret)
    {
      ret.asset_id = RAIL_API_PINVOKE.RailAssetItem_asset_id_get(ptr);
      ret.quantity = RAIL_API_PINVOKE.RailAssetItem_quantity_get(ptr);
    }

    public static void Csharp2Cpp(RailAssetItem data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailAssetItem_asset_id_set(ptr, data.asset_id);
      RAIL_API_PINVOKE.RailAssetItem_quantity_set(ptr, data.quantity);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailAssetProperty ret)
    {
      ret.asset_id = RAIL_API_PINVOKE.RailAssetProperty_asset_id_get(ptr);
      ret.position = RAIL_API_PINVOKE.RailAssetProperty_position_get(ptr);
    }

    public static void Csharp2Cpp(RailAssetProperty data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailAssetProperty_asset_id_set(ptr, data.asset_id);
      RAIL_API_PINVOKE.RailAssetProperty_position_set(ptr, data.position);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailBranchInfo ret)
    {
      ret.branch_name = RAIL_API_PINVOKE.RailBranchInfo_branch_name_get(ptr);
      ret.build_number = RAIL_API_PINVOKE.RailBranchInfo_build_number_get(ptr);
      ret.branch_type = RAIL_API_PINVOKE.RailBranchInfo_branch_type_get(ptr);
      ret.branch_id = RAIL_API_PINVOKE.RailBranchInfo_branch_id_get(ptr);
    }

    public static void Csharp2Cpp(RailBranchInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailBranchInfo_branch_name_set(ptr, data.branch_name);
      RAIL_API_PINVOKE.RailBranchInfo_build_number_set(ptr, data.build_number);
      RAIL_API_PINVOKE.RailBranchInfo_branch_type_set(ptr, data.branch_type);
      RAIL_API_PINVOKE.RailBranchInfo_branch_id_set(ptr, data.branch_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailCrashInfo ret) => ret.exception_type = (RailUtilsCrashType) RAIL_API_PINVOKE.RailCrashInfo_exception_type_get(ptr);

    public static void Csharp2Cpp(RailCrashInfo data, IntPtr ptr) => RAIL_API_PINVOKE.RailCrashInfo_exception_type_set(ptr, (int) data.exception_type);

    public static void Cpp2Csharp(IntPtr ptr, RailDirtyWordsCheckResult ret)
    {
      ret.dirty_type = (EnumRailDirtyWordsType) RAIL_API_PINVOKE.RailDirtyWordsCheckResult_dirty_type_get(ptr);
      ret.replace_string = RAIL_API_PINVOKE.RailDirtyWordsCheckResult_replace_string_get(ptr);
    }

    public static void Csharp2Cpp(RailDirtyWordsCheckResult data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailDirtyWordsCheckResult_dirty_type_set(ptr, (int) data.dirty_type);
      RAIL_API_PINVOKE.RailDirtyWordsCheckResult_replace_string_set(ptr, data.replace_string);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailDlcID ret) => ret.id_ = RAIL_API_PINVOKE.RailDlcID_id__get(ptr);

    public static void Csharp2Cpp(RailDlcID data, IntPtr ptr) => RAIL_API_PINVOKE.RailDlcID_id__set(ptr, data.id_);

    public static void Cpp2Csharp(IntPtr ptr, RailDlcInfo ret)
    {
      ret.original_price = RAIL_API_PINVOKE.RailDlcInfo_original_price_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailDlcInfo_dlc_id_get(ptr), ret.dlc_id);
      ret.description = RAIL_API_PINVOKE.RailDlcInfo_description_get(ptr);
      ret.discount_price = RAIL_API_PINVOKE.RailDlcInfo_discount_price_get(ptr);
      ret.version = RAIL_API_PINVOKE.RailDlcInfo_version_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailDlcInfo_game_id_get(ptr), ret.game_id);
      ret.name = RAIL_API_PINVOKE.RailDlcInfo_name_get(ptr);
    }

    public static void Csharp2Cpp(RailDlcInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailDlcInfo_original_price_set(ptr, data.original_price);
      Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.RailDlcInfo_dlc_id_get(ptr));
      RAIL_API_PINVOKE.RailDlcInfo_description_set(ptr, data.description);
      RAIL_API_PINVOKE.RailDlcInfo_discount_price_set(ptr, data.discount_price);
      RAIL_API_PINVOKE.RailDlcInfo_version_set(ptr, data.version);
      Csharp2Cpp(data.game_id, RAIL_API_PINVOKE.RailDlcInfo_game_id_get(ptr));
      RAIL_API_PINVOKE.RailDlcInfo_name_set(ptr, data.name);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailDlcInstallProgress ret)
    {
      ret.progress = RAIL_API_PINVOKE.RailDlcInstallProgress_progress_get(ptr);
      ret.finished_bytes = RAIL_API_PINVOKE.RailDlcInstallProgress_finished_bytes_get(ptr);
      ret.total_bytes = RAIL_API_PINVOKE.RailDlcInstallProgress_total_bytes_get(ptr);
      ret.speed = RAIL_API_PINVOKE.RailDlcInstallProgress_speed_get(ptr);
    }

    public static void Csharp2Cpp(RailDlcInstallProgress data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailDlcInstallProgress_progress_set(ptr, data.progress);
      RAIL_API_PINVOKE.RailDlcInstallProgress_finished_bytes_set(ptr, data.finished_bytes);
      RAIL_API_PINVOKE.RailDlcInstallProgress_total_bytes_set(ptr, data.total_bytes);
      RAIL_API_PINVOKE.RailDlcInstallProgress_speed_set(ptr, data.speed);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailDlcOwned ret)
    {
      ret.is_owned = RAIL_API_PINVOKE.RailDlcOwned_is_owned_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailDlcOwned_dlc_id_get(ptr), ret.dlc_id);
    }

    public static void Csharp2Cpp(RailDlcOwned data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailDlcOwned_is_owned_set(ptr, data.is_owned);
      Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.RailDlcOwned_dlc_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailFinalize ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(RailFinalize data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, RailFriendsBuddyListChanged ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(RailFriendsBuddyListChanged data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, RailFriendsClearMetadataResult ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(RailFriendsClearMetadataResult data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, RailFriendsGetInviteCommandLine ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_friend_id_get(ptr), ret.friend_id);
      ret.invite_command_line = RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_invite_command_line_get(ptr);
    }

    public static void Csharp2Cpp(RailFriendsGetInviteCommandLine data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.friend_id, RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_friend_id_get(ptr));
      RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_invite_command_line_set(ptr, data.invite_command_line);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailFriendsGetMetadataResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_id_get(ptr), ret.friend_id);
      Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_kvs_get(ptr), ret.friend_kvs);
    }

    public static void Csharp2Cpp(RailFriendsGetMetadataResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.friend_id, RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_id_get(ptr));
      Csharp2Cpp(data.friend_kvs, RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_kvs_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailFriendsReportPlayedWithUserListResult ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(RailFriendsReportPlayedWithUserListResult data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, RailFriendsSetMetadataResult ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(RailFriendsSetMetadataResult data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, RailGameID ret) => ret.id_ = RAIL_API_PINVOKE.RailGameID_id__get(ptr);

    public static void Csharp2Cpp(RailGameID data, IntPtr ptr) => RAIL_API_PINVOKE.RailGameID_id__set(ptr, data.id_);

    public static void Cpp2Csharp(IntPtr ptr, RailGetImageDataResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.image_data = RAIL_API_PINVOKE.RailGetImageDataResult_image_data_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailGetImageDataResult_image_data_descriptor_get(ptr), ret.image_data_descriptor);
    }

    public static void Csharp2Cpp(RailGetImageDataResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailGetImageDataResult_image_data_set(ptr, data.image_data);
      Csharp2Cpp(data.image_data_descriptor, RAIL_API_PINVOKE.RailGetImageDataResult_image_data_descriptor_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailID ret) => ret.id_ = RAIL_API_PINVOKE.RailID_id__get(ptr);

    public static void Csharp2Cpp(RailID data, IntPtr ptr) => RAIL_API_PINVOKE.RailID_id__set(ptr, data.id_);

    public static void Cpp2Csharp(IntPtr ptr, RailImageDataDescriptor ret)
    {
      ret.pixel_format = (EnumRailImagePixelFormat) RAIL_API_PINVOKE.RailImageDataDescriptor_pixel_format_get(ptr);
      ret.image_height = RAIL_API_PINVOKE.RailImageDataDescriptor_image_height_get(ptr);
      ret.stride_in_bytes = RAIL_API_PINVOKE.RailImageDataDescriptor_stride_in_bytes_get(ptr);
      ret.image_width = RAIL_API_PINVOKE.RailImageDataDescriptor_image_width_get(ptr);
      ret.bits_per_pixel = RAIL_API_PINVOKE.RailImageDataDescriptor_bits_per_pixel_get(ptr);
    }

    public static void Csharp2Cpp(RailImageDataDescriptor data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailImageDataDescriptor_pixel_format_set(ptr, (int) data.pixel_format);
      RAIL_API_PINVOKE.RailImageDataDescriptor_image_height_set(ptr, data.image_height);
      RAIL_API_PINVOKE.RailImageDataDescriptor_stride_in_bytes_set(ptr, data.stride_in_bytes);
      RAIL_API_PINVOKE.RailImageDataDescriptor_image_width_set(ptr, data.image_width);
      RAIL_API_PINVOKE.RailImageDataDescriptor_bits_per_pixel_set(ptr, data.bits_per_pixel);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchaseFinishOrderResponse ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.order_id = RAIL_API_PINVOKE.RailInGamePurchaseFinishOrderResponse_order_id_get(ptr);
    }

    public static void Csharp2Cpp(RailInGamePurchaseFinishOrderResponse data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailInGamePurchaseFinishOrderResponse_order_id_set(ptr, data.order_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchasePurchaseProductsResponse ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.order_id = RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_order_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_deliveried_products_get(ptr), ret.deliveried_products);
    }

    public static void Csharp2Cpp(RailInGamePurchasePurchaseProductsResponse data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_order_id_set(ptr, data.order_id);
      Csharp2Cpp(data.deliveried_products, RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_deliveried_products_get(ptr));
    }

    public static void Cpp2Csharp(
      IntPtr ptr,
      RailInGamePurchasePurchaseProductsToAssetsResponse ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.order_id = RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_deliveried_assets_get(ptr), ret.deliveried_assets);
    }

    public static void Csharp2Cpp(
      RailInGamePurchasePurchaseProductsToAssetsResponse data,
      IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_set(ptr, data.order_id);
      Csharp2Cpp(data.deliveried_assets, RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_deliveried_assets_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchaseRequestAllProductsResponse ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchaseRequestAllProductsResponse_all_products_get(ptr), ret.all_products);
    }

    public static void Csharp2Cpp(RailInGamePurchaseRequestAllProductsResponse data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.all_products, RAIL_API_PINVOKE.RailInGamePurchaseRequestAllProductsResponse_all_products_get(ptr));
    }

    public static void Cpp2Csharp(
      IntPtr ptr,
      RailInGamePurchaseRequestAllPurchasableProductsResponse ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_get(ptr), ret.purchasable_products);
    }

    public static void Csharp2Cpp(
      RailInGamePurchaseRequestAllPurchasableProductsResponse data,
      IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.purchasable_products, RAIL_API_PINVOKE.RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailInGameStorePurchasePayWindowClosed ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.order_id = RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowClosed_order_id_get(ptr);
    }

    public static void Csharp2Cpp(RailInGameStorePurchasePayWindowClosed data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowClosed_order_id_set(ptr, data.order_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailInGameStorePurchasePayWindowDisplayed ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.order_id = RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowDisplayed_order_id_get(ptr);
    }

    public static void Csharp2Cpp(RailInGameStorePurchasePayWindowDisplayed data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowDisplayed_order_id_set(ptr, data.order_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailInGameStorePurchaseResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.order_id = RAIL_API_PINVOKE.RailInGameStorePurchaseResult_order_id_get(ptr);
    }

    public static void Csharp2Cpp(RailInGameStorePurchaseResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailInGameStorePurchaseResult_order_id_set(ptr, data.order_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailInviteOptions ret)
    {
      ret.additional_message = RAIL_API_PINVOKE.RailInviteOptions_additional_message_get(ptr);
      ret.expire_time = RAIL_API_PINVOKE.RailInviteOptions_expire_time_get(ptr);
      ret.invite_type = (EnumRailUsersInviteType) RAIL_API_PINVOKE.RailInviteOptions_invite_type_get(ptr);
      ret.need_respond_in_game = RAIL_API_PINVOKE.RailInviteOptions_need_respond_in_game_get(ptr);
    }

    public static void Csharp2Cpp(RailInviteOptions data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailInviteOptions_additional_message_set(ptr, data.additional_message);
      RAIL_API_PINVOKE.RailInviteOptions_expire_time_set(ptr, data.expire_time);
      RAIL_API_PINVOKE.RailInviteOptions_invite_type_set(ptr, (int) data.invite_type);
      RAIL_API_PINVOKE.RailInviteOptions_need_respond_in_game_set(ptr, data.need_respond_in_game);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailKeyValue ret)
    {
      ret.value = RAIL_API_PINVOKE.RailKeyValue_value_get(ptr);
      ret.key = RAIL_API_PINVOKE.RailKeyValue_key_get(ptr);
    }

    public static void Csharp2Cpp(RailKeyValue data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailKeyValue_value_set(ptr, data.value);
      RAIL_API_PINVOKE.RailKeyValue_key_set(ptr, data.key);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailKeyValueResult ret)
    {
      ret.value = RAIL_API_PINVOKE.RailKeyValueResult_value_get(ptr);
      ret.key = RAIL_API_PINVOKE.RailKeyValueResult_key_get(ptr);
      ret.err = (RailResult) RAIL_API_PINVOKE.RailKeyValueResult_err_get(ptr);
    }

    public static void Csharp2Cpp(RailKeyValueResult data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailKeyValueResult_value_set(ptr, data.value);
      RAIL_API_PINVOKE.RailKeyValueResult_key_set(ptr, data.key);
      RAIL_API_PINVOKE.RailKeyValueResult_err_set(ptr, (int) data.err);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailListStreamFileOption ret)
    {
      ret.num_files = RAIL_API_PINVOKE.RailListStreamFileOption_num_files_get(ptr);
      ret.start_index = RAIL_API_PINVOKE.RailListStreamFileOption_start_index_get(ptr);
    }

    public static void Csharp2Cpp(RailListStreamFileOption data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailListStreamFileOption_num_files_set(ptr, data.num_files);
      RAIL_API_PINVOKE.RailListStreamFileOption_start_index_set(ptr, data.start_index);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailPlatformNotifyEventJoinGameByGameServer ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.commandline_info = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_commandline_info_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_get(ptr), ret.gameserver_railid);
    }

    public static void Csharp2Cpp(RailPlatformNotifyEventJoinGameByGameServer data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_commandline_info_set(ptr, data.commandline_info);
      Csharp2Cpp(data.gameserver_railid, RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailPlatformNotifyEventJoinGameByRoom ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.commandline_info = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_commandline_info_get(ptr);
      ret.room_id = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_room_id_get(ptr);
      ret.zone_id = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_zone_id_get(ptr);
    }

    public static void Csharp2Cpp(RailPlatformNotifyEventJoinGameByRoom data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_commandline_info_set(ptr, data.commandline_info);
      RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_zone_id_set(ptr, data.zone_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailPlatformNotifyEventJoinGameByUser ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_get(ptr), ret.rail_id_to_join);
      ret.commandline_info = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_commandline_info_get(ptr);
    }

    public static void Csharp2Cpp(RailPlatformNotifyEventJoinGameByUser data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.rail_id_to_join, RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_get(ptr));
      RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_commandline_info_set(ptr, data.commandline_info);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailProductItem ret)
    {
      ret.product_id = RAIL_API_PINVOKE.RailProductItem_product_id_get(ptr);
      ret.quantity = RAIL_API_PINVOKE.RailProductItem_quantity_get(ptr);
    }

    public static void Csharp2Cpp(RailProductItem data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailProductItem_product_id_set(ptr, data.product_id);
      RAIL_API_PINVOKE.RailProductItem_quantity_set(ptr, data.quantity);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailPublishFileToUserSpaceOption ret)
    {
      Cpp2Csharp(RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_key_value_get(ptr), ret.key_value);
      ret.description = RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_description_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_tags_get(ptr), ret.tags);
      ret.level = (EnumRailSpaceWorkShareLevel) RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_level_get(ptr);
      ret.version = RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_version_get(ptr);
      ret.preview_path_filename = RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_preview_path_filename_get(ptr);
      ret.type = (EnumRailSpaceWorkType) RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_type_get(ptr);
      ret.space_work_name = RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_space_work_name_get(ptr);
    }

    public static void Csharp2Cpp(RailPublishFileToUserSpaceOption data, IntPtr ptr)
    {
      Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_key_value_get(ptr));
      RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_description_set(ptr, data.description);
      Csharp2Cpp(data.tags, RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_tags_get(ptr));
      RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_level_set(ptr, (int) data.level);
      RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_version_set(ptr, data.version);
      RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_preview_path_filename_set(ptr, data.preview_path_filename);
      RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_type_set(ptr, (int) data.type);
      RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_space_work_name_set(ptr, data.space_work_name);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailPurchaseProductExtraInfo ret)
    {
      ret.bundle_rule = RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_bundle_rule_get(ptr);
      ret.exchange_rule = RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_exchange_rule_get(ptr);
    }

    public static void Csharp2Cpp(RailPurchaseProductExtraInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_bundle_rule_set(ptr, data.bundle_rule);
      RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_exchange_rule_set(ptr, data.exchange_rule);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailPurchaseProductInfo ret)
    {
      ret.category = RAIL_API_PINVOKE.RailPurchaseProductInfo_category_get(ptr);
      ret.original_price = RAIL_API_PINVOKE.RailPurchaseProductInfo_original_price_get(ptr);
      ret.description = RAIL_API_PINVOKE.RailPurchaseProductInfo_description_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailPurchaseProductInfo_discount_get(ptr), ret.discount);
      ret.is_purchasable = RAIL_API_PINVOKE.RailPurchaseProductInfo_is_purchasable_get(ptr);
      ret.name = RAIL_API_PINVOKE.RailPurchaseProductInfo_name_get(ptr);
      ret.currency_type = RAIL_API_PINVOKE.RailPurchaseProductInfo_currency_type_get(ptr);
      ret.product_thumbnail = RAIL_API_PINVOKE.RailPurchaseProductInfo_product_thumbnail_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailPurchaseProductInfo_extra_info_get(ptr), ret.extra_info);
      ret.product_id = RAIL_API_PINVOKE.RailPurchaseProductInfo_product_id_get(ptr);
    }

    public static void Csharp2Cpp(RailPurchaseProductInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailPurchaseProductInfo_category_set(ptr, data.category);
      RAIL_API_PINVOKE.RailPurchaseProductInfo_original_price_set(ptr, data.original_price);
      RAIL_API_PINVOKE.RailPurchaseProductInfo_description_set(ptr, data.description);
      Csharp2Cpp(data.discount, RAIL_API_PINVOKE.RailPurchaseProductInfo_discount_get(ptr));
      RAIL_API_PINVOKE.RailPurchaseProductInfo_is_purchasable_set(ptr, data.is_purchasable);
      RAIL_API_PINVOKE.RailPurchaseProductInfo_name_set(ptr, data.name);
      RAIL_API_PINVOKE.RailPurchaseProductInfo_currency_type_set(ptr, data.currency_type);
      RAIL_API_PINVOKE.RailPurchaseProductInfo_product_thumbnail_set(ptr, data.product_thumbnail);
      Csharp2Cpp(data.extra_info, RAIL_API_PINVOKE.RailPurchaseProductInfo_extra_info_get(ptr));
      RAIL_API_PINVOKE.RailPurchaseProductInfo_product_id_set(ptr, data.product_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailQueryWorkFileOptions ret)
    {
      ret.with_url = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_url_get(ptr);
      ret.with_uploader_ids = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_uploader_ids_get(ptr);
      ret.with_vote_detail = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_vote_detail_get(ptr);
      ret.with_description = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_description_get(ptr);
      ret.query_total_only = RAIL_API_PINVOKE.RailQueryWorkFileOptions_query_total_only_get(ptr);
      ret.with_preveiw_url = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_preveiw_url_get(ptr);
    }

    public static void Csharp2Cpp(RailQueryWorkFileOptions data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_url_set(ptr, data.with_url);
      RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_uploader_ids_set(ptr, data.with_uploader_ids);
      RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_vote_detail_set(ptr, data.with_vote_detail);
      RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_description_set(ptr, data.with_description);
      RAIL_API_PINVOKE.RailQueryWorkFileOptions_query_total_only_set(ptr, data.query_total_only);
      RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_preveiw_url_set(ptr, data.with_preveiw_url);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailSessionTicket ret) => ret.ticket = RAIL_API_PINVOKE.RailSessionTicket_ticket_get(ptr);

    public static void Csharp2Cpp(RailSessionTicket data, IntPtr ptr) => RAIL_API_PINVOKE.RailSessionTicket_ticket_set(ptr, data.ticket);

    public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkDescriptor ret)
    {
      Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_vote_details_get(ptr), ret.vote_details);
      ret.description = RAIL_API_PINVOKE.RailSpaceWorkDescriptor_description_get(ptr);
      ret.preview_url = RAIL_API_PINVOKE.RailSpaceWorkDescriptor_preview_url_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_id_get(ptr), ret.id);
      ret.detail_url = RAIL_API_PINVOKE.RailSpaceWorkDescriptor_detail_url_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_uploader_ids_get(ptr), ret.uploader_ids);
      ret.name = RAIL_API_PINVOKE.RailSpaceWorkDescriptor_name_get(ptr);
    }

    public static void Csharp2Cpp(RailSpaceWorkDescriptor data, IntPtr ptr)
    {
      Csharp2Cpp(data.vote_details, RAIL_API_PINVOKE.RailSpaceWorkDescriptor_vote_details_get(ptr));
      RAIL_API_PINVOKE.RailSpaceWorkDescriptor_description_set(ptr, data.description);
      RAIL_API_PINVOKE.RailSpaceWorkDescriptor_preview_url_set(ptr, data.preview_url);
      Csharp2Cpp(data.id, RAIL_API_PINVOKE.RailSpaceWorkDescriptor_id_get(ptr));
      RAIL_API_PINVOKE.RailSpaceWorkDescriptor_detail_url_set(ptr, data.detail_url);
      Csharp2Cpp(data.uploader_ids, RAIL_API_PINVOKE.RailSpaceWorkDescriptor_uploader_ids_get(ptr));
      RAIL_API_PINVOKE.RailSpaceWorkDescriptor_name_set(ptr, data.name);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkFilter ret)
    {
      Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_classes_get(ptr), ret.classes);
      Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_type_get(ptr), ret.type);
      Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_collector_list_get(ptr), ret.collector_list);
      Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_subscriber_list_get(ptr), ret.subscriber_list);
      Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_creator_list_get(ptr), ret.creator_list);
    }

    public static void Csharp2Cpp(RailSpaceWorkFilter data, IntPtr ptr)
    {
      Csharp2Cpp(data.classes, RAIL_API_PINVOKE.RailSpaceWorkFilter_classes_get(ptr));
      Csharp2Cpp(data.type, RAIL_API_PINVOKE.RailSpaceWorkFilter_type_get(ptr));
      Csharp2Cpp(data.collector_list, RAIL_API_PINVOKE.RailSpaceWorkFilter_collector_list_get(ptr));
      Csharp2Cpp(data.subscriber_list, RAIL_API_PINVOKE.RailSpaceWorkFilter_subscriber_list_get(ptr));
      Csharp2Cpp(data.creator_list, RAIL_API_PINVOKE.RailSpaceWorkFilter_creator_list_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkSearchFilter ret)
    {
      Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_excluded_tags_get(ptr), ret.excluded_tags);
      Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_required_tags_get(ptr), ret.required_tags);
      ret.search_text = RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_search_text_get(ptr);
    }

    public static void Csharp2Cpp(RailSpaceWorkSearchFilter data, IntPtr ptr)
    {
      Csharp2Cpp(data.excluded_tags, RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_excluded_tags_get(ptr));
      Csharp2Cpp(data.required_tags, RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_required_tags_get(ptr));
      RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_search_text_set(ptr, data.search_text);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkSyncProgress ret)
    {
      ret.progress = RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_progress_get(ptr);
      ret.finished_bytes = RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_finished_bytes_get(ptr);
      ret.total_bytes = RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_total_bytes_get(ptr);
      ret.current_state = (EnumRailSpaceWorkSyncState) RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_current_state_get(ptr);
    }

    public static void Csharp2Cpp(RailSpaceWorkSyncProgress data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_progress_set(ptr, data.progress);
      RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_finished_bytes_set(ptr, data.finished_bytes);
      RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_total_bytes_set(ptr, data.total_bytes);
      RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_current_state_set(ptr, (int) data.current_state);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkUpdateOptions ret)
    {
      ret.with_my_vote = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_my_vote_get(ptr);
      ret.with_vote_detail = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_vote_detail_get(ptr);
      ret.with_metadata = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_metadata_get(ptr);
      ret.with_detail = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_detail_get(ptr);
      ret.check_has_subscribed = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_subscribed_get(ptr);
      ret.check_has_favorited = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_favorited_get(ptr);
    }

    public static void Csharp2Cpp(RailSpaceWorkUpdateOptions data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_my_vote_set(ptr, data.with_my_vote);
      RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_vote_detail_set(ptr, data.with_vote_detail);
      RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_metadata_set(ptr, data.with_metadata);
      RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_detail_set(ptr, data.with_detail);
      RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_subscribed_set(ptr, data.check_has_subscribed);
      RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_favorited_set(ptr, data.check_has_favorited);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkVoteDetail ret)
    {
      ret.vote_value = (EnumRailSpaceWorkVoteValue) RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_vote_value_get(ptr);
      ret.voted_players = RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_voted_players_get(ptr);
    }

    public static void Csharp2Cpp(RailSpaceWorkVoteDetail data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_vote_value_set(ptr, (int) data.vote_value);
      RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_voted_players_set(ptr, data.voted_players);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailStoreOptions ret)
    {
      ret.window_margin_top = RAIL_API_PINVOKE.RailStoreOptions_window_margin_top_get(ptr);
      ret.window_margin_left = RAIL_API_PINVOKE.RailStoreOptions_window_margin_left_get(ptr);
      ret.store_type = (EnumRailStoreType) RAIL_API_PINVOKE.RailStoreOptions_store_type_get(ptr);
    }

    public static void Csharp2Cpp(RailStoreOptions data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailStoreOptions_window_margin_top_set(ptr, data.window_margin_top);
      RAIL_API_PINVOKE.RailStoreOptions_window_margin_left_set(ptr, data.window_margin_left);
      RAIL_API_PINVOKE.RailStoreOptions_store_type_set(ptr, (int) data.store_type);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailStreamFileInfo ret)
    {
      ret.file_size = RAIL_API_PINVOKE.RailStreamFileInfo_file_size_get(ptr);
      ret.filename = RAIL_API_PINVOKE.RailStreamFileInfo_filename_get(ptr);
    }

    public static void Csharp2Cpp(RailStreamFileInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailStreamFileInfo_file_size_set(ptr, data.file_size);
      RAIL_API_PINVOKE.RailStreamFileInfo_filename_set(ptr, data.filename);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailStreamFileOption ret)
    {
      ret.open_type = (EnumRailStreamOpenFileType) RAIL_API_PINVOKE.RailStreamFileOption_open_type_get(ptr);
      ret.unavaliabe_when_new_file_writing = RAIL_API_PINVOKE.RailStreamFileOption_unavaliabe_when_new_file_writing_get(ptr);
    }

    public static void Csharp2Cpp(RailStreamFileOption data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailStreamFileOption_open_type_set(ptr, (int) data.open_type);
      RAIL_API_PINVOKE.RailStreamFileOption_unavaliabe_when_new_file_writing_set(ptr, data.unavaliabe_when_new_file_writing);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailSyncFileOption ret) => ret.sync_file_not_to_remote = RAIL_API_PINVOKE.RailSyncFileOption_sync_file_not_to_remote_get(ptr);

    public static void Csharp2Cpp(RailSyncFileOption data, IntPtr ptr) => RAIL_API_PINVOKE.RailSyncFileOption_sync_file_not_to_remote_set(ptr, data.sync_file_not_to_remote);

    public static void Cpp2Csharp(IntPtr ptr, RailSystemStateChanged ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.state = (RailSystemState) RAIL_API_PINVOKE.RailSystemStateChanged_state_get(ptr);
    }

    public static void Csharp2Cpp(RailSystemStateChanged data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailSystemStateChanged_state_set(ptr, (int) data.state);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailUserPlayedWith ret)
    {
      ret.user_rich_content = RAIL_API_PINVOKE.RailUserPlayedWith_user_rich_content_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailUserPlayedWith_rail_id_get(ptr), ret.rail_id);
    }

    public static void Csharp2Cpp(RailUserPlayedWith data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RailUserPlayedWith_user_rich_content_set(ptr, data.user_rich_content);
      Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.RailUserPlayedWith_rail_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailUsersCancelInviteResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.invite_type = (EnumRailUsersInviteType) RAIL_API_PINVOKE.RailUsersCancelInviteResult_invite_type_get(ptr);
    }

    public static void Csharp2Cpp(RailUsersCancelInviteResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailUsersCancelInviteResult_invite_type_set(ptr, (int) data.invite_type);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailUsersGetInviteDetailResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.command_line = RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_command_line_get(ptr);
      ret.invite_type = (EnumRailUsersInviteType) RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_invite_type_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_inviter_id_get(ptr), ret.inviter_id);
    }

    public static void Csharp2Cpp(RailUsersGetInviteDetailResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_command_line_set(ptr, data.command_line);
      RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_invite_type_set(ptr, (int) data.invite_type);
      Csharp2Cpp(data.inviter_id, RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_inviter_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailUsersInfoData ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RailUsersInfoData_user_info_list_get(ptr), ret.user_info_list);
    }

    public static void Csharp2Cpp(RailUsersInfoData data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.user_info_list, RAIL_API_PINVOKE.RailUsersInfoData_user_info_list_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailUsersInviteJoinGameResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.response_value = (EnumRailUsersInviteResponseType) RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_response_value_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invitee_id_get(ptr), ret.invitee_id);
      ret.invite_type = (EnumRailUsersInviteType) RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invite_type_get(ptr);
    }

    public static void Csharp2Cpp(RailUsersInviteJoinGameResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_response_value_set(ptr, (int) data.response_value);
      Csharp2Cpp(data.invitee_id, RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invitee_id_get(ptr));
      RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invite_type_set(ptr, (int) data.invite_type);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailUsersInviteUsersResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.invite_type = (EnumRailUsersInviteType) RAIL_API_PINVOKE.RailUsersInviteUsersResult_invite_type_get(ptr);
    }

    public static void Csharp2Cpp(RailUsersInviteUsersResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RailUsersInviteUsersResult_invite_type_set(ptr, (int) data.invite_type);
    }

    public static void Cpp2Csharp(IntPtr ptr, RailUsersNotifyInviter ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RailUsersNotifyInviter_invitee_id_get(ptr), ret.invitee_id);
    }

    public static void Csharp2Cpp(RailUsersNotifyInviter data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.invitee_id, RAIL_API_PINVOKE.RailUsersNotifyInviter_invitee_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailUsersRespondInvation ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RailUsersRespondInvation_original_invite_option_get(ptr), ret.original_invite_option);
      ret.response = (EnumRailUsersInviteResponseType) RAIL_API_PINVOKE.RailUsersRespondInvation_response_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RailUsersRespondInvation_inviter_id_get(ptr), ret.inviter_id);
    }

    public static void Csharp2Cpp(RailUsersRespondInvation data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.original_invite_option, RAIL_API_PINVOKE.RailUsersRespondInvation_original_invite_option_get(ptr));
      RAIL_API_PINVOKE.RailUsersRespondInvation_response_set(ptr, (int) data.response);
      Csharp2Cpp(data.inviter_id, RAIL_API_PINVOKE.RailUsersRespondInvation_inviter_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RailVoiceCaptureOption ret) => ret.voice_data_format = (EnumRailVoiceCaptureFormat) RAIL_API_PINVOKE.RailVoiceCaptureOption_voice_data_format_get(ptr);

    public static void Csharp2Cpp(RailVoiceCaptureOption data, IntPtr ptr) => RAIL_API_PINVOKE.RailVoiceCaptureOption_voice_data_format_set(ptr, (int) data.voice_data_format);

    public static void Cpp2Csharp(IntPtr ptr, RailVoiceChannelID ret) => ret.id_ = RAIL_API_PINVOKE.RailVoiceChannelID_id__get(ptr);

    public static void Csharp2Cpp(RailVoiceChannelID data, IntPtr ptr) => RAIL_API_PINVOKE.RailVoiceChannelID_id__set(ptr, data.id_);

    public static void Cpp2Csharp(IntPtr ptr, ReloadBrowserResult ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(ReloadBrowserResult data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, RequestAllAssetsFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RequestAllAssetsFinished_assetinfo_list_get(ptr), ret.assetinfo_list);
    }

    public static void Csharp2Cpp(RequestAllAssetsFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.assetinfo_list, RAIL_API_PINVOKE.RequestAllAssetsFinished_assetinfo_list_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RequestLeaderboardEntryParam ret)
    {
      ret.range_end = RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_end_get(ptr);
      ret.range_start = RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_start_get(ptr);
      ret.type = (LeaderboardType) RAIL_API_PINVOKE.RequestLeaderboardEntryParam_type_get(ptr);
      ret.user_coordinate = RAIL_API_PINVOKE.RequestLeaderboardEntryParam_user_coordinate_get(ptr);
    }

    public static void Csharp2Cpp(RequestLeaderboardEntryParam data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_end_set(ptr, data.range_end);
      RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_start_set(ptr, data.range_start);
      RAIL_API_PINVOKE.RequestLeaderboardEntryParam_type_set(ptr, (int) data.type);
      RAIL_API_PINVOKE.RequestLeaderboardEntryParam_user_coordinate_set(ptr, data.user_coordinate);
    }

    public static void Cpp2Csharp(IntPtr ptr, RoomAllData ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RoomAllData_room_info_get(ptr), ret.room_info);
    }

    public static void Csharp2Cpp(RoomAllData data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.room_info, RAIL_API_PINVOKE.RoomAllData_room_info_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RoomDataReceived ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.message_type = RAIL_API_PINVOKE.RoomDataReceived_message_type_get(ptr);
      ret.data_len = RAIL_API_PINVOKE.RoomDataReceived_data_len_get(ptr);
      ret.room_id = RAIL_API_PINVOKE.RoomDataReceived_room_id_get(ptr);
      ret.data_buffer = RAIL_API_PINVOKE.RoomDataReceived_data_buffer_get(ptr);
    }

    public static void Csharp2Cpp(RoomDataReceived data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RoomDataReceived_message_type_set(ptr, data.message_type);
      RAIL_API_PINVOKE.RoomDataReceived_data_len_set(ptr, data.data_len);
      RAIL_API_PINVOKE.RoomDataReceived_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.RoomDataReceived_data_buffer_set(ptr, data.data_buffer);
    }

    public static void Cpp2Csharp(IntPtr ptr, RoomInfo ret)
    {
      ret.zone_id = RAIL_API_PINVOKE.RoomInfo_zone_id_get(ptr);
      ret.has_password = RAIL_API_PINVOKE.RoomInfo_has_password_get(ptr);
      ret.create_time = RAIL_API_PINVOKE.RoomInfo_create_time_get(ptr);
      ret.max_members = RAIL_API_PINVOKE.RoomInfo_max_members_get(ptr);
      ret.room_name = RAIL_API_PINVOKE.RoomInfo_room_name_get(ptr);
      ret.game_server_rail_id = RAIL_API_PINVOKE.RoomInfo_game_server_rail_id_get(ptr);
      ret.room_id = RAIL_API_PINVOKE.RoomInfo_room_id_get(ptr);
      ret.current_members = RAIL_API_PINVOKE.RoomInfo_current_members_get(ptr);
      ret.is_joinable = RAIL_API_PINVOKE.RoomInfo_is_joinable_get(ptr);
      ret.room_state = (EnumRoomStatus) RAIL_API_PINVOKE.RoomInfo_room_state_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RoomInfo_room_kvs_get(ptr), ret.room_kvs);
      ret.type = (EnumRoomType) RAIL_API_PINVOKE.RoomInfo_type_get(ptr);
      ret.game_server_channel_id = RAIL_API_PINVOKE.RoomInfo_game_server_channel_id_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RoomInfo_owner_id_get(ptr), ret.owner_id);
    }

    public static void Csharp2Cpp(RoomInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RoomInfo_zone_id_set(ptr, data.zone_id);
      RAIL_API_PINVOKE.RoomInfo_has_password_set(ptr, data.has_password);
      RAIL_API_PINVOKE.RoomInfo_create_time_set(ptr, data.create_time);
      RAIL_API_PINVOKE.RoomInfo_max_members_set(ptr, data.max_members);
      RAIL_API_PINVOKE.RoomInfo_room_name_set(ptr, data.room_name);
      RAIL_API_PINVOKE.RoomInfo_game_server_rail_id_set(ptr, data.game_server_rail_id);
      RAIL_API_PINVOKE.RoomInfo_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.RoomInfo_current_members_set(ptr, data.current_members);
      RAIL_API_PINVOKE.RoomInfo_is_joinable_set(ptr, data.is_joinable);
      RAIL_API_PINVOKE.RoomInfo_room_state_set(ptr, (int) data.room_state);
      Csharp2Cpp(data.room_kvs, RAIL_API_PINVOKE.RoomInfo_room_kvs_get(ptr));
      RAIL_API_PINVOKE.RoomInfo_type_set(ptr, (int) data.type);
      RAIL_API_PINVOKE.RoomInfo_game_server_channel_id_set(ptr, data.game_server_channel_id);
      Csharp2Cpp(data.owner_id, RAIL_API_PINVOKE.RoomInfo_owner_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, RoomInfoList ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.total_room_num_in_zone = RAIL_API_PINVOKE.RoomInfoList_total_room_num_in_zone_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RoomInfoList_room_info_get(ptr), ret.room_info);
      ret.end_index = RAIL_API_PINVOKE.RoomInfoList_end_index_get(ptr);
      ret.zone_id = RAIL_API_PINVOKE.RoomInfoList_zone_id_get(ptr);
      ret.begin_index = RAIL_API_PINVOKE.RoomInfoList_begin_index_get(ptr);
    }

    public static void Csharp2Cpp(RoomInfoList data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.RoomInfoList_total_room_num_in_zone_set(ptr, data.total_room_num_in_zone);
      Csharp2Cpp(data.room_info, RAIL_API_PINVOKE.RoomInfoList_room_info_get(ptr));
      RAIL_API_PINVOKE.RoomInfoList_end_index_set(ptr, data.end_index);
      RAIL_API_PINVOKE.RoomInfoList_zone_id_set(ptr, data.zone_id);
      RAIL_API_PINVOKE.RoomInfoList_begin_index_set(ptr, data.begin_index);
    }

    public static void Cpp2Csharp(IntPtr ptr, RoomInfoListFilter ret)
    {
      ret.room_name_contained = RAIL_API_PINVOKE.RoomInfoListFilter_room_name_contained_get(ptr);
      Cpp2Csharp(RAIL_API_PINVOKE.RoomInfoListFilter_key_filters_get(ptr), ret.key_filters);
      ret.filter_password = (EnumRailOptionalValue) RAIL_API_PINVOKE.RoomInfoListFilter_filter_password_get(ptr);
      ret.filter_friends_owned = (EnumRailOptionalValue) RAIL_API_PINVOKE.RoomInfoListFilter_filter_friends_owned_get(ptr);
      ret.available_slot_at_least = RAIL_API_PINVOKE.RoomInfoListFilter_available_slot_at_least_get(ptr);
    }

    public static void Csharp2Cpp(RoomInfoListFilter data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RoomInfoListFilter_room_name_contained_set(ptr, data.room_name_contained);
      Csharp2Cpp(data.key_filters, RAIL_API_PINVOKE.RoomInfoListFilter_key_filters_get(ptr));
      RAIL_API_PINVOKE.RoomInfoListFilter_filter_password_set(ptr, (int) data.filter_password);
      RAIL_API_PINVOKE.RoomInfoListFilter_filter_friends_owned_set(ptr, (int) data.filter_friends_owned);
      RAIL_API_PINVOKE.RoomInfoListFilter_available_slot_at_least_set(ptr, data.available_slot_at_least);
    }

    public static void Cpp2Csharp(IntPtr ptr, RoomInfoListFilterKey ret)
    {
      ret.filter_value = RAIL_API_PINVOKE.RoomInfoListFilterKey_filter_value_get(ptr);
      ret.key_name = RAIL_API_PINVOKE.RoomInfoListFilterKey_key_name_get(ptr);
      ret.value_type = (EnumRailPropertyValueType) RAIL_API_PINVOKE.RoomInfoListFilterKey_value_type_get(ptr);
      ret.comparison_type = (EnumRailComparisonType) RAIL_API_PINVOKE.RoomInfoListFilterKey_comparison_type_get(ptr);
    }

    public static void Csharp2Cpp(RoomInfoListFilterKey data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RoomInfoListFilterKey_filter_value_set(ptr, data.filter_value);
      RAIL_API_PINVOKE.RoomInfoListFilterKey_key_name_set(ptr, data.key_name);
      RAIL_API_PINVOKE.RoomInfoListFilterKey_value_type_set(ptr, (int) data.value_type);
      RAIL_API_PINVOKE.RoomInfoListFilterKey_comparison_type_set(ptr, (int) data.comparison_type);
    }

    public static void Cpp2Csharp(IntPtr ptr, RoomInfoListSorter ret)
    {
      ret.close_to_value = RAIL_API_PINVOKE.RoomInfoListSorter_close_to_value_get(ptr);
      ret.property_key = RAIL_API_PINVOKE.RoomInfoListSorter_property_key_get(ptr);
      ret.property_sort_type = (EnumRailSortType) RAIL_API_PINVOKE.RoomInfoListSorter_property_sort_type_get(ptr);
      ret.property_value_type = (EnumRailPropertyValueType) RAIL_API_PINVOKE.RoomInfoListSorter_property_value_type_get(ptr);
    }

    public static void Csharp2Cpp(RoomInfoListSorter data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RoomInfoListSorter_close_to_value_set(ptr, data.close_to_value);
      RAIL_API_PINVOKE.RoomInfoListSorter_property_key_set(ptr, data.property_key);
      RAIL_API_PINVOKE.RoomInfoListSorter_property_sort_type_set(ptr, (int) data.property_sort_type);
      RAIL_API_PINVOKE.RoomInfoListSorter_property_value_type_set(ptr, (int) data.property_value_type);
    }

    public static void Cpp2Csharp(IntPtr ptr, RoomMembersInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.RoomMembersInfo_member_info_get(ptr), ret.member_info);
      ret.room_id = RAIL_API_PINVOKE.RoomMembersInfo_room_id_get(ptr);
      ret.member_num = RAIL_API_PINVOKE.RoomMembersInfo_member_num_get(ptr);
    }

    public static void Csharp2Cpp(RoomMembersInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.member_info, RAIL_API_PINVOKE.RoomMembersInfo_member_info_get(ptr));
      RAIL_API_PINVOKE.RoomMembersInfo_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.RoomMembersInfo_member_num_set(ptr, data.member_num);
    }

    public static void Cpp2Csharp(IntPtr ptr, RoomOptions ret)
    {
      ret.max_members = RAIL_API_PINVOKE.RoomOptions_max_members_get(ptr);
      ret.password = RAIL_API_PINVOKE.RoomOptions_password_get(ptr);
      ret.type = (EnumRoomType) RAIL_API_PINVOKE.RoomOptions_type_get(ptr);
      ret.zone_id = RAIL_API_PINVOKE.RoomOptions_zone_id_get(ptr);
      ret.enable_team_voice = RAIL_API_PINVOKE.RoomOptions_enable_team_voice_get(ptr);
    }

    public static void Csharp2Cpp(RoomOptions data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.RoomOptions_max_members_set(ptr, data.max_members);
      RAIL_API_PINVOKE.RoomOptions_password_set(ptr, data.password);
      RAIL_API_PINVOKE.RoomOptions_type_set(ptr, (int) data.type);
      RAIL_API_PINVOKE.RoomOptions_zone_id_set(ptr, data.zone_id);
      RAIL_API_PINVOKE.RoomOptions_enable_team_voice_set(ptr, data.enable_team_voice);
    }

    public static void Cpp2Csharp(IntPtr ptr, ScreenshotRequestInfo ret) => Cpp2Csharp(ptr, (EventBase) ret);

    public static void Csharp2Cpp(ScreenshotRequestInfo data, IntPtr ptr) => Csharp2Cpp((EventBase) data, ptr);

    public static void Cpp2Csharp(IntPtr ptr, SetGameServerMetadataResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.SetGameServerMetadataResult_game_server_id_get(ptr), ret.game_server_id);
    }

    public static void Csharp2Cpp(SetGameServerMetadataResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.SetGameServerMetadataResult_game_server_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, SetMemberMetadataInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.room_id = RAIL_API_PINVOKE.SetMemberMetadataInfo_room_id_get(ptr);
      ret.member_id = RAIL_API_PINVOKE.SetMemberMetadataInfo_member_id_get(ptr);
    }

    public static void Csharp2Cpp(SetMemberMetadataInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.SetMemberMetadataInfo_room_id_set(ptr, data.room_id);
      RAIL_API_PINVOKE.SetMemberMetadataInfo_member_id_set(ptr, data.member_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, SetRoomMetadataInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.room_id = RAIL_API_PINVOKE.SetRoomMetadataInfo_room_id_get(ptr);
    }

    public static void Csharp2Cpp(SetRoomMetadataInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.SetRoomMetadataInfo_room_id_set(ptr, data.room_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, ShareStorageToSpaceWorkResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.ShareStorageToSpaceWorkResult_space_work_id_get(ptr), ret.space_work_id);
    }

    public static void Csharp2Cpp(ShareStorageToSpaceWorkResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.space_work_id, RAIL_API_PINVOKE.ShareStorageToSpaceWorkResult_space_work_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, ShowFloatingWindowResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.window_type = (EnumRailWindowType) RAIL_API_PINVOKE.ShowFloatingWindowResult_window_type_get(ptr);
      ret.is_show = RAIL_API_PINVOKE.ShowFloatingWindowResult_is_show_get(ptr);
      ret.result = (RailResult) RAIL_API_PINVOKE.ShowFloatingWindowResult_result_get(ptr);
    }

    public static void Csharp2Cpp(ShowFloatingWindowResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.ShowFloatingWindowResult_window_type_set(ptr, (int) data.window_type);
      RAIL_API_PINVOKE.ShowFloatingWindowResult_is_show_set(ptr, data.is_show);
      RAIL_API_PINVOKE.ShowFloatingWindowResult_result_set(ptr, (int) data.result);
    }

    public static void Cpp2Csharp(IntPtr ptr, ShowNotifyFloatingWindowResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.notify_window_type = (EnumRailNotifyWindowType) RAIL_API_PINVOKE.ShowNotifyFloatingWindowResult_notify_window_type_get(ptr);
      ret.is_show = RAIL_API_PINVOKE.ShowNotifyFloatingWindowResult_is_show_get(ptr);
      ret.result = (RailResult) RAIL_API_PINVOKE.ShowNotifyFloatingWindowResult_result_get(ptr);
    }

    public static void Csharp2Cpp(ShowNotifyFloatingWindowResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.ShowNotifyFloatingWindowResult_notify_window_type_set(ptr, (int) data.notify_window_type);
      RAIL_API_PINVOKE.ShowNotifyFloatingWindowResult_is_show_set(ptr, data.is_show);
      RAIL_API_PINVOKE.ShowNotifyFloatingWindowResult_result_set(ptr, (int) data.result);
    }

    public static void Cpp2Csharp(IntPtr ptr, SpaceWorkID ret) => ret.id_ = RAIL_API_PINVOKE.SpaceWorkID_id__get(ptr);

    public static void Csharp2Cpp(SpaceWorkID data, IntPtr ptr) => RAIL_API_PINVOKE.SpaceWorkID_id__set(ptr, data.id_);

    public static void Cpp2Csharp(IntPtr ptr, SplitAssetsFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.source_asset = RAIL_API_PINVOKE.SplitAssetsFinished_source_asset_get(ptr);
      ret.to_quantity = RAIL_API_PINVOKE.SplitAssetsFinished_to_quantity_get(ptr);
      ret.new_asset_id = RAIL_API_PINVOKE.SplitAssetsFinished_new_asset_id_get(ptr);
    }

    public static void Csharp2Cpp(SplitAssetsFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.SplitAssetsFinished_source_asset_set(ptr, data.source_asset);
      RAIL_API_PINVOKE.SplitAssetsFinished_to_quantity_set(ptr, data.to_quantity);
      RAIL_API_PINVOKE.SplitAssetsFinished_new_asset_id_set(ptr, data.new_asset_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, SplitAssetsToFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.source_asset = RAIL_API_PINVOKE.SplitAssetsToFinished_source_asset_get(ptr);
      ret.to_quantity = RAIL_API_PINVOKE.SplitAssetsToFinished_to_quantity_get(ptr);
      ret.split_to_asset_id = RAIL_API_PINVOKE.SplitAssetsToFinished_split_to_asset_id_get(ptr);
    }

    public static void Csharp2Cpp(SplitAssetsToFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.SplitAssetsToFinished_source_asset_set(ptr, data.source_asset);
      RAIL_API_PINVOKE.SplitAssetsToFinished_to_quantity_set(ptr, data.to_quantity);
      RAIL_API_PINVOKE.SplitAssetsToFinished_split_to_asset_id_set(ptr, data.split_to_asset_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, StartConsumeAssetsFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.asset_id = RAIL_API_PINVOKE.StartConsumeAssetsFinished_asset_id_get(ptr);
    }

    public static void Csharp2Cpp(StartConsumeAssetsFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.StartConsumeAssetsFinished_asset_id_set(ptr, data.asset_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, StartSessionWithPlayerResponse ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.StartSessionWithPlayerResponse_remote_rail_id_get(ptr), ret.remote_rail_id);
    }

    public static void Csharp2Cpp(StartSessionWithPlayerResponse data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.remote_rail_id, RAIL_API_PINVOKE.StartSessionWithPlayerResponse_remote_rail_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, SyncSpaceWorkResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.SyncSpaceWorkResult_id_get(ptr), ret.id);
    }

    public static void Csharp2Cpp(SyncSpaceWorkResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.id, RAIL_API_PINVOKE.SyncSpaceWorkResult_id_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, TakeScreenshotResult ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.thumbnail_file_size = RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_file_size_get(ptr);
      ret.thumbnail_filepath = RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_filepath_get(ptr);
      ret.image_file_size = RAIL_API_PINVOKE.TakeScreenshotResult_image_file_size_get(ptr);
      ret.image_file_path = RAIL_API_PINVOKE.TakeScreenshotResult_image_file_path_get(ptr);
    }

    public static void Csharp2Cpp(TakeScreenshotResult data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_file_size_set(ptr, data.thumbnail_file_size);
      RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_filepath_set(ptr, data.thumbnail_filepath);
      RAIL_API_PINVOKE.TakeScreenshotResult_image_file_size_set(ptr, data.image_file_size);
      RAIL_API_PINVOKE.TakeScreenshotResult_image_file_path_set(ptr, data.image_file_path);
    }

    public static void Cpp2Csharp(IntPtr ptr, UpdateAssetsPropertyFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.UpdateAssetsPropertyFinished_asset_property_list_get(ptr), ret.asset_property_list);
    }

    public static void Csharp2Cpp(UpdateAssetsPropertyFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.asset_property_list, RAIL_API_PINVOKE.UpdateAssetsPropertyFinished_asset_property_list_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, UpdateConsumeAssetsFinished ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.asset_id = RAIL_API_PINVOKE.UpdateConsumeAssetsFinished_asset_id_get(ptr);
    }

    public static void Csharp2Cpp(UpdateConsumeAssetsFinished data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.UpdateConsumeAssetsFinished_asset_id_set(ptr, data.asset_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, UploadLeaderboardParam ret)
    {
      Cpp2Csharp(RAIL_API_PINVOKE.UploadLeaderboardParam_data_get(ptr), ret.data);
      ret.type = (LeaderboardUploadType) RAIL_API_PINVOKE.UploadLeaderboardParam_type_get(ptr);
    }

    public static void Csharp2Cpp(UploadLeaderboardParam data, IntPtr ptr)
    {
      Csharp2Cpp(data.data, RAIL_API_PINVOKE.UploadLeaderboardParam_data_get(ptr));
      RAIL_API_PINVOKE.UploadLeaderboardParam_type_set(ptr, (int) data.type);
    }

    public static void Cpp2Csharp(IntPtr ptr, UserRoomListInfo ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.UserRoomListInfo_room_info_get(ptr), ret.room_info);
    }

    public static void Csharp2Cpp(UserRoomListInfo data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.room_info, RAIL_API_PINVOKE.UserRoomListInfo_room_info_get(ptr));
    }

    public static void Cpp2Csharp(IntPtr ptr, VoiceDataCapturedEvent ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      ret.is_last_package = RAIL_API_PINVOKE.VoiceDataCapturedEvent_is_last_package_get(ptr);
    }

    public static void Csharp2Cpp(VoiceDataCapturedEvent data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      RAIL_API_PINVOKE.VoiceDataCapturedEvent_is_last_package_set(ptr, data.is_last_package);
    }

    public static void Cpp2Csharp(IntPtr ptr, ZoneInfo ret)
    {
      ret.status = (EnumZoneStatus) RAIL_API_PINVOKE.ZoneInfo_status_get(ptr);
      ret.description = RAIL_API_PINVOKE.ZoneInfo_description_get(ptr);
      ret.name = RAIL_API_PINVOKE.ZoneInfo_name_get(ptr);
      ret.idc_id = RAIL_API_PINVOKE.ZoneInfo_idc_id_get(ptr);
      ret.country_code = RAIL_API_PINVOKE.ZoneInfo_country_code_get(ptr);
      ret.zone_id = RAIL_API_PINVOKE.ZoneInfo_zone_id_get(ptr);
    }

    public static void Csharp2Cpp(ZoneInfo data, IntPtr ptr)
    {
      RAIL_API_PINVOKE.ZoneInfo_status_set(ptr, (int) data.status);
      RAIL_API_PINVOKE.ZoneInfo_description_set(ptr, data.description);
      RAIL_API_PINVOKE.ZoneInfo_name_set(ptr, data.name);
      RAIL_API_PINVOKE.ZoneInfo_idc_id_set(ptr, data.idc_id);
      RAIL_API_PINVOKE.ZoneInfo_country_code_set(ptr, data.country_code);
      RAIL_API_PINVOKE.ZoneInfo_zone_id_set(ptr, data.zone_id);
    }

    public static void Cpp2Csharp(IntPtr ptr, ZoneInfoList ret)
    {
      Cpp2Csharp(ptr, (EventBase) ret);
      Cpp2Csharp(RAIL_API_PINVOKE.ZoneInfoList_zone_info_get(ptr), ret.zone_info);
    }

    public static void Csharp2Cpp(ZoneInfoList data, IntPtr ptr)
    {
      Csharp2Cpp((EventBase) data, ptr);
      Csharp2Cpp(data.zone_info, RAIL_API_PINVOKE.ZoneInfoList_zone_info_get(ptr));
    }

    public static void Csharp2Cpp(List<GameServerPlayerInfo> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_GameServerPlayerInfo__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_GameServerPlayerInfo(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<GameServerPlayerInfo> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_Item(ptr, (uint) jarg2);
        GameServerPlayerInfo serverPlayerInfo = new GameServerPlayerInfo();
        GameServerPlayerInfo ret1 = serverPlayerInfo;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(serverPlayerInfo);
      }
    }

    public static void Csharp2Cpp(List<RailSpaceWorkDescriptor> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailSpaceWorkDescriptor__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailSpaceWorkDescriptor(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailSpaceWorkDescriptor> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_Item(ptr, (uint) jarg2);
        RailSpaceWorkDescriptor spaceWorkDescriptor = new RailSpaceWorkDescriptor();
        RailSpaceWorkDescriptor ret1 = spaceWorkDescriptor;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(spaceWorkDescriptor);
      }
    }

    public static void Csharp2Cpp(List<RailDlcOwned> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailDlcOwned_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailDlcOwned__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailDlcOwned_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailDlcOwned(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailDlcOwned> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailDlcOwned_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailDlcOwned_Item(ptr, (uint) jarg2);
        RailDlcOwned railDlcOwned = new RailDlcOwned();
        RailDlcOwned ret1 = railDlcOwned;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railDlcOwned);
      }
    }

    public static void Csharp2Cpp(List<MemberInfo> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayMemberInfo_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_MemberInfo__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayMemberInfo_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_MemberInfo(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<MemberInfo> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayMemberInfo_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayMemberInfo_Item(ptr, (uint) jarg2);
        MemberInfo memberInfo = new MemberInfo();
        MemberInfo ret1 = memberInfo;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(memberInfo);
      }
    }

    public static void Csharp2Cpp(List<RoomInfoListFilterKey> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RoomInfoListFilterKey__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RoomInfoListFilterKey(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RoomInfoListFilterKey> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_Item(ptr, (uint) jarg2);
        RoomInfoListFilterKey infoListFilterKey = new RoomInfoListFilterKey();
        RoomInfoListFilterKey ret1 = infoListFilterKey;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(infoListFilterKey);
      }
    }

    public static void Csharp2Cpp(List<GameServerInfo> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayGameServerInfo_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_GameServerInfo__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayGameServerInfo_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_GameServerInfo(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<GameServerInfo> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayGameServerInfo_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayGameServerInfo_Item(ptr, (uint) jarg2);
        GameServerInfo gameServerInfo = new GameServerInfo();
        GameServerInfo ret1 = gameServerInfo;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(gameServerInfo);
      }
    }

    public static void Csharp2Cpp(List<RailUserPlayedWith> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailUserPlayedWith__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailUserPlayedWith(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailUserPlayedWith> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_Item(ptr, (uint) jarg2);
        RailUserPlayedWith railUserPlayedWith = new RailUserPlayedWith();
        RailUserPlayedWith ret1 = railUserPlayedWith;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railUserPlayedWith);
      }
    }

    public static void Csharp2Cpp(List<RailID> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailID_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailID__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailID_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailID> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailID_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailID_Item(ptr, (uint) jarg2);
        RailID railId = new RailID();
        RailID ret1 = railId;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railId);
      }
    }

    public static void Csharp2Cpp(List<RailDlcID> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailDlcID_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailDlcID__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailDlcID_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailDlcID(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailDlcID> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailDlcID_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailDlcID_Item(ptr, (uint) jarg2);
        RailDlcID railDlcId = new RailDlcID();
        RailDlcID ret1 = railDlcId;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railDlcId);
      }
    }

    public static void Csharp2Cpp(List<GameServerListSorter> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayGameServerListSorter_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_GameServerListSorter__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayGameServerListSorter_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_GameServerListSorter(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<GameServerListSorter> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayGameServerListSorter_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayGameServerListSorter_Item(ptr, (uint) jarg2);
        GameServerListSorter serverListSorter = new GameServerListSorter();
        GameServerListSorter ret1 = serverListSorter;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(serverListSorter);
      }
    }

    public static void Csharp2Cpp(List<RoomInfo> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRoomInfo_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RoomInfo__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRoomInfo_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RoomInfo(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RoomInfo> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRoomInfo_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRoomInfo_Item(ptr, (uint) jarg2);
        RoomInfo roomInfo = new RoomInfo();
        RoomInfo ret1 = roomInfo;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(roomInfo);
      }
    }

    public static void Csharp2Cpp(List<RailPurchaseProductInfo> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailPurchaseProductInfo__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailPurchaseProductInfo(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailPurchaseProductInfo> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_Item(ptr, (uint) jarg2);
        RailPurchaseProductInfo purchaseProductInfo = new RailPurchaseProductInfo();
        RailPurchaseProductInfo ret1 = purchaseProductInfo;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(purchaseProductInfo);
      }
    }

    public static void Csharp2Cpp(List<RailSpaceWorkVoteDetail> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailSpaceWorkVoteDetail__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailSpaceWorkVoteDetail(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailSpaceWorkVoteDetail> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_Item(ptr, (uint) jarg2);
        RailSpaceWorkVoteDetail spaceWorkVoteDetail = new RailSpaceWorkVoteDetail();
        RailSpaceWorkVoteDetail ret1 = spaceWorkVoteDetail;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(spaceWorkVoteDetail);
      }
    }

    public static void Csharp2Cpp(List<RailKeyValue> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailKeyValue_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailKeyValue();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailKeyValue_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailKeyValue(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailKeyValue> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailKeyValue_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailKeyValue_Item(ptr, (uint) jarg2);
        RailKeyValue railKeyValue = new RailKeyValue();
        RailKeyValue ret1 = railKeyValue;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railKeyValue);
      }
    }

    public static void Csharp2Cpp(List<string> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailString_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
        RAIL_API_PINVOKE.RailString_SetValue(num, ret[index]);
        RAIL_API_PINVOKE.RailArrayRailString_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<string> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailString_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr jarg1 = RAIL_API_PINVOKE.RailArrayRailString_Item(ptr, (uint) jarg2);
        ret.Add(RAIL_API_PINVOKE.RailString_c_str(jarg1));
      }
    }

    public static void Csharp2Cpp(List<RailKeyValueResult> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailKeyValueResult_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailKeyValueResult__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailKeyValueResult_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailKeyValueResult(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailKeyValueResult> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailKeyValueResult_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailKeyValueResult_Item(ptr, (uint) jarg2);
        RailKeyValueResult railKeyValueResult = new RailKeyValueResult();
        RailKeyValueResult ret1 = railKeyValueResult;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railKeyValueResult);
      }
    }

    public static void Csharp2Cpp(List<RoomInfoListSorter> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RoomInfoListSorter__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RoomInfoListSorter(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RoomInfoListSorter> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_Item(ptr, (uint) jarg2);
        RoomInfoListSorter roomInfoListSorter = new RoomInfoListSorter();
        RoomInfoListSorter ret1 = roomInfoListSorter;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(roomInfoListSorter);
      }
    }

    public static void Csharp2Cpp(List<SpaceWorkID> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArraySpaceWorkID_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArraySpaceWorkID_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_SpaceWorkID(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<SpaceWorkID> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArraySpaceWorkID_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArraySpaceWorkID_Item(ptr, (uint) jarg2);
        SpaceWorkID spaceWorkId = new SpaceWorkID();
        SpaceWorkID ret1 = spaceWorkId;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(spaceWorkId);
      }
    }

    public static void Csharp2Cpp(List<EnumRailSpaceWorkType> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.NewInt();
        RAIL_API_PINVOKE.SetInt(num, (int) ret[index]);
        RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_push_back(ptr, num);
        RAIL_API_PINVOKE.DeleteInt(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<EnumRailSpaceWorkType> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr jarg1 = RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_Item(ptr, (uint) jarg2);
        ret.Add((EnumRailSpaceWorkType) RAIL_API_PINVOKE.GetInt(jarg1));
      }
    }

    public static void Csharp2Cpp(List<GameServerListFilter> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayGameServerListFilter_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_GameServerListFilter__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayGameServerListFilter_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_GameServerListFilter(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<GameServerListFilter> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayGameServerListFilter_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayGameServerListFilter_Item(ptr, (uint) jarg2);
        GameServerListFilter serverListFilter = new GameServerListFilter();
        GameServerListFilter ret1 = serverListFilter;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(serverListFilter);
      }
    }

    public static void Csharp2Cpp(List<RoomInfoListFilter> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RoomInfoListFilter__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RoomInfoListFilter(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RoomInfoListFilter> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_Item(ptr, (uint) jarg2);
        RoomInfoListFilter roomInfoListFilter = new RoomInfoListFilter();
        RoomInfoListFilter ret1 = roomInfoListFilter;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(roomInfoListFilter);
      }
    }

    public static void Csharp2Cpp(List<RailStreamFileInfo> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailStreamFileInfo__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailStreamFileInfo(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailStreamFileInfo> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_Item(ptr, (uint) jarg2);
        RailStreamFileInfo railStreamFileInfo = new RailStreamFileInfo();
        RailStreamFileInfo ret1 = railStreamFileInfo;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railStreamFileInfo);
      }
    }

    public static void Csharp2Cpp(List<GameServerListFilterKey> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_GameServerListFilterKey__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_GameServerListFilterKey(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<GameServerListFilterKey> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_Item(ptr, (uint) jarg2);
        GameServerListFilterKey serverListFilterKey = new GameServerListFilterKey();
        GameServerListFilterKey ret1 = serverListFilterKey;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(serverListFilterKey);
      }
    }

    public static void Csharp2Cpp(List<RailAssetProperty> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailAssetProperty_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailAssetProperty__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailAssetProperty_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailAssetProperty(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailAssetProperty> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailAssetProperty_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailAssetProperty_Item(ptr, (uint) jarg2);
        RailAssetProperty railAssetProperty = new RailAssetProperty();
        RailAssetProperty ret1 = railAssetProperty;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railAssetProperty);
      }
    }

    public static void Csharp2Cpp(List<EnumRailWorkFileClass> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.NewInt();
        RAIL_API_PINVOKE.SetInt(num, (int) ret[index]);
        RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_push_back(ptr, num);
        RAIL_API_PINVOKE.DeleteInt(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<EnumRailWorkFileClass> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr jarg1 = RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_Item(ptr, (uint) jarg2);
        ret.Add((EnumRailWorkFileClass) RAIL_API_PINVOKE.GetInt(jarg1));
      }
    }

    public static void Csharp2Cpp(List<PlayerPersonalInfo> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_PlayerPersonalInfo__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_PlayerPersonalInfo(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<PlayerPersonalInfo> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_Item(ptr, (uint) jarg2);
        PlayerPersonalInfo playerPersonalInfo = new PlayerPersonalInfo();
        PlayerPersonalInfo ret1 = playerPersonalInfo;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(playerPersonalInfo);
      }
    }

    public static void Csharp2Cpp(List<RailAssetItem> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailAssetItem_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailAssetItem__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailAssetItem_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailAssetItem(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailAssetItem> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailAssetItem_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailAssetItem_Item(ptr, (uint) jarg2);
        RailAssetItem railAssetItem = new RailAssetItem();
        RailAssetItem ret1 = railAssetItem;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railAssetItem);
      }
    }

    public static void Csharp2Cpp(List<RailAssetInfo> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailAssetInfo_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailAssetInfo__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailAssetInfo_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailAssetInfo(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailAssetInfo> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailAssetInfo_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailAssetInfo_Item(ptr, (uint) jarg2);
        RailAssetInfo railAssetInfo = new RailAssetInfo();
        RailAssetInfo ret1 = railAssetInfo;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railAssetInfo);
      }
    }

    public static void Csharp2Cpp(List<ZoneInfo> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayZoneInfo_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_ZoneInfo__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayZoneInfo_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_ZoneInfo(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<ZoneInfo> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayZoneInfo_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayZoneInfo_Item(ptr, (uint) jarg2);
        ZoneInfo zoneInfo = new ZoneInfo();
        ZoneInfo ret1 = zoneInfo;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(zoneInfo);
      }
    }

    public static void Csharp2Cpp(List<RailProductItem> ret, IntPtr ptr)
    {
      int count = ret.Count;
      RAIL_API_PINVOKE.RailArrayRailProductItem_clear(ptr);
      for (int index = 0; index < count; ++index)
      {
        IntPtr num = RAIL_API_PINVOKE.new_RailProductItem__SWIG_0();
        Csharp2Cpp(ret[index], num);
        RAIL_API_PINVOKE.RailArrayRailProductItem_push_back(ptr, num);
        RAIL_API_PINVOKE.delete_RailProductItem(num);
      }
    }

    public static void Cpp2Csharp(IntPtr ptr, List<RailProductItem> ret)
    {
      ret.Clear();
      uint num = RAIL_API_PINVOKE.RailArrayRailProductItem_size(ptr);
      for (int jarg2 = 0; jarg2 < num; ++jarg2)
      {
        IntPtr ptr1 = RAIL_API_PINVOKE.RailArrayRailProductItem_Item(ptr, (uint) jarg2);
        RailProductItem railProductItem = new RailProductItem();
        RailProductItem ret1 = railProductItem;
        Cpp2Csharp(ptr1, ret1);
        ret.Add(railProductItem);
      }
    }
  }
}
