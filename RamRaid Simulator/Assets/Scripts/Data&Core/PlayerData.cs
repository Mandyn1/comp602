using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Globalization;
using System.Collections.Generic;
using System;
using ExitGames.Client.Photon.StructWrapping;

public class PlayerData : MonoBehaviour
{
    public int score;
    public int raiderBank;
    public int policeBank;
    public string position;

    // For stats page
    public int raiderMoneySpent;
    public int policeMoneySpent;
    public int carsStolen;
    public float timeInRaid;
    public int locationsLooted;
    public int policeUnitsDeployed;
    public float aveTimeToRaid;
    public int raiderItemsBought;
    public int raiderItemsUsed;
    public int policeItemsBought;
    public int policeItemsUsed;

    public Dictionary<String, object> gatherData()
    {
        Dictionary<String, object> data = new Dictionary<string, object>
        {
            { "Score", score },
            { "RaiderMoneyRemaining", raiderBank },
            { "PoliceMoneyRemaining", policeBank },
            { "RaiderMoneySpent", raiderMoneySpent },
            { "PoliceMoneySpent", policeMoneySpent },
            { "CarsStolen", carsStolen },
            { "TimeInRaid", timeInRaid },
            { "LocationsLooted", locationsLooted },
            { "PoliceUnitsDeployed", policeUnitsDeployed },
            { "AveTimeToRaid", aveTimeToRaid },
            { "RaiderItemsBought", raiderItemsBought },
            { "RaiderItemsUsed", raiderItemsUsed },
            { "PoliceItemsBought", policeItemsBought },
            { "PoliceItemsUsed", policeItemsUsed }
        };

        return data;
    }

    public void depositData(Dictionary<String, object> data)
    {
        raiderMoneySpent = (int) data["RaiderMoneySpent"];
    }
}
