using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Data.Common;
using System;
using System.Collections.Generic;

public class SendEvents : MonoBehaviourPunCallbacks
{
    // Identifying codes for events
    public const byte UpdateCurrentRaidLocationEventCode = 1;
    public const byte UpdateScoreEventCode = 2;
    public const byte NextRoundEventCode = 3;
    public const byte SendStartingPositionsEventCode = 4;
    public const byte PlayerNowWaitingEventCode = 5;
    public const byte SendStatDataEventCode = 6;

    // Syncing current raid location
    public void UpdateCurrentRaidLocationEvent(string currentRaidLocation)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(UpdateCurrentRaidLocationEventCode, currentRaidLocation, raiseEventOptions, SendOptions.SendReliable);
    }

    // Syncing new score for raider
    public void UpdateScoreEvent(int score, Player player)
    {
        object[] data = { score, player };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(UpdateScoreEventCode, data, raiseEventOptions, SendOptions.SendReliable);
    }

    // Sync end of round logic
    public void NextRoundEvent(bool swapOrEnd)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(NextRoundEventCode, swapOrEnd, raiseEventOptions, SendOptions.SendReliable);
    }

    // Sync starting positions
    public void SendStartingPositionsEvent(int playerNumber, string position)
    {
        object[] data = { playerNumber, position };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(SendStartingPositionsEventCode, data, raiseEventOptions, SendOptions.SendReliable);
    }

    // Inform a user has completed their side of a stage
    public void PlayerNowWaitingEvent()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(PlayerNowWaitingEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    // Sync stats data, requires calling once from each user
    public void SendStatDataEvent(Dictionary<String, object> data)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(SendStatDataEventCode, data, raiseEventOptions, SendOptions.SendReliable);
    }
}
