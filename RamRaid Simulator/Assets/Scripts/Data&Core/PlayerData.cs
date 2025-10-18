using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Globalization;
using System.Collections.Generic;
using System;
using Photon.Pun.Demo.PunBasics;
using Unity.Mathematics;

public class PlayerData : MonoBehaviour
{
    public int score = 0;
    public int raiderBank = 0; // Money to spend in each position
    public int policeBank = 0;
    public string position = ""; // Police or Raider

    // For stats page
    public int raiderMoneySpent = 0;
    public int policeMoneySpent = 0;
    public int carsStolen = 0;
    public double totalTimeInRaid = 0;
    public double aveTimeInRaid = 0;
    public int containersLooted = 0;
    public int policeUnitsDeployed = 0;
    public double totalTimeToRaid = 0;
    public double aveTimeToRaid = 0;
    public int raiderItemsBought = 0;
    public int raiderItemsUsed = 0;
    public int policeItemsBought = 0;
    public int policeItemsUsed = 0;
    public int escapes = 0;
    public int captures = 0;

    // Create and return hashmap of stats data not shared between players
    public Dictionary<String, object> gatherData(int numOfRounds)
    {
        aveTimeToRaid = calculateAveTime(totalTimeToRaid, numOfRounds);
        aveTimeInRaid = calculateAveTime(totalTimeInRaid, numOfRounds);

        Dictionary<String, object> data = new Dictionary<string, object>
        {
            { "RaiderMoneyRemaining", raiderBank },
            { "PoliceMoneyRemaining", policeBank },
            { "RaiderMoneySpent", raiderMoneySpent },
            { "PoliceMoneySpent", policeMoneySpent },
            { "CarsStolen", carsStolen },
            { "AveTimeInRaid", aveTimeInRaid },
            { "ContainersLooted", containersLooted },
            { "PoliceUnitsDeployed", policeUnitsDeployed },
            { "AveTimeToRaid", aveTimeToRaid },
            { "RaiderItemsBought", raiderItemsBought },
            { "RaiderItemsUsed", raiderItemsUsed },
            { "PoliceItemsBought", policeItemsBought },
            { "PoliceItemsUsed", policeItemsUsed },
            { "Escapes", escapes },
            { "Captures", captures }
        };

        return data;
    }

    // Deposit received hashmap of stats data into correct variables for viewing in stats screen
    public void depositData(Dictionary<String, object> data)
    {
        raiderBank = (int)data["RaiderMoneyRemaining"];
        policeBank = (int)data["PoliceMoneyRemaining"];
        raiderMoneySpent = (int)data["RaiderMoneySpent"];
        policeMoneySpent = (int)data["PoliceMoneySpent"];
        carsStolen = (int)data["CarsStolen"];
        aveTimeInRaid = (double)data["AveTimeInRaid"];
        containersLooted = (int)data["ContainersLooted"];
        policeUnitsDeployed = (int)data["PoliceUnitsDeployed"];
        aveTimeToRaid = (double)data["AveTimeToRaid"];
        raiderItemsBought = (int)data["RaiderItemsBought"];
        raiderItemsUsed = (int)data["RaiderItemsUsed"];
        policeItemsBought = (int)data["PoliceItemsBought"];
        policeItemsUsed = (int)data["PoliceItemsUsed"];
        escapes = (int)data["Escapes"];
        captures = (int)data["Captures"];
    }

    // For display on stats screen
    private double calculateAveTime(double totalTime, int numOfRounds)
    {
        return Math.Round(totalTime / numOfRounds, 2);
    }
}
