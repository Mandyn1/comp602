using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelectController : MonoBehaviour
{
    public void spawnToLocation(string location)
    {
        //transition to the location user selected on the map, first check if selected scene exits
        if (!string.IsNullOrEmpty(location))
        {
            //switch to scene
            SceneManager.LoadScene(location);
        }
        else
        {
            //print out err
            Debug.Log("Scene does not exist");
        }
    }

}
