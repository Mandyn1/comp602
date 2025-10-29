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

        player1NameText.text = playerArray[0].NickName;
        player1ScoreText.text = gameState.playerData[playerArray[0].ActorNumber].score.ToString();

        player2NameText.text = playerArray[1].NickName;
        player2ScoreText.text = gameState.playerData[playerArray[1].ActorNumber].score.ToString();

        wasCaughtText.text = "Escaped";
    }
}
