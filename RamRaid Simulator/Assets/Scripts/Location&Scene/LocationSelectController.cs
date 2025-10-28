using System;
using System.Security.Cryptography;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelectController : MonoBehaviour
{

    [Header("Identity (for future UI)")]
    public int locationId;
    public string displayName = "Unnamed Location";

    [Header("Rewards / Difficulty")]
    [Tooltip("Additional multiplier applied to all item payouts in this location.")]
    [Min(0f)] public float rewardMultiplier;

    [Tooltip("For later: use to scale minigame difficulty, patrol density, etc.")]
    [Range(0, 5)] public int difficultyTier = 1;

    //Selects location, sends info to server, and moves player to next step in stage (car steal)
    public void SelectLocation()
    {
        GameObject.Find("GameManager").GetComponent<SendEvents>().UpdateCurrentRaidLocationEvent(locationId, rewardMultiplier);

        GameObject.Find("ViewStorage").GetComponent<StageViewStorage>().HideAll();
        GameObject.Find("ViewStorage").GetComponent<StageViewStorage>().raider_S1_CarTheft.SetActive(true);
    }

}
