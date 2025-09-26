using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    public GameObject usernameStage;
    public GameObject loadingStage;

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

    // Moves user to lobby for room selection / creation
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("Connected to Server");
        loadingStage.SetActive(false);
        usernameStage.SetActive(true);
    }
}
