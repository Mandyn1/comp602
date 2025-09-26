using UnityEngine;
using Photon.Pun;
using TMPro;

public class FinalScores : MonoBehaviour
{

    public GameObject player1NameText;
    public GameObject player2NameText;
    public GameObject player1ScoreText;
    public GameObject player2ScoreText;

    // On initialisation display playernames and scores
    void Start()
    {
        player1NameText.GetComponent<TextMeshProUGUI>().SetText(this.gameObject.GetComponent<PlayerData>().player1.NickName);
        player2NameText.GetComponent<TextMeshProUGUI>().SetText(this.gameObject.GetComponent<PlayerData>().player2.NickName);

        player1ScoreText.GetComponent<TextMeshProUGUI>().SetText(this.gameObject.GetComponent<PlayerData>().player1Score.ToString());
        player2ScoreText.GetComponent<TextMeshProUGUI>().SetText(this.gameObject.GetComponent<PlayerData>().player2Score.ToString());
    }
}
