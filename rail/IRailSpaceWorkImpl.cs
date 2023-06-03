// Decompiled with JetBrains decompiler
// Type: rail.IRailSpaceWorkImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailSpaceWorkImpl : RailObject, IRailSpaceWork, IRailComponent
  {
    internal IRailSpaceWorkImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailSpaceWorkImpl()
    {
    }

    public virtual void Close() => RAIL_API_PINVOKE.IRailSpaceWork_Close(swigCPtr_);

    public virtual SpaceWorkID GetSpaceWorkID()
    {
      IntPtr spaceWorkId1 = RAIL_API_PINVOKE.IRailSpaceWork_GetSpaceWorkID(swigCPtr_);
      SpaceWorkID spaceWorkId2 = new SpaceWorkID();
      SpaceWorkID ret = spaceWorkId2;
      RailConverter.Cpp2Csharp(spaceWorkId1, ret);
      return spaceWorkId2;
    }

    public virtual bool Editable() => RAIL_API_PINVOKE.IRailSpaceWork_Editable(swigCPtr_);

    public virtual RailResult StartSync(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_StartSync(swigCPtr_, user_data);

    public virtual RailResult GetSyncProgress(RailSpaceWorkSyncProgress progress)
    {
      IntPtr num = progress == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkSyncProgress__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetSyncProgress(swigCPtr_, num);
      }
      finally
      {
        if (progress != null)
          RailConverter.Cpp2Csharp(num, progress);
        RAIL_API_PINVOKE.delete_RailSpaceWorkSyncProgress(num);
      }
    }

    public virtual RailResult CancelSync() => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_CancelSync(swigCPtr_);

    public virtual RailResult GetWorkLocalFolder(out string path)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetWorkLocalFolder(swigCPtr_, num);
      }
      finally
      {
        path = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult AsyncUpdateMetadata(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_AsyncUpdateMetadata(swigCPtr_, user_data);

    public virtual RailResult GetName(out string name)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetName(swigCPtr_, num);
      }
      finally
      {
        name = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult GetDescription(out string description)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetDescription(swigCPtr_, num);
      }
      finally
      {
        description = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult GetUrl(out string url)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetUrl(swigCPtr_, num);
      }
      finally
      {
        url = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual uint GetCreateTime() => RAIL_API_PINVOKE.IRailSpaceWork_GetCreateTime(swigCPtr_);

    public virtual uint GetLastUpdateTime() => RAIL_API_PINVOKE.IRailSpaceWork_GetLastUpdateTime(swigCPtr_);

    public virtual ulong GetWorkFileSize() => RAIL_API_PINVOKE.IRailSpaceWork_GetWorkFileSize(swigCPtr_);

    public virtual RailResult GetTags(List<string> tags)
    {
      IntPtr num = tags == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetTags(swigCPtr_, num);
      }
      finally
      {
        if (tags != null)
          RailConverter.Cpp2Csharp(num, tags);
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual RailResult GetPreviewImage(out string path)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetPreviewImage(swigCPtr_, num);
      }
      finally
      {
        path = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult GetVersion(out string version)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetVersion(swigCPtr_, num);
      }
      finally
      {
        version = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual ulong GetDownloadCount() => RAIL_API_PINVOKE.IRailSpaceWork_GetDownloadCount(swigCPtr_);

    public virtual ulong GetSubscribedCount() => RAIL_API_PINVOKE.IRailSpaceWork_GetSubscribedCount(swigCPtr_);

    public virtual EnumRailSpaceWorkShareLevel GetShareLevel() => (EnumRailSpaceWorkShareLevel) RAIL_API_PINVOKE.IRailSpaceWork_GetShareLevel(swigCPtr_);

    public virtual ulong GetScore() => RAIL_API_PINVOKE.IRailSpaceWork_GetScore(swigCPtr_);

    public virtual RailResult GetMetadata(string key, out string value)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetMetadata(swigCPtr_, key, num);
      }
      finally
      {
        value = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual EnumRailSpaceWorkVoteValue GetMyVote() => (EnumRailSpaceWorkVoteValue) RAIL_API_PINVOKE.IRailSpaceWork_GetMyVote(swigCPtr_);

    public virtual bool IsFavorite() => RAIL_API_PINVOKE.IRailSpaceWork_IsFavorite(swigCPtr_);

    public virtual bool IsSubscribed() => RAIL_API_PINVOKE.IRailSpaceWork_IsSubscribed(swigCPtr_);

    public virtual RailResult SetName(string name) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetName(swigCPtr_, name);

    public virtual RailResult SetDescription(string description) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetDescription(swigCPtr_, description);

    public virtual RailResult SetTags(List<string> tags)
    {
      IntPtr num = tags == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      if (tags != null)
        RailConverter.Csharp2Cpp(tags, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetTags(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual RailResult SetPreviewImage(string path_filename) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetPreviewImage(swigCPtr_, path_filename);

    public virtual RailResult SetVersion(string version) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetVersion(swigCPtr_, version);

    public virtual RailResult SetShareLevel(EnumRailSpaceWorkShareLevel level) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetShareLevel__SWIG_0(swigCPtr_, (int) level);

    public virtual RailResult SetShareLevel() => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetShareLevel__SWIG_1(swigCPtr_);

    public virtual RailResult SetMetadata(string key, string value) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetMetadata(swigCPtr_, key, value);

    public virtual RailResult SetContentFromFolder(string path) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetContentFromFolder(swigCPtr_, path);

    public virtual RailResult GetAllMetadata(List<RailKeyValue> metadata)
    {
      IntPtr num = metadata == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetAllMetadata(swigCPtr_, num);
      }
      finally
      {
        if (metadata != null)
          RailConverter.Cpp2Csharp(num, metadata);
        RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(num);
      }
    }

    public virtual RailResult GetAdditionalPreviewUrls(List<string> preview_urls)
    {
      IntPtr num = preview_urls == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetAdditionalPreviewUrls(swigCPtr_, num);
      }
      finally
      {
        if (preview_urls != null)
          RailConverter.Cpp2Csharp(num, preview_urls);
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual RailResult GetAssociatedSpaceWorks(List<SpaceWorkID> ids)
    {
      IntPtr num = ids == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetAssociatedSpaceWorks(swigCPtr_, num);
      }
      finally
      {
        if (ids != null)
          RailConverter.Cpp2Csharp(num, ids);
        RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(num);
      }
    }

    public virtual RailResult GetLanguages(List<string> languages)
    {
      IntPtr num = languages == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetLanguages(swigCPtr_, num);
      }
      finally
      {
        if (languages != null)
          RailConverter.Cpp2Csharp(num, languages);
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual RailResult RemoveMetadata(string key) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_RemoveMetadata(swigCPtr_, key);

    public virtual RailResult SetAdditionalPreviews(List<string> local_paths)
    {
      IntPtr num = local_paths == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      if (local_paths != null)
        RailConverter.Csharp2Cpp(local_paths, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetAdditionalPreviews(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual RailResult SetAssociatedSpaceWorks(List<SpaceWorkID> ids)
    {
      IntPtr num = ids == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0();
      if (ids != null)
        RailConverter.Csharp2Cpp(ids, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetAssociatedSpaceWorks(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(num);
      }
    }

    public virtual RailResult SetLanguages(List<string> languages)
    {
      IntPtr num = languages == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      if (languages != null)
        RailConverter.Csharp2Cpp(languages, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetLanguages(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual RailResult GetPreviewUrl(out string url)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetPreviewUrl(swigCPtr_, num);
      }
      finally
      {
        url = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult GetVoteDetail(List<RailSpaceWorkVoteDetail> vote_details)
    {
      IntPtr num = vote_details == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailSpaceWorkVoteDetail__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetVoteDetail(swigCPtr_, num);
      }
      finally
      {
        if (vote_details != null)
          RailConverter.Cpp2Csharp(num, vote_details);
        RAIL_API_PINVOKE.delete_RailArrayRailSpaceWorkVoteDetail(num);
      }
    }

    public virtual RailResult GetUploaderIDs(List<RailID> uploader_ids)
    {
      IntPtr num = uploader_ids == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetUploaderIDs(swigCPtr_, num);
      }
      finally
      {
        if (uploader_ids != null)
          RailConverter.Cpp2Csharp(num, uploader_ids);
        RAIL_API_PINVOKE.delete_RailArrayRailID(num);
      }
    }

    public virtual RailResult SetUpdateOptions(RailSpaceWorkUpdateOptions options)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkUpdateOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_SetUpdateOptions(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSpaceWorkUpdateOptions(num);
      }
    }

    public virtual RailResult GetStatistic(EnumRailSpaceWorkStatistic stat_type, out ulong value) => (RailResult) RAIL_API_PINVOKE.IRailSpaceWork_GetStatistic(swigCPtr_, (int) stat_type, out value);

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
