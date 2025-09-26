using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelectController : MonoBehaviour
{
    public void spawnToLocation(string location)
    {
        //transition to the location user selected on the map, first check if selected scene exits
        if (!string.IsNullOrEmpty(location))
        {
            //inform players
            GameObject.Find("PlayerManager").GetComponent<SendEvents>().UpdateCurrentRaidLocationEvent(location);

            //switch to scene
            SceneManager.LoadScene(location);
        }
        else
        {
            //print out err
            Debug.Log("Scene does not exist");
        }
    }

    public void selectRaidLocation(string location)
    {
        //transition to the location user selected on the map, first check if selected scene exits
        if (!string.IsNullOrEmpty(location))
        {
            //inform managers
            GameObject.Find("PlayerManager").GetComponent<SendEvents>().UpdateCurrentRaidLocationEvent(location);
            GameObject.Find("GameManager").GetComponent<SendEvents>().UpdateCurrentRaidLocationEvent(location);

            //switch to scene
            SceneManager.LoadScene("SelectCarScene");
        }
        else
        {
            //print out err
            Debug.Log("Scene does not exist");
        }
    }

}
