using Photon.Pun.UtilityScripts;
using UnityEngine;

public class StageViewStorage : MonoBehaviour
{
    // All stage containers in gameloop scene
    public GameObject raider_S1_LocationMap;
    public GameObject raider_S1_CarTheft;
    public GameObject raider_S1_Waiting;
    public GameObject raider_S2_Indoor01;
    public GameObject raider_S2_OutdoorEnter;
    public GameObject police_S1_CarPlacer;
    public GameObject incomplete_GameStage;
    public GameObject endGame;
    public GameObject raidComplete;
    public GameObject raider_Shop;
    public GameObject police_Shop;

    // Goes through each stage container checking if they are active and disabling them
    public void HideAll()
    {
        if (raider_S1_LocationMap.activeInHierarchy) raider_S1_LocationMap.SetActive(false);
        if (raider_S1_CarTheft.activeInHierarchy) raider_S1_CarTheft.SetActive(false);
        if (raider_S1_Waiting.activeInHierarchy) raider_S1_Waiting.SetActive(false);
        if (raider_S2_Indoor01.activeInHierarchy) raider_S2_Indoor01.SetActive(false);
        if (raider_S2_OutdoorEnter.activeInHierarchy) raider_S2_OutdoorEnter.SetActive(false);
        if (police_S1_CarPlacer.activeInHierarchy) police_S1_CarPlacer.SetActive(false);
        if (incomplete_GameStage.activeInHierarchy) incomplete_GameStage.SetActive(false);
        if (endGame.activeInHierarchy) endGame.SetActive(false);
        if (raidComplete.activeInHierarchy) raidComplete.SetActive(false);
        if (police_Shop.activeInHierarchy) police_Shop.SetActive(false);
        if (raider_Shop.activeInHierarchy) raider_Shop.SetActive(false);
    }
}
