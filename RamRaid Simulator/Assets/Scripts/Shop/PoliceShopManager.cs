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
    public float totalCost = 0;
    public float balance;
    private int prevUpgradeCount = 0;
    private float prevCost = 0f;
    void Start()
    {
        //init the buttons from the confirm box and set their action listeners
        UnityEngine.UI.Button confirm = confirmBox.transform.Find("ConfirmButton").GetComponent<UnityEngine.UI.Button>();
        UnityEngine.UI.Button reset = confirmBox.transform.Find("ResetButton").GetComponent<UnityEngine.UI.Button>();

        confirm.onClick.AddListener(applyUpgrades);
        reset.onClick.AddListener(resetAllUpgrades);

        //clean the error message
        errorMessage.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //constantly update the text description for the stats
        TextMeshProUGUI totalStats = confirmBox.transform.Find("Stats").GetComponent<TextMeshProUGUI>();

        float currentCost = 0f;
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
        }
        else
        {
            //user does not have enough money, update err text
            errorMessage.text = "You Do Not Have Enough Money!!!";
        }
    }
}
