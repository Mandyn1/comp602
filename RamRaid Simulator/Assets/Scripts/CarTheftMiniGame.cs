using UnityEngine;
using UnityEngine.SceneManagement;

public class CarTheftMiniGame : MonoBehaviour
{
    //[SerializeField] private string waitingSceneName = "WaitingScene";

    public void OnWin()
    {
        PlayerPrefs.SetString("Vehicle", "Car");
        PlayerPrefs.Save();
        //SceneManager.LoadScene(waitingSceneName);

        GameObject.Find("GameManager").GetComponent<SendEvents>().FinishedStageEvent();
    }

    public void OnLose()
    {
        PlayerPrefs.SetString("Vehicle", "Motorbike");
        PlayerPrefs.Save();
        //SceneManager.LoadScene(waitingSceneName);

        GameObject.Find("GameManager").GetComponent<SendEvents>().FinishedStageEvent();
    }
}

