using UnityEngine;
using Photon.Pun;

public class ShowCorrectUILocationMap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        int raidPlayerID = GameObject.Find("GameManager").GetComponent<GameState>().raidPlayer.ActorNumber;
        if (PhotonNetwork.LocalPlayer.ActorNumber == raidPlayerID)
        {
            GameObject.Find("WaitingStage").SetActive(false);
            GameObject.Find("GameManager").GetComponent<GameState>().opponentDone = true;
        }
        else
        {
            GameObject.Find("UI").SetActive(false);
            GameObject.Find("GameManager").GetComponent<GameState>().playerDone = true;
        }

    }

}
