// Decompiled with JetBrains decompiler
// Type: rail.RailCallBackHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class RailCallBackHelper
  {
    private static readonly object locker_ = new object();
    private static Dictionary<RAILEventID, RailEventCallBackHandler> eventHandlers_ = new Dictionary<RAILEventID, RailEventCallBackHandler>();
    private static RailEventCallBackFunction delegate_ = OnRailCallBack;

    ~RailCallBackHelper() => UnregisterAllCallback();

    public void RegisterCallback(RAILEventID event_id, RailEventCallBackHandler handler)
    {
      lock (locker_)
      {
        if (eventHandlers_.ContainsKey(event_id))
        {
          eventHandlers_[event_id] += handler;
        }
        else
        {
          eventHandlers_.Add(event_id, handler);
          rail_api.CSharpRailRegisterEvent(event_id, delegate_);
        }
      }
    }

    public void UnregisterCallback(RAILEventID event_id, RailEventCallBackHandler handler)
    {
      lock (locker_)
      {
        if (!eventHandlers_.ContainsKey(event_id))
          return;
        eventHandlers_[event_id] -= handler;
        if (eventHandlers_[event_id] != null)
          return;
        rail_api.CSharpRailUnRegisterEvent(event_id, delegate_);
        eventHandlers_.Remove(event_id);
      }
    }

    public void UnregisterCallback(RAILEventID event_id)
    {
      lock (locker_)
        rail_api.CSharpRailUnRegisterEvent(event_id, delegate_);
    }

    public void UnregisterAllCallback()
    {
      lock (locker_)
        rail_api.CSharpRailUnRegisterAllEvent();
    }

    public static void OnRailCallBack(RAILEventID event_id, IntPtr data)
    {
      RailEventCallBackHandler eventHandler = eventHandlers_[event_id];
      if (eventHandler == null)
        return;
      switch (event_id)
      {
        case RAILEventID.kRailEventFinalize:
          RailFinalize railFinalize = new RailFinalize();
          RailConverter.Cpp2Csharp(data, railFinalize);
          eventHandler(event_id, railFinalize);
          break;
        case RAILEventID.kRailEventSystemStateChanged:
          RailSystemStateChanged systemStateChanged = new RailSystemStateChanged();
          RailConverter.Cpp2Csharp(data, systemStateChanged);
          eventHandler(event_id, systemStateChanged);
          break;
        case RAILEventID.kRailPlatformNotifyEventJoinGameByGameServer:
          RailPlatformNotifyEventJoinGameByGameServer gameByGameServer = new RailPlatformNotifyEventJoinGameByGameServer();
          RailConverter.Cpp2Csharp(data, gameByGameServer);
          eventHandler(event_id, gameByGameServer);
          break;
        case RAILEventID.kRailPlatformNotifyEventJoinGameByRoom:
          RailPlatformNotifyEventJoinGameByRoom eventJoinGameByRoom = new RailPlatformNotifyEventJoinGameByRoom();
          RailConverter.Cpp2Csharp(data, eventJoinGameByRoom);
          eventHandler(event_id, eventJoinGameByRoom);
          break;
        case RAILEventID.kRailPlatformNotifyEventJoinGameByUser:
          RailPlatformNotifyEventJoinGameByUser eventJoinGameByUser = new RailPlatformNotifyEventJoinGameByUser();
          RailConverter.Cpp2Csharp(data, eventJoinGameByUser);
          eventHandler(event_id, eventJoinGameByUser);
          break;
        case RAILEventID.kRailEventStatsPlayerStatsReceived:
          PlayerStatsReceived playerStatsReceived = new PlayerStatsReceived();
          RailConverter.Cpp2Csharp(data, playerStatsReceived);
          eventHandler(event_id, playerStatsReceived);
          break;
        case RAILEventID.kRailEventStatsPlayerStatsStored:
          PlayerStatsStored playerStatsStored = new PlayerStatsStored();
          RailConverter.Cpp2Csharp(data, playerStatsStored);
          eventHandler(event_id, playerStatsStored);
          break;
        case RAILEventID.kRailEventStatsNumOfPlayerReceived:
          NumberOfPlayerReceived ofPlayerReceived = new NumberOfPlayerReceived();
          RailConverter.Cpp2Csharp(data, ofPlayerReceived);
          eventHandler(event_id, ofPlayerReceived);
          break;
        case RAILEventID.kRailEventStatsGlobalStatsReceived:
          GlobalStatsRequestReceived statsRequestReceived = new GlobalStatsRequestReceived();
          RailConverter.Cpp2Csharp(data, statsRequestReceived);
          eventHandler(event_id, statsRequestReceived);
          break;
        case RAILEventID.kRailEventAchievementPlayerAchievementReceived:
          PlayerAchievementReceived achievementReceived1 = new PlayerAchievementReceived();
          RailConverter.Cpp2Csharp(data, achievementReceived1);
          eventHandler(event_id, achievementReceived1);
          break;
        case RAILEventID.kRailEventAchievementPlayerAchievementStored:
          PlayerAchievementStored achievementStored = new PlayerAchievementStored();
          RailConverter.Cpp2Csharp(data, achievementStored);
          eventHandler(event_id, achievementStored);
          break;
        case RAILEventID.kRailEventAchievementGlobalAchievementReceived:
          GlobalAchievementReceived achievementReceived2 = new GlobalAchievementReceived();
          RailConverter.Cpp2Csharp(data, achievementReceived2);
          eventHandler(event_id, achievementReceived2);
          break;
        case RAILEventID.kRailEventLeaderboardReceived:
          LeaderboardReceived leaderboardReceived = new LeaderboardReceived();
          RailConverter.Cpp2Csharp(data, leaderboardReceived);
          eventHandler(event_id, leaderboardReceived);
          break;
        case RAILEventID.kRailEventLeaderboardEntryReceived:
          LeaderboardEntryReceived leaderboardEntryReceived = new LeaderboardEntryReceived();
          RailConverter.Cpp2Csharp(data, leaderboardEntryReceived);
          eventHandler(event_id, leaderboardEntryReceived);
          break;
        case RAILEventID.kRailEventLeaderboardUploaded:
          LeaderboardUploaded leaderboardUploaded = new LeaderboardUploaded();
          RailConverter.Cpp2Csharp(data, leaderboardUploaded);
          eventHandler(event_id, leaderboardUploaded);
          break;
        case RAILEventID.kRailEventLeaderboardAttachSpaceWork:
          LeaderboardAttachSpaceWork leaderboardAttachSpaceWork = new LeaderboardAttachSpaceWork();
          RailConverter.Cpp2Csharp(data, leaderboardAttachSpaceWork);
          eventHandler(event_id, leaderboardAttachSpaceWork);
          break;
        case RAILEventID.kRailEventGameServerListResult:
          GetGameServerListResult serverListResult1 = new GetGameServerListResult();
          RailConverter.Cpp2Csharp(data, serverListResult1);
          eventHandler(event_id, serverListResult1);
          break;
        case RAILEventID.kRailEventGameServerCreated:
          CreateGameServerResult gameServerResult1 = new CreateGameServerResult();
          RailConverter.Cpp2Csharp(data, gameServerResult1);
          eventHandler(event_id, gameServerResult1);
          break;
        case RAILEventID.kRailEventGameServerSetMetadataResult:
          SetGameServerMetadataResult serverMetadataResult1 = new SetGameServerMetadataResult();
          RailConverter.Cpp2Csharp(data, serverMetadataResult1);
          eventHandler(event_id, serverMetadataResult1);
          break;
        case RAILEventID.kRailEventGameServerGetMetadataResult:
          GetGameServerMetadataResult serverMetadataResult2 = new GetGameServerMetadataResult();
          RailConverter.Cpp2Csharp(data, serverMetadataResult2);
          eventHandler(event_id, serverMetadataResult2);
          break;
        case RAILEventID.kRailEventGameServerGetSessionTicket:
          AsyncAcquireGameServerSessionTicketResponse sessionTicketResponse1 = new AsyncAcquireGameServerSessionTicketResponse();
          RailConverter.Cpp2Csharp(data, sessionTicketResponse1);
          eventHandler(event_id, sessionTicketResponse1);
          break;
        case RAILEventID.kRailEventGameServerAuthSessionTicket:
          GameServerStartSessionWithPlayerResponse withPlayerResponse1 = new GameServerStartSessionWithPlayerResponse();
          RailConverter.Cpp2Csharp(data, withPlayerResponse1);
          eventHandler(event_id, withPlayerResponse1);
          break;
        case RAILEventID.kRailEventGameServerPlayerListResult:
          GetGameServerPlayerListResult playerListResult = new GetGameServerPlayerListResult();
          RailConverter.Cpp2Csharp(data, playerListResult);
          eventHandler(event_id, playerListResult);
          break;
        case RAILEventID.kRailEventGameServerRegisterToServerListResult:
          GameServerRegisterToServerListResult serverListResult2 = new GameServerRegisterToServerListResult();
          RailConverter.Cpp2Csharp(data, serverListResult2);
          eventHandler(event_id, serverListResult2);
          break;
        case RAILEventID.kRailEventGameServerFavoriteGameServers:
          AsyncGetFavoriteGameServersResult gameServersResult = new AsyncGetFavoriteGameServersResult();
          RailConverter.Cpp2Csharp(data, gameServersResult);
          eventHandler(event_id, gameServersResult);
          break;
        case RAILEventID.kRailEventGameServerAddFavoriteGameServer:
          AsyncAddFavoriteGameServerResult gameServerResult2 = new AsyncAddFavoriteGameServerResult();
          RailConverter.Cpp2Csharp(data, gameServerResult2);
          eventHandler(event_id, gameServerResult2);
          break;
        case RAILEventID.kRailEventGameServerRemoveFavoriteGameServer:
          AsyncRemoveFavoriteGameServerResult gameServerResult3 = new AsyncRemoveFavoriteGameServerResult();
          RailConverter.Cpp2Csharp(data, gameServerResult3);
          eventHandler(event_id, gameServerResult3);
          break;
        case RAILEventID.kRailEventUserSpaceGetMySubscribedWorksResult:
          AsyncGetMySubscribedWorksResult subscribedWorksResult = new AsyncGetMySubscribedWorksResult();
          RailConverter.Cpp2Csharp(data, subscribedWorksResult);
          eventHandler(event_id, subscribedWorksResult);
          break;
        case RAILEventID.kRailEventUserSpaceGetMyFavoritesWorksResult:
          AsyncGetMyFavoritesWorksResult favoritesWorksResult1 = new AsyncGetMyFavoritesWorksResult();
          RailConverter.Cpp2Csharp(data, favoritesWorksResult1);
          eventHandler(event_id, favoritesWorksResult1);
          break;
        case RAILEventID.kRailEventUserSpaceQuerySpaceWorksResult:
          AsyncQuerySpaceWorksResult spaceWorksResult1 = new AsyncQuerySpaceWorksResult();
          RailConverter.Cpp2Csharp(data, spaceWorksResult1);
          eventHandler(event_id, spaceWorksResult1);
          break;
        case RAILEventID.kRailEventUserSpaceUpdateMetadataResult:
          AsyncUpdateMetadataResult updateMetadataResult = new AsyncUpdateMetadataResult();
          RailConverter.Cpp2Csharp(data, updateMetadataResult);
          eventHandler(event_id, updateMetadataResult);
          break;
        case RAILEventID.kRailEventUserSpaceSyncResult:
          SyncSpaceWorkResult syncSpaceWorkResult = new SyncSpaceWorkResult();
          RailConverter.Cpp2Csharp(data, syncSpaceWorkResult);
          eventHandler(event_id, syncSpaceWorkResult);
          break;
        case RAILEventID.kRailEventUserSpaceSubscribeResult:
          AsyncSubscribeSpaceWorksResult spaceWorksResult2 = new AsyncSubscribeSpaceWorksResult();
          RailConverter.Cpp2Csharp(data, spaceWorksResult2);
          eventHandler(event_id, spaceWorksResult2);
          break;
        case RAILEventID.kRailEventUserSpaceModifyFavoritesWorksResult:
          AsyncModifyFavoritesWorksResult favoritesWorksResult2 = new AsyncModifyFavoritesWorksResult();
          RailConverter.Cpp2Csharp(data, favoritesWorksResult2);
          eventHandler(event_id, favoritesWorksResult2);
          break;
        case RAILEventID.kRailEventUserSpaceRemoveSpaceWorkResult:
          AsyncRemoveSpaceWorkResult removeSpaceWorkResult = new AsyncRemoveSpaceWorkResult();
          RailConverter.Cpp2Csharp(data, removeSpaceWorkResult);
          eventHandler(event_id, removeSpaceWorkResult);
          break;
        case RAILEventID.kRailEventUserSpaceVoteSpaceWorkResult:
          AsyncVoteSpaceWorkResult voteSpaceWorkResult = new AsyncVoteSpaceWorkResult();
          RailConverter.Cpp2Csharp(data, voteSpaceWorkResult);
          eventHandler(event_id, voteSpaceWorkResult);
          break;
        case RAILEventID.kRailEventUserSpaceSearchSpaceWorkResult:
          AsyncSearchSpaceWorksResult spaceWorksResult3 = new AsyncSearchSpaceWorksResult();
          RailConverter.Cpp2Csharp(data, spaceWorksResult3);
          eventHandler(event_id, spaceWorksResult3);
          break;
        case RAILEventID.kRailEventNetChannelCreateChannelResult:
          CreateChannelResult createChannelResult = new CreateChannelResult();
          RailConverter.Cpp2Csharp(data, createChannelResult);
          eventHandler(event_id, createChannelResult);
          break;
        case RAILEventID.kRailEventNetChannelInviteJoinChannelRequest:
          InviteJoinChannelRequest joinChannelRequest = new InviteJoinChannelRequest();
          RailConverter.Cpp2Csharp(data, joinChannelRequest);
          eventHandler(event_id, joinChannelRequest);
          break;
        case RAILEventID.kRailEventNetChannelJoinChannelResult:
          JoinChannelResult joinChannelResult = new JoinChannelResult();
          RailConverter.Cpp2Csharp(data, joinChannelResult);
          eventHandler(event_id, joinChannelResult);
          break;
        case RAILEventID.kRailEventNetChannelChannelException:
          ChannelException channelException = new ChannelException();
          RailConverter.Cpp2Csharp(data, channelException);
          eventHandler(event_id, channelException);
          break;
        case RAILEventID.kRailEventNetChannelChannelNetDelay:
          ChannelNetDelay channelNetDelay = new ChannelNetDelay();
          RailConverter.Cpp2Csharp(data, channelNetDelay);
          eventHandler(event_id, channelNetDelay);
          break;
        case RAILEventID.kRailEventNetChannelInviteMemmberResult:
          InviteMemmberResult inviteMemmberResult = new InviteMemmberResult();
          RailConverter.Cpp2Csharp(data, inviteMemmberResult);
          eventHandler(event_id, inviteMemmberResult);
          break;
        case RAILEventID.kRailEventNetChannelMemberStateChanged:
          ChannelMemberStateChanged memberStateChanged = new ChannelMemberStateChanged();
          RailConverter.Cpp2Csharp(data, memberStateChanged);
          eventHandler(event_id, memberStateChanged);
          break;
        case RAILEventID.kRailEventStorageQueryQuotaResult:
          AsyncQueryQuotaResult queryQuotaResult = new AsyncQueryQuotaResult();
          RailConverter.Cpp2Csharp(data, queryQuotaResult);
          eventHandler(event_id, queryQuotaResult);
          break;
        case RAILEventID.kRailEventStorageShareToSpaceWorkResult:
          ShareStorageToSpaceWorkResult toSpaceWorkResult = new ShareStorageToSpaceWorkResult();
          RailConverter.Cpp2Csharp(data, toSpaceWorkResult);
          eventHandler(event_id, toSpaceWorkResult);
          break;
        case RAILEventID.kRailEventStorageAsyncReadFileResult:
          AsyncReadFileResult asyncReadFileResult = new AsyncReadFileResult();
          RailConverter.Cpp2Csharp(data, asyncReadFileResult);
          eventHandler(event_id, asyncReadFileResult);
          break;
        case RAILEventID.kRailEventStorageAsyncWriteFileResult:
          AsyncWriteFileResult asyncWriteFileResult = new AsyncWriteFileResult();
          RailConverter.Cpp2Csharp(data, asyncWriteFileResult);
          eventHandler(event_id, asyncWriteFileResult);
          break;
        case RAILEventID.kRailEventStorageAsyncListStreamFileResult:
          AsyncListFileResult asyncListFileResult = new AsyncListFileResult();
          RailConverter.Cpp2Csharp(data, asyncListFileResult);
          eventHandler(event_id, asyncListFileResult);
          break;
        case RAILEventID.kRailEventStorageAsyncRenameStreamFileResult:
          AsyncRenameStreamFileResult streamFileResult1 = new AsyncRenameStreamFileResult();
          RailConverter.Cpp2Csharp(data, streamFileResult1);
          eventHandler(event_id, streamFileResult1);
          break;
        case RAILEventID.kRailEventStorageAsyncDeleteStreamFileResult:
          AsyncDeleteStreamFileResult streamFileResult2 = new AsyncDeleteStreamFileResult();
          RailConverter.Cpp2Csharp(data, streamFileResult2);
          eventHandler(event_id, streamFileResult2);
          break;
        case RAILEventID.kRailEventStorageAsyncReadStreamFileResult:
          AsyncReadStreamFileResult streamFileResult3 = new AsyncReadStreamFileResult();
          RailConverter.Cpp2Csharp(data, streamFileResult3);
          eventHandler(event_id, streamFileResult3);
          break;
        case RAILEventID.kRailEventStorageAsyncWriteStreamFileResult:
          AsyncWriteStreamFileResult streamFileResult4 = new AsyncWriteStreamFileResult();
          RailConverter.Cpp2Csharp(data, streamFileResult4);
          eventHandler(event_id, streamFileResult4);
          break;
        case RAILEventID.kRailEventAssetsRequestAllAssetsFinished:
          RequestAllAssetsFinished allAssetsFinished = new RequestAllAssetsFinished();
          RailConverter.Cpp2Csharp(data, allAssetsFinished);
          eventHandler(event_id, allAssetsFinished);
          break;
        case RAILEventID.kRailEventAssetsCompleteConsumeByExchangeAssetsToFinished:
          CompleteConsumeByExchangeAssetsToFinished assetsToFinished1 = new CompleteConsumeByExchangeAssetsToFinished();
          RailConverter.Cpp2Csharp(data, assetsToFinished1);
          eventHandler(event_id, assetsToFinished1);
          break;
        case RAILEventID.kRailEventAssetsExchangeAssetsFinished:
          ExchangeAssetsFinished exchangeAssetsFinished = new ExchangeAssetsFinished();
          RailConverter.Cpp2Csharp(data, exchangeAssetsFinished);
          eventHandler(event_id, exchangeAssetsFinished);
          break;
        case RAILEventID.kRailEventAssetsExchangeAssetsToFinished:
          ExchangeAssetsToFinished assetsToFinished2 = new ExchangeAssetsToFinished();
          RailConverter.Cpp2Csharp(data, assetsToFinished2);
          eventHandler(event_id, assetsToFinished2);
          break;
        case RAILEventID.kRailEventAssetsDirectConsumeFinished:
          DirectConsumeAssetsFinished consumeAssetsFinished1 = new DirectConsumeAssetsFinished();
          RailConverter.Cpp2Csharp(data, consumeAssetsFinished1);
          eventHandler(event_id, consumeAssetsFinished1);
          break;
        case RAILEventID.kRailEventAssetsStartConsumeFinished:
          StartConsumeAssetsFinished consumeAssetsFinished2 = new StartConsumeAssetsFinished();
          RailConverter.Cpp2Csharp(data, consumeAssetsFinished2);
          eventHandler(event_id, consumeAssetsFinished2);
          break;
        case RAILEventID.kRailEventAssetsUpdateConsumeFinished:
          UpdateConsumeAssetsFinished consumeAssetsFinished3 = new UpdateConsumeAssetsFinished();
          RailConverter.Cpp2Csharp(data, consumeAssetsFinished3);
          eventHandler(event_id, consumeAssetsFinished3);
          break;
        case RAILEventID.kRailEventAssetsCompleteConsumeFinished:
          CompleteConsumeAssetsFinished consumeAssetsFinished4 = new CompleteConsumeAssetsFinished();
          RailConverter.Cpp2Csharp(data, consumeAssetsFinished4);
          eventHandler(event_id, consumeAssetsFinished4);
          break;
        case RAILEventID.kRailEventAssetsSplitFinished:
          SplitAssetsFinished splitAssetsFinished = new SplitAssetsFinished();
          RailConverter.Cpp2Csharp(data, splitAssetsFinished);
          eventHandler(event_id, splitAssetsFinished);
          break;
        case RAILEventID.kRailEventAssetsSplitToFinished:
          SplitAssetsToFinished assetsToFinished3 = new SplitAssetsToFinished();
          RailConverter.Cpp2Csharp(data, assetsToFinished3);
          eventHandler(event_id, assetsToFinished3);
          break;
        case RAILEventID.kRailEventAssetsMergeFinished:
          MergeAssetsFinished mergeAssetsFinished = new MergeAssetsFinished();
          RailConverter.Cpp2Csharp(data, mergeAssetsFinished);
          eventHandler(event_id, mergeAssetsFinished);
          break;
        case RAILEventID.kRailEventAssetsMergeToFinished:
          MergeAssetsToFinished assetsToFinished4 = new MergeAssetsToFinished();
          RailConverter.Cpp2Csharp(data, assetsToFinished4);
          eventHandler(event_id, assetsToFinished4);
          break;
        case RAILEventID.kRailEventAssetsUpdateAssetPropertyFinished:
          UpdateAssetsPropertyFinished propertyFinished = new UpdateAssetsPropertyFinished();
          RailConverter.Cpp2Csharp(data, propertyFinished);
          eventHandler(event_id, propertyFinished);
          break;
        case RAILEventID.kRailEventUtilsGetImageDataResult:
          RailGetImageDataResult getImageDataResult = new RailGetImageDataResult();
          RailConverter.Cpp2Csharp(data, getImageDataResult);
          eventHandler(event_id, getImageDataResult);
          break;
        case RAILEventID.kRailEventInGamePurchaseAllProductsInfoReceived:
          RailInGamePurchaseRequestAllProductsResponse productsResponse1 = new RailInGamePurchaseRequestAllProductsResponse();
          RailConverter.Cpp2Csharp(data, productsResponse1);
          eventHandler(event_id, productsResponse1);
          break;
        case RAILEventID.kRailEventInGamePurchaseAllPurchasableProductsInfoReceived:
          RailInGamePurchaseRequestAllPurchasableProductsResponse productsResponse2 = new RailInGamePurchaseRequestAllPurchasableProductsResponse();
          RailConverter.Cpp2Csharp(data, productsResponse2);
          eventHandler(event_id, productsResponse2);
          break;
        case RAILEventID.kRailEventInGamePurchasePurchaseProductsResult:
          RailInGamePurchasePurchaseProductsResponse productsResponse3 = new RailInGamePurchasePurchaseProductsResponse();
          RailConverter.Cpp2Csharp(data, productsResponse3);
          eventHandler(event_id, productsResponse3);
          break;
        case RAILEventID.kRailEventInGamePurchaseFinishOrderResult:
          RailInGamePurchaseFinishOrderResponse finishOrderResponse = new RailInGamePurchaseFinishOrderResponse();
          RailConverter.Cpp2Csharp(data, finishOrderResponse);
          eventHandler(event_id, finishOrderResponse);
          break;
        case RAILEventID.kRailEventInGamePurchasePurchaseProductsToAssetsResult:
          RailInGamePurchasePurchaseProductsToAssetsResponse toAssetsResponse = new RailInGamePurchasePurchaseProductsToAssetsResponse();
          RailConverter.Cpp2Csharp(data, toAssetsResponse);
          eventHandler(event_id, toAssetsResponse);
          break;
        case RAILEventID.kRailEventInGameStorePurchasePayWindowDisplayed:
          RailInGameStorePurchasePayWindowDisplayed payWindowDisplayed = new RailInGameStorePurchasePayWindowDisplayed();
          RailConverter.Cpp2Csharp(data, payWindowDisplayed);
          eventHandler(event_id, payWindowDisplayed);
          break;
        case RAILEventID.kRailEventInGameStorePurchasePayWindowClosed:
          RailInGameStorePurchasePayWindowClosed purchasePayWindowClosed = new RailInGameStorePurchasePayWindowClosed();
          RailConverter.Cpp2Csharp(data, purchasePayWindowClosed);
          eventHandler(event_id, purchasePayWindowClosed);
          break;
        case RAILEventID.kRailEventInGameStorePurchasePaymentResult:
          RailInGameStorePurchaseResult storePurchaseResult = new RailInGameStorePurchaseResult();
          RailConverter.Cpp2Csharp(data, storePurchaseResult);
          eventHandler(event_id, storePurchaseResult);
          break;
        case RAILEventID.kRailEventRoomZoneListResult:
          ZoneInfoList zoneInfoList = new ZoneInfoList();
          RailConverter.Cpp2Csharp(data, zoneInfoList);
          eventHandler(event_id, zoneInfoList);
          break;
        case RAILEventID.kRailEventRoomListResult:
          RoomInfoList roomInfoList = new RoomInfoList();
          RailConverter.Cpp2Csharp(data, roomInfoList);
          eventHandler(event_id, roomInfoList);
          break;
        case RAILEventID.kRailEventRoomCreated:
          CreateRoomInfo createRoomInfo = new CreateRoomInfo();
          RailConverter.Cpp2Csharp(data, createRoomInfo);
          eventHandler(event_id, createRoomInfo);
          break;
        case RAILEventID.kRailEventRoomGotRoomMembers:
          RoomMembersInfo roomMembersInfo = new RoomMembersInfo();
          RailConverter.Cpp2Csharp(data, roomMembersInfo);
          eventHandler(event_id, roomMembersInfo);
          break;
        case RAILEventID.kRailEventRoomJoinRoomResult:
          JoinRoomInfo joinRoomInfo = new JoinRoomInfo();
          RailConverter.Cpp2Csharp(data, joinRoomInfo);
          eventHandler(event_id, joinRoomInfo);
          break;
        case RAILEventID.kRailEventRoomKickOffMemberResult:
          KickOffMemberInfo kickOffMemberInfo = new KickOffMemberInfo();
          RailConverter.Cpp2Csharp(data, kickOffMemberInfo);
          eventHandler(event_id, kickOffMemberInfo);
          break;
        case RAILEventID.kRailEventRoomSetRoomMetadataResult:
          SetRoomMetadataInfo roomMetadataInfo1 = new SetRoomMetadataInfo();
          RailConverter.Cpp2Csharp(data, roomMetadataInfo1);
          eventHandler(event_id, roomMetadataInfo1);
          break;
        case RAILEventID.kRailEventRoomGetRoomMetadataResult:
          GetRoomMetadataInfo roomMetadataInfo2 = new GetRoomMetadataInfo();
          RailConverter.Cpp2Csharp(data, roomMetadataInfo2);
          eventHandler(event_id, roomMetadataInfo2);
          break;
        case RAILEventID.kRailEventRoomGetMemberMetadataResult:
          GetMemberMetadataInfo memberMetadataInfo1 = new GetMemberMetadataInfo();
          RailConverter.Cpp2Csharp(data, memberMetadataInfo1);
          eventHandler(event_id, memberMetadataInfo1);
          break;
        case RAILEventID.kRailEventRoomSetMemberMetadataResult:
          SetMemberMetadataInfo memberMetadataInfo2 = new SetMemberMetadataInfo();
          RailConverter.Cpp2Csharp(data, memberMetadataInfo2);
          eventHandler(event_id, memberMetadataInfo2);
          break;
        case RAILEventID.kRailEventRoomLeaveRoomResult:
          LeaveRoomInfo leaveRoomInfo = new LeaveRoomInfo();
          RailConverter.Cpp2Csharp(data, leaveRoomInfo);
          eventHandler(event_id, leaveRoomInfo);
          break;
        case RAILEventID.kRailEventRoomGetAllDataResult:
          RoomAllData roomAllData = new RoomAllData();
          RailConverter.Cpp2Csharp(data, roomAllData);
          eventHandler(event_id, roomAllData);
          break;
        case RAILEventID.kRailEventRoomGetUserRoomListResult:
          UserRoomListInfo userRoomListInfo = new UserRoomListInfo();
          RailConverter.Cpp2Csharp(data, userRoomListInfo);
          eventHandler(event_id, userRoomListInfo);
          break;
        case RAILEventID.kRailEventRoomClearRoomMetadataResult:
          ClearRoomMetadataInfo roomMetadataInfo3 = new ClearRoomMetadataInfo();
          RailConverter.Cpp2Csharp(data, roomMetadataInfo3);
          eventHandler(event_id, roomMetadataInfo3);
          break;
        case RAILEventID.kRailEventRoomNotifyMetadataChanged:
          NotifyMetadataChange notifyMetadataChange = new NotifyMetadataChange();
          RailConverter.Cpp2Csharp(data, notifyMetadataChange);
          eventHandler(event_id, notifyMetadataChange);
          break;
        case RAILEventID.kRailEventRoomNotifyMemberChanged:
          NotifyRoomMemberChange roomMemberChange = new NotifyRoomMemberChange();
          RailConverter.Cpp2Csharp(data, roomMemberChange);
          eventHandler(event_id, roomMemberChange);
          break;
        case RAILEventID.kRailEventRoomNotifyMemberkicked:
          NotifyRoomMemberKicked roomMemberKicked = new NotifyRoomMemberKicked();
          RailConverter.Cpp2Csharp(data, roomMemberKicked);
          eventHandler(event_id, roomMemberKicked);
          break;
        case RAILEventID.kRailEventRoomNotifyRoomDestroyed:
          NotifyRoomDestroy notifyRoomDestroy = new NotifyRoomDestroy();
          RailConverter.Cpp2Csharp(data, notifyRoomDestroy);
          eventHandler(event_id, notifyRoomDestroy);
          break;
        case RAILEventID.kRailEventRoomNotifyRoomOwnerChanged:
          NotifyRoomOwnerChange notifyRoomOwnerChange = new NotifyRoomOwnerChange();
          RailConverter.Cpp2Csharp(data, notifyRoomOwnerChange);
          eventHandler(event_id, notifyRoomOwnerChange);
          break;
        case RAILEventID.kRailEventRoomNotifyRoomDataReceived:
          RoomDataReceived roomDataReceived = new RoomDataReceived();
          RailConverter.Cpp2Csharp(data, roomDataReceived);
          eventHandler(event_id, roomDataReceived);
          break;
        case RAILEventID.kRailEventRoomNotifyRoomGameServerChanged:
          NotifyRoomGameServerChange gameServerChange = new NotifyRoomGameServerChange();
          RailConverter.Cpp2Csharp(data, gameServerChange);
          eventHandler(event_id, gameServerChange);
          break;
        case RAILEventID.kRailEventFriendsSetMetadataResult:
          RailFriendsSetMetadataResult setMetadataResult = new RailFriendsSetMetadataResult();
          RailConverter.Cpp2Csharp(data, setMetadataResult);
          eventHandler(event_id, setMetadataResult);
          break;
        case RAILEventID.kRailEventFriendsGetMetadataResult:
          RailFriendsGetMetadataResult getMetadataResult = new RailFriendsGetMetadataResult();
          RailConverter.Cpp2Csharp(data, getMetadataResult);
          eventHandler(event_id, getMetadataResult);
          break;
        case RAILEventID.kRailEventFriendsClearMetadataResult:
          RailFriendsClearMetadataResult clearMetadataResult = new RailFriendsClearMetadataResult();
          RailConverter.Cpp2Csharp(data, clearMetadataResult);
          eventHandler(event_id, clearMetadataResult);
          break;
        case RAILEventID.kRailEventFriendsGetInviteCommandLine:
          RailFriendsGetInviteCommandLine inviteCommandLine = new RailFriendsGetInviteCommandLine();
          RailConverter.Cpp2Csharp(data, inviteCommandLine);
          eventHandler(event_id, inviteCommandLine);
          break;
        case RAILEventID.kRailEventFriendsReportPlayedWithUserListResult:
          RailFriendsReportPlayedWithUserListResult withUserListResult = new RailFriendsReportPlayedWithUserListResult();
          RailConverter.Cpp2Csharp(data, withUserListResult);
          eventHandler(event_id, withUserListResult);
          break;
        case RAILEventID.kRailEventFriendsNotifyBuddyListChanged:
          RailFriendsBuddyListChanged buddyListChanged = new RailFriendsBuddyListChanged();
          RailConverter.Cpp2Csharp(data, buddyListChanged);
          eventHandler(event_id, buddyListChanged);
          break;
        case RAILEventID.kRailEventSessionTicketGetSessionTicket:
          AcquireSessionTicketResponse sessionTicketResponse2 = new AcquireSessionTicketResponse();
          RailConverter.Cpp2Csharp(data, sessionTicketResponse2);
          eventHandler(event_id, sessionTicketResponse2);
          break;
        case RAILEventID.kRailEventSessionTicketAuthSessionTicket:
          StartSessionWithPlayerResponse withPlayerResponse2 = new StartSessionWithPlayerResponse();
          RailConverter.Cpp2Csharp(data, withPlayerResponse2);
          eventHandler(event_id, withPlayerResponse2);
          break;
        case RAILEventID.kRailEventPlayerGetGamePurchaseKey:
          PlayerGetGamePurchaseKeyResult purchaseKeyResult = new PlayerGetGamePurchaseKeyResult();
          RailConverter.Cpp2Csharp(data, purchaseKeyResult);
          eventHandler(event_id, purchaseKeyResult);
          break;
        case RAILEventID.kRailEventUsersGetUsersInfo:
          RailUsersInfoData railUsersInfoData = new RailUsersInfoData();
          RailConverter.Cpp2Csharp(data, railUsersInfoData);
          eventHandler(event_id, railUsersInfoData);
          break;
        case RAILEventID.kRailEventUsersNotifyInviter:
          RailUsersNotifyInviter usersNotifyInviter = new RailUsersNotifyInviter();
          RailConverter.Cpp2Csharp(data, usersNotifyInviter);
          eventHandler(event_id, usersNotifyInviter);
          break;
        case RAILEventID.kRailEventUsersRespondInvation:
          RailUsersRespondInvation usersRespondInvation = new RailUsersRespondInvation();
          RailConverter.Cpp2Csharp(data, usersRespondInvation);
          eventHandler(event_id, usersRespondInvation);
          break;
        case RAILEventID.kRailEventUsersInviteJoinGameResult:
          RailUsersInviteJoinGameResult inviteJoinGameResult = new RailUsersInviteJoinGameResult();
          RailConverter.Cpp2Csharp(data, inviteJoinGameResult);
          eventHandler(event_id, inviteJoinGameResult);
          break;
        case RAILEventID.kRailEventUsersInviteUsersResult:
          RailUsersInviteUsersResult inviteUsersResult = new RailUsersInviteUsersResult();
          RailConverter.Cpp2Csharp(data, inviteUsersResult);
          eventHandler(event_id, inviteUsersResult);
          break;
        case RAILEventID.kRailEventUsersGetInviteDetailResult:
          RailUsersGetInviteDetailResult inviteDetailResult = new RailUsersGetInviteDetailResult();
          RailConverter.Cpp2Csharp(data, inviteDetailResult);
          eventHandler(event_id, inviteDetailResult);
          break;
        case RAILEventID.kRailEventUsersCancelInviteResult:
          RailUsersCancelInviteResult cancelInviteResult = new RailUsersCancelInviteResult();
          RailConverter.Cpp2Csharp(data, cancelInviteResult);
          eventHandler(event_id, cancelInviteResult);
          break;
        case RAILEventID.kRailEventShowFloatingWindow:
          ShowFloatingWindowResult floatingWindowResult1 = new ShowFloatingWindowResult();
          RailConverter.Cpp2Csharp(data, floatingWindowResult1);
          eventHandler(event_id, floatingWindowResult1);
          break;
        case RAILEventID.kRailEventShowFloatingNotifyWindow:
          ShowNotifyFloatingWindowResult floatingWindowResult2 = new ShowNotifyFloatingWindowResult();
          RailConverter.Cpp2Csharp(data, floatingWindowResult2);
          eventHandler(event_id, floatingWindowResult2);
          break;
        case RAILEventID.kRailEventBrowserCreateResult:
          CreateBrowserResult createBrowserResult = new CreateBrowserResult();
          RailConverter.Cpp2Csharp(data, createBrowserResult);
          eventHandler(event_id, createBrowserResult);
          break;
        case RAILEventID.kRailEventBrowserReloadResult:
          ReloadBrowserResult reloadBrowserResult = new ReloadBrowserResult();
          RailConverter.Cpp2Csharp(data, reloadBrowserResult);
          eventHandler(event_id, reloadBrowserResult);
          break;
        case RAILEventID.kRailEventBrowserCloseResult:
          CloseBrowserResult closeBrowserResult = new CloseBrowserResult();
          RailConverter.Cpp2Csharp(data, closeBrowserResult);
          eventHandler(event_id, closeBrowserResult);
          break;
        case RAILEventID.kRailEventBrowserJavascriptEvent:
          JavascriptEventResult javascriptEventResult = new JavascriptEventResult();
          RailConverter.Cpp2Csharp(data, javascriptEventResult);
          eventHandler(event_id, javascriptEventResult);
          break;
        case RAILEventID.kRailEventBrowserTryNavigateNewPageRequest:
          BrowserTryNavigateNewPageRequest navigateNewPageRequest = new BrowserTryNavigateNewPageRequest();
          RailConverter.Cpp2Csharp(data, navigateNewPageRequest);
          eventHandler(event_id, navigateNewPageRequest);
          break;
        case RAILEventID.kRailEventBrowserPaint:
          BrowserNeedsPaintRequest needsPaintRequest1 = new BrowserNeedsPaintRequest();
          RailConverter.Cpp2Csharp(data, needsPaintRequest1);
          eventHandler(event_id, needsPaintRequest1);
          break;
        case RAILEventID.kRailEventBrowserDamageRectPaint:
          BrowserDamageRectNeedsPaintRequest needsPaintRequest2 = new BrowserDamageRectNeedsPaintRequest();
          RailConverter.Cpp2Csharp(data, needsPaintRequest2);
          eventHandler(event_id, needsPaintRequest2);
          break;
        case RAILEventID.kRailEventBrowserNavigeteResult:
          BrowserRenderNavigateResult renderNavigateResult = new BrowserRenderNavigateResult();
          RailConverter.Cpp2Csharp(data, renderNavigateResult);
          eventHandler(event_id, renderNavigateResult);
          break;
        case RAILEventID.kRailEventBrowserStateChanged:
          BrowserRenderStateChanged renderStateChanged = new BrowserRenderStateChanged();
          RailConverter.Cpp2Csharp(data, renderStateChanged);
          eventHandler(event_id, renderStateChanged);
          break;
        case RAILEventID.kRailEventBrowserTitleChanged:
          BrowserRenderTitleChanged renderTitleChanged = new BrowserRenderTitleChanged();
          RailConverter.Cpp2Csharp(data, renderTitleChanged);
          eventHandler(event_id, renderTitleChanged);
          break;
        case RAILEventID.kRailEventNetworkCreateSessionRequest:
          CreateSessionRequest createSessionRequest = new CreateSessionRequest();
          RailConverter.Cpp2Csharp(data, createSessionRequest);
          eventHandler(event_id, createSessionRequest);
          break;
        case RAILEventID.kRailEventNetworkCreateSessionFailed:
          CreateSessionFailed createSessionFailed = new CreateSessionFailed();
          RailConverter.Cpp2Csharp(data, createSessionFailed);
          eventHandler(event_id, createSessionFailed);
          break;
        case RAILEventID.kRailEventDlcInstallStart:
          DlcInstallStart dlcInstallStart = new DlcInstallStart();
          RailConverter.Cpp2Csharp(data, dlcInstallStart);
          eventHandler(event_id, dlcInstallStart);
          break;
        case RAILEventID.kRailEventDlcInstallStartResult:
          DlcInstallStartResult installStartResult = new DlcInstallStartResult();
          RailConverter.Cpp2Csharp(data, installStartResult);
          eventHandler(event_id, installStartResult);
          break;
        case RAILEventID.kRailEventDlcInstallProgress:
          DlcInstallProgress dlcInstallProgress = new DlcInstallProgress();
          RailConverter.Cpp2Csharp(data, dlcInstallProgress);
          eventHandler(event_id, dlcInstallProgress);
          break;
        case RAILEventID.kRailEventDlcInstallFinished:
          DlcInstallFinished dlcInstallFinished = new DlcInstallFinished();
          RailConverter.Cpp2Csharp(data, dlcInstallFinished);
          eventHandler(event_id, dlcInstallFinished);
          break;
        case RAILEventID.kRailEventDlcUninstallFinished:
          DlcUninstallFinished uninstallFinished = new DlcUninstallFinished();
          RailConverter.Cpp2Csharp(data, uninstallFinished);
          eventHandler(event_id, uninstallFinished);
          break;
        case RAILEventID.kRailEventDlcCheckAllDlcsStateReadyResult:
          CheckAllDlcsStateReadyResult stateReadyResult = new CheckAllDlcsStateReadyResult();
          RailConverter.Cpp2Csharp(data, stateReadyResult);
          eventHandler(event_id, stateReadyResult);
          break;
        case RAILEventID.kRailEventDlcQueryIsOwnedDlcsResult:
          QueryIsOwnedDlcsResult isOwnedDlcsResult = new QueryIsOwnedDlcsResult();
          RailConverter.Cpp2Csharp(data, isOwnedDlcsResult);
          eventHandler(event_id, isOwnedDlcsResult);
          break;
        case RAILEventID.kRailEventDlcOwnershipChanged:
          DlcOwnershipChanged ownershipChanged = new DlcOwnershipChanged();
          RailConverter.Cpp2Csharp(data, ownershipChanged);
          eventHandler(event_id, ownershipChanged);
          break;
        case RAILEventID.kRailEventDlcRefundChanged:
          DlcRefundChanged dlcRefundChanged = new DlcRefundChanged();
          RailConverter.Cpp2Csharp(data, dlcRefundChanged);
          eventHandler(event_id, dlcRefundChanged);
          break;
        case RAILEventID.kRailEventScreenshotTakeScreenshotFinished:
          TakeScreenshotResult screenshotResult1 = new TakeScreenshotResult();
          RailConverter.Cpp2Csharp(data, screenshotResult1);
          eventHandler(event_id, screenshotResult1);
          break;
        case RAILEventID.kRailEventScreenshotTakeScreenshotRequest:
          ScreenshotRequestInfo screenshotRequestInfo = new ScreenshotRequestInfo();
          RailConverter.Cpp2Csharp(data, screenshotRequestInfo);
          eventHandler(event_id, screenshotRequestInfo);
          break;
        case RAILEventID.kRailEventScreenshotPublishScreenshotFinished:
          PublishScreenshotResult screenshotResult2 = new PublishScreenshotResult();
          RailConverter.Cpp2Csharp(data, screenshotResult2);
          eventHandler(event_id, screenshotResult2);
          break;
        case RAILEventID.kRailEventVoiceChannelCreateResult:
          CreateVoiceChannelResult voiceChannelResult = new CreateVoiceChannelResult();
          RailConverter.Cpp2Csharp(data, voiceChannelResult);
          eventHandler(event_id, voiceChannelResult);
          break;
        case RAILEventID.kRailEventVoiceDataCaptured:
          VoiceDataCapturedEvent dataCapturedEvent = new VoiceDataCapturedEvent();
          RailConverter.Cpp2Csharp(data, dataCapturedEvent);
          eventHandler(event_id, dataCapturedEvent);
          break;
        case RAILEventID.kRailEventAppQuerySubscribeWishPlayStateResult:
          QuerySubscribeWishPlayStateResult wishPlayStateResult = new QuerySubscribeWishPlayStateResult();
          RailConverter.Cpp2Csharp(data, wishPlayStateResult);
          eventHandler(event_id, wishPlayStateResult);
          break;
      }
    }
  }
}
