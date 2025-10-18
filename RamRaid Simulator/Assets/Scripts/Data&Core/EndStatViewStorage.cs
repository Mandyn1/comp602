using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class EndStatViewStorage : MonoBehaviour
{
    // Tie text
    public GameObject tie;

    // Access to text objects for stats scroll panel
    // Player 1 (Left)
    public GameObject p1_winner;
    public TMP_Text p1_name;
    public TMP_Text p1_Score;
    public TMP_Text p1_Escapes;
    public TMP_Text p1_SpentRaider;
    public TMP_Text p1_CashRemainingRaider;
    public TMP_Text p1_CarsStolen;
    public TMP_Text p1_ContainersLooted;
    public TMP_Text p1_TimeInRaid;
    public TMP_Text p1_ItemsBoughtRaider;
    public TMP_Text p1_ItemsUsedRaider;
    public TMP_Text p1_Captures;
    public TMP_Text p1_SpentPolice;
    public TMP_Text p1_CashRemainingPolice;
    public TMP_Text p1_UnitsDeployed;
    public TMP_Text p1_TimeToRaid;
    public TMP_Text p1_ItemsBoughtPolice;
    public TMP_Text p1_ItemsUsedPolice;

    // Player 2 (Right)
    public GameObject p2_winner;
    public TMP_Text p2_name;
    public TMP_Text p2_Score;
    public TMP_Text p2_Escapes;
    public TMP_Text p2_SpentRaider;
    public TMP_Text p2_CashRemainingRaider;
    public TMP_Text p2_CarsStolen;
    public TMP_Text p2_ContainersLooted;
    public TMP_Text p2_TimeInRaid;
    public TMP_Text p2_ItemsBoughtRaider;
    public TMP_Text p2_ItemsUsedRaider;
    public TMP_Text p2_Captures;
    public TMP_Text p2_SpentPolice;
    public TMP_Text p2_CashRemainingPolice;
    public TMP_Text p2_UnitsDeployed;
    public TMP_Text p2_TimeToRaid;
    public TMP_Text p2_ItemsBoughtPolice;
    public TMP_Text p2_ItemsUsedPolice;

    public void loadData(PlayerData p1_Data, PlayerData p2_Data)
    {
        // Check if data provided correctly
        if (p1_Data == null || p2_Data == null) return;

        // Load names
        foreach (Player p in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (p.ActorNumber == gameObject.GetComponent<GameState>().localPlayerNumber) p1_name.text = p.NickName;
            else p2_name.text = p.NickName;
        }
        
        // Load player 1 data
        p1_Score.text = p1_Data.score.ToString();
        p1_Escapes.text = p1_Data.escapes.ToString();
        p1_SpentRaider.text = p1_Data.raiderMoneySpent.ToString();
        p1_CashRemainingRaider.text = p1_Data.raiderBank.ToString();
        p1_CarsStolen.text = p1_Data.carsStolen.ToString();
        p1_ContainersLooted.text = p1_Data.containersLooted.ToString();
        p1_TimeInRaid.text = p1_Data.aveTimeInRaid.ToString();
        p1_ItemsBoughtRaider.text = p1_Data.raiderItemsBought.ToString();
        p1_ItemsUsedRaider.text = p1_Data.raiderItemsUsed.ToString();
        p1_Captures.text = p1_Data.captures.ToString();
        p1_SpentPolice.text = p1_Data.policeMoneySpent.ToString();
        p1_CashRemainingPolice.text = p1_Data.policeBank.ToString();
        p1_UnitsDeployed.text = p1_Data.policeUnitsDeployed.ToString();
        p1_TimeToRaid.text = p1_Data.aveTimeToRaid.ToString();
        p1_ItemsBoughtPolice.text = p1_Data.policeItemsBought.ToString();
        p1_ItemsUsedPolice.text = p1_Data.policeItemsUsed.ToString();

        // Load player 2 data
        p2_Score.text = p2_Data.score.ToString();
        p2_Escapes.text = p2_Data.escapes.ToString();
        p2_SpentRaider.text = p2_Data.raiderMoneySpent.ToString();
        p2_CashRemainingRaider.text = p2_Data.raiderBank.ToString();
        p2_CarsStolen.text = p2_Data.carsStolen.ToString();
        p2_ContainersLooted.text = p2_Data.containersLooted.ToString();
        p2_TimeInRaid.text = p2_Data.aveTimeInRaid.ToString();
        p2_ItemsBoughtRaider.text = p2_Data.raiderItemsBought.ToString();
        p2_ItemsUsedRaider.text = p2_Data.raiderItemsUsed.ToString();
        p2_Captures.text = p2_Data.captures.ToString();
        p2_SpentPolice.text = p2_Data.policeMoneySpent.ToString();
        p2_CashRemainingPolice.text = p2_Data.policeBank.ToString();
        p2_UnitsDeployed.text = p2_Data.policeUnitsDeployed.ToString();
        p2_TimeToRaid.text = p2_Data.aveTimeToRaid.ToString();
        p2_ItemsBoughtPolice.text = p2_Data.policeItemsBought.ToString();
        p2_ItemsUsedPolice.text = p2_Data.policeItemsUsed.ToString();

        // Check who won
        if (p1_Data.score > p2_Data.score) p1_winner.SetActive(true);
        else if (p1_Data.score < p2_Data.score) p2_winner.SetActive(true);
        else tie.SetActive(true);
    }
}
