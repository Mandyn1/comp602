using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Unity.VisualScripting;

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

        // Update Current Raid Location Event
        if (eventCode == SendEvents.UpdateCurrentRaidLocationEventCode)
        {
            int currentRaidLocation = (int)photonEvent.CustomData;
            gameObject.GetComponent<GameState>().currentRaidLocation = currentRaidLocation;
        }

        // Update Score Event
        else if (eventCode == SendEvents.UpdateScoreEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            int score = (int)data[0];
            Player player = (Player)data[1];

            if (gameObject.GetComponent<PlayerData>().player1.ActorNumber == player.ActorNumber)
            {
                gameObject.GetComponent<PlayerData>().player1Score += score;
            }
            else gameObject.GetComponent<PlayerData>().player2Score += score;
        }

        // Next Round Event
        else if (eventCode == SendEvents.NextRoundEventCode)
        {
            if ((bool)photonEvent.CustomData)
            {
                if (gameObject.GetComponent<GameState>().hasPlayerSwapped) gameObject.GetComponent<GameState>().EndGame();
                else
                {
                    gameObject.GetComponent<GameState>().PlayerSwap();
                }
                
                gameObject.GetComponent<GameState>().roundCounter++;
                gameObject.GetComponent<GameState>().Reset();
            }
        }
    }
}
