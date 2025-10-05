using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerData : MonoBehaviour
{
    public Player player1;
    public Player player2;

    public int player1Score;
    public int player2Score;

    public int player1Bank;
    public int player2Bank;

    public void GetPlayers()
    {
        player1 = PhotonNetwork.CurrentRoom.Players[0];
        player2 = PhotonNetwork.CurrentRoom.Players[1];
    }

    public void ResetBank()
    {
        player1Bank = 0;
        player2Bank = 0;
    }

    public Player SendPlayer(int playerNumber)
    {

        if (player1 == null) GetPlayers();

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
