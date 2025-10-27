using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CarMenu : MonoBehaviour
{
    [SerializeField] private GameObject carMenuUI;
    [SerializeField] private GameObject carInfoPanel;
    [SerializeField] private TMP_Text carInfoText;
    [SerializeField] private GameObject lockPickingPanel;//trigger minigame
    [SerializeField] private GameObject waitingScene;//mingame result switch to waiting scene
    [SerializeField] private GameObject carTheftScene;
    private string currentCarInfo;

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

    }
}

