using UnityEngine;

public class RaidMinigameManager : MonoBehaviour
{
    public GameObject currentItem;
    public GameObject miniGame;
    public GameObject menu;
    public GameObject player;

    public void ShowMenu()
    {
        player.GetComponent<PlayerMovement>().Freeze();
        menu.SetActive(true);
    }

    public void StartMinigame()
    {
        menu.SetActive(false);
        miniGame.SetActive(true);
    }

    public void didWin()
    {
        miniGame.GetComponentInChildren<LockPickingManger>().ResetMinigame();
        miniGame.SetActive(false);
        player.GetComponent<PlayerWallet>().balance += currentItem.GetComponent<LootableItem>().GetPayout();
        currentItem.SetActive(false);
        currentItem = null;
        player.GetComponent<PlayerMovement>().UnFreeze();
    }

    public void didLose()
    {
        miniGame.GetComponentInChildren<LockPickingManger>().ResetMinigame();
        currentItem.SetActive(false);
        currentItem = null;
        player.GetComponent<PlayerMovement>().UnFreeze();
    }
}
