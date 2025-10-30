using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Unity.VisualScripting;
using System.Collections.Generic;
using System;

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
            object[] data = (object[])photonEvent.CustomData;
            gs.currentRaidLocation = (int)data[0];
            gs.currentRaidModifier = (float)data[1];
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
            if (gs.roundCounter > gs.maxRounds)
            {
                if (gs.hasPlayerSwapped) gs.EndGame();
                else
                {
                    gs.roundCounter = 0;
                    gs.PlayerSwap();
                }
            }
            gs.Reset();
        }

        // Send starting (player) positions (police or raider). Runs once per player
        else if (eventCode == SendEvents.SendStartingPositionsEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            gs.playerData[(int)data[0]].position = (string)data[1];
            gs.playerPositionsSet++;
            if (gs.playerPositionsSet == 2) gs.ContinuePrep();
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

        // Recieves and deposits stat data for other player that is otherwise not collected over course of game play
        else if (eventCode == SendEvents.SendStatDataEventCode)
        {
            Dictionary<String, object> data = (Dictionary<String, object>)photonEvent.CustomData;

            foreach (int playerNumber in gs.playerData.Keys)
            {
                if (playerNumber != gs.localPlayerNumber)
                {
                    gs.playerData[playerNumber].depositData(data);

                    break;
                }
            }
        }
    }
}
