// Decompiled with JetBrains decompiler
// Type: rail.IRailUserSpaceHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailUserSpaceHelper
  {
    RailResult AsyncGetMySubscribedWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type,
      RailQueryWorkFileOptions options,
      string user_data);

    RailResult AsyncGetMySubscribedWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type,
      RailQueryWorkFileOptions options);

    RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type);

    RailResult AsyncGetMyFavoritesWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type,
      RailQueryWorkFileOptions options,
      string user_data);

    RailResult AsyncGetMyFavoritesWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type,
      RailQueryWorkFileOptions options);

    RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type);

    RailResult AsyncQuerySpaceWorks(
      RailSpaceWorkFilter filter,
      uint offset,
      uint max_works,
      EnumRailSpaceWorkOrderBy order_by,
      RailQueryWorkFileOptions options,
      string user_data);

    RailResult AsyncQuerySpaceWorks(
      RailSpaceWorkFilter filter,
      uint offset,
      uint max_works,
      EnumRailSpaceWorkOrderBy order_by,
      RailQueryWorkFileOptions options);

    RailResult AsyncQuerySpaceWorks(
      RailSpaceWorkFilter filter,
      uint offset,
      uint max_works,
      EnumRailSpaceWorkOrderBy order_by);

    RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works);

    RailResult AsyncSubscribeSpaceWorks(List<SpaceWorkID> ids, bool subscribe, string user_data);

    IRailSpaceWork OpenSpaceWork(SpaceWorkID id);

    IRailSpaceWork CreateSpaceWork(EnumRailSpaceWorkType type);

    RailResult GetMySubscribedWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type,
      QueryMySubscribedSpaceWorksResult result);

    uint GetMySubscribedWorksCount(EnumRailSpaceWorkType type, out int result);

    RailResult AsyncRemoveSpaceWork(SpaceWorkID id, string user_data);

    RailResult AsyncModifyFavoritesWorks(
      List<SpaceWorkID> ids,
      EnumRailModifyFavoritesSpaceWorkType modify_flag,
      string user_data);

    RailResult AsyncVoteSpaceWork(
      SpaceWorkID id,
      EnumRailSpaceWorkVoteValue vote,
      string user_data);

    RailResult AsyncSearchSpaceWork(
      RailSpaceWorkSearchFilter filter,
      RailQueryWorkFileOptions options,
      List<EnumRailSpaceWorkType> types,
      uint offset,
      uint max_works,
      string user_data);
  }
}
