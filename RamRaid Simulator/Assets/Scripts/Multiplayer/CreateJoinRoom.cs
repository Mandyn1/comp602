using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.UIElements;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class CreateJoinRoom : MonoBehaviourPunCallbacks
{

    public GameObject roomStage;
    public GameObject waitingStage;
    public GameObject startButton;
    public GameObject startWaitingText;
    public GameObject player1NameText;
    public GameObject player2NameText;
    public TMP_InputField nicknameInput;

    public void SetUserName()
    {
        PhotonNetwork.NickName = nicknameInput.text;

        player1NameText.GetComponent<TextMeshProUGUI>().SetText("");
        player2NameText.GetComponent<TextMeshProUGUI>().SetText("");
        startWaitingText.SetActive(false);
        startButton.SetActive(false);
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

        player1NameText.GetComponent<TextMeshProUGUI>().SetText(PhotonNetwork.PlayerList[0].NickName);

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            player2NameText.GetComponent<TextMeshProUGUI>().SetText(PhotonNetwork.PlayerList[1].NickName);

            if (PhotonNetwork.CurrentRoom.MasterClientId == PhotonNetwork.LocalPlayer.ActorNumber) startButton.SetActive(true);
            else startWaitingText.SetActive(true);
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

        if (!GameObject.Find("GameManager").GetComponent<GameState>().hasGameStarted)
        {
            player2NameText.GetComponent<TextMeshProUGUI>().SetText(PhotonNetwork.PlayerList[1].NickName);
            if (PhotonNetwork.CurrentRoom.MasterClientId == PhotonNetwork.LocalPlayer.ActorNumber) startButton.SetActive(true);
            else startWaitingText.SetActive(true);
        }
    }
}
