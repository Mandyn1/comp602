using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public int gameState = 0;

    public int stage1RaiderSceneNumber;
    public int stage1PoliceSceneNumber;
    public int stage2RaiderSceneNumber;
    public int stage2PoliceSceneNumber;
    public int stage3RaiderSceneNumber;
    public int stage3PoliceSceneNumber;

    public int timer;


    void Awake()
    {
        if (FindObjectsOfType<PlayerData>().Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
    }

    public void PlayerSwap()
    {
        
    }

    public void EndGame()
    {
        
    }

    public void ProgressGame()
    {
        gameState++;

        switch (gameState)
        {
            case 1:
                if (PhotonNetwork.LocalPlayer.ActorNumber == PhotonNetwork.CurrentRoom.MasterClientId) SceneManager.LoadScene(stage1RaiderSceneNumber);
                else SceneManager.LoadScene(stage1PoliceSceneNumber);
                break;
            case 2:
                if (PhotonNetwork.LocalPlayer.ActorNumber == PhotonNetwork.CurrentRoom.MasterClientId) SceneManager.LoadScene(stage2RaiderSceneNumber);
                else SceneManager.LoadScene(stage2PoliceSceneNumber);
                break;
            case 3:
                if (PhotonNetwork.LocalPlayer.ActorNumber == PhotonNetwork.CurrentRoom.MasterClientId) SceneManager.LoadScene(stage3RaiderSceneNumber);
                else SceneManager.LoadScene(stage3PoliceSceneNumber);
                break;
            case 4:
                PlayerSwap();
                if (PhotonNetwork.LocalPlayer.ActorNumber != PhotonNetwork.CurrentRoom.MasterClientId) SceneManager.LoadScene(stage1RaiderSceneNumber);
                else SceneManager.LoadScene(stage1PoliceSceneNumber);
                break;
            case 5:
                if (PhotonNetwork.LocalPlayer.ActorNumber != PhotonNetwork.CurrentRoom.MasterClientId) SceneManager.LoadScene(stage2RaiderSceneNumber);
                else SceneManager.LoadScene(stage2PoliceSceneNumber);
                break;
            case 6:
                if (PhotonNetwork.LocalPlayer.ActorNumber != PhotonNetwork.CurrentRoom.MasterClientId) SceneManager.LoadScene(stage3RaiderSceneNumber);
                else SceneManager.LoadScene(stage3PoliceSceneNumber);
                break;
            default:
                EndGame();
                break;
        }
    }
}
