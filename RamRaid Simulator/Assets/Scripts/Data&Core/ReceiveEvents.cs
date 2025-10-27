using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Unity.VisualScripting;
using System;
using System.Collections.Generic;

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

    // Registers events in scene across users and completes required logic for each occurence
    // Mostly used for syncing data between users
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

            gameObject.GetComponent<GameState>().playerData[player.ActorNumber].score += score;
        }

        // Next Round Event, checks if players need swapping or the game requires ending
        else if (eventCode == SendEvents.NextRoundEventCode)
        {
            if ((bool)photonEvent.CustomData) // roundCounter > maxRounds
            {
                if (gameObject.GetComponent<GameState>().hasPlayerSwapped) gameObject.GetComponent<GameState>().EndGame();
                else
                {
                    gameObject.GetComponent<GameState>().PlayerSwap();
                }

                gameObject.GetComponent<GameState>().roundCounter = 0;
                gameObject.GetComponent<GameState>().Reset();
            }
        }

        // Send starting (player) positions (police or raider). Runs once per player
        else if (eventCode == SendEvents.SendStartingPositionsEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            gameObject.GetComponent<GameState>().playerData[(int)data[0]].position = (string)data[1];
        }

        // Player waiting event, starts next phase if a player is already waiting
        else if (eventCode == SendEvents.PlayerNowWaitingEventCode)
        {
            if (!gameObject.GetComponent<GameState>().playerWaiting) gameObject.GetComponent<GameState>().playerWaiting = true;
            else
            {
                gameObject.GetComponent<GameState>().playerWaiting = false;
                gameObject.GetComponent<GameState>().ProgressGame();
            }
        }

        // Recieves and deposits stat data for other player that is otherwise not collected over course of game play
        else if (eventCode == SendEvents.SendStatDataEventCode)
        {
            Dictionary<String, object> data = (Dictionary<String, object>)photonEvent.CustomData;

            foreach (int playerNumber in gameObject.GetComponent<GameState>().playerData.Keys)
            {
                if (playerNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    gameObject.GetComponent<GameState>().playerData[playerNumber].depositData(data);
                }
            }
        }

        // Recieves the beginning time for police response time from police player for the raid player
        else if (eventCode == SendEvents.SendPoliceTimeEventCode)
        {
            float time = (float)photonEvent.CustomData;
            gameObject.GetComponent<GameState>().policeResponseTime = time;
        }
    }
}
