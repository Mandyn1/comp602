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
    public const byte StartGameEventCode = 5;

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

    public void FinishedStageEvent()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(FinishedStageEventCode, PhotonNetwork.LocalPlayer.ActorNumber, raiseEventOptions, SendOptions.SendReliable);
    }

    public void FinishedRaidEvent()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(FinishedRaidEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void StartGame()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(StartGameEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }
}
