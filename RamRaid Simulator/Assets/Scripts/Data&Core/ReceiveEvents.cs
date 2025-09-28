using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ReceiveEvents : MonoBehaviour, IOnEventCallback
{
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == SendEvents.UpdateCurrentRaidLocationEventCode)
        {
            string currentRaidLocation = (string)photonEvent.CustomData;
            if (this.gameObject.name == "PlayerManager") this.gameObject.GetComponent<PlayerData>().currentRaidLocation = currentRaidLocation;
            else if (this.gameObject.name == "GameManager") this.gameObject.GetComponent<GameState>().stage2RaiderScene = currentRaidLocation;
        }
        else if (eventCode == SendEvents.UpdateScoreEventCode && this.gameObject.name == "PlayerManager")
        {
            object[] data = (object[])photonEvent.CustomData;
            int score = (int)data[0];
            Player player = (Player)data[1];

            if (this.gameObject.GetComponent<PlayerData>().player1.ActorNumber == player.ActorNumber) this.gameObject.GetComponent<PlayerData>().player1Score += score;
            else this.gameObject.GetComponent<PlayerData>().player2Score += score;
        }
        else if (eventCode == SendEvents.FinishedStageEventCode && this.gameObject.name == "GameManager")
        {
            int finishedPlayer = (int)photonEvent.CustomData;

            if (finishedPlayer == PhotonNetwork.LocalPlayer.ActorNumber) this.gameObject.GetComponent<GameState>().playerDone = true;
            else if (finishedPlayer != PhotonNetwork.LocalPlayer.ActorNumber) this.gameObject.GetComponent<GameState>().opponentDone = true;

            if (this.gameObject.GetComponent<GameState>().playerDone && this.gameObject.GetComponent<GameState>().opponentDone) this.gameObject.GetComponent<GameState>().ProgressGame();
        }
        else if (eventCode == SendEvents.FinishedRaidEventCode && this.gameObject.name == "GameManager")
        {
            this.gameObject.GetComponent<GameState>().ProgressGame();
        }
    }
}