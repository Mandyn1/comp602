using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class GameState : MonoBehaviour
{
    public bool inEndGame = false; // Stops disconnect watcher from moving user to PlayerDisconnected scene allowing uninterrupted viewing of end game stats
    public int gameState = 0; // Counter for which stage to put the user in
    public int roundCounter = 0;
    public int maxRounds = 3; // How many rounds each user gets in each position
    public bool hasPlayerSwapped = false; // Swapped positions
    public bool playerWaiting = false; // Changed in PlayerNowWaitingEvent 

    public int currentRaidLocation; // Index for accessing raid location array

    public int timer = 0;
    public int maxTimer;
    public float policeResponseTime;

    public int localPlayerNumber; // PhotonNetwork.LocalPlayer.ActorNumber

    // Removes requirement to check ordering in PhotonNetwork.CurrentRoom.Players array
    // Key is ActorNumber, can access own data with localPlayerNumber
    public Dictionary<int, PlayerData> playerData; 

    void Start()
    {
        // True if first round
        if (playerData.Count == 0)
        {
            localPlayerNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            SetPlayers();
        }

        gameState = 0;
        ProgressGame();
    }

    // Move to a fresh instance of GameLoop scene for next round
    public void Reset()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // Force other player to change scenes with room host
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel("GameLoop");
    }

    // Called once at start of game
    public void SetPlayers()
    {
        // Populate hashmap
        playerData.Add(PhotonNetwork.CurrentRoom.Players[0].ActorNumber, new PlayerData());
        playerData.Add(PhotonNetwork.CurrentRoom.Players[1].ActorNumber, new PlayerData());

        // Get roles decided in Main Menu scene and destroy delivery container (no longer needed)
        if (PhotonNetwork.IsMasterClient)
        {
            string hostRole = "Raider";
            string otherRole = "Police";
            GameObject startingRoles = GameObject.Find("StartingRoles");

            if (startingRoles.GetComponent<StartingRoles>() != null)
            {
                hostRole = startingRoles.GetComponent<StartingRoles>().hostRole;
                otherRole = startingRoles.GetComponent<StartingRoles>().otherRole;

                Destroy(startingRoles);
            }

            // Sync position data with other user
            foreach (int playerNumber in playerData.Keys)
            {
                if (playerNumber == localPlayerNumber) gameObject.GetComponent<SendEvents>().SendStartingPositionsEvent(playerNumber, hostRole);
                else gameObject.GetComponent<SendEvents>().SendStartingPositionsEvent(playerNumber, otherRole);
            }
        }
    }

    // Swap positions of players, happens once per game
    public void PlayerSwap()
    {
        foreach (PlayerData player in playerData.Values)
        {
            if (player.position == "Raider") player.position = "Police";
            else player.position = "Raider";
        }
        hasPlayerSwapped = true;
    }

    // Display stats screen and allow for return to main menu at own time
    public void EndGame()
    {
        gameObject.GetComponent<SendEvents>().SendStatDataEvent(playerData[localPlayerNumber].gatherData(maxRounds));

        PlayerData p1 = null, p2 = null;

        foreach(int p in playerData.Keys)
        {
            if (p == localPlayerNumber) p1 = playerData[p];
            else p2 = playerData[p];
        }
        GameObject.Find("ViewStorage").GetComponent<EndStatViewStorage>().loadData(p1, p2);
        GameObject.Find("ViewStorage").GetComponent<StageViewStorage>().HideAll();
        GameObject.Find("ViewStorage").GetComponent<StageViewStorage>().endGame.SetActive(true);
        inEndGame = true;
    }

    // Check if users require swapping and if the game is over, then change display to new stage
    public void ProgressGame()
    {
        gameState++;

        var view = GameObject.Find("ViewStorage").GetComponent<StageViewStorage>();

        if (PhotonNetwork.IsMasterClient)
        {
            if (gameState > 3)
            {
                // Input checks if player swapping or game is finished, otherwise resets 
                gameObject.GetComponent<SendEvents>().NextRoundEvent(roundCounter > maxRounds);
            }
        }

        // Change user view to correct current stage
        switch (gameState)
        {
            case 1:
                view.HideAll();
                if (playerData[localPlayerNumber].position == "Raider") view.raider_S1_LocationMap.SetActive(true);
                else view.police_S1_CarPlacer.SetActive(true);
                break;
            case 2:
                view.HideAll();
                if (playerData[localPlayerNumber].position == "Raider") view.raider_S2_OutdoorArray[currentRaidLocation].SetActive(true);
                else view.incomplete_GameStage.SetActive(true);
                break;
            case 3:
                view.HideAll();
                if (playerData[localPlayerNumber].position == "Raider") view.incomplete_GameStage.SetActive(true);
                else view.incomplete_GameStage.SetActive(true);
                break;
            default:
                EndGame();
                break;
        }
    }
}
