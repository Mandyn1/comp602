using Photon.Pun.UtilityScripts;
using UnityEngine;

public class ViewStorage : MonoBehaviour
{
    // All stage containers in gameloop scene
    public GameObject raider_S1_LocationMap;
    public GameObject raider_S1_CarTheft;
    public GameObject raider_S1_Waiting;
    public GameObject raider_S2_Indoor01;
    public GameObject raider_S2_Indoor02;
    public GameObject raider_S2_Indoor03;
    public GameObject raider_S2_Indoor04;
    public GameObject raider_S2_Outdoor01;
    public GameObject raider_S2_Outdoor02;
    public GameObject raider_S2_Outdoor03;
    public GameObject raider_S2_Outdoor04;
    public GameObject police_S1_CarPlacer;
    public GameObject incomplete_GameStage;
    public GameObject endGame_Stats;

    public GameObject[] raider_S2_IndoorArray;
    public GameObject[] raider_S2_OutdoorArray;

    // On start populates indoor and outdoor arrays allowing access via an int value
    void Start()
    {
        raider_S2_IndoorArray[0] = raider_S2_Indoor01;
        raider_S2_IndoorArray[1] = raider_S2_Indoor02;
        raider_S2_IndoorArray[2] = raider_S2_Indoor03;
        raider_S2_IndoorArray[3] = raider_S2_Indoor04;

        raider_S2_OutdoorArray[0] = raider_S2_Outdoor01;
        raider_S2_OutdoorArray[1] = raider_S2_Outdoor02;
        raider_S2_OutdoorArray[2] = raider_S2_Outdoor03;
        raider_S2_OutdoorArray[3] = raider_S2_Outdoor04;
    }

    // Goes through each stage container checking if they are active and disabling them
    public void HideAll()
    {
        if (raider_S1_LocationMap.activeInHierarchy) raider_S1_LocationMap.SetActive(false);
        if (raider_S1_CarTheft.activeInHierarchy) raider_S1_CarTheft.SetActive(false);
        if (raider_S1_Waiting.activeInHierarchy) raider_S1_Waiting.SetActive(false);
        if (raider_S2_Indoor01.activeInHierarchy) raider_S2_Indoor01.SetActive(false);
        if (raider_S2_Indoor02.activeInHierarchy) raider_S2_Indoor02.SetActive(false);
        if (raider_S2_Indoor03.activeInHierarchy) raider_S2_Indoor03.SetActive(false);
        if (raider_S2_Indoor04.activeInHierarchy) raider_S2_Indoor04.SetActive(false);
        if (raider_S2_Outdoor01.activeInHierarchy) raider_S2_Outdoor01.SetActive(false);
        if (raider_S2_Outdoor02.activeInHierarchy) raider_S2_Outdoor02.SetActive(false);
        if (raider_S2_Outdoor03.activeInHierarchy) raider_S2_Outdoor03.SetActive(false);
        if (raider_S2_Outdoor04.activeInHierarchy) raider_S2_Outdoor04.SetActive(false);
        if (police_S1_CarPlacer.activeInHierarchy) police_S1_CarPlacer.SetActive(false);
        if (incomplete_GameStage.activeInHierarchy) incomplete_GameStage.SetActive(false);
        if (endGame_Stats.activeInHierarchy) endGame_Stats.SetActive(false);
    }
}
