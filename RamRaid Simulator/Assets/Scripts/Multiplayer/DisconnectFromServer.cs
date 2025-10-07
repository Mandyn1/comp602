using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class DisconnectFromServer : MonoBehaviourPunCallbacks
{

    // Disconnects user from server and returns to main menu
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();

        if (gameObject.name == "GameManager") Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");

        print("Disconnected from Server");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (gameObject.name == "GameManager") Destroy(gameObject);
        
        SceneManager.LoadScene("PlayerDisconnected");
    }
}
