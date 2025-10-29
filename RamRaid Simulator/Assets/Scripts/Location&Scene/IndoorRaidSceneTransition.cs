using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IndoorRaidSceneTransition : MonoBehaviour
{
    //variables 
    //public GameObject transitionTo;
    //public GameObject currentScene;

    public GameObject Player;
    private PlayerWallet wallet;

    [Header("Location")]
    public bool wayPointActive = true;
    public bool indoorLocation;
    public bool outdoorLocation;

    void Start()
    {
        // //need to disable the parsed in scene first so no obverlap
        // if (transitionTo != null)
        // {
        //     transitionTo.SetActive(false);
        // }
        // else
        // {
        //     //prinmt debiug
        //     Debug.Log("cannot set object as inacive (already null)");
        // }

        //if the location is indoor, get the players wallet
        if (indoorLocation)
        {
            wallet = Player.GetComponent<PlayerWallet>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collides with the collision box and input string scene is set
        if (collision.gameObject.CompareTag("Player") && wayPointActive)
        {
            print("Ending Raid");
            GameObject gameManger = GameObject.Find("GameManager");
            gameManger.GetComponent<GameState>().playerData[gameManger.GetComponent<GameState>().localPlayerNumber].raiderBank += wallet.balance;
            gameManger.GetComponent<SendEvents>().UpdateScoreEvent(wallet.balance, PhotonNetwork.LocalPlayer);
            gameManger.GetComponent<SendEvents>().PlayerNowWaitingEvent();
        //     //enable the object that is being transitioned too
        //     if (transitionTo != null)
        //     {
        //         //if the player is leaving the indoor location (fleeing, update the wallet to live variables)
        //         if (indoorLocation)
        //         {
        //             //update the players wallet
        //             GameObject gameManger = GameObject.Find("GameManager");

        //             //check if the game manager is null to be safe
        //             if(gameManger != null)
        //             {
        //                 //now fetch the sate script to get the live 'raiderBank' variable
        //                 GameState state = gameManger.GetComponent<GameState>();
        //                 if(state != null)
        //                 {
        //                     //add the wallet balance to the raiders bank
        //                     gameManger.GetComponent<GameState>().playerData[state.localPlayerNumber].raiderBank += wallet.balance;
        //                     gameManger.GetComponent<SendEvents>().UpdateScoreEvent(wallet.balance, PhotonNetwork.LocalPlayer);
        //                     gameManger.GetComponent<SendEvents>().PlayerNowWaitingEvent();
        //                 }
        //             }

        //             //log the output
        //             Debug.Log("Balance after Raid: " + wallet.balance);

        //         }

        //         transitionTo.SetActive(true); //enable transition area

        //         //now hide the current object that is active
        //         if (transitionTo.activeInHierarchy)
        //         {
        //             currentScene.SetActive(false);
        //         }

        //         wayPointActive = false;

        //     }
        //     else
        //     {
        //         //give debug err
        //         Debug.Log("Transition To Object to load is null");
        //     }
        // }
        // else
        // {
        //     if (!wayPointActive)
        //     {
        //         //user cannot exit because the waypoint has been deactiviated
        //         Debug.Log("WARN!: waypoint deactivated");
        //     }
        //     else
        //     {
        //         //print err log (change to usefull error eventually)
        //         Debug.Log("ERR!: wrong scene name");
        //     }
        }
    }
}
