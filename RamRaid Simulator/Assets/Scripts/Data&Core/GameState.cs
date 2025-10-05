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

    public int currentRaidLocation;

    public int timer = 0;
    public int maxTimer;
    public float policeResponseTime;

    public Player raidPlayer;
    public Player policePlayer;


    void Start()
    {
        if (raidPlayer == null) GetPlayers();
        gameState = 0;
        ProgressGame();
    }

    public void Reset()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel("GameLoop");
    }

    public void GetPlayers()
    {
        this.raidPlayer = gameObject.GetComponent<PlayerData>().SendPlayer(1);
        this.policePlayer = gameObject.GetComponent<PlayerData>().SendPlayer(2);
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
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel("EndGame");
    }

    public void ProgressGame()
    {
        gameState++;

        if (PhotonNetwork.IsMasterClient)
        {
            if (gameState > 3)
            {
                gameObject.GetComponent<SendEvents>().NextRoundEvent(roundCounter > maxRounds);
            }
        }

        switch (gameState)
        {
            case 1:
                gameObject.GetComponent<ViewStorage>().HideAll();
                if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber) gameObject.GetComponent<ViewStorage>().raider_S1_LocationMap.SetActive(true);
                else gameObject.GetComponent<ViewStorage>().police_S1_CarPlacer.SetActive(true);
                break;
            case 2:
                gameObject.GetComponent<ViewStorage>().HideAll();
                if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber) gameObject.GetComponent<ViewStorage>().raider_S2_OutdoorArray[currentRaidLocation].SetActive(true);
                else gameObject.GetComponent<ViewStorage>().incomplete_GameStage.SetActive(true);
                break;
            case 3:
                gameObject.GetComponent<ViewStorage>().HideAll();
                if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayer.ActorNumber) gameObject.GetComponent<ViewStorage>().incomplete_GameStage.SetActive(true);
                else gameObject.GetComponent<ViewStorage>().incomplete_GameStage.SetActive(true);
                break;
            default:
                EndGame();
                break;
        }
    }
}
