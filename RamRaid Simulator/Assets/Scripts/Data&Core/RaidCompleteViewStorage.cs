using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RaidCompleteViewStorage : MonoBehaviour
{
    public TMP_Text player1NameText;
    public TMP_Text player2NameText;
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;
    public TMP_Text wasCaughtText;
    public TMP_Text cashTakeText;
    public GameObject playerWallet;

    public void UpdateText()
    {
        GameState gameState = GameObject.Find("GameManager").GetComponent<GameState>();
        Dictionary<int, Player> playerArray = PhotonNetwork.CurrentRoom.Players;

        cashTakeText.text = playerWallet.GetComponent<PlayerWallet>().balance.ToString();

        Player player1 = null;
        Player player2 = null;

        foreach(Player p in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (gameState.localPlayerNumber == p.ActorNumber) player1 = p;
            else player2 = p;
        }

        player1NameText.text = player1.NickName;
        player1ScoreText.text = gameState.playerData[player1.ActorNumber].score.ToString();

        player2NameText.text = player2.NickName;
        player2ScoreText.text = gameState.playerData[player2.ActorNumber].score.ToString();

        wasCaughtText.text = "Escaped";
    }
}
