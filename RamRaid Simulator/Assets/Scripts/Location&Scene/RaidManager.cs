using Unity.VisualScripting;
using UnityEngine;

public class RaidManager : MonoBehaviour
{
    //variables
    [Header("Raider and police interaction")]
    public double policeDistanceFromRaid;
    public int raidLocationNum;
    public GameObject policePlacer;
    private DistanceChecker distanceCheckerScript;
    public IndoorRaidSceneTransition sceneTransition;
    public GameObject bounds;
    public GameObject player;
    public bool useTestVals;
    public double testDistance;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the distance script from the police car placer

        if (policePlacer != null)
        {
            //assign the distance checker
            distanceCheckerScript = policePlacer.GetComponentInChildren<DistanceChecker>();
        }

        //get the map scene transition object of from map bound children
        sceneTransition = bounds.GetComponentInChildren<IndoorRaidSceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        //update the distance of the police from the raid scene using switch to check location manager is assigned to
        if (useTestVals) //for testing
        {
            policeDistanceFromRaid = testDistance;
        }
        else
        {
            switch (raidLocationNum)
            {
                case 1:
                    policeDistanceFromRaid = distanceCheckerScript.location1Time;
                    break;
                case 2:
                    policeDistanceFromRaid = distanceCheckerScript.location2Time;
                    break;
                case 3:
                    policeDistanceFromRaid = distanceCheckerScript.location3Time;
                    break;
                case 4:
                    policeDistanceFromRaid = distanceCheckerScript.location4Time;
                    break;
                case -444: //FOR TESTING
                    policeDistanceFromRaid = 0;
                    break;
                default:
                    policeDistanceFromRaid = -1; //invalid location has been used
                    break;
            }
        }

        //now that the update distance has been assigned, check the distance
        if (policeDistanceFromRaid <= 0)
        {
            //raider is to be caught
            raiderCaught();
        }
    }

    void raiderCaught()
    {
        //remove the waypoint, raider should not be able to exit 
        if (bounds != null)
        {
            //get the script component, and set the waypoint to false
            sceneTransition.wayPointActive = false;
        }

        //raider is caught, momey from wallet does not increment, remove the current earnings
        PlayerWallet wallet = player.GetComponent<PlayerWallet>();
        wallet.balance = 0;

        return; //exit the loop, no more updates needed
    }
}
