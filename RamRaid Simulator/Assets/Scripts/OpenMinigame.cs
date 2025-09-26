using UnityEngine;

public class OpenMinigame : MonoBehaviour
{
    public GameObject game;
    private BasicMinigameController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = game.GetComponent<BasicMinigameController>();
        if (controller == null)
        {
            Debug.LogError("BasicMinigameController component is not found on the game object!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collides with the collision box
        if (collision.gameObject.CompareTag("Player"))
        {
            //set the minigame active and freeze players movement
            controller.Open();

        }
    }
}
