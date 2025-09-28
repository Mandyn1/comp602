using Photon.Pun;
using UnityEngine;

public class FinishRaid : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collides with the collision box
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("GameManager").GetComponent<SendEvents>().UpdateScoreEvent(GameObject.Find("TestPlayer").GetComponent<PlayerWallet>().Balance, PhotonNetwork.LocalPlayer);
            GameObject.Find("GameManager").GetComponent<SendEvents>().FinishedRaidEvent();
        }
    }
}
