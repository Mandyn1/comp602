using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameState : MonoBehaviour
{
    private int gameState = 0;
    private int roundCounter = 0;
    public int maxRounds = 3;
    private bool hasPlayerSwapped = false;
    public bool hasGameStarted = false;

    public string stage1RaiderScene = "LocationMap";
    public string stage1PoliceScene;
    public string stage2RaiderScene;
    public string stage2PoliceScene;
    public string stage3RaiderScene;
    public string stage3PoliceScene;

    public int timer = 0;
    public int maxTimer;

    public Player raidPlayer;
    public Player policePlayer;


    void Awake()
    {
        if (FindObjectsOfType<PlayerData>().Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        hasGameStarted = true;
        GetPlayers();
        ProgressGame();
    }

    public void GetPlayers()
    {
        this.raidPlayer = GameObject.Find("PlayerManager").GetComponent<PlayerData>().SendPlayer(1);
        this.policePlayer = GameObject.Find("PlayerManager").GetComponent<PlayerData>().SendPlayer(2);
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
                if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber) SceneManager.LoadScene(stage1RaiderScene);
                else SceneManager.LoadScene(stage1PoliceScene);
                break;
            case 2:
                if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber) SceneManager.LoadScene(stage2RaiderScene);
                else SceneManager.LoadScene(stage2PoliceScene);
                break;
            case 3:
                if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber) SceneManager.LoadScene(stage3RaiderScene);
                else SceneManager.LoadScene(stage3PoliceScene);
                break;
            default:
                EndGame();
                break;
        }
    }
}
