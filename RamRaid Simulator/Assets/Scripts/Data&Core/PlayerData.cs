using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class PlayerData : MonoBehaviour
{
    public Player player1;
    public Player player2;

    public int player1Score;
    public int player2Score;

    public string currentRaidLocation;
    public float policeResponseTime;

    void Awake()
    {
        if (FindObjectsByType<PlayerData>(FindObjectsSortMode.None).Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
    }

    public void GetPlayers()
    {
        if (PhotonNetwork.CurrentRoom.MasterClientId == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            player1 = PhotonNetwork.CurrentRoom.Players.ElementAt(0).Value;
            player2 = PhotonNetwork.CurrentRoom.Players.ElementAt(1).Value;
        }
        else
        {
            player2 = PhotonNetwork.CurrentRoom.Players.ElementAt(0).Value;
            player1 = PhotonNetwork.CurrentRoom.Players.ElementAt(1).Value;
        }
    }

    public Player SendPlayer(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return player1;
            case 2:
                return player2;
            default:
                return null;
        }
    }
}
