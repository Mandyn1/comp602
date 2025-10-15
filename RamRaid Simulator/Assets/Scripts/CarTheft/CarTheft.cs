using UnityEngine;

public class CarTheft : MonoBehaviour
{
    private bool playerInCarZone = false;
    private GameObject theftPlayer;

    [SerializeField] private GameObject miniGamePanel; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCarZone = true;
            theftPlayer = other.gameObject;
            Debug.Log("Player entered car theft zone");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCarZone = false;
            theftPlayer = null;
            Debug.Log("Player exited car theft zone");
        }
    }

    public void OnStealPressed()
    {

        if (playerInCarZone && miniGamePanel != null)
        {
            miniGamePanel.SetActive(true);
        }
    }
}

