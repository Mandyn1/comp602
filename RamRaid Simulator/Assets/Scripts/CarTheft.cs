using UnityEngine;
using UnityEngine.InputSystem;

public class CarTheft : MonoBehaviour
{
    private bool playerInCarZone = false;
    private GameObject theftPlayer;
    private bool menuOpen = false;

    [SerializeField] private CarTheftMiniGame miniGame; // assign in inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCarZone = true;
            theftPlayer = other.gameObject;
            Debug.Log("player entered car theft zone");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCarZone = false;
            theftPlayer = null;
            menuOpen = false;
            Debug.Log("Player exited car theft zone");
        }
    }

    void Update()
    {
        if (playerInCarZone && !menuOpen && Keyboard.current.sKey.wasPressedThisFrame)
        {
            menuOpen = true;
            Debug.Log("Car theft menu Inspect(I) - Steal(S) - Leave(Esc)");
        }

        if (menuOpen)
        {
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                Debug.Log("Vehicle model - Miata 1997");
            }
            else if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                menuOpen = false;
                miniGame.StartMiniGame(theftPlayer, gameObject);
            }
            else if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                menuOpen = false;
                Debug.Log("Car theft menu closed");
            }
        }
    }
}
