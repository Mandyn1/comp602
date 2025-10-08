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

    public GameObject roomStage;
    public GameObject waitingStage;
    public GameObject player1NameText;
    public GameObject player2NameText;
    public TMP_InputField nicknameInput;
    public GameObject startButton;
    public GameObject playerWaitingText;

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

    // Console printing for debugging
    public override void OnJoinedRoom()
    {
        print("Joined " + PhotonNetwork.CurrentRoom.Name);
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            print(p.NickName);
        }

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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            player2NameText.GetComponent<TextMeshProUGUI>().SetText(PhotonNetwork.PlayerList[1].NickName);
            playerWaitingText.SetActive(false);
            startButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel("GameLoop");
    }
}
