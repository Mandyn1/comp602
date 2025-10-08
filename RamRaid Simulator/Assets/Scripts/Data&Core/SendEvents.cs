using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SendEvents : MonoBehaviourPunCallbacks
{
    public const byte UpdateCurrentRaidLocationEventCode = 1;
    public const byte UpdateScoreEventCode = 2;
    public const byte NextRoundEventCode = 3;
    public const byte SendStartingPositionsEventCode = 4;
    public const byte PlayerNowWaitingEventCode = 5;

    public void UpdateCurrentRaidLocationEvent(string currentRaidLocation)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(UpdateCurrentRaidLocationEventCode, currentRaidLocation, raiseEventOptions, SendOptions.SendReliable);
    }

    public void UpdateScoreEvent(int score, Player player)
    {
        object[] data = { score, player };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(UpdateScoreEventCode, data, raiseEventOptions, SendOptions.SendReliable);
    }

    public void NextRoundEvent(bool swapOrEnd)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(NextRoundEventCode, swapOrEnd, raiseEventOptions, SendOptions.SendReliable);
    }

    public void SendStartingPositionsEvent(int playerNumber, string position)
    {
        object[] data = { playerNumber, position };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(SendStartingPositionsEventCode, data, raiseEventOptions, SendOptions.SendReliable);
    }

    public void PlayerNowWaitingEvent()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(PlayerNowWaitingEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }
}
