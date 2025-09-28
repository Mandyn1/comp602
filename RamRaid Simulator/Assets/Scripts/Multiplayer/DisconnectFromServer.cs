using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class DisconnectFromServer : MonoBehaviourPunCallbacks
{

    public GameObject waitingStage;
    public GameObject roomStage;
    public GameObject usernameStage;
    public GameObject loadingStage;
    public GameObject menuStage;

    // Disconnects user from server and returns to main menu
    public void Disconnect()
    {
        Destroy(GameObject.Find("PlayerManager"));
        Destroy(GameObject.Find("GameManager"));

        SceneManager.LoadScene("MainMenu");

        print("Disconnected from Server");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (SceneManager.GetActiveScene().name != "GameEnd") SceneManager.LoadScene("PlayerDisconnected");
    }
}
