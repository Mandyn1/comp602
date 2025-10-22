using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class upgradeBoxManager : MonoBehaviour
{
    //variables
    //buttons
    [Header("Buttons")]
    public UnityEngine.UI.Button upgradeButton;
    public UnityEngine.UI.Button undoButton;

    //item stats
    [Header("Item Stats")]
    public TextMeshProUGUI itemDesc;
    public TextMeshProUGUI upgradeDesc;
    public String itemName;
    public float itemCost;

    //upgrade manager
    [Header("Upgrade Manager Values")]
    public float totalCost = 0;
    public float perUpgradeCost;
    private int currentUpgradeLevel; //temp, for testing

    //upgrade counter

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign action listeners to buttons
        upgradeButton.onClick.AddListener(Upgrade);
        undoButton.onClick.AddListener(undoUpgrade);

        //will need to set the text:
        itemDesc.text = "Item: " + itemName + "\n" +
            "CurrentLevel: " + "REP" + "\n" +
            "Upgrade Cost: " + perUpgradeCost + "\n";

        //current upgrades
        currentUpgradeLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //update the upgrade description
        //temp, replace the string with replace to the respective values
        upgradeDesc.text = "Upgrade Level: " + currentUpgradeLevel + "  |  " + "Total Cost: " + totalCost;

    }

    void Upgrade()
    {
        //increment the upgrade counter and cost
        currentUpgradeLevel++;
        totalCost += perUpgradeCost;
    }
    
    void undoUpgrade()
    {
        //remove the upgrade
        if(currentUpgradeLevel > 0 && totalCost > 0)
        {
            currentUpgradeLevel--;
            totalCost -= perUpgradeCost;
        }
    }
}
