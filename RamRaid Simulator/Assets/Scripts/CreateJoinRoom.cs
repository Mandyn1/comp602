using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.UIElements;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;

public class CreateJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField input_UserName;

    // Creates and joins room
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(input_UserName.text + "'s Room", new RoomOptions() { MaxPlayers = 2, IsVisible = true, IsOpen = true }, TypedLobby.Default, null);
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
        print(PhotonNetwork.CountOfPlayersInRooms);
    }

    // Leaves current room and returns to lobby
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(false);
    }
}
