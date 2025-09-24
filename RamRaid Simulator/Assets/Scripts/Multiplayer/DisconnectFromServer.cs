using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

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
        PhotonNetwork.Disconnect();

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainMenu")) SceneManager.LoadScene("MainMenu");
        else
        {
            waitingStage.SetActive(false);
            roomStage.SetActive(false);
            usernameStage.SetActive(false);
            loadingStage.SetActive(false);
            menuStage.SetActive(true);
        }

        print("Disconnected from Server");
    }
}
