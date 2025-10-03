using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CarMenu : MonoBehaviour
{
    [SerializeField] private GameObject carMenuUI;
    [SerializeField] private GameObject carInfoPanel;
    [SerializeField] private TMP_Text carInfoText;

    private string currentCarInfo;

    public void SetCar(string info)
    {
        currentCarInfo = info;
    }

    public void StealCar()
    {
        carMenuUI.SetActive(false);

        //Charles said he will handle the scene change
        //SceneManager.LoadScene("MiniGame_FollowMap");

    }
    public void Inspect() //not sure if needed
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
