using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SendEvents : MonoBehaviourPunCallbacks
{
    public const byte UpdateCurrentRaidLocationEventCode = 1;
    public const byte UpdateScoreEventCode = 2;

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
}
