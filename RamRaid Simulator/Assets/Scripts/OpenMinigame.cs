using UnityEngine;

public class OpenMinigame : MonoBehaviour
{
    public GameObject game;
    private BasicMinigameController controller;

    private LootableItem lootable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = game ? game.GetComponent<BasicMinigameController>() : null;
        if (controller == null) Debug.LogError("BasicMinigameController not found on 'game' object!");

        // Support both same-object and child setup
        lootable = GetComponent<LootableItem>();
        if (lootable == null) lootable = GetComponentInChildren<LootableItem>(true);
        if (lootable == null) Debug.LogError("LootableItem component not found on this item or its children!");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collides with the collision box
        if (!collision.CompareTag("Player")) return;

        var wallet = collision.GetComponent<PlayerWallet>();
        if (wallet == null)
        {
            Debug.LogError("Player does not have a PlayerWallet component!");
            return;
        }

        controller.currentTargetItem = lootable;
        controller.playerWallet = wallet;

        //set the minigame active and freeze players movement
        controller.Open();
    }
}
