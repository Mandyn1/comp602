using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Unity.VisualScripting;

public class ReceiveEvents : MonoBehaviour, IOnEventCallback
{
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == SendEvents.UpdateCurrentRaidLocationEventCode)
        {
            string currentRaidLocation = (string)photonEvent.CustomData;
            if (this.gameObject.name == "PlayerManager") this.gameObject.GetComponent<PlayerData>().currentRaidLocation = currentRaidLocation;
            else if (this.gameObject.name == "GameManager") this.gameObject.GetComponent<GameState>().stage2RaiderScene = currentRaidLocation;
        }
    }
}
