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

    public string stage1RaiderScene = "SelectCarScene";
    public string stage1PoliceScene = "Car Placer";
    public string stage2RaiderScene;
    public string stage2PoliceScene = "PoliceWaiting";
    public string stage3RaiderScene = "Stage3";
    public string stage3PoliceScene = "Stage3";

    public int timer = 0;
    public int maxTimer;

    public Player raidPlayer;
    public Player policePlayer;

    public bool playerDone = false;
    public bool opponentDone = false;



    void Awake()
    {
        if (FindObjectsByType<GameState>(FindObjectsSortMode.None).Length > 1) Destroy(this.gameObject);
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
        GameObject.Find("PlayerManager").GetComponent<PlayerData>().GetPlayers();
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
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel("GameEnd");
    }

    public void ProgressGame()
    {
        gameState++;

        playerDone = false;
        opponentDone = false;

        if (gameState > 4)
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
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.LoadLevel("LocationMap");
                PhotonNetwork.AutomaticallySyncScene = false;
                break;

            case 2:
                PhotonNetwork.AutomaticallySyncScene = true;
                if (PhotonNetwork.IsMasterClient)
                {
                    if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber)
                    {
                        PhotonNetwork.LoadLevel(stage1PoliceScene);
                        PhotonNetwork.AutomaticallySyncScene = false;
                        PhotonNetwork.LoadLevel(stage1RaiderScene);
                    }
                    else
                    {
                        PhotonNetwork.LoadLevel(stage1RaiderScene);
                        PhotonNetwork.AutomaticallySyncScene = false;
                        PhotonNetwork.LoadLevel(stage1PoliceScene);
                    }
                }
                break;

            case 3:
                PhotonNetwork.AutomaticallySyncScene = true;
                if (PhotonNetwork.IsMasterClient)
                {
                    if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber)
                    {
                        PhotonNetwork.LoadLevel(stage2PoliceScene);
                        PhotonNetwork.AutomaticallySyncScene = false;
                        PhotonNetwork.LoadLevel("RaidScene_I_" + stage2RaiderScene);
                    }
                    else
                    {
                        PhotonNetwork.LoadLevel("RaidScene_I_" + stage2RaiderScene);
                        PhotonNetwork.AutomaticallySyncScene = false;
                        PhotonNetwork.LoadLevel(stage2PoliceScene);
                    }
                }
                break;
                
            case 4:
                PhotonNetwork.AutomaticallySyncScene = true;
                if (PhotonNetwork.IsMasterClient)
                {
                    if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber)
                    {
                        PhotonNetwork.LoadLevel(stage3PoliceScene);
                        PhotonNetwork.AutomaticallySyncScene = false;
                        PhotonNetwork.LoadLevel(stage3RaiderScene);
                    }
                    else
                    {
                        PhotonNetwork.LoadLevel(stage3RaiderScene);
                        PhotonNetwork.AutomaticallySyncScene = false;
                        PhotonNetwork.LoadLevel(stage3PoliceScene);
                    }
                }
                break;

            default:
                EndGame();
                break;
        }
    }
}
