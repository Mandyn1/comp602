using UnityEngine;

public class QuitGame : MonoBehaviour
{

    public void quit()
    {
        Application.Quit();
        print("Exiting Game");
    }
}
