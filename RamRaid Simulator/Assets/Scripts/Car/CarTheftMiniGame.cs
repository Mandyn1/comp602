using UnityEngine;
using UnityEngine.SceneManagement;

public class CarTheftMiniGame : MonoBehaviour
{
    [SerializeField] private string waitingSceneName = "WaitingScene";

    public void OnWin()
    {
        PlayerPrefs.SetString("Vehicle", "Car");
        PlayerPrefs.Save();
        GameObject.Find("ViewStorage").GetComponent<StageViewStorage>().HideAll();
        GameObject.Find("ViewStorage").GetComponent<StageViewStorage>().raider_S1_Waiting.SetActive(true);
    }

    public void OnLose()
    {
        PlayerPrefs.SetString("Vehicle", "Motorbike");
        PlayerPrefs.Save();
        GameObject.Find("ViewStorage").GetComponent<StageViewStorage>().HideAll();
        GameObject.Find("ViewStorage").GetComponent<StageViewStorage>().raider_S1_Waiting.SetActive(true);
    }
}

