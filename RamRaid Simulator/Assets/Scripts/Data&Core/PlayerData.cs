using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerData : MonoBehaviour
{

    public PhotonView photonView;
    public Player player1;
    public Player player2;

    public int player1Score;
    public int player2Score;

    public string currentRaidLocation;
    public float policeResponseTime;

    void Awake()
    {
        if (FindObjectsOfType<PlayerData>().Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
    }

    public void GetPlayers()
    {
        player1 = PhotonNetwork.CurrentRoom.GetPlayer(0);
        player2 = PhotonNetwork.CurrentRoom.GetPlayer(1);
    }

    public Player SendPlayer(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return player1;
            case 2:
                return player2;
        }
    }
}
