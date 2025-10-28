using System.Collections.Generic;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class PoliceShopManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //boxes
    [Header("UI Boxes")]
    [SerializeField] private List<GameObject> upgradeBoxes;
    public GameObject confirmBox;
    private TextMeshProUGUI confirmDesc;

    //error display
    [Header("error message")]
    [SerializeField] private TextMeshProUGUI errorMessage;

    //manager variablers
    [Header("manager variables")]
    public int totalUpgrades = 0;
    public int totalCost = 0;
    private int balance;
    private int prevUpgradeCount = 0;
    private float prevCost = 0f;
    private GameState state;
    void Start()
    {
        //init the buttons from the confirm box and set their action listeners
        UnityEngine.UI.Button confirm = confirmBox.transform.Find("ConfirmButton").GetComponent<UnityEngine.UI.Button>();
        UnityEngine.UI.Button reset = confirmBox.transform.Find("ResetButton").GetComponent<UnityEngine.UI.Button>();

        confirm.onClick.AddListener(applyUpgrades);
        reset.onClick.AddListener(resetAllUpgrades);

        //clean the error message
        errorMessage.text = "";

        //get the game manager game state script to retrive the polices wallet/bank
        GameObject gameManager = GameObject.Find("GameManager");

        //check if game manager exists, obviously it will but for saftey
        if(gameManager != null)
        {
            //get the script to grab the values from the dictronary
            state = gameManager.GetComponent<GameState>();

            //do saftey check again, if exists assign values
            if(state != null)
            {
                //get the bank
                balance = state.playerData[state.localPlayerNumber].policeBank;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //constantly update the text description for the stats
        TextMeshProUGUI totalStats = confirmBox.transform.Find("Stats").GetComponent<TextMeshProUGUI>();

        int currentCost = 0;
        int currentUpgrade = 0;
        //loop through each upgrade box to get stats
        foreach (GameObject box in upgradeBoxes)
        {
            //fetch values
            upgradeBoxManager boxScript = box.GetComponent<upgradeBoxManager>();
            currentUpgrade += boxScript.currentUpgradeLevel;
            currentCost += boxScript.totalCost;
        }

        //now increment values if any changes has been made
        if(totalUpgrades != currentUpgrade && totalCost != currentCost)
        {
            //update values
            totalUpgrades = currentUpgrade;
            totalCost = currentCost;

            //clean the err message
            errorMessage.text = "";
        }

        //display the text
        totalStats.text = "Total Upgrades: " + totalUpgrades + "\n" +
            "Total Cost: " + totalCost + "\n" +
            "Your Balance: " + balance;
    }

void initboxes()
    {
        //this is so cursed but entire script relies on the list of boxes so sticking with it
        foreach (GameObject box in upgradeBoxes){
            upgradeBoxManager boxScript = box.GetComponent<upgradeBoxManager>();

            //init the correct values to the boxes
            if (boxScript.boxID == 1) //add polioce unit
            {
                boxScript.currentUpgradeLevel = state.playerData[state.localPlayerNumber].addPoliceUnit;
            }
            else if (boxScript.boxID == 2) //faster uniut
            {
                boxScript.currentUpgradeLevel = state.playerData[state.localPlayerNumber].fasterPoliceUnit;
            }
            else if(boxScript.boxID == 3) //better modidier
            {
                boxScript.currentUpgradeLevel = state.playerData[state.localPlayerNumber].betterModifierPolice;
            }

        }
    }
    void resetAllUpgrades()
    {
        //loop through all upgrade boxes and reset the total cost and upgrade counter
        foreach (GameObject box in upgradeBoxes)
        {
            //get the script and then edit values
            upgradeBoxManager boxScript = box.GetComponent<upgradeBoxManager>();
            boxScript.currentUpgradeLevel = 0;
            boxScript.totalCost = 0;

        }

        //clear erro message
        errorMessage.text = "";
    }
    
    void applyUpgrades()
    {
        //first check if user has enough money to purchse
        if (balance >= totalCost)
        {
            //upgrade
            Debug.Log("Items upgraded"); //for testing sake, TEMP

            //deduct charge on polices wallet
            if (state != null)
            {
                balance -= totalCost;
                state.playerData[state.localPlayerNumber].policeBank = balance;
            }
            
            //now update the variablers in the player manager
            foreach (GameObject box in upgradeBoxes){
                upgradeBoxManager boxScript = box.GetComponent<upgradeBoxManager>();

                //assign the correct values
                if (boxScript.boxID == 1) //add polioce unit
                {
                    state.playerData[state.localPlayerNumber].addPoliceUnit = boxScript.currentUpgradeLevel;
                }
                else if (boxScript.boxID == 2) //faster uniut
                {
                    state.playerData[state.localPlayerNumber].fasterPoliceUnit = boxScript.currentUpgradeLevel;
                }
                else if(boxScript.boxID == 3) //better modidier
                {
                    state.playerData[state.localPlayerNumber].betterModifierPolice = boxScript.currentUpgradeLevel;
                }

            }
        }
        else
        {
            //user does not have enough money, update err text
            errorMessage.text = "You Do Not Have Enough Money!!!";
        }
    }
}
