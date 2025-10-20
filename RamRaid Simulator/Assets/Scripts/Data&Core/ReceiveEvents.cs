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
        var gs = gameObject.GetComponent<GameState>();

        // Update Current Raid Location Event
        if (eventCode == SendEvents.UpdateCurrentRaidLocationEventCode)
        {
            int currentRaidLocation;

            // handle both string ("3") and int (3) payloads safely
            if (photonEvent.CustomData is int i)
            {
                currentRaidLocation = i;
            }
            else if (photonEvent.CustomData is string s && int.TryParse(s, out var parsed))
            {
                currentRaidLocation = parsed;
            }
            else
            {
                Debug.LogWarning($"ReceiveEvents: unexpected currentRaidLocation type: {photonEvent.CustomData?.GetType().Name}");
                return;
            }

            gs.currentRaidLocation = currentRaidLocation;
            return;
        }

        // Update Score Event
        else if (eventCode == SendEvents.UpdateScoreEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            int score = (int)data[0];
            Player player = (Player)data[1];

            gs.playerData[player.ActorNumber].score += score;
        }

        // Next Round Event
        else if (eventCode == SendEvents.NextRoundEventCode)
        {
            if ((bool)photonEvent.CustomData)
            {
                if (gs.hasPlayerSwapped) gs.EndGame();
                else
                {
                    gs.PlayerSwap();
                }

                gs.roundCounter++;
                gs.Reset();
            }
        }

        // Send starting (player) positions (police or raider). Runs once per player
        else if (eventCode == SendEvents.SendStartingPositionsEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            gs.playerData[(int)data[0]].position = (string)data[1];
        }

        // Player waiting event, starts next phase if a player is already waiting
        else if (eventCode == SendEvents.PlayerNowWaitingEventCode)
        {
            if (!gs.playerWaiting) gs.playerWaiting = true;
            else
            {
                gs.playerWaiting = false;
                gs.ProgressGame();
            }
        }
    }
}
