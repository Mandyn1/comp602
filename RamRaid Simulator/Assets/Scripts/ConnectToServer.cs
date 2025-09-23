using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Connects user to Photon Multiplayer Engine / Pun 
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Connects user to lobby
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    // Moves user to lobby scene for room selection / creation
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("GameLobby");
    }

    // Disconnects user for returning to main menu
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
}
