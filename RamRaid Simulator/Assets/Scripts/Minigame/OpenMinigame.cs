using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMinigame : MonoBehaviour
{
    public GameObject managerObject;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collides with the collision box
        if (!collision.CompareTag("Player")) return;

        managerObject.GetComponent<RaidMinigameManager>().currentItem = gameObject;
        managerObject.GetComponent<RaidMinigameManager>().ShowMenu();
    }
}
