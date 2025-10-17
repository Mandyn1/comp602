using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.UIElements;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class CreateJoinRoom : MonoBehaviourPunCallbacks
{
    // Bits in Main Menu scene
    public GameObject roomStage;
    public GameObject waitingStage;
    public GameObject player1NameText;
    public GameObject player2NameText;
    public TMP_InputField nicknameInput;
    public GameObject startButton;
    public GameObject playerWaitingText;
    public GameObject swapRoleButton;
    public GameObject randomiseRoleButton;

    // Also resets currently disabled text fields in case user goes back to them
    public void SetUserName()
    {
        PhotonNetwork.NickName = nicknameInput.text;

        player1NameText.GetComponent<TextMeshProUGUI>().SetText("");
        player2NameText.GetComponent<TextMeshProUGUI>().SetText("");
        nicknameInput.text = "";
    }

    // Creates and joins room
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName + "'s Room", new RoomOptions() { MaxPlayers = 2, IsVisible = true, IsOpen = true }, TypedLobby.Default, null);
    }

    // Joins existing room
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    // Updates text fields and move to waiting stage
    public override void OnJoinedRoom()
    {
        // Console printing for debugging
        print("Joined " + PhotonNetwork.CurrentRoom.Name);
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            print(p.NickName);
        }

        // Updates text fields displaying player names
        player1NameText.GetComponent<TextMeshProUGUI>().SetText(PhotonNetwork.PlayerList[0].NickName);

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            player2NameText.GetComponent<TextMeshProUGUI>().SetText(PhotonNetwork.PlayerList[1].NickName);
        }

        roomStage.SetActive(false);
        waitingStage.SetActive(true);
    }

    // Leaves current room and returns to lobby
    public void LeaveRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LeaveRoom();
        print("Left Room");

        PhotonNetwork.JoinLobby();
    }

    // Updates player name text fields and allows game to start
    // Also allows positions to be swapped or randomised
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            player2NameText.GetComponent<TextMeshProUGUI>().SetText(PhotonNetwork.PlayerList[1].NickName);
            playerWaitingText.SetActive(false);
            startButton.SetActive(true);
            swapRoleButton.SetActive(true);
            randomiseRoleButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // Forces users to follow host to new scene
        PhotonNetwork.LoadLevel("GameLoop");
    }
}
