using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameState : MonoBehaviour
{
    public int gameState = 0;
    public int roundCounter = 0;
    public int maxRounds = 3;
    public bool hasPlayerSwapped = false;

    public int stage1RaiderSceneNumber;
    public int stage1PoliceSceneNumber;
    public int stage2RaiderSceneNumber;
    public int stage2PoliceSceneNumber;
    public int stage3RaiderSceneNumber;
    public int stage3PoliceSceneNumber;

    public int timer;

    public Player raidPlayer;
    public Player policePlayer;


    void Awake()
    {
        if (FindObjectsOfType<PlayerData>().Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
    }

    public void GetPlayers()
    {
        this.raidPlayer = PlayerData.GetPlayer(1);
        this.policePlayer = PlayerData.GetPlayer(2);
    }

    public void PlayerSwap()
    {
        Player temp = raidPlayer;
        raidPlayer = policePlayer;
        policePlayer = temp;
        hasPlayerSwapped = true;
    }

    public void EndGame()
    {
        
    }

    public void ProgressGame()
    {
        gameState++;

        if (gameState > 3)
        {
            roundCounter++;
            gameState = 1;
        }
        if (roundCounter > maxRounds)
        {
            if (hasPlayerSwapped) EndGame();
            else
            {
                PlayerSwap();
                roundCounter = 1;
            }
        }


        switch (gameState)
        {
            case 1:
                if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber) SceneManager.LoadScene(stage1RaiderSceneNumber);
                else SceneManager.LoadScene(stage1PoliceSceneNumber);
                break;
            case 2:
                if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber) SceneManager.LoadScene(stage2RaiderSceneNumber);
                else SceneManager.LoadScene(stage2PoliceSceneNumber);
                break;
            case 3:
                if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber) SceneManager.LoadScene(stage3RaiderSceneNumber);
                else SceneManager.LoadScene(stage3PoliceSceneNumber);
                break;
            default:
                EndGame();
                break;
        }
    }
}
