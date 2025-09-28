using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SendEvents : MonoBehaviourPunCallbacks
{
    public const byte UpdateCurrentRaidLocationEventCode = 1;
    public const byte UpdateScoreEventCode = 2;
    public const byte FinishedStageEventCode = 3;
    public const byte FinishedRaidEventCode = 4;

    public void UpdateCurrentRaidLocationEvent(string currentRaidLocation)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(UpdateCurrentRaidLocationEventCode, currentRaidLocation, raiseEventOptions, SendOptions.SendReliable);
    }

    public void UpdateScoreEvent(int score, Player player)
    {
        object[] data = { score, player };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(UpdateCurrentRaidLocationEventCode, data, raiseEventOptions, SendOptions.SendReliable);
    }

    public void FinishedStageEvent()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(UpdateCurrentRaidLocationEventCode, PhotonNetwork.LocalPlayer.ActorNumber, raiseEventOptions, SendOptions.SendReliable);
    }

    public void FinishedRaidEvent()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(UpdateCurrentRaidLocationEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }
}
