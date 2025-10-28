using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;


public class CarMenu : MonoBehaviour
{
    [SerializeField] private GameObject carMenuUI;
    [SerializeField] private GameObject carInfoPanel;
    [SerializeField] private TMP_Text carInfoText;
    [SerializeField] private GameObject lockPickingPanel;//trigger minigame
    [SerializeField] private GameObject waitingScene;//mingame result switch to waiting scene
    [SerializeField] private GameObject carTheftScene;

    public GameObject player;
    private PlayerMovement move;
    private LockPickingManger minigameManager;
    private string currentCarInfo;
    private Boolean isAvailable;

    void Start()
    {
        //for menu ui
        isAvailable = false;

        //get the movement script from the player
        if (player != null)
        {
            move = player.GetComponent<PlayerMovement>();
        }

        //get the manager script fropm the lopcking picking scene
        if (lockPickingPanel != null)
        {
            minigameManager = lockPickingPanel.GetComponentInChildren<LockPickingManger>();
        }
    }


    void Update()
    {
        //constatly check for user has picked the car
        if (minigameManager.hasPicked)
        {
            OnLockpickWin();
        }
        else if (minigameManager.hasFailed)
        {
            OnLockpickLose();
        }
    }
    
    public void SetCar(string info)
    {
        currentCarInfo = info;
    }

    public void StealCar()
    {
        Debug.Log("Opening minigame");

        // hide car menu
        carMenuUI.SetActive(false);
        
        // show the new lock-picking minigame
        if (lockPickingPanel != null)
            lockPickingPanel.SetActive(true);
        else
            Debug.LogWarning("minigame fail!");
    }

    //steal car result
    public void OnLockpickWin()
    {
        Debug.Log("WIN - Assigning car.");

        // Hide minigame UI
        lockPickingPanel.SetActive(false);

        // Set PlayerPrefs so VehicleManager knows what to spawn
        PlayerPrefs.SetString("Vehicle", "Car");
        PlayerPrefs.Save();

        // Enable the waiting scene
        waitingScene.SetActive(true);

        // Disable the car theft scene
        if (carTheftScene != null)
            carTheftScene.SetActive(false);
    }

    public void OnLockpickLose()
    {
        Debug.Log("FAIL - Assigning motorbike.");

        lockPickingPanel.SetActive(false);

        PlayerPrefs.SetString("Vehicle", "Motorbike");
        PlayerPrefs.Save();

        waitingScene.SetActive(true);


        // Disable the car theft scene
        if (carTheftScene != null)
            carTheftScene.SetActive(false);
    }
    public void Inspect() 
    {
        if (!string.IsNullOrEmpty(currentCarInfo))
        {
            carInfoPanel.SetActive(true);
            carInfoText.text = currentCarInfo;  
            
        }

    }

    public void CloseInfoPanel()
    {
        carInfoPanel.SetActive(false);
    }

    public void Cancel()
    {
        carMenuUI.SetActive(false);

        //player is now back in the scene, make them move again
        if(move != null)
        {
            move.UnFreeze();
        }
    }
}

