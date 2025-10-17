using UnityEngine;

public class QuitGame : MonoBehaviour
{

    // Stops program
    public void quit()
    {
        Application.Quit();
        print("Exiting Game");
    }
}
