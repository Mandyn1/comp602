using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.UIElements;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;
using ExitGames.Client.Photon;

public class CreateJoinRoom : MonoBehaviourPunCallbacks
{

    public GameObject roomStage;
    public GameObject waitingStage;

    public void SetUserName(TMP_InputField input)
    {
        PhotonNetwork.NickName = input.text;
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
}
